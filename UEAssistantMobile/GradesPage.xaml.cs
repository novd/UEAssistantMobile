using System;
using System.Collections.Generic;
using UEAssistantMobile.ViewModels;
using UEAssistantMobile.Models;

using Xamarin.Forms;

namespace UEAssistantMobile
{
    public partial class GradesPage : ContentPage
    {
        public GradeViewModel gradeViewModel;
        public GradesPage(List<Grade> grades)
        {
            InitializeComponent();
            this.BindingContext = gradeViewModel = new GradeViewModel(grades);
        }

        void OnFieldListItemTapped(object sender, ItemTappedEventArgs e)
        {
            gradeViewModel.SelectedFieldOfStudy = e.Item as FieldOfStudyModel;
            (sender as ListView).SelectedItem = null; //to disable highlight after tap
        }

        void OnEditionListItemTapped(object sender, ItemTappedEventArgs e)
        {
            gradeViewModel.SelectedEditionOfField = e.Item as EditionModel;
            (sender as ListView).SelectedItem = null; //to disable highlight after tap
        }

        void OnSemesterListItemTapped(object sender, ItemTappedEventArgs e)
        {
            gradeViewModel.SelectedSemester = e.Item as SemesterModel;
            (sender as ListView).SelectedItem = null; //to disable highlight after tap
        }

        void OnGradeListItemTapped(object sender, ItemTappedEventArgs e)
        {
            gradeViewModel.SelectedGrade = e.Item as GradeModel;
            (sender as ListView).SelectedItem = null; //to disable highlight after tap
        }

       
    }
}
