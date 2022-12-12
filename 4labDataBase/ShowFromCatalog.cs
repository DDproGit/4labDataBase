using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4labDataBase
{

    public partial class ShowFromCatalog : Form
    {
        /*public class OneFile
        {
            public string fileName { get; set; }
            public System.DateTime date { get; set; }
        }*/
        string path = @"c:\Lab4DB";
        public ShowFromCatalog()
        {
            InitializeComponent();
        }

        private void ShowFromCatalog_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            List<OneFile> lstFiles = new List<OneFile>();
            try
            {
                foreach (string filePath in System.IO.Directory.GetFiles(path))
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    System.DateTime date = System.IO.File.GetLastWriteTime(filePath);
                    OneFile file = new OneFile();
                    file.fileName = fileName;
                    file.date = date;
                    lstFiles.Add(file);

                    //System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand()
                }
            }
            catch
            {
                MessageBox.Show("Не получается найти каталог!");
            }
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lstFiles;
        }
    }
    public class OneFile
    {
        public string fileName { get; set; }
        public System.DateTime date { get; set; }
    }
}
