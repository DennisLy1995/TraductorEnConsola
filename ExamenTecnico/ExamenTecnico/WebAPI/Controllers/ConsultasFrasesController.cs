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
    [RoutePrefix("api/ConsultasFrases")]
    public class ConsultasFrasesController : ApiController
    {
        ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new ConsultasFrasesManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }


        [HttpGet]
        [Route("GetContador")]
        public IHttpActionResult GetContador()
        {
            try
            {
                var mng = new ConsultasFrasesManager();
                var consulta = new ConsultasFrases
                {

                };

                consulta = mng.RetrieveCounter(consulta);
                apiResp = new ApiResponse();
                apiResp.Data = consulta;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpGet]
        [Route("GetByCodigoConsulta/{codigo_consulta}")]
        public IHttpActionResult GetByCodigoConsulta(string codigo_consulta)
        {
            try
            {
                var mng = new ConsultasFrasesManager();
                var consulta = new ConsultasFrases
                {
                    CODIGO_CONSULTA = codigo_consulta
                };

                consulta = mng.RetrieveByCodigo(consulta);
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
        public IHttpActionResult Post(ConsultasFrases consulta)
        {
            try
            {
                var mng = new ConsultasFrasesManager();
                String response = mng.Create(consulta);

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
