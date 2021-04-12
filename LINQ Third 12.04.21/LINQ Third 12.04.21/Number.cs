using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace LINQ_Third_12._04._21
{
    [Serializable]
    public class Number
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Number() { }

        public Number(string name, string number)
        {
            Name = name;
            Phone = number;
        }
    }
}
