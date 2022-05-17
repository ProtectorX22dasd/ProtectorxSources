using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace protector_x_v5
{
    public class TImage
    {
        public string Path;
        public bool Online;

        public TImage()
        {
            Path = "";
            Online = false;
        }
    }
    public class Eb
    {
        public string sa { get; set; }
    }

    public class Example
    {
        public Eb eb { get; set; }
    }

    internal class theme
    {
       

        public static BitmapImage ConvertImage(TImage ThemeImage)
        {
            try
            {
                if (ThemeImage.Path == "") return null;
                if (!ThemeImage.Online) return new BitmapImage(new Uri(ThemeImage.Path));
                using (var WC = new WebClient())
                {
                    var Data = WC.DownloadData(ThemeImage.Path);

                    using (var Stream = new MemoryStream(Data))
                    {
                        var Bitmap = new BitmapImage();
                        Bitmap.BeginInit();
                        Bitmap.StreamSource = Stream;
                        Bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        Bitmap.EndInit();
                        Bitmap.Freeze();

                        return Bitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to parse image.\n\nException details:\n" + ex.Message,
                    "Synapse X Image Parser");
                return null;
            }
        }
        /*/
        public static void ApplyButton(Button Button, TYieldButton ThemeButton)
        {
            if (!string.IsNullOrWhiteSpace(ThemeButton.Image.Path))
                Button.Background = new ImageBrush(ConvertImage(ThemeButton.Image));
            else
                Button.Background = ConvertColor(ThemeButton.BackColor);

            Button.Foreground = ConvertColor(ThemeButton.TextColor);
            Button.FontFamily = new FontFamily(ThemeButton.Font.Name);
            Button.FontSize = ThemeButton.Font.Size;
            Button.Content = ThemeButton.TextNormal;
        }
        /*/
        public void ApplyButton(/*/Button Button, TYieldButton ThemeButton/*/)
        {
            Eb exmp = new Eb();
            string json = JsonConvert.SerializeObject(exmp);
            MessageBox.Show(json);
            /*/
            using (System.IO.StreamReader _StreamReader = new System.IO.StreamReader(Directory.GetCurrentDirectory() + @"\bin\sa.json"))
            {
                string jsonData = _StreamReader.ReadToEnd();
                List<Eb> listPerson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Eb>>(jsonData);

                foreach (var _Person in listPerson)
                {
                    MessageBox.Show(_Person.sa);

                }
            }
            /*/
            //MessageBox.Show("s");
        }
    }
}
