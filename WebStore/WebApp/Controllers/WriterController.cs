﻿using BLL.CMS.Management;
using BLL.CMS.Model;
using BLL.CMS.Model;
using DAL.CMS.EF;
using DAL.CMS.EF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
	public class WriterController : BaseController
	{
		public IPostManagement PostManagement { get; set; }
		public ITagManagement TagManagement { get; set; }
		public ICategoryManagement CategoryManagement { get; set; }
		public IUploadedFileManagement UploadedFileManagement { get; set; }
		public WriterController(IPostManagement postManagement, ITagManagement tagManagement, ICategoryManagement categoryManagement, IUploadedFileManagement uploadedFileManagement)
		{
			this.PostManagement = postManagement;
			this.TagManagement = tagManagement;
			this.CategoryManagement = categoryManagement;
			this.UploadedFileManagement = uploadedFileManagement;
        }
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult Index()
		{
			return View();
		}

		#region Posts
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListPosts()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> ListPostsData(int page, int perpage)
		{
			var posts = await PostManagement.ListPost(page, perpage);

			var tasks = posts.Item1.Select(async post =>
			{
				post.PersianInsertationDateTime = await ConvertToPersianDateTime(post.InsertationDateTime);
				return post;
			}).ToList();

			await Task.WhenAll(tasks);

			return Json(new { successful = true, posts = posts.Item1, postscount = posts.Item2 });
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult AddNewPost()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> AddNewPost([FromBody] AddNewPostViewModel addNewPostViewModel)
		{
			
			if (!ModelState.IsValid)
			{
				string errorMsg = "";

				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						errorMsg += error.ErrorMessage;
					}
				}

				return Json(new { successful = false, message = errorMsg });
			}

			var postViewModel = new PostViewModel
			{
				Title = addNewPostViewModel.Title,
				AbstractContent = addNewPostViewModel.AbstractContent,
				HtmlContent = addNewPostViewModel.HtmlContent,
				CategoryId = addNewPostViewModel.CategoryId,
				TagIdList = addNewPostViewModel.TagIdList,
				AuthorId = UserId
			};

			var result = await PostManagement.AddPost(postViewModel);

			if (result.IsSuccessful)
			{
				return Json(new { successful = true });
			}

			var errorMessage = result is ResultEntityViewModel<Exception> exceptionResult
				? exceptionResult.Message
				: "An error occurred while adding the post.";

			return Json(new { successful = false, message = errorMessage });
		}
		[HttpDelete]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeletePost(int id)
		{
			try
			{
				var result = await PostManagement.DeletePost(id);
				return Json(new { successful = true });
			}
			catch (Exception ex)
			{
				return Json(new { successful = false, message = "خطا رخ داده است!" });
			}
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> GetPostDetails(int id)
		{
			var post = await PostManagement.GetPostById(id);

			if (post == null)
			{
				return Json(new { successful = false, message = "Post not found." });
			}

			var postData = (ResultEntityViewModel<PostViewModel>)post;

			if (postData.IsSuccessful)
			{
				return Json(new { successful = true, post = postData.Entity });
			}

			return Json(new { successful = false });
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult EditPost(int id)
		{
			ViewBag.PostId = id;

			return View("EditPost");
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> UpdatePost([FromBody] PostViewModel postViewModel)
		{
			if (!ModelState.IsValid)
			{
				string errorMsg = "";

				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						errorMsg += error.ErrorMessage;
					}
				}

				return Json(new { successful = false, message = errorMsg });
			}

			var result = await PostManagement.UpdatePost(postViewModel);

			if (result.IsSuccessful)
			{
				return Json(new { successful = true });
			}

			var errorMessage = result is ResultEntityViewModel<Exception> exceptionResult
				? exceptionResult.Message
				: "An error occurred while updating the post.";

			return Json(new { successful = false, message = errorMessage });
		}

		#endregion
		#region Tags
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> GetTags()
		{
			var tags = await TagManagement.GetTags();
			return Json(tags);
		}
		#endregion
		#region Categories
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await CategoryManagement.ListAllCategoriesAsync();
			
			var categoryList = categories.Select(c => new
			{
				Id = c.Id,
				ParentCategoryId = c.ParentCategoryId,
				Name = c.Name,
				HasChildren = categories.Any(sub => sub.ParentCategoryId == c.Id)  // Check if has subcategories
			}).ToList();

			return Json(categoryList);
		}
		#endregion
		#region Files
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListFiles()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> ListFilesData(int page, int perpage)
		{
			var files = await UploadedFileManagement.ListFiles(page, perpage);

			var tasks = files.Item1.Select(async file =>
			{
				file.PersianUploadDate = await ConvertToPersianDateTime(file.UploadedAt);
				return file;
			}).ToList();

			await Task.WhenAll(tasks);

			return Json(new { successful = true, files = files.Item1, filescount = files.Item2 });
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult AddNewFile()
		{
			return View("AddNewFile");
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> AddNewFile([FromForm] IFormFile files)
		{
			if (files != null && files.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					await files.CopyToAsync(memoryStream);

					var uploadedFileViewModel = new UploadedFileViewModel
					{
						Title = "",
						FileName = files.FileName,
						ContentType = files.ContentType,
						Data = memoryStream.ToArray(),
						UploadedAt = DateTime.Now
					};

					var result = await UploadedFileManagement.AddFileAsync(uploadedFileViewModel);

					if (result.IsSuccessful)
					{
						return Json(new { success = true });
					}
				}

				return Json(new { success = true });
			}

			return Json(new { success = false });
		}
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> ViewFile(int id)
		{
			var file = await UploadedFileManagement.GetFileByIdAsync(id);

			if (file == null)
			{
				return NotFound();
			}

			return File(file.Data, file.ContentType, file.Title);
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DownloadFile(int id)
		{
			var file = await UploadedFileManagement.GetFileByIdAsync(id);

			if (file == null)
			{
				return NotFound();
			}

			var contentDisposition = new System.Net.Mime.ContentDisposition
			{
				FileName = file.FileName,
				Inline = false
			};

			Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

			return File(file.Data, file.ContentType, file.FileName);
		}
		[HttpDelete]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeleteFile(int id)
		{
			try
			{
				var result = await UploadedFileManagement.DeleteFileAsync(id);

				if(result.Exception is KeyNotFoundException)
				{
					return Json(new { successful = false, message = "فایل یافت نشد!" });
				}

				if (result.IsSuccessful)
				{
					return Json(new { successful = true });
				}
				return Json(new { successful = false, message = result.Message });
			}
			catch (Exception ex)
			{
				return Json(new { successful = false, message = "خطا رخ داده است!" });
			}
		}

		#endregion
	}
}
