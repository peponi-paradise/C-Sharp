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

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameInput.Text) && !string.IsNullOrWhiteSpace(CompanyInput.Text) && !string.IsNullOrWhiteSpace(PhoneNumberInput.Text))
            {
                var personData = new PersonData(NameInput.Text, CompanyInput.Text, PhoneNumberInput.Text);
                if (!PersonList.Items.Contains(personData)) PersonList.Items.Add(personData);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (PersonList.SelectedItem != null) PersonList.Items.Remove(PersonList.SelectedItem);
        }
    }

    public record PersonData
    {
        public string Name { get; init; }
        public string Company { get; init; }
        public string PhoneNumber { get; init; }

        public PersonData(string name, string company, string phoneNumber)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
        }
    }
}