namespace UIMusicApp
{
    partial class Login
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
            components = new System.ComponentModel.Container();
            txtUsuario = new TextBox();
            txtContraseña = new TextBox();
            button1 = new Button();
            btnCerrar = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            notifyIcon1 = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(13, 16, 50);
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.ForeColor = Color.FromArgb(140, 82, 255);
            txtUsuario.Location = new Point(166, 242);
            txtUsuario.Margin = new Padding(3, 2, 3, 2);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(212, 18);
            txtUsuario.TabIndex = 1;
            txtUsuario.TextAlign = HorizontalAlignment.Center;
            // 
            // txtContraseña
            // 
            txtContraseña.BackColor = Color.FromArgb(13, 16, 50);
            txtContraseña.BorderStyle = BorderStyle.None;
            txtContraseña.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContraseña.ForeColor = Color.FromArgb(140, 82, 255);
            txtContraseña.Location = new Point(169, 302);
            txtContraseña.Margin = new Padding(3, 2, 3, 2);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(205, 18);
            txtContraseña.TabIndex = 2;
            txtContraseña.TextAlign = HorizontalAlignment.Center;
            txtContraseña.TextChanged += txtContraseña_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(94, 23, 235);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Bahnschrift", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(201, 368);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(145, 31);
            button1.TabIndex = 3;
            button1.Text = "LOGIN";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Transparent;
            btnCerrar.BackgroundImage = Properties.Resources.borrar;
            btnCerrar.BackgroundImageLayout = ImageLayout.Zoom;
            btnCerrar.FlatAppearance.BorderColor = Color.FromArgb(174, 103, 222);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.CheckedBackColor = Color.FromArgb(136, 97, 211);
            btnCerrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(174, 103, 222);
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(174, 103, 222);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Location = new Point(1014, -1);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(42, 47);
            btnCerrar.TabIndex = 27;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(161, 263);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(220, 1);
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Location = new Point(161, 323);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(220, 1);
            pictureBox2.TabIndex = 29;
            pictureBox2.TabStop = false;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(23, 27, 80);
            BackgroundImage = Properties.Resources.LoginColor;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1055, 616);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnCerrar);
            Controls.Add(button1);
            Controls.Add(txtContraseña);
            Controls.Add(txtUsuario);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Button button1;
        private Button btnCerrar;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private NotifyIcon notifyIcon1;
    }
}