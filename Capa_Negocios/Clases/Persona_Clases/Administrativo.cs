using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class Administrativo: Persona
    {
        public string Cargo { get; set; }
        public int Sueldo { get; set; }
    }
}
