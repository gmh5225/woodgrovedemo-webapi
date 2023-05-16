using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace woodgroveapi.Services
{
    public class Debugger
    {
        public static void PrintDebugInfo(ControllerBase controller, ILogger<ControllerBase> logger)
        {
            // Print the controller name
            logger.LogInformation( $"#### call to: {controller.GetType().Name}");

            // Validate that REST API received a bearer token in the authorization header.
            if (controller.Request.Headers.Authorization.Count == 0)
            {
                logger.LogInformation( "#### authorization header not found");
            }
            else
            {
                logger.LogInformation( $"#### authorization header: {controller.Request.Headers.Authorization[0]}");
            }
         }
    }
}