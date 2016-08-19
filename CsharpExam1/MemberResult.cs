using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpExam1
{
    class MemberResult
    {
        public string externalOutput;

        public string DisplayName { get; set; }
        public long BbmId { get; set; }
        public bool IsConnected { get; set; }

        public AccessToken AccessToken { get; set; }

        public List<String> Friends { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
