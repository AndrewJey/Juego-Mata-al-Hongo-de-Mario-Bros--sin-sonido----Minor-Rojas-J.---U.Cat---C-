namespace Juego
{
    partial class frmGestor_memoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.barRamUtilizada = new System.Windows.Forms.ProgressBar();
            this.barRamSistema = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblramutilizada = new System.Windows.Forms.Label();
            this.lblramsistema = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // barRamUtilizada
            // 
            this.barRamUtilizada.Location = new System.Drawing.Point(33, 27);
            this.barRamUtilizada.Name = "barRamUtilizada";
            this.barRamUtilizada.Size = new System.Drawing.Size(376, 23);
            this.barRamUtilizada.TabIndex = 0;
            // 
            // barRamSistema
            // 
            this.barRamSistema.Location = new System.Drawing.Point(33, 85);
            this.barRamSistema.Name = "barRamSistema";
            this.barRamSistema.Size = new System.Drawing.Size(376, 23);
            this.barRamSistema.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(37, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Memoria Ram Utilizada ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(37, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Memoria Ram Libre del Sitema";
            // 
            // lblramutilizada
            // 
            this.lblramutilizada.AutoSize = true;
            this.lblramutilizada.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblramutilizada.Location = new System.Drawing.Point(415, 37);
            this.lblramutilizada.Name = "lblramutilizada";
            this.lblramutilizada.Size = new System.Drawing.Size(13, 13);
            this.lblramutilizada.TabIndex = 4;
            this.lblramutilizada.Text = "..";
            // 
            // lblramsistema
            // 
            this.lblramsistema.AutoSize = true;
            this.lblramsistema.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblramsistema.Location = new System.Drawing.Point(415, 95);
            this.lblramsistema.Name = "lblramsistema";
            this.lblramsistema.Size = new System.Drawing.Size(13, 13);
            this.lblramsistema.TabIndex = 5;
            this.lblramsistema.Text = "..";
            // 
            // frmGestor_memoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(518, 147);
            this.Controls.Add(this.lblramsistema);
            this.Controls.Add(this.lblramutilizada);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barRamSistema);
            this.Controls.Add(this.barRamUtilizada);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGestor_memoria";
            this.Text = "Gestor de Memoria";
            this.Load += new System.EventHandler(this.frmGestor_memoria_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar barRamUtilizada;
        private System.Windows.Forms.ProgressBar barRamSistema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblramutilizada;
        private System.Windows.Forms.Label lblramsistema;
    }
}