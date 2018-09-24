
using SolverMeetingApp.data;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace SolverMeetingApp
{
	/// <summary>
	/// 出勤管理
	/// </summary>
	internal class AttendanceManager
	{
		DataManager dataManager;

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
        /// NFC読み取り通知受信(delegate通知)
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idm"></param>
        internal void ReadComplete(ReadTask.ReadStatus status, string idm)
        {
            try
            {
                //Console.WriteLine(MethodBase.GetCurrentMethod().Name + ", IDm(" + idm + ")");

                dataManager.FindIdmFromRegisterCardInfo(idm);
            }
            catch (MemberListFileException e)
            {
                MessageBox.Show("登録されていないカードです");
            }
        }
    }
}
