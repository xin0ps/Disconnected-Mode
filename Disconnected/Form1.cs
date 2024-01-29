using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Disconnected
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string SqlConnect = "Data Source=LAPTOP-JLBUNNBV\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        SqlDataAdapter SqlDataAdapter = new SqlDataAdapter();
        DataTable dataTable = new DataTable();


        public void DisconnectDataRead()
        {
            string query = "Select * from Authors";
            SqlDataAdapter = new SqlDataAdapter(query, SqlConnect);
            SqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }



        private void btnfill_Click(object sender, EventArgs e)
        {
            DisconnectDataRead();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(SqlDataAdapter);
            SqlDataAdapter.Update(dataTable);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataTable.Clear();
            string name = textBox1.Text;

            
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Parameters.AddWithValue("@Name", "%" + name + "%"); 

         
            string query = "SELECT * FROM Authors WHERE Authors.FirstName LIKE @Name";
            selectCommand.CommandText = query;
            selectCommand.Connection = new SqlConnection(SqlConnect);
            SqlDataAdapter.SelectCommand = selectCommand;


            SqlDataAdapter.Fill(dataTable);

        
            dataGridView1.DataSource = dataTable;
        }
    }
}
