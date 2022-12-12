using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace _4labDataBase
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        string path = @"c:\Lab4DB";
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.AutoGenerateColumns = true;
            // УКАЖИТЕСВОЙАДРЕССЕРВЕРАИЗСРЕДЫSSMS!
            connection = new SqlConnection("Server=DESKTOP-U257O00;Database=Lab4DB;Trusted_Connection=True; MultipleActiveResultSets=True");
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            //dataGridView1.DataSource = table;
        }
        /*private void ShowTable(string text)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            command.CommandText = text;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }*/

        private void loadDB_Click(object sender, EventArgs e)
        {
            //ShowTable("SELECT * FROM Files");
            ShowFromDB formShow = new ShowFromDB();
            formShow.Show();
            //Form2 f2 = new Form2();
            //f2.ShowDialog();
            /* Application.Run(new Form2());
             Form.ShowDialog(new Form2());*/
        }

        private void loadCatalog_Click(object sender, EventArgs e)
        {
            ShowFromCatalog formShow = new ShowFromCatalog();
            formShow.Show();
            /*foreach (string filePath in System.IO.Directory.GetFiles(path))
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                System.DateTime date = System.IO.File.GetLastWriteTime(filePath);

                //System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand()
            }*/
        }

        private void sync_Click(object sender, EventArgs e)
        {
            int del = 0;
            int ins = 0;
            int upd = 0;
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Не получается подключиться к базе!");
                return;

            }
            /*SqlCommand transact = new SqlCommand("begin transaction", connection);
            transact.ExecuteReader();*/
            SqlCommand cmd0 = new SqlCommand("Select Name, DateFile from Files", connection);
            SqlDataReader reader = cmd0.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string chck = path + @"\" + reader["Name"];
                    if (System.IO.File.Exists(path + @"\" + reader["Name"]))
                    {
                        string filePath = path + @"\" + reader["Name"];
                        string fileName = System.IO.Path.GetFileName(filePath);
                        System.DateTime date_file = System.IO.File.GetLastWriteTime(filePath);
                        System.DateTime date_db = (System.DateTime)reader["DateFile"];
                        if (new SqlDateTime(date_file) != date_db)
                        {
                            SqlCommand cmd1 = new SqlCommand("update Files set DateFile = @date where Name = @name", connection);
                            cmd1.Parameters.AddWithValue("date", date_file);
                            cmd1.Parameters.AddWithValue("name", fileName);
                            upd = upd + 1;
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("delete from Files where Name = @f_name", connection);
                        cmd1.Parameters.AddWithValue("f_name", reader["Name"]);
                        del = del + 1;
                        cmd1.ExecuteNonQuery();
                        //cmd1.ExecuteReader();
                    }
                }
            }
            foreach (string filePath in System.IO.Directory.GetFiles(path))
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                System.DateTime date_file = System.IO.File.GetLastWriteTime(filePath);
                SqlCommand cmd1 = new SqlCommand("if not exists (select Name from Files where Name = @f_name) insert into Files(Name, DateFile) values (@f_name, @f_date)", connection);
                /*SqlCommand cmd1 = new SqlCommand("if not exists (select Name from Files where Name = @f_name) begin " +
                    "insert into Files(Name, DateFile) values (@f_name, @f_date)" +
                    "select * from Files where Name = @f_name " +
                    "end", connection);*/
                cmd1.Parameters.AddWithValue("f_name", fileName);
                cmd1.Parameters.AddWithValue("f_date", date_file);
                //cmd1.ExecuteNonQuery();
                //ins = ins + (int)cmd1.ExecuteScalar();
                int check = cmd1.ExecuteNonQuery();
                if (check > 0)
                    ins = ins + check;
            }
            /*transact.CommandText = "commit";
            transact.ExecuteNonQuery();*/
            connection.Close();
            MessageBox.Show("Изменено: " + upd.ToString() + "; удалено: " + del.ToString() + "; вставлено: " + ins.ToString());
        }
    }
}
