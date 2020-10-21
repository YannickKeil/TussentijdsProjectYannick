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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            EditAdminrechten();
            //EditPersoneel();
            EditCategorie();
            //EditKlant();
            //EditLeverancier();
            EditProductenfillCombobox();
            EditBestellingfillCombobox();
            EditBestellingProductfillCombobox();
            
        }

        private void EditPersoneel()
        {
            
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditPersoneellid.ItemsSource = null;
                var listPersoneellid = ctx.Personeelslids.Select(x => new { naamUser = x.Voornaam + " " + x.Achternaam + " " + x.Username, id = x.PersoneelslidID }).ToList();
                cbEditPersoneellid.DisplayMemberPath = "naamUser";
                cbEditPersoneellid.SelectedValuePath = "id";
                cbEditPersoneellid.ItemsSource = listPersoneellid;
                cbEditPersoneellid.SelectedIndex = 0;
                cbAdminRechtenPersoneellidEdit.ItemsSource = null;
                var listRechten = ctx.AdminRechtens.Select(x => x).ToList();
                cbAdminRechtenPersoneellidEdit.DisplayMemberPath = "titel";
                cbAdminRechtenPersoneellidEdit.SelectedValuePath = "AdminRechtenID";
                cbAdminRechtenPersoneellidEdit.ItemsSource = listRechten;
                cbAdminRechtenPersoneellidEdit.SelectedIndex = 0;
                EditBestellingfillCombobox();
            }
        }
        private void EditAdminrechten()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbAdminRechten.ItemsSource = null;
                var listRechten = ctx.AdminRechtens.Select(x => x).ToList();
                cbAdminRechten.DisplayMemberPath = "titel";
                cbAdminRechten.SelectedValuePath = "AdminRechtenID";
                cbAdminRechten.ItemsSource = listRechten;
                cbAdminRechten.SelectedIndex = 0;
            }
        }


        private void EditCategorie()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbCategorie.ItemsSource = null;
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
                var listKlant = ctx.Klants.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbEditKlant.DisplayMemberPath = "Naam";
                cbEditKlant.SelectedValuePath = "Id";
                cbEditKlant.ItemsSource = listKlant;
                cbEditKlant.SelectedIndex = 0;
                EditBestellingfillCombobox();
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
                EditBestellingfillCombobox();
            }
        }
        private void EditProducten()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditProducten.ItemsSource = null;
                var listProducten = ctx.Products.Select(x => x).ToList();
                cbEditProducten.DisplayMemberPath = "Naam";
                cbEditProducten.SelectedValuePath = "ProductID";
                cbEditProducten.ItemsSource = listProducten;
                cbEditProducten.SelectedIndex = 0;
                EditBestellingProductfillCombobox();
            }
        }
        private void EditProductenfillCombobox()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditLeverancier.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbLeverancierEditProducten.DisplayMemberPath = "Contactpersoon";
                cbLeverancierEditProducten.SelectedValuePath = "LeverancierID";
                cbLeverancierEditProducten.ItemsSource = listLeverancier;
                cbLeverancierEditProducten.SelectedIndex = 0;
                var listCategorie = ctx.Categories.Select(x => x).ToList();
                cbCategorieEditProducten.DisplayMemberPath = "CategorieNaam";
                cbCategorieEditProducten.SelectedValuePath = "CategorieID";
                cbCategorieEditProducten.ItemsSource = listCategorie;
                cbCategorieEditProducten.SelectedIndex = 0;
            }
        }
        private void EditBestelling()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditBestelling.ItemsSource = null;
                var listBestellingen = ctx.Bestellings.Select(x => new
                {
                    Naam = x.Klant.KlantID +
                    " " +
                    x.Klant.Voornaam +
                    " " +
                    x.Klant.Achternaam +
                    " " +
                    x.DatumOpgemaakt,
                    Id = x.BestellingID
                }).ToList();
                cbEditBestelling.DisplayMemberPath = "Naam";
                cbEditBestelling.SelectedValuePath = "Id";
                cbEditBestelling.ItemsSource = listBestellingen;
                cbEditBestelling.SelectedIndex = 0;
                EditBestellingProductfillCombobox();
            }
        }
        private void EditBestellingfillCombobox()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbPersoneelslidEditBestelling.ItemsSource = null;
                var listPersoneellid = ctx.Personeelslids.Select(x => new { naamUser = x.Voornaam + " " + x.Achternaam + " " + x.Username, id = x.PersoneelslidID }).ToList();
                cbPersoneelslidEditBestelling.DisplayMemberPath = "naamUser";
                cbPersoneelslidEditBestelling.SelectedValuePath = "id";
                cbPersoneelslidEditBestelling.ItemsSource = listPersoneellid;
                cbPersoneelslidEditBestelling.SelectedIndex = 0;
                cbLeverancierEditBestelling.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbLeverancierEditBestelling.DisplayMemberPath = "Contactpersoon";
                cbLeverancierEditBestelling.SelectedValuePath = "LeverancierID";
                cbLeverancierEditBestelling.ItemsSource = listLeverancier;
                cbLeverancierEditBestelling.SelectedIndex = 0;
                cbKlantEditBestelling.ItemsSource = null;
                var listKlant = ctx.Klants.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbKlantEditBestelling.DisplayMemberPath = "Naam";
                cbKlantEditBestelling.SelectedValuePath = "Id";
                cbKlantEditBestelling.ItemsSource = listKlant;
                cbKlantEditBestelling.SelectedIndex = 0;
            }
        }
        private void EditBestellingProduct()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbEditBestellingProduct.ItemsSource = null;
                var listBestellingProducten = ctx.BestellingProducts.Select(x => new
                {
                    Naam = x.Bestelling.Klant.KlantID +
                    " " +
                    x.Bestelling.Klant.Voornaam +
                    " " +
                    x.Bestelling.Klant.Achternaam +
                    " " +
                    x.Bestelling.DatumOpgemaakt+
                    " "+
                    x.Product.Naam+
                    " "+
                    x.Product.Eenheid,
                    Id = x.BestellingID
                }).ToList();
                cbEditBestellingProduct.DisplayMemberPath = "Naam";
                cbEditBestellingProduct.SelectedValuePath = "Id";
                cbEditBestellingProduct.ItemsSource = listBestellingProducten;
                cbEditBestellingProduct.SelectedIndex = 0;
            }
        }
        private void EditBestellingProductfillCombobox()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbBestellingenEditBestellingProduct.ItemsSource = null;
                var listBestellingen = ctx.Bestellings.Select(x => new
                {
                    Naam = x.Klant.KlantID +
                    " " +
                    x.Klant.Voornaam +
                    " " +
                    x.Klant.Achternaam +
                    " " +
                    x.DatumOpgemaakt,
                    Id = x.BestellingID
                }).ToList();
                cbBestellingenEditBestellingProduct.DisplayMemberPath = "Naam";
                cbBestellingenEditBestellingProduct.SelectedValuePath = "Id";
                cbBestellingenEditBestellingProduct.ItemsSource = listBestellingen;
                cbBestellingenEditBestellingProduct.SelectedIndex = 0;
                cbProductenEditBestellingProduct.ItemsSource = null;
                var listProducten = ctx.Products.Select(x => x).ToList();
                cbProductenEditBestellingProduct.DisplayMemberPath = "Naam";
                cbProductenEditBestellingProduct.SelectedValuePath = "ProductID";
                cbProductenEditBestellingProduct.ItemsSource = listProducten;
                cbProductenEditBestellingProduct.SelectedIndex = 0;

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
        private void tbPersoneellidEdit_Checked(object sender, RoutedEventArgs e)
        {
            if (tbPersoneellidEdit.IsChecked == true)
            {

                btnEditPersoneellid.IsEnabled = true;
                btnDeletePersoneellid.IsEnabled = true;
                cbEditPersoneellid.IsEnabled = true;
                btnToevoegenPersoneellid.IsEnabled = false;
                EditPersoneel();
                lblPassword.Text = "Als de passwordbox leeg is behoud de user het dezelfde wachtwoord";
            }
            else if (tbPersoneellidEdit.IsChecked == false)
            {
                btnEditPersoneellid.IsEnabled = false;
                btnDeletePersoneellid.IsEnabled = false;
                cbEditPersoneellid.IsEnabled = false;
                btnToevoegenPersoneellid.IsEnabled = true;
                txtVoornaamPersoneellidEdit.Text = "Voornaam";
                txtAchternaamPersoneellidEdit.Text = "Achternaam";
                txtUsernamePersoneellidEdit.Text = "Username";
                dtIndiensttredingPersoneellidEdit.DisplayDate = DateTime.Now;
                dtGeboortedatumPersoneellidEdit.DisplayDate = DateTime.Now;
                dtIndiensttredingPersoneellidEdit.SelectedDate = DateTime.Now;
                dtGeboortedatumPersoneellidEdit.SelectedDate = DateTime.Now;
                lblPassword.Text = "Wachtwoord";
            }
        }
        private void btnAddPersoneellid_Click(object sender, RoutedEventArgs e)
        {
            //if (checks)
            //{
            var password = txtPasswordPersoneellidEdit.Password;
            var salt = CreateSalt();
            var hash = HashPassword(password, salt);
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Personeelslids.Add(new Personeelslid()
                {
                    Voornaam = txtVoornaamPersoneellidEdit.Text,
                    Achternaam = txtAchternaamPersoneellidEdit.Text,
                    Wachtwoord = Convert.ToBase64String(hash),
                    AdminRechtenID = (int)cbAdminRechtenPersoneellidEdit.SelectedValue,
                    Salt = Convert.ToBase64String(salt),
                    Username = txtUsernamePersoneellidEdit.Text,
                    Indiensttreding = dtIndiensttredingPersoneellidEdit.SelectedDate.Value,
                    GeboorteDatum = dtGeboortedatumPersoneellidEdit.SelectedDate.Value
                });
                ctx.SaveChanges();
                MessageBox.Show("toegevoegt");
                EditPersoneel();
            }
            //}
            // else
            //{
            //error endings
            //}
        }
        private void cbPersoneellidEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditPersoneellid.SelectedValue != null)
                {
                    if (ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue) != null)
                    {
                        var selectedPersoneel = ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue);
                        txtVoornaamPersoneellidEdit.Text = selectedPersoneel.Voornaam;
                        txtAchternaamPersoneellidEdit.Text = selectedPersoneel.Achternaam;
                        cbAdminRechtenPersoneellidEdit.SelectedValue = selectedPersoneel.AdminRechtenID;
                        txtUsernamePersoneellidEdit.Text = selectedPersoneel.Username;
                        dtIndiensttredingPersoneellidEdit.SelectedDate = Convert.ToDateTime(selectedPersoneel.Indiensttreding);
                        dtGeboortedatumPersoneellidEdit.SelectedDate = Convert.ToDateTime(selectedPersoneel.GeboorteDatum);
                    }
                }
            }
        }

        private void btnEditPersoneellid_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedPersoneel = ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue);

                selectedPersoneel.Voornaam = txtVoornaamPersoneellidEdit.Text;
                selectedPersoneel.Achternaam = txtAchternaamPersoneellidEdit.Text;
                selectedPersoneel.AdminRechtenID = (int)cbAdminRechtenPersoneellidEdit.SelectedValue;
                if (txtPasswordPersoneellidEdit.Password != "")
                {
                    var password = txtPasswordPersoneellidEdit.Password;
                    var salt = CreateSalt();
                    var hash = HashPassword(password, salt);
                    selectedPersoneel.Wachtwoord = Convert.ToBase64String(hash);
                    selectedPersoneel.Salt = Convert.ToBase64String(salt);
                }
                selectedPersoneel.Username = txtUsernamePersoneellidEdit.Text;
                selectedPersoneel.Indiensttreding = dtIndiensttredingPersoneellidEdit.SelectedDate.Value;
                selectedPersoneel.GeboorteDatum = dtGeboortedatumPersoneellidEdit.SelectedDate.Value;
                ctx.SaveChanges();
            }
            MessageBox.Show("edited");
            EditPersoneel();
        }

        private void btnDeletePersoneellid_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Personeelslids.Remove(ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditPersoneel();
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
            }
            EditAdminrechten();
        }
        private void btnEditAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten.SelectedValue).titel = txtAdminRechtenToevoegen.Text;
                //ctx.SaveChanges();
            }
            EditAdminrechten();
        }

        private void btnDeleteAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Remove(ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten.SelectedValue));
                //ctx.SaveChanges();
            }
            EditAdminrechten();
        }

        private void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Categories.Add(new Categorie { CategorieNaam = txtCategorieToevoegen.Text });
                ctx.SaveChanges();
            }
            EditCategorie();
            EditProductenfillCombobox();

        }
        private void btnEditCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue).CategorieNaam = txtCategorieToevoegen.Text;
                ctx.SaveChanges();
            }
            EditCategorie();
            EditProductenfillCombobox();
        }

        private void btnDeleteCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {

                ctx.Categories.Remove(ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue));
                ctx.SaveChanges();
            }
            EditCategorie();
            EditProductenfillCombobox();
        }

        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
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

        private void tbEditKlant_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditKlant.IsChecked == true)
            {
                EditKlant();
                btnEditKlant.IsEnabled = true;
                btnDeleteKlant.IsEnabled = true;
                cbEditKlant.IsEnabled = true;
                btnToevoegenKlant.IsEnabled = false;
            }
            else if (tbEditKlant.IsChecked == false)
            {
                btnEditKlant.IsEnabled = false;
                btnDeleteKlant.IsEnabled = false;
                cbEditKlant.IsEnabled = false;
                btnToevoegenKlant.IsEnabled = true;
                txtVoornaamEditKlant.Text = "Voornaam";
                txtAchternaamEditKlant.Text = "Achternaam";
                txtStraatnaamEditKlant.Text = "Straatnaam";
                txtHuisnummerEditKlant.Text = "Huisnummer";
                txtBusEditKlant.Text = "Bus";
                txtPostcodeEditKlant.Text = "Postcode";
                txtGemeenteEditKlant.Text = "Gemeente";
                txtTelefoonnummerEditKlant.Text = "Telefoon nummer";
                txtEmailadresEditKlant.Text = "e-mail";
                txtOpmerkingEditKlant.Text = "Opmerking";

            }
        }
        private void btnToevoegenKlant_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusEditKlant.Text != "Bus")
            { bus = txtBusEditKlant.Text; }
            string opmerking = "";
            if (txtOpmerkingEditKlant.Text != "Opmerking")
            { opmerking = txtOpmerkingEditKlant.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Klants.Add(new Klant
                {
                    Voornaam = txtVoornaamEditKlant.Text,
                    Achternaam = txtAchternaamEditKlant.Text,
                    Straatnaam = txtStraatnaamEditKlant.Text,
                    Huisnummer = Convert.ToInt32(txtHuisnummerEditKlant.Text),
                    Bus = bus,
                    Postcode = txtPostcodeEditKlant.Text,
                    Gemeente = txtGemeenteEditKlant.Text,
                    Telefoonnummer = txtTelefoonnummerEditKlant.Text,
                    Emailadres = txtEmailadresEditKlant.Text,
                    AangemaaktOp = DateTime.Now,
                    Opmerking = opmerking
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
                if (cbEditKlant.SelectedValue != null)
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
            if (txtBusEditKlant.Text != "Bus")
            { bus = txtBusEditKlant.Text; }
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
        private void tbEditLeverancier_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditLeverancier.IsChecked == true)
            {
                EditLeverancier();
                btnEditLeverancier.IsEnabled = true;
                btnDeleteLeverancier.IsEnabled = true;
                cbEditLeverancier.IsEnabled = true;
                btnToevoegenLeverancier.IsEnabled = false;
            }
            else if (tbEditLeverancier.IsChecked == false)
            {
                btnEditLeverancier.IsEnabled = false;
                btnDeleteLeverancier.IsEnabled = false;
                cbEditLeverancier.IsEnabled = false;
                btnToevoegenLeverancier.IsEnabled = true;
                txtContactpersoonEditLeverancier.Text = "Contactpersoon";
                txtTelefoonnummerEditLeverancier.Text = "Telefoon nummer";
                txtEmailadresEditLeverancier.Text = "e-mail";
                txtStraatnaamEditLeverancier.Text = "Straatnaam";
                txtHuisnummerEditLeverancier.Text = "Huisnummer";
                txtBusEditLeverancier.Text = "Bus";
                txtPostcodeEditLeverancier.Text = "Postcode";
                txtGemeenteEditLeverancier.Text = "Gemeente";
            }
        }
        private void btnToevoegenLeverancier_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusEditLeverancier.Text != "Bus")
            { bus = txtBusEditLeverancier.Text; }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Leveranciers.Add(new Leverancier()
                {
                    Contactpersoon = txtContactpersoonEditLeverancier.Text,
                    Telefoonnummer = txtTelefoonnummerEditLeverancier.Text,
                    Emailadres = txtEmailadresEditLeverancier.Text,
                    Straatnaam = txtStraatnaamEditLeverancier.Text,
                    Huisnummer = Convert.ToInt32(txtHuisnummerEditLeverancier.Text),
                    Bus = bus,
                    Postcode = txtPostcodeEditLeverancier.Text,
                    Gemeente = txtGemeenteEditLeverancier.Text
                });
                ctx.SaveChanges();
            }
            MessageBox.Show("Toevoegen");
            EditLeverancier();
            EditProductenfillCombobox();
            //cbEditLeverancier.SelectedIndex = cbEditLeverancier.Items.Count - 1;

        }
        private void cbEditLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditLeverancier.SelectedValue != null)
                {
                    if (ctx.Leveranciers.Single(l => l.LeverancierID == (int)cbEditLeverancier.SelectedValue) != null)
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
            EditProductenfillCombobox();
        }

        private void btnDeleteLeverancier_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Leveranciers.Remove(ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbEditLeverancier.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditLeverancier();
            EditProductenfillCombobox();
        }
        private void tbEditProducten_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditProducten.IsChecked == true)
            {
                EditProducten();
                btnEditProducten.IsEnabled = true;
                btnDeleteProducten.IsEnabled = true;
                cbEditProducten.IsEnabled = true;
                btnToevoegenProducten.IsEnabled = false;
            }
            else if (tbEditProducten.IsChecked == false)
            {
                btnEditProducten.IsEnabled = false;
                btnDeleteProducten.IsEnabled = false;
                cbEditProducten.IsEnabled = false;
                btnToevoegenProducten.IsEnabled = true;
                txtNaamEditProducten.Text = "Naam";
                txtMargeEditProducten.Text = "Marge";
                txtEenheidEditProducten.Text = "Eenheid";
                txtBTWEditProducten.Text = "BTW";
                nudAantalOpVooraadProducten.Text = "0";
                EditProductenfillCombobox();
            }

        }
        private void cbEditProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditProducten.SelectedValue != null)
                {
                    if (ctx.Products.Single(p => p.ProductID == (int)cbEditProducten.SelectedValue) != null)
                    {
                        var selectedProduct = ctx.Products.Single(p => p.ProductID == (int)cbEditProducten.SelectedValue);
                        txtNaamEditProducten.Text = selectedProduct.Naam;
                        txtMargeEditProducten.Text = selectedProduct.Marge.ToString();
                        txtEenheidEditProducten.Text = selectedProduct.Eenheid;
                        txtBTWEditProducten.Text = selectedProduct.BTW.ToString();
                        cbLeverancierEditProducten.SelectedValue = selectedProduct.LeverancierID;
                        cbCategorieEditProducten.SelectedValue = selectedProduct.CategorieID;
                        nudAantalOpVooraadProducten.Text = selectedProduct.AantalOpVooraad.ToString();
                    }
                }
            }
        }
        private void btnNudAantalOpVooraadProductenUp_Click(object sender, RoutedEventArgs e)
        {
            decimal getal = Convert.ToDecimal(nudAantalOpVooraadProducten.Text);
            getal++;
            nudAantalOpVooraadProducten.Text = getal.ToString();

        }
        private void btnNudAantalOpVooraadProductenDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDecimal(nudAantalOpVooraadProducten.Text) > 0)
            nudAantalOpVooraadProducten.Text = $"{Convert.ToDecimal(nudAantalOpVooraadProducten.Text) - 1}";
        }
        private void btnToevoegenProducten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Products.Add(new Product
                {
                    Naam = txtNaamEditProducten.Text,
                    Marge = Convert.ToDecimal(txtMargeEditProducten.Text),
                    Eenheid = txtEenheidEditProducten.Text,
                    BTW = Convert.ToDecimal(txtBTWEditProducten.Text),
                    LeverancierID = (int)cbLeverancierEditProducten.SelectedValue,
                    CategorieID = (int)cbCategorieEditProducten.SelectedValue,
                    AantalOpVooraad = Convert.ToInt32(nudAantalOpVooraadProducten.Text)
                });
                ctx.SaveChanges();
            }
            MessageBox.Show("Toevoegen");
            EditProducten();

        }

        private void btnEditProducten_Click(object sender, RoutedEventArgs e)
        {

            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedProduct = ctx.Products.Single(p => p.ProductID == (int)cbEditProducten.SelectedValue);
                selectedProduct.Naam = txtNaamEditProducten.Text;
                selectedProduct.Marge = Convert.ToDecimal(txtMargeEditProducten.Text);
                selectedProduct.Eenheid = txtEenheidEditProducten.Text;
                selectedProduct.BTW = Convert.ToDecimal(txtBTWEditProducten.Text);
                selectedProduct.LeverancierID = (int)cbLeverancierEditProducten.SelectedValue;
                selectedProduct.CategorieID = (int)cbCategorieEditProducten.SelectedValue;
                selectedProduct.AantalOpVooraad = Convert.ToInt32(nudAantalOpVooraadProducten.Text);
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            EditProducten();
        }

        private void btnDeleteProducten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Products.Remove(ctx.Products.Single(p => p.ProductID == (int)cbEditProducten.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditProducten();
        }

        private void tbEditBestelling_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditBestelling.IsChecked == true)
            {
                EditBestelling();
                btnEditBestelling.IsEnabled = true;
                btnDeleteBestelling.IsEnabled = true;
                cbEditBestelling.IsEnabled = true;
                btnToevoegenBestelling.IsEnabled = false;
            }
            else if (tbEditProducten.IsChecked == false)
            {
                btnEditBestelling.IsEnabled = false;
                btnDeleteBestelling.IsEnabled = false;
                cbEditBestelling.IsEnabled = false;
                btnToevoegenBestelling.IsEnabled = true;
                dtDatumOpgemaakt.SelectedDate = DateTime.Now;
                dtDatumOpgemaakt.DisplayDate = DateTime.Now;
                EditBestellingfillCombobox();
            }
        }

        private void cbEditBestelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditBestelling.SelectedValue != null)
                {
                    if (ctx.Bestellings.Single(b => b.BestellingID == (int)cbEditBestelling.SelectedValue) != null)
                    {
                        var selectedBestelling = ctx.Bestellings.Single(b => b.BestellingID == (int)cbEditBestelling.SelectedValue);
                        dtDatumOpgemaakt.SelectedDate = selectedBestelling.DatumOpgemaakt;
                        dtDatumOpgemaakt.DisplayDate = selectedBestelling.DatumOpgemaakt;
                        cbPersoneelslidEditBestelling.SelectedValue = selectedBestelling.PersoneelslidID;
                        cbLeverancierEditBestelling.SelectedValue = selectedBestelling.LeverancierID;
                        cbKlantEditBestelling.SelectedValue = selectedBestelling.KlantID;
                    }
                }
            }
        }

        private void btnToevoegenBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Bestellings.Add(new Bestelling
                {
                    DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate,
                    PersoneelslidID = (int)cbPersoneelslidEditBestelling.SelectedValue,
                    LeverancierID = (int)cbLeverancierEditBestelling.SelectedValue,
                    KlantID = (int)cbKlantEditBestelling.SelectedValue
                });
                ctx.SaveChanges();
            }
            MessageBox.Show("Toevoegen");
            EditBestelling();
        }

        private void btnEditBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedBestelling = ctx.Bestellings.Single(b => b.BestellingID == (int)cbEditBestelling.SelectedValue);
                selectedBestelling.DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate;
                selectedBestelling.PersoneelslidID = (int)cbPersoneelslidEditBestelling.SelectedValue;
                selectedBestelling.LeverancierID = (int)cbLeverancierEditBestelling.SelectedValue;
                selectedBestelling.KlantID = (int)cbKlantEditBestelling.SelectedValue;
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            EditBestelling();
        }

        private void btnDeleteBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.Bestellings.Remove(ctx.Bestellings.Single(b => b.BestellingID == (int)cbEditBestelling.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditProducten();
        }

        private void tbEditBestellingProduct_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditBestelling.IsChecked == true)
            {
                EditBestellingProduct();
                btnEditBestellingProduct.IsEnabled = true;
                btnDeleteBestellingProduct.IsEnabled = true;
                cbEditBestellingProduct.IsEnabled = true;
                btnToevoegenBestellingProduct.IsEnabled = false;
            }
            else if (tbEditProducten.IsChecked == false)
            {
                btnEditBestellingProduct.IsEnabled = false;
                btnDeleteBestellingProduct.IsEnabled = false;
                cbEditBestellingProduct.IsEnabled = false;
                btnToevoegenBestellingProduct.IsEnabled = true;
                EditBestellingProductfillCombobox();
            }

        }

        private void cbEditBestellingProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditBestellingProduct.SelectedValue != null)
                {
                    if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue) != null)
                    {
                        var selectedBestellingProduct = ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue);
                        cbBestellingenEditBestellingProduct.SelectedValue = selectedBestellingProduct.BestellingID;
                        cbProductenEditBestellingProduct.SelectedValue = selectedBestellingProduct.ProductID;
                        
                    }
                }
            }
        }

        private void btnToevoegenBestellingProduct_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.BestellingProducts.Add(new BestellingProduct
                {
                    BestellingID = (int)cbBestellingenEditBestellingProduct.SelectedValue,
                    ProductID = (int)cbProductenEditBestellingProduct.SelectedValue,
                    AantalProtuctBesteld = Convert.ToInt32(nudAantalProductenBesteld.Text)

                });
                if (ctx.Bestellings.Single(b=>b.BestellingID == (int)cbBestellingenEditBestellingProduct.SelectedValue).LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == (int)cbProductenEditBestellingProduct.SelectedValue).AantalOpVooraad -= Convert.ToInt32(nudAantalProductenBesteld.Text); }
                else if (ctx.Bestellings.Single(b => b.BestellingID == (int)cbBestellingenEditBestellingProduct.SelectedValue).KlantID == null)
                { ctx.Products.Single(p => p.ProductID == (int)cbProductenEditBestellingProduct.SelectedValue).AantalOpVooraad += Convert.ToInt32(nudAantalProductenBesteld.Text); }

                ctx.SaveChanges();
            }
            MessageBox.Show("Toevoegen");
            EditBestellingProduct();
        }

        private void btnEditBestellingProduct_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedBestelling = ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue);
                if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).Bestelling.LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad += selectedBestelling.AantalProtuctBesteld - Convert.ToInt32(nudAantalProductenBesteld.Text); }
                else if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).Bestelling.KlantID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad -= selectedBestelling.AantalProtuctBesteld - Convert.ToInt32(nudAantalProductenBesteld.Text); }

                selectedBestelling.BestellingID = (int)cbBestellingenEditBestellingProduct.SelectedValue;
                selectedBestelling.ProductID = (int)cbProductenEditBestellingProduct.SelectedValue;
                selectedBestelling.AantalProtuctBesteld = Convert.ToInt32(nudAantalProductenBesteld.Text);
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            EditBestellingProduct();
        }

        private void btnDeleteBestellingProduct_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).Bestelling.LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad += ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).AantalProtuctBesteld; }
                else if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).Bestelling.KlantID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad -= ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).AantalProtuctBesteld; }

                
                ctx.BestellingProducts.Remove(ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditProducten();
        }
        private void btnNudAantalProductenBesteldUp_Click(object sender, RoutedEventArgs e)
        {
            decimal getal = Convert.ToDecimal(nudAantalProductenBesteld.Text);
            getal++;
            nudAantalProductenBesteld.Text = getal.ToString();
        }

        private void btnNudProductenBesteldDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDecimal(nudAantalProductenBesteld.Text) > 0)
                nudAantalProductenBesteld.Text = $"{Convert.ToDecimal(nudAantalProductenBesteld.Text) - 1}";
        }
        private void btnJsonTemplateCreate_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var producten = ctx.Products.Select(w => w);
                List<object> ListProducten = new List<object>();
                foreach (var item in producten)
                {
                   
                    Product productsForList = new Product() { ProductID = item.ProductID,Naam = item.Naam,Marge = item.Marge,Eenheid = item.Eenheid,BTW = item.BTW, LeverancierID = item.LeverancierID, CategorieID = item.CategorieID, AantalOpVooraad = item.AantalOpVooraad, AantalNaBesteld = item.AantalNaBesteld, AantalBesteld = item.AantalBesteld, AantalBeschikbaar = item.AantalBeschikbaar };
                    ListProducten.Add(productsForList);
                }
                JsonCreate(ListProducten);
                MessageBox.Show("Template created");
            }
        }

        private void btnEditProductenMetJson_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var json = File.ReadAllText("gegevens.Json");
                List<Product> listProducten = JsonConvert.DeserializeObject<List<Product>>(json);
                foreach (var item in listProducten)
                {
                    var selectedProduct = ctx.Products.Single(p => p.ProductID == item.ProductID);
                    selectedProduct.Naam = item.Naam;
                    selectedProduct.Marge = item.Marge;
                    selectedProduct.Eenheid = item.Eenheid;
                    selectedProduct.BTW = item.BTW;
                    selectedProduct.LeverancierID = item.LeverancierID;
                    selectedProduct.CategorieID = item.CategorieID;
                    selectedProduct.AantalOpVooraad = item.AantalOpVooraad;
                    ctx.SaveChanges();
                }
                MessageBox.Show("Edited");
                EditProducten();
            }           
        }
        public void JsonCreate(List<object> listObject)
        {
            if (!File.Exists("gegevens.Json"))
            {
                using (FileStream fs = File.Create("gegevens.Json"))
                {
                }
            }

            var jsonString = JsonConvert.SerializeObject(listObject, Formatting.Indented);
            File.WriteAllText("gegevens.Json", jsonString);
            MessageBox.Show(jsonString.ToString());
        }
    }
}
