
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
            //Console.WriteLine(MethodBase.GetCurrentMethod().Name + ", IDm(" + idm + ")");

            DataManager.RegisterCardInfo[] readData = dataManager.GetRegisterCardInfo();
            for ( int i = 0; i < readData.Length; i++)
            {
                if (readData[i].idm == idm)
                {
                    Console.WriteLine("登録済データ " + readData[i].name);
                }
                else
                {
                    Console.WriteLine("非登録 " + idm);
                }
            }
        }
    }
}
