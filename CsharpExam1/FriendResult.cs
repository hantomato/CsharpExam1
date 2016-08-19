using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpExam1
{
    class FriendResult
    {
        public FriendResult()
        {
            Age = 0;
            ServerUrl = "";

        }
        public int Age { get; set; }

        public Friend MyFriend { get; set;  }

        //private String serverUrl = "";
        //public String ServerUrl
        //{
        //    get { return serverUrl; }
        //    set { serverUrl = value; }
        //}

        public String ServerUrl { get; set; }

        private List<Friend> list = new List<Friend>();
        public List<Friend> List { get; set; }

        public override string ToString()
        {
            String str = "ServerUrl : " + ServerUrl + "\n";
            for (int i=0; i< List.Count; ++i)
            {
                str += ("Friend[" + i + "] : " + List[i].ToString());
            }
            return str;
        }
    }
}
