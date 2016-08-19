using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpExam1
{
    class GZipUtil
    {

        private class WrapperBool
        {
            public bool Value { get; set; }
        }

        private static WrapperBool isSupport = null;

        public static bool IsSupported()
        {
            if (null == isSupport)
                isSupport.Value = TestGZip();

            return isSupport.Value;
        }

        public static bool TestGZip()
        {
            try
            {
                System.Text.Encoding.UTF8.GetBytes("hello");
            } catch (Exception e)
            {

            }
            return true;
        }
    }
}
