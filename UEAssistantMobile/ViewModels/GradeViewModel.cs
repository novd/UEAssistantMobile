using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using UEAssistantMobile.Models;

namespace UEAssistantMobile.ViewModels
{
    public class GradeViewModel : INotifyPropertyChanged
    {
        private FieldOfStudyModel _visibleField;
        private EditionModel _visibleEdition;
        private SemesterModel _visibleSemester;
        private GradeModel _visibleGrade;

        private List<GradeModel> _allGrades;

        List<GradeModel> gradeModels;
        public List<GradeModel> Grades
        {
            get { return gradeModels; }
            set
            {
                gradeModels = value;
                OnPropertyChanged("Grades");
            }
        }

        List<FieldOfStudyModel> fieldsOfStudy;
        public List<FieldOfStudyModel> FieldsOfStudy
        {
            get { return fieldsOfStudy; }
            set
            {
                fieldsOfStudy = value;
                OnPropertyChanged("FieldsOfStudy");
            }
        }

        List<EditionModel> editions;
        public List<EditionModel> EditionsOfField
        {
            get { return editions; }
            set
            {
                editions = value;
                OnPropertyChanged("EditionsOfField");
            }
        }

        List<SemesterModel> semesters;
        public List<SemesterModel> Semesters
        {
            get { return semesters; }
            set
            {
                semesters = value;
                OnPropertyChanged("Semesters");
            }
        }

        //when one of fields is tapped, then set matching editions
        //Every edition is binded with same property
        //Every semester is binded with same property
        // so just change value of this property when specific item is selected
        FieldOfStudyModel selectedFieldOfStudy;
        public FieldOfStudyModel SelectedFieldOfStudy
        {
            get { return selectedFieldOfStudy; }
            set
            {
                selectedFieldOfStudy = value;
                UpdateVisibilityOfField(selectedFieldOfStudy);

                selectedFieldOfStudy.Editions = EditionsOfField = GetEditions(selectedFieldOfStudy);

                OnPropertyChanged("SelectedFieldOfStudy");
            }
        }


        EditionModel selectedEditionOfField;
        public EditionModel SelectedEditionOfField
        {
            get { return selectedEditionOfField; }
            set
            {
                selectedEditionOfField = value;
                UpdateVisibilityOfEdition(selectedEditionOfField);

                selectedEditionOfField.Semesters = Semesters = GetSemesters(SelectedFieldOfStudy, selectedEditionOfField);

                OnPropertyChanged("SelectedEditionOfField");
            }
        }

        SemesterModel selectedSemester;
        public SemesterModel SelectedSemester
        {
            get { return selectedSemester; }
            set
            {
                selectedSemester = value;
                UpdateVisibilityOfSemester(selectedSemester);

                selectedSemester.Grades = Grades = _allGrades.Where(grade => (grade.FieldOfStudy == SelectedFieldOfStudy.NameOfField) &&
                                                   (grade.Edition == SelectedEditionOfField.NameOfEdition) &&
                                                   (grade.Semester == selectedSemester.NumberOfSemester))
                                                   .Select(grade => grade).ToList();

                OnPropertyChanged("SelectedSemester");
            }
        }

        GradeModel selectedGrade;
        public GradeModel SelectedGrade
        {
            get { return selectedGrade; }
            set
            {
                selectedGrade = value;
                UpdateVisibilityOfGrade(selectedGrade);

                OnPropertyChanged("SelectedGrade");
            }
        }
        //Constructors =================
        public GradeViewModel()
        {
            Grades = GetGradeModels();
        }

        public GradeViewModel(List<Grade> grades)
        {

            _allGrades = GetGradeModels(grades);
            FieldsOfStudy = GetFieldsOfStudy();

        }
        //==============================


        public void UpdateVisibilityOfGrade(GradeModel grade)
        {
            //if any grade is not visible, then updated grade will be shown
            if (_visibleGrade == null)
            {
                _visibleGrade = grade;
                _visibleGrade.Visibility = true;
            }
            //if any grade is visible
            else
            {
                //and visible grade is updated grade, then just hide it
                if (_visibleGrade == grade)
                {
                    grade.Visibility = false;
                    _visibleGrade = null;
                }
                //in other case, hide current grade and show updated grade
                else
                {
                    _visibleGrade.Visibility = false;
                    _visibleGrade = grade;
                    _visibleGrade.Visibility = true;
                }
            }
        }

        public void UpdateVisibilityOfEdition(EditionModel edition)
        {
            //if any grade is not visible, then updated grade will be shown
            if (_visibleEdition == null)
            {
                _visibleEdition = edition;
                _visibleEdition.Visibility = true;
            }
            //if any grade is visible
            else
            {
                //and visible grade is updated grade, then just hide it
                if (_visibleEdition == edition)
                {
                    edition.Visibility = false;
                    _visibleEdition = null;
                }
                //in other case, hide current grade and show updated grade
                else
                {
                    _visibleEdition.Visibility = false;
                    _visibleEdition = edition;
                    _visibleEdition.Visibility = true;
                }
            }
        }

        public void UpdateVisibilityOfField(FieldOfStudyModel field)
        {
            //if any grade is not visible, then updated grade will be shown
            if (_visibleField == null)
            {
                _visibleField = field;
                _visibleField.Visibility = true;
            }
            //if any grade is visible
            else
            {
                //and visible grade is updated grade, then just hide it
                if (_visibleField == field)
                {
                    field.Visibility = false;
                    _visibleField = null;
                }
                //in other case, hide current grade and show updated grade
                else
                {
                    _visibleField.Visibility = false;
                    _visibleField = field;
                    _visibleField.Visibility = true;
                }
            }
        }

        public void UpdateVisibilityOfSemester(SemesterModel semester)
        {
            //if any grade is not visible, then updated grade will be shown
            if (_visibleSemester == null)
            {
                _visibleSemester = semester;
                _visibleSemester.Visibility = true;
            }
            //if any grade is visible
            else
            {
                //and visible grade is updated grade, then just hide it
                if (_visibleSemester == semester)
                {
                    semester.Visibility = false;
                    _visibleSemester = null;
                }
                //in other case, hide current grade and show updated grade
                else
                {
                    _visibleSemester.Visibility = false;
                    _visibleSemester = semester;
                    _visibleSemester.Visibility = true;
                }
            }
        }


        #region Getters
        private List<GradeModel> GetGradeModels()
        {
            List<GradeModel> listOfGrades = new List<GradeModel>();


            return listOfGrades;
        }
        private List<GradeModel> GetGradeModels(List<Grade> grades)
        {
            return grades.Select(grade => grade.ConvertToGradeModel()).ToList();
        }


        private List<FieldOfStudyModel> GetFieldsOfStudy()
        {
            var gradesGroupedByField = _allGrades.GroupBy(grade => grade.FieldOfStudy);

            return gradesGroupedByField.Select(grade =>
                                               new FieldOfStudyModel
                                               {
                                                   NameOfField = grade.Key
                                               }).ToList();
        }

        private List<EditionModel> GetEditions(FieldOfStudyModel fieldOfStudy)
        {
            // selecting only from grades where fieldOfStudy is equal to given fieldOfStudy
            return _allGrades.Where(grade => grade.FieldOfStudy == fieldOfStudy.NameOfField) 
                             .Select(grade => grade.Edition).Distinct()
                             .Select(edition => new EditionModel { NameOfEdition = edition }).ToList();


        }

        private List<SemesterModel> GetSemesters(FieldOfStudyModel fieldOfStudy, EditionModel editionOfField)
        {
            // selecting only from grades where fieldOfStudy is equal to given fieldOfStudy
            // && editionOfField is equal to given editionOfField
            return _allGrades.Where(grade => (grade.FieldOfStudy == fieldOfStudy.NameOfField) &&
                                             (grade.Edition == editionOfField.NameOfEdition))
                             .Select(grade => grade.Semester).Distinct().OrderBy(semester => int.Parse(semester))
                             .Select(semester => new SemesterModel { NumberOfSemester = semester }).ToList();


        }
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
