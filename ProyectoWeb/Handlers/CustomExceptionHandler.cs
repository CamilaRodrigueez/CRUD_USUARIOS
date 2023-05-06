using Common.Utils.Exeptions;
using Common.Utils.Resources;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoWeb.Domain.Dto;
using Serilog;

namespace ProyectoWeb.Handlers
{
    public class CustomExceptionHandler : ExceptionFilterAttribute
    {
        /// <summary>
        /// Metodo encargado de capturar todas las Excepciones del proyecto,
        /// Se debe agregar el decorador a cada controller [TypeFilter(typeof(CustomExceptionHandler))]
        /// </summary>
        /// <param name="exception"> Parametro donde queda capturada la Exception </param>
        public override void OnException(ExceptionContext context)
        {

            HttpResponseException responseExeption = new HttpResponseException()
            {
                Status = StatusCodes.Status500InternalServerError
            };

            ResponseDto response = new ResponseDto()
            {
                Result = JsonConvert.SerializeObject(context.Exception),
                Message = GeneralMessages.Error500

            };

            context.ExceptionHandled = true;


            context.Result = new ObjectResult(responseExeption.Value)
            {
                StatusCode = responseExeption.Status,
                Value = response
            };

            Log.Fatal(context.Exception, GeneralMessages.Error500);

            if (responseExeption.Status == StatusCodes.Status500InternalServerError)
                context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Ha ocurrido un error";
        }
    }
}
