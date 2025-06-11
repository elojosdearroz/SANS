using Capa_Negocios;
using Capa_Negocios.Clases;
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
    /// Lógica de interacción para PagReportes.xaml
    /// </summary>
    public partial class PagReportes : Page
    {
        public PagReportes()
        {
            InitializeComponent();

            List<Carrera> carreras = RellenarFromsReportes.ObtenerCarreras();

            cmbCarrera.ItemsSource = carreras;
            cmbCarrera.DisplayMemberPath = "Nombre";
            cmbCarrera.SelectedValuePath = "Id";
        }
        private void cmbCarrera_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idCarrera = (int)cmbCarrera.SelectedValue;
            cmbGestion.ItemsSource = RellenarFromsReportes.ObtenerGestionesPorCarrera(idCarrera);
            cmbGestion.DisplayMemberPath = "Descripcion"; 
            cmbGestion.SelectedValuePath = "Id";

            cmbPlanEstudio.ItemsSource = RellenarFromsReportes.ObtenerPlanesPorCarrera(idCarrera);
        }

        private void cmbGestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
