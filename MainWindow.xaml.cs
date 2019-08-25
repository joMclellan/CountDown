using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan timeRemaining;
        private DateTime ZeroDate = new DateTime(2016, 09, 9, 17, 0, 0);
        private DateTime holiday = new DateTime(2016, 09, 05);
        private DateTime Class = new DateTime(2016, 09, 08);
        private int daysOff;

        public MainWindow()
        {
            InitializeComponent();

            adjustDaysOff();
            GC.Collect();
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(20);
            t.Tick += t_Tick;
            timeRemaining = ZeroDate - DateTime.Now;
            t.Start();
  
        }

        private void adjustDaysOff()
        {
            timeRemaining = ZeroDate - DateTime.Now;
            DateTime today = DateTime.Now;

            daysOff = 0;

            for (int i = 0; i < timeRemaining.Days; ++i)
            {
                if (today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Saturday || today.Date == holiday || today.Date == Class)
                    ++daysOff;
                today += TimeSpan.FromDays(1);
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            if (today.DayOfWeek != DayOfWeek.Sunday && today.DayOfWeek != DayOfWeek.Saturday && !(today.Month == holiday.Month && today.Day == holiday.Day && today.Year == holiday.Year) && !(today.Month == Class.Month && today.Day == Class.Day && today.Year == Class.Year))
            {
                timeRemaining = ZeroDate - DateTime.Now - TimeSpan.FromDays(daysOff);

                txtBlkCountDown.Text = timeRemaining.ToString("dd") + " Days\n" + timeRemaining.ToString(@"hh\:mm\:ss\.fff");
                Title = timeRemaining.ToString("dd") + " Days " + timeRemaining.ToString(@"hh\:mm\:ss\.fff");
            }
            else
            {
                adjustDaysOff();
            }
        }
    }
}
