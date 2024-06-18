using System;
namespace UIMusicApp
{
    partial class Carga
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
            timer2 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // Carga
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Sangeetic;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(683, 622);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Carga";
            Text = "Carga";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer2;
    }
}