
using Newtonsoft.Json;
using System.Net;

namespace WaterJugChallengeWS.Helpers
{
    public class GlobalExceptionHandler : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                
                var response = new Response<Exception>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = message,
                    Data = ex
                };


                //Here we can add some instructions handling the exception and logging it. 🤓🔥

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
