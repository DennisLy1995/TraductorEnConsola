using Entities_POJO;
using CoreAPI;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Idiomas")]
    public class IdiomasController : ApiController
    {
        ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new IdiomasManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("GetByName/{nombre_idioma}")]
        public IHttpActionResult GetByName(string nombre_idioma)
        {
            try
            {
                var mng = new IdiomasManager();
                var idioma = new Idiomas
                {
                    NOMBRE_IDIOMA = nombre_idioma
                };

                idioma = mng.RetrieveByName(idioma);
                apiResp = new ApiResponse();
                apiResp.Data = idioma;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpPost]
        public IHttpActionResult Post(Idiomas idioma)
        {
            try
            {
                var mng = new IdiomasManager();
                String response = mng.Create(idioma);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return Content(HttpStatusCode.BadRequest, bex);

            }

        }
    }
}
