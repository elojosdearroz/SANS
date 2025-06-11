using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Negocios.Clases
{
    public class Estudiante : Persona
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public int IdPlanEstudio { get; set; }

        public Estudiante obtenerInfoEstudiante(int idEstudiante)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante) // Corregir nombre del parámetro
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerInfoEstudiante", parametros); // Asumo que es un DataTable

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Estudiante estudiante = new Estudiante
                {
                    Id = Convert.ToInt32(row["id_Estudiante"]),
                    Nombre = row["nombre"].ToString(),
                    Apellido = row["apellido"].ToString(),
                    IdCarrera = Convert.ToInt32(row["id_Carrera"]),
                    NombreCarrera = row["nombre_carrera"].ToString()
                };
                return estudiante;
            }
            return null;
        }
    }
}
