using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using DevExpress.Mvvm.CodeGenerators;
using System.ComponentModel;
using FirstWPFDXApplication.Models;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace FirstWPFDXApplication.ViewModels
{
    [GenerateViewModel]
    public partial class MainViewModel
    {
        [GenerateProperty]
        private BindingList<PersonData> persons;

        [GenerateProperty]
        private PersonData currentEditInfo;

        public MainViewModel()
        {
            persons = new BindingList<PersonData>();
            currentEditInfo = new PersonData();
        }

        [GenerateCommand]
        private void AddPerson(PersonData data)
        {
            PersonData person = new PersonData();
            person.FirstName = data.FirstName;
            person.LastName = data.LastName;
            person.PhoneNumber = data.PhoneNumber;
            person.ID = data.ID;
            Persons.Add(person);
        }

        private bool CanAddPerson(PersonData data)
        {
            if (!PersonDataValidation(data)) return false;
            else return true;
        }

        private bool PersonDataValidation(PersonData data)
        {
            if (data == null) return false;
            Regex regex = new Regex(@"^\(?([0-10]{3})\)?[-. ]?([0-10]{4})[-. ]?([0-10]{4})$");
            var match = regex.Match(data.PhoneNumber);
            if (!match.Success) return false;
            return true;
        }
    }
}