using System.Collections.Generic;
using System.Linq;
using WebApiPrueba.Repo;
using WebApiPrueba.Models;

namespace WebApiPrueba.Repo
{
    public class RepoRoulette: IRepoRoulette
    {
        #region List Roulette and Bet
        public static List<Roulette> _listRoulette = new List<Roulette>();
        public static List<Bet> _listBet = new List<Bet>();
        RPClientes rpCli = new RPClientes();

        #endregion

        public List<Roulette> ListRoulette()
        {
            return _listRoulette;
        }

        public Roulette ConsultRoulette(int id)
        {
            var Roulette = _listRoulette.Where(rou => rou.IdRoulette == id);

            return Roulette.FirstOrDefault();
        }

        public int CreateNewRoulette(string RouletteName)
        {
            int IdRoulette = _listRoulette.Count()+1;

            Roulette newroulette = new Roulette()
            {
                IdRoulette = IdRoulette,
                RouletteName = RouletteName,
                RouletteState = false
            };

            _listRoulette.Add(newroulette);

            return IdRoulette;
        }

        public bool OpenRoulette(int IdRoulette)
        {
            bool state = false;
            foreach (var rou in _listRoulette)
            {
                if (rou.IdRoulette == IdRoulette)
                {
                    rou.RouletteState = true;
                    state = true;
                }
            }

            return state;
        }

        public List<Bet> ListBet()
        {
            return _listBet;
        }

        public int CreateNewBet(Bet newbet)
        {
            if ((newbet.BetNumber % 2) == 0)
            {
                newbet.BetColor = "Red";
            }
            else
            {
                newbet.BetColor = "Black";
            }
            newbet.IdBet = _listBet.Count() + 1;
            _listBet.Add(newbet);

            var Listcli = rpCli.ObtenerClientes();

            foreach(var cli in Listcli)
            {
                if(newbet.IdClient == cli.Id)
                {
                    cli.MontoDisponible = cli.MontoDisponible - newbet.BetAmount;
                }
            }

            return newbet.IdBet;
        }

        public bool CloseRoulette(int IdRoulette)
        {
            bool state = false;
            foreach (var rou in _listRoulette)
            {
                if (rou.IdRoulette == IdRoulette)
                {
                    rou.RouletteState = false;
                    state = true;
                }
            }

            return state;
        }
    }
}
