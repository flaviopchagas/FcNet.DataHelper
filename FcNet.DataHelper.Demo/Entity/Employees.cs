using System;
using System.ComponentModel.DataAnnotations;

namespace FcNet.DataHelper.Demo.Entity
{
    public class Employees
    {
        [Key]
        public int EmployeeID;
        public string LastName;
        public string FirstName;
        public string Title;
        public string TitleOfCourtesy;
        public DateTime BirthDate;
        public DateTime HireDate;
        public string Address;
        public string City;
        public string Region;
        public string PostalCode;
        public string Country;
        public string HomePhone;
        public string Extension;
        public string Notes;
        public int ReportsTo;
        public string PhotoPath;
    }
}
