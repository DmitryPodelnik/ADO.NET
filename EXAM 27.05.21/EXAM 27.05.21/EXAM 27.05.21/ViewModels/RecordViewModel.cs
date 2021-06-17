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
    class RecordViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private RecordEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddRecord(Byte.Parse(_window.textCoins.Text), Byte.Parse(_window.textCourse.Text), _window.textSubject.Text);
                    else
                        EditRecord();
                }));
            }
        }
        public RecordViewModel(RecordEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textCoins.Text = fullString.Substring(fullString.IndexOf(";") + 1, fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";"));
                _window.textCourse.Text = fullString.Substring(fullString.LastIndexOf(";") - 1, 1);
                _window.textSubject.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
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

        public async Task EditRecord()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editRecord = await StepAcademyDataBase.Context.Records.FirstOrDefaultAsync(a => a.Id == id);
            if (editRecord != null)
            {
                editRecord.Coins = Byte.Parse(_window.textCoins.Text);
                editRecord.Course = Byte.Parse(_window.textCourse.Text);
                editRecord.Subject = _window.textSubject.Text;

                //StepAcademyDataBase.Context.Entry(editRecord).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editRecord);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Record has been successfully edited!");
        }

        public async Task AddRecord(byte coins, byte course, string subject)
        {
            var newRecord = new Record
            {
                Coins = coins,
                Course = course,
                Subject = subject
            };
            await StepAcademyDataBase.Context.Records.AddAsync(newRecord);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new record has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textCoins.Text = "";
                _window.textCourse.Text = "";
                _window.textSubject.Text = "";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
