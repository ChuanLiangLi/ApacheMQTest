namespace ApacheMQConsumer
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
            this.lblReceiveMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblReceiveMessage
            // 
            this.lblReceiveMessage.AutoSize = true;
            this.lblReceiveMessage.Location = new System.Drawing.Point(137, 59);
            this.lblReceiveMessage.Name = "lblReceiveMessage";
            this.lblReceiveMessage.Size = new System.Drawing.Size(55, 15);
            this.lblReceiveMessage.TabIndex = 0;
            this.lblReceiveMessage.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 318);
            this.Controls.Add(this.lblReceiveMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReceiveMessage;
    }
}

