using Capa_Negocios;
using Capa_Negocios.Clases;
using Capa_Presentacion.Resources;
using Capa_Presentacion.Resources.DataSet2TableAdapters;
using Capa_Presentacion.Resources.DataSet3TableAdapters;
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
                var dataSet = new DataSet3();
                dataSet.EnforceConstraints = false;

                adaptador.Fill(dataSet.pa_ReporteNotasMateriasPorEstudiante, idEstudiante, idGestion);

                var tabla = dataSet.pa_ReporteNotasMateriasPorEstudiante;

                string rutaReporte = @"C:\Users\nacho\Desktop\ISI\Semestre III\SANS\SANS\Capa_Presentacion\Resources\ReporteNotasMateriasPorEstudiante.rdlc";

                var datos = tabla.AsEnumerable()
                .Select(row => new MateriaNotaReporte
                {
                    id_estudiante = row.IsNull("id_estudiante") ? 0 : row.Field<int>("id_estudiante"),
                    nombre_estudiante = row.IsNull("nombre_estudiante") ? "" : row.Field<string>("nombre_estudiante"),
                    ci = row.IsNull("ci") ? "" : row.Field<string>("ci"),
                    nombre_materia = row.IsNull("nombre_materia") ? "" : row.Field<string>("nombre_materia"),
                    creditos = row.IsNull("creditos") ? 0 : row.Field<int>("creditos"),
                    semestre = row.IsNull("semestre") ? 0 : row.Field<int>("semestre"),
                    anio = row.IsNull("anio") ? 0 : row.Field<int>("anio"),
                    grupo = row.IsNull("grupo") ? "" : row.Field<string>("grupo"),
                    nombre_docente = row.IsNull("nombre_docente") ? "" : row.Field<string>("nombre_docente"),
                    nota_parcial_1 = row.IsNull("nota_parcial_1") ? 0m : row.Field<decimal>("nota_parcial_1"),
                    nota_parcial_2 = row.IsNull("nota_parcial_2") ? 0m : row.Field<decimal>("nota_parcial_2"),
                    nota_parcial_3 = row.IsNull("nota_parcial_3") ? 0m : row.Field<decimal>("nota_parcial_3"),
                    nota_parcial_4 = row.IsNull("nota_parcial_4") ? 0m : row.Field<decimal>("nota_parcial_4"),
                    nota_final = row.IsNull("nota_final") ? 0m : row.Field<decimal>("nota_final"),
                    estado = row.IsNull("estado") ? "" : row.Field<string>("estado"),
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
                var adaptador = new pa_ReporteNotasMateriasPorEstudianteTableAdapter();
                var dataSet = new DataSet3();
                dataSet.EnforceConstraints = false;

                adaptador.Fill(dataSet.pa_ReporteNotasMateriasPorEstudiante, idEstudiante, idGestion);

                var tabla = dataSet.pa_ReporteNotasMateriasPorEstudiante;

                string rutaReporte = @"C:\Users\nacho\Desktop\ISI\Semestre III\SANS\SANS\Capa_Presentacion\Resources\ReporteNotasMateriasPorEstudiante.rdlc";

                var datos = tabla.AsEnumerable()
                .Where(row => !row.Isnota_finalNull() && row.nota_final >= 51) 
                .Select(row => new MateriaNotaReporte
                {
                    id_estudiante = row.id_estudiante,
                    nombre_estudiante = row.nombre_estudiante ?? "",
                    ci = row.ci ?? "",
                    nombre_materia = row.nombre_materia ?? "",
                    creditos = row.creditos,
                    semestre = row.semestre,
                    anio = row.anio,
                    grupo = row.grupo ?? "",
                    nombre_docente = row.nombre_docente ?? "",
                    nota_parcial_1 = row.Isnota_parcial_1Null() ? 0 : row.nota_parcial_1,
                    nota_parcial_2 = row.Isnota_parcial_2Null() ? 0 : row.nota_parcial_2,
                    nota_parcial_3 = row.Isnota_parcial_3Null() ? 0 : row.nota_parcial_3,
                    nota_parcial_4 = row.Isnota_parcial_4Null() ? 0 : row.nota_parcial_4,
                    nota_final = row.nota_final,
                    estado = row.estado ?? ""
                }).ToList();
                if (datos == null || datos.Count == 0)
                {
                    MessageBox.Show("No hay materias aprobadas para mostrar en el reporte.", "Sin datos", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                // Configurar el reporte
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
