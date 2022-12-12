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
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd0 = new SqlCommand("Select Name, DateFile from Files", connection);
            cmd0.Transaction = transaction;
            SqlCommand cmd1 = new SqlCommand("Select Name, DateFile from Files", connection);
            cmd1.Transaction = transaction;
            SqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                try
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
                                cmd0.CommandText = "update Files set DateFile = @date where Name = @name";
                                cmd0.Parameters.Clear();
                                cmd0.Parameters.AddWithValue("date", date_file);
                                cmd0.Parameters.AddWithValue("name", fileName);
                                upd = upd + 1;
                                cmd0.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            cmd0.CommandText = "delete from Files where Name = @f_name";
                            cmd0.Parameters.Clear();
                            cmd0.Parameters.AddWithValue("f_name", reader["Name"]);
                            del = del + 1;
                            cmd0.ExecuteNonQuery();
                            //cmd1.ExecuteReader();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Something wrong!");
                    transaction.Rollback();
                    return;
                }
            }
            reader.Close();
            transaction.Commit();
            transaction = connection.BeginTransaction();
            cmd0.Transaction = transaction;
            try
            {
                foreach (string filePath in System.IO.Directory.GetFiles(path))
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    System.DateTime date_file = System.IO.File.GetLastWriteTime(filePath);
                    cmd0.CommandText = "if not exists (select Name from Files where Name = @f_name) insert into Files(Name, DateFile) values (@f_name, @f_date)";
                    //SqlCommand cmd1 = new SqlCommand("if not exists (select Name from Files where Name = @f_name) insert into Files(Name, DateFile) values (@f_name, @f_date)", connection);
                    cmd0.Parameters.Clear();
                    cmd0.Parameters.AddWithValue("f_name", fileName);
                    cmd0.Parameters.AddWithValue("f_date", date_file);
                    int check = cmd0.ExecuteNonQuery();
                    if (check > 0)
                        ins = ins + check;
                }
                transaction.Commit();
            }
            catch
            {
                MessageBox.Show("Something wrong2!");
                transaction.Rollback();
                return;
            }
            connection.Close();
            MessageBox.Show("Изменено: " + upd.ToString() + "; удалено: " + del.ToString() + "; вставлено: " + ins.ToString());
        }
    }
}
