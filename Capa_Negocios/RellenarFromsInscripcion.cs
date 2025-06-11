using Capa_Datos;
using Capa_Negocios.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Capa_Negocios
{
    public static class RellenarFromsInscripcion
    {
        public static Estudiante obtenerInfoEstudiante(int idEstudiante)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante) 
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerInfoEstudiante", parametros); 

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

        public static List<Gestion> ObtenerGestiones()
        {
            List<Gestion> gestiones = new List<Gestion>();
            DataTable tabla = gDatos.Consultar("pa_ObtenerGestiones");

            foreach (DataRow fila in tabla.Rows)
            {
                Gestion g = new Gestion
                {
                    Id = Convert.ToInt32(fila["id_Gestion"]),
                    Semestre = Convert.ToInt32(fila["semestre"]),
                    Anio = Convert.ToInt32(fila["anio"])
                };
                gestiones.Add(g);
            }
            return gestiones;
        }

        public static List<Materia> CargarMateriasOfertadas(int idCarrera, int idGestion)
        {
            List<Materia> materias = new List<Materia>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idCarrera", idCarrera),
                new SqlParameter("@idGestion", idGestion)
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerEdicionesPorCarreraYGestion", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                Materia m = new Materia
                {
                    Id = Convert.ToInt32(fila["id_Edicion"]),
                    Nombre = fila["nombre_materia"].ToString(),
                    Grupo = fila["nombre_grupo"].ToString(),
                    Creditos = Convert.ToInt32(fila["creditos"]),
                    HoraInicio = TimeSpan.Parse(fila["hora_inicio"].ToString()).ToString(@"hh\:mm"),
                    HoraFin = TimeSpan.Parse(fila["hora_fin"].ToString()).ToString(@"hh\:mm")
                };
                materias.Add(m);
            }
            return materias;
        }

        public static bool IncribirMateriaEstudiante(int idEstudiante, List<Materia> materias)
        {
            int resultado = 0;
            foreach (Materia materia in materias)
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@seleccion", 'I'),
                    new SqlParameter("@FK_id_Estudiante", idEstudiante),
                    new SqlParameter("@FK_id_Edicion", materia.Id)
                };
                resultado = gDatos.Ejecutar("pa_MateriasInscritasCRUD", parametros);
            }
            return resultado > 0;
        }
    }
}
