
using System;
using System.Diagnostics;
using System.Reflection;

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
            DataManager.RegisterCardInfo[] readData = dataManager.GetRegisterCardInfo();

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
            Console.WriteLine(MethodBase.GetCurrentMethod().Name + ", IDm(" + idm + ")");
        }
    }
}
