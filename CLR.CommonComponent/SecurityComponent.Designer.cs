using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CLR.CommonComponent
{
    partial class SecurityComponent
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            this.txtSource = new TextBox();
            this.txtDestination = new TextBox();
            this.label1 = new Label();
            this.cboSecurityMode = new ComboBox();
            this.btnDecrypt = new Button();
            this.btnEncrypt = new Button();
            this.panel1 = new Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.txtSource.BackColor = SystemColors.Window;
            this.txtSource.Location = new Point(0, 0);
            this.txtSource.Margin = new Padding(0);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new Size(350, 25);
            this.txtSource.TabIndex = 0;
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            this.txtDestination.BackColor = SystemColors.Window;
            this.txtDestination.Location = new Point(0, 55);
            this.txtDestination.Margin = new Padding(0);
            this.txtDestination.Multiline = true;
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new Size(350, 25);
            this.txtDestination.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 10);
            this.label1.Margin = new Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "类型：";
            // 
            // cboSecurityMode
            // 
            this.cboSecurityMode.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
            this.cboSecurityMode.FormattingEnabled = true;
            this.cboSecurityMode.Items.AddRange(new object[] {
            "Base64",
            "DES",
            "MD5",
            "RSACryption",
            "TripleDES"});
            this.cboSecurityMode.Location = new Point(55, 6);
            this.cboSecurityMode.Name = "cboSecurityMode";
            this.cboSecurityMode.Size = new Size(121, 20);
            this.cboSecurityMode.TabIndex = 11;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
            this.btnDecrypt.Location = new Point(187, 4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new Size(75, 24);
            this.btnDecrypt.TabIndex = 9;
            this.btnDecrypt.Text = "解  密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new EventHandler(btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
            this.btnEncrypt.Location = new Point(273, 4);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new Size(75, 24);
            this.btnEncrypt.TabIndex = 10;
            this.btnEncrypt.Text = "加  密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new EventHandler(btnEncrypt_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            this.panel1.BackColor = SystemColors.Control;
            this.panel1.Controls.Add(this.btnDecrypt);
            this.panel1.Controls.Add(this.cboSecurityMode);
            this.panel1.Controls.Add(this.btnEncrypt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(0, 25);
            this.panel1.Margin = new Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(350, 30);
            this.panel1.TabIndex = 13;
            // 
            // SecurityComponent
            // 
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.panel1);
            this.Margin = new Padding(0);
            this.Name = "SecurityComponent";
            this.Size = new Size(350, 80);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.SizeChanged += new EventHandler(SecurityComponent_SizeChanged);
            this.PerformLayout();
        }

        private TextBox txtSource;
        private TextBox txtDestination;
        private Label label1;
        private ComboBox cboSecurityMode;
        private Button btnDecrypt;
        private Button btnEncrypt;
        private Panel panel1;

        #endregion
    }
}
