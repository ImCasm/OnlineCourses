using System;
using System.Net;

namespace Aplicacion.ExceptionHandlers
{
    public class ExceptionHandler : Exception
    {

        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public ExceptionHandler(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
