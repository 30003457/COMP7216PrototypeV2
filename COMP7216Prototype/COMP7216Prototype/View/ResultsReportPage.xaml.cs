using COMP7216Prototype.Controller;
using COMP7216Prototype.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COMP7216Prototype.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsReportPage : ContentPage
    {
        Report reportController;
        public ResultsReportPage(List<CreditRequests> requests, Customers user)
        {
            InitializeComponent();
            reportController = new Report(user);
            reportController.Requests = requests;
            reportController.SetRequestStatements();
            lvResults.ItemsSource = null;
            lvResults.ItemsSource = reportController.Requests;
            BindingContext = null;
            BindingContext = reportController;
        }

        public ResultsReportPage(List<CreditShares> shares, Customers user)
        {
            InitializeComponent();
            reportController = new Report(user);
            reportController.Shares = shares;
            reportController.SetShareStatements();
            lvResults.ItemsSource = null;
            lvResults.ItemsSource = reportController.Shares;
            BindingContext = null;
            BindingContext = reportController;
        }
    }
}