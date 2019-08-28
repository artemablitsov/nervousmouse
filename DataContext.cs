using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
                        Win32.POINT initialPos = new Win32.POINT();
                        Win32.GetCursorPos(out initialPos);
                        int radius = 300;
                        for (double i = -180.0; i < 180.0; i += 1)
                        {
                            double angle = i * System.Math.PI / 180;
                            int x = (int)(radius * System.Math.Cos(angle));
                            int y = (int)(radius * System.Math.Sin(angle));

                            Win32.SetCursorPos(initialPos.x + x + radius, initialPos.y + y);
                            Thread.Sleep(3);
                        }
                        Win32.SetCursorPos(initialPos.x, initialPos.y);
                    }
                    Thread.Sleep(5000);
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
