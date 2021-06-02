using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EXAM_27._05._21.Models
{
    class Operation
    {
        public string Name { get; private set; } // имя
        public ICommand AddCommand { get; private set; } // команда для кнопки Add
        public ICommand DeleteCommand { get; private set; } // команда для кнопки Delete
        public ICommand EditCommand { get; private set; } // команда для кнопки Delete
    }
}
