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
    public partial class UserUI : Form
    {
       // public System.Windows.Forms.TextBox NickBox;
        public string linkdb;
        SqlConnection sqlConnection;
        SqlDataReader reader = null;
        SqlCommand commandnick;
        private SqlCommandBuilder AllBasketBuilder = null;
        private SqlCommandBuilder MyOrdersBuilder = null;
        private SqlCommandBuilder SearchBuilder = null;
        private SqlCommandBuilder OrdersBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlDataAdapter MyOrdersAdapter = null;
        private SqlDataAdapter sqlDataAdapter2 = null;
        private SqlDataAdapter AllBasketAdapter = null;
        private SqlDataAdapter SearchAdapter = null;
        private DataSet dataSet = null;
        private DataSet dataSet1 = null;
        private DataSet dataSet2 = null;
        private DataSet OrdersSet = null;

        private DataSet AllBasketSet = null;
        private DataSet SearchSet = null;
        private DataSet EmptyBasketSet = null;
        private SqlDataAdapter sqlDataAdapter5 = null;
        private SqlDataAdapter OrdersAdapter = null;

        int countOrders;
        int Orderid;
        int countBaskets;
        int BasketNum;
        int OrdNum;
        bool f=false;
       
        public UserUI()
        {
            InitializeComponent();
        }

        private void OrderSearch()
        {
            try
            {
                SearchAdapter = new SqlDataAdapter("Select ProductName, [Basket].BasketID, [Basket].OrderID, Quantity, Sum from [Basket],[Orders], [Products]" +
                    " where [Basket].ProductID = [Products].ProductID and [Orders].OrderID = [Basket].OrderID and [Pioneer_nickname]=@Pioneer_nickname and [Orders].OrderID=@OrderID", sqlConnection);
                //
                SearchAdapter.SelectCommand.Parameters.AddWithValue("Pioneer_nickname", NickBox.Text);
                SearchAdapter.SelectCommand.Parameters.AddWithValue("OrderID", OrderSearchBox.Text);
                SearchBuilder = new SqlCommandBuilder(SearchAdapter);

                SearchSet = new DataSet();

                SearchAdapter.Fill(SearchSet, "Basket");

                dataGridView3.DataSource = SearchSet.Tables["Basket"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteOrder()
        {
            try
            {
                if (sqlConnection.State.ToString() == "Closed")
                {
                    await sqlConnection.OpenAsync();
                }
                SqlCommand command = new SqlCommand("Delete from Orders where [Pioneer_nickname]=@Pioneer_nickname and [OrderID]=@OrderID", sqlConnection);
                command.Parameters.AddWithValue("Pioneer_nickname",NickBox.Text);
                int order;
                order = int.Parse(OrderSearchBox.Text);
                command.Parameters.AddWithValue("OrderID", order);
                await command.ExecuteNonQueryAsync();
                MyOrdersLoadData();
               // ReloadOrdersData();
                BasketLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OrdersLoadData()
        {
            try
            {
                OrdersAdapter = new SqlDataAdapter("Select * from [Orders]", sqlConnection);
                
                OrdersBuilder = new SqlCommandBuilder(OrdersAdapter);

                OrdersBuilder.GetInsertCommand();
                OrdersBuilder.GetUpdateCommand();
                OrdersBuilder.GetDeleteCommand();

                OrdersSet = new DataSet();

                OrdersAdapter.Fill(OrdersSet, "Orders");

                //dataGridView2.DataSource = OrdersSet.Tables["Orders"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MyOrdersLoadData()
        {
            try
            {
                MyOrdersAdapter = new SqlDataAdapter("Select * from [Orders] where [Pioneer_nickname] = @Pioneer_nickname", sqlConnection);
                MyOrdersAdapter.SelectCommand.Parameters.AddWithValue("Pioneer_nickname", NickBox.Text);
                MyOrdersBuilder = new SqlCommandBuilder(MyOrdersAdapter);

                MyOrdersBuilder.GetInsertCommand();
                //AllBasketBuilder.GetUpdateCommand();
                MyOrdersBuilder.GetDeleteCommand();

                dataSet1 = new DataSet();

                MyOrdersAdapter.Fill(dataSet1, "Orders");

                dataGridView2.DataSource = dataSet1.Tables["Orders"];
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void ProductsLoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("Select * from Products", sqlConnection);
                AllBasketBuilder = new SqlCommandBuilder(sqlDataAdapter);

                AllBasketBuilder.GetInsertCommand();
                AllBasketBuilder.GetUpdateCommand();
                AllBasketBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Products");

                dataGridView1.DataSource = dataSet.Tables["Products"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UserUI_Load(object sender, EventArgs e)
        {
            
            NickBox.Enabled = false;
            FIOBox.Enabled = false;
            PhoneBox.Enabled = false;
            EmailBox.Enabled = false;
            PassBox.Enabled = false;
            SaveProfileButton.Enabled = false;
            DiscardChangesProfile.Enabled = false;
            OrderConfirmingButton.Enabled = false;
            BasketAddButton.Enabled = false ;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+linkdb+";Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            commandnick = new SqlCommand("Select * from [Users] where [Nickname] = @Nickname", sqlConnection);

            commandnick.Parameters.AddWithValue("Nickname", NickBox.Text);

            reader = commandnick.ExecuteReader();
            reader.Read();
            FIOBox.Text = reader["FIO"].ToString();
            PhoneBox.Text = reader["Phone"].ToString();
            EmailBox.Text = reader["Email"].ToString();
            reader.Close();
            ProductsLoadData();
            MyOrdersLoadData();
            BasketLoad();
            OrdersLoadData();
        //    EmptyBasketLoadData();

            sqlConnection.Close();
     
        }

        private void ChangeProfileButton_Click(object sender, EventArgs e)
        {
            if (FIOBox.Enabled==false)
            
               // NickBox.Enabled = true;
                FIOBox.Enabled = true;
                PhoneBox.Enabled = true;
                EmailBox.Enabled = true;
                PassBox.Enabled = true;
            ChangeProfileButton.Enabled = false;
            SaveProfileButton.Enabled = true;
            DiscardChangesProfile.Enabled = true;
            OrderButton.Enabled = false;
                //   updatecom.Parameters.Add("@Password", SqlDbType.NVarChar).Value = PassBox.Text;
                // updatecom.Connection.Open();
                //  updatecom.ExecuteNonQuery();
        }

        private async void SaveProfileButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection.State.ToString() == "Closed")
            {
                await sqlConnection.OpenAsync();
            }
            SqlCommand updatecom = new SqlCommand("UPDATE [Users] SET [FIO]=@FIO, [Phone]=@Phone, [Email]=@Email WHERE [Nickname]=@Nickname", sqlConnection);
            updatecom.Parameters.AddWithValue("Nickname", NickBox.Text);

            updatecom.Parameters.AddWithValue("FIO", FIOBox.Text);
            updatecom.Parameters.AddWithValue("Phone", PhoneBox.Text);
            updatecom.Parameters.AddWithValue("Email", EmailBox.Text);
            await updatecom.ExecuteNonQueryAsync();

            if (!string.IsNullOrWhiteSpace(PassBox.Text))
            {
                SqlCommand passupdate = new SqlCommand("Update [Users] SET [Password]=@Password Where [Nickname] = @Nickname", sqlConnection);
                passupdate.Parameters.AddWithValue("Nickname", NickBox.Text);
                passupdate.Parameters.Add("@Password", SqlDbType.NVarChar).Value = PassBox.Text;   //Просто еще один способ добавления 
                await passupdate.ExecuteNonQueryAsync();
            }
            sqlConnection.Close();
            ChangeProfileButton.Enabled = true;
            SaveProfileButton.Enabled = false;
            DiscardChangesProfile.Enabled = false;
            FIOBox.Enabled = false;
            PhoneBox.Enabled = false;
            EmailBox.Enabled = false;
            PassBox.Enabled = false;
            OrderButton.Enabled = true;
            PassBox.Clear();
        }

        private async void DiscardChangesProfile_Click(object sender, EventArgs e)
        {
            if (sqlConnection.State.ToString() == "Closed")
            {
                await sqlConnection.OpenAsync();
            }
            reader = null;

            commandnick = new SqlCommand("Select * from [Users] where [Nickname] = @Nickname", sqlConnection);
            commandnick.Parameters.AddWithValue("Nickname", NickBox.Text);

            reader = commandnick.ExecuteReader();
            await reader.ReadAsync();
            FIOBox.Text = reader["FIO"].ToString();
            PhoneBox.Text = reader["Phone"].ToString();
            EmailBox.Text = reader["Email"].ToString();
            reader.Close();
            sqlConnection.Close();

            ChangeProfileButton.Enabled = true;
            SaveProfileButton.Enabled = false;
            DiscardChangesProfile.Enabled = false;
            FIOBox.Enabled = false;
            PhoneBox.Enabled = false;
            EmailBox.Enabled = false;
            PassBox.Enabled = false;
            OrderButton.Enabled = true;
            PassBox.Clear();
        }

        private void ExitProfileButton_Click(object sender, EventArgs e)
        {
            //Hide();
           // Login f0 = new Login();
         //   f0.ConBox.Text = this.linkdb;
            sqlConnection.Close();
            //  f0.ShowDialog();
            f = true;
            this.Close();
          //  this.Close();
           // this.Dispose();
        }

        private async void OrderButton_Click(object sender, EventArgs e)
        {
            ChangeProfileButton.Enabled = false;
            DateTime start = DateTime.Now;
            ReloadOrdersData();
            dataGridView3.DataSource = null;
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            if (sqlConnection.State.ToString() == "Closed")
            {
                await sqlConnection.OpenAsync();
            }

            DataRow row = OrdersSet.Tables["Orders"].NewRow();  //Интересное наблюдение, если несколько раз жать на кнопку , то функция всегда будет перезаписывать последнюю строку в OrderSet
            try
            {
                countOrders = OrdersSet.Tables["Orders"].Rows.Count - 1; // т.к. строки считаются с 0
               // if (countOrders<=0)
                OrdNum = (int)OrdersSet.Tables["Orders"].Rows[countOrders]["OrderID"];
                OrdNum++;
                row["OrderID"] = OrdNum; //
                row["Leader_nickname"] = "ZloyDux";
                row["Pioneer_nickname"]  = NickBox.Text;
                row["OrderDate"] = start;

                OrdersSet.Tables["Orders"].Rows.Add(row);   // В OrderSet все заказы

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "cranti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  newRowAdding = false;
            }

            //  ReloadOrdersData();
          //  dataGridView2.DataSource = OrdersSet.Tables["Orders"];
            dataGridView2.DataSource = dataSet1.Tables["Orders"]; // в dataSet1 именно заказы юзера

            countOrders = OrdersSet.Tables["Orders"].Rows.Count - 1; // т.к. строки считаются с 0, +1 строку добавляет datagridview

            OrdNum = (int)OrdersSet.Tables["Orders"].Rows[countOrders]["OrderID"];

            OrderButton.Enabled = false;
            BasketAddButton.Enabled = true;
            OrderSearchButton.Enabled = false;
            DeleteOrderButton.Enabled = false;

        }

        private async void BasketAddButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection.State.ToString() == "Closed")
            {
                await sqlConnection.OpenAsync();
            }

            // MyBasketLoadData();
            DataRow row = AllBasketSet.Tables["Basket"].NewRow();
            //  DataRow row1 = EmptyBasketSet.Tables["Basket"].NewRow();
            if (!string.IsNullOrWhiteSpace(QuantityBox.Text) && !string.IsNullOrWhiteSpace(ProductIDBox.Text))
            {
                //if (dataGridView3==null)
                //   {
                dataGridView3.ColumnCount = 3;
                dataGridView3.Columns[0].Name = "ProductName";
                dataGridView3.Columns[1].Name = "Quantity";
                dataGridView3.Columns[2].Name = "Sum";
                //    }


                //row["BasketID"] = BasketNum + 1;
                row["OrderID"] = OrdNum; //
                int ProductID = int.Parse(ProductIDBox.Text);
                row["ProductID"] = ProductID;
                row["Quantity"] = QuantityBox.Text;

                SqlCommand commandAdd1 = new SqlCommand("Select Price from [Products] where [ProductID] = @ProductID", sqlConnection);
                commandAdd1.Parameters.AddWithValue("@ProductID", SqlDbType.Int).Value = ProductIDBox.Text;
                if (commandAdd1.ExecuteScalar() != null)
                {
                    decimal price = (decimal)commandAdd1.ExecuteScalar();


                    row["Price"] = price;
                    decimal sum = price * int.Parse(QuantityBox.Text);
                    row["Sum"] = sum;

                    AllBasketSet.Tables["Basket"].Rows.Add(row);

                    string ProductName;
                    commandAdd1 = new SqlCommand("Select ProductName from [Products] where [ProductID] = @ProductID", sqlConnection);
                    commandAdd1.Parameters.AddWithValue("@ProductID", SqlDbType.Int).Value = ProductIDBox.Text;
                    ProductName = commandAdd1.ExecuteScalar().ToString();


                    string[] row1 = new string[] { ProductName, QuantityBox.Text, sum.ToString() };
                    dataGridView3.Rows.Add(row1);

                    OrderConfirmingButton.Enabled = true;
                }
                else
                    MessageBox.Show("Product not exist. Open your eyes");
            }
            else
                MessageBox.Show("ProductdID or Quantity empty");
            
        }


        private void ReloadOrdersData()
        {
            try
            {
                OrdersSet.Tables["Orders"].Clear();

                OrdersAdapter.Fill(OrdersSet, "Orders");
                //dataGridView2.DataSource = OrdersSet.Tables["Orders"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadBasketData()
        {
            try
            {
                AllBasketSet.Tables["Basket"].Clear();

                AllBasketAdapter.Fill(AllBasketSet, "Basket");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BasketLoad()
        {
            AllBasketAdapter = new SqlDataAdapter("Select * from [Basket]", sqlConnection);
            AllBasketSet = new DataSet();

            AllBasketBuilder = new SqlCommandBuilder(AllBasketAdapter);
            AllBasketBuilder.GetInsertCommand();

            AllBasketAdapter.Fill(AllBasketSet, "Basket");
            
            // dataGridView3.DataSource = dataSet2.Tables["Basket"];
        }


        private void EmptyBasketLoadData()
        {
            
                sqlDataAdapter5 = new SqlDataAdapter("Select * from [Basket] where [BasketID] = null", sqlConnection);
                // commandnick.Parameters.AddWithValue("Pioneer_nickname", NickBox.Text);
                //commandnick.ExecuteScalar();
                sqlDataAdapter5.SelectCommand.Parameters.AddWithValue("OrderID", OrdNum);
                //  AllBasketBuilder = new SqlCommandBuilder(MyOrdersAdapter);

                //AllBasketBuilder.GetInsertCommand();
                //AllBasketBuilder.GetUpdateCommand();
                //AllBasketBuilder.GetDeleteCommand();

                EmptyBasketSet = new DataSet();

                sqlDataAdapter5.Fill(EmptyBasketSet, "Basket");

                dataGridView3.DataSource = EmptyBasketSet.Tables["Basket"];

        }

        private void MyBasketLoadData()
        {
            try
            {
                sqlDataAdapter2 = new SqlDataAdapter("Select * from [Basket] where [OrderID] = @OrderID", sqlConnection);
                // commandnick.Parameters.AddWithValue("Pioneer_nickname", NickBox.Text);
                //commandnick.ExecuteScalar();
                sqlDataAdapter2.SelectCommand.Parameters.AddWithValue("OrderID", OrdNum);
                //  AllBasketBuilder = new SqlCommandBuilder(MyOrdersAdapter);

                dataSet2 = new DataSet();

                sqlDataAdapter2.Fill(dataSet2, "Basket");

                dataGridView3.DataSource = dataSet2.Tables["Basket"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OrderConfirmingButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm this Order?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    OrdersAdapter.Update(OrdersSet, "Orders");
                    MyOrdersLoadData();
                    //ReloadOrdersData();
                    AllBasketAdapter.Update(AllBasketSet, "Basket");
                    ReloadBasketData();
                    OrderButton.Enabled = true;
                    OrderConfirmingButton.Enabled = false;
                    BasketAddButton.Enabled = false;
                    OrderSearchButton.Enabled = true;
                    DeleteOrderButton.Enabled = true;
                    ChangeProfileButton.Enabled = true;
                    dataGridView3.Columns.Clear();
                    dataGridView3.Rows.Clear();
                    dataGridView3.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                OrdersLoadData();
                MyOrdersLoadData();
                AllBasketSet.Tables["Basket"].Clear();
                dataGridView3.Columns.Clear();
                dataGridView3.Rows.Clear();
                dataGridView3.Refresh();
                OrderButton.Enabled = true;
                OrderConfirmingButton.Enabled = false;
                BasketAddButton.Enabled = false;
                OrderSearchButton.Enabled = true;
                DeleteOrderButton.Enabled = true;
                ChangeProfileButton.Enabled = true;
            }
        }

        private void OrderSearchButton_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            try  { OrderSearch(); }  catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DeleteOrderButton_Click(object sender, EventArgs e)
        {
            DeleteOrder();
        }

        private void SearchProdButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter SearchProdAdapter = new SqlDataAdapter("Select * from Products where [ProductName] like '"+SearchProdBox.Text.ToString()+"%'" , sqlConnection);
                SqlCommandBuilder SearchProdBuilder = new SqlCommandBuilder(SearchProdAdapter);
              //  SearchProdAdapter.SelectCommand.Parameters.AddWithValue(ProductName, SearchProdBox.Text);

                DataSet SearchProdSet = new DataSet();

                SearchProdAdapter.Fill(SearchProdSet, "Products");

                dataGridView1.DataSource = SearchProdSet.Tables["Products"];
                //Не обязательно здесь было пользоваться SqlDataAdapter`ом. Я рассчитывал, что все-таки мне эти данные пригодтся.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllProdsButton_Click(object sender, EventArgs e)
        {
            ProductsLoadData();
        }

        private void UserUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!f) { sqlConnection.Close(); }
        }
    }
}
