using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class gDatos
    {
        //Codigo realativo a la BD
        #region Atributos
        private string bd, tb, cSel;
        public DataSet ds = new DataSet();
        string SQL;
        SqlConnection miCon;
        SqlDataAdapter adap;
        #endregion
        #region Propiedades
        public string BD
        { set { if (value != null) bd = value; } }
        public string TB
        { set { if (value != null) tb = value; } }
        public string CSel
        { set { if (value != null) cSel = value; } }
        #endregion
        #region Metodos
        public void Conectar()
        {
            //Creando una cadena de conexion a la BD
            miCon = new SqlConnection();
            miCon.ConnectionString = @"Data Source=LAPTOP-OQ930FCP\MSSQLSERVERLTP;Initial Catalog=UniversidadDB;Integrated Security=True;Encrypt=False";
            string crit = "%" + cSel + "%";
            SQL = "SELECT * FROM " + tb + " WHERE descripcion LIKE " + "'" + crit + "'";
        }
        public void CrearTablaRAM()
        {
            adap = new SqlDataAdapter(SQL, miCon);
            //Creando un objeto DataSet
            ds = new DataSet();
            //Cargar la tabla en el Data Set
            adap.Fill(ds, tb);
        }
        public static string ObtenerCadenaConexion()
        {
            return @"Data Source=LAPTOP-OQ930FCP\MSSQLSERVERLTP;Initial Catalog=UniversidadDB;Integrated Security=True;Encrypt=False";
        }
        #endregion
        public static int Ejecutar(string procedimiento, SqlParameter[] parametros)
        {
            using (var con = new SqlConnection(ObtenerCadenaConexion()))
            using (var cmd = new SqlCommand(procedimiento, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametros);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable Consultar(string procedimiento, SqlParameter[] parametros = null)
        {
            var tabla = new DataTable();
            using (var con = new SqlConnection(ObtenerCadenaConexion()))
            using (var cmd = new SqlCommand(procedimiento, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                da.Fill(tabla);
            }
            return tabla;
        }
    }
}
