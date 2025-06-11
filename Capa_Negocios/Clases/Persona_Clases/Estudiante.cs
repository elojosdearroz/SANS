using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Negocios.Clases
{
    public class Estudiante : Persona
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public int IdPlanEstudio { get; set; }
    }
}
