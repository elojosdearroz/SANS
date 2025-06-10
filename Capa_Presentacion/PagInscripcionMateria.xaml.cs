using Capa_Negocios;
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
        private DataTable materiasSeleccionadas = new DataTable();
        private DataRowView filaSeleccionada = null;


        public PagInscripcionMateria()
        {
            InitializeComponent();

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            RellenarFroms.RellenarFormulario(int.Parse(txtIdEstudiante.Text));
            txtNombreEstudiante.Text = RellenarFroms.NombreEstudiante;
            txtNombreCarrera.Text = RellenarFroms.NombreCarrera;

            cmbPlanEstudio.ItemsSource = RellenarFroms.PlanesEstudio;
            cmbPlanEstudio.DisplayMemberPath = "Value";
            cmbPlanEstudio.SelectedValuePath = "Key";
        }

        private void SeleccionarPlan(object sender, SelectionChangedEventArgs e)
        {
            int idPlan = (int)cmbPlanEstudio.SelectedValue;
            DataTable materias = RellenarFroms.ObtenerMateriasPorPlan(idPlan);

            dgMateriasDisponibles.ItemsSource = materias.DefaultView;

            // Copiar estructura al inicio
            materiasSeleccionadas = materias.Clone();
            dgMateriasSeleccionadas.ItemsSource = materiasSeleccionadas.DefaultView;
        }

        private void dgMateriasDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filaSeleccionada = dgMateriasDisponibles.SelectedItem as DataRowView;
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (filaSeleccionada != null)
            {
                DataRow nuevaFila = materiasSeleccionadas.NewRow();
                nuevaFila.ItemArray = filaSeleccionada.Row.ItemArray;
                materiasSeleccionadas.Rows.Add(nuevaFila);

                // Limpia la selección después de agregar
                dgMateriasDisponibles.UnselectAll();
                filaSeleccionada = null;
            }
        }
    }
}
