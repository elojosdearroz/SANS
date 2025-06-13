using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios.Clases
{
    public class EdicionMateria
    {
        public int Id {get; set; }
        public string NombreMateria { get; set; }
        public string Grupo { get; set; }
        public int Anio { get; set; }
        public string Descripcion => $"{NombreMateria} : Grupo({Grupo}) : Gestion({Anio})";
    }
}
