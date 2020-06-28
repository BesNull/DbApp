using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaitanProjectUltraBD
{
    public partial class OrderUI : Form
    {
        public string linkdb;
        SqlConnection sqlConnection1;
        int i = 1;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        public OrderUI()
        {
            InitializeComponent();
        }


        private async void OrderUI_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + linkdb + ";Integrated Security=True";
            sqlConnection1 = new SqlConnection(connectionString);
            await sqlConnection1.OpenAsync();
            LoadData();
        }


        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("Select *, 'Delete' AS [Edit] from Products", sqlConnection1);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Products");

                dataGridView1.DataSource = dataSet.Tables["Products"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, i] = linkCell;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BasketAddButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection1.State.ToString() == "Closed")
            {
                await sqlConnection1.OpenAsync();
            }
            string check = string.Empty;
            if (string.IsNullOrWhiteSpace(ProductIDBox.Text) && string.IsNullOrWhiteSpace(QuantityBox.Text))// && FIOBox.TextLength!=0 && PhoneBox.TextLength != 0 && EmailBox.TextLength != 0 && PassBox.TextLength != 0)
            {
                MessageBox.Show("Не все поля заполнены");

            }
            else
            {
                /*
                SqlCommand commandAdd = new SqlCommand("INSERT INTO [Basket] (BasketID, , OrderID, ProductID, Quantity, Price, Sum) VALUES (@BasketID, , @OrderID, @ProductID, @Quantity, @Price, @Sum)", sqlConnection1);
                commandAdd.Parameters.AddWithValue("@BasketID", i);
                commandAdd.Parameters.AddWithValue("@OrderID", FIOBox.Text);
                commandAdd.Parameters.AddWithValue("@Phone", PhoneBox.Text);
                commandAdd.Parameters.AddWithValue("@Email", EmailBox.Text);
                commandAdd.Parameters.AddWithValue("@Password", PassRegBox.Text);
                await commandAdd.ExecuteNonQueryAsync();
                // MessageBox.Show("Учетная запись успешно создана, входите скорей");
                i++;
                */
            }
        }

     
    }
}
