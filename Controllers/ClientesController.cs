using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPrueba.Models;
using WebApiPrueba.Repo;
using System;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            RPClientes rpCli = new RPClientes();

            //200
            return Ok(rpCli.ObtenerClientes());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPClientes rpCli = new RPClientes();

            var cliRet = rpCli.ObtenerCliente(id);
            try
            {
                if (cliRet == null)
                {
                    //404
                    var nf = NotFound("El cliente " + id.ToString() + " no existe.");
                    return nf;
                }

                return Ok(cliRet);
            }
            catch(Exception ex)
            {
                //500
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //400
                //return BadRequest(ex.Message);
            }
        }

        [HttpPost("agregar")]
        public IActionResult AgregarCliente(Cliente nuevoCliente)
        {
            RPClientes rpCli = new RPClientes();
            rpCli.Agregar(nuevoCliente);

            //201
            return CreatedAtAction(nameof(AgregarCliente), nuevoCliente);
        }
    }
}