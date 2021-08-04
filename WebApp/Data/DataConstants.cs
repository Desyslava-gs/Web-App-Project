using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int MakeMaxLength = 20;
            public const int ModelMaxLength = 30;
            public const int YearMinValue = 1900;
            public const int YearMaxValue = 2100;

            public const int PlateNumberMaxLength = 8;
            public const int VinNumberMaxLength = 50;
            public const int ColorMaxLength = 10;

            public const int MakeMinLength = 2;
            public const int ModelMinLength = 2;
            public const int ColorMinLength = 3;
            public const int VinNumberMinLength = 6;
            public const int DescriptionMinLength = 4;
            public const string RegexPlateNumber = "^[A-Z]{2}[0-9]{4}[A-Z]{2}$";
        }

        public class FuelType
        {
            public const int NameMaxLength = 10;
        }

        public class Repair
        {
            public const int NameMaxLength =40;
            public const string PriceFormat= "decimal(15,2)";
        }

        public class RepairType
        {
            public const int NameMaxLength =28;
        }

        public class Part
        {
            public const int NameMaxLength =50;
            public const string PriceFormat= "decimal(15,2)";
        }

        public class Provider
        {
            public const int NameMaxLength = 60;
            public const int PhoneNumberMaxLength = 30;
            public const int CompanyNumberMaxLength = 10;
            public const int IbanNumberMaxLength = 34;
            public const int AddressMaxLength = 120;
        }
    }
}
