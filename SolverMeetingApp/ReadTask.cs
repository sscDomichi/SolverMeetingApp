using System;
using System.Threading.Tasks;
using FelicaLib;
using System.Windows;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace SolverMeetingApp
{
    internal class ReadTask
    {
        internal enum ReadStatus
        {
            /// <summary>
            /// 読み込み失敗
            /// </summary>
            NO_READ,
            /// <summary>
            /// 読み込み成功
            /// </summary>
            COMPLETE,
        }

        /// <summary>
        /// 1秒をミリ秒で定義
        /// </summary>
        private const int oneSecond = 1000;

        /// <summary>
        /// 読み取り完了通知delegate登録
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idm"></param>
        internal delegate void ReadComplete(ReadStatus status, string idm);
        private ReadComplete ReadCompleteNotify;


        /// <summary>
        /// 読み取り完了通知delegate登録
        /// </summary>
        /// <param name="func"></param>
        internal void RegisterReadComplete(ReadComplete func)
        {
            ReadCompleteNotify += func;
        }

        /// <summary>
        /// 非同期で読み取り処理を行う
        /// </summary>
        internal void ReadTaskMain()
        {
            Task task = Task.Run(new Action(() =>
            {
				try
				{
                    ReadCard();
				}
				catch
				{
					//読み取り失敗
					MessageBox.Show("読み取り異常");
				}

			}));
        }

        /// <summary>
        /// カード読み込み処理
        /// </summary>
        private void ReadCard()
        {
            using (Felica felica = new Felica())
            {
                // 読み込みループ
                while (true)
                {
                    try
                    {
                        felica.Polling((int)SystemCode.Any);

                        string dataStr = string.Empty;
                        byte[] idm = felica.IDm();
                        for (int i = 0; i < idm.Length; i++)
                        {
                            dataStr += idm[i].ToString("X2");
                        }

                        ReadCompleteNotify(ReadStatus.COMPLETE, dataStr);
                        //Console.WriteLine(MethodBase.GetCurrentMethod().Name + ", IDm(" + idm + ")");
                    }
                    catch
                    {
                        //ReadCompleteNotify(ReadStatus.NO_READ, null);
                    }

                    System.Threading.Thread.Sleep(oneSecond);
                }
            }
        }
    }
}
