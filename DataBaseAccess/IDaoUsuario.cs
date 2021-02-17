using System.Collections.Generic;
using WebApiPrueba.Models;

namespace WebApiPrueba.DataBaseAccess
{
    public interface IDaoUsuario
    {
        /// <summary>
        /// Consulta los tipos de Identificacion
        /// </summary>
        /// <returns></returns>
        List<TipoIdentificacion> ConsultaTipoIdentificacion();

        /// <summary>
        /// Consulta un usuario por codigo
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Usuario ConsultaUsuarioXCodigo(int UserId);

        /// <summary>
        /// Consulta todos los Usuarios
        /// </summary>
        /// <returns></returns>
        List<Usuario> ConsultaUsuarios(string user, string tipo, string doc);

        /// <summary>
        /// Crea un Usuario
        /// </summary>
        /// <param name="p"></param>
        int CrearUsuario(Usuario User);

        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="p"></param>
        void ActualizarUsuario(Usuario User);

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="p"></param>
        void EliminarUsuario(int User);
    }
}
