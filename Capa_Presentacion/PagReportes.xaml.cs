using BoldReports.UI.Xaml;
using Capa_Negocios;
using Capa_Negocios.Clases;
using Capa_Presentacion.Resources;
using Capa_Presentacion.Resources.DataSet1TableAdapters;
using Microsoft.Reporting.WinForms;
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
        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            int idCarrera = (int)cmbCarrera.SelectedValue;
            int idPlan = (int)cmbPlanEstudio.SelectedValue;
            int idGestion = (int)cmbGestion.SelectedValue;

            try
            {
                var adaptador = new pa_ReporteMateriasOfertadasTableAdapter();
                var tabla = adaptador.GetData(idCarrera, idPlan, idGestion);

                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados.");
                    return;
                }
                string rutaReporte = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ReporteMateriasOfertadas.rdlc");
                if (!System.IO.File.Exists(rutaReporte))
                {
                    MessageBox.Show("No se encontró el archivo RDLC en la ruta: " + rutaReporte);
                    return;
                }
                reportViewer.ReportPath = rutaReporte;

                var datos = tabla.AsEnumerable()
                .Select(row => new MateriaOfertada
                {
                    id_Materia = row.Field<int>("id_Materia"),
                    NombreMateria = row.Field<string>("NombreMateria"),
                    creditos = row.Field<int>("creditos"),
                    semestre = row.Field<int>("semestre"),
                    anio = row.Field<int>("anio"),
                    hora_Inicio = row.Field<TimeSpan>("hora_inicio"),
                    hora_Fin = row.Field<TimeSpan>("hora_fin"),
                    num_Aula = row.Field<string>("num_aula"),
                    bloque = row.Field<string>("bloque"),
                    NombreDocente = row.Field<string>("NombreDocente")
                }).ToList();

                reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                reportViewer.ReportPath = rutaReporte;
                reportViewer.DataSources.Clear();
                reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource
                {
                    Name = "ReporteMateriasOfertadas", 
                    Value = datos
                });

                reportViewer.RefreshReport();

                reportViewer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }

    }
}
