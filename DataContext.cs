using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NervousMouse
{
    class DataContext : INotifyPropertyChanged, IDisposable
    {
        public DataContext()
        {
            taskBeingNervous = Task.Factory.StartNew(() =>
            {
                Random rnd = new Random();
                while (true)
                {
                    if (taskBeingNervousToken.Token.IsCancellationRequested)
                        break;
                    if (!m_PauseTask)
                    {
                        Win32.POINT p = new Win32.POINT();
                        Win32.GetCursorPos(out p);
                        /*Win32.SetCursorPos(p.x + (rnd.Next(50) == 0 ? -1 : 1) * (rnd.Next(50) + 1)
                                         , p.y + (rnd.Next(50) == 0 ? -1 : 1) * (rnd.Next(50) + 1));*/
                        var x = 10;
                        for (int i = 0; i < x; i++)
                        {
                            Win32.SetCursorPos(p.x + i*100, p.y);
                            Thread.Sleep(10*i);
                        }
                        Win32.GetCursorPos(out p);
                        for (int i = 0; i < x; i++)
                        {
                            Win32.SetCursorPos(p.x - i*100, p.y);
                            Thread.Sleep(10*i);
                        }
                    }
                    Thread.Sleep(3000);
                }
            }, taskBeingNervousToken.Token);
        }

        private bool m_PauseTask = true;

        private Task taskBeingNervous;
        private CancellationTokenSource taskBeingNervousToken = new CancellationTokenSource();

        public bool isWorking
        {
            get { return !m_PauseTask; }
            set
            {
                m_PauseTask = !value;
                InvokePropertyChanged(new PropertyChangedEventArgs("isWorking"));
            }
        }

        public void Dispose()
        {
            if (taskBeingNervousToken != null)
            {
                if (taskBeingNervousToken.IsCancellationRequested)
                    return;
                else
                    taskBeingNervousToken.Cancel();
            }
            taskBeingNervous.Wait();
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}
