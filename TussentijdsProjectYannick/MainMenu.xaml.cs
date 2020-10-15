﻿using System;
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
            Databeheer data = new Databeheer();
            data.ShowDialog();
        }
    }
}
