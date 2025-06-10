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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        // Aqui se inicializa la barra laterail
        private void InitializeSidebar()
        {
            SidebarBorder.Width = 60;
            SidebarContent.Opacity = 0;
        }

        // Simplemente altera la barralateral
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

        // Expandir la barra lateral
        private void ExpandSidebar()
        {
            Storyboard expandStoryboard = (Storyboard)FindResource("ExpandSidebar");
            expandStoryboard.Begin();
        }

        // Colapsar la barra lateral
        private void CollapseSidebar()
        {
            Storyboard collapseStoryboard = (Storyboard)FindResource("CollapseSidebar");
            collapseStoryboard.Begin();
        }

        // El boton inscribir materias y lleva al nuevo frame
        private void InscribirMaterias_Click(object sender, RoutedEventArgs e)
        {
            // Aquí navegarías a la página de inscripción de materias
             MainFrame.Navigate(new inscripcionMateria());
        }

        // Cerrar sesión
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                                       "Confirmar",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Aquí deberiamos poenr que abra de nuevo dlo del login
                Application.Current.Shutdown();
            }
        }

        // Cerrar la ventana
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

        // Esto si quieren lo quitan o no c pero es para poder mover la ventana ya que le quite el estilo de windows
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
