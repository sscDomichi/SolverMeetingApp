using SolverMeetingApp.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolverMeetingApp
{
    class DataManager
    {
        readonly private string REGISTER_CARD_INFO_FILE_PATH = @".\REGISTER_CARD_INFO.txt";
		private Member member = new Member();

        /// <summary>
        /// カード登録情報
        /// </summary>
        internal struct RegisterCardInfo
        {
            /// <summary>
            /// IDm
            /// </summary>
            internal string idm { get; set; }
            /// <summary>
            /// 名前
            /// </summary>
            internal string name { get; set; }
        }

		internal List<string> GetDisplayMember()
		{
			return member.MemberDisplayList;
		}

		/// <summary>
		/// カード登録情報をファイルから読み込み
		/// </summary>
		/// <returns></returns>
		private byte[] OpenReadRegistetCardInfoFile()
        {
            byte[] readData = new byte[1024];

            // ファイルが存在するか確認
            if (File.Exists(REGISTER_CARD_INFO_FILE_PATH))
            {
                // ファイルOPEN
                using (FileStream fs = File.OpenRead(REGISTER_CARD_INFO_FILE_PATH))
                {
                    if (fs.Length > 0)
                    {
                        fs.Read(readData, 0, readData.Length);
                    }
                }
            }

            return readData;
        }

        /// <summary>
        /// カード登録情報構造体へデータを代入
        /// </summary>
        /// <returns></returns>
        internal RegisterCardInfo[] GetRegisterCardInfo()
        {
            byte[] readData = OpenReadRegistetCardInfoFile();

            // 読み込んだデータを文字列へ変換し、不要なデータ(\0)を削除
            string strData = string.Empty;
            strData += Encoding.GetEncoding("Shift_JIS").GetString(readData);
            strData = strData.TrimEnd('\0');
            //Console.WriteLine(strData);

            // カンマ、改行を区切り文字とし、データを分ける
            string[] splitWord = { ",", "\r\n" };
            string[] splitData = strData.Split(splitWord, StringSplitOptions.RemoveEmptyEntries);

            // カード登録情報構造体へデータを代入
            int oneDataNum = splitData.Count() / 2;
            RegisterCardInfo[] registerCardInfo = new RegisterCardInfo[oneDataNum];

            int dataCount = 0;
            for (int i = 0; i < oneDataNum; i++)
            {
                if (dataCount < splitData.Count())
                {
                    registerCardInfo[i].idm = splitData[dataCount];
                    dataCount++;
                    registerCardInfo[i].name = splitData[dataCount];
                    dataCount++;
                }

                Console.WriteLine(MethodBase.GetCurrentMethod().Name + " idm(" + registerCardInfo[i].idm + ")");
                Console.WriteLine(MethodBase.GetCurrentMethod().Name + " name(" + registerCardInfo[i].name + ")");
            }

            return registerCardInfo;
        }

    }
}
