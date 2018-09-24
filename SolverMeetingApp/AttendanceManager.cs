
using SolverMeetingApp.data;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using static SolverMeetingApp.AttendanceStatus;

namespace SolverMeetingApp
{
	/// <summary>
	/// 出勤管理
	/// </summary>
	internal class AttendanceManager
	{
		DataManager dataManager;

		/// <summary>
		/// ステータス変更状態通知
		/// </summary>
		/// <param name="status"></param>
		/// <param name="idm"></param>
		internal delegate void ChangeState(AttendanceStatusInfo status);

		private ChangeState ChangeStateNotify;

		internal AttendanceManager(DataManager dataMng)
        {
			dataManager = dataMng;
        }

        internal void AttendanceManagerMain()
		{
            ReadTask readTask = new ReadTask();
            readTask.RegisterReadComplete(ReadComplete);
            readTask.ReadTaskMain();
		}

		/// <summary>
		/// ステータス変更通知(delegate)登録
		/// </summary>
		/// <param name="func"></param>
		internal void RegisterChangeStateNotify(ChangeState func)
		{
			ChangeStateNotify += func;
		}

		/// <summary>
		/// NFC読み取り通知受信(delegate通知)
		/// </summary>
		/// <param name="status"></param>
		/// <param name="idm"></param>
		internal void ReadComplete(ReadTask.ReadStatus status, string idm)
        {
            try
            {
                //Console.WriteLine(MethodBase.GetCurrentMethod().Name + ", IDm(" + idm + ")");
                
				AttendanceStatusInfo info = dataManager.UpdateAttendanceStatus(idm);
				ChangeStateNotify(info);
			}
            catch (MemberListFileException e)
            {
                MessageBox.Show("登録されていないカードです");
            }
        }
    }
}
