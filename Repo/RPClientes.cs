using System.Collections.Generic;
using System.Linq;
using WebApiPrueba.Models;

namespace WebApiPrueba.Repo
{
    public class RPClientes: IRPClientes
    {
        public static List<Cliente> _listaClientes = new List<Cliente>()
        {
            new Cliente() { Id = 1, Nombre = "Cliente 1" , Apellido = "Apellido 1", MontoDisponible = 1000 },
            new Cliente() { Id = 2, Nombre = "Cliente 2" , Apellido = "Apellido 2", MontoDisponible = 10000 },
            new Cliente() { Id = 3, Nombre = "Cliente 3" , Apellido = "Apellido 3", MontoDisponible = 250 }
        };

        public IEnumerable<Cliente> ObtenerClientes()
        {
            return _listaClientes;
        }

        public Cliente ObtenerCliente(int id)
        {
            var cliente = _listaClientes.Where(cli => cli.Id == id);

            return cliente.FirstOrDefault();
        }

        public void Agregar(Cliente nuevoCliente)
        {
            _listaClientes.Add(nuevoCliente);
        }

        public void AgregarPremio(int cliId, decimal monto)
        {
            foreach (var cli in _listaClientes)
            {
                if ( cli.Id == cliId)
                {
                    cli.MontoDisponible = cli.MontoDisponible + monto;
                };
            }
        }
    }
}
