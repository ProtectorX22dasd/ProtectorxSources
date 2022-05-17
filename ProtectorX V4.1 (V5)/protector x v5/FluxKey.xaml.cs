using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace protector_x_v5
{
    /// <summary>
    /// FluxKey.xaml etkileşim mantığı
    /// </summary>
    /// 
    public partial class FluxKey : Window
    {

        public FluxKey()
        {
            InitializeComponent();
        }
        private static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText)).Replace("=", "1");
        }
        private string HWID = Base64Encode(Environment.UserName + long.Parse(new ManagementObject("win32_logicaldisk.deviceid=\"c:\"")["VolumeSerialNumber"].ToString(), NumberStyles.HexNumber).ToString());

        private void fluxgetkey_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://fluxteam.xyz/ks/checkpoint/Start.php?HWID=" + this.HWID);
        }
        private static string HttpGet(string Url)
        {
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(Url);
            webClient.Dispose();
            return result;
        }
        public bool VerifyKey(string Key)
        {
            return !(HttpGet("https://fluxteam.xyz/ks/checkpoint/Verify.php?key=" + this.HWID + "&HWID=" + this.HWID) != this.HWID) || !(HttpGet("https://fluxteam.xyz/ks/checkpoint/Verify.php?key=" + Key + "&HWID=" + this.HWID) != this.HWID);
        }
        private void verkey_Click(object sender, RoutedEventArgs e)
        {
            new Task(delegate
            {
                MessageBox.Show("Checking Key Please Wait");
            });
            if (VerifyKey(keytext.Text.ToString()) == true)
            {
                protector_x_v5.Properties.Settings.Default.fluxkey = keytext.Text;
                protector_x_v5.Properties.Settings.Default.Save();
                this.Hide();
                inject();
            }
            else
            {
                MessageBox.Show("incorect key");
            }
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WaitNamedPipe(string pipe, int timeout = 10);
        public bool Exists(string Name)
        {
            return WaitNamedPipe("\\\\.\\pipe\\" + Name, 10);
        }
        private void inject()
        {
            if (!File.Exists(this.RobloxPath + "\\DACInject.exe"))
            {
                //this.LogConsole("DACInject.exe is not found.");
                System.Windows.MessageBox.Show("DACInject.exe is not found.\nThis program does not delete its self.\nYou have an active anti virus or a third party app causing issues with Fluxus.\nPlease disable and or remove them and reinstall Fluxus.", "Fluxus Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                Process.GetCurrentProcess().Kill();
                return;
            }
            bool flag = true;
            Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
            if (processesByName.Length != 0)
            {
                for (int i = 0; i < processesByName.Length; i++)
                {
                    if (!Exists("fluxus_send_pipe" + processesByName[i].Id.ToString()))
                    {
                        flag = false;
                    }
                }
            }
            else
            {
                flag = false;
            }
            if (flag)
            {
                //this.LogConsole("Fluxus already injected!");
                return;
            }
            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    Arguments = "/C Inject.bat",
                    WorkingDirectory = this.RobloxPath
                }
            }.Start();
        }
        private string RobloxPath = "C:\\Program Files (x86)\\Fluxus";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Task(delegate
            {
                MessageBox.Show("Checking Key Please Wait");
            });
            if (!Directory.Exists(this.RobloxPath))
            {
                Directory.CreateDirectory(this.RobloxPath);
            }
            if (!Directory.Exists(this.RobloxPath))
            {
                this.RobloxPath = Directory.GetCurrentDirectory() + "\\bin";
                System.Windows.MessageBox.Show("Failed to create Fluxus directory in program files.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            if (!File.Exists(this.RobloxPath + "/FluxusTeamAPI.dll"))
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Uri address = new Uri(HttpGet("https://fluxteam.xyz/dll"));
                    try
                    {
                        webClient.DownloadFile(address, this.RobloxPath + "/FluxusTeamAPI.dll");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error downloading DLL!\n\nPlease make sure any running ROBLOX and Fluxus processes are CLOSED!\n" + ex.ToString());
                    }
                }
            }
            if (!File.Exists(this.RobloxPath + "/Indicium Supra.dll"))
            {
                using (WebClient webClient2 = new WebClient())
                {
                    webClient2.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Uri address2 = new Uri(HttpGet("https://fluxteam.xyz/Indicium%20Supra"));
                    try
                    {
                        webClient2.DownloadFile(address2, this.RobloxPath + "/Indicium Supra.dll");
                    }
                    catch (Exception ex2)
                    {
                        System.Windows.Forms.MessageBox.Show(ex2.ToString());
                    }
                }
                if (!File.Exists(this.RobloxPath + "/DACInject.exe"))
                {
                    using (WebClient webClient3 = new WebClient())
                    {
                        webClient3.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        Uri address3 = new Uri(HttpGet("https://fluxteam.xyz/ij"));
                        try
                        {
                            webClient3.DownloadFile(address3, this.RobloxPath + "/DACInject.exe");
                        }
                        catch (Exception ex3)
                        {
                            System.Windows.Forms.MessageBox.Show(ex3.ToString());
                        }
                    }
                }
                if (!File.Exists(this.RobloxPath + "/Inject.bat"))
                {
                    using (StreamWriter streamWriter = new StreamWriter(this.RobloxPath + "/Inject.bat", false))
                    {
                        streamWriter.WriteLine("DACInject.exe \"" + this.RobloxPath + "\\FluxusTeamAPI.dll\"");
                        streamWriter.Close();
                    }
                }

                using (StreamWriter streamWriter2 = new StreamWriter(this.RobloxPath + "\\Verify.Fluxus", false))
                {
                    streamWriter2.Write(HWID);
                    streamWriter2.Close();
                }
                if (!File.Exists(this.RobloxPath + @"\Flux.Fluxus"))
                {
                    File.WriteAllText(this.RobloxPath + @"\Flux.Fluxus", Directory.GetCurrentDirectory());
                }
                if (!Directory.Exists(this.RobloxPath))
                {
                    this.RobloxPath = Directory.GetCurrentDirectory() + "\\bin";
                    System.Windows.MessageBox.Show("Failed to create Fluxus directory in program files.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                if (VerifyKey(protector_x_v5.Properties.Settings.Default.fluxkey) == true)
                {

                    inject();
                }
                else
                {
                    protector_x_v5.Properties.Settings.Default.fluxkey = "";
                    protector_x_v5.Properties.Settings.Default.Save();
                    MessageBox.Show("incorect key,GetKey");
                }
            }
        }
    }
}