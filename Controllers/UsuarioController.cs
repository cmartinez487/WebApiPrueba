using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using WebApiPrueba.Models;
using WebApiPrueba.DataBaseAccess;
using Microsoft.AspNetCore.Http;


namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        #region Instancia

        private readonly IDaoUsuario DaoUser;

        public UsuarioController(IDaoUsuario Instancia)
        {
            this.DaoUser = Instancia;
        }

        #endregion

        // GET: api/<UsuarioController> 
        [HttpGet("ConsultarTD")]
        public IActionResult ConsultarTD()
        {
            try
            {
                List<TipoIdentificacion> Tipos = DaoUser.ConsultaTipoIdentificacion();

                if (Tipos == null)
                {
                    //404
                    var nf = NotFound("La consulta no arrojo resultado");
                    return nf;
                }
                else
                {
                    return Ok(Tipos);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: api/<UsuarioController> 
        [HttpGet("{user},{tipo},{doc}")]
        public IActionResult Get(string user, string tipo, string doc)
        {
            try
            {

                List<Usuario> Usuarios = DaoUser.ConsultaUsuarios(user, tipo, doc);

                if (Usuarios == null)
                {
                    //404
                    var nf = NotFound("La consulta no arrojo resultado");
                    return nf;
                }
                else
                {
                    return Ok(Usuarios);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Usuario Usuario = DaoUser.ConsultaUsuarioXCodigo(id);

                Usuario.Tipos = DaoUser.ConsultaTipoIdentificacion();

                if (Usuario == null)
                {
                    //404
                    var nf = NotFound("La consulta no arrojo resultado");
                    return nf;
                }
                else
                {
                    return Ok(Usuario);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post(Usuario user)
        {
            try
            {
                int USUid = DaoUser.CrearUsuario(user);
                return CreatedAtAction(nameof(Post), "El usuario fue registrado bajo el Id: " + USUid);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public IActionResult Put(Usuario user)
        {
            try
            {
                Usuario Usuarios = DaoUser.ConsultaUsuarioXCodigo(user.IdUsuario);

                if (Usuarios != null)
                {
                    DaoUser.ActualizarUsuario(user);

                    return Ok("El usuario fue actualizado correctamente...");
                }
                else
                {
                    return NotFound("el Usuario no Existe");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Usuario Usuarios = DaoUser.ConsultaUsuarioXCodigo(id);

                if (Usuarios != null)
                {
                    DaoUser.EliminarUsuario(id);

                    return Ok("El usuario fue eliminado correctamente...");
                }
                else
                {
                    return NotFound("el Usuario no Existe");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
