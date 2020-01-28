using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CrmLeadPage : ContentPage
    {
      

        List<CRMLead> crmLeadAll;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string, string>("MyApp", "leadListUpdated", (sender, arg) =>
            {
               // List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                crmLeadListView.ItemsSource = App.crmList;

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
              
        public CrmLeadPage()
        {
            Title = "CRM Leads";
            BackgroundColor = Color.White;
            InitializeComponent();


            if (App.filterstring == "Month")
            {
                RefreshDataconstructor();
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }


            if(App.NetAvailable == true)
            {
                
                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    crmLeadListView.ItemsSource = App.crmList;
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb;
                }
            }




            //if (App.NetAvailable == true && App.crmListDb.Count == App.crmList.Count)
            //{
            //    try
            //    {


            //        //var query = from item in App.crmList
            //                    //                    where !App.crmListDb.Contains(item.id)
            //                    //select item;
            //       // var commonElements = App.crmList.Where(a => App.crmListDb.Any(x => x.id == a.id )).ToList();

            //        crmLeadListView.ItemsSource = App.crmList;

            //    }

            //    catch (Exception ea)
            //    {
            //        if (ea.Message.Contains("(Network is unreachable)") || ea.Message.Contains("NameResolutionFailure"))
            //        {
            //            App.NetAvailable = false;
            //        }

            //        else if (ea.Message.Contains("(503) Service Unavailable"))
            //        {
            //            App.responseState = false;
            //        }
            //    }
            //}

            else if(App.NetAvailable == false)
            {
                crmLeadListView.ItemsSource = App.crmListDb;
            }
                      
         //   crmLeadListView.ItemsSource = App.crmList;

            crmLeadListView.Refreshing += this.RefreshRequested;

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += async (s, e) => {
     
                await Navigation.PushPopupAsync(new LeadCreationPage());

            };
            plus.GestureRecognizers.Add(plusRecognizer);

        }

        private async void RefreshRequested(object sender, object e)
        {
            //await Task.Delay(2000);
            //List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            crmLeadListView.IsRefreshing = true;

           // await RefreshData();

            if (App.filterstring == "Month")
            {
                RefreshDataconstructor();
            }

            if (App.NetAvailable == true )
            {                
                crmLeadListView.ItemsSource = App.crmList;
                crmLeadListView.IsRefreshing = false;

                //var result1 = from y in App.crmListDb
                //              where y.yellowimg_string == "yellowcircle.png"
                //              select y;

                //if (result1.Count() == 0)
                //{
                //    await Task.Delay(2000);
                //    List<CRMLead> crmLeadData1 = Controller.InstanceCreation().crmLeadData();
                //    crmLeadListView.ItemsSource = App.crmList;
                //    crmLeadListView.EndRefresh();
                //}

                //else
                //{
                //    crmLeadListView.ItemsSource = App.crmListDb;
                //    crmLeadListView.EndRefresh();
                //}

                //await Task.Delay(2000);
                //List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmFilterData();

            }

            else if (App.NetAvailable == false)
            {
               // await Task.Delay(500);
                crmLeadListView.ItemsSource = App.crmListDb;
                crmLeadListView.EndRefresh();
            }

            crmLeadListView.EndRefresh();

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
            Navigation.PushPopupAsync(new FilterPopupPage("tab1"));
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    MessagingCenter.Subscribe<Dictionary<string,dynamic>, string>(vals, "NotifyMsg", (sender, arg) =>
        //    {
        //        string retarg = arg;
        //        List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
        //    });
        //}

        async void phoneClicked(object sender, EventArgs e1)
        {
            var args = (TappedEventArgs)e1;

            try
            {
                CRMLead myobj = args.Parameter as CRMLead;
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
                CRMLeadDB myobj = args.Parameter as CRMLeadDB;
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

        //private void OnLabelTapped(Object sender, EventArgs e)
        //{


        //   // CRMLeadDB imageSender = (CRMLeadDB)sender;
        //        var args = (TappedEventArgs)e;
        //        CRMLeadDB myobj = args.Parameter as CRMLeadDB;
           
        // //   CRMLeadDB masterItemObj = (CRMLeadDB)e.Item

        //    int j = 0;
        //    //Your code here
        //}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {               
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //if (App.crmList.Count != 0)
                //{
                //    crmLeadListView.ItemsSource = App.crmList;
                //}

                //else
                //{
                //    crmLeadListView.ItemsSource = App.crmListDb;
                //}

                if (App.NetAvailable == true)
                {

                    var result1 = from y in App.crmListDb
                                  where y.yellowimg_string == "yellowcircle.png"
                                  select y;

                    if (result1.Count() == 0)
                    {
                        crmLeadListView.ItemsSource = App.crmList;
                    }

                    else
                    {
                        crmLeadListView.ItemsSource = App.crmListDb;
                    }
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb;
                }

            }
            else
            {

                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    crmLeadListView.ItemsSource = App.crmList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }
            }
        }

        private void crmLeadListView_ItemTapped(object sender, ItemTappedEventArgs ea)
        {
            //if (App.NetAvailable == true && App.crmList.Count != App.crmListDb.Count && App.crmListDb.Count != 0)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if (App.NetAvailable == true && App.crmList.Count == App.crmListDb.Count)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if(App.NetAvailable == true && App.crmList.Count > App.crmListDb.Count)
            //{
            //    CRMLead masterItemObj = (CRMLead)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if(App.NetAvailable == false)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}


             //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();



            //CRMLead masterItemObj = (CRMLead)ea.Item;
            //Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));

            if (App.NetAvailable == true)
            {
                crmLeadListView.ItemsSource = App.crmList;

                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    try
                    {
                        CRMLead masterItemObj = (CRMLead)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }

                    catch
                    {
                        CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                }

                else
                {
                    try
                    {
                        CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                    catch{
                        CRMLead masterItemObj = (CRMLead)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                }
            }


            else if (App.NetAvailable == false)
            {
                try
                {
                    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                }

                catch
                {
                    CRMLead masterItemObj = (CRMLead)ea.Item;
                    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                }
            }


        }
    }
}
