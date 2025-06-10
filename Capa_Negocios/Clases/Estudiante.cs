using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class Estudiante:Persona
    {
        public int IdCarrera { get; set; }
        public int IdPlanEstudio { get; set; }
    }
}
