using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// http://nshj.tistory.com/21
/// 
/// thread 상태들
/// - unstarted : 쓰레드 Start() 호출 전 상태
/// - Running : 쓰레드가 동작중인 상태
/// - waitSleepJoin : 쓰레드가 블록된 상태. 쓰레드에 Monitor.Enter(), Thread.Sleep(), Thread.Join() 호출하면 이 상태가 됨.
/// - Aborted : 쓰레드가 취소된 상태. Thread.Abort() 호출하면 이 상태가 됨. 이건 다시 Stop 상태로 전환됨.
/// - Stopped : 쓰레드가 중지된 상태.
///
/// - suspended :  쓰레드가 일시 중단 상태. Thread.Suspend() 호출하면 이 상태가 됨. Thread.Resume() 하면 재가동됨.
/// - background : 
/// 
/// * Join : A_thread.join() : 이 코드를 수행한 쓰레드가 대기한다. A_thread가 종료될때까지.
/// 
/// </summary>
namespace CsharpExam1
{
    public partial class Form2 : Form
    {
        private Thread thd;
        public Form2()
        {
            InitializeComponent();
        }

        private void threadProc()
        {
            Logger.log("thread start");
            try
            {
                for (int i=0; i<20; ++i)
                {
                    Thread.Sleep(100);
                    Logger.log(".. sleep 1");
                    Thread.Sleep(100);
                    Logger.log(".. sleep 2");
                    Thread.Sleep(100);
                    Logger.log(".. sleep 3");

                    Logger.log(".. sleep after:" + i);
                }
            }
            catch (Exception e)
            {
                Logger.log("[!!Exception!!] thread ThreadInterruptedException :" + e);
            }
            finally
            {
                Logger.log("thread finally");
            }
            Logger.log("thread end");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // thread new
            thd = new Thread(threadProc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // thread start
            thd.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // thread status
            Logger.log("Thd status: " + thd.ThreadState);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // thread join
            Logger.log("join 호출전");
            thd.Join();
            Logger.log("join 호출후.(thd 의 작업이 종료됨)");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            thd.Abort();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            thd.Suspend();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            thd.Resume();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            thd.Interrupt();
            
        }
    }
}
