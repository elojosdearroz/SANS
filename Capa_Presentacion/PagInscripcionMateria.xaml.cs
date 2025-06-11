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
        int idCarrera;
        int idEstudiante;
        Materia materiaSeleccionada;
        List<Materia> materiasSeleccionadas = new List<Materia>();
        public PagInscripcionMateria()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Estudiante est = RellenarFromsInscripcion.obtenerInfoEstudiante(int.Parse(txtIdEstudiante.Text));
            idEstudiante = est.Id;
            txtNombreEstudiante.Text = est.Nombre + " " + est.Apellido;
            txtNombreCarrera.Text = est.NombreCarrera;
            idCarrera= est.IdCarrera;

            List<Gestion> gestiones = RellenarFromsInscripcion.ObtenerGestiones();
            cmbPlanEstudio.ItemsSource = gestiones;
            cmbPlanEstudio.DisplayMemberPath = "Descripcion";
            cmbPlanEstudio.SelectedValuePath = "Id";
        }

        private void SeleccionarPlan(object sender, SelectionChangedEventArgs e)
        {
            List<Materia> ediciones = RellenarFromsInscripcion.CargarMateriasOfertadas(idCarrera, (int)cmbPlanEstudio.SelectedValue);
            dgMateriasDisponibles.ItemsSource = ediciones;
        }

        private void dgMateriasDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            materiaSeleccionada = (Materia)dgMateriasDisponibles.SelectedItem;
        }
        private void dgMateriasSeleccionadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            materiaSeleccionada = (Materia)dgMateriasSeleccionadas.SelectedItem;
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (materiaSeleccionada != null && !materiasSeleccionadas.Any(m => m.Id == materiaSeleccionada.Id))
            {
                materiasSeleccionadas.Add(materiaSeleccionada);
                dgMateriasSeleccionadas.ItemsSource = null;
                dgMateriasSeleccionadas.ItemsSource = materiasSeleccionadas;
                txtPrecioTotal.Text = CalcularPrecioTotal(materiasSeleccionadas).ToString();

            }
            else
            {
                MessageBox.Show("La materia ya fue seleccionada.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            materiasSeleccionadas.Remove(materiaSeleccionada);
            dgMateriasSeleccionadas.ItemsSource = null;
            dgMateriasSeleccionadas.ItemsSource = materiasSeleccionadas;
            txtPrecioTotal.Text = CalcularPrecioTotal(materiasSeleccionadas).ToString();
        }

        private string CalcularPrecioTotal(List<Materia> materiasSeleccionadas)
        {
            int totalbs = 0;
            int totalCreditos= 0;
            foreach (var materia in materiasSeleccionadas)
            {
                totalbs += 500 * materia.Creditos;
                totalCreditos += materia.Creditos;
            }
            return $"{totalCreditos} créditos : {totalbs} Bs.";
        }

        private void btnInscribir_Click(object sender, RoutedEventArgs e)
        {
            if (materiasSeleccionadas == null || materiasSeleccionadas.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una materia para inscribirse.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool resultado = RellenarFromsInscripcion.IncribirMateriaEstudiante(idEstudiante, materiasSeleccionadas);

            if (resultado)
            {
                MessageBox.Show("Inscripción realizada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                materiasSeleccionadas.Clear();
                dgMateriasSeleccionadas.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("No se pudo realizar la inscripción. Verifique los datos o intente nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
