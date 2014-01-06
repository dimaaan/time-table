using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

/// <summary>
///	Класс для работы с расписанием на неделю
/// </summary>
namespace TimeTable {
class Tbl { //=========================================================================

	//					К О Н С Т А Н Т Ы
	public const int DAY_IN_WEEK = 7;

	/// <summary>Максимальное количество пар в день</summary>
	public const int MAX_PARA_COUNT = 6;

	/// <summary>Кол-во типов пар/// </summary>
	public const int PARA_TYPE_COUNT = 6;

	public static string LoadedTbl;

	// -------------------------------------------------------------------------------------
	//					П Е Р Е Ч И С Л Е Н И Я
	/// <summary>
	/// Типы занятий. 
	/// При добавлении нового типа увеличить константу PARA_TYPES_COUNT
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
	//					С Т Р У К Т У Р Ы
	/// <summary>Хранит информацию о паре</summary>
	public struct Para {
		public string Name;
		public ParaType Type;
		public string Location;
	}

	/// <summary>
	/// Содержит дополнительную информацию о расписании (группа, курс, семестр) 
	/// и версию формата файла расписания.
	/// </summary>
	public struct TblHeader {
		/// <summary>Версия формата файла расписания</summary>
		public Version Ver;

		/// <summary>Название группы, у которой данное расписание</summary>
		public string Group;

		/// <summary>Курс, на котором учиться группа с данным расписанием</summary>
		public int Course;

		/// <summary>Семестр, в котором группа занимается по данному расписанию</summary>
		public int Term;
	}

	/// <summary>Хранит информацию о праздниках</summary>
	public struct Holiday {

		/// <summary>Дата празника</summary>
		public DateTime Date;

		/// <summary>Название праздика</summary>
		public string Name;

		/// <summary>Учаться ли в этот день?</summary>
		public bool NoStudy;
	}

	// -------------------------------------------------------------------------------------
	//					П О Л Я

	/// <summary>Названия всех дней недели</summary>
	public static readonly string[] WeekDayNames;

	/// <summary>Типы пар ("Лекция", "Практика" и т.д.)</summary>
	public static readonly string[] ParaTypeNames;

	/// <summary>
	/// Расписание на 2 недели(3D массив).
	/// 1ый индекс - чентная (0) или не чентая (1) неделя.
	/// 2ой - это день недели, 3ый - номер пары
	/// </summary>
	public static Para[, ,] Table;

	/// <summary>Содержит дополнительную информацию о расписании</summary>
	public static TblHeader Info;

	/// <summary>Оцифиальные праздники Росии</summary>
	public static Holiday[] Holidays;

	public static readonly string[] MonthNames;

	// -------------------------------------------------------------------------------------
	//					 К О Н С Т Р У К Т О Р Ы
	static Tbl() {
		WeekDayNames = new string[DAY_IN_WEEK];
		WeekDayNames[0] = "Понедельник";
		WeekDayNames[1] = "Вторник";
		WeekDayNames[2] = "Среда";
		WeekDayNames[3] = "Четверг";
		WeekDayNames[4] = "Пятница";
		WeekDayNames[5] = "Суббота";
		WeekDayNames[6] = "Воскресенье";
		ParaTypeNames = new string[PARA_TYPE_COUNT];
		ParaTypeNames[0] = "Лекция";
		ParaTypeNames[1] = "Практика";
		ParaTypeNames[2] = "Семинар";
		ParaTypeNames[3] = "Лаба";
		ParaTypeNames[4] = "Консультация";
		ParaTypeNames[5] = "Доп. занятие";
		Table = new Para[2, DAY_IN_WEEK, MAX_PARA_COUNT];

		MonthNames = new string[12] {"января", "февраля", "мара", "апреля", "мая",
	"июня", "июля", "авгуса", "сентября", "октября", "ноября", "декабря"};

		LoadHolidays();
	}

	// -------------------------------------------------------------------------------------
	//					П У Б Л И Ч Н Ы Е  М Е Т О Д Ы

	/// <summary>
	/// Загружает расписание из xml файла DATA_FILE_PATH. 
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
				ErrMsg = string.Format("Неизвестная версия формата файла расписания в файле {0}.\n" +
					"Программа поддерживает версию: {1}.\n" +
					"Обнаруженная версия: {2}\n" +
					"\nДанная версия не поддерживается! " +
					"Для работы с этим расписанием попытайтесь скачать новую версию" +
					" программы по адресу ftp://10.6.1.4", TblPath,
					Settings.FileFormatVer.ToString(), Info.Ver.ToString());
				MessageBox.Show(ErrMsg, "Несоответствие версий", MessageBoxButtons.OK,
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
			ErrMsg = string.Format("Файл {0} поврежден!\n"
				+ "Переустановите приложение.\nТекст ошибки:\n{1}",
				TblPath, e.Message);
			MessageBox.Show(ErrMsg, "Ошибка загрузки", MessageBoxButtons.OK,
														MessageBoxIcon.Error);
			return false;
		}
	}

	/// <summary>Сохраняет расписание в xml файл. Так же сохраняет доп. информацию
	/// из структуры Info</summary>
	public static void SaveTblToFile(string FilePath, Para[, ,] Table) {
		XmlDocument doc = new XmlDocument();
		XmlNode Root, DayNode, ParaNode;

		doc.Load(FilePath);
		Root = doc.DocumentElement;
		DayNode = Root.FirstChild;

		// пропускаем дополнительную информацию
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

		file.WriteLine("Этот файл сгенерирован программой \"Расписание пар\"");
		file.WriteLine("Расписание группы {0} за {1} курс {2} семестр.",
			Info.Group, Info.Course, Info.Term);
		file.WriteLine(LINE);
		for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
			if(Tbl.ParaCount(i, WeekType.CHET) == 0 &&
				Tbl.ParaCount(i, WeekType.NECHET) == 0)
				continue; // если выходной - пропускаем этот день
			file.WriteLine(" --- > {0} < ---", Tbl.WeekDayNames[i]);
			for(int i2 = 0; i2 < Tbl.MAX_PARA_COUNT; i2++) {
				if(Tbl.Table[(int) Tbl.WeekType.NECHET, i, i2].Equals(
					Tbl.Table[(int) Tbl.WeekType.CHET, i, i2])) { // если на четной и нечетной неделе эта одна и та же пара

					if(Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name == "" ||
						Tbl.Table[(int) Tbl.WeekType.CHET, i, i2].Name == null) { // если пары нет
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
	/// Копирует файл в папку с расписаниями (Если там нет файла с таким же именем)
	/// </summary>
	/// <returns>Возвращает истину если файл скопирован</returns>
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
	/// <summary>Сохраняет в файле с расписанием дополнительную информацию</summary>
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
	/// Переводит из перечисления System.DayOfWeek в номер недели.
	/// В DayOfWeek 0 соотв. Воскресенье, 1 - Пон. и т.д.
	/// А то что возвращает ф-ия: 0 - Пон., 1 - Вторник и т.д.
	/// </summary>
	public static int WeekDayToInt(DayOfWeek d) {
		int res = (int) d;
		res--;
		if(res == -1) res = 6;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// Время в которое начинается пара № ParaNo (день, месяц и год текущие, 
	/// индексация ParaNo начинается с 0)
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
		if(ParaNo >= 3) // добавим большую перемену после 3 пары
			res += Settings.BigBreakLen - Settings.BreakLen;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// Время в которое кончается пара № ParaNo (день, месяц и год текущие, 
	/// индексация ParaNo начинается с 0)
	/// </summary>
	public static DateTime ParaEnd(int ParaNo) {
		DateTime res = new DateTime();

		if(!(0 <= ParaNo && ParaNo <= MAX_PARA_COUNT))
			return res;
		res = ParaStart(ParaNo);
		res += Settings.ParaLen;
		return res;
	}

	/// <summary>Определяет сколько пар по расписанию в заданный день</summary>
	// -------------------------------------------------------------------------------------
	public static int ParaCount(int Day, WeekType wt) {
		int _wt = (int) wt, res = 0; ;

		for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++)
			if(Table[_wt, Day, i].Name != null) res++;
		return res;
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// Находит последнюю пару на заданный день (индексация начинается с 0)
	/// </summary>
	public static int LastPara(int Day, WeekType wt) {
		int _wt = (int) wt, i;

		for(i = MAX_PARA_COUNT - 1; i >= 0; i--) {
			if(Table[_wt, Day, i].Name != null) break;
		}
		return i;
	}

	/// <summary>
	/// Находит номер первой пары на заданный день (возвр. -1 если пар в этот день нет).
	/// Индексация начинается с 0
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
	/// <summary>Пуста ли таблица?</summary>
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
	/// Возвращает номер типа пары, по его имени. Например f("Лекция") == 0 или
	/// f("Доп. занятие") == 5. Возвращает -1 если строка не соотв. ни 
	/// одному из названий типов пар.
	/// </summary>
	public static int ParaTypeNameToInt(string ParaTypeName) {
		for(int i = 0; i < ParaTypeNames.Length; i++) {
			if(ParaTypeName == ParaTypeNames[i]) return i;
		}
		return -1;
	}
	// -------------------------------------------------------------------------------------
	//								П Р И В А Т Н Ы Е   М Е Т О Д Ы

	/// <summary>
	/// Загружает запрос с информацией о паре из файла с расписанием в таблицу.
	/// </summary>
	/// <param name="nav">Созданный заранее навигатор для документа</param>
	/// <param name="Query">
	/// XPath 1.0 запрос, который содержит список узлов "Name", 
	/// "Locaton", "Type". Например "/TimeTable/Day[@number=1]/Para[number=1]/Even/*
	/// </param>
	/// <param name="Day">Узлы какого дня недели содержит запрос?(индексация с 0)</param>
	/// <param name="PataNo">Узлы какой по счету пары содержит запрос?(индексация с 0)</param>
	/// <param name="wt">Узлы расписания четной или нечетной недели?</param>
	static bool LoadXmlQueryToTable(XPathNavigator nav, string Query,
		int Day, int ParaNo, WeekType wt, string TblPath) {
		try {
			XPathNodeIterator nodes;
			int _wt = (int) wt;

			nodes = nav.Select(Query);
			nodes.MoveNext();
			if(nodes.Current.Value == string.Empty) return true;	//если нет имя значит на этой паре занятий нет
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
				string.Format("Файл {0} был изменен некорректным образом!\n" +
					"Не могу обработать запрос {1}\n" +
					"Переустановите приложение.\n" +
					"Текст ошибки: {2}", TblPath, Query, e.Message);
			MessageBox.Show(ErrMsg, "Ошибка загрузки", MessageBoxButtons.OK,
													MessageBoxIcon.Error);
			return false;
		}
	}

	// -------------------------------------------------------------------------------------
	/// <summary>
	/// Загружает из файла конфигурации список праздников в массив Holidays
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