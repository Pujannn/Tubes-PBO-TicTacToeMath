using System;
using System.Drawing;
using System.Windows.Forms;

namespace tic_tac_toe_versi_saya
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Width = 800;
            this.Height = 600;

            // Panel untuk menampung judul dan nama pembuat
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.Transparent;
            this.Controls.Add(panel);

            // Membuat label untuk menampilkan judul
            Label lblTitle = new Label();
            lblTitle.Text = "Tic Tac Toe Math PVP PRO Remake 8 Game";
            lblTitle.Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold); // Judul dengan font Arial, ukuran 20, tebal
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitle.ForeColor = Color.White;  // Warna font judul
            lblTitle.AutoSize = true;           // Membuat label menyesuaikan ukuran
            panel.Controls.Add(lblTitle);

            // Membuat label untuk menampilkan nama pembuat
            Label lblCredit = new Label();
            lblCredit.Text = "Created by:\nM.Fauzan Fredi (707082300138)\nFairuz Tsany Rabbani (707082300143)\nRaghed Davli Putra (707082300094)";
            lblCredit.Font = new System.Drawing.Font("Verdana", 12, System.Drawing.FontStyle.Italic); // Nama pembuat dengan font Verdana, ukuran 12, italic
            lblCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblCredit.ForeColor = Color.White;  // Warna font nama pembuat
            lblCredit.AutoSize = true;          // Membuat label menyesuaikan ukuran
            panel.Controls.Add(lblCredit);

            // Mengatur posisi label nama pembuat agar berada di bawah judul
            lblTitle.Location = new Point(panel.Width / 2 - lblTitle.Width / 2, panel.Height / 2 - lblTitle.Height);
            lblCredit.Location = new Point(panel.Width / 2 - lblCredit.Width / 2, lblTitle.Bottom + 10);
        }

        // Event handler untuk SplashScreen Load
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            // Kode yang ingin Anda jalankan saat SplashScreen dimuat
            Console.WriteLine("SplashScreen Loaded");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Timer timer = new Timer();
            timer.Interval = 5000; // 5 detik
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                this.Close(); // Menutup SplashScreen setelah 5 detik
            };
            timer.Start();
        }
    }
}
