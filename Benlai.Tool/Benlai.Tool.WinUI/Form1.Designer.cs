namespace Benlai.Tool.WinUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSeleFile = new System.Windows.Forms.Button();
            this.btnLoadNuget = new System.Windows.Forms.Button();
            this.dgvProjData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.ofdFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dgvPackData = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPackname = new System.Windows.Forms.Label();
            this.lblPackTotal = new System.Windows.Forms.Label();
            this.lblProjTotal = new System.Windows.Forms.Label();
            this.cbVersionList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackData)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(52, 29);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(631, 25);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnSeleFile
            // 
            this.btnSeleFile.Location = new System.Drawing.Point(697, 25);
            this.btnSeleFile.Name = "btnSeleFile";
            this.btnSeleFile.Size = new System.Drawing.Size(115, 33);
            this.btnSeleFile.TabIndex = 1;
            this.btnSeleFile.Text = "选择sln文件";
            this.btnSeleFile.UseVisualStyleBackColor = true;
            this.btnSeleFile.Click += new System.EventHandler(this.btnSeleFile_Click);
            // 
            // btnLoadNuget
            // 
            this.btnLoadNuget.Location = new System.Drawing.Point(820, 25);
            this.btnLoadNuget.Name = "btnLoadNuget";
            this.btnLoadNuget.Size = new System.Drawing.Size(171, 33);
            this.btnLoadNuget.TabIndex = 2;
            this.btnLoadNuget.Text = "加载nuget信息";
            this.btnLoadNuget.UseVisualStyleBackColor = true;
            this.btnLoadNuget.Click += new System.EventHandler(this.btnLoadNuget_Click);
            // 
            // dgvProjData
            // 
            this.dgvProjData.AllowUserToAddRows = false;
            this.dgvProjData.AllowUserToDeleteRows = false;
            this.dgvProjData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProjData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjData.Location = new System.Drawing.Point(55, 358);
            this.dgvProjData.Name = "dgvProjData";
            this.dgvProjData.ReadOnly = true;
            this.dgvProjData.RowTemplate.Height = 27;
            this.dgvProjData.Size = new System.Drawing.Size(973, 227);
            this.dgvProjData.TabIndex = 3;
            this.dgvProjData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProjData_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 594);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "系统信息：";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(140, 594);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(31, 15);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "***";
            // 
            // ofdFileDialog
            // 
            this.ofdFileDialog.FileName = "openFileDialog1";
            this.ofdFileDialog.Filter = "|*.sln";
            // 
            // dgvPackData
            // 
            this.dgvPackData.AllowUserToAddRows = false;
            this.dgvPackData.AllowUserToDeleteRows = false;
            this.dgvPackData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPackData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackData.ContextMenuStrip = this.cmsMenu;
            this.dgvPackData.Location = new System.Drawing.Point(52, 93);
            this.dgvPackData.Name = "dgvPackData";
            this.dgvPackData.ReadOnly = true;
            this.dgvPackData.RowTemplate.Height = 27;
            this.dgvPackData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPackData.Size = new System.Drawing.Size(973, 230);
            this.dgvPackData.TabIndex = 6;
            this.dgvPackData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPackData_DataBindingComplete);
            this.dgvPackData.Click += new System.EventHandler(this.dgvPackData_DoubleClick);
            // 
            // cmsMenu
            // 
            this.cmsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(139, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem1.Text = "版本对齐";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "资源包信息(双击数据行查看被引用项目信息)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "被引项目信息";
            // 
            // lblPackname
            // 
            this.lblPackname.AutoSize = true;
            this.lblPackname.Location = new System.Drawing.Point(157, 334);
            this.lblPackname.Name = "lblPackname";
            this.lblPackname.Size = new System.Drawing.Size(15, 15);
            this.lblPackname.TabIndex = 9;
            this.lblPackname.Text = "*";
            // 
            // lblPackTotal
            // 
            this.lblPackTotal.AutoSize = true;
            this.lblPackTotal.Location = new System.Drawing.Point(575, 71);
            this.lblPackTotal.Name = "lblPackTotal";
            this.lblPackTotal.Size = new System.Drawing.Size(15, 15);
            this.lblPackTotal.TabIndex = 10;
            this.lblPackTotal.Text = "*";
            // 
            // lblProjTotal
            // 
            this.lblProjTotal.AutoSize = true;
            this.lblProjTotal.Location = new System.Drawing.Point(575, 334);
            this.lblProjTotal.Name = "lblProjTotal";
            this.lblProjTotal.Size = new System.Drawing.Size(15, 15);
            this.lblProjTotal.TabIndex = 11;
            this.lblProjTotal.Text = "*";
            // 
            // cbVersionList
            // 
            this.cbVersionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersionList.FormattingEnabled = true;
            this.cbVersionList.Location = new System.Drawing.Point(836, 329);
            this.cbVersionList.Name = "cbVersionList";
            this.cbVersionList.Size = new System.Drawing.Size(189, 23);
            this.cbVersionList.TabIndex = 13;
            this.cbVersionList.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(975, 597);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "[帮助]";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 628);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbVersionList);
            this.Controls.Add(this.lblProjTotal);
            this.Controls.Add(this.lblPackTotal);
            this.Controls.Add(this.lblPackname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPackData);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvProjData);
            this.Controls.Add(this.btnLoadNuget);
            this.Controls.Add(this.btnSeleFile);
            this.Controls.Add(this.txtFilePath);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1079, 675);
            this.MinimumSize = new System.Drawing.Size(1079, 675);
            this.Name = "Form1";
            this.Text = "Nuget包版本对齐工具(适用以packages.config方式管理包的工程)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackData)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSeleFile;
        private System.Windows.Forms.Button btnLoadNuget;
        private System.Windows.Forms.DataGridView dgvProjData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.OpenFileDialog ofdFileDialog;
        private System.Windows.Forms.DataGridView dgvPackData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPackname;
        private System.Windows.Forms.Label lblPackTotal;
        private System.Windows.Forms.Label lblProjTotal;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ComboBox cbVersionList;
        private System.Windows.Forms.Label label5;
    }
}

