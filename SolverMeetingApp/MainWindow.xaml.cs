using System.Windows;

namespace SolverMeetingApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AttendanceManager.AttendanceManagerMain();
        }

		/// <summary>
		/// Dummyカード読み取りボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DummyReadButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
