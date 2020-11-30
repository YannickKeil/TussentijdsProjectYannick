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
            EditBestellingProductfillListbox();
            //tbSwitching();
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
            //EditBestellingProductfillListbox();
            //tbSwitching();
            ReloadTextBoxKlant();
            ReloadTextBoxLeverancier();
            ReloadTextBoxProducten();
            ResetTextBoxPersoneel();
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
                tabBestellingen.IsEnabled = true;
                tabBestellingProducten.IsEnabled = true;
                tabJsonProducten.IsEnabled = false;
                tbEditBestellingKlantLeverancier.IsChecked = true;
                tbEditBestellingKlantLeverancier.Visibility = Visibility.Hidden;
                tbEditBestellingProductKlantLeverancier.IsChecked = true;
                tbEditBestellingProductKlantLeverancier.Visibility = Visibility.Hidden;
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
                tabBestellingen.IsEnabled = true;
                tabBestellingProducten.IsEnabled = true;
                tabJsonProducten.IsEnabled = false;
                tbEditBestellingKlantLeverancier.IsChecked = false;
                tbEditBestellingKlantLeverancier.Visibility = Visibility.Hidden;
                tbEditBestellingProductKlantLeverancier.IsChecked = false;
                tbEditBestellingProductKlantLeverancier.Visibility = Visibility.Hidden;
            }

        }
        public string GetSource = $"{Directory.GetCurrentDirectory()}/cross-mark.png";

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
                EditBestellingProductfillListbox();
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
                if (tbEditBestellingKlantLeverancier.IsChecked == true)
                {
                    cbEditBestelling.ItemsSource = null;
                    var listBestellingen = ctx.Bestellings.Where(b => b.LeverancierID != null).Select(x => new
                    {
                        Naam = x.Leverancier.LeverancierID +
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
                    EditBestellingProductfillListbox();
                }
                else if (tbEditBestellingKlantLeverancier.IsChecked == false)
                {
                    cbEditBestelling.ItemsSource = null;
                    var listBestellingen = ctx.Bestellings.Where(b => b.KlantID != null).Select(x => new
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
                    EditBestellingProductfillListbox();
                }

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
                if (tbEditBestellingKlantLeverancier.IsChecked == true)
                {
                    lblCbLeverancierKlantEditBestelling.Text = "Leverancier";
                    cbLeverancierKlantEditBestelling.ItemsSource = null;
                    var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                    cbLeverancierKlantEditBestelling.DisplayMemberPath = "Contactpersoon";
                    cbLeverancierKlantEditBestelling.SelectedValuePath = "LeverancierID";
                    cbLeverancierKlantEditBestelling.ItemsSource = listLeverancier;
                    cbLeverancierKlantEditBestelling.SelectedIndex = 0;
                }
                else if (tbEditBestellingKlantLeverancier.IsChecked == false)
                {
                    lblCbLeverancierKlantEditBestelling.Text = "Klant";
                    cbLeverancierKlantEditBestelling.ItemsSource = null;
                    var listKlant = ctx.Klants.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                    cbLeverancierKlantEditBestelling.DisplayMemberPath = "Naam";
                    cbLeverancierKlantEditBestelling.SelectedValuePath = "Id";
                    cbLeverancierKlantEditBestelling.ItemsSource = listKlant;
                    cbLeverancierKlantEditBestelling.SelectedIndex = 0;
                }

            }
        }
        private void EditBestellingProduct()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (tbEditBestellingProductKlantLeverancier.IsChecked == true)
                {
                    cbEditBestellingProduct.ItemsSource = null;
                    var listBestellingProducten = ctx.BestellingProducts.Where(x => x.Bestelling.LeverancierID != null).Select(x => new
                    {
                        Naam = x.BestellingID +
                          " " +
                          x.Bestelling.Leverancier.LeverancierID +
                          " " +
                          x.Bestelling.Leverancier.Contactpersoon +
                          " " +
                          x.Bestelling.DatumOpgemaakt
                          //" " +
                          //x.Product.Naam +
                          //" " +
                          //x.Product.AantalOpVooraad+
                          //" "+
                          //x.Product.Eenheid
                          ,
                        Id = x.BestellingID
                    }).ToList();

                    cbEditBestellingProduct.DisplayMemberPath = "Naam";
                    cbEditBestellingProduct.SelectedValuePath = "Id";
                    cbEditBestellingProduct.ItemsSource = listBestellingProducten;
                    cbEditBestellingProduct.SelectedIndex = 0;
                }
                else if (tbEditBestellingProductKlantLeverancier.IsChecked == false)
                {
                    var listBestellingProducten = ctx.BestellingProducts.Where(x => x.Bestelling.KlantID != null).Select(x => new
                    {
                        Naam = x.BestellingID +
                        " " +
                        x.Bestelling.Klant.KlantID +
                          " " +
                          x.Bestelling.Klant.Voornaam +
                          " " +
                          x.Bestelling.Klant.Achternaam +
                          " " +
                          x.Bestelling.DatumOpgemaakt
                          //" " +
                          //x.Product.Naam +
                          //" " +
                          //x.Product.Eenheid
                          ,
                        Id = x.BestellingID
                    }).ToList();

                    cbEditBestellingProduct.DisplayMemberPath = "Naam";
                    cbEditBestellingProduct.SelectedValuePath = "Id";
                    cbEditBestellingProduct.ItemsSource = listBestellingProducten;
                    cbEditBestellingProduct.SelectedIndex = 0;
                }

            }
        }
        private void EditBestellingProductfillListbox()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (cbEditBestellingProduct.SelectedValue != null)

                {
                    if (tbEditBestellingProductKlantLeverancier.IsChecked == true)
                    {
                        if (ctx.BestellingProducts.Where(x => x.Bestelling.LeverancierID == (int)cbEditBestellingProduct.SelectedValue) != null)
                        {
                            lbBestellingenEditBestellingProduct.ItemsSource = null;
                            var listBestellingen = ctx.BestellingProducts.Where(x => x.BestellingID == (int)cbEditBestellingProduct.SelectedValue).Select(x => new
                            {
                                Naam = x.Product.Naam,
                                Vooraad = x.Product.AantalOpVooraad,
                                Eenheid = x.Product.Eenheid,
                                Prijs = x.Product.AankoopPrijs,
                                Id = x.BestellingProductID
                            }).ToList();
                            //MessageBox.Show("test Leverancier");
                            lbBestellingenEditBestellingProduct.SelectedValuePath = "Id";
                            lbBestellingenEditBestellingProduct.ItemsSource = listBestellingen;
                            cbProductenEditBestellingProduct.ItemsSource = null;
                            var listProducten = ctx.Products.Select(x => x).ToList();
                            cbProductenEditBestellingProduct.DisplayMemberPath = "Naam";
                            cbProductenEditBestellingProduct.SelectedValuePath = "ProductID";
                            cbProductenEditBestellingProduct.ItemsSource = listProducten;
                        }
                        cbProductenEditBestellingProduct.SelectedIndex = 0;
                    }
                    else if (tbEditBestellingProductKlantLeverancier.IsChecked == false)
                    {
                        if (ctx.BestellingProducts.Where(x => x.BestellingID == (int)cbEditBestellingProduct.SelectedValue) != null)
                        {
                            lbBestellingenEditBestellingProduct.ItemsSource = null;
                            var listBestellingen = ctx.BestellingProducts.Where(x => x.BestellingID == (int)cbEditBestellingProduct.SelectedValue).Select(x => new
                            {
                                Naam = x.Product.Naam,
                                Vooraad = x.Product.AantalOpVooraad,
                                Eenheid = x.Product.Eenheid,
                                Prijs = x.Product.AankoopPrijs,
                                Id = x.BestellingProductID
                            }).ToList();
                            //MessageBox.Show("test klant");
                            lbBestellingenEditBestellingProduct.SelectedValuePath = "Id";
                            lbBestellingenEditBestellingProduct.ItemsSource = listBestellingen;

                            cbProductenEditBestellingProduct.ItemsSource = null;
                            var listProducten = ctx.Products.Select(x => x).ToList();
                            cbProductenEditBestellingProduct.DisplayMemberPath = "Naam";
                            cbProductenEditBestellingProduct.SelectedValuePath = "ProductID";
                            cbProductenEditBestellingProduct.ItemsSource = listProducten;
                            cbProductenEditBestellingProduct.SelectedIndex = 0;
                        }
                    }


                }

                else
                {
                    lbBestellingenEditBestellingProduct.ItemsSource = null;
                    cbProductenEditBestellingProduct.ItemsSource = null;
                    var listProducten = ctx.Products.Select(x => x).ToList();
                    cbProductenEditBestellingProduct.DisplayMemberPath = "Naam";
                    cbProductenEditBestellingProduct.SelectedValuePath = "ProductID";
                    cbProductenEditBestellingProduct.ItemsSource = listProducten;
                    cbProductenEditBestellingProduct.SelectedIndex = 0;
                }
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
            var password = txtPasswordPersoneellidEdit.Password;
            bool checks = true;
            string errorPassword = "Het wachtwoord voldoet niet aan de regels.";
            string errorUsername = "";
            if (!password.Any(char.IsUpper))
            {
                errorPassword += "Het bevat geen hoofdletters.";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (!password.Any(char.IsLower))
            {
                errorPassword += " Het bevat geen kleine letters.";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (!password.Any(char.IsDigit))
            {
                errorPassword += " Het bevat geen cijfers.";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (password.Length < 8)
            {
                errorPassword += " Het is te kort";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (password.Length > 20)
            {
                errorPassword += " Het is te lang";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            {
                errorPassword += " Het bevat geen vreemd teken";
                checks = false;
                txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (txtVoornaamPersoneellidEdit.Text == "")
            {
                txtVoornaamPersoneellidEdit.ToolTip = "Voornaam mag niet leeg zijn";
                checks = false;
                imgVoornaamPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (txtAchternaamPersoneellidEdit.Text == "")
            {
                txtAchternaamPersoneellidEdit.ToolTip = "Achternaam mag niet leeg zijn";
                checks = false;
                imgAchternaamPersoneellidEdit.Visibility = Visibility.Visible;
            }
            if (txtUsernamePersoneellidEdit.Text == "")
            {
                checks = false;
                errorUsername += "Username mag niet leeg zijn";
                txtUsernamePersoneellidEdit.ToolTip = errorUsername;
                imgUsernamePersoneellidEdit.Visibility = Visibility.Visible;
            }
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (ctx.Personeelslids.Where(p => p.Username == txtUsernamePersoneellidEdit.Text).Any())
                {
                    checks = false;
                    errorUsername += "het moet een unique Username zijn.";
                    txtUsernamePersoneellidEdit.ToolTip = errorUsername;
                    imgUsernamePersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (checks)
                {
                    imgPasswordPersoneellidEdit.Visibility =
                    imgVoornaamPersoneellidEdit.Visibility =
                    imgAchternaamPersoneellidEdit.Visibility =
                    imgUsernamePersoneellidEdit.Visibility = Visibility.Hidden;

                    txtPasswordPersoneellidEdit.ToolTip = "wachtwoord voorwaarden Minstens 1 hoofdletter Minstens 1 kleine letter Minstens 1 cijfer Minstens 1 vreemd teken Een lengte van 8 - 20 characters";
                    txtVoornaamPersoneellidEdit.ToolTip = "";
                    txtAchternaamPersoneellidEdit.ToolTip = "";
                    txtUsernamePersoneellidEdit.ToolTip = "";
                    var salt = CreateSalt();
                    var hash = HashPassword(password, salt);

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
                var password = txtPasswordPersoneellidEdit.Password;
                bool checks = true;
                string errorPassword = "Het wachtwoord voldoet niet aan de regels.";
                string errorUsername = "";
                if (!password.Any(char.IsUpper))
                {
                    errorPassword += "Het bevat geen hoofdletters.";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (!password.Any(char.IsLower))
                {
                    errorPassword += " Het bevat geen kleine letters.";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (!password.Any(char.IsDigit))
                {
                    errorPassword += " Het bevat geen cijfers.";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (password.Length < 8)
                {
                    errorPassword += " Het is te kort";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (password.Length > 20)
                {
                    errorPassword += " Het is te lang";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
                {
                    errorPassword += " Het bevat geen vreemd teken";
                    checks = false;
                    txtPasswordPersoneellidEdit.ToolTip = errorPassword;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (password.Length == 0)
                {
                    checks = true;
                    imgPasswordPersoneellidEdit.Visibility = Visibility.Hidden;
                }
                if (txtVoornaamPersoneellidEdit.Text == "")
                {
                    txtVoornaamPersoneellidEdit.ToolTip = "Voornaam mag niet leeg zijn";
                    checks = false;
                    imgVoornaamPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (txtAchternaamPersoneellidEdit.Text == "")
                {
                    txtAchternaamPersoneellidEdit.ToolTip = "Achternaam mag niet leeg zijn";
                    checks = false;
                    imgAchternaamPersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (txtUsernamePersoneellidEdit.Text == "")
                {
                    checks = false;
                    errorUsername += "Username mag niet leeg zijn";
                    txtUsernamePersoneellidEdit.ToolTip = errorUsername;
                    imgUsernamePersoneellidEdit.Visibility = Visibility.Visible;
                }
                if (ctx.Personeelslids.Where(p => p.Username == txtUsernamePersoneellidEdit.Text).Any())
                {
                    if(ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue).Username != txtUsernamePersoneellidEdit.Text) 
                    { 
                    checks = false;
                    errorUsername += "het moet een unique Username zijn.";
                    txtUsernamePersoneellidEdit.ToolTip = errorUsername;
                    imgUsernamePersoneellidEdit.Visibility = Visibility.Visible;
                    }
                }
                if (checks)
                {
                    imgPasswordPersoneellidEdit.Visibility =
                    imgVoornaamPersoneellidEdit.Visibility =
                    imgAchternaamPersoneellidEdit.Visibility =
                    imgUsernamePersoneellidEdit.Visibility = Visibility.Hidden;
                    var selectedPersoneel = ctx.Personeelslids.Single(p => p.PersoneelslidID == (int)cbEditPersoneellid.SelectedValue);

                    selectedPersoneel.Voornaam = txtVoornaamPersoneellidEdit.Text;
                    selectedPersoneel.Achternaam = txtAchternaamPersoneellidEdit.Text;
                    selectedPersoneel.AdminRechtenID = (int)cbAdminRechtenPersoneellidEdit.SelectedValue;
                    if (txtPasswordPersoneellidEdit.Password != "")
                    {
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
            //tbSwitching();
        }

        private void btnAddAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtAdminRechtenToevoegen.Text == "")
            {
                check = false;
                txtAdminRechtenToevoegen.ToolTip = "Textbox mag niet leeg zijn";
                imgAdminRechtenToevoegen.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgAdminRechtenToevoegen.Visibility = Visibility.Hidden;

                if (MessageBox.Show("Ben je zeker dat je rechten wil toevoegen?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                    {
                        ctx.AdminRechtens.Add(new AdminRechten { titel = txtAdminRechtenToevoegen.Text });
                        ctx.SaveChanges();
                    }
                    EditAdminrechten();
                    txtAdminRechtenToevoegen.Text = "";
                    txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Visible;
                }


            }
        }
        private void btnEditAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtAdminRechtenToevoegen.Text == "")
            {
                check = false;
                txtAdminRechtenToevoegen.ToolTip = "Textbox mag niet leeg zijn";
                imgAdminRechtenToevoegen.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgAdminRechtenToevoegen.Visibility = Visibility.Hidden;
                if (MessageBox.Show("Ben je zeker dat je rechten wil editeren?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                    {
                        ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten.SelectedValue).titel = txtAdminRechtenToevoegen.Text;
                        ctx.SaveChanges();
                    }
                    EditAdminrechten();
                }
                txtAdminRechtenToevoegen.Text = "";
                txtAdminRechtenToevoegenWordHint.Visibility = Visibility.Visible;
            }
        }

        private void btnDeleteAdminRechten_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtAdminRechtenToevoegen.Text == "")
            {
                check = false;
                txtAdminRechtenToevoegen.ToolTip = "Textbox mag niet leeg zijn";
                imgAdminRechtenToevoegen.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgAdminRechtenToevoegen.Visibility = Visibility.Hidden;
                if (MessageBox.Show("Ben je zeker dat je rechten wil verwijderen.? programa gaat niet meer werken zoals het hoort.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                    {
                        ctx.AdminRechtens.Remove(ctx.AdminRechtens.Single(x => x.AdminRechtenID == (int)cbAdminRechten.SelectedValue));
                        ctx.SaveChanges();
                    }
                    EditAdminrechten();
                }
            }
        }

        private void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtCategorieToevoegen.Text == "")
            {
                check = false;
                txtCategorieToevoegen.ToolTip = "Textbox mag niet leeg zijn";
                imgCategorieToevoegen.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgUsernamePersoneellidEdit.Visibility = Visibility.Hidden;
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
        }
        private void btnEditCategorie_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtCategorieToevoegen.Text == "")
            {
                check = false;
                txtCategorieToevoegen.ToolTip = "Textbox mag niet leeg zijn";
                imgCategorieToevoegen.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgUsernamePersoneellidEdit.Visibility = Visibility.Hidden;
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
            bool check = true;
            if (txtVoornaamEditKlant.Text == "")
            {
                check = false;
                txtVoornaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgVoornaamEditKlant.Visibility = Visibility.Visible;
            }
            if (txtAchternaamEditKlant.Text == "")
            {
                check = false;
                txtAchternaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgAchternaamEditKlant.Visibility = Visibility.Visible;
            }
            if (txtStraatnaamEditKlant.Text == "")
            {
                check = false;
                txtStraatnaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgStraatnaamEditKlant.Visibility = Visibility.Visible;
            }
            if (txtHuisnummerEditKlant.Text == "")
            {
                check = false;
                txtHuisnummerEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgHuisnummerEditKlant.Visibility = Visibility.Visible;
            }
            if (txtGemeenteEditKlant.Text == "")
            {
                check = false;
                txtGemeenteEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgGemeenteEditKlant.Visibility = Visibility.Visible;
            }
             if (txtPostcodeEditKlant.Text == "")
            {
                check = false;
                txtPostcodeEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgPostcodeEditKlant.Visibility = Visibility.Visible;
            }
             if (txtTelefoonnummerEditKlant.Text == "")
            {
                check = false;
                txtTelefoonnummerEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgTelefoonnummerEditKlant.Visibility = Visibility.Visible;
            }
            if (txtEmailadresEditKlant.Text == "")
            {
                check = false;
                txtEmailadresEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgEmailadresEditKlant.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgVoornaamEditKlant.Visibility =
                imgAchternaamEditKlant.Visibility =
                imgStraatnaamEditKlant.Visibility =
                imgHuisnummerEditKlant.Visibility =
                imgGemeenteEditKlant.Visibility =
            imgPostcodeEditKlant.Visibility =
                imgTelefoonnummerEditKlant.Visibility =
                imgEmailadresEditKlant.Visibility = Visibility.Hidden;



                using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                {
                    ctx.Klants.Add(new Klant
                    {
                        Voornaam = txtVoornaamEditKlant.Text,
                        Achternaam = txtAchternaamEditKlant.Text,
                        Straatnaam = txtStraatnaamEditKlant.Text,
                        Huisnummer = Convert.ToInt32(txtHuisnummerEditKlant.Text),
                        Bus = txtBusEditKlant.Text,
                        Postcode = txtPostcodeEditKlant.Text,
                        Gemeente = txtGemeenteEditKlant.Text,
                        Telefoonnummer = txtTelefoonnummerEditKlant.Text,
                        Emailadres = txtEmailadresEditKlant.Text,
                        AangemaaktOp = DateTime.Now,
                        Opmerking = txtOpmerkingEditKlant.Text
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
                        if (selectedKlant.Bus == "") { txtBusEditKlantWordHint.Visibility = Visibility.Visible; }
                        else
                        {
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
            bool check = true;
            if (txtVoornaamEditKlant.Text == "")
            {
                check = false;
                txtVoornaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgVoornaamEditKlant.Visibility = Visibility.Visible;
            }
            if (txtAchternaamEditKlant.Text == "")
            {
                check = false;
                txtAchternaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgAchternaamEditKlant.Visibility = Visibility.Visible;
            }
             if (txtStraatnaamEditKlant.Text == "")
            {
                check = false;
                txtStraatnaamEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgStraatnaamEditKlant.Visibility = Visibility.Visible;
            }
             if (txtHuisnummerEditKlant.Text == "")
            {
                check = false;
                txtHuisnummerEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgHuisnummerEditKlant.Visibility = Visibility.Visible;
            }
            if (txtGemeenteEditKlant.Text == "")
            {
                check = false;
                txtGemeenteEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgGemeenteEditKlant.Visibility = Visibility.Visible;
            }
             if (txtPostcodeEditKlant.Text == "")
            {
                check = false;
                txtPostcodeEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgPostcodeEditKlant.Visibility = Visibility.Visible;
            }
            if (txtTelefoonnummerEditKlant.Text == "")
            {
                check = false;
                txtTelefoonnummerEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgTelefoonnummerEditKlant.Visibility = Visibility.Visible;
            }
            if (txtEmailadresEditKlant.Text == "")
            {
                check = false;
                txtEmailadresEditKlant.ToolTip = "Textbox mag niet leeg zijn";
                imgEmailadresEditKlant.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgVoornaamEditKlant.Visibility =
                imgAchternaamEditKlant.Visibility =
                imgStraatnaamEditKlant.Visibility =
                imgHuisnummerEditKlant.Visibility =
                imgGemeenteEditKlant.Visibility =
            imgPostcodeEditKlant.Visibility =
                imgTelefoonnummerEditKlant.Visibility =
                imgEmailadresEditKlant.Visibility = Visibility.Hidden;

                using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                {
                    var selectedKlant = ctx.Klants.Single(k => k.KlantID == (int)cbEditKlant.SelectedValue);
                    selectedKlant.Voornaam = txtVoornaamEditKlant.Text;
                    selectedKlant.Achternaam = txtAchternaamEditKlant.Text;
                    selectedKlant.Straatnaam = txtStraatnaamEditKlant.Text;
                    selectedKlant.Huisnummer = Convert.ToInt32(txtHuisnummerEditKlant.Text);
                    selectedKlant.Bus = txtBusEditKlant.Text;
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
            bool check = true;
            if (txtContactpersoonEditLeverancier.Text == "")
            {
                check = false;
                txtContactpersoonEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgContactpersoonEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtTelefoonnummerEditLeverancier.Text == "")
            {
                check = false;
                txtTelefoonnummerEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgTelefoonnummerEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtEmailadresEditLeverancier.Text == "")
            {
                check = false;
                txtEmailadresEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgEmailadresEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtStraatnaamEditLeverancier.Text == "")
            {
                check = false;
                txtStraatnaamEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgStraatnaamEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtHuisnummerEditLeverancier.Text == "")
            {
                check = false;
                txtHuisnummerEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgHuisnummerEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtPostcodeEditLeverancier.Text == "")
            {
                check = false;
                txtPostcodeEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgPostcodeEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtGemeenteEditLeverancier.Text == "")
            {
                check = false;
                txtGemeenteEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgGemeenteEditLeverancier.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgContactpersoonEditLeverancier.Visibility =
                imgTelefoonnummerEditLeverancier.Visibility =
                imgEmailadresEditLeverancier.Visibility =
                imgStraatnaamEditLeverancier.Visibility =
                imgHuisnummerEditLeverancier.Visibility =
            imgPostcodeEditKlant.Visibility =
                imgGemeenteEditLeverancier.Visibility = Visibility.Hidden;

                using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                {
                    ctx.Leveranciers.Add(new Leverancier()
                    {
                        Contactpersoon = txtContactpersoonEditLeverancier.Text,
                        Telefoonnummer = txtTelefoonnummerEditLeverancier.Text,
                        Emailadres = txtEmailadresEditLeverancier.Text,
                        Straatnaam = txtStraatnaamEditLeverancier.Text,
                        Huisnummer = Convert.ToInt32(txtHuisnummerEditLeverancier.Text),
                        Bus = txtBusEditLeverancier.Text,
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
            bool check = true;
            if (txtContactpersoonEditLeverancier.Text == "")
            {
                check = false;
                txtContactpersoonEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgContactpersoonEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtTelefoonnummerEditLeverancier.Text == "")
            {
                check = false;
                txtTelefoonnummerEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgTelefoonnummerEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtEmailadresEditLeverancier.Text == "")
            {
                check = false;
                txtEmailadresEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgEmailadresEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtStraatnaamEditLeverancier.Text == "")
            {
                check = false;
                txtStraatnaamEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgStraatnaamEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtHuisnummerEditLeverancier.Text == "")
            {
                check = false;
                txtHuisnummerEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgHuisnummerEditLeverancier.Visibility = Visibility.Visible;
            }
            if (txtPostcodeEditLeverancier.Text == "")
            {
                check = false;
                txtPostcodeEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgPostcodeEditLeverancier.Visibility = Visibility.Visible;
            }
             if (txtGemeenteEditLeverancier.Text == "")
            {
                check = false;
                txtGemeenteEditLeverancier.ToolTip = "Textbox mag niet leeg zijn";
                imgGemeenteEditLeverancier.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgContactpersoonEditLeverancier.Visibility =
                imgTelefoonnummerEditLeverancier.Visibility =
                imgEmailadresEditLeverancier.Visibility =
                imgStraatnaamEditLeverancier.Visibility =
                imgHuisnummerEditLeverancier.Visibility =
            imgPostcodeEditKlant.Visibility =
                imgGemeenteEditLeverancier.Visibility = Visibility.Hidden;

                using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                {
                    var selectedLeverancier = ctx.Leveranciers.Single(k => k.LeverancierID == (int)cbEditLeverancier.SelectedValue);
                    selectedLeverancier.Contactpersoon = txtContactpersoonEditLeverancier.Text;
                    selectedLeverancier.Telefoonnummer = txtTelefoonnummerEditLeverancier.Text;
                    selectedLeverancier.Emailadres = txtEmailadresEditLeverancier.Text;
                    selectedLeverancier.Straatnaam = txtStraatnaamEditLeverancier.Text;
                    selectedLeverancier.Huisnummer = Convert.ToInt32(txtHuisnummerEditLeverancier.Text);
                    selectedLeverancier.Bus = txtBusEditLeverancier.Text;
                    selectedLeverancier.Postcode = txtPostcodeEditLeverancier.Text;
                    selectedLeverancier.Gemeente = txtGemeenteEditLeverancier.Text;
                    ctx.SaveChanges();
                }
                MessageBox.Show("Edited");
                EditLeverancier();
                EditProductenfillCombobox();
            }
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
            bool check = true;
            if (txtNaamEditProducten.Text == "")
            {
                check = false;
                txtNaamEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgNaamEditProducten.Visibility = Visibility.Visible;
            }
            if (txtMargeEditProducten.Text == "")
            {
                check = false;
                txtMargeEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgMargeEditProducten.Visibility = Visibility.Visible;
            }
            if (txtEenheidEditProducten.Text == "")
            {
                check = false;
                txtEenheidEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgEenheidEditProducten.Visibility = Visibility.Visible;
            }
            if (txtBTWEditProducten.Text == "")
            {
                check = false;
                txtBTWEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgBTWEditProducten.Visibility = Visibility.Visible;
            }
            if (nudAantalOpVooraadProducten.Text == "")
            {
                check = false;
                nudAantalOpVooraadProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgAantalOpVooraadProducten.Visibility = Visibility.Visible;
            }
            if (nudAankoopPrijs.Text == "")
            {
                check = false;
                nudAankoopPrijs.ToolTip = "Textbox mag niet leeg zijn";
                imgAankoopPrijs.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgNaamEditProducten.Visibility =
                imgMargeEditProducten.Visibility =
                imgEenheidEditProducten.Visibility =
                imgAantalOpVooraadProducten.Visibility =
                imgAankoopPrijs.Visibility =
                imgBTWEditProducten.Visibility = Visibility.Hidden;
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

        }

        private void btnEditProducten_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            if (txtNaamEditProducten.Text == "")
            {
                check = false;
                txtNaamEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgNaamEditProducten.Visibility = Visibility.Visible;
            }
            if (txtMargeEditProducten.Text == "")
            {
                check = false;
                txtMargeEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgMargeEditProducten.Visibility = Visibility.Visible;
            }
            if (txtEenheidEditProducten.Text == "")
            {
                check = false;
                txtEenheidEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgEenheidEditProducten.Visibility = Visibility.Visible;
            }
            if (txtBTWEditProducten.Text == "")
            {
                check = false;
                txtBTWEditProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgBTWEditProducten.Visibility = Visibility.Visible;
            }
            if (nudAantalOpVooraadProducten.Text == "")
            {
                check = false;
                nudAantalOpVooraadProducten.ToolTip = "Textbox mag niet leeg zijn";
                imgAantalOpVooraadProducten.Visibility = Visibility.Visible;
            }
            if (nudAankoopPrijs.Text == "")
            {
                check = false;
                nudAankoopPrijs.ToolTip = "Textbox mag niet leeg zijn";
                imgAankoopPrijs.Visibility = Visibility.Visible;
            }
            if (check)
            {
                imgNaamEditProducten.Visibility =
                imgMargeEditProducten.Visibility =
                imgEenheidEditProducten.Visibility =
                imgAantalOpVooraadProducten.Visibility =
                imgAankoopPrijs.Visibility =
                imgBTWEditProducten.Visibility = Visibility.Hidden;

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
        private void tbEditBestellingKlantLeverancier_Checked(object sender, RoutedEventArgs e)
        {
            EditBestelling();
            EditBestellingfillCombobox();
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
            else if (tbEditBestelling.IsChecked == false)
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
                        if (tbEditBestellingKlantLeverancier.IsChecked == true)
                        {
                            cbLeverancierKlantEditBestelling.SelectedValue = selectedBestelling.LeverancierID;
                        }
                        else if (tbEditBestellingKlantLeverancier.IsChecked == false)
                        {
                            cbLeverancierKlantEditBestelling.SelectedValue = selectedBestelling.KlantID;
                        }
                    }
                }
            }
        }

        private void btnToevoegenBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                if (tbEditBestellingKlantLeverancier.IsChecked == true)
                {
                    ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate,
                        PersoneelslidID = (int)cbPersoneelslidEditBestelling.SelectedValue,
                        LeverancierID = (int)cbLeverancierKlantEditBestelling.SelectedValue
                    });
                }
                else if (tbEditBestellingKlantLeverancier.IsChecked == false)
                {
                    ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = (DateTime)dtDatumOpgemaakt.SelectedDate,
                        PersoneelslidID = (int)cbPersoneelslidEditBestelling.SelectedValue,
                        KlantID = (int)cbLeverancierKlantEditBestelling.SelectedValue
                    });
                }
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
                if (tbEditBestellingKlantLeverancier.IsChecked == true)
                {
                    selectedBestelling.LeverancierID = (int)cbLeverancierKlantEditBestelling.SelectedValue;
                }
                else if (tbEditBestellingKlantLeverancier.IsChecked == false)
                {
                    selectedBestelling.KlantID = (int)cbLeverancierKlantEditBestelling.SelectedValue;
                }
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            EditBestelling();
        }

        private void btnDeleteBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var CanceledOrder = ctx.BestellingProducts.Where(x => x.BestellingID == (int)cbEditBestelling.SelectedValue);
                foreach (var item in CanceledOrder)
                {
                    if (item.Bestelling.Klant == null)
                    {
                        item.Product.AantalOpVooraad += item.AantalProtuctBesteld;
                    }
                    else if (item.Bestelling.Leverancier == null)
                    {
                        item.Product.AantalOpVooraad -= item.AantalProtuctBesteld;
                    }

                }
                ctx.BestellingProducts.RemoveRange(CanceledOrder);
                ctx.Bestellings.Remove(ctx.Bestellings.Single(b => b.BestellingID == (int)cbEditBestelling.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            EditProducten();
        }
        private void tbEditBestellingProductKlantLeverancier_Checked(object sender, RoutedEventArgs e)
        {
            EditBestellingProduct();
            EditBestellingProductfillListbox();
        }


        private void cbEditBestellingProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditBestellingProductfillListbox();
            //using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            //{
            //    if (cbEditBestellingProduct.SelectedValue != null)
            //    {
            //        if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue) != null)
            //        {
            //            var selectedBestellingProduct = ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue);
            //            lbBestellingenEditBestellingProduct.SelectedValue = selectedBestellingProduct.BestellingID;
            //            cbProductenEditBestellingProduct.SelectedValue = selectedBestellingProduct.ProductID;

            //        }
            //    }
            //}
        }
        private void lbBestellingenEditBestellingProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbProductenEditBestellingProduct.SelectedValue = ctx.BestellingProducts.Single(x => x.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).ProductID;
            }

        }
        private void btnToevoegenBestellingProduct_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                ctx.BestellingProducts.Add(new BestellingProduct
                {
                    BestellingID = (int)cbEditBestellingProduct.SelectedValue,
                    ProductID = (int)cbProductenEditBestellingProduct.SelectedValue,
                    AantalProtuctBesteld = Convert.ToInt32(nudAantalProductenBesteld.Text)

                });
                if (ctx.Bestellings.Single(b => b.BestellingID == (int)lbBestellingenEditBestellingProduct.SelectedValue).LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == (int)cbProductenEditBestellingProduct.SelectedValue).AantalOpVooraad -= Convert.ToInt32(nudAantalProductenBesteld.Text); }
                else if (ctx.Bestellings.Single(b => b.BestellingID == (int)lbBestellingenEditBestellingProduct.SelectedValue).KlantID == null)
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
                var selectedBestelling = ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue);
                if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).Bestelling.LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad += selectedBestelling.AantalProtuctBesteld - Convert.ToInt32(nudAantalProductenBesteld.Text); }
                else if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).Bestelling.KlantID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad -= selectedBestelling.AantalProtuctBesteld - Convert.ToInt32(nudAantalProductenBesteld.Text); }

                selectedBestelling.BestellingID = (int)lbBestellingenEditBestellingProduct.SelectedValue;
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
                if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).Bestelling.LeverancierID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad += ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).AantalProtuctBesteld; }
                else if (ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).Bestelling.KlantID == null)
                { ctx.Products.Single(p => p.ProductID == ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)lbBestellingenEditBestellingProduct.SelectedValue).ProductID).AantalOpVooraad -= ctx.BestellingProducts.Single(bp => bp.BestellingProductID == (int)cbEditBestellingProduct.SelectedValue).AantalProtuctBesteld; }


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
            if (Convert.ToDecimal(nudAantalProductenBesteld.Text) > 1)
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
                var producten = ctx.Products.Where(p => p.LeverancierID == (int)cbJsonLeveranciers.SelectedValue).Select(p => p);
                List<object> ListProducten = new List<object>();
                foreach (var item in producten)
                {

                    JsonProductenLeverancier productsForList = new JsonProductenLeverancier(item.ProductID, item.Naam, item.Eenheid, item.BTW, item.AankoopPrijs);
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
            if (!File.Exists("gegevens.txt"))
            {
                using (FileStream fs = File.Create("gegevens.txt"))
                {
                }
            }

            var jsonString = JsonConvert.SerializeObject(listObject, Formatting.Indented);
            File.WriteAllText("gegevens.txt", jsonString);
            MessageBox.Show(jsonString.ToString());
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                //string uri = $"mailto:{ctx.Leveranciers.Where(l=>l.LeverancierID==(int)cbJsonLeveranciers.SelectedValue).Select(l=>l.Emailadres).FirstOrDefault()}?subject=Gegevens Producten&body={jsonString.ToString()}";
                //Uri myUri = new Uri(uri);
                //Process.Start(myUri.AbsoluteUri);

                MAPI mapi = new MAPI();

                mapi.AddAttachment($"{Directory.GetCurrentDirectory()}/gegevens.txt");
                mapi.AddRecipientTo(ctx.Leveranciers.Where(l => l.LeverancierID == (int)cbJsonLeveranciers.SelectedValue).Select(l => l.Emailadres).FirstOrDefault());
                mapi.SendMailPopup("Json Gevens producten om bijtewerken", $"Beste {ctx.Leveranciers.Where(l => l.LeverancierID == (int)cbJsonLeveranciers.SelectedValue).Select(l => l.Contactpersoon).FirstOrDefault()},\n" +
                    $"\n" +
                    $"bij deze de Json file om uw productenlijst mee aan te passen as requested.\n" +
                    $"\n" +
                    $"met vriendelijke groeten {Selected.Voornaam} {Selected.Achternaam}");
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
            if (txtPasswordPersoneellidEdit.Password.Length == 0)
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
