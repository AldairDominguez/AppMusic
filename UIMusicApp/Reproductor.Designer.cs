namespace UIMusicApp
{
    partial class Reproductor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reproductor));
            label1 = new Label();
            label2 = new Label();
            lblClima = new Label();
            label4 = new Label();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            listBox1 = new ListBox();
            listView1 = new ListView();
            btnPlayPause = new Button();
            btnAleatorio = new Button();
            btnRepetir = new Button();
            trackBar1 = new TrackBar();
            btnCancion = new Button();
            button3 = new Button();
            button5 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            lblTiempoT = new Label();
            lblNomM = new Label();
            label6 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnCerrar = new Button();
            btnSonido = new Button();
            mtrackVolumen = new TrackBar();
            lblVolumen = new TextBox();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mtrackVolumen).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Emoji", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(140, 82, 255);
            label1.Location = new Point(261, 598);
            label1.Name = "label1";
            label1.Size = new Size(45, 19);
            label1.TabIndex = 0;
            label1.Text = "0000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Emoji", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(840, 598);
            label2.Name = "label2";
            label2.Size = new Size(45, 19);
            label2.TabIndex = 1;
            label2.Text = "0000";
            // 
            // lblClima
            // 
            lblClima.AutoSize = true;
            lblClima.BackColor = Color.FromArgb(140, 82, 255);
            lblClima.Font = new Font("Yu Gothic Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClima.ForeColor = Color.White;
            lblClima.Location = new Point(293, 265);
            lblClima.Name = "lblClima";
            lblClima.Size = new Size(47, 16);
            lblClima.TabIndex = 2;
            lblClima.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(140, 82, 255);
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(296, 137);
            label4.Name = "label4";
            label4.Size = new Size(100, 21);
            label4.TabIndex = 3;
            label4.Text = "Clima Actual";
            label4.Click += label4_Click;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(1469, 845);
            axWindowsMediaPlayer1.Margin = new Padding(3, 2, 3, 2);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(307, 187);
            axWindowsMediaPlayer1.TabIndex = 4;
            axWindowsMediaPlayer1.PlayStateChange += axWindowsMediaPlayer1_PlayStateChange;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(18, 17, 70);
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.ForeColor = Color.FromArgb(140, 82, 255);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(38, 106);
            listBox1.Margin = new Padding(3, 2, 3, 2);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(152, 360);
            listBox1.TabIndex = 5;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // listView1
            // 
            listView1.BackColor = Color.White;
            listView1.BorderStyle = BorderStyle.None;
            listView1.ForeColor = Color.FromArgb(140, 82, 255);
            listView1.Location = new Point(861, 127);
            listView1.Margin = new Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(261, 266);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // btnPlayPause
            // 
            btnPlayPause.BackColor = Color.Transparent;
            btnPlayPause.BackgroundImage = Properties.Resources.boton_de_play;
            btnPlayPause.BackgroundImageLayout = ImageLayout.Zoom;
            btnPlayPause.FlatStyle = FlatStyle.Flat;
            btnPlayPause.Location = new Point(541, 645);
            btnPlayPause.Margin = new Padding(3, 2, 3, 2);
            btnPlayPause.Name = "btnPlayPause";
            btnPlayPause.Size = new Size(44, 38);
            btnPlayPause.TabIndex = 11;
            btnPlayPause.UseVisualStyleBackColor = false;
            btnPlayPause.Click += button5_Click;
            // 
            // btnAleatorio
            // 
            btnAleatorio.BackColor = Color.Transparent;
            btnAleatorio.BackgroundImage = Properties.Resources.barajar;
            btnAleatorio.BackgroundImageLayout = ImageLayout.Zoom;
            btnAleatorio.FlatStyle = FlatStyle.Flat;
            btnAleatorio.Location = new Point(356, 645);
            btnAleatorio.Margin = new Padding(3, 2, 3, 2);
            btnAleatorio.Name = "btnAleatorio";
            btnAleatorio.Size = new Size(44, 38);
            btnAleatorio.TabIndex = 12;
            btnAleatorio.UseVisualStyleBackColor = false;
            btnAleatorio.Click += btnAleatorio_Click;
            // 
            // btnRepetir
            // 
            btnRepetir.BackColor = Color.Transparent;
            btnRepetir.BackgroundImage = Properties.Resources.repetir;
            btnRepetir.BackgroundImageLayout = ImageLayout.Zoom;
            btnRepetir.FlatStyle = FlatStyle.Flat;
            btnRepetir.Location = new Point(692, 645);
            btnRepetir.Margin = new Padding(3, 2, 3, 2);
            btnRepetir.Name = "btnRepetir";
            btnRepetir.Size = new Size(44, 38);
            btnRepetir.TabIndex = 13;
            btnRepetir.UseVisualStyleBackColor = false;
            btnRepetir.Click += btnRepetir_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(313, 593);
            trackBar1.Margin = new Padding(3, 2, 3, 2);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(521, 45);
            trackBar1.TabIndex = 14;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // btnCancion
            // 
            btnCancion.BackColor = Color.Transparent;
            btnCancion.BackgroundImage = Properties.Resources.anadir__1_;
            btnCancion.BackgroundImageLayout = ImageLayout.Zoom;
            btnCancion.FlatAppearance.BorderColor = Color.FromArgb(35, 80, 183);
            btnCancion.FlatAppearance.BorderSize = 0;
            btnCancion.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 80, 183);
            btnCancion.FlatAppearance.MouseOverBackColor = Color.FromArgb(35, 80, 183);
            btnCancion.FlatStyle = FlatStyle.Flat;
            btnCancion.Location = new Point(255, 9);
            btnCancion.Margin = new Padding(3, 2, 3, 2);
            btnCancion.Name = "btnCancion";
            btnCancion.Size = new Size(69, 57);
            btnCancion.TabIndex = 15;
            btnCancion.UseVisualStyleBackColor = false;
            btnCancion.Click += btnCancion_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BackgroundImage = Properties.Resources._1;
            button3.BackgroundImageLayout = ImageLayout.Zoom;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(458, 645);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(44, 38);
            button3.TabIndex = 16;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BackgroundImage = Properties.Resources.siguiente_pista;
            button5.BackgroundImageLayout = ImageLayout.Zoom;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(617, 645);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(44, 38);
            button5.TabIndex = 17;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click_1;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // lblTiempoT
            // 
            lblTiempoT.AutoSize = true;
            lblTiempoT.BackColor = Color.Transparent;
            lblTiempoT.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTiempoT.ForeColor = Color.FromArgb(140, 82, 255);
            lblTiempoT.Location = new Point(861, 509);
            lblTiempoT.Name = "lblTiempoT";
            lblTiempoT.Size = new Size(39, 15);
            lblTiempoT.TabIndex = 18;
            lblTiempoT.Text = "label3";
            // 
            // lblNomM
            // 
            lblNomM.AutoSize = true;
            lblNomM.BackColor = Color.Transparent;
            lblNomM.Font = new Font("Yu Gothic UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomM.ForeColor = Color.FromArgb(140, 82, 255);
            lblNomM.Location = new Point(12, 609);
            lblNomM.Name = "lblNomM";
            lblNomM.Size = new Size(160, 20);
            lblNomM.TabIndex = 19;
            lblNomM.Text = "Nombre de la canción";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(140, 82, 255);
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(444, 136);
            label6.Name = "label6";
            label6.Size = new Size(61, 21);
            label6.TabIndex = 21;
            label6.Text = "Ciudad";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(140, 82, 255);
            label5.Font = new Font("Yu Gothic Medium", 9F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(454, 265);
            label5.Name = "label5";
            label5.Size = new Size(38, 16);
            label5.TabIndex = 23;
            label5.Text = "Lima";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Image = Properties.Resources.clima;
            pictureBox1.Location = new Point(293, 160);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(102, 103);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Image = Properties.Resources.paisaje_urbano;
            pictureBox2.Location = new Point(423, 160);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(102, 103);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 25;
            pictureBox2.TabStop = false;
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
            btnCerrar.Location = new Point(1107, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(58, 57);
            btnCerrar.TabIndex = 26;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnSonido
            // 
            btnSonido.BackColor = Color.Transparent;
            btnSonido.BackgroundImage = Properties.Resources.sonido__1_;
            btnSonido.BackgroundImageLayout = ImageLayout.Stretch;
            btnSonido.FlatStyle = FlatStyle.Flat;
            btnSonido.Location = new Point(902, 633);
            btnSonido.Name = "btnSonido";
            btnSonido.Size = new Size(50, 50);
            btnSonido.TabIndex = 27;
            btnSonido.UseVisualStyleBackColor = false;
            btnSonido.Click += btnSonido_Click;
            // 
            // mtrackVolumen
            // 
            mtrackVolumen.Location = new Point(972, 633);
            mtrackVolumen.Maximum = 100;
            mtrackVolumen.Name = "mtrackVolumen";
            mtrackVolumen.Size = new Size(107, 45);
            mtrackVolumen.TabIndex = 28;
            mtrackVolumen.TickStyle = TickStyle.None;
            mtrackVolumen.Value = 50;
            mtrackVolumen.Scroll += mtrackVolumen_Scroll;
            // 
            // lblVolumen
            // 
            lblVolumen.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVolumen.Location = new Point(998, 656);
            lblVolumen.Name = "lblVolumen";
            lblVolumen.Size = new Size(55, 21);
            lblVolumen.TabIndex = 29;
            lblVolumen.Text = "50";
            lblVolumen.TextAlign = HorizontalAlignment.Center;
            lblVolumen.UseWaitCursor = true;
            // 
            // Reproductor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Purple_Minimalist_Login_Website_App_Desktop_Prototype__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1167, 696);
            Controls.Add(lblVolumen);
            Controls.Add(mtrackVolumen);
            Controls.Add(btnSonido);
            Controls.Add(btnCerrar);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(lblNomM);
            Controls.Add(lblTiempoT);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(btnCancion);
            Controls.Add(trackBar1);
            Controls.Add(btnRepetir);
            Controls.Add(btnAleatorio);
            Controls.Add(btnPlayPause);
            Controls.Add(listView1);
            Controls.Add(listBox1);
            Controls.Add(axWindowsMediaPlayer1);
            Controls.Add(label4);
            Controls.Add(lblClima);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Reproductor";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reproductor";
            Load += Reproductor_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)mtrackVolumen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label lblClima;
        private Label label4;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private ListBox listBox1;
        private Button btnCerrar;
        private Button button2;
        private Button btnAtras;
        private Button button4;
        private ListView listView1;
        private Button btnPlayPause;
        private Button btnAleatorio;
        private Button btnRepetir;
        private TrackBar trackBar1;
        private Button btnCancion;
        private Button button3;
        private Button button5;
        private System.Windows.Forms.Timer timer1;
        private Label lblTiempoT;
        private Label lblNomM;
        private Label label6;
        private Label label5;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnSonido;
        private TrackBar mtrackVolumen;
        private TextBox lblVolumen;
    }
}