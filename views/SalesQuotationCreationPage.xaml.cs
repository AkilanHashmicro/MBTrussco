using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SalesApp.DBModel;
using SalesApp.models;
using SalesApp.Pages;
using SalesApp.Persistance;
using SalesApp.wizard;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class SalesQuotationCreationPage : PopupPage
    {

        bool add_orderline_btn_clicked = false;

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        List<KeyValuePair<string, dynamic>> order_lines = new List<KeyValuePair<string, dynamic>>();
        List<int> taxidList = new List<int>();

        List<Dictionary<string, dynamic>> abc = new List<Dictionary<string, dynamic>>();
        List<OrderLinesList> orderLineList1 = new List<OrderLinesList>();
        List<OrderLinesList> orderLineList2 = new List<OrderLinesList>();

        Dictionary<int, string> cusdictdb = new Dictionary<int, string>();
        List<taxes> taxListdb = new List<taxes>();
        List<paytermList> payment_termsdb = new List<paytermList>();
        List<commisiongroupList> commission_groupdb = new List<commisiongroupList>();

     //   Dictionary<int, object> salesteamdict = new Dictionary<int, object>(

       
        public SalesQuotationCreationPage()
        {
            InitializeComponent();
            orderListview.HeightRequest = 0;
       //     App.tap_plusbtncheck = true;


          //  addorderline_Btn.Clicked += addorderline_Btn_clicked;

            if (App.NetAvailable == true)
            {

                cuspicker1.ItemsSource = App.cusdict.Select(x => x.Value).ToList();
                cuspicker1.SelectedIndex = 0;


                //var prostackRecognizer = new TapGestureRecognizer();
                //prostackRecognizer.Tapped += (s, e) =>
                //{
                //    Navigation.PushPopupAsync(new PickerSelectionPage());


                //};
                //searchprod.GestureRecognizers.Add(prostackRecognizer);




                var dropdownImgRecognizer = new TapGestureRecognizer();
                dropdownImgRecognizer.Tapped += (s, e) =>
                {
                     Navigation.PushPopupAsync(new PickerSelectionPage());

                   
                    searchprod.IsVisible = true;
                    //product_listview.IsVisible = true;

                    //List<ProductsList> proesult = new List<ProductsList>();
                    //if (App.NetAvailable == true)
                    //{
                    //    proesult = App.productList;
                    //    product_listview.ItemsSource = proesult;
                    //}

                    //if (App.NetAvailable == false)
                    //{

                    //    proesult = App.ProductListDb;
                    //    product_listview.ItemsSource = proesult;
                    //}


                };
                pickerdropimg.GestureRecognizers.Add(dropdownImgRecognizer);

                //taxpicker.ItemsSource = App.taxList.Select(x => x.Name).ToList();
                //taxpicker.SelectedIndex = 0;

                ptpicker.ItemsSource = App.paytermList.Select(x => x.name).ToList();
                ptpicker.SelectedIndex = 0;

                comgroup_picker.ItemsSource = App.commisiongroupList.Select(x => x.name).ToList();
                comgroup_picker.SelectedIndex = 0;

            }

            else if(App.NetAvailable ==false)
            {
                int user_iddb = 0;
                int partner_iddb = 0;

                JArray taxtlist_array = new JArray();

                foreach (var res in App.UserListDb)
                {
                    
                    cusdictdb = JsonConvert.DeserializeObject<Dictionary<int, string>>(res.customers_list);
                    taxtlist_array = JsonConvert.DeserializeObject<JArray>(res.tax_list);                   
                    payment_termsdb = JsonConvert.DeserializeObject<List<paytermList>>(res.payment_terms);
                    commission_groupdb = JsonConvert.DeserializeObject<List<commisiongroupList>>(res.commission_group);

                    user_iddb = res.userid;
                    partner_iddb = res.partnerid;

                }

                foreach (JObject obj in taxtlist_array)
                        {
                           taxes  taxesdb = new taxes("");                                      
                            taxesdb.Id  = obj["id"].ToObject<int>();
                            taxesdb.Name  = obj["name"].ToString();
                            taxListdb.Add(taxesdb);
                        }


                App.taxListdb = taxListdb;

                cuspicker1.ItemsSource = cusdictdb.Select(x => x.Value).ToList();
                cuspicker1.SelectedIndex = 0;

                var dropdownImgRecognizer = new TapGestureRecognizer();
                dropdownImgRecognizer.Tapped += (s, e) =>
                {
                    Navigation.PushPopupAsync(new PickerSelectionPage());
                };
                pickerdropimg.GestureRecognizers.Add(dropdownImgRecognizer);

                //taxpicker.ItemsSource = taxListdb.Select(x => x.Name).ToList();
                //taxpicker.SelectedIndex = 0;

                ptpicker.ItemsSource = payment_termsdb.Select(x => x.name).ToList();
                ptpicker.SelectedIndex = 0;

                comgroup_picker.ItemsSource = commission_groupdb.Select(x => x.name).ToList();
                comgroup_picker.SelectedIndex = 0;
            }

            var AirConImgRecognizer = new TapGestureRecognizer();
            AirConImgRecognizer.Tapped += (s, e) => {
                // handle the tap 
                //pd.ItemsSource = App.productList.Select(x => x.Name).ToList();
                //pd.SelectedIndex = 0;
                airconImg1.IsVisible = true;
                AddAirCon.IsVisible = false;
                orderLineGrid.IsVisible = true;
            //    orderLineGrid_1.IsVisible = true;

                taxlistviewGrid.IsVisible = true;

                Addtax_line.IsVisible = true;
              //  addtaxGrid.IsVisible = true;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
              //  taxpicker.SelectedIndex = 0;
                taxListView.ItemsSource = null;
                orderListview.ItemsSource = orderLineList1;
            };
            AddAirCon.GestureRecognizers.Add(AirConImgRecognizer);

            //var AirConImgRecognizer1 = new TapGestureRecognizer();
            //AirConImgRecognizer1.Tapped += (s, e) => {
            //    // handle the tap 

            //    airconImg1.IsVisible = false;
            //    taxlistviewGrid.IsVisible = false;
            //   // addtaxGrid.IsVisible = false;

            //    orderListview.ItemsSource = orderLineList2;
            //    Dictionary<string, dynamic> xyz = new Dictionary<string, dynamic>();

            //    if (up.Text == "" || oqty.Text == null)
            //    {
            //        DisplayAlert("Alert", "Please fill all the fields", "Ok");
            //        orderLineGrid.IsVisible = false;
            //      //  orderLineGrid_1.IsVisible = false;
            //        airconImg.IsVisible = true;
            //        AddAirCon.IsVisible = true;
            //    }
            //    else
            //    {
            //      //  xyz.Add("product", pd.SelectedItem.ToString());
                  
            //        xyz.Add("product", searchprod.Text.ToString());
            //        xyz.Add("ordered_qty", Convert.ToDouble(oqty.Text));
            //        xyz.Add("unit_price", Convert.ToDouble(up.Text));
            //        xyz.Add("description", orderline_des.Text);

            //        orderLineList1.Add(new OrderLinesList(searchprod.Text, Convert.ToDouble(oqty.Text), Convert.ToDouble(up.Text), taxidList, orderline_des.Text ));
            //        //  abc.Add(new Dictionary<string, dynamic>(xyz));

            //        orderListview.ItemsSource = orderLineList1;
            //        orderListview.RowHeight = 40;
            //        orderListview.HeightRequest = 40 * orderLineList1.Count;

            //        orderLineGrid.IsVisible = false;
            //      //  orderLineGrid_1.IsVisible = false;
            //        airconImg.IsVisible = true;
            //        AddAirCon.IsVisible = true;
                   
            //    }

            //};
            //AddAirCon1.GestureRecognizers.Add(AirConImgRecognizer1);

            //var taxImgRecognizer = new TapGestureRecognizer();
            //taxImgRecognizer.Tapped += (s, e) => {
            //    // handle the tap 

               

            //    taxListView.ItemsSource = null;

            //    App.taxListRemove.Add(new taxes(taxpicker.SelectedItem.ToString()));

            //    App.taxListRemove =   App.taxListRemove.GroupBy(i => i.Name).Select(g => g.First()).ToList();
            //   // taxpicker.IsVisible = false;
            //    taxStackLayout.IsVisible = true;
            //    taxListView.ItemsSource = App.taxListRemove;

            //    taxListView.RowHeight = 35;
            //    taxListView.HeightRequest = 35 * App.taxListRemove.Count;

            //    taxStackLayout.BackgroundColor = Color.FromHex("#363E4B");
            //    taxListView.BackgroundColor = Color.FromHex("#363E4B");
            //    taxStackLayout.CornerRadius = 20;

            //    taxStackLayout.Padding = 5;

            //   // taxpickstringList.Add(taxpicker.SelectedItem.ToString());
            //    var taxesid =
            //   (
            //   from i in App.taxList
            //   where i.Name == taxpicker.SelectedItem.ToString()

            //   select new
            //   {
            //       i.Id,
            //   }
            //   ).ToList();

            //    foreach (var person in taxesid)
            //    {
            //        int selecttaxid = person.Id;
            //        taxidList.Add(selecttaxid);
            //        taxidList = taxidList.GroupBy(i => i).Select(g => g.First()).ToList();
            //    }
            //};
            //Addtax.GestureRecognizers.Add(taxImgRecognizer);


            var Addtax_lineImgRecognizer = new TapGestureRecognizer();
            Addtax_lineImgRecognizer.Tapped += (s, e) => {

               // addtaxGrid.IsVisible = true;
                Addtax_line.IsVisible = false;

                Navigation.PushPopupAsync(new TaxSelectionPage());

            };
            Addtax_line.GestureRecognizers.Add(Addtax_lineImgRecognizer);



         var overallcloseImgRecognizer = new TapGestureRecognizer();
            overallcloseImgRecognizer.Tapped += (s, e) => {
                // handle the tap              
                Navigation.PopAllPopupAsync();
            };
            overall_close.GestureRecognizers.Add(overallcloseImgRecognizer);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string, string>("MyApp", "NotifyMsg", (sender, arg) =>
            {
               // HideLbl.Text = "New Quotation Creation";

            });


            MessagingCenter.Subscribe<string, string>("MyApp", "taxPickerMsg", (sender, arg) =>
            {
                
                taxListView.ItemsSource = null;

                App.taxListRemove.Add(new taxes(arg));

                App.taxListRemove = App.taxListRemove.GroupBy(i => i.Name).Select(g => g.First()).ToList();
                // taxpicker.IsVisible = false;
                taxStackLayout.IsVisible = true;
                taxListView.ItemsSource = App.taxListRemove;

                taxListView.RowHeight = 35;
                taxListView.HeightRequest = 35 * App.taxListRemove.Count;

                taxStackLayout.BackgroundColor = Color.FromHex("#363E4B");
                taxListView.BackgroundColor = Color.FromHex("#363E4B");
                taxStackLayout.CornerRadius = 20;

                taxStackLayout.Padding = 5;

                // taxpickstringList.Add(taxpicker.SelectedItem.ToString());
                var taxesid =
               (
               from i in App.taxList
               where i.Name == arg

               select new
               {
                   i.Id,
               }
               ).ToList();

                foreach (var person in taxesid)
                {
                    int selecttaxid = person.Id;
                    taxidList.Add(selecttaxid);
                    taxidList = taxidList.GroupBy(i => i).Select(g => g.First()).ToList();
                }

                Addtax_line.IsVisible = true;

            });



            MessagingCenter.Subscribe<string, int>("MyApp", "PickerMsg", (sender, arg) =>
            {
                // HideLbl.Text = "New Quotation Creation";

                if (App.productList.Count != 0)
                {
                    var productlis = from pro in App.productList
                                               where pro.Id == arg
                                               select pro;

                    foreach (var prodresults in productlis)
                    {
                        searchprod.Text = prodresults.Name;
                        orderline_des.Text = prodresults.Name;
                        up.Text = prodresults.list_price;
                    }
                }

                else
                {
                    var productlis = from pro in App.ProductListDb
                                               where pro.Id == arg
                                               select pro;

                    foreach (var prodresults in productlis)
                    {
                        searchprod.Text = prodresults.Name;
                        orderline_des.Text = prodresults.Name;
                        up.Text = prodresults.list_price;
                    }
                }

                int i = 0;

            });
        }


        private void Button_Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAllPopupAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
           // return base.OnBackButtonPressed();

            Navigation.PopAllPopupAsync();
            return true;
        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        private async void Button_Add_ClickedAsync(object sender, EventArgs e)
        {
            var currentpage = new LoadingAlert();
            await PopupNavigation.PushAsync(currentpage);

            int selectpaytermid = 0;
            int selectcomgroupid = 0;
            Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();
                   
            try
            {
                List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                             
            }

            catch (Exception ea)
            {
                if (ea.Message.Contains("(Network is unreachable)") || ea.Message.Contains("NameResolutionFailure"))
                {
                    App.NetAvailable = false;
                }

                else if (ea.Message.Contains("(503) Service Unavailable"))
                {
                    App.responseState = false;
                }

             }

            if(orderLineList1.Count == 0)
            {
               await DisplayAlert("Alert", "Please select atleast one", "Ok");

                // await Navigation.PopAsync();

                await PopupNavigation.PopAsync();

               // Loadingalertcall();
            }


            else
            {
                // vals["customer"] = cuspicker1.SelectedItem;

             
                vals["order_date"] = orDatePicker.Date;

                vals["expiration_date"] = exDatePicker.Date;

                if (ptpicker.SelectedItem == null)
                {
                    vals["payment_terms"] = false;
                }
                else
                {
                    var paytermid =
                    (
                            from i in App.paytermList
                            where i.name == ptpicker.SelectedItem.ToString()
                            select new
                            {
                                i.id,
                            }
               ).ToList();

                    foreach (var person in paytermid)
                    {
                        selectpaytermid = person.id;
                    }

                    vals["payment_terms"] = selectpaytermid;
                }

                if (comgroup_picker.SelectedItem == null)
                {
                    vals["commission_group"] = false;
                }

                else
                {
                    var comgroupid =
                    (
                            from i in App.commisiongroupList
                            where i.name == comgroup_picker.SelectedItem.ToString()
                            select new
                            {
                                i.id,
                            }
               ).ToList();

                    foreach (var comgroup in comgroupid)
                    {
                        selectcomgroupid = comgroup.id;
                    }

                    vals["commission_group"] = selectcomgroupid;
                }



                vals["user_id"] = App.userid;

                var cusid = App.cusdict.FirstOrDefault(x => x.Value == cuspicker1.SelectedItem.ToString()).Key;
                vals["customer"] = cusid;

                vals["order_lines"] = orderLineList1;
                vals["state"] = "draft";


                if (App.NetAvailable == true)
                {

                    string updated = Controller.InstanceCreation().UpdateCRMOpporData("sale.crm", "create_sale_quotation", vals);

                    if (updated == "true")
                    {
                        // App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                        // Navigation.PushPopupAsync(new MasterPage(  );
                       // await  DisplayAlert("Alert", "Successfully created", "Ok");
                     //  await Navigation.PopAllPopupAsync();

                        App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab3"));

                        Loadingalertcall();
                    }

                    else
                    {
                       await DisplayAlert("Alert", "Please try again", "Ok");
                        Loadingalertcall();
                    }
                }

                else if(App.NetAvailable == false)
                {

                    string ptpickerstring = ptpicker.SelectedItem.ToString();
                    string cgpickerstring = comgroup_picker.SelectedItem.ToString();

                   // ptpickerstring = ptpicker.SelectedItem.ToString();

                    if (ptpicker.SelectedItem == null)
                    {
                        //  vals["payment_terms"] = false;

                        ptpickerstring = "";
                    }
                    else
                    {
                        
                        var paytermid =
                        (
                                from i in payment_termsdb
                                where i.name == ptpicker.SelectedItem.ToString()
                                select new
                                {
                                    i.id,
                                }
                   ).ToList();


                        foreach (var person in paytermid)
                        {
                            selectpaytermid = person.id;
                        }

                    }

                   vals["payment_terms"] = selectpaytermid;


                    if (comgroup_picker.SelectedItem == null)
                    {
                        //vals["commission_group"] = false;
                        cgpickerstring = "";
                    }

                    else
                    {
                        var comgroupid =
                        (
                                from i in commission_groupdb
                                where i.name == comgroup_picker.SelectedItem.ToString()
                                select new
                                {
                                    i.id,
                                }
                   ).ToList();

                        foreach (var comgroup in comgroupid)
                        {
                            selectcomgroupid = comgroup.id;
                        }

                        vals["commission_group"] = selectcomgroupid;
                    }

                    var cusiddb = App.cusdictDb.FirstOrDefault(x => x.Value == cuspicker1.SelectedItem.ToString()).Key;
                    vals["customer"] = cusiddb;

                       // var jso_tags_list = Newtonsoft.Json.JsonConvert.SerializeObject(meetingLineList);

                    //List<OrderLinesListDB> orderLineListdb = new List<OrderLinesListDB>();


                    //List<OrderLinesListDB> orderLineListdb = new List<OrderLinesListDB>();

                    //foreach(var obj in orderLineList1)
                    //{
                    //    orderLineListdb.Add(new OrderLinesListDB(obj.product.ToString(), obj.ordered_qty.ToString(), obj.unit_price.ToString()));
                    //}
                                       
                   // var orderLineListnew = JsonConvert.SerializeObject(orderLineListdb.ToString());

                  //  var orderLineListnew = JsonConvert.SerializeObject(orderLineListdb);


                    List<OrderLine> or_linelistdb = new List<OrderLine>();


                    foreach (var obj in orderLineList1)
                    {
                        OrderLine or_line = new OrderLine();

                        List<int> tax_id = new List<int>();

                        or_line.product_name = obj.product.ToString();
                        or_line.product_uom_qty = obj.ordered_qty.ToString();
                        or_line.price_subtotal = obj.ordered_qty.ToString();
                       // or_line.taxes = tax_id;

                        or_linelistdb.Add(or_line);
                    }

                    var orderLineListnew = JsonConvert.SerializeObject(or_linelistdb);


                    string order_date = orDatePicker.Date.ToString();
                  //  DateTime oDate = DateTime.ParseExact(order_date, "yyyy-MM-dd HH:mm tt", null);
                
                    String order_date_string = String.Format("{0:yyyy-MM-dd HH:mm:ss}", orDatePicker.Date);
                    String expiration_date_string = String.Format("{0:yyyy-MM-dd HH:mm:ss}", exDatePicker.Date);


                    var sample = new SalesQuotationDB
                    {
                        //  id = item.id,

                        order_date = order_date_string,
                        expiration_date = expiration_date_string,
                        payment_term = ptpickerstring,
                        commission_group = cgpickerstring,
                        payment_term_id = selectpaytermid,
                        commission_group_id = selectcomgroupid,
                        user_id = App.userid_db,
                        customer_id = cusiddb,
                        order_line = orderLineListnew,
                        customer = cuspicker1.SelectedItem.ToString(),
                        date_Order = orDatePicker.Date.ToString(),
                        name = "LocalSO",
                        FullState = "draft",
                        state = "draft",
                        state_colour = "#3498db",

                        yellowimg_string = "yellowcircle.png",
                       
                     //   ColorCode  = "#3498db"
                    
                        //order_line = item.order_line[0].ToString()
                    };
                    App._connection.Insert(sample);

                                   
                    App._connection.CreateTable<SalesQuotationDB>();
                    try
                    {
                        var details = (from y in App._connection.Table<SalesQuotationDB>() select y).ToList();
                        App.SalesQuotationListDb = details;
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }


                    App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab3"));

                   // await DisplayAlert("Alert", "Created Successfull", "Ok");
                   // await Navigation.PopAllPopupAsync();

                    Loadingalertcall();

                }

            }
        }

        void productentry(object sender, EventArgs args)
        {
            Navigation.PushPopupAsync(new PickerSelectionPage());
        }


        //private void  product_focused(object sender, EventArgs e)
        //{
        //    Navigation.PushPopupAsync(new PickerSelectionPage());
        //}

        //private void prodpicker_Focused(object sender, EventArgs e)
        //{
        //    //if (add_orderline_btn_clicked == false)
        //    //{
        //    //    Navigation.PushPopupAsync(new PickerSelectionPage());
        //    //}
        //    //else
        //    //{

        //    //}

        //    Navigation.PushPopupAsync(new PickerSelectionPage());

        //    //  searchprod.Unfocused += searchprod_Unfocused;


        //}

        //private void searchprod_Unfocusednew(object sender, FocusEventArgs e)
        //{
        //    //unfocus event of Email entry

        //    PopupNavigation.PopAllAsync();
        //}

       
        private void ol_clicked(object sender, EventArgs e)
        {
           // searchprod.Unfocused += searchprod_Unfocusednew;
            //oqty.Keyboard = Keyboard.Default;
            //up.Keyboard = Keyboard.Default;
            //oqty.Unfocus();
            //up.Unfocus();

            //   PopupNavigation.PopAsync();


            airconImg1.IsVisible = false;
            taxlistviewGrid.IsVisible = false;
            //  addtaxGrid.IsVisible = false;

            orderListview.ItemsSource = orderLineList2;
            Dictionary<string, dynamic> xyz = new Dictionary<string, dynamic>();

            if (up.Text == "" || oqty.Text == null || oqty.Text == "" || searchprod.Text == "")
            {

                DisplayAlert("Alert", "Please fill all the fields", "Ok");

                orderLineGrid.IsVisible = false;
                // orderLineGrid_1.IsVisible = false;
                airconImg.IsVisible = true;
                AddAirCon.IsVisible = true;
                Addtax_line.IsVisible = false;

                orderListview.ItemsSource = orderLineList1;

            }
            else
            {

                //  xyz.Add("product", pd.SelectedItem.ToString());
                xyz.Add("product", searchprod.Text);

                try
                {
                    xyz.Add("ordered_qty", Convert.ToDouble(oqty.Text));
                }

                catch
                {
                    DisplayAlert("Alert", "Please fill all the fields", "Ok");
                }


                try
                {
                    xyz.Add("unit_price", Convert.ToDouble(up.Text));
                }
                catch
                {
                    DisplayAlert("Alert", "Please fill all the fields", "Ok");
                }

                xyz.Add("description", orderline_des.Text);


                orderLineList1.Add(new OrderLinesList(searchprod.Text, Convert.ToDouble(oqty.Text), Convert.ToDouble(up.Text), taxidList, orderline_des.Text));
                //  abc.Add(new Dictionary<string, dynamic>(xyz));

                orderListview.ItemsSource = orderLineList1;
                orderListview.RowHeight = 40;

                orderLineGrid.IsVisible = false;
                // orderLineGrid_1.IsVisible = false;
                airconImg.IsVisible = true;
                AddAirCon.IsVisible = true;

                orderListview.HeightRequest = 40 * orderLineList1.Count;

                searchprod.Text = "";
                oqty.Text = "";
                up.Text = "";
                orderline_des.Text = "";

                Addtax_line.IsVisible = false;


            }
        }

        //private void Button_Add_Ol_ClickedAsync(object sender, EventArgs e)
        //{

        //    // add_orderline_btn_clicked = true;

        //    searchprod.Unfocused += searchprod_Unfocusednew;
        //    //oqty.Keyboard = Keyboard.Default;
        //    //up.Keyboard = Keyboard.Default;
        //    //oqty.Unfocus();
        //    //up.Unfocus();
           
        // //   PopupNavigation.PopAsync();
           

        //    airconImg1.IsVisible = false;
        //    taxlistviewGrid.IsVisible = false;
        //  //  addtaxGrid.IsVisible = false;

        //    orderListview.ItemsSource = orderLineList2;
        //    Dictionary<string, dynamic> xyz = new Dictionary<string, dynamic>();

        //    if (up.Text == "" || oqty.Text == null || oqty.Text == "" || searchprod.Text =="")
        //    {

        //        DisplayAlert("Alert", "Please fill all the fields", "Ok");

        //        orderLineGrid.IsVisible = false;
        //       // orderLineGrid_1.IsVisible = false;
        //        airconImg.IsVisible = true;
        //        AddAirCon.IsVisible = true;
        //        Addtax_line.IsVisible = false;

        //        orderListview.ItemsSource = orderLineList1;

        //    }
        //    else
        //    {

        //      //  xyz.Add("product", pd.SelectedItem.ToString());
        //        xyz.Add("product", searchprod.Text);

        //        try
        //        {
        //            xyz.Add("ordered_qty", Convert.ToDouble(oqty.Text));
        //        }

        //        catch
        //        {
        //            DisplayAlert("Alert", "Please fill all the fields", "Ok");
        //        }


        //        try
        //        {
        //            xyz.Add("unit_price", Convert.ToDouble(up.Text));
        //        }
        //        catch
        //        {
        //            DisplayAlert("Alert", "Please fill all the fields", "Ok");
        //        }

        //        xyz.Add("description", orderline_des.Text);


        //        orderLineList1.Add(new OrderLinesList(searchprod.Text, Convert.ToDouble(oqty.Text), Convert.ToDouble(up.Text), taxidList, orderline_des.Text));
        //        //  abc.Add(new Dictionary<string, dynamic>(xyz));

        //        orderListview.ItemsSource = orderLineList1;
        //        orderListview.RowHeight = 40;

        //        orderLineGrid.IsVisible = false;
        //       // orderLineGrid_1.IsVisible = false;
        //        airconImg.IsVisible = true;
        //        AddAirCon.IsVisible = true;

        //        orderListview.HeightRequest = 40 * orderLineList1.Count;

        //        searchprod.Text = "";
        //        oqty.Text = "";
        //        up.Text = "";
        //        orderline_des.Text = "";

        //        Addtax_line.IsVisible = false;

              
        //    }
        //}

        public void ListviewcloseClicked(object sender, EventArgs e1)
        {
            try
            {
                var args = (TappedEventArgs)e1;
                taxes t2 = args.Parameter as taxes;

                var itemToRemove = App.taxListRemove.Single(r => r.Name == t2.Name);

                App.taxListRemove.Remove(itemToRemove);

               
                taxListView.ItemsSource = null;

             
              //  taxStackLayout.Padding = 0;

                taxListView.ItemsSource = App.taxListRemove;
                taxListView.RowHeight = 35;
                taxListView.HeightRequest = 35 * App.taxListRemove.Count;

            

                if(App.taxListRemove.Count == 0)
                {
                    taxStackLayout.BackgroundColor = Color.White;
                    taxListView.BackgroundColor = Color.White;
                    taxStackLayout.CornerRadius = 0;
                    taxStackLayout.Padding = 0;
                }

              
           
            }
            catch( Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
               
            }

        }


        //private void picker_TextChanged(object sender, EventArgs e)
        //{
        //    Navigation.PushPopupAsync(new FilterPopupPage());
        //}

   

      


        //private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)         //{          //    if (string.IsNullOrEmpty(e.NewTextValue))         //    {         //       // pickerListView.ItemsSource = App.productList;         //      //  product_picker_layout.IsVisible = false;         //        product_listview.IsVisible = false;         //    }          //    //else if(e.NewTextValue =="")         //    //{         //    //    pickerListView.ItemsSource = App.productList.Select(x => x.Name).ToList();         //    //}          //    else         //    {
        //        if (App.NetAvailable == true)
        //        {
        //            product_listview.ItemsSource = App.productList.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()));
        //        }
        //        else
        //        {
        //            product_listview.ItemsSource = App.ProductListDb.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()));
        //        }         //       // pickerListView.ItemsSource = App.productList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));         //    }          //}          //private void pickerListView_ItemTapped(object sender, ItemTappedEventArgs ea)         //{         //    ProductsList masterItemObj = (ProductsList)ea.Item;          //    searchprod.Text = masterItemObj.Name;
        //    product_listview.IsVisible = false;
         //   // //product_picker_layout.IsVisible = false;         //   //// searchBar.IsVisible = false;         //    //searchBar.Text = masterItemObj.Name;;          //  //  MessagingCenter.Send<string, string>("MyApp", "PickerMsg", masterItemObj.Name);          //   // Navigation.PopPopupAsync();         //    // Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));         //} 
        private void ViewCelltax_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#102b1e");
            //  m_title.TextColor = Color.Red;
        }

    }
}
