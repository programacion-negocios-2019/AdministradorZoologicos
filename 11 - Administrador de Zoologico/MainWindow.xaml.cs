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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// Agregando los namespaces necesarios para SQL Server
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace _11___Administrador_de_Zoologico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variable miembro
        SqlConnection sqlconnection;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // ZoologicoConnectionString
            string connectionString = ConfigurationManager.ConnectionStrings["_11___Administrador_de_Zoologico.Properties.Settings.ZoologicoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);

            // Llenar el ListBox de Zoológicos
            MostrarZoologicos();
        }

        private void MostrarZoologicos()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT * FROM Zoo.Zoologico";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaZoologico = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaZoologico);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    lbZoologicos.DisplayMemberPath = "ciudad";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    lbZoologicos.SelectedValuePath = "id";
                    // ¿Quién es la referencia de los datos para el ListBox (popular?
                    lbZoologicos.ItemsSource = tablaZoologico.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
