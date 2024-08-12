using CS58_Razor09EF.Models;
using CS58_Razor09EF.Models;
using CS58_Razor09EF.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CS58_Razor09EF.Config
{
	public static class AppConfiguration
	{
		public static void AddAppService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions();
			var mailSetting = configuration.GetSection("MailSettings");
			services.Configure<MailSettings>(mailSetting);
			services.AddSingleton<IEmailSender, SendMailService>();

			services.AddRazorPages().AddRazorRuntimeCompilation();
			services.AddDbContext<MyBlogContext>(option =>
			{
				string connectString = configuration.GetConnectionString("MyBlogContext");
				option.UseSqlServer(connectString);
			});
			services.AddIdentity<AppUser, IdentityRole>()
					 .AddEntityFrameworkStores<MyBlogContext>()
					 .AddDefaultTokenProviders();


			//services.AddDefaultIdentity<AppUser>()
			//	   .AddEntityFrameworkStores<MyBlogContext>()
			//	   .AddDefaultTokenProviders();
			// Truy cập IdentityOptions
			services.Configure<IdentityOptions>(options =>
			{
				// Thiết lập về Password
				options.Password.RequireDigit = false; // Không bắt phải có số
				options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
				options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
				options.Password.RequireUppercase = false; // Không bắt buộc chữ in
				options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
				options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

				// Cấu hình Lockout - khóa user
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
				options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
				options.Lockout.AllowedForNewUsers = true;

				// Cấu hình về User.
				options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
					"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;  // Email là duy nhất

				// Cấu hình đăng nhập.
				options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
				options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
				options.SignIn.RequireConfirmedAccount = false;

			});

			services.ConfigureApplicationCookie(option => {
				option.LoginPath = "/login/";
				option.LogoutPath = "/logout";
				option.AccessDeniedPath = "/khongduoctruycap";
			});

			services.AddAuthentication()
				.AddGoogle(options => {
					IConfigurationSection googleAuthSection = configuration.GetSection("Authentication:Google");
					options.ClientId = googleAuthSection["ClientId"];
					options.ClientSecret = googleAuthSection["ClientSecret"];
					options.CallbackPath = "/login-with-google";
				});
		}
	}
}
