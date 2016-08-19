using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpExam1
{
    class Friend
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return String.Format("UserId : {0}\n DisplayName : {1}\n", UserId, DisplayName);
        }
    }
}
