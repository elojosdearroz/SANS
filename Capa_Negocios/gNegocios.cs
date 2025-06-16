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
    public static class gNegocios
    {
        public static List<EdicionMateria> ReporteNotasMateriasPorEstudiante(int idEstudiante, int idGestion)
        {
            List<EdicionMateria> estudiantes = new List<EdicionMateria>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante),
                new SqlParameter("@idGestion", idGestion)
            };

            DataTable dt = gDatos.Consultar("pa_ReporteNotasMateriasPorEstudiante", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                EdicionMateria estudiante = EdicionMateriaFactory.FromDataRow(fila);
                estudiantes.Add(estudiante);

            }
            return estudiantes;
        }

        public static List<EdicionMateria> ObtenerEdicionesPorDocente(int idDocente)
        {
            List<EdicionMateria> materias = new List<EdicionMateria>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idDocente", idDocente)
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerEdicionesPorDocente", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                EdicionMateria materia = new EdicionMateria
                {
                    Id = Convert.ToInt32(fila["id_edicion"]),
                    Materia = new Materia
                    {
                        Id = Convert.ToInt32(fila["id_materia"]),
                        Nombre = fila["nombre_materia"].ToString(),
                    },
                    Semestre = Convert.ToInt32(fila["semestre"]),
                    Anio = Convert.ToInt32(fila["anio"].ToString()),
                    Grupo = fila["grupo"].ToString(),
                    Curso = Convert.ToInt32(fila["curso"]),
                    Bloque = fila["bloque"].ToString(),
                };
                materias.Add(materia);
            }
            return materias;
        }

        public static List<EdicionMateria> ObtenerEstudiantesPorEdicion(int idEdicion)
        {
            List<EdicionMateria> estudiantes = new List<EdicionMateria>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEdicion", idEdicion)
            };
            DataTable dt = gDatos.Consultar("pa_ObtenerEstudiantesPorEdicion", parametros);
            foreach (DataRow fila in dt.Rows)
            {
                EdicionMateria estudiante = EdicionMateriaFactory.FromDataRow(fila);
                estudiantes.Add(estudiante);
            }
            return estudiantes;
        }

        public static List<EdicionMateria> CargarMateriasOfertadas(int idCarrera, int idGestion)
        {
            List<EdicionMateria> materias = new List<EdicionMateria>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idCarrera", idCarrera),
                new SqlParameter("@idGestion", idGestion)
            };

            DataTable dt = gDatos.Consultar("pa_ObtenerEdicionesPorCarreraYGestion", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                EdicionMateria m = new EdicionMateria
                {
                    Id = Convert.ToInt32(fila["id_Edicion"]),
                    Materia = new Materia
                    {
                        Nombre = fila["nombre_materia"].ToString(),
                        Creditos = Convert.ToInt32(fila["creditos"]),
                    },
                    Grupo = fila["grupo"].ToString(),
                };
                materias.Add(m);
            }
            return materias;
        }

        public static List<int> ObtenerMateriasInscritasPorEstudiante(int idEstudiante)
        { 
            List<int> materiasInscritas = new List<int>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante)
            };
            DataTable dt = gDatos.Consultar("pa_ObtenerMateriasInscritasPorEstudiante", parametros);
            foreach (DataRow fila in dt.Rows)
            {
                int idMateria = Convert.ToInt32(fila["id_Edicion"]);
                materiasInscritas.Add(idMateria);
            }
            return materiasInscritas;
        }

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



        public static bool IncribirMateriaEstudiante(int idEstudiante, List<EdicionMateria> materias)
        {
            int resultado = 0;
            foreach (EdicionMateria materia in materias)
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
