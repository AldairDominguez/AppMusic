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
            // Inicializaci�n de la configuraci�n de la aplicaci�n
            // ApplicationConfiguration.Initialize();

            // Obtiene el formulario principal usando el m�todo GetMainForm de la clase go
            // var mainForm = go.GetMainForm();

            // Ejecuta la aplicaci�n con el formulario principal
            //System.Windows.Forms.Application.Run(mainForm);


            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Crear una instancia del formulario Admin
            var ReproductorForm = new  Carga();

            // Ejecutar la aplicaci�n con el formulario Admin
            System.Windows.Forms.Application.Run(ReproductorForm);


        }
    }
}