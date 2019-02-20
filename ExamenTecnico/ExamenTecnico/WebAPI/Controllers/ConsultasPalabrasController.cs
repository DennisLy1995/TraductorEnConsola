using CoreAPI;
using Entities_POJO;
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
    [RoutePrefix("api/ConsultasPalabras")]
    public class ConsultasPalabrasController : ApiController
    {
        ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new ConsultasPalabrasManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("GetByCodigo/{codigo_registro}")]
        public IHttpActionResult GetByCodigo(int codigo_registro)
        {
            try
            {
                var mng = new ConsultasPalabrasManager();
                var consulta = new ConsultasPalabras
                {
                    CODIGO_REGISTRO = codigo_registro
                };

                consulta = mng.RetrieveByName(consulta);
                apiResp = new ApiResponse();
                apiResp.Data = consulta;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpPost]
        public IHttpActionResult Post(ConsultasPalabras palabra)
        {
            try
            {
                var mng = new ConsultasPalabrasManager();
                String response = mng.Create(palabra);

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
