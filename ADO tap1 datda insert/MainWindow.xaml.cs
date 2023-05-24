using System.Windows;
using System.Data.SqlClient;

namespace ADO_tap1_datda_insert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=Human;Integrated Security=True;";

        public MainWindow()
        {
            InitializeComponent();
            LoadDataToListbox(); 

        }

        private void LoadDataToListbox()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT id, name, surname FROM People";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string surname = reader.GetString(2);
                            listbox1.Items.Add($"{id} {name} {surname}");
                        }
                    }
                }
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = name1.Text;
            string surname = surname1.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO People (name, surname) VALUES (@name, @surname)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            listbox1.Items.Add($"{name} {surname}");
        }
    }
}
      
    
    






