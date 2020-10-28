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
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

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
            EditLeverancier();
            EditProductenfillCombobox();
            EditBestellingfillCombobox();
            EditBestellingProductfillCombobox();
            
        }
        public Personeelslid Selected { get; set; }
        public Databeheer(Personeelslid selected)
        {
            Selected = selected;
            InitializeComponent();
            EditAdminrechten();
            EditPersoneel();
            EditCategorie();
            EditKlant();
            EditLeverancier();
            EditProductenfillCombobox();
            EditBestellingfillCombobox();
            EditBestellingProductfillCombobox();
            if (selected.AdminRechtenID == 1)
            {
                tabPersoneellid.IsEnabled = true;
                tabAdminRechten.IsEnabled = true;
                tabCategorie.IsEnabled = true;
                tabKlanten.IsEnabled = true;
                tabLeverancier.IsEnabled = true;
                tabProducten.IsEnabled = true;
                tabBestellingen.IsEnabled = true;
                tabBestellingProducten.IsEnabled = true;
                tabJsonProducten.IsEnabled = true;
            }
            else if (selected.AdminRechtenID == 2)
            {
                TabDatabeheer.SelectedItem = tabProducten;
                tabPersoneellid.IsEnabled = false;
                tabAdminRechten.IsEnabled = false;
                tabCategorie.IsEnabled = true;
                tabKlanten.IsEnabled = false;
                tabLeverancier.IsEnabled = true;
                tabProducten.IsEnabled = true;
                tabBestellingen.IsEnabled = false;
                tabBestellingProducten.IsEnabled = false;
                tabJsonProducten.IsEnabled = false;
            }
            else if (selected.AdminRechtenID == 3)
            {
                TabDatabeheer.SelectedItem = tabKlanten;
                tabPersoneellid.IsEnabled = false;
                tabAdminRechten.IsEnabled = false;
                tabCategorie.IsEnabled = false;
                tabKlanten.IsEnabled = true;
                tabLeverancier.IsEnabled = false;
                tabProducten.IsEnabled = false;
                tabBestellingen.IsEnabled = false;
                tabBestellingProducten.IsEnabled = false;
                tabJsonProducten.IsEnabled = false;
            }

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
                cbJsonLeveranciers.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbEditLeverancier.DisplayMemberPath = "Contactpersoon";
                cbEditLeverancier.SelectedValuePath = "LeverancierID";
                cbEditLeverancier.ItemsSource = listLeverancier;
                cbEditLeverancier.SelectedIndex = 0;
                cbJsonLeveranciers.DisplayMemberPath = "Contactpersoon";
                cbJsonLeveranciers.SelectedValuePath = "LeverancierID";
                cbJsonLeveranciers.ItemsSource = listLeverancier;
                cbJsonLeveranciers.SelectedIndex = 0;
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
                    x.Leverancier.LeverancierID +
                    " " +
                    x.Leverancier.Contactpersoon +
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
                passWordHint.Text = "Als de passwordbox leeg is behoud de user het dezelfde wachtwoord";
                txtVoornaamPersoneellidEditWordHint.Visibility = Visibility.Hidden;
                txtAchternaamPersoneellidEditWordHint.Visibility = Visibility.Hidden;
                UsernameWordHint.Visibility = Visibility.Hidden;
            }
            else if (tbPersoneellidEdit.IsChecked == false)
            {
                btnEditPersoneellid.IsEnabled = false;
                btnDeletePersoneellid.IsEnabled = false;
                cbEditPersoneellid.IsEnabled = false;
                btnToevoegenPersoneellid.IsEnabled = true;
                dtIndiensttredingPersoneellidEdit.DisplayDate = DateTime.Now;
                dtGeboortedatumPersoneellidEdit.DisplayDate = DateTime.Now;
                dtIndiensttredingPersoneellidEdit.SelectedDate = DateTime.Now;
                dtGeboortedatumPersoneellidEdit.SelectedDate = DateTime.Now;
                passWordHint.Text = "Wachtwoord";
                ResetTextBoxPersoneel();
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
                ResetTextBoxPersoneel();
            }
            //}
            // else
            //{
            //error endings
            //}
        }
        private void ResetTextBoxPersoneel()
        {
            txtVoornaamPersoneellidEdit.Text = "";
            txtAchternaamPersoneellidEdit.Text = "";
            txtUsernamePersoneellidEdit.Text = "";
            txtPasswordPersoneellidEdit.Password = "";
            txtVoornaamPersoneellidEditWordHint.Visibility = Visibility.Visible;
            txtAchternaamPersoneellidEditWordHint.Visibility = Visibility.Visible;
            UsernameWordHint.Visibility = Visibility.Visible;
            passWordHint.Visibility = Visibility.Visible;
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
            txtAdminRechtenToevoegen.Text = "";
            txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Visible;
        }
        private void btnEditAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten.SelectedValue).titel = txtAdminRechtenToevoegen.Text;
                //ctx.SaveChanges();
            }
            EditAdminrechten();
            txtAdminRechtenToevoegen.Text = "";
            txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Visible;
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
            txtCategorieToevoegen.Text = "";
            txtCategorieToevoegenWordHint.Visibility = Visibility.Visible;
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
            txtCategorieToevoegen.Text = "";
            txtCategorieToevoegenWordHint.Visibility = Visibility.Visible;
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
                txtVoornaamEditKlantWordHint.Visibility = Visibility.Hidden;
                txtAchternaamEditKlantWordHint.Visibility = Visibility.Hidden;
                txtStraatnaamEditKlantWordHint.Visibility = Visibility.Hidden;
                txtHuisnummerEditKlantWordHint.Visibility = Visibility.Hidden;
                if (txtBusEditKlant.Text != "") txtBusEditKlantWordHint.Visibility = Visibility.Hidden;
                txtPostcodeEditKlantWordHint.Visibility = Visibility.Hidden;
                txtGemeenteEditKlantWordHint.Visibility = Visibility.Hidden;
                txtTelefoonnummerEditKlantWordHint.Visibility = Visibility.Hidden;
                txtEmailadresEditKlantWordHint.Visibility = Visibility.Hidden;
                txtOpmerkingEditKlantWordHint.Visibility = Visibility.Hidden;
            }
            else if (tbEditKlant.IsChecked == false)
            {
                btnEditKlant.IsEnabled = false;
                btnDeleteKlant.IsEnabled = false;
                cbEditKlant.IsEnabled = false;
                btnToevoegenKlant.IsEnabled = true;
                ReloadTextBoxKlant();

            }

        }
        private void ReloadTextBoxKlant()
        {
            txtVoornaamEditKlant.Text = "";
            txtAchternaamEditKlant.Text = "";
            txtStraatnaamEditKlant.Text = "";
            txtHuisnummerEditKlant.Text = "";
            txtBusEditKlant.Text = "";
            txtPostcodeEditKlant.Text = "";
            txtGemeenteEditKlant.Text = "";
            txtTelefoonnummerEditKlant.Text = "";
            txtEmailadresEditKlant.Text = "";
            txtOpmerkingEditKlant.Text = "";            
            txtVoornaamEditKlantWordHint.Visibility = Visibility.Visible;
            txtAchternaamEditKlantWordHint.Visibility = Visibility.Visible;
            txtStraatnaamEditKlantWordHint.Visibility = Visibility.Visible;
            txtHuisnummerEditKlantWordHint.Visibility = Visibility.Visible;
            txtBusEditKlantWordHint.Visibility = Visibility.Visible;
            txtPostcodeEditKlantWordHint.Visibility = Visibility.Visible;
            txtGemeenteEditKlantWordHint.Visibility = Visibility.Visible;
            txtTelefoonnummerEditKlantWordHint.Visibility = Visibility.Visible;
            txtEmailadresEditKlantWordHint.Visibility = Visibility.Visible;
            txtOpmerkingEditKlantWordHint.Visibility = Visibility.Visible;
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
                ReloadTextBoxKlant();
                if (Selected == null)
                {
                    this.DialogResult = true;
                }
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
                        if(selectedKlant.Bus == "") { txtBusEditKlantWordHint.Visibility = Visibility.Visible; }
                        else {
                            txtBusEditKlantWordHint.Visibility = Visibility.Hidden;
                            txtBusEditKlant.Text = selectedKlant.Bus; 
                        }                       
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
                txtContactpersoonEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtTelefoonnummerEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtEmailadresEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtStraatnaamEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtHuisnummerEditLeverancierWordHint.Visibility = Visibility.Hidden;
                if (txtBusEditLeverancier.Text != "") txtBusEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtPostcodeEditLeverancierWordHint.Visibility = Visibility.Hidden;
                txtGemeenteEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
            else if (tbEditLeverancier.IsChecked == false)
            {
                btnEditLeverancier.IsEnabled = false;
                btnDeleteLeverancier.IsEnabled = false;
                cbEditLeverancier.IsEnabled = false;
                btnToevoegenLeverancier.IsEnabled = true;
                ReloadTextBoxLeverancier();
            }
        }
        private void ReloadTextBoxLeverancier()
        {
            txtContactpersoonEditLeverancier.Text = "";
            txtTelefoonnummerEditLeverancier.Text = "";
            txtEmailadresEditLeverancier.Text = "";
            txtStraatnaamEditLeverancier.Text = "";
            txtHuisnummerEditLeverancier.Text = "";
            txtBusEditLeverancier.Text = "";
            txtPostcodeEditLeverancier.Text = "";
            txtGemeenteEditLeverancier.Text = "";
            txtContactpersoonEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtTelefoonnummerEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtEmailadresEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtStraatnaamEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtHuisnummerEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtBusEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtPostcodeEditLeverancierWordHint.Visibility = Visibility.Visible;
            txtGemeenteEditLeverancierWordHint.Visibility = Visibility.Visible;
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
            ReloadTextBoxLeverancier();
            //cbEditLeverancier.SelectedIndex = cbEditLeverancier.Items.Count - 1;
            if (Selected == null)
            {
                this.DialogResult = true;
            }
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
                        if (selectedPersoneel.Bus == "") { txtBusEditLeverancierWordHint.Visibility = Visibility.Visible; }
                        else
                        {
                            txtBusEditLeverancierWordHint.Visibility = Visibility.Hidden;
                            txtBusEditLeverancier.Text = selectedPersoneel.Bus;
                        }                      
                        txtPostcodeEditLeverancier.Text = selectedPersoneel.Postcode;
                        txtGemeenteEditLeverancier.Text = selectedPersoneel.Gemeente;
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
                txtNaamEditProductenWordHint.Visibility = Visibility.Hidden;
                txtMargeEditProductenWordHint.Visibility = Visibility.Hidden;
                txtEenheidEditProductenWordHint.Visibility = Visibility.Hidden;
                txtBTWEditProductenWordHint.Visibility = Visibility.Hidden;
            }
            else if (tbEditProducten.IsChecked == false)
            {
                btnEditProducten.IsEnabled = false;
                btnDeleteProducten.IsEnabled = false;
                cbEditProducten.IsEnabled = false;
                btnToevoegenProducten.IsEnabled = true;
                
                EditProductenfillCombobox();
                ReloadTextBoxProducten();
            }

        }
        private void ReloadTextBoxProducten()
        {
            txtNaamEditProducten.Text = "";
            txtMargeEditProducten.Text = "";
            txtEenheidEditProducten.Text = "";
            txtBTWEditProducten.Text = "";
            nudAantalOpVooraadProducten.Text = "0";           
            nudAankoopPrijs.Text = "0.00";
            txtNaamEditProductenWordHint.Visibility = Visibility.Visible;
            txtMargeEditProductenWordHint.Visibility = Visibility.Visible;
            txtEenheidEditProductenWordHint.Visibility = Visibility.Visible;
            txtBTWEditProductenWordHint.Visibility = Visibility.Visible;
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
                        nudAankoopPrijs.Text = selectedProduct.AankoopPrijs.ToString();
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
        private static readonly Regex _regex2 = new Regex("[^0-9,]+"); //regex that matches disallowed text
        private static bool IsTextAllowed2(string text)
        {
            return !_regex2.IsMatch(text);
        }
        private void nudAankoopPrijs_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed2(e.Text);
        }

        private void nudAankoopPrijs_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed2(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
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
                    AantalOpVooraad = Convert.ToInt32(nudAantalOpVooraadProducten.Text),
                    AankoopPrijs = Convert.ToDecimal(nudAankoopPrijs.Text)
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
                selectedProduct.AankoopPrijs = Convert.ToDecimal(nudAankoopPrijs.Text);
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
                nudAantalProductenBesteld.Text = "0";
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
        public class JsonProductenLeverancier
        {  
            public int ProductID { get; set; }
            public string Naam { get; set; }
            public string Eenheid { get; set; }
            public decimal BTW { get; set; }
            public decimal AankoopPrijs { get; set; }

            public JsonProductenLeverancier(int productID, string naam, string eenheid, decimal bTW, decimal aankoopPrijs)
            {
                ProductID = productID;
                Naam = naam;
                Eenheid = eenheid;
                BTW = bTW;
                AankoopPrijs = aankoopPrijs;
            }
        }
        private void btnJsonTemplateCreate_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var producten = ctx.Products.Where(p=>p.LeverancierID == (int)cbJsonLeveranciers.SelectedValue).Select(p => p);
                List<object> ListProducten = new List<object>();
                foreach (var item in producten)
                {

                    JsonProductenLeverancier productsForList = new JsonProductenLeverancier(item.ProductID,item.Naam,item.Eenheid,item.BTW,item.AankoopPrijs);
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
                List<JsonProductenLeverancier> listProducten = JsonConvert.DeserializeObject<List<JsonProductenLeverancier>>(json);
                foreach (var item in listProducten)
                {
                    var selectedProduct = ctx.Products.Single(p => p.ProductID == item.ProductID);
                    selectedProduct.Naam = item.Naam;                    
                    selectedProduct.Eenheid = item.Eenheid;
                    selectedProduct.BTW = item.BTW;
                    selectedProduct.AankoopPrijs = item.AankoopPrijs;
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
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                string uri = $"mailto:{ctx.Leveranciers.Where(l=>l.LeverancierID==(int)cbJsonLeveranciers.SelectedValue).Select(l=>l.Emailadres).FirstOrDefault()}?subject=Gegevens Producten&body={jsonString.ToString()}";
                Uri myUri = new Uri(uri);
                Process.Start(myUri.AbsoluteUri);
            }
        }

        private void txtVoornaamPersoneellidEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtVoornaamPersoneellidEdit.Text.Length == 0)
            {
                txtVoornaamPersoneellidEditWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtVoornaamPersoneellidEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtVoornaamPersoneellidEdit.Text.Length == 0)
            {
                txtVoornaamPersoneellidEditWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtAchternaamPersoneellidEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAchternaamPersoneellidEdit.Text.Length == 0)
            {
                txtAchternaamPersoneellidEditWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtAchternaamPersoneellidEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAchternaamPersoneellidEdit.Text.Length == 0)
            {
                txtAchternaamPersoneellidEditWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtUsernamePersoneellidEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsernamePersoneellidEdit.Text.Length == 0)
            {
                UsernameWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtUsernamePersoneellidEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsernamePersoneellidEdit.Text.Length == 0)
            {
                UsernameWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtPasswordPersoneellidEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPasswordPersoneellidEdit.Password.Length == 0)
            {
                passWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtPasswordPersoneellidEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtPasswordPersoneellidEdit.Password.Length == 0)
            {
                passWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtAdminRechtenToevoegen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAdminRechtenToevoegen.Text.Length == 0)
            {
                txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtAdminRechtenToevoegen_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAdminRechtenToevoegen.Text.Length == 0)
            {
                txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtCategorieToevoegen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCategorieToevoegen.Text.Length == 0)
            {
                txtCategorieToevoegenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtCategorieToevoegen_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCategorieToevoegen.Text.Length == 0)
            {
                txtCategorieToevoegenWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtVoornaamEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtVoornaamEditKlant.Text.Length == 0)
            {
                txtVoornaamEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtVoornaamEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtVoornaamEditKlant.Text.Length == 0)
            {
                txtVoornaamEditKlantWordHint.Visibility = Visibility.Hidden;

            }
        }

        private void txtAchternaamEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAchternaamEditKlant.Text.Length == 0)
            {
                txtAchternaamEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtAchternaamEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAchternaamEditKlant.Text.Length == 0)
            {
                txtAchternaamEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtStraatnaamEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtStraatnaamEditKlant.Text.Length == 0)
            {
                txtStraatnaamEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtStraatnaamEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtStraatnaamEditKlant.Text.Length == 0)
            {
                txtStraatnaamEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtHuisnummerEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtHuisnummerEditKlant.Text.Length == 0)
            {
                txtHuisnummerEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtHuisnummerEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtHuisnummerEditKlant.Text.Length == 0)
            {
                txtHuisnummerEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtBusEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusEditKlant.Text.Length == 0)
            {
                txtBusEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtBusEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusEditKlant.Text.Length == 0)
            {
                txtBusEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtGemeenteEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtGemeenteEditKlant.Text.Length == 0)
            {
                txtGemeenteEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtGemeenteEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtGemeenteEditKlant.Text.Length == 0)
            {
                txtGemeenteEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtPostcodeEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPostcodeEditKlant.Text.Length == 0)
            {
                txtPostcodeEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtPostcodeEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPostcodeEditKlant.Text.Length == 0)
            {
                txtPostcodeEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtTelefoonnummerEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefoonnummerEditKlant.Text.Length == 0)
            {
                txtTelefoonnummerEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtTelefoonnummerEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefoonnummerEditKlant.Text.Length == 0)
            {
                txtTelefoonnummerEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtEmailadresEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmailadresEditKlant.Text.Length == 0)
            {
                txtEmailadresEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtEmailadresEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmailadresEditKlant.Text.Length == 0)
            {
                txtEmailadresEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtOpmerkingEditKlant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtOpmerkingEditKlant.Text.Length == 0)
            {
                txtOpmerkingEditKlantWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtOpmerkingEditKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtOpmerkingEditKlant.Text.Length == 0)
            {
                txtOpmerkingEditKlantWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtContactpersoonEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtContactpersoonEditLeverancier.Text.Length == 0)
            {
                txtContactpersoonEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtContactpersoonEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtContactpersoonEditLeverancier.Text.Length == 0)
            {
                txtContactpersoonEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtTelefoonnummerEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefoonnummerEditLeverancier.Text.Length == 0)
            {
                txtTelefoonnummerEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtTelefoonnummerEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefoonnummerEditLeverancier.Text.Length == 0)
            {
                txtTelefoonnummerEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtEmailadresEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmailadresEditLeverancier.Text.Length == 0)
            {
                txtEmailadresEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtEmailadresEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmailadresEditLeverancier.Text.Length == 0)
            {
                txtEmailadresEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtStraatnaamEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtStraatnaamEditLeverancier.Text.Length == 0)
            {
                txtStraatnaamEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtStraatnaamEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtStraatnaamEditLeverancier.Text.Length == 0)
            {
                txtStraatnaamEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtHuisnummerEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtHuisnummerEditLeverancier.Text.Length == 0)
            {
                txtHuisnummerEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtHuisnummerEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtHuisnummerEditLeverancier.Text.Length == 0)
            {
                txtHuisnummerEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtBusEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusEditLeverancier.Text.Length == 0)
            {
                txtBusEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtBusEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusEditLeverancier.Text.Length == 0)
            {
                txtBusEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }
        private void txtGemeenteEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtGemeenteEditLeverancier.Text.Length == 0)
            {
                txtGemeenteEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtGemeenteEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtGemeenteEditLeverancier.Text.Length == 0)
            {
                txtGemeenteEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtPostcodeEditLeverancier_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPostcodeEditLeverancier.Text.Length == 0)
            {
                txtPostcodeEditLeverancierWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtPostcodeEditLeverancier_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPostcodeEditLeverancier.Text.Length == 0)
            {
                txtPostcodeEditLeverancierWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtNaamEditProducten_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNaamEditProducten.Text.Length == 0)
            {
                txtNaamEditProductenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtNaamEditProducten_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNaamEditProducten.Text.Length == 0)
            {
                txtNaamEditProductenWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtMargeEditProducten_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMargeEditProducten.Text.Length == 0)
            {
                txtMargeEditProductenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtMargeEditProducten_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMargeEditProducten.Text.Length == 0)
            {
                txtMargeEditProductenWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtEenheidEditProducten_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEenheidEditProducten.Text.Length == 0)
            {
                txtEenheidEditProductenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtEenheidEditProducten_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEenheidEditProducten.Text.Length == 0)
            {
                txtEenheidEditProductenWordHint.Visibility = Visibility.Hidden;
            }
        }

        private void txtBTWEditProducten_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBTWEditProducten.Text.Length == 0)
            {
                txtBTWEditProductenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtBTWEditProducten_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBTWEditProducten.Text.Length == 0)
            {
                txtBTWEditProductenWordHint.Visibility = Visibility.Hidden;
            }
        }

      
    }
}
