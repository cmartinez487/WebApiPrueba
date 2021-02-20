using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPrueba.Models;

namespace WebApiPrueba.Repo
{
    public interface IRPClientes
    {
        IEnumerable<Cliente> ObtenerClientes();
        Cliente ObtenerCliente(int id);
        void Agregar(Cliente nuevoCliente);
        void AgregarPremio(int cliId, decimal monto);
    }
}
