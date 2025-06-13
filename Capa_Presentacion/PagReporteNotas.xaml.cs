using Capa_Negocios;
using Capa_Negocios.Clases;
using Capa_Presentacion.Resources.DataSet2TableAdapters;
using Capa_Presentacion.Resources.DataSet3TableAdapters;
using Capa_Presentacion.Resources.DataSet4TableAdapters;
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
    /// Lógica de interacción para PagReporteNotas.xaml
    /// </summary>
    public partial class PagReporteNotas : Page
    {
        int idEstudiante;
        public PagReporteNotas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            idEstudiante = (int.Parse(txtCodigoEstudiante.Text));
            cmbMateria.ItemsSource = RellenarFromsReportes.GestionesCursadasPorEstudiante(idEstudiante);
            cmbMateria.DisplayMemberPath = "Descripcion";
            cmbMateria.SelectedValuePath  = "Id";
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            //pa_ReporteNotasMateriasPorEstudiante
            int idGestion = (int)cmbMateria.SelectedValue;
            int idEstudiante = this.idEstudiante;
            try
            {
                var adaptador = new pa_ReporteNotasMateriasPorEstudianteTableAdapter();
                var tabla = adaptador.GetData(idEstudiante,idGestion);

                string rutaReporte = @"C:\Users\nacho\Desktop\ISI\Semestre III\SANS\SANS\Capa_Presentacion\Resources\ReporteNotasMateriasPorEstudiante.rdlc";

                reportViewer.ReportPath = rutaReporte;

                var datos = tabla.AsEnumerable()
                .Select(row => new Nota
                {
                    nombre_estudiante = row.Field<string>("nombre_estudiante"),
                    nombre_materia = row.Field<string>("nombre_materia"),
                    nota = row.Field<int>("nota"),
                    nombre_docente = row.Field<string>("nombre_docente"),
                    grupo = row.Field<string>("grupo")
                }).ToList();

                reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                reportViewer.ReportPath = rutaReporte;
                reportViewer.DataSources.Clear();
                reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource
                {
                    Name = "ReporteNotasMateriasPorEstudiante",
                    Value = datos
                });

                reportViewer.RefreshReport();
                panelSinDatos.Visibility = Visibility.Collapsed;
                reportViewer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }

        private void btnNotasAprobadas_Click(object sender, RoutedEventArgs e)
        {

            //pa_ReporteNotasMateriasPorEstudiante
            int idGestion = (int)cmbMateria.SelectedValue;
            int idEstudiante = this.idEstudiante;
            try
            {
                var adaptador = new pa_ReporteNotasMateriasAprobadasPorEstudianteTableAdapter();
                var tabla = adaptador.GetData(idEstudiante, idGestion);

                string rutaReporte = @"C:\Users\nacho\Desktop\ISI\Semestre III\SANS\SANS\Capa_Presentacion\Resources\ReporteNotasMateriasAprobadasPorEstudiante.rdlc";

                reportViewer.ReportPath = rutaReporte;

                var datos = tabla.AsEnumerable()
                .Select(row => new Nota
                {
                    nombre_estudiante = row.Field<string>("nombre_estudiante"),
                    nombre_materia = row.Field<string>("nombre_materia"),
                    nota = row.Field<int>("nota"),
                    nombre_docente = row.Field<string>("nombre_docente"),
                    grupo = row.Field<string>("grupo")

                }).ToList();

                reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                reportViewer.ReportPath = rutaReporte;
                reportViewer.DataSources.Clear();
                reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource
                {
                    Name = "ReporteNotasMateriasAprobadasPorEstudiante",
                    Value = datos
                });

                reportViewer.RefreshReport();
                panelSinDatos.Visibility = Visibility.Collapsed;
                reportViewer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }
    }
}
