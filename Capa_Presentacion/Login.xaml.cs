using System;
using System.Windows;

namespace Capa_Presentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        // Cierra completamente la aplicación
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Botón de iniciar sesión
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Aquí podrías validar credenciales antes de abrir el MainWindow
            new MainWindow().Show();
            this.Close(); // Cierra la ventana de login
        }
    }
}
