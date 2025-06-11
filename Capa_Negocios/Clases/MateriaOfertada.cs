using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public class MateriaOfertada
    {
        public int id_Materia { get; set; }
        public string NombreMateria { get; set; }
        public int creditos { get; set; }
        public int semestre { get; set; }
        public int anio { get; set; }
        public TimeSpan hora_Inicio { get; set; }
        public TimeSpan hora_Fin { get; set; }
        public string num_Aula { get; set; }
        public string bloque { get; set; }
        public string NombreDocente { get; set; }
    }

}
