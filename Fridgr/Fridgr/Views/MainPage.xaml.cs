using System.ComponentModel;
using Fridgr.Models;
using Xamarin.Forms;

namespace Fridgr.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private Color _barColor;
        public MainPage()
        {
            InitializeComponent();
            
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new Constants();
        }
    }
}