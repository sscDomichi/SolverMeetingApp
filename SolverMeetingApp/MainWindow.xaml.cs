using SolverMeetingApp.data;
using System;
using System.Collections.Generic;
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
		DataManager dataMng = null;


		public MainWindow()
        {
            InitializeComponent();

			dataMng = new DataManager();

			attendanceMan = new AttendanceManager(dataMng);
			//通知登録
			attendanceMan.RegisterChangeStateNotify(ChangeStateNotify);
			//動作開始
			attendanceMan.AttendanceManagerMain();

			//ダミーモード準備
			StartDummyMode();

			InitAttendancePanel();
		}

		/// <summary>
		/// ステータス変更通知受信
		/// 変更された情報をもとに、表示を変更する
		/// </summary>
		/// <param name="name"></param>
		void ChangeStateNotify(string name)
		{
			//表示を更新する
			Console.WriteLine("ChangeStateNotify() name " + name);
		}

		/// <summary>
		/// 従業員出欠パネルの初期化
		/// </summary>
		private void InitAttendancePanel()
		{
			//DataManager経由で表示メンバーを取得し、
			List<string> memberList = dataMng.GetDisplayMember();

			//人数分のコントロールを作成する。
			foreach (string member in memberList)
			{
				AttendanceControl attdCtrl = new AttendanceControl();
				attdCtrl.NameText.Text = member;
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
