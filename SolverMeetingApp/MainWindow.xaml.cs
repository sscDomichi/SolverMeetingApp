using System;
using System.Windows;
using static SolverMeetingApp.ReadTask;

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

			//ダミーモード準備
			StartDummyMode();

			InitAttendancePanel();
		}

		/// <summary>
		/// 従業員出欠パネルの初期化
		/// </summary>
		private void InitAttendancePanel()
		{
			//TODO:ファイルか何かから読み込んで、人数分作成する。
			for (int i = 0; i < 5; i++)
			{
				AttendanceControl attdCtrl = new AttendanceControl();
				AttendanceStackPanel.Children.Add(attdCtrl);
			}
		}

		/// <summary>
		/// ダミーモード用ファイルがあれば、カードリーダダミーモード開始
		/// </summary>
		private void StartDummyMode()
		{
			try
			{
				dummyRead = new DummyRead();
				DummyReadButton.IsEnabled = true;
			}
			catch (System.IO.FileNotFoundException)
			{
				// ダミーモードではない。
				DummyReadButton.IsEnabled = false;
			}
		}

		DummyRead dummyRead = null;

		/// <summary>
		/// Dummyカード読み取りボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DummyReadButton_Click(object sender, RoutedEventArgs e)
		{
			string idm = dummyRead.GetDummyIdm();
			Console.WriteLine("DummyModeRead:" + idm);
			attendanceMan.ReadComplete(ReadStatus.COMPLETE, idm);
		}
	}
}
