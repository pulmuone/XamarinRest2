using DevExpress.Mobile.DataGrid.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinJson.ViewModels;

namespace XamarinJson.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
		public MainView ()
		{
            InitializeComponent ();
            ThemeManager.ThemeName = Themes.Light;

            //this.BindingContext = new MainViewModel();
        }

    }
}