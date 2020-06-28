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
    public partial class AdmUI : Form
    {
        SqlConnection sqlConnection2 = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        public string linkdb;
        bool check = false;
        private bool newRowAdding = false;


        public AdmUI()
        {
            InitializeComponent();
        }

        private async void AdmUI_Load(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+linkdb+";Integrated Security=True";
            sqlConnection2 = new SqlConnection(connectionString);
            await sqlConnection2.OpenAsync();
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("Select *, 'Delete' AS [Edit] from Products", sqlConnection2);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                   
                sqlDataAdapter.Fill(dataSet, "Products");

                dataGridView1.DataSource = dataSet.Tables["Products"];
               

                for (int i=0; i<dataGridView1.Rows.Count; i++)
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


        private void ReloadData()
        {
            try
            {
                dataSet.Tables["Products"].Clear();  

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



        private void ExitAdmAcc_Click(object sender, EventArgs e)
        {
         //   Hide();
          //  Login f0 = new Login();
            //f0.ConBox.Text = this.linkdb;
            sqlConnection2.Close();
           // f0.ShowDialog();
            this.Dispose(true);
           // this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Delete this row?", "Deleteing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(rowIndex);

                            dataSet.Tables["Products"].Rows[rowIndex].Delete();

                            sqlDataAdapter.Update(dataSet,"Products");
                        }
                    }
                    else if (task == "Insert")
                    {
                       // if (dataGridView1.Rows.Count )
                            int rowIndex = dataGridView1.Rows.Count - 2;
                     
                            DataRow row = dataSet.Tables["Products"].NewRow();
                            try { 
                            row["ProductID"] = dataGridView1.Rows[rowIndex].Cells["ProductID"].Value; //
                            row["ProductName"] = dataGridView1.Rows[rowIndex].Cells["ProductName"].Value;
                            row["Price"] = dataGridView1.Rows[rowIndex].Cells["Price"].Value;
                            row["Leader_nickname"] = dataGridView1.Rows[rowIndex].Cells["Leader_nickname"].Value = NickAdmBox.Text;
                            row["Create_Date"] = dataGridView1.Rows[rowIndex].Cells["Create_date"].Value;

                            dataSet.Tables["Products"].Rows.Add(row);

                            dataSet.Tables["Products"].Rows.RemoveAt(dataSet.Tables["Products"].Rows.Count - 1);

                            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                            dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete";

                            sqlDataAdapter.Update(dataSet, "Products");
                              }
                            catch (Exception ex){
                            MessageBox.Show(ex.Message, "cranti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          //  newRowAdding = false;
                            }
                      
                            newRowAdding = false;

                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        try
                        {
                            dataSet.Tables["Products"].Rows[r]["ProductID"] = dataGridView1.Rows[r].Cells["ProductID"].Value; //
                            dataSet.Tables["Products"].Rows[r]["ProductName"] = dataGridView1.Rows[r].Cells["ProductName"].Value;
                            dataSet.Tables["Products"].Rows[r]["Price"] = dataGridView1.Rows[r].Cells["Price"].Value;
                            dataSet.Tables["Products"].Rows[r]["Leader_nickname"] = dataGridView1.Rows[r].Cells["Leader_nickname"].Value = NickAdmBox.Text;
                            dataSet.Tables["Products"].Rows[r]["Create_Date"] = dataGridView1.Rows[r].Cells["Create_date"].Value;

                            sqlDataAdapter.Update(dataSet, "Products");
                            dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete"; 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "cranti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }
                 
                    ReloadData();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, vse propalo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;
                    int lastRow = dataGridView1.Rows.Count - 2;
                 //   NickAdmBox.Text = lastRow.ToString();
                    DataGridViewRow row = dataGridView1.Rows[lastRow];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, lastRow] = linkCell;
                    row.Cells["Edit"].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sheff, propadaem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex; //получаем индекс строки выделенной ячейки
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, rowIndex] = linkCell;
                    editingRow.Cells["Edit"].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "cranti", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
