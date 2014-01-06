#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
#endregion

namespace TimeTable {
	partial class frmStatist: Form {
		//						К о н с т а н т ы
		const string FULL_STUDY_TIME		= "Полное время учебы за неделю:";
		const string AVERAGE_DAY_TIME		= "Среднее время учебы каждый день";
		const string LONGEST_WORK_DAY		= "Самый длинный учебный день";
		const string SHORTEST_WORK_DAY		= "Самый короткий учебный день";
		const string AVERAGE_1ST_PARA_TIME	= "Среднее время начала занятий";
		const string AVERAGE_LAST_PARA_TIME = "Среднее время окончания занятий";
		const string MOST_FREQ_SUBJ			= "Самый частый предмет";
		const string MOST_UNFREQ_SUBJ		= "Самый редкий предмет";
		const string PARA_HOUR_MIN_PATTERN	= "{0} пар ({1} дн. {2} час. {3} мин.)";
		const string PARA_HOUR_MIN_PAT_AVER = "{0} (~ {1}) пар ({2} дн. {3} час. {4} мин.)";

		public frmStatist() {
			InitializeComponent();
		}

		void frmStatist_Load(object sender, EventArgs e) {
			this.Text = string.Format("Статистика гр. {0}, курс {1}, сем. {2}",
				Tbl.Info.Group, Tbl.Info.Course, Tbl.Info.Term);

			ShowHolidays();
			ShowStatistics(Tbl.WeekType.NECHET, ref lstOddStat);
			ShowStatistics(Tbl.WeekType.CHET,   ref lstEvenStat);
		}

		/// <summary>Заполняет listView списком праздников</summary>
		void ShowHolidays() {
			string strDate;
			for(int i = 0; i < Tbl.Holidays.Length; i++) {
				lstHolidays.Items.Add(Tbl.Holidays[i].Name);
				strDate = string.Format("{0} {1}",
					Tbl.Holidays[i].Date.Day,
					Tbl.MonthNames[Tbl.Holidays[i].Date.Month - 1]);
				lstHolidays.Items[i].SubItems.Add(strDate);
				lstHolidays.Items[i].SubItems.Add(
					Tbl.Holidays[i].NoStudy ? "Да" : "Нет");
				if(Tbl.Holidays[i].NoStudy)
					lstHolidays.Items[i].BackColor = Color.Pink;
			}
		}

		void ShowStatistics(Tbl.WeekType wt, ref ListView lv) {
			string str, str2; 
			int x, y;
			float z;
			TimeSpan ts;
			DateTime dt;

			x = FullStudyTime(wt);
			ts = ParaToTime(x);
			str = string.Format(PARA_HOUR_MIN_PATTERN, x, ts.Days, ts.Hours, ts.Minutes);
			lv.Items.Add(FULL_STUDY_TIME);
			lv.Items[0].SubItems.Add(str);

			z = AverageDayTime(wt);
			ts = ParaToTime((int) z);
			str = string.Format(PARA_HOUR_MIN_PAT_AVER, z, (int) z, ts.Days, ts.Hours, ts.Minutes);
			lv.Items.Add(AVERAGE_DAY_TIME);
			lv.Items[1].SubItems.Add(str);

			x = LongestWorkDay(wt, out y);
			ts = ParaToTime(y);
			str = string.Format("{0} - {1} пар(ы)", Tbl.WeekDayNames[x], y);
			lv.Items.Add(LONGEST_WORK_DAY);
			lv.Items[2].SubItems.Add(str);

			x = ShortestWorkDay(wt, out y);
			ts = ParaToTime(y);
			str = string.Format("{0} - {1} пар(ы)", Tbl.WeekDayNames[x], y);
			lv.Items.Add(SHORTEST_WORK_DAY);
			lv.Items[3].SubItems.Add(str);

			x = Average1stParaTime(wt);
			dt = Tbl.ParaStart(x - 1);
			str = string.Format("{0} пара ({1}:{2})", x, dt.Hour, dt.Minute);
			lv.Items.Add(AVERAGE_1ST_PARA_TIME);
			lv.Items[4].SubItems.Add(str);

			x = AverageLastParaTime(wt);
			dt = Tbl.ParaEnd(x - 1);
			str = string.Format("{0} пара ({1}:{2})", x, dt.Hour, dt.Minute);
			lv.Items.Add(AVERAGE_LAST_PARA_TIME);
			lv.Items[5].SubItems.Add(str);

			str = MostFreqSubj(wt, out x);
			str2 = string.Format("{0} - {1} пар/нед", str, x);
			lv.Items.Add(MOST_FREQ_SUBJ);
			lv.Items[6].SubItems.Add(str2);

			str = MostUnfreqSubj(wt, out x);
			str2 = string.Format("{0} - {1} пар/нед", str, x);
			lv.Items.Add(MOST_UNFREQ_SUBJ);
			lv.Items[7].SubItems.Add(str2);

		}

		/// <summary>Конвертирует кол-во пар в кол-во часов, которые идут эти пары</summary>
		TimeSpan ParaToTime(int Paras) {
			return TimeSpan.FromMinutes(Paras * Settings.ParaLen.TotalMinutes);
		}

		//===============================================================================
		//								Функции вычисления статистики
		//===============================================================================

		/// <summary>Вычисляет кол-во пар в неделе</summary>
		int FullStudyTime(Tbl.WeekType wt) {
			int iwt = (int) wt;
			byte ParaCount = 0;

			for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				for(int i2 = 0; i2 < Tbl.MAX_PARA_COUNT; i2++) {
					if(Tbl.Table[iwt, i, i2].Name != null &&
						Tbl.Table[iwt, i, i2].Name != "")
						ParaCount++;
				}
			}
			return ParaCount;
		}
		//--------------------------------------------------------------------------------
		/// <summary>Вычисляет среднее время обучения за день</summary>
		float AverageDayTime(Tbl.WeekType wt) {
			float res = (float) FullStudyTime(wt);
			if(res != 0f) {
				res /= (float) Tbl.DAY_IN_WEEK;
				return res;
			}
			else
				return 0f;
		}
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Возвращает номер самого длинного учебного дня недели
		/// </summary>
		/// <param name="Paras">Кол-во пар в этот день</param>
		int LongestWorkDay(Tbl.WeekType wt, out int Paras) {
			int iwt = (int) wt, Day, x;

			Paras = Day = 0;
			for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				x = Tbl.ParaCount(i, wt);
				if(x > Paras) {
					Day = i;
					Paras = x;
				}
			}
			return Day;
		}
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Возвращает номер самого короткого учебного дня недели
		/// </summary>
		/// <param name="Paras">Кол-во пар в этот день</param>
		int ShortestWorkDay(Tbl.WeekType wt, out int Paras) {
			int iwt = (int) wt, 
				Day = 0, 
				x = Tbl.MAX_PARA_COUNT + 1;

			Paras = Tbl.MAX_PARA_COUNT;
			for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				x = Tbl.ParaCount(i, wt);
				if(x < Paras) {
					Day = i;
					Paras = x;
				}
			}
			return Day;
		}
		//--------------------------------------------------------------------------------
		/// <summary>Вычисляет среднее время начала занятий</summary>
		int Average1stParaTime(Tbl.WeekType wt) {
			int x, Num = 0, WorkDayCount = 0;

			for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				x = Tbl.FirstPara(i, wt);
				if(x != -1) {
					Num += x;
					WorkDayCount++;
				}
			}
			if(WorkDayCount != 0)
				return Num / WorkDayCount + 1;
			else
				return -1;
		}
		//--------------------------------------------------------------------------------
		/// <summary>Среднее время окончания занятий</summary>
		int AverageLastParaTime(Tbl.WeekType wt) {
			int x = 0, a = 0, StudyDays = Tbl.DAY_IN_WEEK;

			for(int i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				a = Tbl.ParaCount(i, wt);
				if(a == 0) {
					StudyDays--;
					continue;
				}
				x += a;
			}
			if (StudyDays != 0) x /= StudyDays;
			return x;
		}
		//--------------------------------------------------------------------------------
		/// <summary>Вычисляет самый частый предмет</summary>
		/// <param name="Paras">Сколько раз в неделю он бывает</param>
		string MostFreqSubj(Tbl.WeekType wt, out int Paras) {
			StringCollection Subjs = new StringCollection();
			List<int> numParas = new List<int>();
			int iwt = (int) wt, i, max = 0, MaxIndex = 0;
			bool f;

			for(i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				for(int i2 = 0; i2 < Tbl.MAX_PARA_COUNT; i2++) {
					if(Tbl.Table[iwt, i, i2].Name == "" ||
						Tbl.Table[iwt, i, i2].Name == null) continue;
					f = false;
					for(int i3 = 0; i3 < Subjs.Count; i3++) {
						if(Tbl.Table[iwt, i, i2].Name == Subjs[i3]) {
							f = true;
							numParas[i3]++;
							break;
						}
					}
					if(!f) {
						Subjs.Add(Tbl.Table[iwt, i, i2].Name);
						numParas.Add(1);
					}
				}
			}

			for(i = 0; i < Subjs.Count; i++) {
				if(numParas[i] > max) {
					max = numParas[i];
					MaxIndex = i;
				}
			}
			Paras = numParas[MaxIndex];
			return Subjs[MaxIndex];
		}
		//--------------------------------------------------------------------------------
		/// <summary>Вычисляет самый частый предмет</summary>
		/// <param name="Paras">Сколько раз в неделю он бывает</param>
		string MostUnfreqSubj(Tbl.WeekType wt, out int Paras) {
			StringCollection Subjs = new StringCollection();
			List<int> numParas = new List<int>();
			int iwt = (int) wt, i, min = int.MaxValue, MinIndex = 0;
			bool f;

			for(i = 0; i < Tbl.DAY_IN_WEEK; i++) {
				for(int i2 = 0; i2 < Tbl.MAX_PARA_COUNT; i2++) {
					f = false;
					for(int i3 = 0; i3 < Subjs.Count; i3++) {
						if(Tbl.Table[iwt, i, i2].Name == Subjs[i3]) {
							f = true;
							numParas[i3]++;
							break;
						}
					}
					if(!f) {
						Subjs.Add(Tbl.Table[iwt, i, i2].Name);
						numParas.Add(1);
					}
				}
			}

			for(i = 0; i < Subjs.Count; i++) {
				if(numParas[i] < min)
					MinIndex = i;
			}
			Paras = numParas[MinIndex];
			return Subjs[MinIndex];
		}

		//==============================================================================

		private void lstOddStat_SizeChanged(object sender, EventArgs e) {
			lstOddStat.Columns[0].Width = lstOddStat.Width - lstOddStat.Columns[1].Width;
		}

		private void lstEvenStat_SizeChanged(object sender, EventArgs e) {
			lstEvenStat.Columns[0].Width = lstEvenStat.Width - lstEvenStat.Columns[1].Width;
		}

		private void lstHolidays_SizeChanged(object sender, EventArgs e) {
			lstHolidays.Columns[0].Width = lstHolidays.Width - 
				lstHolidays.Columns[1].Width - lstHolidays.Columns[2].Width;
		}

		private void frmStatist_SizeChanged(object sender, EventArgs e) {
			lstHolidays.Dock = DockStyle.Fill;
		}
	}
}	