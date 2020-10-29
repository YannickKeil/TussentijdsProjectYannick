using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using Microsoft.Office.Interop.Word;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Table = Microsoft.Office.Interop.Word.Table;

namespace TussentijdsProjectYannick
{
    /// <summary>
    /// Interaction logic for BestellingMenu.xaml
    /// </summary>
    public partial class BestellingMenu : System.Windows.Window
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
            else if (selected.AdminRechtenID == 2)
            {
                tbLeverancierKlant.Visibility = Visibility.Hidden;
                tbLeverancierKlant.IsChecked = true;
            }
            else if (selected.AdminRechtenID == 3)
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
            if (lbProductenBestelling.SelectedIndex != -1)
                gekozenProducten.RemoveAt(lbProductenBestelling.SelectedIndex);
            LoadListBox();
        }

        private void btnPlaatsBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
            {
                var bestelling = ctx.Bestellings.FirstOrDefault();
                if ((bool)tbLeverancierKlant.IsChecked)
                {
                    bestelling = ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = DateTime.Now,
                        PersoneelslidID = Selected.PersoneelslidID,
                        LeverancierID = (int)cbLeveranciers.SelectedValue,
                    });
                    MessageBox.Show("Leverancier");
                }
                else if (!(bool)tbLeverancierKlant.IsChecked)
                {
                    bestelling = ctx.Bestellings.Add(new Bestelling
                    {
                        DatumOpgemaakt = DateTime.Now,
                        PersoneelslidID = Selected.PersoneelslidID,
                        KlantID = (int)cbKlants.SelectedValue,
                    });
                    MessageBox.Show("klant");
                }
                else
                {
                    MessageBox.Show("Oops somthing went wrong. Please contact a dev.");
                    return;
                }
                ctx.SaveChanges();
                MessageBox.Show($"{bestelling.BestellingID} {bestelling.DatumOpgemaakt} {bestelling.KlantID}");

                foreach (var item in gekozenProducten)
                {
                    ctx.BestellingProducts.Add(new BestellingProduct
                    {
                        BestellingID = bestelling.BestellingID,
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
                if (!(bool)tbLeverancierKlant.IsChecked)
                {
                    FactuurKlant(bestelling.BestellingID);
                }

            }
            this.DialogResult = true;
        }
        private void FactuurKlant(int selectedBestellingID)
        {
            MessageBox.Show(selectedBestellingID.ToString());
            try
            {

                using (Projectweek_YannickEntities ctx = new Projectweek_YannickEntities())
                {


                    //Create an instance for word app  
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                    //Set animation status for word application  
                    winword.ShowAnimation = false;

                    //Set status for word application is to be visible or not.  
                    winword.Visible = false;

                    //Create a missing variable for missing value  
                    object missing = System.Reflection.Missing.Value;

                    //Create a new document  
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    //Add header into the document  
                    foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                    {
                        //Get the header range and add the header details.  
                        Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                        headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                        headerRange.Font.Size = 30;
                        headerRange.Text = "Bedrijfnaam";
                    }

                    //Add the footers into the document  
                    foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                    {
                        //Get the footer range and add the footer details.  
                        Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                        footerRange.Font.Size = 10;
                        footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        footerRange.Text = "www.Bedrijfsite.com info@bedrijfsnaam.com BTWnummer  Bedrijfsnaam©";
                    }

                    //adding text to document  
                    //document.Content.SetRange(0, 0);
                    //document.Content.Text = "This is test document " + Environment.NewLine;

                    var bestelling = ctx.Bestellings.Where(b => b.BestellingID == selectedBestellingID).Select(b => b).FirstOrDefault();
                    //Add paragraph with Heading 1 style
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Heading 1";
                    para1.Range.set_Style(ref styleHeading1);
                    para1.Range.Text = $""/*Kortemark, {DateTime.Now.ToShortDateString()}*/;
                    para1.Range.InsertParagraphAfter();
                    //if (bestelling != null)
                    //{
                    //    MessageBox.Show($"{ bestelling.Klant.Voornaam} { bestelling.Klant.Achternaam}");
                    //    var TitelTable = document.Tables.Add(para1.Range, 10, 4, ref missing, ref missing);
                    //    TitelTable.Borders.Enable = 0;
                    //    TitelTable.Cell(1, 1).Range.Text = $"{ bestelling.Klant.Voornaam} { bestelling.Klant.Achternaam}";
                    //    TitelTable.Cell(1, 1).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(1, 1).Range.Font.Size = 20;

                    //    TitelTable.Cell(1, 4).Range.Text = $"Kortemark, { DateTime.Now.ToShortDateString()}";
                    //    TitelTable.Cell(1, 4).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(1, 4).Range.Font.Size = 20;

                    //    TitelTable.Cell(2, 1).Range.Text = $"{bestelling.Klant.Straatnaam} {bestelling.Klant.Huisnummer}";
                    //    TitelTable.Cell(2, 1).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(2, 1).Range.Font.Size = 20;

                    //    TitelTable.Cell(3, 1).Range.Text = $"{bestelling.Klant.Postcode} {bestelling.Klant.Gemeente}";
                    //    TitelTable.Cell(3, 1).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(3, 1).Range.Font.Size = 20;

                    //    TitelTable.Cell(4, 1).Range.Text = $"{bestelling.Klant.Telefoonnummer}";
                    //    TitelTable.Cell(4, 1).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(4, 1).Range.Font.Size = 20;

                    //    TitelTable.Cell(6, 1).Range.Text = $"Bestelnr.:{bestelling.BestellingID}";
                    //    TitelTable.Cell(6, 1).Range.Font.Name = "verdana";
                    //    TitelTable.Cell(6, 1).Range.Font.Size = 20;
                    //}




                    //Add paragraph with Heading 2 style  
                    Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    para2.Range.set_Style(ref styleHeading1);
                    string test = $"{bestelling.Klant.Voornaam} {bestelling.Klant.Achternaam}";
                    para2.Range.Text = test;
                    para2.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                    para3.Range.set_Style(ref styleHeading1);
                    para3.Range.Text = $"{bestelling.Klant.Straatnaam} {bestelling.Klant.Huisnummer}";
                    para3.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                    para4.Range.set_Style(ref styleHeading1);
                    para4.Range.Text = $"{bestelling.Klant.Postcode} {bestelling.Klant.Gemeente}";
                    para4.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para5 = document.Content.Paragraphs.Add(ref missing);
                    para5.Range.set_Style(ref styleHeading1);
                    para5.Range.Text = $"{bestelling.Klant.Telefoonnummer}";
                    para5.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para6 = document.Content.Paragraphs.Add(ref missing);
                    para6.Range.set_Style(ref styleHeading1);
                    para6.Range.Text = "";
                    para6.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para7 = document.Content.Paragraphs.Add(ref missing);
                    para7.Range.set_Style(ref styleHeading1);
                    para7.Range.Text = $"Bestelnr.:{bestelling.BestellingID}";
                    para7.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para8 = document.Content.Paragraphs.Add(ref missing);
                    para8.Range.set_Style(ref styleHeading1);
                    para8.Range.Text = "";
                    para8.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para9 = document.Content.Paragraphs.Add(ref missing);
                    para9.Range.set_Style(ref styleHeading1);
                    para9.Range.Text = "";
                    para9.Range.InsertParagraphAfter();
                    Microsoft.Office.Interop.Word.Paragraph para10 = document.Content.Paragraphs.Add(ref missing);
                    para10.Range.set_Style(ref styleHeading1);
                    para10.Range.Text = "";
                    para10.Range.InsertParagraphAfter();
                    var bestellingProducten = ctx.BestellingProducts.Where(bp => bp.BestellingID == selectedBestellingID).Select(bp => bp).ToList();


                    //Create a 5X5 table and insert some dummy record  
                    int Rows = bestellingProducten.Count + 4;
                    var firstTable = document.Tables.Add(para1.Range, Rows, 4, ref missing, ref missing);
                    string products;
                    if (Rows == 1)
                    {
                        products = "Product";
                    }
                    else
                    {
                        products = "Producten";
                    }
                    //WdPreferredWidthType wdPreferredWidthPercent = default;
                    //firstTable.Columns.PreferredWidthType = wdPreferredWidthPercent;
                    //firstTable.Columns[1].PreferredWidth = 50;
                    //firstTable.Columns[2].PreferredWidth = 25;
                    //firstTable.Columns[3].PreferredWidth = 25;
                    firstTable.Borders.Enable = 0;
                    firstTable.Cell(1, 1).Range.Text = products;
                    firstTable.Cell(1, 1).Range.Font.Name = "verdana";
                    firstTable.Cell(1, 1).Range.Font.Size = 10;
                    firstTable.Cell(1, 1).Range.Font.Bold = 1;
                    firstTable.Cell(1, 2).Range.Text = "Aantal";
                    firstTable.Cell(1, 2).Range.Font.Name = "verdana";
                    firstTable.Cell(1, 2).Range.Font.Size = 10;
                    firstTable.Cell(1, 2).Range.Font.Bold = 1;
                    firstTable.Cell(1, 3).Range.Text = "Eenheidsprijs";
                    firstTable.Cell(1, 3).Range.Font.Name = "verdana";
                    firstTable.Cell(1, 3).Range.Font.Size = 10;
                    firstTable.Cell(1, 3).Range.Font.Bold = 1;
                    firstTable.Cell(1, 4).Range.Text = "Prijs";
                    firstTable.Cell(1, 4).Range.Font.Name = "verdana";
                    firstTable.Cell(1, 4).Range.Font.Size = 10;
                    firstTable.Cell(1, 4).Range.Font.Bold = 1;

                    double excBTW = 0;
                    double BTW = 0;
                    double Totaal = 0;

                    for (int i = 2; i < firstTable.Rows.Count + 1; i++)
                    {

                        if (i - 2 < Rows - 4)
                        {
                            firstTable.Cell(i, 1).Range.Text = $"{bestellingProducten[i - 2].Product.Naam}";
                            firstTable.Cell(i, 1).Range.Font.Name = "verdana";
                            firstTable.Cell(i, 1).Range.Font.Size = 10;
                            firstTable.Cell(i, 2).Range.Text = $"{bestellingProducten[i - 2].AantalProtuctBesteld} {bestellingProducten[i - 2].Product.Eenheid}";
                            firstTable.Cell(i, 2).Range.Font.Name = "verdana";
                            firstTable.Cell(i, 2).Range.Font.Size = 10;
                            firstTable.Cell(i, 3).Range.Text = $"€ {bestellingProducten[i - 2].Product.Bruto()}";
                            firstTable.Cell(i, 3).Range.Font.Name = "verdana";
                            firstTable.Cell(i, 3).Range.Font.Size = 10;
                            firstTable.Cell(i, 4).Range.Text = $"€ {bestellingProducten[i - 2].Product.Bruto() * bestellingProducten[i - 2].AantalProtuctBesteld}";
                            firstTable.Cell(i, 4).Range.Font.Name = "verdana";
                            firstTable.Cell(i, 4).Range.Font.Size = 10;

                            excBTW += bestellingProducten[i - 2].Product.Netto() * bestellingProducten[i - 2].AantalProtuctBesteld;
                            BTW += bestellingProducten[i - 2].Product.BTWVerschil() * bestellingProducten[i - 2].AantalProtuctBesteld;
                            Totaal += bestellingProducten[i - 2].Product.Bruto() * bestellingProducten[i - 2].AantalProtuctBesteld;
                        }
                        else
                        {

                            if (firstTable.Rows.Count + 1 - i == 3)
                            {
                                firstTable.Cell(i, 2).Range.Text = "Exclusief BTW";
                                firstTable.Cell(i, 2).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 2).Range.Font.Size = 10;
                                firstTable.Cell(i, 2).Range.Font.Bold = 1;
                                firstTable.Cell(i, 4).Range.Text = $"€ {excBTW}";
                                firstTable.Cell(i, 4).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 4).Range.Font.Size = 10;
                                firstTable.Cell(i, 4).Range.Font.Bold = 1;
                            }
                            else if (firstTable.Rows.Count + 1 - i == 2)
                            {
                                firstTable.Cell(i, 2).Range.Text = "BTW";
                                firstTable.Cell(i, 2).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 2).Range.Font.Size = 10;
                                firstTable.Cell(i, 2).Range.Font.Bold = 1;
                                firstTable.Cell(i, 4).Range.Text = $"€ {BTW}";
                                firstTable.Cell(i, 4).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 4).Range.Font.Size = 10;
                                firstTable.Cell(i, 4).Range.Font.Bold = 1;
                            }
                            else if (firstTable.Rows.Count + 1 - i == 1)
                            {
                                firstTable.Cell(i, 2).Range.Text = "Totaal:";
                                firstTable.Cell(i, 2).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 2).Range.Font.Size = 10;
                                firstTable.Cell(i, 2).Range.Font.Bold = 1;
                                firstTable.Cell(i, 4).Range.Text = $"€ {Totaal}";
                                firstTable.Cell(i, 4).Range.Font.Name = "verdana";
                                firstTable.Cell(i, 4).Range.Font.Size = 10;
                                firstTable.Cell(i, 4).Range.Font.Bold = 1;
                            }
                        }

                    }
                    //foreach (Row row in firstTable.Rows)
                    //{

                    //    foreach (Cell cell in row.Cells)
                    //    {
                    //        //Header row  
                    //        if (cell.RowIndex == 1)
                    //        {
                    //            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                    //            cell.Range.Font.Bold = 1;
                    //            //other format properties goes here  
                    //            cell.Range.Font.Name = "verdana";
                    //            cell.Range.Font.Size = 10;
                    //            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                              

                    //            //Center alignment for the Header cells  
                    //            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    //        }
                    //        //Data row  
                    //        else
                    //        {
                    //            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
                    //        }
                    //    }
                    //}

                    //Save the document  
                    object filename = $"{Directory.GetCurrentDirectory()}/Besrelling{bestelling.BestellingID}.docx";
                    document.SaveAs2(ref filename);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    MessageBox.Show("Document created successfully !");
                    MAPI mapi = new MAPI();

                    mapi.AddAttachment($"{Directory.GetCurrentDirectory()}/Besrelling{bestelling.BestellingID}.docx");
                    mapi.AddRecipientTo(bestelling.Klant.Emailadres);
                    mapi.SendMailPopup("factuur bestelling", $"Beste {bestelling.Klant.Voornaam} {bestelling.Klant.Achternaam},\n" +
                        $"\n" +
                        $"Bedankt dat u voor ons gekozen heeft.\n" +
                        $"in bijlagen vind u de factuur van uw bestelling.\n" +
                        $"\n" +
                        $"met vriendelijke groeten {Selected.Voornaam} {Selected.Achternaam}\n" +
                        $"team Bedrijfsnaam");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelBestelling_Click(object sender, RoutedEventArgs e)
        {
            CreateDocument();
            this.DialogResult = false;
        }
        private void CreatePDF()
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
            string pdfFilename = "firstpage.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }
        private void CreateDocument()
        {
            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application  
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.  
                winword.Visible = false;

                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Add header into the document  
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.  
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Header text goes here";
                }

                //Add the footers into the document  
                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.  
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Footer text goes here";
                }

                //adding text to document  
                document.Content.SetRange(0, 0);
                document.Content.Text = "This is test document " + Environment.NewLine;

                //Add paragraph with Heading 1 style  
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = "Para 1 text";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style  
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading2 = "Heading 2";
                para2.Range.set_Style(ref styleHeading2);
                para2.Range.Text = "Para 2 text";
                para2.Range.InsertParagraphAfter();

                //Create a 5X5 table and insert some dummy record  
                Table firstTable = document.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);

                firstTable.Borders.Enable = 1;
                foreach (Row row in firstTable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        //Header row  
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here  
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                              
                            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            //Center alignment for the Header cells  
                            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                        }
                        //Data row  
                        else
                        {
                            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
                        }
                    }
                }

                //Save the document  
                object filename = $"{Directory.GetCurrentDirectory()}/latesttemp1.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
                MessageBox.Show("Document created successfully !");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class AantalProductBesteling
        {
            public int ProductIDAPB { get; set; }
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
