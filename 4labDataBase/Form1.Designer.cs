
namespace _4labDataBase
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadDB = new System.Windows.Forms.Button();
            this.loadCatalog = new System.Windows.Forms.Button();
            this.sync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadDB
            // 
            this.loadDB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadDB.Location = new System.Drawing.Point(0, 427);
            this.loadDB.Name = "loadDB";
            this.loadDB.Size = new System.Drawing.Size(800, 23);
            this.loadDB.TabIndex = 1;
            this.loadDB.Text = "Вывод из базы";
            this.loadDB.UseVisualStyleBackColor = true;
            this.loadDB.Click += new System.EventHandler(this.loadDB_Click);
            // 
            // loadCatalog
            // 
            this.loadCatalog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadCatalog.Location = new System.Drawing.Point(0, 404);
            this.loadCatalog.Name = "loadCatalog";
            this.loadCatalog.Size = new System.Drawing.Size(800, 23);
            this.loadCatalog.TabIndex = 2;
            this.loadCatalog.Text = "Вывод из каталога";
            this.loadCatalog.UseVisualStyleBackColor = true;
            this.loadCatalog.Click += new System.EventHandler(this.loadCatalog_Click);
            // 
            // sync
            // 
            this.sync.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sync.Location = new System.Drawing.Point(0, 381);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(800, 23);
            this.sync.TabIndex = 3;
            this.sync.Text = "Синхронизировать";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.sync_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.loadCatalog);
            this.Controls.Add(this.loadDB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loadDB;
        private System.Windows.Forms.Button loadCatalog;
        private System.Windows.Forms.Button sync;
    }
}

