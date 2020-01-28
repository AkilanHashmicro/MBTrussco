﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SalesApp.DBModel;
using SalesApp.models;
using SalesApp.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class SalesOrderListviewDetail : PopupPage
    {
        public SalesOrderListviewDetail( SalesOrder item )
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //    App.orderLineList = item.order_line;


            Cus.Text = item.customer;
            CD.Text = item.DateOrder;
            PT.Text = item.payment_term;
            SP.Text = item.sales_person;
            ST.Text = item.sales_team;
            CR.Text = item.customer_reference;
            FP.Text = item.fiscal_position;

            orderListview.ItemsSource = item.order_line;
           

            //var backImgRecognizer = new TapGestureRecognizer();
            //backImgRecognizer.Tapped +=  (s, e) => {
            //    // handle the tap    

            //    Navigation.PopAllPopupAsync();

            //};
            //backImg.GestureRecognizers.Add(backImgRecognizer);

        }


        protected override bool OnBackButtonPressed()
        {

            //   Navigation.PopAllPopupAsync();
            PopupNavigation.PopAsync();
            return true;
        }

        public SalesOrderListviewDetail(SalesOrderDB item)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //    App.orderLineList = item.order_line;


            Cus.Text = item.customer;
            CD.Text = item.DateOrder;
            PT.Text = item.payment_term;
            SP.Text = item.sales_person;
            ST.Text = item.sales_team;
            CR.Text = item.customer_reference;
            FP.Text = item.fiscal_position;

                   List<OrderLine> or_linelistdb = new List<OrderLine>();

           
                    var json_orderline = JsonConvert.SerializeObject(item.order_line);

                    String convertstring = json_orderline.ToString();

                    //  "\"[{\\\"customer_lead\\\":\\\"0\\\",\\\"price_unit\\\":\\\"10000\\\",\\\"product_uom_qty\\\":\\\"10\\\",\\\"price_subtotal\\\":\\\"100000\\\",\\\"taxes\\\":[],\\\"product_name\\\":\\\"Floordeck 1000x060 MM\\\"},{\\\"customer_lead\\\":\\\"0\\\",\\\"price_unit\\\":\\\"1\\\",\\\"product_uom_qty\\\":\\\"1\\\",\\\"price_subtotal\\\":\\\"1\\\",\\\"taxes\\\":[\\\"Sales Tax N/A SRCA-S\\\"],\\\"product_name\\\":\\\"Floordeck 1000x060 MM\\\"}]\""

                    String finstring = convertstring.Replace("\\", "");

                    finstring = finstring.Substring(1);

                    finstring = finstring.Remove(finstring.Length - 1);

                    JArray stringres = JsonConvert.DeserializeObject<JArray>(finstring);

                    //  OrderLine stringres = JsonConvert.DeserializeObject<OrderLine>(json_orderline)

                    int cus_lead = 0;
                    string prod_name = "";




                    foreach (JObject obj in stringres)
                    {
                        OrderLine or_line = new OrderLine();


                        or_line.product_name = obj["product_name"].ToString();
                        or_line.product_uom_qty = obj["product_uom_qty"].ToString();
                        or_line.price_subtotal = obj["price_subtotal"].ToString();

                        or_linelistdb.Add(or_line);
                    }


             


            orderListview.ItemsSource = or_linelistdb;


            //var backImgRecognizer = new TapGestureRecognizer();
            //backImgRecognizer.Tapped += async (s, e) => {
            //    // handle the tap    

            //    var currentpage = new LoadingAlert();
            //    await PopupNavigation.PushAsync(currentpage);

            //    // Navigation.PopAllPopupAsync();
            //    App.Current.MainPage = new MasterPage(new CrmTabbedPage());
            //    //  orderListview.ItemsSource = null;
            //    Loadingalertcall();

            //};
            //backImg.GestureRecognizers.Add(backImgRecognizer);
        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
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

            tab1.TextColor = Color.White;
            tab2.TextColor = Color.Black;
        }

        private void Tab2Clicked(object sender, EventArgs ea)
        {
            //tab2stack.BackgroundColor = Color.FromHex("#363E4B");
            //tab2.BackgroundColor = Color.FromHex("#363E4B");
            //tab1stack.BackgroundColor = Color.White;
            //tab1.BackgroundColor = Color.White;
            //tab2borderstack.BackgroundColor = Color.FromHex("#363E4B");
            //tab2frame.BackgroundColor = Color.FromHex("#363E4B");
            //orderLineList.IsVisible = false;
            //OtherInfoStack1.IsVisible = true;
            //OtherInfoStack2.IsVisible = true;
            //tab1frame.BackgroundColor = Color.White;
            //tab1borderstack.BackgroundColor = Color.FromHex("#363E4B");
            //OrderLineList1.IsVisible = false;

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

          

            tab1.TextColor = Color.Black;
            tab2.TextColor = Color.White;


        }

    }
}   
