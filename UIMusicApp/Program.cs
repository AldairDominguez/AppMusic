using System;
using System.Windows.Forms;

namespace UIMusicApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Inicialización de la configuración de la aplicación
            // ApplicationConfiguration.Initialize();

            // Obtiene el formulario principal usando el método GetMainForm de la clase go
            // var mainForm = go.GetMainForm();

            // Ejecuta la aplicación con el formulario principal
            //System.Windows.Forms.Application.Run(mainForm);


            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Crear una instancia del formulario Admin
            var ReproductorForm = new  Carga();

            // Ejecutar la aplicación con el formulario Admin
            System.Windows.Forms.Application.Run(ReproductorForm);


        }
    }
}