using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverMeetingApp
{
	class DummyRead
	{
		readonly private string NO_READER_MODE_FILE = @".\data\NO_READER.txt";

		System.IO.StreamReader file = null;
		internal DummyRead()
		{
			OpenDummyReadFile();
		}

		/// <summary>
		/// ダミーファイルオープン
		/// </summary>
		private void OpenDummyReadFile()
		{
			file = new System.IO.StreamReader(NO_READER_MODE_FILE);
		}

		/// <summary>
		/// ダミーのIDm取得
		/// </summary>
		/// <returns></returns>
		internal string GetDummyIdm()
		{
			// 1行読み込み
			string line = file.ReadLine();
			if (line == null)
			{
				//何も読めなかった(末端)なら、先頭に戻してもう一度読み込み
				file.Dispose();

				OpenDummyReadFile();
				line = file.ReadLine();

				if (line == null)
				{
					//開きなおしてもNULLなら異常
					throw new Exception("dummy file error.");
				}
			}
			return line;
		}
	}
}
