using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Capa_Datos;
using Capa_Negocios;

namespace Capa_Presentacion
{
    public partial class MainWindow : Window
    {
        private bool isSidebarExpanded = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSidebar();
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
            Storyboard expandStoryboard = (Storyboard)FindResource("ExpandSidebar");
            expandStoryboard.Begin();
        }

        // Colapsa el sidebar con animación
        private void CollapseSidebar()
        {
            Storyboard collapseStoryboard = (Storyboard)FindResource("CollapseSidebar");
            collapseStoryboard.Begin();
        }

        // Navega a la página de inscripción de materias
        private void InscribirMaterias_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new inscripcionMateria());
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
                // Aquí puedes navegar de vuelta al login si tienes uno, o cerrar la app
                Application.Current.Shutdown();
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
    }
}
