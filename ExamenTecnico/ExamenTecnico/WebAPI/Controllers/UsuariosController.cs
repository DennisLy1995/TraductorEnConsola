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
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new UsuariosManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("GetByUserName/{nombre_usuario}")]
        public IHttpActionResult GetByUserName(string nombre_usuario)
        {
            try
            {
                var mng = new UsuariosManager();
                var usuario = new Usuarios
                {
                    NOMBRE_USUARIO = nombre_usuario
                };

                usuario = mng.RetrieveByUserName(usuario);
                apiResp = new ApiResponse();
                apiResp.Data = usuario;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.MESSAGE));
            }
        }

        [HttpPost]
        public IHttpActionResult Post(Usuarios usuario)
        {

            try
            {
                var mng = new UsuariosManager();
                String response = mng.Create(usuario);

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
