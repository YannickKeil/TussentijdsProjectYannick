using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Databeheer.xaml
    /// </summary>
    public partial class Databeheer : Window
    {
        public Databeheer()
        {
            InitializeComponent();
            AddPersoneel();
            EditPersoneel();
            EditCategorie();
            EditKlant();
            EditLeverancier();
        }
        private void AddPersoneel() 
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {              
                var listRechten = ctx.AdminRechtens.Select(x => x).ToList();
                cbAdminRechten.DisplayMemberPath = "titel";
                cbAdminRechten.SelectedValuePath = "AdminRechtenID";
                cbAdminRechten.ItemsSource = listRechten;
                cbAdminRechten.SelectedIndex = 0;
                cbAdminRechten2.DisplayMemberPath = "titel";
                cbAdminRechten2.SelectedValuePath = "AdminRechtenID";
                cbAdminRechten2.ItemsSource = listRechten;
                cbAdminRechten2.SelectedIndex = 0;
            }
        }

        private void EditPersoneel()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var listPersoneellid = ctx.Personeelslids.Select(x => new { naamUser = x.Voornaam + " " + x.Achternaam + " " + x.Username, id = x.PersoneelslidID }).ToList();
                cbPersoneellidEdit.DisplayMemberPath = "naamUser";
                cbPersoneellidEdit.SelectedValuePath = "id";
                cbPersoneellidEdit.ItemsSource = listPersoneellid;
                cbPersoneellidEdit.SelectedIndex = 0;
            }
        }
        private void EditCategorie()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {               
                var listCategorie = ctx.Categories.Select(x => x).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = listCategorie;
                cbCategorie.SelectedIndex = 0;
            }
        }
        private void EditKlant() 
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditKlant.ItemsSource = null;
                var listKlant = ctx.Klants.Select(x => new  { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID}).ToList();
                cbEditKlant.DisplayMemberPath = "Naam";
                cbEditKlant.SelectedValuePath = "Id";
                cbEditKlant.ItemsSource = listKlant;
                cbEditKlant.SelectedIndex = 0;
            }
        }
        private void EditLeverancier()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditLeverancier.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbEditLeverancier.DisplayMemberPath = "Contactpersoon";
                cbEditLeverancier.SelectedValuePath = "LeverancierID";
                cbEditLeverancier.ItemsSource = listLeverancier;
                cbEditLeverancier.SelectedIndex = 0;
            }
        }
        //private void btnToevoegenPersoneellid_Click(object sender, RoutedEventArgs e)
        //{
        //    AddPersoneelslid addP = new AddPersoneelslid();
        //    addP.ShowDialog();
        //}
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
                AddPersoneel();
                EditPersoneel();

            }
            //}
            // else
            //{
            //error endings
            //}
        }

        private void TabDatabeheer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabDatabeheer.SelectedIndex == TabDatabeheer.Items.Count - 1)
            {
                this.DialogResult = false;
            }
        }

        private void btnAddAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Add(new AdminRechten { titel = txtAdminRechtenToevoegen.Text });
                //ctx.SaveChanges();
                AddPersoneel();

            }
        }
        private void btnEditCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten2.SelectedValue).titel = txtAdminRechtenToevoegen.Text;
                //ctx.SaveChanges();
                AddPersoneel();

            }
        }

        private void btnDeleteAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Remove(ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten2.SelectedValue));
                //ctx.SaveChanges();
                AddPersoneel();
            }
        }

        private void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Categories.Add(new Categorie { CategorieNaam = txtCategorieToevoegen.Text });
                ctx.SaveChanges();
                EditCategorie();
            }

        }
        private void btnEditAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue).CategorieNaam = txtCategorieToevoegen.Text;
                ctx.SaveChanges();
                EditCategorie();
            }
        }

        private void btnDeleteCategorie_Click(object sender, RoutedEventArgs e)
        {           
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                
                ctx.Categories.Remove(ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue));
                ctx.SaveChanges();
                EditCategorie();
            }
        }
        private void cbPersoneellidEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if(cbPersoneellidEdit.SelectedValue != null)
                { 
                    if (ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbPersoneellidEdit.SelectedValue) != null)
                    {
                        var selectedPersoneel = ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbPersoneellidEdit.SelectedValue);
                        txtVoornaamEdit.Text = selectedPersoneel.Voornaam;
                        txtAchternaamEdit.Text = selectedPersoneel.Achternaam;
                        cbAdminRechtenEdit.SelectedValue = selectedPersoneel.AdminRechtenID;
                        txtUsernameEdit.Text = selectedPersoneel.Username;
                        dtIndiensttredingEdit.SelectedDate = Convert.ToDateTime(selectedPersoneel.Indiensttreding);
                        dtGeboortedatumEdit.SelectedDate = Convert.ToDateTime(selectedPersoneel.GeboorteDatum);
                    }
                }
            }
        }

        private void btnEditPersoneellid_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedPersoneel = ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbPersoneellidEdit.SelectedValue);

                selectedPersoneel.Voornaam = txtVoornaamEdit.Text;
                selectedPersoneel.Achternaam = txtAchternaamEdit.Text;
                selectedPersoneel.AdminRechtenID = (int)cbAdminRechtenEdit.SelectedValue;
                if (txtPasswordEdit.Password != "")
                {
                    var password = txtPasswordEdit.Password;
                    var salt = CreateSalt();
                    var hash = HashPassword(password, salt);
                    selectedPersoneel.Wachtwoord = Convert.ToBase64String(hash);
                    selectedPersoneel.Salt = Convert.ToBase64String(salt);
                }
                selectedPersoneel.Username = txtUsernameEdit.Text;
                selectedPersoneel.Indiensttreding = dtIndiensttredingEdit.SelectedDate.Value;
                selectedPersoneel.GeboorteDatum = dtGeboortedatumEdit.SelectedDate.Value;
                ctx.SaveChanges();
            }
            MessageBox.Show("edited");
            EditPersoneel();
        }

        private void btnDeletePersoneellid_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Personeelslids.Remove(ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbPersoneellidEdit.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditPersoneel();
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtHuisnummerKlantToevoegen_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtHuisnummerKlantToevoegen_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        private void btnToevoegenKlant_Click(object sender, RoutedEventArgs e)
        {            
            string bus = "";
            if (txtBusKlantToevoegen.Text != "Bus")
            { bus = txtBusKlantToevoegen.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Klants.Add(new Klant
                {
                    Voornaam = txtVoornaamKlantToevoegen.Text,
                    Achternaam = txtAchternaamKlantToevoegen.Text,
                    Straatnaam = txtStraatnaamKlantToevoegen.Text,
                    Huisnummer = Convert.ToInt32(txtHuisnummerKlantToevoegen.Text),
                    Bus = bus,
                    Postcode = txtPostcodeKlantToevoegen.Text,
                    Gemeente = txtGemeenteKlantToevoegen.Text,
                    Telefoonnummer = txtTelefoonnummerKlantToevoegen.Text,
                    Emailadres = txtEmailadresKlantToevoegen.Text,
                    AangemaaktOp = DateTime.Now,
                    Opmerking = txtOpmerkingKlantToevoegen.Text
                });
                ctx.SaveChanges();
                MessageBox.Show("Toegevoegd");
                EditKlant();
            }
        }
        private void cbEditKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditKlant.SelectedValue !=null)
                {
                    if (ctx.Klants.Single(p => p.KlantID == (int)cbEditKlant.SelectedValue) != null)
                    {
                        var selectedKlant = ctx.Klants.Single(p => p.KlantID == (int)cbEditKlant.SelectedValue);
                        txtVoornaamEditKlant.Text = selectedKlant.Voornaam;
                        txtAchternaamEditKlant.Text = selectedKlant.Achternaam;
                        txtStraatnaamEditKlant.Text = selectedKlant.Straatnaam;
                        txtHuisnummerEditKlant.Text = selectedKlant.Huisnummer.ToString();
                        if (selectedKlant.Bus != "")
                        { txtBusEditKlant.Text = selectedKlant.Bus; }
                        else { txtBusEditKlant.Text = "Bus"; }
                        txtPostcodeEditKlant.Text = selectedKlant.Postcode;
                        txtGemeenteEditKlant.Text = selectedKlant.Gemeente;
                        txtTelefoonnummerEditKlant.Text = selectedKlant.Telefoonnummer;
                        txtEmailadresEditKlant.Text = selectedKlant.Emailadres;
                        txtOpmerkingEditKlant.Text = selectedKlant.Opmerking;
                    }
                }
            }
        }

        private void btnEditKlant_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusKlantToevoegen.Text != "Bus")
            { bus = txtBusKlantToevoegen.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedKlant = ctx.Klants.Single(k => k.KlantID == (int)cbEditKlant.SelectedValue);
                selectedKlant.Voornaam = txtVoornaamEditKlant.Text;
                selectedKlant.Achternaam = txtAchternaamEditKlant.Text;
                selectedKlant.Straatnaam = txtStraatnaamEditKlant.Text;
                selectedKlant.Huisnummer = Convert.ToInt32(txtHuisnummerEditKlant.Text);
                selectedKlant.Bus = bus;
                selectedKlant.Postcode = txtPostcodeEditKlant.Text;
                selectedKlant.Gemeente = txtGemeenteEditKlant.Text;
                selectedKlant.Telefoonnummer = txtTelefoonnummerEditKlant.Text;
                selectedKlant.Emailadres = txtEmailadresEditKlant.Text;
                selectedKlant.Opmerking = txtOpmerkingEditKlant.Text;                
                ctx.SaveChanges();
            }
                MessageBox.Show("Edited");
                EditKlant();
        }

        private void btnDeleteKlant_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Klants.Remove(ctx.Klants.Single(k => k.KlantID == (int)cbEditKlant.SelectedValue));
                ctx.SaveChanges();
            }
                MessageBox.Show("Deleted");
                EditKlant();
        }

        private void btnLeverancierToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusLeverancierToevoegen.Text != "Bus")
            { bus = txtBusLeverancierToevoegen.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Leveranciers.Add(new Leverancier()
                {
                    Contactpersoon = txtContactpersoonLeverancierToevoegen.Text,
                    Telefoonnummer = txtTelefoonnummerLeverancierToevoegen.Text,
                    Emailadres = txtEmailadresLeverancierToevoegen.Text,
                    Straatnaam = txtStraatnaamLeverancierToevoegen.Text,
                    Huisnummer = Convert.ToInt32(txtHuisnummerLeverancierToevoegen.Text),
                    Bus = bus,
                    Postcode = txtPostcodeLeverancierToevoegen.Text,
                    Gemeente = txtGemeenteLeverancierToevoegen.Text
                });
                ctx.SaveChanges();
            }
                MessageBox.Show("Toevoegen");
                EditLeverancier();

        }
        private void cbEditLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditLeverancier.SelectedValue != null)
                {
                    if (ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbEditLeverancier.SelectedValue) != null)
                    {
                        var selectedPersoneel = ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbEditLeverancier.SelectedValue);
                        txtContactpersoonEditLeverancier.Text = selectedPersoneel.Contactpersoon;
                        txtTelefoonnummerEditLeverancier.Text = selectedPersoneel.Telefoonnummer;
                        txtEmailadresEditLeverancier.Text = selectedPersoneel.Emailadres;
                        txtStraatnaamEditLeverancier.Text = selectedPersoneel.Straatnaam;
                        txtHuisnummerEditLeverancier.Text = selectedPersoneel.Huisnummer.ToString();
                        if (selectedPersoneel.Bus != "")
                        { txtBusEditLeverancier.Text = selectedPersoneel.Bus; }
                        else { txtBusEditKlant.Text = "Bus"; }
                        txtPostcodeEditLeverancier.Text = selectedPersoneel.Postcode;
                        txtPostcodeEditLeverancier.Text = selectedPersoneel.Gemeente;
                    }
                }
            }

        }

        private void btnEditLeverancier_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusEditLeverancier.Text != "Bus")
            { bus = txtBusEditLeverancier.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedLeverancier = ctx.Leveranciers.Single(k => k.LeverancierID == (int)cbEditLeverancier.SelectedValue);
                selectedLeverancier.Contactpersoon = txtContactpersoonEditLeverancier.Text;
                selectedLeverancier.Telefoonnummer = txtTelefoonnummerEditLeverancier.Text;
                selectedLeverancier.Emailadres = txtEmailadresEditLeverancier.Text;
                selectedLeverancier.Straatnaam = txtStraatnaamEditLeverancier.Text;
                selectedLeverancier.Huisnummer = Convert.ToInt32(txtHuisnummerEditLeverancier.Text);
                selectedLeverancier.Bus = bus;
                selectedLeverancier.Postcode = txtPostcodeEditLeverancier.Text;
                selectedLeverancier.Gemeente = txtGemeenteEditLeverancier.Text;                              
                ctx.SaveChanges();
            }
                MessageBox.Show("Edited");
                EditLeverancier();
        }

        private void btnDeleteLeverancier_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Leveranciers.Remove(ctx.Leveranciers.Single(k => k.LeverancierID == (int)cbEditLeverancier.SelectedValue));
                ctx.SaveChanges();
            }
                MessageBox.Show("Deleted");
                EditLeverancier();
        }

    }
}
