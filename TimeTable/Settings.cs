using System;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Win32;

namespace TimeTable {
	/// <summary> 
	/// Класс хранит настройки приложения. Содержит только статические методы
	/// </summary>
	static class Settings {

		/// <summary>Путь к файлу конфигурации</summary>
		public const string CfgFilePath = "TimeTable.exe.config";

		/// <summary>Имя папки, в которой хранятся файлы расписаний</summary>
		public const string TblsFolder = "Tables";

		/// <summary>Имя элемента с текущим типом недели в файле конфигурации</summary>
		public const string CfgCurrWeekType = "CurrWeekType";

		/// <summary>
		/// Имя элемента указывающего название файла c расписанем в папке Tables, 
		/// который будет открываться по умолчанию
		/// </summary>
		public const string CfgDefTbl = "DefaultTable";

		/// <summary>Имя элемента с датой последнего обновления в файле конфигурации
		/// </summary>
		public const string CfgLastUpdateDate = "LastUpdateDate";

		/// <summary>
		/// Имя элемента в файле конфигурации, показывает включен ли 
		/// режим подцветки (== "1") или нет (== "0")
		/// </summary>
		public const string CfgHightlightMode = "HLMode";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержаться элементы с 
		/// информацией о подцветке
		/// </summary>
		public const string CfgHightlightElName = "Highlight";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержиться время начала занятий
		/// в виде "часы:минуты"
		/// </summary>
		public const string CfgParaStartTime = "ParaStart";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержиться длинна пары в минутах
		/// </summary>
		public const string CfgParaLen = "ParaLen";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержиться длинна перемены в минутах
		/// </summary>
		public const string CfgBreakLen = "BreakLen";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержиться длинна больщой 
		/// перемены в минутах
		/// </summary>
		public const string CfgBigBreakLen = "BigBreakLen";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержаться все праздники
		/// </summary>
		public const string CfgHolidaysElName = "Holidays";

		/// <summary>
		/// Имя элементов в файле конфигурации, в которых содеражется список праздников
		/// </summary>
		public const string CfgHolidayElName = "Holiday";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержится дата праздника
		/// (в формате "месяц.число")
		/// </summary>
		public const string CfgHolidayDate = "Date";

		/// <summary>
		/// Имя элемента в файле конфигурации, в котором содержится название праздника
		/// </summary>
		public const string CfgHolidaysName = "Name";

		/// <summary>
		/// Имя элемента в файле конфигурации, который показывает, учаться ли в этот праздник 
		/// или нет ("0" или "1")
		/// </summary>
		public const string CfgHolidayNoStudy = "NoStudy";

		// именя элементов в файле конфига, где храниться инфа об фоновом режиме(трее, автостарте)
		public const string CfgNotifyOnParaStart	= "NotifyOnParaStart";
		public const string CfgNotifyOnParaEnd		= "NotifyOnParaEnd";
		public const string CfgPlaySndOnParaStart	= "PlaySndOnParaStart";
		public const string CfgPlaySndOnParaEnd		= "PlaySndOnParaEnd";
		public const string CfgParaStartSoundPath	= "ParaStartSoundPath";
		public const string CfgParaEndSoundPath		= "ParaEndSoundPath";

		/// <summary>
		/// Имя элемента в файле конфигурации, где хранится величина, 
		/// показывающая запускается ли программа при старте Windows ("1") или
		/// нет ("0")
		/// </summary>
		public const string CfgAutostart = "Autostart";

		public const string EmptyTblFile = "EmptyTbl.xml";
		
		/// <summary>путь к папке автозапуска в реестре</summary>
		public const string AUTO_RUN_PATH = 
			@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		/// <summary>параметр, с которым запускается программа при автозагрузке</summary>
		public const string AUTOSTART_ARG = "Autostart";

		/// <summary>Имя ключа в реестровой папке автозагрузки</summary>
		public const string AUTOSTART_KEY_NAME = "TimeTable";

		//==============================================================================
		//								Поля с настройками
		//==============================================================================

		/// <summary>
		/// Версия формата xml файла, с которой работает данная версия программы.
		/// Версия формата файла так же определяет xml-схему
		/// </summary>
		public static Version FileFormatVer = new Version(1, 0);

		/// <summary>Длительность одной пары</summary>
		public static TimeSpan ParaLen;

		/// <summary>Длинна малой перемены/// </summary>
		public static TimeSpan BreakLen;

		/// <summary>Длинна большой перемены</summary>
		public static TimeSpan BigBreakLen;

		/// <summary>Время начала занятий/// </summary>
		public static TimeSpan FirstPara;

		/// <summary>Текущий тип недели (четная/нечетная)</summary>
		public static Tbl.WeekType CurrWeekType;

		/// <summary>Дата последнего обновления текущей типа текущей недели</summary>
		public static DateTime LastUpdateDate;

		/// <summary>
		/// Путь к файлу с расписанием, которое будет открываться при запуске программы
		/// </summary>
		public static string DefaultTable;

		/// <summary>Включен ли режим подцветки пар</summary>
		public static bool HighlightMode;

		/// <summary>
		/// Массив цветов используемых при подцветке пар. В качестве индекса использовать 
		/// Tbl.ParaType
		/// </summary>
		public static Color[] HighlightColors;

		/// <summary>Запускается ли программа вместе с Windows или нет</summary>
		public static bool Autostart;

		/// <summary>Оповещение о начале пары из трея</summary>
		public static bool NotifyOnParaStart;

		/// <summary>Оповещение о конце пары из трея</summary>
		public static bool NotifyOnParaEnd;

		/// <summary>Надо ли проигрывать оповещательный звук при начале пары</summary>
		public static bool PlaySndOnParaStart;

		/// <summary>Надо ли проигрывать оповещательный звук по окончанию пары</summary>
		public static bool PlaySndOnParaEnd;

		/// <summary>Путь к звуковому файлу, который проигрывается при начале пары</summary>
		public static string ParaStartSoundPath;

		/// <summary>Путь к звуковому файлу, который проигрывается по окончанию пары</summary>
		public static string ParaEndSoundPath;


		//==============================================================================
		//									Методы
		//==============================================================================
		public static bool LoadSettings(string ConfigFilePath) {
			XPathDocument doc = new XPathDocument(CfgFilePath);
			XPathNavigator nav = doc.CreateNavigator();
			XPathNodeIterator nodes;
			string[] StrArr;
			string KeysPath;

			KeysPath = "configuration/appSettings/add[@key = \"{0}\"]/@value";

			nodes = nav.Select(String.Format(KeysPath, CfgCurrWeekType));
			nodes.MoveNext();
			CurrWeekType = (Tbl.WeekType) Convert.ToInt32(nodes.Current.Value);

			nodes = nav.Select(String.Format(KeysPath, CfgLastUpdateDate));
			nodes.MoveNext();
			StrArr = nodes.Current.Value.Split(".".ToCharArray());
			LastUpdateDate = new DateTime(Convert.ToInt32(StrArr[2]), Convert.ToInt32(StrArr[1]),
				Convert.ToInt32(StrArr[0]));

			nodes = nav.Select(String.Format(KeysPath, CfgDefTbl));
			nodes.MoveNext();
			DefaultTable = nodes.Current.Value;

			nodes = nav.Select(String.Format(KeysPath, CfgHightlightMode));
			nodes.MoveNext();
			HighlightMode = (nodes.Current.Value == "1") ? true : false;
			
			nodes = nav.Select("configuration/Highlight/*");
			if(nodes.Count != Tbl.PARA_TYPE_COUNT) {
				string a = string.Format("Файл конфигурации поврежден\n\nconfiguration/Highlight/* вернул {0} элементов вместо {1}", nodes.Count, Tbl.PARA_TYPE_COUNT);
				System.Windows.Forms.MessageBox.Show(a, "Ошибка загрузки",
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error);
				return false;
			}
			HighlightColors = new Color[Tbl.PARA_TYPE_COUNT];
			for(int i = 0; i < Tbl.PARA_TYPE_COUNT; i++) {
				nodes.MoveNext();
				StrArr = nodes.Current.Value.Split(";".ToCharArray());
				HighlightColors[i] = Color.FromArgb(Convert.ToInt32(StrArr[0]), 
					Convert.ToInt32(StrArr[1]), Convert.ToInt32(StrArr[2]));
			}

			nodes = nav.Select(String.Format(KeysPath, CfgParaStartTime));
			nodes.MoveNext();
			StrArr = nodes.Current.Value.Split(":".ToCharArray());
			Settings.FirstPara = new TimeSpan(Convert.ToInt32(StrArr[0]), 
										 Convert.ToInt32(StrArr[1]), 0);

			nodes = nav.Select(String.Format(KeysPath, CfgParaLen));
			nodes.MoveNext();
			Settings.ParaLen = new TimeSpan(0, Convert.ToInt32(nodes.Current.Value), 0);

			nodes = nav.Select(String.Format(KeysPath, CfgBreakLen));
			nodes.MoveNext();
			Settings.BreakLen = new TimeSpan(0, Convert.ToInt32(nodes.Current.Value), 0);

			nodes = nav.Select(String.Format(KeysPath, CfgBigBreakLen));
			nodes.MoveNext();
			Settings.BigBreakLen = new TimeSpan(0, Convert.ToInt32(nodes.Current.Value), 0);

			nodes = nav.Select(String.Format(KeysPath, CfgAutostart));
			nodes.MoveNext();
			Autostart = Convert.ToBoolean(Convert.ToInt32(nodes.Current.Value));

			nodes = nav.Select(String.Format(KeysPath, CfgNotifyOnParaStart));
			nodes.MoveNext();
			NotifyOnParaStart = Convert.ToBoolean(Convert.ToInt32(nodes.Current.Value));

			nodes = nav.Select(String.Format(KeysPath, CfgNotifyOnParaEnd));
			nodes.MoveNext();
			NotifyOnParaEnd = Convert.ToBoolean(Convert.ToInt32(nodes.Current.Value));

			nodes = nav.Select(String.Format(KeysPath, CfgPlaySndOnParaStart));
			nodes.MoveNext();
			PlaySndOnParaStart = Convert.ToBoolean(Convert.ToInt32(nodes.Current.Value));

			nodes = nav.Select(String.Format(KeysPath, CfgPlaySndOnParaEnd));
			nodes.MoveNext();
			PlaySndOnParaEnd = Convert.ToBoolean(Convert.ToInt32(nodes.Current.Value));

			nodes = nav.Select(String.Format(KeysPath, CfgParaStartSoundPath));
			nodes.MoveNext();
			ParaStartSoundPath = nodes.Current.Value;

			nodes = nav.Select(String.Format(KeysPath, CfgParaEndSoundPath));
			nodes.MoveNext();
			ParaEndSoundPath = nodes.Current.Value;
			return true;
		}
		//---------------------------------------------------------------------------
		public static bool SaveSettings(string ConfigFilePath) {
			XmlDocument doc = new XmlDocument();
			XmlElement el;

			doc.Load(ConfigFilePath);
			el = doc.DocumentElement;
			foreach(XmlElement i in el.ChildNodes) {
				if(i.LocalName == "appSettings") {
					XmlAttribute atrib;
					foreach(XmlElement i2 in i.ChildNodes) {
						atrib = i2.Attributes[0];
						switch(atrib.InnerText) {
						case CfgCurrWeekType:
							i2.Attributes[1].InnerText = ((int) CurrWeekType).ToString();
							break;
						case CfgLastUpdateDate:
							i2.Attributes[1].InnerText = LastUpdateDate.ToString("dd.MM.yyyy");
							break;
						case CfgDefTbl:
							i2.Attributes[1].InnerText = DefaultTable;
							break;
						case CfgHightlightMode:
							i2.Attributes[1].InnerText = (Convert.ToInt32(HighlightMode)).ToString();
							break;
						case CfgParaStartTime:
							i2.Attributes[1].InnerText = 
								string.Format("{0}:{1}", FirstPara.Hours,
														 FirstPara.Minutes);
							break;
						case CfgParaLen:
							i2.Attributes[1].InnerText = ParaLen.TotalMinutes.ToString();
							break;
						case CfgBreakLen:
							i2.Attributes[1].InnerText = BreakLen.TotalMinutes.ToString();
							break;
						case CfgBigBreakLen:
							i2.Attributes[1].InnerText = BigBreakLen.TotalMinutes.ToString();
							break;
						case CfgAutostart:
							i2.Attributes[1].InnerText = Convert.ToInt32(Autostart).ToString();
							break;
						case CfgNotifyOnParaStart:
							i2.Attributes[1].InnerText = Convert.ToInt32(NotifyOnParaStart).ToString();
							break;
						case CfgNotifyOnParaEnd:
							i2.Attributes[1].InnerText = Convert.ToInt32(NotifyOnParaEnd).ToString();
							break;
						case CfgPlaySndOnParaStart:
							i2.Attributes[1].InnerText = Convert.ToInt32(PlaySndOnParaStart).ToString();
							break;
						case CfgPlaySndOnParaEnd:
							i2.Attributes[1].InnerText = Convert.ToInt32(PlaySndOnParaEnd).ToString();
							break;
						case CfgParaStartSoundPath:
							i2.Attributes[1].InnerText = ParaStartSoundPath;
							break;
						case CfgParaEndSoundPath:
							i2.Attributes[1].InnerText = ParaEndSoundPath;
							break;
					}
					}
				}
				else if(i.LocalName == CfgHightlightElName) {
					string strRGB;
					for(int i2 = 0; i2 < i.ChildNodes.Count; i2++) {
						strRGB = string.Format("{0};{1};{2}", 
							HighlightColors[i2].R, 
							HighlightColors[i2].G, 
							HighlightColors[i2].B);
						i.ChildNodes[i2].InnerText = strRGB;
					}
				}
			}
			doc.Save(ConfigFilePath);
			return true;
		}

		/// <summary>Создает ключ для автостарта в реестре</summary>
		public static void MakeAppAutostart() {
			string Command = System.Windows.Forms.Application.ExecutablePath 
				+ " " + AUTOSTART_ARG;
			Registry.SetValue(AUTO_RUN_PATH, AUTOSTART_KEY_NAME, Command);
		}

		/// <summary>Удаляет ключ автостарта из реестра</summary>
		public static void CancelAutostart() {
			RegistryKey k = (RegistryKey) Registry.GetValue(AUTO_RUN_PATH, AUTOSTART_ARG, null);
			if(k != null) k.DeleteSubKey(AUTOSTART_ARG);
		}
	}
}
