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
    [RoutePrefix("api/Palabras")]
    public class PalabrasController : ApiController
    {

        ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new PalabrasManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("GetByPalabra/{palabraVar}")]
        public IHttpActionResult GetByPalabra(string palabraVar)
        {
            try
            {
                var mng = new PalabrasManager();
                var palabra = new Palabras
                {
                    PALABRA = palabraVar
                };

                palabra = mng.RetrieveByName(palabra);
                apiResp = new ApiResponse();
                apiResp.Data = palabra;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpGet]
        [Route("GetByPrimeraPalabra/{palabraVar}")]
        public IHttpActionResult GetByPrimeraPalabra(string palabraVar)
        {
            try
            {
                var mng = new PalabrasManager();
                var palabra = new Palabras
                {
                    PALABRA = palabraVar
                };

                palabra = mng.RetrieveByPrimeraPalabra(palabra);
                apiResp = new ApiResponse();
                apiResp.Data = palabra;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpGet]
        [Route("GetPalabraEnIdioma")]
        public IHttpActionResult GetPalabraEnIdioma(string palabraVar, string nombre_idioma)
        {
            try
            {
                var mng = new PalabrasManager();
                var palabra = new Palabras
                {
                    PALABRA = palabraVar,
                    NOMBRE_IDIOMA = nombre_idioma
            };

                palabra = mng.RetrieveByNameAndIdiom(palabra);
                apiResp = new ApiResponse();
                apiResp.Data = palabra;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpGet]
        [Route("GetAllPrimerasPalabras")]
        public IHttpActionResult GetAllPrimerasPalabras()
        {
            apiResp = new ApiResponse();
            var mng = new PalabrasManager();
            apiResp.Data = mng.RetrieveAllPrimerasPalabras();

            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult Post(Palabras palabra)
        {
            try
            {
                var mng = new PalabrasManager();
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

        [HttpPost]
        [Route("PostPrimera")]
        public IHttpActionResult PostPrimera(Palabras palabra)
        {
            try
            {
                var mng = new PalabrasManager();
                String response = mng.CreatePrimeraPalabra(palabra);

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
