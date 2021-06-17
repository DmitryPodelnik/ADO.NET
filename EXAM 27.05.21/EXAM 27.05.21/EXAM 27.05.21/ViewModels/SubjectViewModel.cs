using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EXAM_27._05._21.Models;
using EXAM_27._05._21.Views;
using Microsoft.EntityFrameworkCore;

namespace EXAM_27._05._21.ViewModels
{
    class SubjectViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private SubjectEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddSubject(_window.textName.Text, Byte.Parse(_window.textCourse.Text), Int16.Parse(_window.textHours.Text));
                    else
                        EditSubject();
                }));
            }
        }
        public SubjectViewModel(SubjectEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textName.Text = fullString.Substring(fullString.IndexOf(";") + 1, fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";"));
                _window.textCourse.Text = fullString.Substring(fullString.LastIndexOf(";") - 1, 1);
                _window.textHours.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
            }
        }

        private RelayCommand _closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return _closeWindow =
                (_closeWindow = new RelayCommand(obj =>
                {
                    _window.Close();
                }));
            }
        }
        public async Task EditSubject()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editSubject = await StepAcademyDataBase.Context.Subjects.FirstOrDefaultAsync(a => a.Id == id);
            if (editSubject != null)
            {
                editSubject.Name = _window.textName.Text;
                editSubject.Course = Byte.Parse(_window.textCourse.Text);
                editSubject.Hours = Int16.Parse(_window.textHours.Text);

                //StepAcademyDataBase.Context.Entry(editSubject).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editSubject);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Subject has been successfully edited!");
        }

        public async Task AddSubject(string name, byte course, short hours)
        {
            var newSubject = new Subject
            {
                Name = name,
                Course = course,
                Hours = hours
            };
            await StepAcademyDataBase.Context.Subjects.AddAsync(newSubject);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new subject has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textName.Text = "";
                _window.textCourse.Text = "";
                _window.textHours.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
