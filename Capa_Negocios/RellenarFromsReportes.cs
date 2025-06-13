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
    public static class RellenarFromsReportes
    {
        public static List<Carrera> ObtenerCarreras()
        {
            List<Carrera> carreras = new List<Carrera>();
            DataTable tabla = gDatos.Consultar("pa_ObtenerCarreras");

            foreach (DataRow fila in tabla.Rows)
            {
                Carrera c = new Carrera
                {
                    Id = Convert.ToInt32(fila["id_Carrera"]),
                    Nombre = fila["nombre_carrera"].ToString(),
                    NombreFacultad =  fila["nombre_facultad"].ToString(),
                    NombreUniversidad = fila["nombre_universidad"].ToString()
                };
                carreras.Add(c);
            }
            return carreras;
        }

        public static List<Gestion> ObtenerGestionesPorCarrera(int idCarrera)
        {
            List<Gestion> gestiones = new List<Gestion>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idCarrera", idCarrera)
            };
            DataTable tabla = gDatos.Consultar("pa_GestionesPorCarrera", parametros);

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

        public static List<int> ObtenerPlanesPorCarrera(int idCarrera)
        {
            List<int> planes = new List<int>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id_Carrera", idCarrera)
            };
            DataTable tabla = gDatos.Consultar("pa_ObtenerPlanesPorCarrera", parametros);
            foreach (DataRow fila in tabla.Rows)
            {
                planes.Add(Convert.ToInt32(fila["id_PlanEstudio"]));
            }
            return planes;
        }

        //pa_ObtenerEdicionesPorCarrera
        public static List<EdicionMateria> ObtenerEdicionesPorCarrera(int idCarrera)
        { 
            List<EdicionMateria> ediciones = new List<EdicionMateria>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idCarrera", idCarrera)
            };
            DataTable tabla = gDatos.Consultar("pa_ObtenerEdicionesPorCarrera", parametros);
            foreach (DataRow fila in tabla.Rows)
            {
                EdicionMateria edicion = new EdicionMateria
                {
                    Id = Convert.ToInt32(fila["id_Edicion"]),
                    NombreMateria = fila["nombre_materia"].ToString(),
                    Grupo = fila["grupo"].ToString(),
                    Anio = Convert.ToInt32(fila["anio"])
                };
                ediciones.Add(edicion);
            }
            return ediciones;
        }
        //pa_GestionesCursadasPorEstudiante
        public static List<Gestion> GestionesCursadasPorEstudiante(int idEstudiante)
        { 
            List<Gestion> gestiones = new List<Gestion>();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEstudiante", idEstudiante)
            };
            DataTable tabla = gDatos.Consultar("pa_GestionesCursadasPorEstudiante", parametros);
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
    }
}
