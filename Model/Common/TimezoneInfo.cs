using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common {
    public class TimezoneInfoDetails {
        //private int _PropertyOffSet;
        //public int PropertyOffSet
        //{
        //    get { return _PropertyOffSet; }
        //    set
        //    {
        //        _PropertyOffSet = GetOffsetIntoMinutes(value.ToString());

        //    }
        //}
        //private int _ServerOffSet;
        //public int ServerOffSet
        //{
        //    get { return _ServerOffSet; }
        //    set
        //    {
        //        _ServerOffSet = GetOffsetIntoMinutes(value.ToString());

        //    }
        //}

        public String PropertyOffSet { get; set; }
        public string ServerOffSet { get; set; }
        public string ServerTime { get; set; }

        public int ServerTimeOffsetInMinutes { get; set; }
        public int PropertyTimeOffsetInMinutes { get; set; }


        public int TimeZoneDiffrenceInMinutes { get; set; }

        public int GetOffsetIntoMinutes(string Offset) {
            //Converting offset value into minutes with sign(+/-)
            string Sign = Offset.Substring(0, 1);
            string[] HoursAndMinutes = Offset.Substring(1, Offset.Length - 1).Split(':');
            int HoursInMinutes = Convert.ToInt32(HoursAndMinutes[0]) * 60;
            int Minutes = Convert.ToInt32(HoursAndMinutes[1]);
            int TotalMinutes = HoursInMinutes + Minutes;

            return Sign == "+" ? TotalMinutes : -TotalMinutes;
        }

    }
}
