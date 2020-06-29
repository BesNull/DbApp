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
    public partial class Login : Form
    {
        SqlConnection sqlConnection1;
        SqlCommand commandnick;
        SqlCommand commandpass;
        // string linkdb;
        // string linkdb = @"C:\db\Database1.mdf";
        public string linkdb;// = @"C:\Users\GoodPC\Desktop\Unich Mazatrucka v6441\4 kurs 2 sem\БД курсач\ShaitanProjectUltraBD\ShaitanProjectUltraBD\Database1.mdf";
        public Login()
        {
            
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //NickAdmBox.Text = "ZloyDux1"; 
            ConBox.Text = @"C:\Users\GoodPC\Desktop\Unich Mazatrucka v6441\4 kurs 2 sem\БД курсач\ShaitanProjectUltraBD\ShaitanProjectUltraBD\Database1.mdf";
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ ConBox.Text.ToString() +";Integrated Security=True";
            sqlConnection1 = new SqlConnection(connectionString);
            await sqlConnection1.OpenAsync();

            NickAdmBox.Enabled = false;
            NickBox.Enabled = false;
            NickRegBox.Enabled = false;
            FIOBox.Enabled = false;
            PhoneBox.Enabled = false;
            EmailBox.Enabled = false;
            PassAdmBox.Enabled = false;
            PassBox.Enabled = false;
            PassRegBox.Enabled = false;
            LoginAdmButton.Enabled = false;
            LoginButton.Enabled = false;
            RegisterButton.Enabled = false;
            MessageBox.Show("Enter the matrixdb");

            // SqlCommand command = new SqlCommand("Select Password from [Users]", sqlConnection);
        }

        private async void ChangeDB_Click(object sender, EventArgs e)
        {
            linkdb = ConBox.Text; //.ToString();
                                  //ConBox.Text = linkdb;
            if (sqlConnection1.State.ToString() == "Open")
            {
                sqlConnection1.Close();
            }
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + @linkdb + ";Integrated Security=True";
            sqlConnection1 = new SqlConnection(connectionString);

            try { await sqlConnection1.OpenAsync(); } catch { MessageBox.Show("No db this path", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (sqlConnection1.State.ToString() == "Open")
            {
                NickAdmBox.Enabled = true;
                NickBox.Enabled = true;
                NickRegBox.Enabled = true;
                FIOBox.Enabled = true;
                PhoneBox.Enabled = true;
                EmailBox.Enabled = true;
                PassAdmBox.Enabled = true;
                PassBox.Enabled = true;
                PassRegBox.Enabled = true;
                // ConBox.Enabled = false;
                LoginAdmButton.Enabled = true;
                LoginButton.Enabled = true;
                RegisterButton.Enabled = true;
                MessageBox.Show("Connection OK", "Connection check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                NickAdmBox.Enabled = false;
                NickBox.Enabled = false;
                NickRegBox.Enabled = false;
                FIOBox.Enabled = false;
                PhoneBox.Enabled = false;
                EmailBox.Enabled = false;
                PassAdmBox.Enabled = false;
                PassBox.Enabled = false;
                PassRegBox.Enabled = false;
                LoginAdmButton.Enabled = false;
                LoginButton.Enabled = false;
                RegisterButton.Enabled = false;
                MessageBox.Show("Connection Error", "Connection check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                


        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            
            if (sqlConnection1.State.ToString()=="Closed")
            {
                await sqlConnection1.OpenAsync();
            }
            string nick = NickBox.Text;
            string pass = PassBox.Text;
            SqlDataReader sqlReader = null;
            /*
            SqlCommand updatecom = new SqlCommand("UPDATE [Users] SET [Password]=@Password WHERE [Nickname]=@Nickname", sqlConnection1); 
            updatecom.Parameters.AddWithValue("Nickname", NickBox.Text);
            updatecom.Parameters.AddWithValue("Password", PassBox.Text);
            await updatecom.ExecuteNonQueryAsync();
            */

            
            commandnick = new SqlCommand("Select Nickname from [Users] where [Nickname] = @Nickname", sqlConnection1);
            commandnick.Parameters.AddWithValue("Nickname", NickBox.Text);  //Позволяет нам сделать Select со значением, взятым из ТекстБокса, аналогичнно Insert

            commandpass = new SqlCommand("Select Password from [Users] where [Nickname] = @Nickname", sqlConnection1);
            commandpass.Parameters.AddWithValue("Nickname", NickBox.Text);
          //  commandpass.Parameters.AddWithValue("Password", PassBox.Text);
            //  sqlReader = (string)commandpass.ExecuteScalar();
            //string passbd;
            // sqlReader.Read();
            string nickbd = (string)commandnick.ExecuteScalar();
            string passbd = (string)commandpass.ExecuteScalar();
            
            if (nickbd == nick)  
            {
                if (passbd == pass)
                {
                Visible = false;
                UserUI f1 = new UserUI();
                f1.NickBox.Text = this.NickBox.Text;
                f1.linkdb = this.ConBox.Text;
               // sqlConnection1.Close();
                f1.ShowDialog();
                    //this.Dispose(true);
                f1.Dispose();
                Visible = true;
                //this.Close();
                }
                else
                    MessageBox.Show("Неверный пароль");
            }
            else
                MessageBox.Show("Неверный Ник");
            //sqlReader.Close();
            sqlConnection1.Close();


        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection1.State.ToString() == "Closed")
            {
                await sqlConnection1.OpenAsync();
            }
            string check = string.Empty;
            if (string.IsNullOrWhiteSpace(NickRegBox.Text) && string.IsNullOrWhiteSpace(PassRegBox.Text))// && FIOBox.TextLength!=0 && PhoneBox.TextLength != 0 && EmailBox.TextLength != 0 && PassBox.TextLength != 0)
            {
                MessageBox.Show("Nick or Password not entered");
               
            }
            else
            {
                SqlCommand commandAddUser = new SqlCommand("INSERT INTO [Users] (Nickname, FIO, Phone, Email, Password) VALUES (@Nickname, @FIO, @Phone, @Email, @Password)", sqlConnection1);
                commandAddUser.Parameters.AddWithValue("@Nickname", NickRegBox.Text);
                commandAddUser.Parameters.AddWithValue("@FIO", FIOBox.Text);
                commandAddUser.Parameters.AddWithValue("@Phone", PhoneBox.Text);
                commandAddUser.Parameters.AddWithValue("@Email", EmailBox.Text);
                commandAddUser.Parameters.AddWithValue("@Password", PassRegBox.Text);
                await commandAddUser.ExecuteNonQueryAsync();
                MessageBox.Show("Учетная запись успешно создана, входите скорей");
            }
        }

        private async void LoginAdmButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection1.State.ToString() == "Closed")
            {
                await sqlConnection1.OpenAsync();
            }
            string nick = NickAdmBox.Text;
            string pass = PassAdmBox.Text;
            SqlDataReader sqlReader = null;
            /*
            SqlCommand updatecom = new SqlCommand("UPDATE [Users] SET [Password]=@Password WHERE [Nickname]=@Nickname", sqlConnection1); 
            updatecom.Parameters.AddWithValue("Nickname", NickBox.Text);
            updatecom.Parameters.AddWithValue("Password", PassBox.Text);
            await updatecom.ExecuteNonQueryAsync();
            */


            commandnick = new SqlCommand("Select Nickname from [Leaders] where [Nickname] = @Nickname", sqlConnection1);
            commandnick.Parameters.AddWithValue("Nickname", NickAdmBox.Text);  //Позволяет нам сделать Select со значением, взятым из ТекстБокса, аналогичнно Insert

            commandpass = new SqlCommand("Select Password from [Leaders] where [Nickname] = @Nickname", sqlConnection1);
            commandpass.Parameters.AddWithValue("Nickname", NickAdmBox.Text);
            //  commandpass.Parameters.AddWithValue("Password", PassBox.Text);
            //  sqlReader = (string)commandpass.ExecuteScalar();
            //string passbd;
            // sqlReader.Read();
            string nickbd = (string)commandnick.ExecuteScalar();
            string passbd = (string)commandpass.ExecuteScalar();

            if (nickbd == nick)
            {
                if (passbd == pass)
                {
                    Visible = false;
                    AdmUI f2 = new AdmUI();
                    f2.NickAdmBox.Text = this.NickAdmBox.Text;
                    f2.linkdb = this.ConBox.Text;
                    f2.ShowDialog();
                    f2.Dispose();
                    Visible = true;
                    //this.Close();
                }
                else
                    MessageBox.Show("Неверный пароль");
            }
            else
                MessageBox.Show("Неверный Ник");
            sqlConnection1.Close();
        }

       
    }
}
