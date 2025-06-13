using Capa_Negocios;
using Capa_Negocios.Clases;
using Capa_Presentacion.Resources.DataSet1TableAdapters;
using Capa_Presentacion.Resources.DataSet2TableAdapters;
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
    /// Lógica de interacción para PagReporteAsistencia.xaml
    /// </summary>
    public partial class PagReporteAsistencia : Page
    {
        public PagReporteAsistencia()
        {
            InitializeComponent();
            List<Carrera> carreras = RellenarFromsReportes.ObtenerCarreras();

            cmbCarreras.ItemsSource = carreras;
            cmbCarreras.DisplayMemberPath = "Nombre";
            cmbCarreras.SelectedValuePath = "Id";
        }

        private void cmbCarreras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idCarrera = (int)cmbCarreras.SelectedValue;
            cmbMaterias.ItemsSource = RellenarFromsReportes.ObtenerEdicionesPorCarrera(idCarrera);
            cmbMaterias.DisplayMemberPath = "Descripcion";
            cmbMaterias.SelectedValuePath = "Id";
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            int idEdicion = (int)cmbMaterias.SelectedValue;
            try
            {
                var adaptador = new pa_ReporteAsistenciasPorEdicionTableAdapter();
                var tabla = adaptador.GetData(idEdicion);

                string rutaReporte = @"C:\Users\nacho\Desktop\ISI\Semestre III\SANS\SANS\Capa_Presentacion\Resources\ReporteAsistenciasPorEdicion.rdlc";

                reportViewer.ReportPath = rutaReporte;

                var datos = tabla.AsEnumerable()
                .Select(row => new Asistencia
                {
                    nombre_estudiante = row.Field<string>("nombre_estudiante"),
                    fecha = row.Field<DateTime>("fecha")
                }).ToList();

                reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                reportViewer.ReportPath = rutaReporte;
                reportViewer.DataSources.Clear();
                reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource
                {
                    Name = "ReporteAsistenciasPorEdicion",
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
