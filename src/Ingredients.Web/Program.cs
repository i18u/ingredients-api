using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ingredients.Web
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel((config, options) => {
					if (config.HostingEnvironment.IsDevelopment()) {
						options.Listen(IPAddress.Loopback, 5000);
					} else {
						options.Listen(IPAddress.Any, 80);
					}
				})
				.UseStartup<Startup>();
	}
}
