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
        public ResultsReportPage(List<CreditRequests> requests)
        {
            InitializeComponent();
            reportController = new Report();
            reportController.Requests = requests;
            reportController.SetRequestStatements();
            lvResults.ItemsSource = null;
            lvResults.ItemsSource = reportController.Requests;
            BindingContext = reportController;
        }

        public ResultsReportPage(List<CreditShares> shares)
        {
            InitializeComponent();
            reportController = new Report();
            reportController.Shares = shares;
            reportController.SetShareStatements();
            lvResults.ItemsSource = null;
            lvResults.ItemsSource = reportController.Shares;
            BindingContext = reportController;
        }
    }
}