using Microsoft.Extensions.DependencyInjection;

namespace TestDotNetCore.Services
{
    public static class ServiceProviderExtension
    {
		public static void AddTimeService(this IServiceCollection services)
		{
			services.AddTransient<TimeService>();
		}
    }
}
