using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using EXAM_27._05._21.Models;
using EXAM_27._05._21.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EXAM_27._05._21.Views
{
    /// <summary>
    /// Interaction logic for SubjectEdition.xaml
    /// </summary>
    public partial class SubjectEdition : Window
    {
        public SubjectEdition()
        {
            InitializeComponent();

            DataContext = new SubjectViewModel(this);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((StepAcademy)Application.Current.MainWindow).mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Subjects.ToListAsync();
        }
    }
}
