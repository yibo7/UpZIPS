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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_del_all_ziprar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHzs = new System.Windows.Forms.TextBox();
            this.btnStartFind = new System.Windows.Forms.Button();
            this.btnDelSames = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.btnStartUpZip.Location = new System.Drawing.Point(36, 29);
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
            this.lbStateInfo.Location = new System.Drawing.Point(14, 193);
            this.lbStateInfo.Name = "lbStateInfo";
            this.lbStateInfo.Size = new System.Drawing.Size(29, 12);
            this.lbStateInfo.TabIndex = 3;
            this.lbStateInfo.Text = "状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "注:解压rar需要安装WinRar";
            // 
            // btn_del_all_ziprar
            // 
            this.btn_del_all_ziprar.Location = new System.Drawing.Point(200, 29);
            this.btn_del_all_ziprar.Name = "btn_del_all_ziprar";
            this.btn_del_all_ziprar.Size = new System.Drawing.Size(88, 37);
            this.btn_del_all_ziprar.TabIndex = 5;
            this.btn_del_all_ziprar.Text = "删除打包文件";
            this.btn_del_all_ziprar.UseVisualStyleBackColor = true;
            this.btn_del_all_ziprar.Click += new System.EventHandler(this.btn_del_all_ziprar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelSames);
            this.groupBox1.Controls.Add(this.btnStartUpZip);
            this.groupBox1.Controls.Add(this.btn_del_all_ziprar);
            this.groupBox1.Location = new System.Drawing.Point(16, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 88);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "常规操作";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtHzs);
            this.groupBox2.Controls.Add(this.btnStartFind);
            this.groupBox2.Location = new System.Drawing.Point(16, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 76);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "提取指定文件(文件存放在当前目录的output下)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "文件的后缀";
            // 
            // txtHzs
            // 
            this.txtHzs.Location = new System.Drawing.Point(77, 30);
            this.txtHzs.Name = "txtHzs";
            this.txtHzs.Size = new System.Drawing.Size(324, 21);
            this.txtHzs.TabIndex = 3;
            // 
            // btnStartFind
            // 
            this.btnStartFind.Location = new System.Drawing.Point(407, 25);
            this.btnStartFind.Name = "btnStartFind";
            this.btnStartFind.Size = new System.Drawing.Size(88, 28);
            this.btnStartFind.TabIndex = 2;
            this.btnStartFind.Text = "开始";
            this.btnStartFind.UseVisualStyleBackColor = true;
            this.btnStartFind.Click += new System.EventHandler(this.btnStartFind_Click);
            // 
            // btnDelSames
            // 
            this.btnDelSames.Location = new System.Drawing.Point(373, 29);
            this.btnDelSames.Name = "btnDelSames";
            this.btnDelSames.Size = new System.Drawing.Size(88, 37);
            this.btnDelSames.TabIndex = 6;
            this.btnDelSames.Text = "删除重复文件";
            this.btnDelSames.UseVisualStyleBackColor = true;
            this.btnDelSames.Click += new System.EventHandler(this.btnDelSames_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 323);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbStateInfo);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelPath);
            this.Name = "Main";
            this.Text = "批量解压zip与rar";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnStartUpZip;
        private System.Windows.Forms.Label lbStateInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_del_all_ziprar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStartFind;
        private System.Windows.Forms.TextBox txtHzs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelSames;
    }
}

