using System;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Win32;

namespace TimeTable {
	/// <summary> 
	/// ����� ������ ��������� ����������. �������� ������ ����������� ������
	/// </summary>
	static class Settings {

		/// <summary>���� � ����� ������������</summary>
		public const string CfgFilePath = "TimeTable.exe.config";

		/// <summary>��� �����, � ������� �������� ����� ����������</summary>
		public const string TblsFolder = "Tables";

		/// <summary>��� �������� � ������� ����� ������ � ����� ������������</summary>
		public const string CfgCurrWeekType = "CurrWeekType";

		/// <summary>
		/// ��� �������� ������������ �������� ����� c ���������� � ����� Tables, 
		/// ������� ����� ����������� �� ���������
		/// </summary>
		public const string CfgDefTbl = "DefaultTable";

		/// <summary>��� �������� � ����� ���������� ���������� � ����� ������������
		/// </summary>
		public const string CfgLastUpdateDate = "LastUpdateDate";

		/// <summary>
		/// ��� �������� � ����� ������������, ���������� ������� �� 
		/// ����� ��������� (== "1") ��� ��� (== "0")
		/// </summary>
		public const string CfgHightlightMode = "HLMode";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� �������� � 
		/// ����������� � ���������
		/// </summary>
		public const string CfgHightlightElName = "Highlight";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� ����� ������ �������
		/// � ���� "����:������"
		/// </summary>
		public const string CfgParaStartTime = "ParaStart";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� ������ ���� � �������
		/// </summary>
		public const string CfgParaLen = "ParaLen";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� ������ �������� � �������
		/// </summary>
		public const string CfgBreakLen = "BreakLen";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� ������ ������� 
		/// �������� � �������
		/// </summary>
		public const string CfgBigBreakLen = "BigBreakLen";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ����������� ��� ���������
		/// </summary>
		public const string CfgHolidaysElName = "Holidays";

		/// <summary>
		/// ��� ��������� � ����� ������������, � ������� ����������� ������ ����������
		/// </summary>
		public const string CfgHolidayElName = "Holiday";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ���������� ���� ���������
		/// (� ������� "�����.�����")
		/// </summary>
		public const string CfgHolidayDate = "Date";

		/// <summary>
		/// ��� �������� � ����� ������������, � ������� ���������� �������� ���������
		/// </summary>
		public const string CfgHolidaysName = "Name";

		/// <summary>
		/// ��� �������� � ����� ������������, ������� ����������, ������� �� � ���� �������� 
		/// ��� ��� ("0" ��� "1")
		/// </summary>
		public const string CfgHolidayNoStudy = "NoStudy";

		// ����� ��������� � ����� �������, ��� ��������� ���� �� ������� ������(����, ����������)
		public const string CfgNotifyOnParaStart	= "NotifyOnParaStart";
		public const string CfgNotifyOnParaEnd		= "NotifyOnParaEnd";
		public const string CfgPlaySndOnParaStart	= "PlaySndOnParaStart";
		public const string CfgPlaySndOnParaEnd		= "PlaySndOnParaEnd";
		public const string CfgParaStartSoundPath	= "ParaStartSoundPath";
		public const string CfgParaEndSoundPath		= "ParaEndSoundPath";

		/// <summary>
		/// ��� �������� � ����� ������������, ��� �������� ��������, 
		/// ������������ ����������� �� ��������� ��� ������ Windows ("1") ���
		/// ��� ("0")
		/// </summary>
		public const string CfgAutostart = "Autostart";

		public const string EmptyTblFile = "EmptyTbl.xml";
		
		/// <summary>���� � ����� ����������� � �������</summary>
		public const string AUTO_RUN_PATH = 
			@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		/// <summary>��������, � ������� ����������� ��������� ��� ������������</summary>
		public const string AUTOSTART_ARG = "Autostart";

		/// <summary>��� ����� � ���������� ����� ������������</summary>
		public const string AUTOSTART_KEY_NAME = "TimeTable";

		//==============================================================================
		//								���� � �����������
		//==============================================================================

		/// <summary>
		/// ������ ������� xml �����, � ������� �������� ������ ������ ���������.
		/// ������ ������� ����� ��� �� ���������� xml-�����
		/// </summary>
		public static Version FileFormatVer = new Version(1, 0);

		/// <summary>������������ ����� ����</summary>
		public static TimeSpan ParaLen;

		/// <summary>������ ����� ��������/// </summary>
		public static TimeSpan BreakLen;

		/// <summary>������ ������� ��������</summary>
		public static TimeSpan BigBreakLen;

		/// <summary>����� ������ �������/// </summary>
		public static TimeSpan FirstPara;

		/// <summary>������� ��� ������ (������/��������)</summary>
		public static Tbl.WeekType CurrWeekType;

		/// <summary>���� ���������� ���������� ������� ���� ������� ������</summary>
		public static DateTime LastUpdateDate;

		/// <summary>
		/// ���� � ����� � �����������, ������� ����� ����������� ��� ������� ���������
		/// </summary>
		public static string DefaultTable;

		/// <summary>������� �� ����� ��������� ���</summary>
		public static bool HighlightMode;

		/// <summary>
		/// ������ ������ ������������ ��� ��������� ���. � �������� ������� ������������ 
		/// Tbl.ParaType
		/// </summary>
		public static Color[] HighlightColors;

		/// <summary>����������� �� ��������� ������ � Windows ��� ���</summary>
		public static bool Autostart;

		/// <summary>���������� � ������ ���� �� ����</summary>
		public static bool NotifyOnParaStart;

		/// <summary>���������� � ����� ���� �� ����</summary>
		public static bool NotifyOnParaEnd;

		/// <summary>���� �� ����������� �������������� ���� ��� ������ ����</summary>
		public static bool PlaySndOnParaStart;

		/// <summary>���� �� ����������� �������������� ���� �� ��������� ����</summary>
		public static bool PlaySndOnParaEnd;

		/// <summary>���� � ��������� �����, ������� ������������� ��� ������ ����</summary>
		public static string ParaStartSoundPath;

		/// <summary>���� � ��������� �����, ������� ������������� �� ��������� ����</summary>
		public static string ParaEndSoundPath;


		//==============================================================================
		//									������
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
				string a = string.Format("���� ������������ ���������\n\nconfiguration/Highlight/* ������ {0} ��������� ������ {1}", nodes.Count, Tbl.PARA_TYPE_COUNT);
				System.Windows.Forms.MessageBox.Show(a, "������ ��������",
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

		/// <summary>������� ���� ��� ���������� � �������</summary>
		public static void MakeAppAutostart() {
			string Command = System.Windows.Forms.Application.ExecutablePath 
				+ " " + AUTOSTART_ARG;
			Registry.SetValue(AUTO_RUN_PATH, AUTOSTART_KEY_NAME, Command);
		}

		/// <summary>������� ���� ���������� �� �������</summary>
		public static void CancelAutostart() {
			RegistryKey k = (RegistryKey) Registry.GetValue(AUTO_RUN_PATH, AUTOSTART_ARG, null);
			if(k != null) k.DeleteSubKey(AUTOSTART_ARG);
		}
	}
}
