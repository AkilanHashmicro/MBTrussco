using System;
using System.Collections.Generic;
using SalesApp.models;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class TargetDetailPage : ContentPage
    {
        List<target_line> saleresult = new List<target_line>();
        public TargetDetailPage(SalesTarget item)
        {
            InitializeComponent();
            // saleresult = Controller.InstanceCreation().salesTargetData();
            targetListview.ItemsSource = item.target_line;
            name_val.Text = item.name;
            com_val.Text = item.commission_type;
        }


        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#FFFFFF");
        }

    }
}
