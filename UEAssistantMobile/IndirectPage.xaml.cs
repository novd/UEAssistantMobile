using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UEAssistantMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace UEAssistantMobile
{
    public partial class IndirectPage : ContentPage
    {
        IndirectViewModel viewModel;
        private string login;
        private string password;

        Thread rotationThread;
        bool isLogoRotating;
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
                viewModel.InfoText = "Zalogowano poprawnie! \n\t\tPobieram oceny...";
                var grades = await GetGradesAsync();
                viewModel.InfoText = "Aktualizuje plan...";
                var localPath = await DownloadScheduleAsync("schedule.pdf");
                viewModel.InfoText = "Wszystko gotowe!";
                isLogoRotating = false;
                await pageLayout.FadeTo(0, 1500, Easing.Linear);
                await Navigation.PushModalAsync(new MainPage(grades, localPath));
            }

            else
            {
                viewModel.InfoText = "Wystąpił problem \n\t\t z logowaniem";
                isLogoRotating = false;
            }

        }


        async Task LogoRotate()
        {
            await pageLayout.FadeTo(1, 1500, Easing.Linear);
            isLogoRotating = true;
            await Task.Run(async () =>
            {
                rotationThread = Thread.CurrentThread;
                while (isLogoRotating)
                {   
                    await logoImg.RotateTo(360, 1500, Easing.Linear);
                    logoImg.Rotation = 0;
                    Thread.Sleep(300);
                }
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

        async Task<string> DownloadScheduleAsync(string filename)
        {
            string fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            await requestManager.DownloadScheduleFile(fullpath);
            return fullpath;
        }

}
}
