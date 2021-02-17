using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrueba.Models
{
    public class Usuario
    {
        /// <summary>
        /// 
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Id del usuario
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Tipo de documento del usuario
        /// </summary> 
        public string TipoDocumento { get; set; }

        /// <summary>
        /// tipos de Identificacion
        /// </summary>
        public List<TipoIdentificacion> Tipos { get; set; }

        /// <summary>
        /// Documento del usuario
        /// </summary> 
        public string Documento { get; set; }

        /// <summary>
        /// Nombres del usuario
        /// </summary> 
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario
        /// </summary> 
        public string Apellidos { get; set; }

        /// <summary>
        /// Teléfono móvil del usuario
        /// </summary> 
        public string TelefonoMovil { get; set; }

        /// <summary>
        /// email del usuario
        /// </summary> 
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaActualizacion { get; set; }
    }
}
