using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public class MateriaNotaReporte
    {
        public int id_estudiante { get; set; }
        public string nombre_estudiante { get; set; }
        public string ci { get; set; }

        public string nombre_materia { get; set; }

        public int creditos { get; set; }
        public int semestre { get; set; }
        public int anio { get; set; }
        public string grupo { get; set; }
        public string nombre_docente { get; set; }
        public decimal? nota_parcial_1 { get; set; }
        public decimal? nota_parcial_2 { get; set; }
        public decimal? nota_parcial_3 { get; set; }
        public decimal? nota_parcial_4 { get; set; }
        public decimal nota_final { get; set; }
        public string estado { get; set; }
    }
}
