using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FelicaLib;

namespace SolverMeetingApp
{
    public class ReadTask
    {
        private const int oneSecond = 1000;

        /// <summary>
        /// カード読み込み処理
        /// </summary>
        public void ReadCard()
        {
            Felica felica = new Felica();

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
