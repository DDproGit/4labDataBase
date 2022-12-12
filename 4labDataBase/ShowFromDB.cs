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
    public partial class ShowFromDB : Form
    {
        public ShowFromDB()
        {
            InitializeComponent();
        }

        private void ShowFromDB_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "lab4DBDataSet.Files". При необходимости она может быть перемещена или удалена.
            try
            {
                this.filesTableAdapter.Fill(this.lab4DBDataSet.Files);
            }
            catch 
            {
                MessageBox.Show("Не получается подключиться к базе!");

            }

        }
    }
}
