using Capa_Negocios;
using Capa_Negocios.Clases;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para PagInscripcionMateria.xaml
    /// </summary>
    public partial class PagInscripcionMateria : Page
    {
        Estudiante estudiante = new Estudiante();

        public PagInscripcionMateria()
        {
            InitializeComponent();

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Estudiante est = estudiante.obtenerInfoEstudiante(int.Parse(txtIdEstudiante.Text));
            txtNombreEstudiante.Text = est.Nombre + " " + est.Apellido;
            txtNombreCarrera.Text = est.NombreCarrera;
        }

        private void SeleccionarPlan(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgMateriasDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
