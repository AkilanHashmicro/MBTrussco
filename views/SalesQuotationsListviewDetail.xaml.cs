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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;


namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesQuotationsListviewDetail : PopupPage
    {

        bool editbtn_clicked = false;

        bool add_new_orderline = false;

        bool tap2_clicked = false;

        int saleoder_id = 0;

        int orderline_id = 0;

        SalesQuotationDB dbobj = new SalesQuotationDB();

        SalesQuotation obj = new SalesQuotation();

        List<OrderLine> final_listview = new List<OrderLine>();

       List<OrderLine> final_listviewnew = new List<OrderLine>();

       List<OrderLinesListForUpdate> orderLinelist = new List<OrderLinesListForUpdate>();

        protected override bool OnBackButtonPressed()
        {

             //  Loading();

            //var currentpage = new LoadingAlert();
             //PopupNavigation.PushAsync(currentpage);

          //  App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab3"));

            PopupNavigation.PopAllAsync();

          // Loadingalertcall();

              return true;
           
        }

        async void Loading()
        {
            var currentpage = new LoadingAlert();
            await PopupNavigation.PushAsync(currentpage);
        }

        async void Loadingalertcall()
        {
            await  PopupNavigation.PopAllAsync();
        }

        public SalesQuotationsListviewDetail(SalesQuotation item)
        {
            InitializeComponent();
            //   NavigationPage.SetHasNavigationBar(this, false);

            //    App.orderLineList = item.order_line;

            obj = item;

            Cus.Text = item.customer;
            CD.Text = item.DateOrder;
            PT.Text = item.payment_term;
            CG.Text = item.commission_group;
            SP.Text = item.sales_person;
            ST.Text = item.sales_team;
            CR.Text = item.customer_reference;
            FP.Text = item.fiscal_position;
            saleoder_id = item.id;
            userlocation.Text = App.user_location_string;

            final_listview = item.order_line;

            orderListview.ItemsSource = item.order_line;

            orderListview.HeightRequest = item.order_line.Count * 50;

            var sq_editRecognizer = new TapGestureRecognizer();
            sq_editRecognizer.Tapped += async (s, e) =>
            {
                // handle the tap              
                // noedit_layout.IsVisible = false;

               // List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();


                sq_editbtn.IsVisible = false;

                updatebtn.IsVisible = false;

                savebtn_layout.IsVisible = true;

                editbtn_clicked = true;

                cus_edit.IsVisible = true;
                con_dateedit.IsVisible = true;
                ptpicker_edit.IsVisible = true;
                commissionpicker_edit.IsVisible = true;
                sales_teamedit.IsVisible = true;
                sales_personsedit.IsVisible = true;

                cr_edit.IsVisible = true;
                fp_edit.IsVisible = true;
                is_edit.IsVisible = true;

                cus_noedit.IsVisible = false;
                con_datenoedit.IsVisible = false;
                ptpicker_noedit.IsVisible = false;
                commissionpicker_noedit.IsVisible = false;
                sales_teamnoedit.IsVisible = false;
                sales_personsnoedit.IsVisible = false;

                cr_noedit.IsVisible = false;
                fp_noedit.IsVisible = false;
                is_noedit.IsVisible = false;


                salesteam_picker.ItemsSource = App.salesteam.Select(x => x.Value).ToList();
                salesteam_picker.SelectedItem = item.sales_team;

                salesperson_picker.ItemsSource = App.salespersons.Select(x => x.Value).ToList();
                salesperson_picker.SelectedItem = item.sales_person;

                cuspicker1.ItemsSource = cuspicker1.ItemsSource = App.cusdict.Select(x => x.Value).ToList();
                cuspicker1.SelectedItem = item.customer;

                DateTime enteredDate = DateTime.Parse(item.DateOrder);

                cd_Picker.Date = enteredDate;

                ptpicker.ItemsSource = App.paytermList.Select(x => x.name).ToList();
                ptpicker.SelectedItem = item.payment_term;


                comgroup_picker.ItemsSource = App.commisiongroupList.Select(x => x.name).ToList();
                comgroup_picker.SelectedItem = item.commission_group;

                orderLineGrid_ol.IsVisible = false;


                addbtn_orderline.IsVisible = true;

                if (tap2_clicked == true) 
                {
                    addbtn_orderline.IsVisible = false;
                }
              
            };
            sq_editbtn.GestureRecognizers.Add(sq_editRecognizer);


            var addbtn_orderlineRecognizer = new TapGestureRecognizer();
            addbtn_orderlineRecognizer.Tapped +=  (s, e) =>
            {
                orderLineGrid_ol.IsVisible = true;
                addbtn_orderline.IsVisible = false;
                savebtn_layout.IsVisible = true;
                updatebtn.IsVisible = false;

                searchprod_ol.Text = "";
                orderline_des_ol.Text = "";
                up_ol.Text = "";
                oqty_ol.Text = "";

                add_new_orderline = true;

            };
            Add_OrderLineBtn.GestureRecognizers.Add(addbtn_orderlineRecognizer);


        }

        public SalesQuotationsListviewDetail(SalesQuotationDB item)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //    App.orderLineList = item.order_line;


            dbobj = item;

            Cus.Text = item.customer;
            // CD.Text = item.DateOrder;
            CD.Text = item.date_Order;
            PT.Text = item.payment_term.ToString();
            SP.Text = item.sales_person;
            ST.Text = item.sales_team;
            CR.Text = item.customer_reference;
            FP.Text = item.fiscal_position;
            saleoder_id = item.id;

                    List<OrderLine> or_linelistdb = new List<OrderLine>();
             
                    var json_orderline = JsonConvert.SerializeObject(item.order_line);

                    String convertstring = json_orderline.ToString();

                    //  "\"[{\\\"customer_lead\\\":\\\"0\\\",\\\"price_unit\\\":\\\"10000\\\",\\\"product_uom_qty\\\":\\\"10\\\",\\\"price_subtotal\\\":\\\"100000\\\",\\\"taxes\\\":[],\\\"product_name\\\":\\\"Floordeck 1000x060 MM\\\"},{\\\"customer_lead\\\":\\\"0\\\",\\\"price_unit\\\":\\\"1\\\",\\\"product_uom_qty\\\":\\\"1\\\",\\\"price_subtotal\\\":\\\"1\\\",\\\"taxes\\\":[\\\"Sales Tax N/A SRCA-S\\\"],\\\"product_name\\\":\\\"Floordeck 1000x060 MM\\\"}]\""

                    String finstring = convertstring.Replace("\\", "");

                    finstring = finstring.Substring(1);

                    finstring = finstring.Remove(finstring.Length - 1);

                    JArray stringres = JsonConvert.DeserializeObject<JArray>(finstring);

                    //  or_linelistdb = JsonConvert.DeserializeObject<List<OrderLine>>(finstring);


                    //  OrderLine stringres = JsonConvert.DeserializeObject<OrderLine>(json_orderline)

                    int cus_lead = 0;
                    string prod_name = "";

           
                foreach (JObject obj in stringres)
                {
                    OrderLine or_line = new OrderLine();
                   object[] tax_id = new object[2];

                    or_line.product_name = obj["product_name"].ToString();
                    or_line.product_uom_qty = obj["product_uom_qty"].ToString();
                    or_line.price_subtotal = obj["price_subtotal"].ToString();

                //

                    or_line.taxes = tax_id;
                    or_linelistdb.Add(or_line);
                }
           

            orderListview.ItemsSource = or_linelistdb;


            var sq_editRecognizer = new TapGestureRecognizer();
            sq_editRecognizer.Tapped += async (s, e) =>
            {
                await DisplayAlert("Alert", "Need Internet Connection", "Ok");
            };
            sq_editbtn.GestureRecognizers.Add(sq_editRecognizer);

            //orderListview.ItemsSource = item.order_line;


        }

       


        protected override void OnAppearing()
        {
            base.OnAppearing();


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

                        searchprod_ol.Text = prodresults.Name;
                        orderline_des_ol.Text = prodresults.Name;
                        up_ol.Text = prodresults.list_price;

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


        private void prodpicker_Focused(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new PickerSelectionPage());

            searchprod.Unfocus();

        }


        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {
            orderLineGrid_ol.IsVisible = false;
            addbtn_orderline.IsVisible = false;

            if (editbtn_clicked == true)
            {
                updatebtn.IsVisible = false;

                OrderLine masterItemObj = (OrderLine)ea.Item;

                if (masterItemObj.is_promotion == false)
                {

                    listview_editlayout.IsVisible = true;
                    OrderLineList1.IsVisible = false;

                    searchprod.Text = masterItemObj.product_name;
                    orderline_des.Text = masterItemObj.product_name;
                    oqty.Text = masterItemObj.product_uom_qty;
                    up.Text = masterItemObj.price_unit;

                    orderline_id = masterItemObj.id;
                }

                else
                {

                }
            }
                
          //  savebtn_layout.IsVisible = true;
        }


        private void applypromotion_clicked(object sender, EventArgs ea)
        {

            String res = "";
            try
            {
                res = Controller.InstanceCreation().SaleOrderConfirm("sale.order", "confirm_sale_quotation", 0);
            }

            catch
            {
                int jm = 0;
            }

            if (App.NetAvailable == true)
            {

                Navigation.PushPopupAsync(new ApplyPromotionsPopupPage(saleoder_id));
            }

            else
            {
                DisplayAlert("Alert", "Need Internet Connection", "Ok");
            }

          //  Navigation.PushPopupAsync(new FilterPopupPage("tab4"));
        }

        private void ConfirmSOClicked(object sender, EventArgs ea)
        {
            String res = "";
            try
            {
               res = Controller.InstanceCreation().SaleOrderConfirm("sale.order", "confirm_sale_quotation", saleoder_id);
            }

            catch
            {
                int j = 0;
            }
            //  String res = "Stock not available for this products : \n Down payment";

            if (App.NetAvailable == true)
            {
                //  var data = await DisplayAlert("Logout Alert", "Are you sure you want to log out?", "OK", "Cancel");
                if (res.Contains("\n"))
                {
                    string[] stringSeparators = new string[] { "\n" };
                    string[] results = res.Split(stringSeparators, StringSplitOptions.None);

                    String first = results[0];
                    String second = results[1];

                    DisplayAlert(first, second, "Ok");
                }

                else
                {

                    if (res == "Success")
                    {
                        DisplayAlert("Alert", "Sale Order Confirmed Successfully", "Ok");
                        Application.Current.MainPage = new MasterPage(new CrmTabbedPage());
                    }
                    else
                    {
                        DisplayAlert("Alert", res, "Ok");

                        //  orderListview.ItemsSource = null;
                        // Loadingalertcall();
                    }
                }
            }
            else
            {
                DisplayAlert("Alert", "Need Internet Connection", "Ok");
            }

        }


        private void Tab1Clicked(object sender, EventArgs ea)
        {
            tab1stack.BackgroundColor = Color.FromHex("#363E4B");
            tab1.BackgroundColor = Color.FromHex("#363E4B");
            tab2stack.BackgroundColor = Color.White;
            tab2.BackgroundColor = Color.White;
            tab2frame.BackgroundColor = Color.FromHex("#363E4B");
            tab2borderstack.BackgroundColor = Color.White;
            orderLineList.IsVisible = true;
            OtherInfoStack1.IsVisible = false;
            OtherInfoStack2.IsVisible = false;
            tab1frame.BackgroundColor = Color.FromHex("#363E4B");
            tab1borderstack.BackgroundColor = Color.FromHex("#363E4B");
            OrderLineList1.IsVisible = true;

            tap2_clicked = false;

            if(editbtn_clicked == true)
            {
                addbtn_orderline.IsVisible = true;
            }
          //  savebtn_layout.IsVisible = true;

            tab1.TextColor = Color.White;
            tab2.TextColor = Color.Black;
        }

        private void Tab2Clicked(object sender, EventArgs ea)
        {
            tab2stack.BackgroundColor = Color.FromHex("#363E4B");
            tab2.BackgroundColor = Color.FromHex("#363E4B");
            tab1stack.BackgroundColor = Color.White;
            tab1.BackgroundColor = Color.White;
            tab2borderstack.BackgroundColor = Color.FromHex("#363E4B");
            tab2frame.BackgroundColor = Color.FromHex("#363E4B");
            orderLineList.IsVisible = false;
            OtherInfoStack1.IsVisible = true;
            OtherInfoStack2.IsVisible = true;
            tab1frame.BackgroundColor = Color.FromHex("#363E4B");
            tab1borderstack.BackgroundColor = Color.White;
            OrderLineList1.IsVisible = false;

            listview_editlayout.IsVisible = false;
            addbtn_orderline.IsVisible = false;

            tab1.TextColor = Color.Black;
            tab2.TextColor = Color.White;

            tap2_clicked = true;

           
          //  savebtn_layout.IsVisible = false;

        }


        private void save_clicked(object sender, EventArgs ea)
        {
            // final_listview.Clear();
            updatebtn.IsVisible = true;
            sq_editbtn.IsVisible = true;
            orderLineGrid_ol.IsVisible = false;
            addbtn_orderline.IsVisible = true;

            savebtn_layout.IsVisible = false;

            OrderLine orderLine = new OrderLine();

            //if(editbtn_clicked==true)


            //if (up_ol.Text == "" || oqty_ol.Text == null )
            //{

            //    DisplayAlert("Alert", "Please fill all the fields", "Ok");
            //    updatebtn.IsVisible = false;
            //}


            //else
            //{

                // OrderLinesListForUpdate orderLine = new OrderLine();

                if (add_new_orderline == true)
                {

                    OrderLine orderLinenew = new OrderLine();

                    var productlis = from pro in App.productList
                                     where pro.Name == searchprod_ol.Text
                                     select pro;

                    int prod_id = 0;

                    foreach (var pro in productlis)
                    {
                        prod_id = pro.Id;
                    }

                    //  orderLine.product_id = prod_id;

                    int newodid = 0;

                    foreach (var ids in obj.order_line)
                    {
                        newodid = ids.id + 11;
                        //  newodid = newodid;
                    }




                    orderLinenew.id = newodid;
                    orderLinenew.product_name = searchprod_ol.Text;
                    orderLinenew.product_id = prod_id;
                    orderLinenew.product_uom_qty = oqty_ol.Text;
                    orderLinenew.price_unit = up_ol.Text;
                    orderLinenew.price_subtotal = (Convert.ToInt32(oqty_ol.Text) * Convert.ToInt32(up_ol.Text)).ToString();



                    // List<Object> emptytx = new List<Object>();

                    Object[] tax_id = new Object[0];
                    // tax_id[0] = emptytx;
                    orderLinenew.taxes = tax_id;

                    foreach (var newobj in obj.order_line)
                    {
                        orderLinenew.taxes = newobj.taxes;
                    }

                    obj.order_line.Add(orderLinenew);

                }

                foreach (var newobj in obj.order_line)
                {
                    if (newobj.id == orderline_id && newobj.id != 0)
                    {
                        // orderLine.id = newobj.id;
                        orderLine.product_name = searchprod.Text;


                        var productlis = from pro in App.productList
                                         where pro.Name == newobj.product_name
                                         select pro;

                        int prod_id = 0;

                        foreach (var pro in productlis)
                        {
                            prod_id = pro.Id;
                        }

                        orderLine.product_id = prod_id;
                        orderLine.product_uom_qty = oqty.Text;
                        orderLine.price_unit = up.Text;
                        orderLine.taxes = newobj.taxes;
                        orderLine.price_subtotal = (Convert.ToInt32(oqty.Text) * Convert.ToInt32(up.Text)).ToString();



                        orderLine.customer_lead = newobj.customer_lead;
                    }
                }

                if (add_new_orderline == false)
                {

                    int index = final_listview.FindIndex(m => m.id == orderline_id);
                    if (index >= 0)
                        final_listview[index] = orderLine;

                }
                orderLinelist.Clear();

                foreach (var newobj in final_listview)
                {
                    OrderLinesListForUpdate orderLineupdate = new OrderLinesListForUpdate();
                    // orderLineupdate.id = newobj.id;

                var productlis = from pro in App.productList
                                 where pro.Name == newobj.product_name
                                 select pro;

                int prod_id = 0;
                foreach (var pro in productlis)
                {
                    prod_id = pro.Id;
                }


                //  orderLineupdate.product_id = newobj.product_id;
                    orderLineupdate.product_id = prod_id;
                    orderLineupdate.product = newobj.product_name;
                    orderLineupdate.ordered_qty = newobj.product_uom_qty;
                    orderLineupdate.unit_price = newobj.price_unit;

                Object[] tax_id = new object[newobj.taxes.Count()];

                for (int i = 0; i < newobj.taxes.Count(); i++)
                {
                    var tax_list = from tx in App.taxList
                                                     where tx.Name == newobj.taxes[i].ToString()
                                     select tx;

                    int tax_list_id = 0;
                    foreach (var tax in tax_list)
                    {
                        tax_list_id = tax.Id;
                    }

                    //var tax_list_id = from x in App.taxList
                                              //         where x.Name == newobj.taxes[i].ToString()
                                              //select x.Id;

                    tax_id[i] = tax_list_id;
                }

                orderLineupdate.tax_id = tax_id;
                   // orderLineupdate.tax_id = newobj.taxes;

                    orderLinelist.Add(orderLineupdate);
                }


                //   orderListview.ClearValue();

                OrderLineList1.IsVisible = true;
                orderListview.ItemsSource = final_listviewnew;
                final_listviewnew.Clear();
                orderListview.ItemsSource = final_listview;

                orderListview.HeightRequest = final_listview.Count * 50;

                listview_editlayout.IsVisible = false;


                Cus.Text = cuspicker1.SelectedItem.ToString();
                // CD.Text = item.DateOrder;
                CD.Text = cd_Picker.Date.ToString();
                PT.Text = ptpicker.SelectedItem.ToString();
                CG.Text = comgroup_picker.SelectedItem.ToString();
                SP.Text = salesperson_picker.SelectedItem.ToString();
                ST.Text = salesteam_picker.SelectedItem.ToString();
                CR.Text = cr_entry.Text;
                FP.Text = fp_entry.Text;

                //   orderListview.ItemsSource = final_listview;

                //  App.final_listviewnew.Clear();

                // App.final_listviewnew = final_listview;


                editbtn_clicked = false;

                add_new_orderline = false;

                addbtn_orderline.IsVisible = false;

                cus_edit.IsVisible = false;
                con_dateedit.IsVisible = false;
                ptpicker_edit.IsVisible = false;
                commissionpicker_edit.IsVisible = false;
                sales_teamedit.IsVisible = false;
                sales_personsedit.IsVisible = false;

                cr_edit.IsVisible = false;
                fp_edit.IsVisible = false;
                is_edit.IsVisible = false;

                cus_noedit.IsVisible = true;
                con_datenoedit.IsVisible = true;
                ptpicker_noedit.IsVisible = true;
                commissionpicker_noedit.IsVisible = true;
                sales_teamnoedit.IsVisible = true;
                sales_personsnoedit.IsVisible = true;

                cr_noedit.IsVisible = true;
                fp_noedit.IsVisible = true;
                is_noedit.IsVisible = true;

                OtherInfoStack1.IsVisible = false;
                OtherInfoStack2.IsVisible = false;

           // }
        }
  
        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#FFFFFF");
        }

        private void cancel_clicked(object sender, EventArgs ea)
        {

            savebtn_layout.IsVisible = false;
            orderLineGrid_ol.IsVisible = false;
            OtherInfoStack1.IsVisible = false;
            
            OrderLineList1.IsVisible = true;
            orderListview.ItemsSource = final_listviewnew;
            final_listviewnew.Clear();
            orderListview.ItemsSource = final_listview;

            editbtn_clicked = false;

            cus_edit.IsVisible = false;
            con_dateedit.IsVisible = false;
            ptpicker_edit.IsVisible = false;
            sales_teamedit.IsVisible = false;
            sales_personsedit.IsVisible = false;

            cr_edit.IsVisible = false;
            fp_edit.IsVisible = false;
            is_edit.IsVisible = false;

            cus_noedit.IsVisible = true;
            con_datenoedit.IsVisible = true;
            ptpicker_noedit.IsVisible = true;
            sales_teamnoedit.IsVisible = true;
            sales_personsnoedit.IsVisible = true;

            cr_noedit.IsVisible = true;
            fp_noedit.IsVisible = true;
            is_noedit.IsVisible = true;

            sq_editbtn.IsVisible = true;

            listview_editlayout.IsVisible = false;

            updatebtn.IsVisible = true;
            addbtn_orderline.IsVisible = false;

            OtherInfoStack2.IsVisible = false;
        }

        private async void update_clickedAsync(object sender, EventArgs ea)
        {
            Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();
            int selectpaytermid = 0;
            int selectcomgroupid = 0;

            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            if (App.NetAvailable == true)
            {

                vals["order_date"] = obj.dateOrder;

                vals["expiration_date"] = obj.validity_date;

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

                            
                // vals["order_lines"] = final_listview;
                vals["order_lines"] = orderLinelist;

                vals["state"] = "draft";

                var currentpage = new LoadingAlert();
                await PopupNavigation.PushAsync(currentpage);

                bool updated = Controller.InstanceCreation().UpdateSaleOrder("sale.order", "update_sale_quotation", saleoder_id, vals);

                if (updated == true)
                {
                    App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab3"));
                    await Navigation.PopAllPopupAsync();
                }

                else
                {
                    await DisplayAlert("Alert", "Please try again", "Ok");
                    await Navigation.PopAllPopupAsync();

                }

            }

            else
            {
                await DisplayAlert("Alert", "Need Internet Connection", "Ok");
              //  await Navigation.PopAllPopupAsync();
            }
        }

        private async void updatecancel_clickedAsync(object sender, EventArgs ea)
        {
            var currentpage = new LoadingAlert();
            await PopupNavigation.PushAsync(currentpage);

            App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab3"));

            await Navigation.PopAllPopupAsync();
        }

    }
}