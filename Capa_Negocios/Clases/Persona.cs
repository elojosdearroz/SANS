using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocios
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CI { get; set; }
        public int Contrasena { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";

    }
}
