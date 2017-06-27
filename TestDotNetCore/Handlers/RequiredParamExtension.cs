using Microsoft.AspNetCore.Builder;

namespace TestDotNetCore.Handlers
{
    public static class RequiredParamExtension
    {
		public static IApplicationBuilder UseRequiredParams(this IApplicationBuilder builder, string requiredParam)
		{
			return builder.UseMiddleware<RequiredParamChecker>(requiredParam);
		}
    }
}
