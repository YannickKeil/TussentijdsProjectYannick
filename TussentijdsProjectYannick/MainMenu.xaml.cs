using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        
        public MainMenu()
        {
            InitializeComponent();
        }
        public Personeelslid Selected { get; set; }
        public MainMenu(Personeelslid selected)
        {
            Selected = selected;
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.DialogResult != true)
            { Application.Current.Shutdown(); }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnDatabeheer_Click(object sender, RoutedEventArgs e)
        {
            //temp enkel personeel toevoegen
            Databeheer data = new Databeheer(Selected);
            data.ShowDialog();
        }

        private void btnOverzicht_Click(object sender, RoutedEventArgs e)
        {
            TussentijdsProjectYannickOverzicht.Personeelslid thisSelected = new TussentijdsProjectYannickOverzicht.Personeelslid() { Achternaam = Selected.Achternaam, Voornaam = Selected.Voornaam, AdminRechtenID = Selected.AdminRechtenID, PersoneelslidID = Selected.PersoneelslidID, Wachtwoord = Selected.Wachtwoord, Salt = Selected.Salt, Username = Selected.Username, Indiensttreding = Selected.Indiensttreding, GeboorteDatum = Selected.GeboorteDatum, };

            var overzicht = new TussentijdsProjectYannickOverzicht.MainWindow(thisSelected);
            overzicht.ShowDialog();
        }

        private void btnBestelling_Click(object sender, RoutedEventArgs e)
        {
            BestellingMenu bestelling = new BestellingMenu(Selected);
            bestelling.ShowDialog();
        }
    }
}
