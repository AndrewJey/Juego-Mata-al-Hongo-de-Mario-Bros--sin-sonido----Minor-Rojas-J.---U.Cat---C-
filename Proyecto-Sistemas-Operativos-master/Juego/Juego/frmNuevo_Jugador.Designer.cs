namespace Juego
{
    partial class frmNuevo_Jugador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevo_Jugador));
            this.btnnuevojugador = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtnombrenu = new System.Windows.Forms.TextBox();
            this.txtcontraseñanu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnnuevojugador
            // 
            this.btnnuevojugador.Location = new System.Drawing.Point(115, 108);
            this.btnnuevojugador.Name = "btnnuevojugador";
            this.btnnuevojugador.Size = new System.Drawing.Size(75, 23);
            this.btnnuevojugador.TabIndex = 0;
            this.btnnuevojugador.Text = "Crear";
            this.btnnuevojugador.UseVisualStyleBackColor = true;
            this.btnnuevojugador.Click += new System.EventHandler(this.btnnuevojugador_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña:";
            // 
            // txtnombrenu
            // 
            this.txtnombrenu.Location = new System.Drawing.Point(104, 37);
            this.txtnombrenu.Name = "txtnombrenu";
            this.txtnombrenu.Size = new System.Drawing.Size(100, 20);
            this.txtnombrenu.TabIndex = 3;
            // 
            // txtcontraseñanu
            // 
            this.txtcontraseñanu.Location = new System.Drawing.Point(104, 68);
            this.txtcontraseñanu.Name = "txtcontraseñanu";
            this.txtcontraseñanu.Size = new System.Drawing.Size(100, 20);
            this.txtcontraseñanu.TabIndex = 4;
            // 
            // frmNuevo_Jugador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(245, 147);
            this.Controls.Add(this.txtcontraseñanu);
            this.Controls.Add(this.txtnombrenu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnnuevojugador);
            this.Name = "frmNuevo_Jugador";
            this.Text = "Nuevo Jugador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnnuevojugador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnombrenu;
        private System.Windows.Forms.TextBox txtcontraseñanu;
    }
}