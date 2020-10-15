using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Shapes;
using Konscious.Security.Cryptography;

namespace TussentijdsProjectYannick
{
    /// <summary>
    /// Interaction logic for AddPersoneelslid.xaml
    /// </summary>
    public partial class AddPersoneelslid : Window
    {
        public AddPersoneelslid()
        {
            InitializeComponent();
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var listRechten = ctx.AdminRechtens.Select(x => x).ToList();
                cbAdminRechten.DisplayMemberPath = "titel";
                cbAdminRechten.SelectedValuePath = "AdminRechtenID";
                cbAdminRechten.ItemsSource = listRechten;
                cbAdminRechten.SelectedIndex = 0;
            }
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //if (checks)
            //{
            var password = txtPassword.Password;
            var salt = CreateSalt();
            var hash = HashPassword(password, salt);
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Personeelslids.Add(new Personeelslid()
                {
                    Voornaam = txtVoornaam.Text,
                    Achternaam = txtAchternaam.Text,
                    Wachtwoord = Convert.ToBase64String(hash),
                    AdminRechtenID = (int)cbAdminRechten.SelectedValue,
                    Salt = Convert.ToBase64String(salt),
                    Username = txtUsername.Text,
                    Indiensttreding = dtIndiensttreding.SelectedDate.Value,
                    GeboorteDatum = dtGeboortedatum.SelectedDate.Value                  
                });
                ctx.SaveChanges();
                MessageBox.Show("toegevoegt");
                foreach (var item in ctx.Personeelslids.Select(x=>x))
                {
                    MessageBox.Show(item.ToString());
                }

            }
            //}
            // else
            //{
            //error endings
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
