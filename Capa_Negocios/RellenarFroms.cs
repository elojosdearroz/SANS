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
    public static class RellenarFroms
    {
        public static Dictionary<int, string> PlanesEstudio = new Dictionary<int, string>();
        static gDatos datos = new Capa_Datos.gDatos();
        public static string NombreEstudiante { get; set; }
        public static string NombreCarrera { get; set; }


        public static void RellenarFormulario(int idEstudiante)
        {
            using (var con = new SqlConnection(gDatos.ObtenerCadenaConexion()))
            using (SqlCommand cmd = new SqlCommand("pa_ObtenerInfoEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersona", idEstudiante);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Primer conjunto: info estudiante
                    if (reader.Read())
                    {
                        string nombreCompleto = reader["nombreCompleto"] as string;
                        string nombreCarrera = reader["nombreCarrera"] as string;

                        // Validar que no sea null (no estudiante)
                        if (string.IsNullOrEmpty(nombreCompleto))
                        {
                        }
                        else
                        {
                            // Cargamos los datos del estudiante
                            NombreEstudiante = nombreCompleto;
                            NombreCarrera = nombreCarrera;
                        }
                    }

                    // Segundo conjunto: planes de estudio
                    if (reader.NextResult())
                    {
                        PlanesEstudio.Clear();
                        while (reader.Read())
                        {
                            string tempNombre = reader["nombrePlan"].ToString();
                            int tempId = int.Parse(reader["id_PlanEstudio"].ToString());

                            PlanesEstudio.Add(tempId, tempNombre);
                        }
                    }
                }
            }
        }

        public static DataTable ObtenerMateriasPorPlan(int idPlanEstudio)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPlanEstudio", idPlanEstudio)
            };
            return gDatos.Consultar("pa_ObtenerMateriasPorPlanes", parametros);
        }
    }
}
