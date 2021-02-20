using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using WebApiPrueba.Models;
using WebApiPrueba.DataBaseAccess;
using Microsoft.AspNetCore.Http;
using WebApiPrueba.Repo;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        #region Instance

        private readonly IRepoRoulette Instance;
        private readonly IRPClientes instacli;

        public RouletteController(IRepoRoulette Instancia, IRPClientes Instacli)
        {
            this.Instance = Instancia;
            instacli = Instacli;
        }

        #endregion

        [HttpGet("ListRoulette")]
        public IActionResult ListRoulette()
        {
            try
            {
                return Ok(Instance.ListRoulette());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ListBet")]
        public IActionResult ListBet()
        {
            try
            {
                return Ok(Instance.ListBet());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ConsultRoulette {IdRoulette}")]
        public IActionResult ConsultRoulette(int IdRoulette)
        {
            try
            {
                Roulette consult = Instance.ConsultRoulette(IdRoulette);

                if (consult != null)
                {

                    return Ok(consult);
                }
                else
                {
                    return NotFound("La Ruleta " + IdRoulette + " no existe.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateRoulette {RouletteName}")]
        public IActionResult CreateRoulette(string RouletteName)
        {
            try
            {
                int IdRoulette = Instance.CreateNewRoulette(RouletteName);

                return CreatedAtAction(nameof(CreateRoulette), "La ruleta fue registrado bajo el Id: " + IdRoulette);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("OpenRoulette {IdRoulette}")]
        public IActionResult OpenRoulette(int IdRoulette)
        {
            try
            {
                Roulette consult = Instance.ConsultRoulette(IdRoulette);
                if (consult == null)
                {
                    return NotFound("La Ruleta " + IdRoulette + " no existe.");
                }
                if (consult.RouletteState)
                {
                    return BadRequest("La Ruleta " + IdRoulette + " ya fue activada.");
                }
                bool state = Instance.OpenRoulette(IdRoulette);
                if (state)
                {
                    return Ok("La Ruleta " + IdRoulette + " fue activada.");
                }
                else
                {
                    return BadRequest("La Ruleta " + IdRoulette + " no puse ser activada.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("CreateNewBet")]
        public IActionResult CreateNewBet(Bet newbet)
        {
            try
            {
                Cliente cli = instacli.ObtenerCliente(newbet.IdClient);
                Roulette consult = Instance.ConsultRoulette(newbet.IdRoulette);

                if (cli == null)
                {
                    return NotFound("El cliente " + newbet.IdClient + " no existe.");
                }
                if (cli.MontoDisponible == 0 || newbet.BetAmount > cli.MontoDisponible)
                {
                    return NotFound("El cliente " + newbet.IdClient + " no tiene el dinero suficiente para apostar.");
                }
                if (consult == null)
                {
                    return NotFound("El la Ruleta " + newbet.IdRoulette + " no existe.");
                }
                if (!consult.RouletteState)
                {
                    return BadRequest("La Ruleta " + newbet.IdRoulette + " no ha sido activada.");
                }
                if (newbet.BetNumber > 36 || newbet.BetNumber < 1)
                {
                    return BadRequest("El numero al que apuestas no es valido");
                }
                if (newbet.BetAmount > 10000 || newbet.BetAmount < 1)
                {
                    return BadRequest("el rango de la apuesta va desde 1 hasta 10000.");
                }

                int IdBet = Instance.CreateNewBet(newbet);

                return CreatedAtAction(nameof(CreateNewBet), "La apuesta fue registrado bajo el Id: " + IdBet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("CloseBet {IdRoulette}")]
        public IActionResult CloseBet(int IdRoulette)
        {
            try
            {
                Random rnd = new Random();
                Roulette Roulette = Instance.ConsultRoulette(IdRoulette);
                List<Bet> Bets = Instance.ListBet();
                Roulette.WiningBets = new List<Bet>();
                int win = 0;
                string colorwing = "Black";
                var Win = 30;//rnd.Next(1, 36);

                if ((win % 2) == 0)
                {
                    colorwing = "Red";
                }
                if (Roulette == null)
                {
                    return NotFound("La Ruleta " + IdRoulette + " no existe.");
                }
                if (!Roulette.RouletteState)
                {
                    return BadRequest("La Ruleta " + IdRoulette + " no ha sido activada.");
                }
                foreach (var bet in Bets)
                {
                    if (bet.BetNumber == Win)
                    {
                        decimal AmountWinner = bet.BetAmount * 5m;
                        instacli.AgregarPremio(bet.IdClient, AmountWinner);
                        Roulette.WiningBets.Add(bet);
                    }
                    else if (bet.BetColor == colorwing)
                    {
                        decimal AmountWinner = bet.BetAmount * 1.8m;
                        instacli.AgregarPremio(bet.IdClient, AmountWinner);
                        Roulette.WiningBets.Add(bet);
                    }
                }
                bool state = Instance.CloseRoulette(IdRoulette);
                if (state)
                {
                    return Ok("La Ruleta " + IdRoulette + " fue activada.");
                }
                else
                {
                    return BadRequest("La Ruleta " + IdRoulette + " no puse ser activada.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}

