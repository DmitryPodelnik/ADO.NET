using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EXAM_27._05._21.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EXAM_27._05._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StepAcademy : Window
    {
        public StepAcademy()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void mainDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate 
            if (headername == "Id" || headername == "AcademyPhones" || headername == "Students" ||
                headername == "Academy" || headername == "Student" || headername == "Lecturers" ||
                headername == "Leader" || headername == "Gender" || headername == "Group" || 
                headername == "Address" || headername == "Grades" || headername == "Specialty" ||
                headername == "StudentGrade" || headername == "StudentGradeId" || headername == "Groups")
            {
                e.Cancel = true;
            }

            if (headername == "AcademyId")
            {
                e.Column.Header = "Academy";
            }
            else if (headername == "AcademyCity")
            {
                e.Column.Header = "Academy City";
            }
            else if (headername == "AcademyStreet")
            {
                e.Column.Header = "Academy Street";
            }
            else if (headername == "AcademyHouse")
            {
                e.Column.Header = "Academy House";
            }
            else if (headername == "GroupId")
            {
                e.Column.Header = "Group";
            }
            else if (headername == "GenderId")
            {
                e.Column.Header = "Gender";
            }
            else if (headername == "AddressId")
            {
                e.Column.Header = "Address";
            }
            else if (headername == "SpecialtyId")
            {
                e.Column.Header = "Specialty";
            }
            else if (headername == "StudentId")
            {
                e.Column.Header = "Student";
            }
        }

        private void mainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainDataGrid.SelectedItem != null)
            {
                deleteButton.IsEnabled = true;
                editButton.IsEnabled = true;
            }
            else
            {
                deleteButton.IsEnabled = false;
                editButton.IsEnabled = false;
            }
        }
    }
}
