﻿using Capa_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Capa_Presentacion
{
    /// <summary>
    /// Lógica de interacción para PagMateriasProfesor.xaml
    /// </summary>
    public partial class PagMateriasProfesor : Page
    {
        private Usuario usuario;
        public PagMateriasProfesor()
        {
            InitializeComponent();
            usuario = Capa_Negocios.Login.Usuarioactual;

            txtNombreProfesor.Text = usuario.Nombre + " " + usuario.Apellido;

            CargarMaterias();
        }

        private void CargarMaterias()
        {
            dgClases.ItemsSource = null;
            dgClases.ItemsSource = gNegocios.ObtenerEdicionesPorDocente(usuario.Id);
        }
    }
}
