using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Service;

namespace viTouch.BackgroundServices
{
	public class SwaggerAddHeaderParameter : IOperationFilter
	{
		private readonly IConfiguration _configuration;

		public SwaggerAddHeaderParameter(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Parameters ??= new List<OpenApiParameter>();
			operation.Parameters.Add(new OpenApiParameter
			{
				Name = AppSettings.AppToken,
				In = ParameterLocation.Header,
				Description = "access token",
				Schema = new OpenApiSchema
				{
					Type = "String",
					Default = new OpenApiString(_configuration[AppSettings.AppToken])
				}
			});
		}
	}
}
