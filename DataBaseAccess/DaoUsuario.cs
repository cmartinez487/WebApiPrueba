using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiPrueba.Models;

namespace WebApiPrueba.DataBaseAccess
{
    public class DaoUsuario : IDaoUsuario
    {
        #region Instancia Config

        private readonly IConfiguration _configuration;

        public DaoUsuario(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #endregion

        public List<TipoIdentificacion> ConsultaTipoIdentificacion()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("spTIPIDESelObtenerActivos", Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(ds);
                }
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return (from row in ds.Tables[0].AsEnumerable()
                    select new TipoIdentificacion
                    {
                        Codigo = row.Field<string>("TIPIDEcodigo"),
                        Nombre = row.Field<string>("TIPIDEnombre"),
                        Activo = row.Field<bool>("TIPIDEactivo"),

                    }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Usuario> ConsultaUsuarios(string user, string tipo, string doc)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("spUSUSelObtenerUsuarios", Conn))
                {
                    cmd.Parameters.AddWithValue("@USUnombreUsuario", null);
                    cmd.Parameters.AddWithValue("@USUtipoIdentificacion", null);
                    cmd.Parameters.AddWithValue("@USUidentificacion", null);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(ds);
                }
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return (from row in ds.Tables[0].AsEnumerable()
                    select new Usuario
                    {
                        IdUsuario = row.Field<int>("USUid"),
                        UserName = row.Field<string>("USUnombreUsuario"),
                        TipoDocumento = row.Field<string>("USUtipoIdentificacion"),
                        Documento = row.Field<string>("USUidentificacion"),
                        Nombres = row.Field<string>("USUnombres"),
                        Apellidos = row.Field<string>("USUapellidos"),
                        TelefonoMovil = row.Field<string>("USUtelefonoMovil"),
                        Email = row.Field<string>("USUcorreoElectronico"),
                        FechaCreacion = row.Field<DateTime>("USUfechaCreacion"),

                    }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Usuario ConsultaUsuarioXCodigo(int UserId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("spUSUSelObtenerUsuarioXCodigo", Conn))
                {
                    cmd.Parameters.AddWithValue("@USUid", UserId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(ds);
                }
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return (from row in ds.Tables[0].AsEnumerable()
                    select new Usuario
                    {
                        IdUsuario = row.Field<int>("USUid"),
                        UserName = row.Field<string>("USUnombreUsuario"),
                        TipoDocumento = row.Field<string>("USUtipoIdentificacion"),
                        Documento = row.Field<string>("USUidentificacion"),
                        Nombres = row.Field<string>("USUnombres"),
                        Apellidos = row.Field<string>("USUapellidos"),
                        TelefonoMovil = row.Field<string>("USUtelefonoMovil"),
                        Email = row.Field<string>("USUcorreoElectronico"),
                        FechaCreacion = row.Field<DateTime>("USUfechaCreacion"),

                    }).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int CrearUsuario(Usuario User)
        {
            int USUid = 0;

            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                Conn.Open();

                using (SqlCommand cmd = new SqlCommand("spUSUInsCreacionUsuario", Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USUnombreUsuario", User.UserName);
                    cmd.Parameters.AddWithValue("@USUPasword", User.Password);
                    cmd.Parameters.AddWithValue("@USUtipoIdentificacion", User.TipoDocumento);
                    cmd.Parameters.AddWithValue("@USUidentificacion", User.Documento);
                    cmd.Parameters.AddWithValue("@USUnombres", User.Nombres);
                    cmd.Parameters.AddWithValue("@USUapellidos", User.Apellidos);
                    cmd.Parameters.AddWithValue("@USUtelefonoMovil", User.TelefonoMovil);
                    cmd.Parameters.AddWithValue("@USUcorreoElectronico", User.Email);

                    SqlParameter paran = new SqlParameter()
                    {
                        ParameterName = "@USUid",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output,
                    };

                    cmd.Parameters.Add(paran);
                    cmd.ExecuteNonQuery();

                    USUid = Convert.ToInt32(cmd.Parameters["@USUid"].Value);
                }

                Conn.Close();

                return USUid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public void ActualizarUsuario(Usuario User)
        {

            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                Conn.Open();

                using (SqlCommand cmd = new SqlCommand("spUSUUpdActualizacionUsuario", Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USUid", User.UserName);
                    cmd.Parameters.AddWithValue("@USUPasword", User.Password);
                    cmd.Parameters.AddWithValue("@USUnombres", User.Nombres);
                    cmd.Parameters.AddWithValue("@USUapellidos", User.Apellidos);
                    cmd.Parameters.AddWithValue("@USUtelefonoMovil", User.TelefonoMovil);
                    cmd.Parameters.AddWithValue("@USUcorreoElectronico", User.Email);
                    cmd.ExecuteNonQuery();
                }

                Conn.Close();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public void EliminarUsuario(int User)
        {
            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                Conn.Open();

                using (SqlCommand cmd = new SqlCommand("spUSUDelEliminarUsuarios", Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USUid", User);
                    cmd.ExecuteNonQuery();
                }

                Conn.Close();
            }
        }
    }
}
