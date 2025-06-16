using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class EdicionMateria
    {
        public int Id {get; set; }
        public Estudiante Estudiante { get; set; }
        public Materia Materia { get; set; }
        public string Grupo { get; set; }
        public int Anio { get; set; }
        public int Semestre { get; set; }
        public int Curso { get; set; }
        public string Bloque { get; set; }
        public string Docente { get; set; }
        public List<Nota> Notas { get; set; }
        public int NotaFinal { get; set; }
        public string Estado { get; set; } 
        public string Descripcion => $"{Materia.Nombre} : Grupo({Grupo}) : Gestion({Anio})";

        // Propiedades derivadas para el DataGrid
        public int IdEstudiante => Estudiante?.Id ?? 0;
        public string NombreEstudiante => Estudiante?.Nombre ?? "";
        public int CedulaEstudiante => Estudiante?.CI ?? 0; 
        public string NombreCarrera => Estudiante?.NombreCarrera ?? ""; // O Carrera, si ese campo existe
        public int IdMateria => Materia?.Id ?? 0;
        public string NombreMateria => Materia?.Nombre ?? "";
        public int Creditos => Materia?.Creditos ?? 0;

        public int Nota1 => Notas.FirstOrDefault(n => n.parcial == 1)?.nota ?? 0;
        public int Nota2 => Notas.FirstOrDefault(n => n.parcial == 2)?.nota ?? 0;
        public int Nota3 => Notas.FirstOrDefault(n => n.parcial == 3)?.nota ?? 0;
        public int Nota4 => Notas.FirstOrDefault(n => n.parcial == 4)?.nota ?? 0;
    }
}
