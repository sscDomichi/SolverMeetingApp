using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SolverMeetingApp.data
{
	/// <summary>
	/// メンバーリストのファイル(.\data\member.csv)情報を読み込み、保持する。
	/// csvフォーマットは
	/// int,string
	/// 
	/// int 0:非表示 0以外の数値:表示
	/// string:氏名
	/// </summary>
	class Member
	{
		/// <summary>
		/// メンバー情報ファイルパス定義
		/// </summary>
		readonly string MEMBER_LIST_FILE = @".\data\member.csv";

		/// <summary>
		/// 表示メンバー一覧
		/// </summary>
		List<string> memberDisplayList = new List<string>();

		/// <summary>
		/// 非表示メンバー一覧
		/// </summary>
		List<string> memberNonDispList = new List<string>();


		internal Member()
		{
			try
			{
				//メンバー一覧ファイル読み込み
				ReadMemberListFile();
			}
			catch (MemberListFileException memListExp)
			{
				MessageBox.Show(memListExp.Message);
			}
			catch
			{
				MessageBox.Show("メンバーリスト異常\ndataフォルダのnmember.csvファイルを確認してください。");
			}
		}

		private void ReadMemberListFile()
		{

			using (StreamReader file = new StreamReader(MEMBER_LIST_FILE, Encoding.GetEncoding("shift_jis")))
			{
				int lineCounter = 0;
				//ファイル全行読み込み
				while (true)
				{
					lineCounter++;
					// 1行を,区切りで読み込み
					string line = file.ReadLine();
					if (line == null)
					{
						//ファイル終端
						break;
					}
					//カンマ区切りで分割
					string[] lines = line.Split(',');

					//読み込んだメンバー情報を振り分け
					SortMemberList(lineCounter, lines);


				}
				dump();

			}
		}

		/// <summary>
		/// 表示/非表示情報から保持先を振り分ける。
		/// </summary>
		private void SortMemberList(int lineCounter, string[] lines)
		{
			if (lines.Length != 2)
			{
				throw new MemberListFileException("member.csv異常(,区切りチェック)です。\n" + lineCounter + "行目を確認してください。");
			}

			try
			{
				//表示種別判定
				if (Convert.ToBoolean(int.Parse(lines[0])))
				{
					//表示メンバーへ追加
					memberDisplayList.Add(lines[1]);
				}
				else
				{
					//非表示メンバーへ追加
					memberNonDispList.Add(lines[1]);
				}

			}
			catch
			{
				throw new MemberListFileException("member.csv異常(表示判定)です。\n" + lineCounter + "行目を確認してください。");
			}
		}

		/// <summary>
		/// デバッグ用:メンバー情報出力
		/// </summary>
		private void dump()
		{
			Console.WriteLine("====表示メンバー出力");
			foreach (string name in memberDisplayList)
			{
				Console.WriteLine(name);
			}
			Console.WriteLine("====非表示メンバー出力");
			foreach (string name in memberNonDispList)
			{
				Console.WriteLine(name);
			}
		}
	}

	class MemberListFileException : Exception
	{
		public MemberListFileException(string message)
			: base(message)
		{
		}
	}


}
