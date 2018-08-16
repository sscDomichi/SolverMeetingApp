using System;

namespace SolverMeetingApp
{
	/// <summary>
	/// 出勤管理
	/// </summary>
	internal class AttendanceManager
	{
        internal AttendanceManager()
        {

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
            Console.Write("{0}\r\n", idm);
        }
    }
}
