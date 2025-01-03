using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace tic_tac_toe_versi_saya
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Hilangkan border
            this.BackColor = System.Drawing.Color.White; // Warna latar belakang
            this.Width = 400;
            this.Height = 300;

            Label lblCredit = new Label();
            lblCredit.Text = "Tic Tac Toe\n\nCreated by: [Nama Pembuat]";
            lblCredit.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblCredit.Dock = DockStyle.Fill;

            this.Controls.Add(lblCredit);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Timer timer = new Timer();
            timer.Interval = 3000; // 3 detik
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }
}
