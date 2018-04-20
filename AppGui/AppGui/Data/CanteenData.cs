using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGui
{
    class CanteenData
    {
        private string canteen;
        private string meal;
        private string date;
        private string weekday;
        private int weekdayNr;
        private string disabled;
        private string meat;
        private string fish;
        private string diet;
        private string vegetarian;

        public int intWeekDay() {
            //fast way, but i dont know if 1 appers in 01 format
            //return int.Parse(date.Substring(5, 2));
            return int.Parse(date.Split(' ')[1]); //slower that^^ 
        }

        public string Canteen
        {
            get
            {
                return canteen;
            }

            set
            {
                canteen = value;
            }
        }

        public string Meal
        {
            get
            {
                return meal;
            }

            set
            {
                meal = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Weekday
        {
            get
            {
                return weekday;
            }

            set
            {
                weekday = value;
            }
        }

        public int WeekdayNr
        {
            get
            {
                return weekdayNr;
            }

            set
            {
                weekdayNr = value;
            }
        }

        public string Disabled
        {
            get
            {
                return disabled;
            }

            set
            {
                disabled = value;
            }
        }

        public string Meat
        {
            get
            {
                return meat;
            }

            set
            {
                meat = value;
            }
        }

        public string Fish
        {
            get
            {
                return fish;
            }

            set
            {
                fish = value;
            }
        }

        public string Diet
        {
            get
            {
                return diet;
            }

            set
            {
                diet = value;
            }
        }

        public string Vegetarian
        {
            get
            {
                return vegetarian;
            }

            set
            {
                vegetarian = value;
            }
        }

    }
}
