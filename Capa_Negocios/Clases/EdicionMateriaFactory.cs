using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Capa_Negocios.Clases
{
    public static class EdicionMateriaFactory
    {
        public static EdicionMateria FromDataRow(DataRow fila)
        {
            return new EdicionMateria
            {
                Id = fila.Table.Columns.Contains("id_edicion") ? Convert.ToInt32(fila["id_edicion"]) : 0,
                Materia = new Materia
                {
                    Id = fila.Table.Columns.Contains("id_materia") ? Convert.ToInt32(fila["id_materia"]) : 0,
                    Nombre = fila.Table.Columns.Contains("nombre_materia") ? fila["nombre_materia"].ToString() : "",
                    Creditos = fila.Table.Columns.Contains("creditos") ? Convert.ToInt32(fila["creditos"]) : 0,
                },
                Estudiante = fila.Table.Columns.Contains("id_estudiante") ? new Estudiante
                {
                    Id = Convert.ToInt32(fila["id_estudiante"]),
                    Nombre = fila.Table.Columns.Contains("nombre_estudiante") ? fila["nombre_estudiante"].ToString() : "",
                    CI = fila.Table.Columns.Contains("ci") ? Convert.ToInt32(fila["ci"]) : 0,
                    NombreCarrera = fila.Table.Columns.Contains("carrera") ? fila["carrera"].ToString() : ""
                } : null,
                Semestre = fila.Table.Columns.Contains("semestre") ? Convert.ToInt32(fila["semestre"]) : 0,
                Anio = fila.Table.Columns.Contains("anio") ? Convert.ToInt32(fila["anio"]) : 0,
                Grupo = fila.Table.Columns.Contains("grupo") ? fila["grupo"].ToString() : "",
                Curso = fila.Table.Columns.Contains("curso") ? Convert.ToInt32(fila["curso"]) : 0,
                Bloque = fila.Table.Columns.Contains("bloque") ? fila["bloque"].ToString() : "",
                Docente = fila.Table.Columns.Contains("nombre_docente") ? fila["nombre_docente"].ToString() : "",
                Notas = new List<Nota>
                {
                    new Nota { parcial = 1, nota = fila.Table.Columns.Contains("nota_parcial_1") && fila["nota_parcial_1"] != DBNull.Value ? Convert.ToInt32(fila["nota_parcial_1"]) : 0 },
                    new Nota { parcial = 2, nota = fila.Table.Columns.Contains("nota_parcial_2") && fila["nota_parcial_2"] != DBNull.Value ? Convert.ToInt32(fila["nota_parcial_2"]) : 0 },
                    new Nota { parcial = 3, nota = fila.Table.Columns.Contains("nota_parcial_3") && fila["nota_parcial_3"] != DBNull.Value ? Convert.ToInt32(fila["nota_parcial_3"]) : 0 },
                    new Nota { parcial = 4, nota = fila.Table.Columns.Contains("nota_parcial_4") && fila["nota_parcial_4"] != DBNull.Value ? Convert.ToInt32(fila["nota_parcial_4"]) : 0 },
                },
                NotaFinal = fila.Table.Columns.Contains("nota_final") && fila["nota_final"] != DBNull.Value ? Convert.ToInt32(fila["nota_final"]) : 0,
                Estado = fila.Table.Columns.Contains("estado") ? fila["estado"].ToString() : ""
            };
        }
    }
}

