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
		private readonly Color defaultColor = SystemColors.Control;

		private readonly Color errorColor = Color.IndianRed;
		
		private readonly Color successColor = Color.Green;
		
		private readonly Color warningColor = Color.LightYellow;
		
		private readonly Color noticeColor = Color.LightSkyBlue;

		private const string studentsFileName = "students";
		
		private const string dQInfoFileName = "info";
		
		private const string picturesPath = "pictures";

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

		private class Student
		{
			public string Name { get; set; }
			public string FatherName { get; set; }
			public string Code { get; set; }
		}

		private List<Student> students = new List<Student>();

		private class DarAlQuranInfo
		{
			public string Province { get; set; }
			public bool IsCity { get; set; }
			public string Region { get; set; }
			public string DQName { get; set; }
			public string DQManager { get; set; }
			public string DoEManager { get; set; }
		}

		private readonly DarAlQuranInfo darAlQuranInfo = new DarAlQuranInfo();

		private readonly PersianCalendar persianCalendar = new PersianCalendar();

		private bool hasPicture = false;

		public MainForm()
		{

			uint temp = 0;
			byte[][] fontData = new byte[][] { Properties.Resources.Samim, Properties.Resources.IranNastaliq_Web,
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


			InitializeComponent();

			SetAllControlsFont(Controls);

			Application.CurrentCulture = new CultureInfo("fa-IR");

			Font = new Font(Samim, Font.Size);


			DefaultImage = ConvertTextToImage("عکس" + Environment.NewLine + "قرآن آموز", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);

			NAImage = ConvertTextToImage("عکس" + Environment.NewLine + "قرآن آموز" + Environment.NewLine + "یافت نشد!", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);

			ErrorImage = ConvertTextToImage("خطا در" + Environment.NewLine + "بارگذاری" + Environment.NewLine + "عکس" + Environment.NewLine + "قرآن آموز!", Samim, 50, FontStyle.Regular,
				new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft },
				studentPicture.Width, studentPicture.Height, 72, 72, Color.Transparent, Color.SlateGray);


			studentPicture.Image = DefaultImage;
			score.SelectedIndex = 0;
			background.SelectedIndex = 0;

			DateTime now = DateTime.Today;

			date.Text = (
				persianCalendar.GetYear(now).ToString() + '/' +
				persianCalendar.GetMonth(now).ToString() + '/' +
				persianCalendar.GetDayOfMonth(now)
				).EnglishNumbersToPersian();

			if (File.Exists(dQInfoFileName + ".csv"))
			{
				try
				{
					using (StreamReader reader = new StreamReader(dQInfoFileName + ".csv"))
					using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
					{
						darAlQuranInfo = csv.GetRecords<DarAlQuranInfo>().ToList()[0];
					}

					province.Text = darAlQuranInfo.Province;
					if (!darAlQuranInfo.IsCity)
					{
						cityRadioButton.Checked = false;
						districtRadioButton.Checked = true;
					}
					region.Text = darAlQuranInfo.Region;
					dQName.Text = darAlQuranInfo.DQName;
					dQManager.Text = darAlQuranInfo.DQManager;
					doEManager.Text = darAlQuranInfo.DoEManager;
				}
				catch (Exception)
				{
					message.BackColor = errorColor;
					message.Text = "خطا در بارگزاری اطلاعات دارالقرآن. لطفا اطلاعات را دستی وارد کنید." + Environment.NewLine;

					if (File.Exists(dQInfoFileName + ".csv.BAK"))
					{
						int count = 1;
						while (File.Exists(dQInfoFileName + count.ToString() + ".csv.BAK"))
						{
							count++;
						}

						File.Move(dQInfoFileName + ".csv", dQInfoFileName + count.ToString() + ".csv.BAK");
					}
					else File.Move(dQInfoFileName + ".csv", dQInfoFileName + ".csv.BAK");
				}
			}
			else
			{
				message.BackColor = noticeColor;
				message.Text = "فایل اطلاعات دارالقرآن یافت نشد. نرم افزار فایل را به طور خودکار ایجاد خواهد کرد." + Environment.NewLine;
			}

			if (File.Exists(studentsFileName + ".csv"))
			{
				try
				{
					using (StreamReader reader = new StreamReader(studentsFileName + ".csv"))
					using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
					{
						students = csv.GetRecords<Student>().ToList();
					}

					foreach (Student student in students) studentName.Items.Add(student.Name);

					studentName.SelectedIndex = 0;
				}
				catch (Exception)
				{
					message.BackColor = errorColor;
					message.Text += "خطا در بارگزاری اطلاعات قرآن آموزان. لطفا اطلاعات را دستی وارد کنید.";
					studentName.Items.Clear();

					if (File.Exists(studentsFileName + ".csv.BAK"))
					{
						int count = 1;
						while (File.Exists(studentsFileName + count.ToString() + ".csv.BAK")) count++;

						File.Move(studentsFileName + ".csv", studentsFileName + count.ToString() + ".csv.BAK");
					}
					else File.Move(studentsFileName + ".csv", studentsFileName + ".csv.BAK");
				} 
			}
			else
			{
				message.BackColor = noticeColor;
				message.Text += "فایل اطلاعات قرآن آموزان یافت نشد. نرم افزار فایل را به طور خودکار ایجاد خواهد کرد.";
			}
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
			//Point point = new Point((int)x, (int)y);
			//TextRenderer.DrawText(graphics, text, font, point, ((SolidBrush)brush).Color, TextFormatFlags.RightToLeft);
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

		private Bitmap LoadBitmapUnlocked(string fileName)
		{
			using (Bitmap bmp = new Bitmap(fileName))
			{
				return new Bitmap(bmp);
			}
		}

		private void UpdateStudents()
		{
			if (studentCode.Text == "") return;

			bool newStudent = true;

			foreach (Student student in students)
			{
				if (student.Code == studentCode.Text)
				{
					student.Name = studentName.Text;
					student.FatherName = fatherName.Text;
					newStudent = false;
					break;
				}
			}

			if (newStudent)
			{
				students.Add(new Student { Name = studentName.Text, FatherName = fatherName.Text, Code = studentCode.Text });
			}

			students = students.OrderBy(o => o.Code).ToList();

			try
			{
				if (!File.Exists(studentsFileName + ".csv")) File.Create(studentsFileName + ".csv");
				using (StreamWriter writer = new StreamWriter(studentsFileName + ".csv"))
				using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					csv.WriteRecords(students);
				}
			}
			catch (Exception)
			{
				message.BackColor = errorColor;
				message.Text = "خطا هنگام ذخیره سازی اطلاعات قرآن آموز" + Environment.NewLine;
			}

			studentName.Items.Clear();
			foreach (Student st in students)
			{
				studentName.Items.Add(st.Name);
			}
		}

		private void GetStudentImage()
		{
			OpenFileDialog openFile = new OpenFileDialog
			{
				Title = "Open Student Picture",
				Multiselect = false,
				CheckFileExists = true,
				Filter = "All Supported Picture Files|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff"
			};
			if (openFile.ShowDialog() != DialogResult.OK) return;

			Bitmap image;
			try
			{
				image = LoadBitmapUnlocked(openFile.FileName);
			}
			catch (Exception)
			{
				message.BackColor = errorColor;
				message.Text = "خطا در بارگذاری عکس قرآن آموز." + Environment.NewLine;
				studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
				studentPicture.Image = ErrorImage;
				return;
			}

			float ratio = image.Width / (float)image.Height;
			if (ratio > 0.8 || ratio < 0.7)
			{
				message.BackColor = errorColor;
				message.Text = "نسبت عرض به ارتفاع عکس قرآن آموز باید بین ۰/۷ و ۰/۸ (سه در چهار) باشد." + Environment.NewLine;
				studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
				studentPicture.Image = ErrorImage;
				return;
			}
			studentPicture.SizeMode = PictureBoxSizeMode.StretchImage;
			studentPicture.Image = ResizeImage(image, 285, 380);
			hasPicture = true;
			studentPicture.Image.Save(picturesPath + "/" + studentCode.Text.EnglishNumbersToPersian() + ".jpg", ImageFormat.Jpeg);
		}

		private void UpdateDQInfo(object sender, EventArgs e)
		{
			darAlQuranInfo.Province = province.Text;
			darAlQuranInfo.IsCity = cityRadioButton.Checked;
			darAlQuranInfo.Region = region.Text;
			darAlQuranInfo.DQName = dQName.Text;
			darAlQuranInfo.DQManager = dQManager.Text;
			darAlQuranInfo.DoEManager = doEManager.Text;


			try
			{
				if (!File.Exists(dQInfoFileName + ".csv")) File.Create(dQInfoFileName + ".csv").Dispose();
				using (StreamWriter writer = new StreamWriter(dQInfoFileName + ".csv"))
				using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					List<DarAlQuranInfo> records = new List<DarAlQuranInfo> { darAlQuranInfo };
					csv.WriteRecords(records);
				}
			}
			catch (Exception)
			{
				message.BackColor = errorColor;
				message.Text = "خطا هنگام ذخیره سازی اطلاعات دارالقرآن";
			}
		}

		private void ShowStudentInfo(object sender, EventArgs e)
		{
			message.Text = "";

			if (students != null)
			{
				foreach (Student student in students)
				{
					if ((sender == studentName && studentName.Text == student.Name)
						|| (sender == studentCode && studentCode.Text.PersianNumbersToEnglish() == student.Code.PersianNumbersToEnglish()))
					{
						studentName.Text = student.Name;
						fatherName.Text = student.FatherName;
						studentCode.Text = student.Code;
						break;
					}
				}
			}

			hasPicture = false;

			if (studentCode.Text.Length > 0)
			{
				if (!Directory.Exists(picturesPath)) Directory.CreateDirectory(picturesPath);
				string[] files = Directory.GetFiles(picturesPath);
				string path = "";
				bool notFound = true;
				foreach (string file in files)
				{
					if (Path.GetFileName(file) == studentCode.Text.EnglishNumbersToPersian() + ".jpg")
					{
						path = file;
						notFound = false;
					}
				}

				if (notFound)
				{
					studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
					studentPicture.Image = NAImage;
					return; 
				}

				Bitmap image;
				try
				{
					image = LoadBitmapUnlocked(path);
				}
				catch (Exception)
				{
					message.BackColor = errorColor;
					message.Text = "خطا در بارگذاری عکس قرآن آموز.";
					studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
					studentPicture.Image = ErrorImage;
					return;
				}

				studentPicture.SizeMode = PictureBoxSizeMode.StretchImage;
				studentPicture.Image = image;
				hasPicture = true;
			}
			else
			{
				studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
				studentPicture.Image = DefaultImage;
				message.BackColor = defaultColor;
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

				level_background = (background.SelectedIndex + 1).ToString();

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

				if (region.Text.Length > 0)
				{
					FitAndDrawString("اداره آموزش و پرورش " + (cityRadioButton.Checked ? "شهرستان " : "منطقه ") + region.Text, IranNastaliq32, RTLC, BlackBrush, ref graphics, bitmap.Width / 2, 540);
					FitAndDrawString("منطقه / شهرستان " + region.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1175, 3030 - 15, 700);
				}
				else EmptyFields.Add("شهرستان");

				if (dQName.Text.Length > 0)
				{
					FitAndDrawString("دارالقرآن " + dQName.Text, IranNastaliq32, RTLC, BlackBrush, ref graphics, bitmap.Width / 2, 620);
					FitAndDrawString("مدیر دارالقرآن " + dQName.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1810, 2955 - 15, 590);
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
					message.BackColor = errorColor;
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

				FitAndDrawString(customLicenseText.Text.EnglishNumbersToPersian() + ": ", IranNastaliq58, RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1920, 2100 - 5);
				int cLTWidth = (int)graphics.MeasureString(customLicenseText.Text.EnglishNumbersToPersian() + ": ", IranNastaliq58, int.MaxValue, RTL).Width;
				if (customLicenseText.Text.Length > 0) FitAndDrawString(licenseText.Text.EnglishNumbersToPersian(), new Font(IranNastaliq, 77), RTL, new SolidBrush(Color.FromArgb(137, 24, 28)), ref graphics, 1880 - cLTWidth, 2100 - 45, 800);
				else EmptyFields.Add("نوع گواهینامه");

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

				if (dQManager.Text.Length > 0) FitAndDrawString(dQManager.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1845, 2875 - 15, 550);
				else EmptyFields.Add("مدیر دارالقرآن");

				if (doEManager.Text.Length > 0) FitAndDrawString(doEManager.Text, BNazanin37, RTLC, BlackBrush, ref graphics, 1175, 2875 - 15, 700);
				else EmptyFields.Add("مدیر آموزش و پرورش");

				if (!hasPicture)
				{
					DialogResult result = MessageBox.Show(
						"عکس قرآن آموز یافت نشد. آیا مایل هستید عکس را انتخاب و بارگزاری کنید؟",
						"توجه",
						MessageBoxButtons.YesNo);

					if (result == DialogResult.Yes) GetStudentImage();
				}

				if (hasPicture)
				{
					Bitmap picture = (Bitmap)studentPicture.Image;
					picture.SetResolution(96, 96);
					graphics.DrawImage(picture, 438, 366);
				}

				bitmap.SetResolution(300, 300);

				graphics.DrawImage((Bitmap)resourceManager.GetObject("_" + level_background + "b"), 398, 336);

				message.BackColor = successColor;
				message.Text = "";
				if (EmptyFields.Count > 0)
				{
					message.BackColor = warningColor;
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
				string dir = Path.Combine("گواهینامه‌ها", date.Text.Split('/')[0].EnglishNumbersToPersian(), monthString, licenseText.Text);
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
				bitmap.Save(Path.Combine(dir, studentName.Text + "(" + studentCode.Text.EnglishNumbersToPersian() + ").jpg"), ImageFormat.Jpeg);
				UpdateStudents();
				message.Text += "گواهینامه با موفقیت ایجاد شد.";
			}
			catch (Exception)
			{
				message.BackColor = errorColor;
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
				message.BackColor = errorColor;
				message.Text = "خطا در نمایش راهنما";
			}
		}

		private void DeleteStudent_Click(object sender, EventArgs e)
		{
			foreach (Student student in students)
			{
				if (student.Code == studentCode.Text)
				{
					students.Remove(student);
					string picturePath = picturesPath + "/" + studentCode.Text.EnglishNumbersToPersian() + ".jpg";
					if (File.Exists(picturePath))
					{
						studentPicture.SizeMode = PictureBoxSizeMode.CenterImage;
						studentPicture.Image = DefaultImage;
						File.Delete(picturePath);
					}
					studentName.Text = fatherName.Text = studentCode.Text = "";

					try
					{
						using (StreamWriter writer = new StreamWriter(studentsFileName + ".csv"))
						using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
						{
							csv.WriteRecords(students);
						}
					}
					catch (Exception)
					{
						break;
					}

					studentName.Items.Clear();
					foreach (Student st in students)
					{
						studentName.Items.Add(st.Name);
					}

					message.BackColor = noticeColor;
					message.Text = "قرآن آموز حذف شد.";
					return;
				}
			}
			message.BackColor = errorColor;
			message.Text = "خطا هنگام حذف قرآن آموز";
		}

		private void ChangePicture_Click(object sender, EventArgs e)
		{
			GetStudentImage();
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