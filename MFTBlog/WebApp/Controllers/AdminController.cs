using BLL.Management;
using DAL.EF.Repository;
using DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Model;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
	public class AdminController : BaseController
	{
		public IPostManagement PostManagement { get; set; }
		public ITagManagement TagManagement { get; set; }
		public ICategoryManagement CategoryManagement { get; set; }
		public IUploadedFileManagement UploadedFileManagement { get; set; }
		public IUserManagement UserManagement { get; set; }
		public AdminController(IPostManagement postManagement, ITagManagement tagManagement, ICategoryManagement categoryManagement, IUploadedFileManagement uploadedFileManagement, IUserManagement userManagement)
		{
			this.PostManagement = postManagement;
			this.TagManagement = tagManagement;
			this.CategoryManagement = categoryManagement;
			this.UploadedFileManagement = uploadedFileManagement;
			this.UserManagement = userManagement;
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListUsers()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ListUsersData(int page, int perpage)
		{
			var users = await UserManagement.ListUsers(page, perpage);

			var tasks = users.Item1.Select(async user =>
			{
				user.PersianBirthDate = await ConvertToPersianDateTime(user.BirthDate);
				return user;
			}).ToList();

			await Task.WhenAll(tasks);

			return Json(new { successful = true, users = users.Item1, userscount = users.Item2 });
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				var result = await UserManagement.DeleteUserById(id);
				return Json(new { successful = true });
			}
			catch (Exception ex)
			{
				return Json(new { successful = false, message = "خطا رخ داده است!" });
			}
		}


		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListRoles()
		{
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListMenu()
		{
			return View();
		}
		
		#region Posts
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListPosts()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public IActionResult AddNewPost()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public IActionResult EditPost(int id)
		{
			ViewBag.PostId = id;

			return View("EditPost");
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult AddTag([FromBody] WebApp.ViewModel.TagViewModel tagViewModel)
		{
			try
			{
				TagManagement.AddTag(new BLL.Model.TagViewModel()
				{
					Id = tagViewModel.Id,
					Name = tagViewModel.Name,
				});

				return Json(new { success = true });
			}
			catch (Exception ex)
			{

				return Json(new { success = false, message = ex.Message });
			}
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetTags()
		{
			var tags = await TagManagement.GetTags();
			return Json(tags);
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListTags()
		{
			return View();
		}
		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteTag([FromBody] WebApp.ViewModel.TagViewModel tagViewModel)
		{
			try
			{
				var result = await TagManagement.DeleteTag(tagViewModel.Id);

				if (result.IsSuccessful)
				{
					return Json(new { success = true });
				}
				return Json(new { success = false, message = "برای حذف خطا رخ داده است!" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = "برای حذف خطا رخ داده است!" });
			}
		}
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateTag([FromBody] WebApp.ViewModel.TagUpdateViewModel tagViewModel)
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

				return Json(new { success = false, message = errorMsg });
			}

			var result = await TagManagement.UpdateTag(new BLL.Model.TagViewModel()
			{
				Id = tagViewModel.Id,
				Name = tagViewModel.Name,
			});

			if (result.IsSuccessful)
			{
				return Json(new { success = true });
			}

			return Json(new { success = false, message = "خطا رخ داده است.!" });
		}
		#endregion
		#region Categories
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListCategories()
		{
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]

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
		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			try
			{
				var result = await CategoryManagement.DeleteCategoryAsync(id);
				if (result.IsSuccessful)
				{
					return Json(new { successful = true });
				}
				return Json(new { successful = false, message = result.Message });
			}
			catch (Exception ex)
			{
				return Json(new { successful = false, message = "An error occurred while deleting the category." });
			}
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult AddNewCategory(int parentId)
		{
			ViewBag.ParentId = parentId;

			return View("AddNewCategory");
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddNewCategory([FromBody] WebApp.ViewModel.CategoryViewModel categoryViewModel)
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

			var result = await CategoryManagement.AddNewCategoryAsync(new BLL.Model.CategoryViewModel()
			{
				Name = categoryViewModel.Name,
				ParentCategoryId = categoryViewModel.ParentCategoryId,
			});

			if (result.IsSuccessful)
			{
				return Json(new { successful = true });
			}

			return Json(new { successful = false, message = result.Message });
		}
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateCategory([FromBody] WebApp.ViewModel.UpdateCategoryViewModel categoryViewModel)
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

			var result = await CategoryManagement.UpdateCategoryAsync(new BLL.Model.CategoryViewModel()
			{
				Id = categoryViewModel.Id,
				Name = categoryViewModel.Name,
				ParentCategoryId = categoryViewModel.ParentCategoryId
			});

			if (result.IsSuccessful)
			{
				return Json(new { successful = true });
			}

			return Json(new { successful = false, message = "خطا رخ داده است!" });
		}
		#endregion
		#region Files
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult ListFiles()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public IActionResult AddNewFile()
		{
			return View("AddNewFile");
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteFile(int id)
		{
			try
			{
				var result = await UploadedFileManagement.DeleteFileAsync(id);

				if (result.Exception is KeyNotFoundException)
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
