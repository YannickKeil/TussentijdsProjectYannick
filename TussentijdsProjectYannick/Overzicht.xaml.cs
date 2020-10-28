using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TussentijdsProjectYannick
{
    /// <summary>
    /// Interaction logic for Overzicht.xaml
    /// </summary>
    public partial class Overzicht : Window
    {
        public Overzicht()
        {
            InitializeComponent();
            using (Projectweek_YannickEntities dc = new Projectweek_YannickEntities()) {
                var bestellingen = dc.BestellingProducts.Select(bp => new { BestellingsNummer = bp.BestellingID, DateOpmaak = bp.Bestelling.DatumOpgemaakt.ToShortDateString(), Personeel = bp.Bestelling.Personeelslid.Voornaam + " " + bp.Bestelling.Personeelslid.Achternaam, Producten = dc.BestellingProducts.Where(x => x.BestellingID == bp.BestellingID).Select(x => x.AantalProtuctBesteld + " " + x.Product.Naam + " " + x.Product.Eenheid + " €" + x.Product.AankoopPrijs) }).GroupBy(x => x.BestellingsNummer);
                ListViewBestellingen.ItemsSource = bestellingen.ToList();
            }
        }
    }
}
