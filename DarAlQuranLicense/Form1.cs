using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using CsvHelper;

namespace DarAlQuranLicense
{
	public partial class MainForm : Form
	{
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
			IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

		public readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

		public readonly FontFamily Samim;

		private readonly FontFamily IranNastaliq;

		private readonly FontFamily BKoodak;

		private readonly FontFamily BNazanin;

		private readonly Bitmap DefaultImage;

		private readonly Bitmap NAImage;

		private readonly Bitmap ErrorImage;

		private class Student_C
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }

			public string GetFullName()
			{
				return FirstName + " " + LastName;
			}

			public string FatherName { get; set; }
			public string Code { get; set; }
		}
		
		private List<Student_C> students_C;

		private readonly PersianCalendar persianCalendar = new PersianCalendar();

		private bool hasPicture = false;

		public MainForm()
		{
			#region Initialize fonts

			uint temp = 0;
			byte[][] fontData = new byte[][] { Properties.Resources.Samim, Properties.Resources.IranNastaliq,
				Properties.Resources.BNazanin, Properties.Resources.BKoodak };

			for (int i = 0; i < fontData.Length; i++)
			{
				IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData[i].Length);
				System.Runtime.InteropServices.Marshal.Copy(fontData[i], 0, fontPtr, fontData[i].Length);
				privateFontCollection.AddMemoryFont(fontPtr, fontData[i].Length);
				AddFontMemResourceEx(fontPtr, (uint)fontData[i].Length, IntPtr.Zero, ref temp);
				System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
			}

			Samim = privateFontCollection.Families[3];
			IranNastaliq = privateFontCollection.Families[2];
			BNazanin = privateFontCollection.Families[1];
			BKoodak = privateFontCollection.Families[0];

			#endregion Initialize fonts

			InitializeComponent();

			SetAllControlsFont(Controls);

			Application.CurrentCulture = new CultureInfo("fa-IR");

			Font = new Font(Samim, Font.Size);

			#region Initialize images

			DefaultImage = ConvertTextToImage("عکس\nقرآن آموز", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);

			NAImage = ConvertTextToImage("عکس\nقرآن آموز\nیافت نشد!", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);

			ErrorImage = ConvertTextToImage("خطا در\nبارگذاری\nعکس\nقرآن آموز!", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);

			#endregion Initialize images

			studentPicture.Image = DefaultImage;
			level.SelectedIndex = 0;
			score.SelectedIndex = 0;
			background.SelectedIndex = 0;
			if (File.Exists("لیست قرآن آموزان.txt")) studentsListAddress.Text = Path.GetFullPath("لیست قرآن آموزان.txt");
			if (Directory.Exists("عکس قرآن آموزان")) studentsPicturesAddress.Text = Path.GetFullPath("عکس قرآن آموزان");
			saveAddress.Text = Path.GetFullPath("گواهینامه ها");

			DateTime now = DateTime.Today;

			date.Text = (
				persianCalendar.GetYear(now).ToString() + '/' +
				persianCalendar.GetMonth(now).ToString() + '/' +
				persianCalendar.GetDayOfMonth(now)
				).EnglishNumbersToPersian();
		}

		private void SetAllControlsFont(Control.ControlCollection ctrls)
		{
			foreach (Control c in ctrls)
			{
				if (c.Controls != null)
				{
					SetAllControlsFont(c.Controls);
				}
				c.Font = new Font(Samim, c.Font.Size);
			}
		}

		private Bitmap ConvertTextToImage(string text, FontFamily fontFamily, float fontSize,
			FontStyle fontStyle = FontStyle.Regular, StringFormat stringFormat = default,
			float maxWidth = float.MaxValue, float maxHeight = float.MaxValue, float xDpi = 72, float yDpi = 72,
			Color backgroundColor = default, Color foregroundColor = default)
		{
			if (text == "") return null;
			Bitmap bitmap = new Bitmap(1, 1);
			Graphics graphics = Graphics.FromImage(bitmap);
			if (stringFormat == default) stringFormat = new StringFormat();
			if (backgroundColor == default) backgroundColor = Color.Transparent;
			if (foregroundColor == default) foregroundColor = Color.Black;

			Font font = new Font(fontFamily, fontSize, fontStyle);

			SizeF stringSize = graphics.MeasureString(text, font, int.MaxValue, stringFormat);
			while (stringSize.Width > maxWidth || stringSize.Height > maxHeight)
			{
				fontSize -= (float)0.1;
				font = new Font(fontFamily, fontSize, fontStyle);
				stringSize = graphics.MeasureString(text, font, int.MaxValue, stringFormat);
			}

			bitmap = new Bitmap((int)stringSize.Width, (int)stringSize.Height);
			graphics = Graphics.FromImage(bitmap);
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

			bitmap.SetResolution(xDpi, yDpi);
			graphics.Clear(backgroundColor);

			int x = 0;
			if ((stringFormat.FormatFlags & StringFormatFlags.DirectionRightToLeft) > 0 && stringFormat.Alignment == StringAlignment.Center)
				x = (int)stringSize.Width / 2;
			else if ((stringFormat.FormatFlags & StringFormatFlags.DirectionRightToLeft) > 0 && stringFormat.Alignment != StringAlignment.Far) x = (int)stringSize.Width;
			else if (stringFormat.Alignment == StringAlignment.Center) x += (int)stringSize.Width / 2;

			graphics.DrawString(text, font, new SolidBrush(foregroundColor), x, 0, stringFormat);

			return bitmap;
		}

		private void FitAndDrawString(string text, Font font, StringFormat stringFormat, Brush brush,
			ref Graphics graphics, float x, float y, float maxWidth = float.MaxValue, float maxHeight = float.MaxValue)
		{
			SizeF stringSize = graphics.MeasureString(text, font, int.MaxValue, stringFormat);
			float initialHeight = stringSize.Height;

			while (stringSize.Width > maxWidth || stringSize.Height > maxHeight)
			{
				font = new Font(font.FontFamily, (float)(font.Size - 0.1), font.Style);
				stringSize = graphics.MeasureString(text, font, int.MaxValue, stringFormat);
			}

			y += (initialHeight - stringSize.Height) / 2;
			if ((stringFormat.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0 && stringFormat.Alignment == StringAlignment.Far)
				x -= stringSize.Width;

			graphics.DrawString(text, font, brush, x, y, stringFormat);
		}

		private static Bitmap ResizeImage(Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			return destImage;
		}

		private void StudentsListBrowse_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
			{
				Multiselect = false,
				EnsureFileExists = true
			};
			fileDialog.Filters.Add(new CommonFileDialogFilter("Text Files", "*.txt"));

			if (fileDialog.ShowDialog() != CommonFileDialogResult.Ok) return;

			studentsListAddress.Text = fileDialog.FileName;
		}

		private void StudentsPicturesBrowse_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
			{
				Multiselect = false,
				EnsurePathExists = true,
				IsFolderPicker = true
			};
			if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok) studentsPicturesAddress.Text = fileDialog.FileName;
		}

		private void SaveAddressBrowse_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
			{
				Multiselect = false,
				EnsurePathExists = true,
				IsFolderPicker = true
			};
			if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok) saveAddress.Text = fileDialog.FileName;
		}

		private void LevelRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (levelRadioButton.Checked)
			{
				level.Enabled = true;
				customLicenseText.Enabled = false;
				background.Enabled = false;
			}
			else
			{
				level.Enabled = false;
				customLicenseText.Enabled = true;
				background.Enabled = true;
			}
		}

		private void StudentsListAddress_TextChanged(object sender, EventArgs e)
		{
			using (StreamReader reader = new StreamReader(studentsListAddress.Text))
			using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				students_C = csv.GetRecords<Student_C>().ToList();
			}
			string[] fileLines = File.ReadAllLines(studentsListAddress.Text);

			try
			{
				if (fileLines.Length < 1) throw new ArgumentException("فایل خالی است.");

				if (!(fileLines[0].StartsWith("//") && fileLines[0].EndsWith("//"))) throw new ArgumentException("خط اول فایل باید با // شروع و تمام شود.");
				string[] values = fileLines[0].Trim('/').Split('،');
				if (values.Length != 8) throw new ArgumentException("تعداد موارد در خط اول " +
							 " در فایل لیست قرآن آموزان اشتباه است.");
				if (values[1] == "م")
				{
					cityRadioButton.Checked = false;
					districtRadioButton.Checked = true;
				}
				else if (values[1] != "ش") throw new ArgumentException("مورد دوم در خط اول در فایل لیست قرآن آموزان باید «ش» یا «م» (ش برای شهرستان و م برای منطقه) باشد.");
				province.Text = values[0];
				city.Text = values[2];
				darAlQuran.Text = values[3];
				darAlQuranManager.Text = values[4] + ' ' + values[5];
				DepartmentOfEducationManager.Text = values[6] + ' ' + values[7];

				foreach (Student_C student in students_C)
				{
					if (student.FirstName == "" || student.LastName == "" || student.FatherName == "" || student.Code == "")
					{
						throw new ArgumentException("بارگزاری مشخصات قرآن آموزان ناموفق بود.");
					}
				}
				message.BackColor = SystemColors.Control;
				message.Text = "لیست و مشخصات قرآن آموزان با موفقیت بارگذاری شد.";
				foreach (Student_C student in students_C) studentName.Items.Add(student.FirstName + " " + student.LastName);
			}
			catch (Exception ex)
			{
				message.BackColor = Color.IndianRed;
				message.Text = "خطا: " + ex.Message;
				studentName.Items.Clear();
			}
		}

		private void ShowStudentInfo(object sender, EventArgs e)
		{
			if (students_C != null)
			{
				foreach (Student_C student in students_C)
				{
					if ((sender == studentName && studentName.Text == student.GetFullName())
						|| (sender == studentCode && studentCode.Text.EnglishNumbersToPersian() == student.Code.EnglishNumbersToPersian()))
					{
						studentName.Text = student.GetFullName();
						fatherName.Text = student.FatherName;
						studentCode.Text = student.Code;
						break;
					}
				}
			}

			hasPicture = false;
			if (studentsPicturesAddress.Text.Length > 0 && studentName.Text.Length > 0)
			{
				string[] files = Directory.GetFiles(studentsPicturesAddress.Text);
				string path = "";
				foreach (string file in files)
				{
					if (Path.GetFileNameWithoutExtension(file).EnglishNumbersToPersian() == studentCode.Text.EnglishNumbersToPersian())
					{
						path = file;
						goto ImageFound;
					}
				}

				message.BackColor = Color.LightYellow;
				message.Text = "توجه: عکس قرآن آموز یافت نشد. گواهینامه بدون عکس خواهد بود.";
				studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
				studentPicture.Image = NAImage;
				return;

			ImageFound:

				if (!new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".jpe", ".jfif", ".bmp", ".dib", ".tif", ".tiff" }.Contains(Path.GetExtension(path)))
				{
					message.BackColor = Color.LightYellow;
					message.Text = "توجه: فرمت عکس قرآن آموز پشتیبانی نمی‌شود. گواهینامه بدون عکس خواهد بود.";
					studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
					studentPicture.Image = ErrorImage;
					return;
				}

				Image image;
				try
				{
					image = Image.FromFile(path);
				}
				catch (Exception)
				{
					message.BackColor = Color.LightYellow;
					message.Text = "خطا در بارگذاری عکس قرآن آموز. گواهینامه بدون عکس خواهد بود.";
					studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
					studentPicture.Image = ErrorImage;
					return;
				}

				float ratio = image.Width / (float)image.Height;
				if (ratio > 0.8 || ratio < 0.7)
				{
					message.BackColor = Color.IndianRed;
					message.Text = "نسبت عرض به ارتفاع عکس قرآن آموز باید بین ۰/۷ و ۰/۸ (سه در چهار) باشد. گواهینامه بدون عکس خواهد بود.";
					studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
					studentPicture.Image = ErrorImage;
					return;
				}
				studentPicture.SizeMode = PictureBoxSizeMode.StretchImage;
				studentPicture.Image = image;
				message.BackColor = SystemColors.Control;
				message.Text = "عکس قرآن آموز با موفقیت بارگذاری شد.";
				hasPicture = true;
			}
			else
			{
				studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
				studentPicture.Image = DefaultImage;
				message.BackColor = SystemColors.Control;
				message.Clear();
				if (studentName.Text == "" && studentCode.Text == "")
				{
					fatherName.Clear();
					studentCode.Clear();
				}
			}
		}

		private void Generate_Click(object sender, EventArgs e)
		{
			try
			{
				ResourceManager resourceManager = Properties.Resources.ResourceManager;
				string level_background;

				if (levelRadioButton.Checked) level_background = (level.SelectedIndex + 1).ToString();
				else level_background = (background.SelectedIndex + 1).ToString();

				Bitmap bitmap = (Bitmap)resourceManager.GetObject("_" + level_background);
				bitmap.SetResolution(96, 96);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				SolidBrush BlackBrush = new SolidBrush(Color.Black);
				SolidBrush BrownBrush = new SolidBrush(Color.FromArgb(137, 24, 28));
				Font BKoodak31 = new Font(BKoodak, 31);
				Font IranNastaliq58 = new Font(IranNastaliq, 58);
				Font IranNastaliq32 = new Font(IranNastaliq, 32);
				Font BNazanin37 = new Font(BNazanin, 37, FontStyle.Bold);

				List<string> EmptyFields = new List<string>();
				StringFormat RTL = new StringFormat
				{
					Alignment = StringAlignment.Far,
					FormatFlags = StringFormatFlags.DirectionRightToLeft
				};
				StringFormat RTLC = new StringFormat
				{
					Alignment = StringAlignment.Center,
					FormatFlags = StringFormatFlags.DirectionRightToLeft
				};

				if (province.Text.Length > 0) FitAndDrawString("اداره کل آموزش و پرورش استان " + province.Text, IranNastaliq32, RTLC, BlackBrush, ref graphics, bitmap.Width / 2, 460);
				else EmptyFields.Add("استان");

				if (city.Text.Length > 0)
				{
					FitAndDrawString("اداره آموزش و پرورش " + (cityRadioButton.Checked ? "شهرستان " : "منطقه ") + city.Text, IranNastaliq32, RTLC, BlackBrush, ref graphics, bitmap.Width / 2, 540);
					FitAndDrawString("منطقه / شهرستان " + city.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1175, 3025 - 15, 700);
				}
				else EmptyFields.Add("شهرستان");

				if (darAlQuran.Text.Length > 0)
				{
					FitAndDrawString("دارالقرآن " + darAlQuran.Text, IranNastaliq32, RTLC, BlackBrush, ref graphics, bitmap.Width / 2, 620);
					FitAndDrawString("مدیر دارالقرآن " + darAlQuran.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1810, 2955 - 15, 590);
				}
				else EmptyFields.Add("دارالقرآن");

				if (number.Text.Length > 0) FitAndDrawString(number.Text.EnglishNumbersToPersian(), BKoodak31, RTL, BlackBrush, ref graphics, 685, 1080 + 13, 300);
				else EmptyFields.Add("شماره");

				string monthString, academicYear;
				string[] splittedDate = date.Text.PersianNumbersToEnglish().Split('/');
				try
				{
					persianCalendar.ToDateTime(
						int.Parse(splittedDate[0]),
						int.Parse(splittedDate[1]),
						int.Parse(splittedDate[2]),
						0, 0, 0, 0
						);
				}
				catch (Exception)
				{
					message.BackColor = Color.IndianRed;
					message.Text = "تاریخ وارد شده نامعتبر است.";
					return;
				}
				string prevYear = (int.Parse(splittedDate[0]) - 1).ToString();
				string nextYear = (int.Parse(splittedDate[0]) + 1).ToString();
				if (dateCheckBox.Checked)
				{
					splittedDate[0] = splittedDate[0].Substring(splittedDate[0].Length - 2);
					prevYear = prevYear.Substring(prevYear.Length - 2);
					nextYear = nextYear.Substring(nextYear.Length - 2);
				}
				FitAndDrawString((splittedDate[0] + '/' + splittedDate[1] + '/' + splittedDate[2]).EnglishNumbersToPersian(), BKoodak31, RTL, BlackBrush, ref graphics, 685, 1180 + 13, 300);

				if (studentName.Text.Length > 0) FitAndDrawString(studentName.Text, IranNastaliq58, RTL, BlackBrush, ref graphics, 1440, 1700 - 5, 525);
				else EmptyFields.Add("نام قرآن آموز");

				if (fatherName.Text.Length > 0) FitAndDrawString(fatherName.Text, IranNastaliq58, RTL, BlackBrush, ref graphics, 780, 1700 - 5, 400);
				else EmptyFields.Add("نام پدر");

				if (studentCode.Text.Length > 0) FitAndDrawString(studentCode.Text.EnglishNumbersToPersian(), IranNastaliq58, RTL, BlackBrush, ref graphics, 1650, 1900 - 5, 400);
				else EmptyFields.Add("کد قرآن آموز");

				if (levelRadioButton.Checked)
				{
					FitAndDrawString("گذراندن سطح:", IranNastaliq58, RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1920, 2100 - 5);
					FitAndDrawString("(" + level.SelectedItem + ")", new Font(IranNastaliq, 77), RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1600, 2100 - 45);
				}
				else
				{
					FitAndDrawString(":", IranNastaliq58, RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1920, 2100 - 5);
					if (customLicenseText.Text.Length > 0) FitAndDrawString(customLicenseText.Text.EnglishNumbersToPersian(), new Font(IranNastaliq, 77), RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1880, 2100 - 45, 800);
					else EmptyFields.Add("نوع گواهینامه");
				}

				byte month = byte.Parse(splittedDate[1]);
				if (month <= 3) monthString = "بهار";
				else if (month <= 6) monthString = "تابستان ";
				else if (month <= 9) monthString = "پاییز";
				else monthString = "زمستان";
				FitAndDrawString(monthString, IranNastaliq58, RTL, BlackBrush, ref graphics, 1915, 2300 - 5);

				if (month < 7) academicYear = prevYear + '-' + splittedDate[0];
				else academicYear = splittedDate[0] + '-' + nextYear;
				FitAndDrawString(academicYear.EnglishNumbersToPersian(), IranNastaliq58, RTL, BlackBrush, ref graphics, 1410, 2300 - 5);

				if (score.Text.Length > 0) FitAndDrawString(score.Text.EnglishNumbersToPersian(), IranNastaliq58, RTL, BlackBrush, ref graphics, 1925, 2500 - 5, 400);
				else EmptyFields.Add("درجه گواهینامه");

				if (darAlQuranManager.Text.Length > 0) FitAndDrawString(darAlQuranManager.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1845, 2875 - 15, 550);
				else EmptyFields.Add("مدیر دارالقرآن");

				if (DepartmentOfEducationManager.Text.Length > 0) FitAndDrawString(DepartmentOfEducationManager.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1175, 2875 - 15, 700);
				else EmptyFields.Add("مدیر آموزش و پرورش");

				if (hasPicture)
				{
					Bitmap picture = ResizeImage(studentPicture.Image, 285, 380);
					picture.SetResolution(96, 96);
					graphics.DrawImage(picture, 438, 366);
				}

				bitmap.SetResolution(300, 300);

				graphics.DrawImage((Bitmap)resourceManager.GetObject("_" + level_background + "b"), 398, 336);

				message.BackColor = Color.Green;
				message.Text = "";
				if (EmptyFields.Count > 0)
				{
					message.BackColor = Color.LightYellow;
					if (EmptyFields.Count == 1) message.Text = "توجه: مورد «" + EmptyFields[0] + "» خالی است. ";
					else
					{
						message.Text = "توجه: موارد «";
						for (int i = 0; i < EmptyFields.Count; i++)
						{
							message.Text += EmptyFields[i];
							if (EmptyFields.Count - i > 2) message.Text += "، ";
							else if (EmptyFields.Count - i == 2) message.Text += " و ";
						}
						message.Text += "» خالی هستند. ";
					}
				}
				monthString = monthString.Trim();
				string dir = Path.Combine(saveAddress.Text, date.Text.Split('/')[0].PersianNumbersToEnglish(), monthString, levelRadioButton.Checked ? "سطح " + level.Text : customLicenseText.Text);
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
				bitmap.Save(Path.Combine(dir, studentName.Text + "(" + studentCode.Text.EnglishNumbersToPersian() + ").jpg"), ImageFormat.Jpeg);
				message.Text += "گواهینامه با موفقیت ایجاد شد.";
			}
			catch (Exception)
			{
				message.BackColor = Color.IndianRed;
				message.Text = "خطا در ایجاد گواهینامه";
			}
		}

		async private void HelpButton_Click(object sender, EventArgs e)
		{
			try
			{
				Version version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
				string fileName = Path.GetTempPath() + "DQLHLPv" + version.Major + '.' + version.Minor + ".pdf";
				if (!File.Exists(fileName))
				{
					FileStream fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
					await fileStream.WriteAsync(Properties.Resources.Help, 0, Properties.Resources.Help.Length);
				}
				Process.Start(fileName);
			}
			catch
			{
				message.BackColor = Color.IndianRed;
				message.Text = "خطا در نمایش راهنما";
			}
		}
	}

	public static class StringExtensions
	{
		public static string EnglishNumbersToPersian(this string text)
		{
			for (int i = 0; i < text.Length; i++) if (text[i] >= '0' && text[i] <= '9')
					text = text.Replace(text[i], (char)(text[i] + '۰' - '0'));
			return text;
		}
		
		public static string PersianNumbersToEnglish(this string text)
		{
			for (int i = 0; i < text.Length; i++) if (text[i] >= '۰' && text[i] <= '۹')
					text = text.Replace(text[i], (char)(text[i] + '0' - '۰'));
			return text;
		}
	}
}