using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


/// <summary>
/// AutoResetEvent, ManualResetEvent
/// ManualResetEvent를 기준으로 설명하자면..
/// 철도 건널목의 차단기와 같다.
///   - set : 차단기 올리기. 쓰레드 통과(동작)
///   - reset : 차단기 내리기. 쓰레드 멈춤
/// set을 하면 차단기는 올라간 상태이므로. WaitOne 구문을 쓰레드가 계속 통과한다.
/// 따라서 딱 1번만 쓰레드가 통과하게 하려면, Set() 호출 후, Reset() 호출해야함.
/// 근데 AutoResetEvent는 Set() 호출 후 Reset() 호출하는 것을 자동으로 해줌.
/// 
/// 참고 : http://blog.naver.com/PostView.nhn?blogId=kjs077&logNo=10171695147
/// 
/// </summary>
namespace CsharpExam1
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        private Thread sendThread;
        //private AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        public Form1()
        {
            InitializeComponent();
        }

        private void threadFunc()
        {
            Console.WriteLine("[start thread] threadFunc");
            while (true)
            {
                Console.WriteLine("while start");
                Thread.Sleep(2000);     // 5000 으로 잡았지만, 하다보니 특정 상황에서는 sleep을 풀고싶을수 있자나. 이때 AutoResetEvent.WaitOne 사용.
                resetEvent.WaitOne();
                Console.WriteLine("thread sleep after");

            }
            Console.WriteLine("[end thread] threadFunc");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ThreadStart
            if (sendThread == null)
            {
                sendThread = new Thread(threadFunc);
                sendThread.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetEvent.Set();
            resetEvent.Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // AutoResetEvent.Wait
            resetEvent.Reset();
            OutputDebugString("mmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
