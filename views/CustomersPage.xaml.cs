using FFImageLoading.Forms;
using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        Image Pic = new Image();
        List<CustomersModel> customerdata = new List<CustomersModel>();
        public CustomersPage()
        {
            InitializeComponent();

            customerdata = Controller.InstanceCreation().GetCustomerData();
            Customerlist.ItemsSource = customerdata;
             
            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) =>
            {
              //  App.Current.MainPage = new MasterPage(new CRMLeadCreationPage());

                Navigation.PushPopupAsync( new CRMLeadCreationPage());
            };
            plus.GestureRecognizers.Add(plusRecognizer);

        }

        private void CustomerListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CustomersModel modelObj = e.Item as CustomersModel;

            Navigation.PushPopupAsync(new CustomerListviewDetailPage(modelObj));
        //    Navigation.PushAsync(new CustomerListviewDetailPage(modelObj));

        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
           // obj.View.BackgroundColor = Color.FromHex("#414141");  
            obj.View.BackgroundColor = Color.White;
        }

    }
}
