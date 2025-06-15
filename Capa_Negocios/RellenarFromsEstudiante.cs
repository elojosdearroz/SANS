using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public static class RellenarFromsEstudiante
    {
        public static List<Materia> ObtenerNotasPorEstudianteYGestion(int idEstudiante, int idGestion)
            {
            List<Materia> materias = new List<Materia>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante),
                new SqlParameter("@idGestion", idGestion)
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerNotasPorEstudianteYGestion", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                Materia materia = new Materia
                {
                    Id = Convert.ToInt32(fila["id_Materia"]),
                    Nombre = fila["nombre_materia"].ToString(),
                    Grupo = fila["grupo"].ToString(),
                    Nota = Convert.ToInt32(fila["nota"]),
                };
                materias.Add(materia);
            }
            return materias;
        }
    }
}
