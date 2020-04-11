namespace db_datagridview
{
    partial class client_coach
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
            this.dgv_clients = new System.Windows.Forms.DataGridView();
            this.dgv_coaches = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_coaches)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_clients
            // 
            this.dgv_clients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_clients.Location = new System.Drawing.Point(3, 3);
            this.dgv_clients.Name = "dgv_clients";
            this.dgv_clients.RowHeadersWidth = 51;
            this.dgv_clients.RowTemplate.Height = 24;
            this.dgv_clients.Size = new System.Drawing.Size(816, 308);
            this.dgv_clients.TabIndex = 0;
            this.dgv_clients.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_clients_RowValidating);
            this.dgv_clients.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_clients_UserDeletingRow);
            // 
            // dgv_coaches
            // 
            this.dgv_coaches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_coaches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_coaches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_coaches.Location = new System.Drawing.Point(3, 317);
            this.dgv_coaches.Name = "dgv_coaches";
            this.dgv_coaches.RowHeadersWidth = 51;
            this.dgv_coaches.RowTemplate.Height = 24;
            this.dgv_coaches.Size = new System.Drawing.Size(816, 309);
            this.dgv_coaches.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_coaches, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgv_clients, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 629);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // client_coach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 629);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "client_coach";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_coaches)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_clients;
        private System.Windows.Forms.DataGridView dgv_coaches;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

