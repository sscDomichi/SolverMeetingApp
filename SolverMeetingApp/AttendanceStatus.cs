using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverMeetingApp
{
    class AttendanceStatus
    {
        /// <summary>
        /// 出欠状態
        /// </summary>
        internal enum Status
        {
            /// <summary>
            /// 出席
            /// </summary>
            ATTENDANCE,
            /// <summary>
            /// 欠席
            /// </summary>
            ABSENCE,
            /// <summary>
            /// 遅刻
            /// </summary>
            ARRIVINGlLATE,
        }

        /// <summary>
        /// 出欠状態情報
        /// </summary>
        internal struct AttendanceStatusInfo
        {
            /// <summary>
            /// 名前
            /// </summary>
            internal string name { get; set; }

            /// <summary>
            /// 出欠状態
            /// </summary>
            internal Status status { get; set; }

            /// <summary>
            /// 到着時間
            /// </summary>
            internal DateTime arrivalTime { get; set; }

            /// <summary>
            /// 備考
            /// </summary>
            internal string remarksColumn { get; set; }
        }

        internal List<AttendanceStatusInfo> attendanceStatusInfo = new List<AttendanceStatusInfo>();

    }
}
