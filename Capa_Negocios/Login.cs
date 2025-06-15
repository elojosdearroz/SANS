using Capa_Datos;
using Capa_Negocios.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public static class Login
    {
        #region Logica Singleton
        private static Usuario usuarioactual;

        public static Usuario Usuarioactual
        {
            get { return usuarioactual; }
        }
        public static void IniciarSesion(Usuario usuario)
        {
            if (Usuarioactual == null)
            {
                usuarioactual = usuario;
            }
            else
            {
                throw new InvalidOperationException("Ya hay un usuario en sesión.");
            }
        }

        public static void CerrarSesion()
        {
            usuarioactual = null;
        }

        public static bool HaySesion()
        {
            return usuarioactual != null;
        }
        #endregion
        public static Usuario ObtenerUsuarioPorCredenciales(string ci, string contrasena)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@CI", ci),
                new SqlParameter("@Contrasena", contrasena)
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerUsuarioPorCredenciales", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["rol"].ToString() == "Estudiante")
                {
                    return new Estudiante
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nombre = row["nombre"].ToString(),
                        Apellido = row["apellido"].ToString(),
                        CI = Convert.ToInt32(row["ci"]),
                        Contrasena = Convert.ToInt32(row["contrasena"]),
                        IdCarrera = Convert.ToInt32(row["id_Carrera"]),
                        NombreCarrera = row["nombre_Carrera"].ToString()
                    };
                }
                else if (row["rol"].ToString() == "Docente")
                {
                    return new Docente
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nombre = row["nombre"].ToString(),
                        Apellido = row["apellido"].ToString(),
                        CI = Convert.ToInt32(row["ci"]),
                        Contrasena = Convert.ToInt32(row["contrasena"])
                    };
                }
                else if (row["rol"].ToString() == "Administrativo")
                {
                    return new Administrativo
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nombre = row["nombre"].ToString(),
                        Apellido = row["apellido"].ToString(),
                        CI = Convert.ToInt32(row["ci"]),
                        Contrasena = Convert.ToInt32(row["contrasena"])
                    };
                }
            }
            return null; 
        }
    }
}
