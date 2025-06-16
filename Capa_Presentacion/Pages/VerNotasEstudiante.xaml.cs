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
    /// Lógica de interacción para VerNotasEstudiante.xaml
    /// </summary>
    public partial class VerNotasEstudiante : Page
    {
        private Usuario usuario;
        public VerNotasEstudiante()
        {
            InitializeComponent();
            usuario = Capa_Negocios.Login.Usuarioactual;
            Estudiante estudiante = usuario as Estudiante;

            txtNombreEstudiante.Text = estudiante.Nombre + " " + estudiante.Apellido;
            txtNombreCarrera.Text = estudiante.NombreCarrera;

            cmbGestion.ItemsSource = RellenarFromsReportes.GestionesCursadasPorEstudiante(estudiante.Id);
            cmbGestion.DisplayMemberPath = "Descripcion";
            cmbGestion.SelectedValuePath = "Id";
        }
        private void cmbGestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgNotas.ItemsSource = null;
            dgNotas.ItemsSource = gNegocios.ReporteNotasMateriasPorEstudiante(usuario.Id, (int)cmbGestion.SelectedValue);
        }
    }
}
