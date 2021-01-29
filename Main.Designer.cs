namespace U
{
    partial class Main
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
            this.btnSelPath = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnStartUpZip = new System.Windows.Forms.Button();
            this.lbStateInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rabZip = new System.Windows.Forms.RadioButton();
            this.rabRar = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelPath
            // 
            this.btnSelPath.Location = new System.Drawing.Point(389, 18);
            this.btnSelPath.Name = "btnSelPath";
            this.btnSelPath.Size = new System.Drawing.Size(88, 23);
            this.btnSelPath.TabIndex = 0;
            this.btnSelPath.Text = "选择目录";
            this.btnSelPath.UseVisualStyleBackColor = true;
            this.btnSelPath.Click += new System.EventHandler(this.btnSelPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 18);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(362, 21);
            this.txtPath.TabIndex = 1;
            // 
            // btnStartUpZip
            // 
            this.btnStartUpZip.Location = new System.Drawing.Point(187, 62);
            this.btnStartUpZip.Name = "btnStartUpZip";
            this.btnStartUpZip.Size = new System.Drawing.Size(88, 37);
            this.btnStartUpZip.TabIndex = 2;
            this.btnStartUpZip.Text = "开始解压";
            this.btnStartUpZip.UseVisualStyleBackColor = true;
            this.btnStartUpZip.Click += new System.EventHandler(this.btnStartUpZip_Click);
            // 
            // lbStateInfo
            // 
            this.lbStateInfo.AutoSize = true;
            this.lbStateInfo.Location = new System.Drawing.Point(12, 120);
            this.lbStateInfo.Name = "lbStateInfo";
            this.lbStateInfo.Size = new System.Drawing.Size(29, 12);
            this.lbStateInfo.TabIndex = 3;
            this.lbStateInfo.Text = "状态";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rabRar);
            this.groupBox1.Controls.Add(this.rabZip);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 46);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择格式";
            // 
            // rabZip
            // 
            this.rabZip.AutoSize = true;
            this.rabZip.Checked = true;
            this.rabZip.Location = new System.Drawing.Point(19, 21);
            this.rabZip.Name = "rabZip";
            this.rabZip.Size = new System.Drawing.Size(41, 16);
            this.rabZip.TabIndex = 0;
            this.rabZip.TabStop = true;
            this.rabZip.Text = "zip";
            this.rabZip.UseVisualStyleBackColor = true;
            this.rabZip.CheckedChanged += new System.EventHandler(this.rabZip_CheckedChanged);
            // 
            // rabRar
            // 
            this.rabRar.AutoSize = true;
            this.rabRar.Location = new System.Drawing.Point(80, 21);
            this.rabRar.Name = "rabRar";
            this.rabRar.Size = new System.Drawing.Size(41, 16);
            this.rabRar.TabIndex = 1;
            this.rabRar.Text = "rar";
            this.rabRar.UseVisualStyleBackColor = true;
            this.rabRar.CheckedChanged += new System.EventHandler(this.rabRar_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 157);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbStateInfo);
            this.Controls.Add(this.btnStartUpZip);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelPath);
            this.Name = "Main";
            this.Text = "批量解压zip";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnStartUpZip;
        private System.Windows.Forms.Label lbStateInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rabZip;
        private System.Windows.Forms.RadioButton rabRar;
    }
}

