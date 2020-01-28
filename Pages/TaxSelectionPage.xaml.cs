using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.Pages
{
    public partial class TaxSelectionPage : PopupPage
    {
      //  List<taxes> proesult = new List<taxes>();
        public TaxSelectionPage()
        {
            InitializeComponent();

            if (App.NetAvailable == true)
            {
                taxpickerListView.ItemsSource = App.taxList;
               
            }

            if (App.NetAvailable == false)
            {

                //proesult = App.ProductListDb;
                taxpickerListView.ItemsSource = App.taxListdb;
            }
        }


        private void taxpickerListView_ItemTapped(object sender, ItemTappedEventArgs ea)
        {
            taxes masterItemObj = (taxes)ea.Item;

            MessagingCenter.Send<string, String>("MyApp", "taxPickerMsg", masterItemObj.Name);

            Navigation.PopPopupAsync();
            // Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#f0eaea");
            //  m_title.TextColor = Color.Red;
        }
    }
}
