using System.Collections.Generic;
using WebApiPrueba.Models;

namespace WebApiPrueba.Repo
{
    public interface IRepoRoulette
    {
        List<Roulette> ListRoulette();
        List<Bet> ListBet();
        Roulette ConsultRoulette(int id);
        int CreateNewRoulette(string RouletteName);
        bool OpenRoulette(int IdRoulette);
        bool CloseRoulette(int IdRoulette);
        int CreateNewBet(Bet newbet);
    }
}
