using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using DAL.CMS.EF;
using DAL.CMS.EF.Repository;
using BLL.CMS.Management;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add CMSContext as a database context
builder.Services.AddDbContext<CMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<DbContext>(options =>
//        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Management
builder.Services.AddScoped<ICategoryManagement, CategoryManagement>();
builder.Services.AddScoped<IMenuManagement, MenuManagement>();
builder.Services.AddScoped<IPostManagement, PostManagement>();
builder.Services.AddScoped<ITagManagement, TagManagement>();
builder.Services.AddScoped<IUploadedFileManagement, UploadedFileManagement>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<ISpecialConfigurationManagement, SpecialConfigurationManagement>();

//Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISpecialConfigurationRepository, SpecialConfigurationRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISpecialConfigurationRepository, SpecialConfigurationRepository>();

builder
    .Services
                .AddAuthentication("mft")
                .AddCookie("mft", options =>
                {
                    options.AccessDeniedPath = new PathString("/Account/");
                    options.Cookie = new CookieBuilder
                    {
                        //Domain = "localhost",
                        HttpOnly = true,
                        Name = ".mft.Cookie",
                        Path = "/",
                        SameSite = SameSiteMode.Strict,
                        SecurePolicy = CookieSecurePolicy.Always
                    };
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSignedIn = context =>
                        {
                            Console.WriteLine("{0} - {1}: {2}", DateTime.Now, "OnSignedIn",
                                context.Principal.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnSigningOut = context =>
                        {
                            Console.WriteLine("{0} - {1}: {2}", DateTime.Now, "OnSigningOut",
                                context.HttpContext.User.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnValidatePrincipal = context =>
                        {
                            try
                            {
                                if (context.Principal.Identity.IsAuthenticated)
                                {

                                    return Task.CompletedTask;
                                }
                                return Task.CompletedTask;
                            }
                            catch (Exception ex)
                            {
                                context.RejectPrincipal();

                                context.HttpContext.SignOutAsync(scheme: "mft");

                                context.HttpContext.Response.Redirect("~");

                                return Task.FromResult(false);
                            }
                        }
                    };
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.LoginPath = new PathString("/Account");
                    options.ReturnUrlParameter = "RequestPath";
                    options.SlidingExpiration = true;
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
