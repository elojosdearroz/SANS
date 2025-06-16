using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class MateriaOfertaReporte
    {
        public int id_materia { get; set; }
        public string nombre_materia { get; set; }
        public int creditos { get; set; }
        public int semestre { get; set; }
        public int anio { get; set; }
        public string nombre_docente { get; set; }
    }
}
