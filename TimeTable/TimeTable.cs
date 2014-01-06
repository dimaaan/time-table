using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

/// <summary>
///	����� ��� ������ � ����������� �� ������
/// </summary>
namespace TimeTable {
class Tbl { //=========================================================================

	//					� � � � � � � � �
	public const int DAY_IN_WEEK = 7;

	/// <summary>������������ ���������� ��� � ����</summary>
	public const int MAX_PARA_COUNT = 6;

	/// <summary>���-�� ����� ���/// </summary>
	public const int PARA_TYPE_COUNT = 6;

	public static string LoadedTbl;

	// -------------------------------------------------------------------------------------
	//					� � � � � � � � � � � �
	/// <summary>
	/// ���� �������. 
	/// ��� ���������� ������ ���� ��������� ��������� PARA_TYPES_COUNT
	/// </summary>
	public enum ParaType {
		LECTION,
		PRACTIZE,
		SEMINAR,
		LAB_WORK,
		CONSULTATION,
		ADDITION_LESSON
	}

	public enum WeekType {
		CHET = 0,
		NECHET = 1
	}

	// -------------------------------------------------------------------------------------
	//					� � � � � � � � �
	/// <summary>������ ���������� � ����</summary>
	public struct Para {
		public string Name;
		public ParaType Type;
		public string Location;
	}

	/// <summary>
	/// �������� �������������� ���������� � ���������� (������, ����, �������) 
	/// � ������ ������� ����� ����������.
	/// </summary>
	public struct TblHeader {
		/// <summary>������ ������� ����� ����������</summary>
		public Version Ver;

		/// <summary>�������� ������, � ������� ������ ����������</summary>
		public string Group;

		/// <summary>����, �� ������� ������� ������ � ������ �����������</summary>
		public int Course;

		/// <summary>�������, � ������� ������ ���������� �� ������� ����������</summary>
		public int Term;
	}

	/// <summary>������ ���������� � ����������</summary>
	public struct Holiday {

		/// <summary>���� ��������</summary>
		public DateTime Date;

		/// <summary>�������� ��������</summary>
		public string Name;

		/// <summary>������� �� � ���� ����?</summary>
		public bool NoStudy;
	}

	// -------------------------------------------------------------------------------------
	//					� � � �

	/// <summary>�������� ���� ���� ������</summary>
	public static readonly string[] WeekDayNames;

	/// <summary>���� ��� ("������", "��������" � �.�.)</summary>
	public static readonly string[] ParaTypeNames;

	/// <summary>
	/// ���������� �� 2 ������(3D ������).
	/// 1�� ������ - ������� (0) ��� �� ������ (1) ������.
	/// 2�� - ��� ���� ������, 3�� - ����� ����
	/// </summary>
	public static Para[, ,] Table;

	/// <summary>�������� �������������� ���������� � ����������</summary>
	public static TblHeader Info;

	/// <summary>����������� ��������� �����</summary>
	public static Holiday[] Holidays;

	public static readonly string[] MonthNames;

	// -------------------------------------------------------------------------------------
	//					 � � � � � � � � � � � �
	static Tbl() {
		WeekDayNames = new string[DAY_IN_WEEK];
		WeekDayNames[0] = "�����������";
		WeekDayNames[1] = "�������";
		WeekDayNames[2] = "�����";
		WeekDayNames[3] = "�������";
		WeekDayNames[4] = "�������";
		WeekDayNames[5] = "�������";
		WeekDayNames[6] = "�����������";
		ParaTypeNames = new string[PARA_TYPE_COUNT];
		ParaTypeNames[0] = "������";
		ParaTypeNames[1] = "��������";
		ParaTypeNames[2] = "�������";
		ParaTypeNames[3] = "����";
		ParaTypeNames[4] = "������������";
		ParaTypeNames[5] = "���. �������";
		Table = new Para[2, DAY_IN_WEEK, MAX_PARA_COUNT];

		MonthNames = new string[12] {"������", "�������", "����", "������", "���",
	"����", "����", "������", "��������", "�������", "������", "�������"};

		LoadHolidays();
	}

	// -------------------------------------------------------------------------------------
	//					� � � � � � � � �  � � � � � �

	/// <summary>
	/// ��������� ���������� �� xml ����� DATA_FILE_PATH. 
	/// </summary>
	public static bool LoadTable(string TblPath) {
		try {
			XPathDocument doc = new XPathDocument(TblPath);
			XPathNavigator nav = doc.CreateNavigator();
			XPathNodeIterator NodeIter;
			string Query, ErrMsg;
			int Major, Minor;

			Table = new Para[2, DAY_IN_WEEK, MAX_PARA_COUNT];
			Query = "/TimeTable/Ver|/TimeTable/Group|/TimeTable/Course|/TimeTable/Term";
			NodeIter = nav.Select(Query);
			NodeIter.MoveNext();
			Query = NodeIter.Current.Value;
			Major = Convert.ToInt32(Query.Split('.')[0]);
			Minor = Convert.ToInt32(Query.Split('.')[1]);
			Info.Ver = new Version(Major, Minor);
			if(Info.Ver != Settings.FileFormatVer) {
				ErrMsg = string.Format("����������� ������ ������� ����� ���������� � ����� {0}.\n" +
					"��������� ������������ ������: {1}.\n" +
					"������������ ������: {2}\n" +
					"\n������ ������ �� ��������������! " +
					"��� ������ � ���� ����������� ����������� ������� ����� ������" +
					" ��������� �� ������ ftp://10.6.1.4", TblPath,
					Settings.FileFormatVer.ToString(), Info.Ver.ToString());
				MessageBox.Show(ErrMsg, "�������������� ������", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
			NodeIter.MoveNext();
			Info.Group = NodeIter.Current.Value;
			NodeIter.MoveNext();
			Info.Course = Convert.ToInt32(NodeIter.Current.Value);
			NodeIter.MoveNext();
			Info.Term = Convert.ToInt32(NodeIter.Current.Value);

			for(int CurrDay = 0; CurrDay < DAY_IN_WEEK; CurrDay++) {
				for(int CurrPara = 0; CurrPara < MAX_PARA_COUNT; CurrPara++) {
					Query = string.Format(
						"/TimeTable/Day[{0}]/Para[{1}]/Even/*", CurrDay + 1, CurrPara + 1);
					if(!LoadXmlQueryToTable(nav, Query, CurrDay, CurrPara, WeekType.CHET, TblPath))
						return false;
					Query = string.Format(
						"/TimeTable/Day[{0}]/Para[{1}]/Odd/*", CurrDay + 1, CurrPara + 1);
					if(!LoadXmlQueryToTable(nav, Query, CurrDay, CurrPara, WeekType.NECHET, TblPath))
						return false; ;
				}
			}
			LoadedTbl = TblPath;
			return true;
		}
		catch(System.Xml.XmlException e) {
			string ErrMsg;
			ErrMsg = string.Format("���� {0} ���������!\n"
				+ "�������������� ����������.\n����� ������:\n{1}",
				TblPath, e.Message);
			MessageBox.Show(ErrMsg, "������ ��������", MessageBoxButtons.OK,
														MessageBoxIcon.Error);
			return false;
		}
	}

	/// <summary>��������� ���������� � xml ����. ��� �� ��������� ���. ����������
	/// �� ��������� Info</summary>
	public static void SaveTblToFile(string FilePath, Para[, ,] Table) {
		XmlDocument doc = new XmlDocument();
		XmlNode Root, DayNode, ParaNode;

		doc.Load(FilePath);
		Root = doc.DocumentElement;
		DayNode = Root.FirstChild;

		// ���������� �������������� ����������
		while(DayNode.Name != "Day")
			DayNode = DayNode.NextSibling;

		for(int Day = 0; Day < Tbl.DAY_IN_WEEK; Day++, DayNode = DayNode.NextSibling) {
			ParaNode = DayNode.FirstChild;
			for(int Para = 0;
				Para < Tbl.MAX_PARA_COUNT;
				Para++, ParaNode = ParaNode.NextSibling) 
			{
				ParaNode.ChildNodes[0].ChildNodes[0].InnerText =
					Table[0, Day, Para].Name;
				ParaNode.ChildNodes[0].ChildNodes[1].InnerText =
					Table[0, Day, Para].Location;
				ParaNode.ChildNodes[0].ChildNodes[2].InnerText =
					((int) Table[0, Day, Para].Type).ToString();

				ParaNode.ChildNodes[1].ChildNodes[0].InnerText =
					Table[1, Day, Para].Name;
				ParaNode.ChildNodes[1].ChildNodes[1].InnerText =
					Table[1, Day, Para].Location;
				ParaNode.ChildNodes[1].ChildNodes[2].InnerText =
					((int) Table[1, Day, Para].Type).ToString();
			}
		}
		doc.Save(FilePath);
		SaveInfo(FilePath, Info);
	}

	// -------------------------------------------------------------------------------------
	public static bool SaveAsText(string FilePath) {
		StreamWriter file = new StreamWriter(FilePath);
		const string LINE = "----------------------------------------------------------------\r\n\r\n";

		file.WriteLine("���� ���� ������������ ���������� \"���������� ���\"");
		file.WriteLine("���������� ������ {0} �� {1} ���� {2} �������.",
			Info.Group, Info.Course, Info.Term);
		file.WriteLine(LINE);
		for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
			if(Tbl.ParaCount(i, WeekType.CHET) == 0 &&
				Tbl.ParaCount(i, WeekType.NECHET) == 0)
				continue; // ���� �������� - ���������� ���� ����
			file.WriteLine(" --- > {0} < ---", Tbl.WeekDayNames[i]);
			for(int i2 = 0; i2 < Tbl.MAX_PARA_COUNT; i2++) {
				if(Tbl.Table[(int) Tbl.WeekType.NECHET, i, i2].Equals(
					Tbl.Table[(int) Tbl.WeekType.CHET, i, i2])) { // ���� �� ������ � �������� ������ ��� ���� � �� �� ����

					if(Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name == "" ||
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name == null) { // ���� ���� ���
						file.WriteLine("{0}.", i2 + 1);
						continue;
					}

					file.WriteLine("{0}. {1} {2} {3}", i2 + 1,
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name,
						Tbl.ParaTypeNames[(int) Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Type],
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Location);
				}
				else {
					file.WriteLine("{0}. 1| {1} {2} {3}\r\n   2| {4} {5} {6}",
						i2 + 1, Tbl.Table[(int) Tbl.WeekType.NECHET, i, i2].Name,
						Tbl.ParaTypeNames[(int) Tbl.Table[(int) Tbl.WeekType.NECHET, i, i2].Type],
						Tbl.Table[(int) Tbl.WeekType.NECHET, i, i2].Location,
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name,
						Tbl.ParaTypeNames[(int) Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Type],
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Location);
				}
			}
		}
		file.Close();
		return true;
	}
	// -------------------------------------------------------------------------------------
	/// <summary>
	/// �������� ���� � ����� � ������������ (���� ��� ��� ����� � ����� �� ������)
	/// </summary>
	/// <returns>���������� ������ ���� ���� ����������</returns>
	public static bool ExportTbl(string FilePath) {
		string[] StrArr = FilePath.Split("\\".ToCharArray());
		string FileName = StrArr[StrArr.Length - 1];
		string DestFile = Settings.TblsFolder + "\\" + FileName;

		if(File.Exists(DestFile))
			return false;
		File.Copy(FilePath, DestFile);
		return true;
	}
	// -------------------------------------------------------------------------------------
	/// <summary>��������� � ����� � ����������� �������������� ����������</summary>
	public static bool SaveInfo(string TblPath, TblHeader NewInfo) {
		XmlDocument doc = new XmlDocument();
		XmlNode node;

		doc.Load(TblPath);
		node = doc.DocumentElement;
		node = node.FirstChild;
		node.InnerText = NewInfo.Ver.ToString();
		node = node.NextSibling;
		node.InnerText = NewInfo.Group;
		node = node.NextSibling;
		node.InnerText = NewInfo.Course.ToString();
		node = node.NextSibling;
		node.InnerText = NewInfo.Term.ToString();
		doc.Save(TblPath);
		return true;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ��������� �� ������������ System.DayOfWeek � ����� ������.
	/// � DayOfWeek 0 �����. �����������, 1 - ���. � �.�.
	/// � �� ��� ���������� �-��: 0 - ���., 1 - ������� � �.�.
	/// </summary>
	public static int WeekDayToInt(DayOfWeek d) {
		int res = (int) d;
		res--;
		if(res == -1) res = 6;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ����� � ������� ���������� ���� � ParaNo (����, ����� � ��� �������, 
	/// ���������� ParaNo ���������� � 0)
	/// </summary>
	public static DateTime ParaStart(int ParaNo) {
		DateTime res = new DateTime();

		if(!(0 <= ParaNo && ParaNo <= MAX_PARA_COUNT))
			return res;
		res = res.AddYears(DateTime.Now.Year - 1);
		res = res.AddMonths(DateTime.Now.Month - 1);
		res = res.AddDays(DateTime.Now.Day - 1);
		res += Settings.FirstPara;
		for(int i = 1; i <= ParaNo; i++)
			res = res + Settings.ParaLen + Settings.BreakLen;
		if(ParaNo >= 3) // ������� ������� �������� ����� 3 ����
			res += Settings.BigBreakLen - Settings.BreakLen;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ����� � ������� ��������� ���� � ParaNo (����, ����� � ��� �������, 
	/// ���������� ParaNo ���������� � 0)
	/// </summary>
	public static DateTime ParaEnd(int ParaNo) {
		DateTime res = new DateTime();

		if(!(0 <= ParaNo && ParaNo <= MAX_PARA_COUNT))
			return res;
		res = ParaStart(ParaNo);
		res += Settings.ParaLen;
		return res;
	}

	/// <summary>���������� ������� ��� �� ���������� � �������� ����</summary>
	// -------------------------------------------------------------------------------------
	public static int ParaCount(int Day, WeekType wt) {
		int _wt = (int) wt, res = 0; ;

		for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++)
			if(Table[_wt, Day, i].Name != null) res++;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ������� ��������� ���� �� �������� ���� (���������� ���������� � 0)
	/// </summary>
	public static int LastPara(int Day, WeekType wt) {
		int _wt = (int) wt, i;

		for(i = MAX_PARA_COUNT - 1; i >= 0; i--) {
			if(Table[_wt, Day, i].Name != null) break;
		}
		return i;
	}

	/// <summary>
	/// ������� ����� ������ ���� �� �������� ���� (�����. -1 ���� ��� � ���� ���� ���).
	/// ���������� ���������� � 0
	/// </summary>
	public static int FirstPara(int Day, WeekType wt) {
		int _wt = (int) wt, i;

		for(i = 0; i < MAX_PARA_COUNT; i++) {
			if(Table[_wt, Day, i].Name != null) break;
		}
		if(i != MAX_PARA_COUNT)
			return i;
		else
			return -1;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>����� �� �������?</summary>
	public static bool IsEmpty() {
		bool Empty = true;

		for(int i = 0; i < DAY_IN_WEEK; i++)
			if(ParaCount(i, WeekType.CHET) != 0 ||
				ParaCount(i, Tbl.WeekType.NECHET) != 0) {
				Empty = false;
				break;
			}
		return Empty;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ���������� ����� ���� ����, �� ��� �����. �������� f("������") == 0 ���
	/// f("���. �������") == 5. ���������� -1 ���� ������ �� �����. �� 
	/// ������ �� �������� ����� ���.
	/// </summary>
	public static int ParaTypeNameToInt(string ParaTypeName) {
		for(int i = 0; i < ParaTypeNames.Length; i++) {
			if(ParaTypeName == ParaTypeNames[i]) return i;
		}
		return -1;
	}
	// -------------------------------------------------------------------------------------
	//								� � � � � � � � �   � � � � � �

	/// <summary>
	/// ��������� ������ � ����������� � ���� �� ����� � ����������� � �������.
	/// </summary>
	/// <param name="nav">��������� ������� ��������� ��� ���������</param>
	/// <param name="Query">
	/// XPath 1.0 ������, ������� �������� ������ ����� "Name", 
	/// "Locaton", "Type". �������� "/TimeTable/Day[@number=1]/Para[number=1]/Even/*
	/// </param>
	/// <param name="Day">���� ������ ��� ������ �������� ������?(���������� � 0)</param>
	/// <param name="PataNo">���� ����� �� ����� ���� �������� ������?(���������� � 0)</param>
	/// <param name="wt">���� ���������� ������ ��� �������� ������?</param>
	static bool LoadXmlQueryToTable(XPathNavigator nav, string Query,
		int Day, int ParaNo, WeekType wt, string TblPath) {
		try {
			XPathNodeIterator nodes;
			int _wt = (int) wt;

			nodes = nav.Select(Query);
			nodes.MoveNext();
			if(nodes.Current.Value == string.Empty) return true;	//���� ��� ��� ������ �� ���� ���� ������� ���
			Table[_wt, Day, ParaNo].Name = nodes.Current.Value;
			nodes.MoveNext();
			Table[_wt, Day, ParaNo].Location = nodes.Current.Value;
			nodes.MoveNext();
			Table[_wt, Day, ParaNo].Type = (ParaType)
				Convert.ToInt16(nodes.Current.Value);
			return true;
		}
		catch(System.FormatException e) {
			string ErrMsg =
				string.Format("���� {0} ��� ������� ������������ �������!\n" +
					"�� ���� ���������� ������ {1}\n" +
					"�������������� ����������.\n" +
					"����� ������: {2}", TblPath, Query, e.Message);
			MessageBox.Show(ErrMsg, "������ ��������", MessageBoxButtons.OK,
													MessageBoxIcon.Error);
			return false;
		}
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// ��������� �� ����� ������������ ������ ���������� � ������ Holidays
	/// </summary>
	static void LoadHolidays() {
		XPathDocument doc = new XPathDocument(Settings.CfgFilePath);
		XPathNavigator nav = doc.CreateNavigator();
		XPathNodeIterator nodes;
		XPathNodeIterator CurrNode;
		string Query;
		int i = 0;
		string[] StrArr;

		Query = string.Format("/configuration/{0}/{1}", Settings.CfgHolidaysElName,
			Settings.CfgHolidayElName);
		nodes = nav.Select(Query);

		Holidays = new Holiday[nodes.Count];
		while(nodes.MoveNext()) {
			CurrNode = nodes.Clone();
			CurrNode.Current.MoveToFirstChild();
			Holidays[i].Name = CurrNode.Current.InnerXml;
			CurrNode.Current.MoveToNext();
			StrArr = CurrNode.Current.InnerXml.Split(".".ToCharArray());
			Holidays[i].Date = new DateTime(DateTime.Now.Year, Convert.ToInt32(StrArr[0]),
				Convert.ToInt32(StrArr[1]));
			CurrNode.Current.MoveToNext();
			Holidays[i].NoStudy = Convert.ToBoolean(Convert.ToInt32(CurrNode.Current.InnerXml));
			i++;
		}
	}

} // class Table
}