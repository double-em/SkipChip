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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace SkipChip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> persons = new List<Person>();
        private Person currentPerson = new Person("person-icon.png", "no", 0, new List<string>());
        private int personIndex = 0;
        private bool personNoFind = false;
        public MainWindow()
        {
            InitializeComponent();
            List<string> brianOffenses = new List<string>();
            brianOffenses.Add("(1/1 - 2018: Slog en mand ned på Torvet");
            List<string> marianneOffenses = new List<string>();
            List<string> hanneOffenses = new List<string>();

            persons.Add(new Person("brian.jpg", "Brian", 21, brianOffenses));
            persons.Add(new Person("marianne.jpg", "Marianne", 69, marianneOffenses));
            persons.Add(new Person("hanne.jpg", "Hanne", 31, hanneOffenses));
            KeyDown += new KeyEventHandler(SimulateScanIn);
        }

        private void SimulateScanIn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1 && persons.Count > personIndex)
            {
                ListBox.Items.Clear();
                ScanIn(persons[personIndex]);
                personIndex++;
            }
        }

        void ScanIn(Person p)
        {
            ProfilePic.Source = p.ProfilePic;
            NameAge.Content = p.Name + ", " + p.Age;
            if (p.offenses.Count > 0)
            {
                foreach (string offense in p.offenses)
                {
                    ListBox.Items.Add(offense);
                }
            }
            else
            {
                ListBox.Items.Add("Ingen bemærkninger");
            }
            StackPanel newPanel = new StackPanel();
            Label name = new Label();
            DateTime date = DateTime.Today;
            Label dateLabel = new Label();
            dateLabel.Content = date;

            Image profileImage = new Image();
            profileImage.Height = 50;
            profileImage.Source = p.ProfilePic;

            name.Content = p.Name;

            newPanel.Children.Add(dateLabel);
            newPanel.Children.Add(profileImage);
            newPanel.Children.Add(name);
            History.Items.Add(newPanel);
        }

        private void btnFindBruger_Click(object sender, RoutedEventArgs e)
        {
            Message.Content = "";
            ProfilePicFind.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "person-icon.png")));
            Person p = findPerson(tbBruger.Text);
            if (p != null)
            {
                currentPerson = p;
                ProfilePicFind.Source = p.ProfilePic;
                NameAgeReg.Content = p.Name + ", " + p.Age;
                DatePicker.IsEnabled = true;
                Offense.IsEnabled = true;
            }
            else
            {
                tbBruger.Text = "Kunne ikke finde personen.";
                personNoFind = true;
                ProfilePic.Source = null;
                NameAgeReg.Content = "Name, Alder";
            }
        }

        Person findPerson(string name)
        {
            if (name != "")
            {
                foreach (var person in persons)
                {
                    if (person.Name.Contains(name))
                    {
                        return person;
                    }
                }
            }
            return null;
        }

        private void AddOffense_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate != null  && Offense.Text != "")
            {
                var date = DatePicker.SelectedDate.Value;
                currentPerson.offenses.Add("(" + date.Day + "/" + date.Month + " - " + date.Year + "): " + Offense.Text);
                DatePicker.SelectedDate = null;
                Offense.Text = string.Empty;
                AddOffense.IsEnabled = false;
                Message.Content = "Bemærkning Tilføjet!";
            }
            else
            {
                Message.Content = "Husk alle felter!";
            }
            
        }

        private void Offense_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddOffense.IsEnabled = true;
        }

        private void TiScanning_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            if (personIndex > 0)
            {
                ScanIn(persons[personIndex - 1]);
            }
            
        }

        private void TbBruger_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (personNoFind)
            {
                tbBruger.Text = "";
                personNoFind = false;
            }
        }
    }
}
