using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPrueba.Models
{
    public class TipoIdentificacion
    {
        /// <summary>
        /// Codigo del tipo de identificacion
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del tipo de identificacion
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Indica si es activo
        /// </summary>
        public bool Activo { get; set; }
    }
}
