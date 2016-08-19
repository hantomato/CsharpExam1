using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpExam1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 123;

            object o = i;

            Console.WriteLine("i : {0}", i); // 456
            Console.WriteLine("o : {0}", o); // 123
            Console.WriteLine("check ss: {0}", IsBoxed2(i));
            Console.WriteLine("check ss: {0}", IsBoxed2(o));
            Console.WriteLine("IsReferenceType aa : {0}", IsReferenceType(i));
            Console.WriteLine("IsReferenceType aa : {0}", IsReferenceType(o));

            string ssss = "mmm";
            String ss = "abc";
            object oo = ss;
            ss = "def";
            Console.WriteLine("ss : {0}", ss); // 456
            Console.WriteLine("oo : {0}", oo); // 123
            Console.WriteLine("check ss: {0}", IsBoxed2(ss));
            Console.WriteLine("check oo: {0}", IsBoxed2(oo));

            
            Console.WriteLine("IsReferenceType ss : {0}", IsReferenceType(ss));
            Console.WriteLine("IsReferenceType oo : {0}", IsReferenceType(oo));
            Console.WriteLine("----");
            Console.WriteLine(typeof(string).IsPrimitive);          // false
            Console.WriteLine(typeof(String).IsPrimitive);          // false
            Console.WriteLine(typeof(int).IsPrimitive);             // true
            Console.WriteLine(typeof(System.Int32).IsPrimitive);    // true
        }
        public static bool IsReferenceType<T>(T input)
        {
            object surelyBoxed = input;
            return object.ReferenceEquals(surelyBoxed, input);
        }

        public static bool IsBoxed2<T>(T value)
        {
            return
                (typeof(T).IsInterface || typeof(T) == typeof(object)) &&
                value != null &&
                value.GetType().IsValueType;
        }

        public static bool IsBoxed(object item)
        {
            return true;
        }

        public static bool IsBoxed<T>(T item) where T : struct
        {
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("func start");
            Console.WriteLine("thd : {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            // public IAsyncResult BeginInvoke(
            //Delegate method
            Action<bool> tempAct1 = (bb) =>
            {
                
                Console.WriteLine("this is tempAct : {0}", bb);
                Console.WriteLine("thd 2 : {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            };

            Action<bool, int> tempAct2 = (bb, ii) =>
            {
                Console.WriteLine("this is tempAct : {0}, {1}", bb, ii);
                Console.WriteLine("thd 2 : {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            };

            tempAct2.BeginInvoke(true, 40, null, null);
            Console.WriteLine("func end");
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // yield test
            foreach (int item in yieldNumber())
            {
                Console.WriteLine(item);
                if (item == 2)
                    break;
            }

            foreach (int item in yieldNumber())
            {
                Console.WriteLine(item);
                if (item == 2)
                    break;
            }

        }

        private IEnumerable<int> yieldNumber()
        {
            //while (true)
            //{
                yield return 1;
            yield break;
            yield return 2;
                yield return 4;
                yield return 5;
            //}
        }
    }
}
