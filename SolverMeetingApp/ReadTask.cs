using System;
using System.Threading.Tasks;
using FelicaLib;
using System.Windows;
using System.Text;

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
					MessageBox.Show("読み取り失敗");
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

                        String dataStr = String.Empty;
                        byte[] idm = felica.IDm();
                        for (int i = 0; i < idm.Length; i++)
                        {
                            dataStr += idm[i].ToString("X2");
                        }

                        ReadCompleteNotify(ReadStatus.COMPLETE, dataStr);
                        //Console.Write("{0}\r\n", dataStr);

                        Console.Write("IDm: ");
                        foreach (var b in idm)
                        {
                            Console.Write(string.Format("{0:X2}", b));
                        }
                        Console.Write("\r\n");
                    }
                    catch
                    {
                        ReadCompleteNotify(ReadStatus.NO_READ, null);
                    }

                    System.Threading.Thread.Sleep(oneSecond);
                }
            }
        }
    }
}
