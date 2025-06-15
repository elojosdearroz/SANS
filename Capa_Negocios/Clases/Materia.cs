using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Grupo { get; set; }
        public int Creditos { get; set; }
        public int Nota { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin{ get; set; }
        public string Horario => $"{HoraInicio} - {HoraFin}";
    }
}
