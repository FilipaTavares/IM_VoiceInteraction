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
    }
}
