using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UEAssistantMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new LoginViewModel();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Task.Run(()=>GuiEffector.OpacityMagicToMin(viewModel, 0.005f, 1, false));
            await Navigation.PushAsync(new IndirectPage(loginEntry.Text, passwordEntry.Text));
        }
    }
}
