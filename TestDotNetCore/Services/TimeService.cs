using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDotNetCore.Services
{
    public class TimeService
    {
		public string getTime() => System.DateTime.Now.ToString("hh:mm:ss");
    }
}
