using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class Gestion
    {
        public int Id { get; set; }
        public int Semestre { get; set; }
        public int Anio { get; set; }
        public string Descripcion => $"SEMESTRE {Semestre} : AÑO {Anio}";
    }
}
