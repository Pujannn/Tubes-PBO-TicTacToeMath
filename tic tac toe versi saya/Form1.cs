using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace tic_tac_toe_versi_saya
{
    public partial class Form1 : Form
    {
        public enum Player { X, O } // Pemain

        Player currentPlayer; // Pemain saat ini
        List<Button> buttons; // Daftar tombol permainan
        string[,] mathQuestions; // Menyimpan soal matematika
        int[,] answers; // Jawaban soal
        Random random = new Random(); // Untuk membuat soal acak
        int playerXScore = 0; // Skor pemain X
        int playerOScore = 0; // Skor pemain O
        bool isGameOver = false; // Menandakan apakah permainan selesai

        public Form1()
        {
            InitializeComponent();
            InitializeGame();

            int cornerRadius = 10; // Set the corner radius for the round buttons
            SetButtonRound(btnSubmit, cornerRadius);
            SetButtonRound(btnRestart, cornerRadius);

            // Set button styles for Submit and Restart
            btnSubmit.BackColor = Color.Lime; // You can customize the color
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;

            btnRestart.BackColor = Color.Red; // Customize this color too
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.FlatAppearance.BorderSize = 0;

            // Event handler untuk tombol Submit dan Restart
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnRestart.Click += new EventHandler(btnRestart_Click);

        }

        private void InitializeGame()
        {
            // Inisialisasi daftar tombol
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            // Atur sudut membulat untuk setiap tombol
            int cornerRadius = 10; // Radius sudut
            foreach (Button btn in buttons)
            {
                SetButtonRound(btn, cornerRadius);
                btn.BackColor = Color.White; // Warna latar belakang tombol
                btn.FlatStyle = FlatStyle.Flat; // Atur gaya tombol menjadi flat
                btn.FlatAppearance.BorderSize = 0; // Hilangkan border
            }

            // Soal-soal matematika dan jawaban
            mathQuestions = new string[3, 3];
            answers = new int[3, 3];
            GenerateMathQuestions();

            RestartGame();
        }

        private void SetButtonRound(Button button, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(button.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(button.Width - cornerRadius, button.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, button.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseFigure();

            button.Region = new Region(path);
        }

        private void GenerateMathQuestions()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int a = random.Next(1, 10);
                    int b = random.Next(1, 10);
                    int operation = random.Next(0, 4);

                    switch (operation)
                    {
                        case 0: // Penjumlahan
                            mathQuestions[i, j] = $"{a} + {b} = ?";
                            answers[i, j] = a + b;
                            break;
                        case 1: // Pengurangan
                            mathQuestions[i, j] = $"{a} - {b} = ?";
                            answers[i, j] = a - b;
                            break;
                        case 2: // Perkalian
                            mathQuestions[i, j] = $"{a} × {b} = ?";
                            answers[i, j] = a * b;
                            break;
                        case 3: // Pembagian (hasil pembagian akan bulat)
                            a = random.Next(2, 10) * b; // Pastikan a habis dibagi b
                            mathQuestions[i, j] = $"{a} ÷ {b} = ?";
                            answers[i, j] = a / b;
                            break;
                    }
                }
            }
        }

        private void RestartGame()
        {
            // Reset tombol permainan
            foreach (Button btn in buttons)
            {
                btn.Text = "?";
                btn.Enabled = true;
                btn.BackColor = Color.White; // Reset warna latar belakang
                btn.Tag = null; // Hapus tag lama
            }

            currentPlayer = Player.X;
            lblTurn.Text = "Player X is Turn";
            lblSoal.Text = "";
            txtJawaban.Text = "";
            isGameOver = false;
        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            if (isGameOver) return;

            Button clickedButton = sender as Button;

            if (clickedButton.Text == "?")
            {
                // Ambil posisi tombol dalam grid
                int index = buttons.IndexOf(clickedButton);
                int row = index / 3;
                int col = index % 3;

                // Tampilkan soal matematika
                lblSoal.Text = mathQuestions[row, col];
                btnSubmit.Tag = new Tuple<Button, int>(clickedButton, answers[row, col]);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Tag is Tuple<Button, int> questionData)
            {
                Button clickedButton = questionData.Item1;
                int correctAnswer = questionData.Item2;

                if (int.TryParse(txtJawaban.Text, out int playerAnswer))
                {
                    if (playerAnswer == correctAnswer)
                    {
                        // Jawaban benar: tampilkan simbol pemain
                        clickedButton.Text = currentPlayer.ToString();
                        clickedButton.Enabled = false;
                        clickedButton.BackColor = currentPlayer == Player.X ? Color.LightGreen : Color.Pink;

                        // Periksa kondisi kemenangan
                        if (CheckWin())
                        {
                            MessageBox.Show($"Player {currentPlayer} Wins!", "Game Over");
                            UpdateScore();
                            DisableAllButtons();
                            isGameOver = true;
                            RestartGame();
                            GenerateMathQuestions();
                            return;
                        }

                        // Periksa apakah seri
                        if (IsDraw())
                        {
                            MessageBox.Show("It's a Draw!", "Game Over");
                            DisableAllButtons();
                            isGameOver = true;
                            RestartGame();
                            GenerateMathQuestions();
                            return;
                        }

                        // Ganti pemain
                        currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
                        lblTurn.Text = $"Player {currentPlayer} is Turn";
                    }
                    else
                    {
                        MessageBox.Show("Jawaban Salah! Giliran berpindah ke pemain lain.", "Wrong Answer");
                        currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
                        lblTurn.Text = $"Player {currentPlayer} is Turn";
                    }

                    // Reset soal dan jawaban
                    lblSoal.Text = "";
                    txtJawaban.Text = "";
                }
                else
                {
                    MessageBox.Show("Masukkan angka yang valid!", "Invalid Input");
                }
            }
        }

        private bool CheckWin()
        {
            return (CheckLine(button1, button2, button3) ||
                    CheckLine(button4, button5, button6) ||
                    CheckLine(button7, button8, button9) ||
                    CheckLine(button1, button4, button7) ||
                    CheckLine(button2, button5, button8) ||
                    CheckLine(button3, button6, button9) ||
                    CheckLine(button1, button5, button9) ||
                    CheckLine(button3, button5, button7));
        }

        private bool CheckLine(Button b1, Button b2, Button b3)
        {
            return b1.Text != "?" && b1.Text == b2.Text && b2.Text == b3.Text;
        }

        private bool IsDraw()
        {
            return buttons.All(b => b.Text != "?");
        }

        private void UpdateScore()
        {
            if (currentPlayer == Player.X)
            {
                playerXScore++;
                lblScore.Text = $"Player X: {playerXScore} | Player O: {playerOScore}";
            }
            else
            {
                playerOScore++;
                lblScore.Text = $"Player X: {playerXScore} | Player O: {playerOScore}";
            }
        }

        private void DisableAllButtons()
        {
            foreach (Button btn in buttons)
            {
                btn.Enabled = false;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
            GenerateMathQuestions();
        }

        
    }
}