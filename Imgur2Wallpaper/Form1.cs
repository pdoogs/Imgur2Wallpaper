using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Imgur2Wallpaper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadDefaults();
        }

        private void ReadDefaults()
        {
            try
            {
                urlTextBox.Text = Properties.Settings.Default.urlTextBox;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteDefaults()
        {
            try
            {
                Properties.Settings.Default.urlTextBox = urlTextBox.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ParseImgUrl(Match match)
        {
            string imgUrl;
            int quoteIndex = -1;

            imgUrl = match.ToString();
            quoteIndex = imgUrl.IndexOf("//");

            if (quoteIndex < 0)
                return "";

            imgUrl = imgUrl.Remove(0, quoteIndex);
            quoteIndex = imgUrl.IndexOf("\"");

            if (quoteIndex < 0)
                return "";

            imgUrl = imgUrl.Remove(quoteIndex);
            imgUrl = "http:" + imgUrl;

            Debug.WriteLine("ImgURL: " + imgUrl);
            return imgUrl;
        }

        string PickImgUrl(MatchCollection matches)
        {
            Random random = new Random();
            string imgUrl;
            int count = 0;

            while (count < matches.Count)
            {
                count++;

                Match match = matches[random.Next(matches.Count)];
                imgUrl = ParseImgUrl(match);

                if (imgUrl.Length > 0)
                {
                    return imgUrl;
                }
            }

            return "";
        }

        internal sealed class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int SystemParametersInfo(
                int uAction,
                int uParam,
                String lpvParam,
                int fuWinIni);
        }

        public enum PicturePosition
        {
            Tile, Center, Stretch, Fit, Fill
        }

        public static void SetBackground(string pathOnDisk, PicturePosition style)
        {
            Console.WriteLine("Setting background...");
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            switch (style)
            {
                case PicturePosition.Tile:
                    key.SetValue(@"PicturePosition", "0");
                    key.SetValue(@"TileWallpaper", "1");
                    break;
                case PicturePosition.Center:
                    key.SetValue(@"PicturePosition", "0");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Stretch:
                    key.SetValue(@"PicturePosition", "2");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Fit:
                    key.SetValue(@"PicturePosition", "6");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Fill:
                    key.SetValue(@"PicturePosition", "10");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
            }
            key.Close();

            const int SET_DESKTOP_BACKGROUND = 20;
            const int UPDATE_INI_FILE = 1;
            const int SEND_WINDOWS_INI_CHANGE = 2;
            NativeMethods.SystemParametersInfo(SET_DESKTOP_BACKGROUND, 0, pathOnDisk, UPDATE_INI_FILE | SEND_WINDOWS_INI_CHANGE);
            Console.WriteLine("Set background!\n");
        }

        private void DownloadImage(string url, string savePath)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        // If you want it as Png
                        yourImage.Save(savePath, ImageFormat.Png);

                        // If you want it as Jpeg
                        //yourImage.Save("path_to_your_file.jpg", ImageFormat.Jpeg);
                    }
                }

            }
        }

        public bool SetWallpaperFromImgurUrl(string url)
        {
            Uri uriResult;
            bool validUri = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!validUri)
                return false;

            using (WebClient client = new WebClient())
            {
                const string pathOnDisk = @"E:\Temp\temp.png";
                string htmlCode = client.DownloadString(url);
                Regex rg = new Regex(@"<img.*?src=""//i.imgur(.*?)""", RegexOptions.IgnoreCase);

                MatchCollection matches = rg.Matches(htmlCode);

                string imgUrl = PickImgUrl(matches);
                if (imgUrl.Length > 0)
                {
                    DownloadImage(imgUrl, pathOnDisk);
                    SetBackground(pathOnDisk, PicturePosition.Fill);
                }
            }

            return true;
        }

        private void setWallpaperButton_Click(object sender, EventArgs e)
        {
            SetWallpaperFromImgurUrl(urlTextBox.Text);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteDefaults();
        }
    }
}
