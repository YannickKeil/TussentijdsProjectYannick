using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Xml.Schema;

namespace TussentijdsProjectYannick
{
    /// <summary>
    /// Interaction logic for BestellingMenu.xaml
    /// </summary>
    public partial class BestellingMenu : Window
    {
        public BestellingMenu()
        {
            InitializeComponent();
        }
        public Personeelslid Selected { get; set; }
        public BestellingMenu(Personeelslid selected)
        {
            Selected = selected;
            InitializeComponent();
            LoadKlanten();
            LoadLeveranciers();
            LoadProducten();
            if (selected.AdminRechtenID == 1)
            {
                tbLeverancierKlant.Visibility = Visibility.Visible;
            }
            else if(selected.AdminRechtenID == 2)
            {
                tbLeverancierKlant.Visibility = Visibility.Hidden;
                tbLeverancierKlant.IsChecked = true;
            }
            else if(selected.AdminRechtenID == 3)
            {
                tbLeverancierKlant.Visibility = Visibility.Hidden;
                tbLeverancierKlant.IsChecked = false;
            }
        }
        public void LoadKlanten()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbKlants.ItemsSource = null;
                var listKlant = ctx.Klants.Select(x => new { Naam = x.Voornaam + " " + x.Achternaam, Id = x.KlantID }).ToList();
                cbKlants.DisplayMemberPath = "Naam";
                cbKlants.SelectedValuePath = "Id";
                cbKlants.ItemsSource = listKlant;
                cbKlants.SelectedIndex = 0;

            }
        }
        public void LoadLeveranciers()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbLeveranciers.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbLeveranciers.DisplayMemberPath = "Contactpersoon";
                cbLeveranciers.SelectedValuePath = "LeverancierID";
                cbLeveranciers.ItemsSource = listLeverancier;
                cbLeveranciers.SelectedIndex = 0;             
            }
        }
        public void LoadProducten()
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                cbProducten.ItemsSource = null;
                var listProducten = ctx.Products.Select(x => x).ToList();
                cbProducten.DisplayMemberPath = "Naam";
                cbProducten.SelectedValuePath = "ProductID";
                cbProducten.ItemsSource = listProducten;
                cbProducten.SelectedIndex = 0;              
            }
        }
        public List<AantalProductBesteling> gekozenProducten = new List<AantalProductBesteling>();
        public void LoadListBox()
        {
            lbProductenBestelling.ItemsSource = null;
            //lbProductenBestelling.SelectedValuePath = "ProductID";
            lbProductenBestelling.ItemsSource = gekozenProducten;

        }
        private static readonly Regex _regex = new Regex("[^0-9]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
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
        private void tbLeverancierKlant_Checked(object sender, RoutedEventArgs e)
        {
            cbKlants.Visibility = Visibility.Hidden;
            cbLeveranciers.Visibility = Visibility.Visible;
            btnAddNewKlant.Content = "Voeg Nieuwe Leverancier Toe";
        }

        private void tbLeverancierKlant_Unchecked(object sender, RoutedEventArgs e)
        {
            cbKlants.Visibility = Visibility.Visible;
            cbLeveranciers.Visibility = Visibility.Hidden;
            btnAddNewKlant.Content = "Voeg Nieuwe Klant Toe";
        }
        private void btnAddNewKlant_Click(object sender, RoutedEventArgs e)
        {
            Databeheer db = new Databeheer();
            db.tabKlanten.Visibility = Visibility.Hidden;
            db.tabAdminRechten.Visibility = Visibility.Hidden;
            db.tabBestellingen.Visibility = Visibility.Hidden;
            db.tabBestellingProducten.Visibility = Visibility.Hidden;
            db.tabCategorie.Visibility = Visibility.Hidden;
            db.tabJsonProducten.Visibility = Visibility.Hidden;
            db.tabLeverancier.Visibility = Visibility.Hidden;
            db.tabPersoneellid.Visibility = Visibility.Hidden;
            db.tabProducten.Visibility = Visibility.Hidden;           
            if ((bool)tbLeverancierKlant.IsChecked)
            {
                db.TabDatabeheer.SelectedItem = db.tabLeverancier;
                db.tbEditLeverancier.Visibility = Visibility.Hidden;
                db.cbEditLeverancier.Visibility = Visibility.Hidden;
                db.btnEditLeverancier.Visibility = Visibility.Hidden;
                db.btnDeleteLeverancier.Visibility = Visibility.Hidden;
            }
            else if (!(bool)tbLeverancierKlant.IsChecked)
            {
                db.TabDatabeheer.SelectedItem = db.tabKlanten;
                db.tbEditKlant.Visibility = Visibility.Hidden;
                db.cbEditKlant.Visibility = Visibility.Hidden;
                db.btnEditKlant.Visibility = Visibility.Hidden;
                db.btnDeleteKlant.Visibility = Visibility.Hidden;
            }
            
            db.ShowDialog();
            
                db.tabKlanten.Visibility = Visibility.Visible;
                db.tabAdminRechten.Visibility = Visibility.Visible;
                db.tabBestellingen.Visibility = Visibility.Visible;
                db.tabBestellingProducten.Visibility = Visibility.Visible;
                db.tabCategorie.Visibility = Visibility.Visible;
                db.tabJsonProducten.Visibility = Visibility.Visible;
                db.tabLeverancier.Visibility = Visibility.Visible;
                db.tabPersoneellid.Visibility = Visibility.Visible;
                db.tabProducten.Visibility = Visibility.Visible;
            db.tbEditKlant.Visibility = Visibility.Visible;
            db.cbEditKlant.Visibility = Visibility.Visible;
            db.btnEditKlant.Visibility = Visibility.Visible;
            db.btnDeleteKlant.Visibility = Visibility.Visible;
            db.tbEditLeverancier.Visibility = Visibility.Visible;
            db.cbEditLeverancier.Visibility = Visibility.Visible;
            db.btnEditLeverancier.Visibility = Visibility.Visible;
            db.btnDeleteLeverancier.Visibility = Visibility.Visible;

        }
        private void btnNumUp_Click(object sender, RoutedEventArgs e)
        {
            decimal getal = Convert.ToDecimal(nudAantal.Text);
            getal++;
            nudAantal.Text = getal.ToString();
        }

        private void btnNumDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDecimal(nudAantal.Text) > 0)
                nudAantal.Text = $"{Convert.ToDecimal(nudAantal.Text) - 1}";
        }
        private void btnToevoegenAanList_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var selectedProduct = ctx.Products.Single(p => p.ProductID == (int)cbProducten.SelectedValue); 
                AantalProductBesteling productVoorList = new AantalProductBesteling(selectedProduct.ProductID, selectedProduct.Naam, Convert.ToInt32(nudAantal.Text), selectedProduct.Eenheid);
                gekozenProducten.Add(productVoorList);              
                LoadListBox();
            }

        }
        private void btnVerwijderUitList_Click(object sender, RoutedEventArgs e)
        {
            if(lbProductenBestelling.SelectedIndex != -1)
            gekozenProducten.RemoveAt(lbProductenBestelling.SelectedIndex);          
            LoadListBox();
        }

        private void btnPlaatsBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var besteling = ctx.Bestellings.FirstOrDefault();
                if ((bool)tbLeverancierKlant.IsChecked)
                {
                    besteling = ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = DateTime.Now,
                        PersoneelslidID = Selected.PersoneelslidID,
                        LeverancierID = (int)cbLeveranciers.SelectedValue,
                    });
                    MessageBox.Show("Leverancier");
                }
                else if (!(bool)tbLeverancierKlant.IsChecked)
                {
                    besteling = ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = DateTime.Now,
                        PersoneelslidID = Selected.PersoneelslidID,
                        KlantID = (int)cbKlants.SelectedValue,
                    });
                    MessageBox.Show("klant");
                }
                ctx.SaveChanges();
                MessageBox.Show($"{besteling.BestellingID} {besteling.DatumOpgemaakt} {besteling.KlantID}");
                
                foreach (var item in gekozenProducten)
                {
                    ctx.BestellingProducts.Add(new BestellingProduct
                    {
                        BestellingID = besteling.BestellingID,
                        ProductID = item.ProductIDAPB,
                        AantalProtuctBesteld = item.AantalGekozenProductAPB
                    });
                    if ((bool)tbLeverancierKlant.IsChecked)
                    {
                        ctx.Products.Single(p => p.ProductID == item.ProductIDAPB).AantalOpVooraad += item.AantalGekozenProductAPB;
                        MessageBox.Show("Leverancier2");
                    }
                    else if (!(bool)tbLeverancierKlant.IsChecked)
                    {
                        ctx.Products.Single(p => p.ProductID == item.ProductIDAPB).AantalOpVooraad -= item.AantalGekozenProductAPB;
                        MessageBox.Show("klant2");
                    }
                    ctx.SaveChanges();
                }
            }
            this.DialogResult = true;
        }

        private void btnCancelBestelling_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public class AantalProductBesteling
        {
            public int ProductIDAPB {get;set;}
            public string ProductNaamAPB { get; set; }
            public int AantalGekozenProductAPB { get; set; }
            public string EenheidAPB { get; set; }

            public AantalProductBesteling(int productIDAPB, string productNaamAPB, int aantalGekozenProductAPB, string eenheidAPB)
            {
                ProductIDAPB = productIDAPB;
                ProductNaamAPB = productNaamAPB;
                AantalGekozenProductAPB = aantalGekozenProductAPB;
                EenheidAPB = eenheidAPB;
            }
        }
    }
}
