using System;
using System.Collections.Generic;
using System.Diagnostics;
using UEAssistantMobile.ViewModels;
using Xamarin.Forms;

/*
TODO: Create Grades view and bind with GradeViewModel
    */
namespace UEAssistantMobile
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MenuViewModel> MenuElements { get; set; }
        private List<Grade> Grades;
        public MainPage(List<Grade> grades)
        {
            InitializeComponent();

            Grades = grades;
            MenuElements = new List<MenuViewModel> {
                new MenuViewModel {Id = 1, ElementImageSource="gradeIcon.png", ElementName="Oceny" },
                new MenuViewModel {Id = 2, ElementImageSource="planIcon.png", ElementName="Plan zajęć" }
            };
            this.BindingContext = this;
            Detail = new NavigationPage(new GradesPage(Grades));

            menuListView.ItemsSource = MenuElements;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            switch((e.Item as MenuViewModel).Id)
            {
                case 1:
                    Detail = new NavigationPage(new GradesPage(Grades));
                    HideMasterPage();
                    break;

                case 2:
                    Detail = new NavigationPage();
                    HideMasterPage();
                    break;
                        
            }
        }

        void HideMasterPage()
        {
            menuListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}
