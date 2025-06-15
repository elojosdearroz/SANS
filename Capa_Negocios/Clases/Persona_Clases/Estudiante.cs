using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Negocios.Clases
{
    public class Estudiante : Usuario
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
    }
}
