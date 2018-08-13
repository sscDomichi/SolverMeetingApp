using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FelicaLib;

namespace SolverMeetingApp
{
    public class CardObj
    {
        //private const time = 1000;

        public void ReadCard()
        {
            Felica felica = new Felica();

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

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
