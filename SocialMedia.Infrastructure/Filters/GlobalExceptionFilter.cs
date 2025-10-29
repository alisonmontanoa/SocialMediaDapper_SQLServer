using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Core.Exceptions;
using System.Net;

namespace SocialMedia.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status;
            string title;
            string detail;

            // 🔹 Manejo de excepciones personalizadas (Business)
            if (context.Exception is BussinesException businessEx)
            {
                status = HttpStatusCode.BadRequest;
                title = "Bad Request";
                detail = businessEx.Message;
            }
            // 🔹 Excepciones genéricas
            else
            {
                status = HttpStatusCode.InternalServerError;
                title = "Internal Server Error";
                detail = context.Exception.Message;
            }

            // 🔹 Estructura del error de salida
            var errorResponse = new
            {
                errors = new[]
                {
                    new
                    {
                        Status = (int)status,
                        Title = title,
                        Detail = detail
                    }
                }
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)status
            };

            context.ExceptionHandled = true;
        }
    }
}
