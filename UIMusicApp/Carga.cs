using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIMusicApp
{
    public partial class Carga : Form
    {
        private MulticolorProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        public Carga()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            progressBar1 = new MulticolorProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Step = 1,
                Location = new Point(10, this.ClientSize.Height - 50),
                Size = new Size(this.ClientSize.Width - 20, 30),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(progressBar1);


            label1 = new Label
            {
                Location = new Point(10, this.ClientSize.Height - 70),
                AutoSize = true,
                BackColor = ColorTranslator.FromHtml("#6593F4"),
                ForeColor = Color.White,
                Text = "0%",
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            this.Controls.Add(label1);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 100;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.PerformStep();
                label1.Text = $"{progressBar1.Value}%";
                int newLabelX = progressBar1.Location.X + (progressBar1.Width * progressBar1.Value / progressBar1.Maximum) - (label1.Width / 2);
                label1.Location = new Point(newLabelX, label1.Location.Y);
            }
            else
            {
                timer1.Stop();
                this.Hide();
                Login loginForm = new Login();
                loginForm.Show();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (progressBar1 != null && label1 != null)
            {
                progressBar1.Size = new Size(this.ClientSize.Width - 20, 30);
                progressBar1.Location = new Point(10, this.ClientSize.Height - 50);
                label1.Location = new Point(label1.Location.X, this.ClientSize.Height - 70);
            }
        }
    }
}
