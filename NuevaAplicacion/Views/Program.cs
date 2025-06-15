using System; // Asegúrate de incluir este espacio de nombres
using System.Windows.Forms; // Este espacio de nombres es necesario para usar 'Application'
using NuevaAplicacion.Views;

namespace NuevaAplicacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}