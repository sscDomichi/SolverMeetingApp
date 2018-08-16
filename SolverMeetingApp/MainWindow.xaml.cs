using System.Windows;

namespace SolverMeetingApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        AttendanceManager attendanceMan = null;
        public MainWindow()
        {
            InitializeComponent();

            attendanceMan = new AttendanceManager();
            attendanceMan.AttendanceManagerMain();

        }
    }
}
