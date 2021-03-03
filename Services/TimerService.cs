using System;
using System.Collections.Generic;
using System.Linq;
using TestRazor.Model;
using System.Threading.Tasks;

namespace TestRazor.Services
{
    public class TimerService
    {
        public static bool IsEndLotTime(Item item, DateTime startTime)
        {
           // while(DateTime.Now!=(startTime.TimeOfDay+item.Time.TimeOfDay.))

            //while((DateTime.Now-startTime).TotalHours<item.Time.Hour)
            //{
            //    return false;
            //}
            return true;
        }
        //public static void RunTimer(int h)
        //{
        //    var tmp = DateTime.Now;

        //}
    }
}
