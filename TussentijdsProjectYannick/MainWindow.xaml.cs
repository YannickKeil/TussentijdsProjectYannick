using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Konscious.Security.Cryptography;

namespace TussentijdsProjectYannick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Run();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 1;
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
        public void Run()
        {
            var password = "Hello World!";
            var stopwatch = Stopwatch.StartNew();

            MessageBox.Show($"Creating hash for password '{ password }'.");

            var salt = CreateSalt();
            MessageBox.Show($"Using salt '{ Convert.ToBase64String(salt) }'.");

            var hash = HashPassword(password, salt);
            MessageBox.Show($"Hash is '{ Convert.ToBase64String(hash) }'.");

            stopwatch.Stop();
            MessageBox.Show($"Process took { stopwatch.ElapsedMilliseconds / 1024.0 } s");

            stopwatch = Stopwatch.StartNew();
            MessageBox.Show($"Verifying hash...");

            var success = VerifyHash(password, salt, hash);
            MessageBox.Show(success ? "Success!" : "Failure!");

            stopwatch.Stop();
            MessageBox.Show($"Process took { stopwatch.ElapsedMilliseconds / 1024.0 } s");
        }


        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (ctx.Personeelslids.Where(pl => pl.Username == txtUsername.Text).FirstOrDefault() != null)
                {
                    byte[] hash = Convert.FromBase64String(ctx.Personeelslids.Where(pl => pl.Username == txtUsername.Text).FirstOrDefault().Wachtwoord);                   
                    byte[] salt = Convert.FromBase64String(ctx.Personeelslids.Where(pl => pl.Username == txtUsername.Text).FirstOrDefault().Salt);
                    string password = txtWachtwoord.Password;                    
                    var success = VerifyHash(password, salt, hash);
                    MessageBox.Show(success ? "Success!" : "Failure!");
                    if (success)
                    {
                        MessageBox.Show("account verrified");
                        MainMenu mm = new MainMenu();
                        mm.Owner = this;
                        this.Hide();
                        if (mm.ShowDialog() == true)
                        {
                            this.Show();
                        }

                    }
                    else { /*username doen't exist*/MessageBox.Show("account failed verrification"); }
                }
                else { /*username doen't exist*/MessageBox.Show("account failed verrification"); }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
            }
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
                txtUsername.Text = "Username";
        }

        private void txtWachtwoord_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtWachtwoord.Password == "Password")
            {
                txtWachtwoord.Password = "";
            }
        }

        private void txtWachtwoord_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWachtwoord.Password))
                txtWachtwoord.Password = "Password";
        }
    }
}
