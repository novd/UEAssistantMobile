using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UEAssistantMobile.ViewModels; 
using Xamarin.Forms;


namespace UEAssistantMobile
{
    public partial class IndirectPage : ContentPage
    {
        IndirectViewModel viewModel;
        private string login;
        private string password;

        Thread rotationThread;
        RequestManager requestManager;

        public IndirectPage(string login, string password)
        {
            this.login = login;
            this.password = password;

            InitializeComponent();

            this.BindingContext = viewModel = new IndirectViewModel();

        }

        protected override async void OnAppearing()
        {
            LogoRotate(); // visual rotation effect

            if (await SignInAsync(login, password))
            {
                viewModel.InfoText = "Zalogowano poprawnie! \n\tPobieram oceny...";
                var grades = await GetGradesAsync();
                viewModel.InfoText = "Oceny pobrane!";
                rotationThread.Abort();
                viewModel.RotationEffect = 0;
                await Task.Run(() => GuiEffector.OpacityMagicToMin(viewModel, 0.005f, 1, false));
                await Navigation.PushModalAsync(new MainPage(grades));
            }

            else
            {
                viewModel.InfoText = "Wystąpił problem \n\t\t z logowaniem";
                rotationThread.Abort();
                viewModel.RotationEffect = 0;
            }
                
        }

        async Task LogoRotate()
        {
            await Task.Run(() =>GuiEffector.OpacityMagicToMax(viewModel, 0.005f, 1, false));
            Task.Run(() => {
                rotationThread = Thread.CurrentThread;
                GuiEffector.RotationMagic(viewModel, 0.6f, 1, 100, 1000, false);
                });

        }

        async Task<bool> SignInAsync(string login, string password)
        {
            viewModel.InfoText = "Rozpoczynam logowanie";
            requestManager = new RequestManager(login, password);
            viewModel.InfoText = "Trwa logowanie...";
            return await Task.Run(() => requestManager.SignIn());
        }

        async Task<List<Grade>> GetGradesAsync()
        {
            return await requestManager.GetGradesAsync(null);
        }

        bool SignIn(string login, string password)
        {
            viewModel.InfoText = "Rozpoczynam logowanie";
            RequestManager requestManager = new RequestManager(login, password);
            viewModel.InfoText = "Trwa logowanie...";
            return requestManager.SignIn();
        }
    }
}
