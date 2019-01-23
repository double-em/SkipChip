using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SkipChip
{
    public class Person
    {
        public List<string> offenses = new List<string>();
        public BitmapImage ProfilePic;
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string profilePic, string name, int age, List<string> offenses)
        {
            ProfilePic = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, profilePic)));
            Name = name;
            Age = age;
            this.offenses = offenses;
        }
    }
}
