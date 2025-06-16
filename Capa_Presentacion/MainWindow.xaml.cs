using Capa_Datos;
using Capa_Negocios;
using Capa_Negocios.Clases;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Capa_Presentacion
{
    public partial class MainWindow : Window
    {
        private bool isSidebarExpanded = false;
        private Usuario usuario;
        public MainWindow()
        {
            InitializeComponent();
            InitializeSidebar();

            CargarUsuario();
            ConfigurarMenuSegunUsuario();
        }

        //Carga los datos del usuario actual en la tarjeta de usuario
        private void CargarUsuario()
        {
            usuario = Capa_Negocios.Login.Usuarioactual;
            txtNombreUsuario.Text = usuario.Nombre + " "+ usuario.Apellido; 

            if (usuario is Estudiante)
                txtRolUsuario.Text = "Estudiante";
            else if (usuario is Docente)
                txtRolUsuario.Text = "Docente";
            else if (usuario is Administrativo)
                txtRolUsuario.Text = "Administrativo";
            else
                txtRolUsuario.Text = "Rol desconocido";
        }

        private void ConfigurarMenuSegunUsuario()
        {
            btnInscribirMaterias.Visibility = Visibility.Collapsed;
            btnImprimirReporte.Visibility = Visibility.Collapsed;
            btnReporteAsistencia.Visibility = Visibility.Collapsed;
            btnReporteNotas.Visibility = Visibility.Collapsed;
            btnConsultarNotas.Visibility = Visibility.Collapsed;
            btnConsultarMaterias.Visibility = Visibility.Collapsed;
            btnVerNotas.Visibility = Visibility.Collapsed;

            if (usuario is Estudiante)
            {
                btnVerNotas.Visibility = Visibility.Visible;
                // El estudiante quizá no vea reportes
            }
            else if (usuario is Docente)
            {
                btnConsultarMaterias.Visibility = Visibility.Visible;
                btnConsultarNotas.Visibility = Visibility.Visible;
                // Docente puede ver reportes
            }
            else if (usuario is Administrativo)
            {
                // Administrativo puede ver todo, por ejemplo
                btnInscribirMaterias.Visibility = Visibility.Visible;
                btnImprimirReporte.Visibility = Visibility.Visible;
                btnReporteAsistencia.Visibility = Visibility.Visible;
                btnReporteNotas.Visibility = Visibility.Visible;
            }
        }

        // Inicializa la barra lateral colapsada
        private void InitializeSidebar()
        {
            SidebarBorder.Width = 60;
            SidebarContent.Opacity = 0;
        }

        // Alternar entre expandir y colapsar el sidebar
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!isSidebarExpanded)
            {
                ExpandSidebar();
            }
            else
            {
                CollapseSidebar();
            }

            isSidebarExpanded = !isSidebarExpanded;
        }

        // Expande el sidebar con animación
        private void ExpandSidebar()
        {
            Storyboard expandSidebar = (Storyboard)FindResource("ExpandSidebar");
            expandSidebar.Begin();

            Storyboard expandCard = (Storyboard)FindResource("ExpandCardUsuario");
            expandCard.Begin();
        }

        private void CollapseSidebar()
        {
            Storyboard collapseSidebar = (Storyboard)FindResource("CollapseSidebar");
            collapseSidebar.Begin();

            Storyboard collapseCard = (Storyboard)FindResource("CollapseCardUsuario");
            collapseCard.Begin();
        }

        // Navega a la página de inscripción de materias
        private void InscribirMaterias_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagInscripcionMateria());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        // Confirma y cierra la sesión actual
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                                         "Confirmar",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Capa_Negocios.Login.CerrarSesion();
                new Login().Show();
                this.Close(); 
            }
        }

        // Confirma y cierra completamente la aplicación
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea salir del sistema?",
                                         "Confirmar salida",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        // Permite mover la ventana con clic sostenido (porque no tiene barra de título de Windows)
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            base.OnMouseLeftButtonDown(e);
        }

        private void ImprimirRepote_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagReportes());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void ReporteAsistencia_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagReporteAsistencia());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void ReporteNotas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagReporteNotas());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void ConsultarNotas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ConsultaDeNotasProfesores());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void ConsultarMaterias_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagMateriasProfesor());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void VerNotas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new VerNotasEstudiante());
            CollapseSidebar();
            isSidebarExpanded = !isSidebarExpanded;
        }
    }
}
