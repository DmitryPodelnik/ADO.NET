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
    class LeaderViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private LeaderEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddLeader(Int32.Parse(_window.textStudent.Text), Int32.Parse(_window.textGroup.Text));
                    else
                        EditLeader();
                }));
            }
        }
        public LeaderViewModel(LeaderEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textStudent.Text = fullString.Substring(fullString.IndexOf("Learner = ") + 10, fullString.Substring(fullString.IndexOf("Learner = ") + 10).IndexOf(","));
                _window.textGroup.Text = fullString.Substring(fullString.IndexOf("Class = ") + 8, (fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3));
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

        public async Task EditLeader()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            string firstNameStudent = _window.textStudent.Text.Substring(0, _window.textStudent.Text.IndexOf(" "));
            string lastNameStudent = _window.textStudent.Text.Substring(_window.textStudent.Text.IndexOf(" ") + 1);
            

            var student = await StepAcademyDataBase.Context.Students.FirstOrDefaultAsync(a => a.FirstName + " " + a.LastName == 
                                                                                         firstNameStudent + " " + lastNameStudent);
            if (student == null)
            {
                MessageBox.Show("You entered incorrect student!", "Error");
                return;
            }

            var group = await StepAcademyDataBase.Context.Groups.FirstOrDefaultAsync(a => a.Name == _window.textGroup.Text);
            if (group == null)
            {
                MessageBox.Show("You entered incorrect group!", "Error");
                return;
            }

            var editLeader = await StepAcademyDataBase.Context.Leaders.FirstOrDefaultAsync(a => a.Id == id);
            if (editLeader != null)
            {
                editLeader.StudentId = student.Id;
                editLeader.GroupId = group.Id;

                //StepAcademyDataBase.Context.Entry(editLeader).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editLeader);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Leader has been successfully edited!");
        }

        public async Task AddLeader(int studentID, int groupID)
        {
            var newLeader = new Leader
            {
                StudentId = studentID,
                GroupId = groupID
            };
            await StepAcademyDataBase.Context.Leaders.AddAsync(newLeader);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new leader has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textStudent.Text = "";
                _window.textGroup.Text = "";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
