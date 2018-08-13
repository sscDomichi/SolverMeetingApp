using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FelicaLib;
using System.Windows;

namespace SolverMeetingApp
{
    internal class ReadTask
    {
        /// <summary>
        /// 1秒をミリ秒で定義
        /// </summary>
        private const int oneSecond = 1000;

        /// <summary>
        /// 非同期で読み取り処理を行う
        /// </summary>
        internal static void ReadTaskMain()
        {
            Task task = Task.Run(new Action(() =>
            {
                ReadTask cardObj = new ReadTask();
                cardObj.ReadCard();
            }));
        }

        /// <summary>
        /// カード読み込み処理
        /// </summary>
        internal void ReadCard()
        {
            using (Felica felica = new Felica())
            {
                // 読み込みループ
                while (true)
                {
                    try
                    {
                        felica.Polling((int)SystemCode.Any);

                        var idm = felica.IDm();

                        Console.Write("IDm: ");
                        foreach (var b in idm)
                        {
                            Console.Write(string.Format("{0:X2}", b));
                        }
                        Console.Write("\r\n");
                    }
                    catch
                    {
                    }

                    System.Threading.Thread.Sleep(oneSecond);
                }
            }
        }
    }
}
