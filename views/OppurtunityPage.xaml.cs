using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SalesApp.Pages;
using static SalesApp.models.CRMModel;
using Plugin.Messaging;
using SalesApp.DBModel;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OppurtunityPage : ContentPage
    {
       
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string, string>("MyApp", "oppListUpdated", (sender, arg) =>
            {
               // List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                oppurtunityListView.ItemsSource = App.crmOpprList;

            });
        }

        private async void RefreshDataconstructor()
        {
            await RefreshData();
        }

        async Task RefreshData()
        {
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
        }


        public OppurtunityPage()
        {
            Title = "CRM Opportunities";

            BackgroundColor = Color.White;
            InitializeComponent();

            if (App.filterstring == "Month" && App.load_rpc == true)
            {
                RefreshDataconstructor();
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }

            if (App.NetAvailable == true)
            {

                var result1 = from y in App.CRMOpportunitiesListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    oppurtunityListView.ItemsSource = App.crmOpprList;
                }

                else
                {
                    oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
                }
            }

       
            else  if (App.NetAvailable == false)
            {
                oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
            }

            // oppurtunityListView.ItemsSource = getOppurtunityDetails();
           
            oppurtunityListView.Refreshing += this.RefreshRequested;

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {

               Navigation.PushPopupAsync(new CRMOpportunityCreationPage1());
                                              
            };
            plus.GestureRecognizers.Add(plusRecognizer);
         }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            if (App.NetAvailable == true)
            {

                var result1 = from y in App.CRMOpportunitiesListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    try
                    {
                        CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
                        Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                    }

                    catch
                    {
                        CRMOpportunitiesDB masterItemObj = (CRMOpportunitiesDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                    }
                }

                else
                {
                    try
                    {
                        CRMOpportunitiesDB masterItemObj = (CRMOpportunitiesDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                    }

                    catch
                    {
                        CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
                        Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                    }
                }

            }

            else if (App.NetAvailable == false)
            {
                try
                {
                    CRMOpportunitiesDB masterItemObj = (CRMOpportunitiesDB)ea.Item;
                    Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                }

                catch
                {
                    CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
                    Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
                }
            }


            //if (App.NetAvailable == true)
            //{
            //    CRMOpportunities masterItemObj = (CRMOpportunities)ea.Item;
            //    Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
            //}

            //else if (App.NetAvailable == false)
            //{
            //    CRMOpportunitiesDB masterItemObj = (CRMOpportunitiesDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmOppDetailWizard(masterItemObj));
            //}

            //  App.Current.MainPage = new MasterPage(new OppurtunityDetailPage(masterItemObj));
        }

        private async void RefreshRequested(object sender, object e)
        {

            //await Task.Delay(2000);
            //List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            oppurtunityListView.IsRefreshing = true;
            //   await Task.Delay(200);

          //  await RefreshData();

            if (App.filterstring == "Month")
            {
                RefreshDataconstructor();
            }

            if (App.NetAvailable == true)
            {
                
                oppurtunityListView.ItemsSource = App.crmOpprList;
                oppurtunityListView.IsRefreshing = false;

                //var result1 = from y in App.CRMOpportunitiesListDb
                //              where y.yellowimg_string == "yellowcircle.png"
                //              select y;

                //if (result1.Count() == 0)
                //{
                //    await Task.Delay(2000);

                //    // oppurtunityListView.ItemsSource = this.getOppurtunityDetails();
                //    List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                //    oppurtunityListView.ItemsSource = App.crmOpprList;
                //    oppurtunityListView.EndRefresh();
                //}

                //else
                //{
                //    await Task.Delay(500);
                //    oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
                //    oppurtunityListView.EndRefresh();
                //}
            }

            //if (App.NetAvailable == true && App.crmList.Count == App.crmListDb.Count)
            //{
            //    await Task.Delay(2000);

            //    // oppurtunityListView.ItemsSource = this.getOppurtunityDetails();
            //    List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
            //    oppurtunityListView.ItemsSource = App.crmOpprList;
            //    oppurtunityListView.EndRefresh();
            //}

            //else if (App.NetAvailable == true && App.crmList.Count < App.crmListDb.Count)
            //{
            //    await Task.Delay(500);
            //    oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
            //    oppurtunityListView.EndRefresh();
            //}

            else if(App.NetAvailable == false)
            {
              //  await Task.Delay(500);
                oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
               // oppurtunityListView.EndRefresh();
            }

            oppurtunityListView.EndRefresh();
        }

        private void Toolbar_Search_Activated(object sender, EventArgs e)
        {
            if (searchBar.IsVisible)
            {
                searchBar.IsVisible = false;
            }
            else { searchBar.IsVisible = true; }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            // Navigation.PushPopupAsync(new CrmFilterWizard());
            Navigation.PushPopupAsync(new FilterPopupPage("tab2")); 
        }

        //private void MenuItem_Clicked(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);

        //    CRMLead obj = (CRMLead)mi.CommandParameter;
        //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(obj));
        //}

        async void phoneClicked(object sender, EventArgs e1)
        {
            // taxes myobj = sender as taxes;
            var args = (TappedEventArgs)e1;

            try
            {

                CRMOpportunities myobj = args.Parameter as CRMOpportunities;
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall && myobj.phone != "")
                {
                    phoneDialer.MakePhoneCall(myobj.phone);
                }
                else
                {
                    await Navigation.PushPopupAsync(new PhoneWizard());
                }

            }

            catch
            {
                CRMOpportunitiesDB myobj = args.Parameter as CRMOpportunitiesDB;
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall && myobj.phone != "")
                {
                    phoneDialer.MakePhoneCall(myobj.phone);
                }
                else
                {
                    await Navigation.PushPopupAsync(new PhoneWizard());
                }
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                var result1 = from y in App.CRMOpportunitiesListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    oppurtunityListView.ItemsSource = App.crmOpprList;
                }

                else
                {
                    oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb;
                }
            }

            else
            {

                var result1 = from y in App.CRMOpportunitiesListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    oppurtunityListView.ItemsSource = App.crmOpprList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }

                else
                {
                    oppurtunityListView.ItemsSource = App.CRMOpportunitiesListDb.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }


            }
        }
    }
}