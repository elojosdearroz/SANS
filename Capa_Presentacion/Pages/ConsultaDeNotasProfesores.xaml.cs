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
    /// Lógica de interacción para ConsultaDeNotasProfesores.xaml
    /// </summary>
    public partial class ConsultaDeNotasProfesores : Page
    {
        private Usuario usuario;
        public ConsultaDeNotasProfesores()
        {
            InitializeComponent();
            usuario = Capa_Negocios.Login.Usuarioactual;
            cmbMaterias.ItemsSource = gNegocios.ObtenerEdicionesPorDocente(usuario.Id);
            cmbMaterias.DisplayMemberPath = "NombreMateria";
            cmbMaterias.SelectedValuePath = "Id";
        }

        private void cmbMaterias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgEstudiantesNotas.ItemsSource = null;
            dgEstudiantesNotas.ItemsSource = gNegocios.ObtenerEstudiantesPorEdicion((int)cmbMaterias.SelectedValue);
        }

        private void dgEstudiantesNotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EdicionMateria estudiante = (EdicionMateria)dgEstudiantesNotas.SelectedItem;

            lblIdEstudianteEdit.Text = estudiante.Estudiante.Id.ToString();
            lblNombreEstudianteEdit.Text = estudiante.Estudiante.Nombre.ToString();
            lblCarreraEstudianteEdit.Text = estudiante.Estudiante.NombreCarrera.ToString(); 
            txtNota1.Text = estudiante.Nota1.ToString();
            txtNota2.Text = estudiante.Nota2.ToString();
            txtNota3.Text = estudiante.Nota3.ToString();
            txtNota4.Text = estudiante.Nota4.ToString();
        }

        private void btnGuardarNotas_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
