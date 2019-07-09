using NervousMouse.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NervousMouse
{
    class MainForm : ApplicationContext
    {
        private NotifyIcon trayIcon;

        private DataContext Context;

        public MainForm()
        {
            Context = new DataContext();
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Start being nervous", onClick),
                    new MenuItem("Exit", onExit)}
                ),
                Visible = true
            };
        }

        private void onClick(object sender, EventArgs e)
        {
            Context.isWorking = !Context.isWorking;
            trayIcon.ContextMenu.MenuItems[0].Text = Context.isWorking ? "Stop being nervous" 
                                                                       : "Start being nervous";
        }

        void onExit(object sender, EventArgs e)
        {
            Context.Dispose();
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
