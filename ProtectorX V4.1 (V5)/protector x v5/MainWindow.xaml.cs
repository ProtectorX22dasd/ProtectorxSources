using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using WeAreDevs_API;
using KrnlAPI;
using System.Net;
using System.Diagnostics;
using System.Windows.Threading;
using System.Net.Sockets;


namespace protector_x_v5
{


    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {

        WeAreDevs_API.ExploitAPI wrd = new WeAreDevs_API.ExploitAPI();

        api api = new api();

        public MainWindow()
        {
            proai.check();
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                proai.upper(ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private int GetAvailablePort(int Port)
        {

            IPAddress IP = Dns.GetHostEntry("localhost").AddressList[0];
            if (Port == 9999) return 0; // Yeah, you dont have any ports you brainiac.

            try
            {
                TcpListener Listener = new TcpListener(IP, Port);
                Listener.Start();
                Listener.Stop();
                return Port;
            }
            catch (SocketException) // Port not available oof. Let's scan again!
            {
                return GetAvailablePort(Port + 1);
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Nesneleri birden çok kez atmayın")]
        private void LoadHighlighting()
        {
            using (Stream s = File.OpenRead($"{Directory.GetCurrentDirectory()}\\bin\\rbx.lua.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    CodeEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load
                        (reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                }
            }
        }
        WebClient wc = new WebClient();
        ai proai = new ai();
        //modernui modernuiiw = new modernui();

        private void exxxzxx()
        {
            // modernuiiw.Show();
        }

        websocket websocket = new websocket();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (proai.checkablty() == "yes")
            {

            }
            if (proai.checkablty() == "eh")
            {
                Process.Start("https://protectorx.ga/req.html");
                //Environment.Exit(0);
                MessageBox.Show("A few things are missing from your computer please follow the instructions (but protector x run without theese files but we reccomend download)");
            }
            if (proai.checkablty() == "no")
            {
                Process.Start("https://protectorx.ga/req.html");
                Environment.Exit(0);
                MessageBox.Show("Follow Instructions");
            }

            //MessageBox.Show(Base64Decode());

            //modernuiiw.Show();

            //websocket.start();

            if (protector_x_v5.Properties.Settings.Default.theme == 1)
            {
                mainui mn = new mainui();
                mn.Show();
                this.Hide();
            }

            timer.Interval = TimeSpan.FromSeconds(2);
            timer.IsEnabled = true;
            timer.Tick += dispatcherTimer_Tick;

            timer.Start();

            LoadHighlighting();


            try
            {

                sts_api.Content = "Api :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/api_status.txt");
                sts_main.Content = "Main :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/main_status.txt");
                sts_note.Content = "Status Note :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/status_note.txt");
                sts_web.Content = "Web :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/web_status.txt");
            }
            catch (Exception excxws)
            {
                proai.upper(excxws.Message + "\n" + excxws.Data + "\n" + excxws.StackTrace + "\n" + excxws.Source + "\n" + excxws.InnerException);
                return;
            }

            try
            {

                var fullFilePath = wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/image_link.txt");

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                news_image.Source = bitmap;
                news_button.Content = wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/button_name.txt");

            }
            catch (Exception excxws)
            {
                proai.upper(excxws.Message + "\n" + excxws.Data + "\n" + excxws.StackTrace + "\n" + excxws.Source + "\n" + excxws.InnerException);
                return;

            }
            if (proai.checkver() == true)
            {

            }
            else
            {
                this.Hide();

                MessageBox.Show("(AutoUpdater) Update Found New Version : V4." + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/version") + "Your Version : V4.0");

                //Process.Start("https://discord.gg/F9gzyMbSuP");
                //TODO
                string pathv2 = Directory.GetCurrentDirectory() + @"\ProtectorXDownload.exe";
                if (Directory.Exists(pathv2) == true)
                {
                    File.Delete(pathv2);
                    //MessageBox.Show("Download Started Please Wait");
                    //Delete Roblox directory from %localappdata%
                    // Directory.Delete(pathv2, true);
                    //Downloads RobloxPlayerLauncher and attempts to open to install
                    using (WebClient Client = new WebClient())
                    {

                        //FileInfo file = new FileInfo("RobloxPlayerBeta.exe");

                        Client.DownloadFile("https://tuaxa.pro/Files/ProtectorX/PortectorXDownload.exe", pathv2);
                        Process.Start(pathv2);
                        Environment.Exit(0);

                        //MessageBox.Show("Downloaded");

                    }
                }
                // if path dont exist
                else
                {
                    //MessageBox.Show("Download Started Please Wait");
                    using (WebClient Client = new WebClient())
                    {


                        Client.DownloadFile("https://tuaxa.pro/Files/ProtectorX/PortectorXDownload.exe", pathv2);
                        Process.Start(pathv2);
                        Environment.Exit(0);
                        //MessageBox.Show("Downloaded");

                    }
                }
            }


            int Port = GetAvailablePort(9000);

            if (Port == 0)
            {

                return;
            }
            if (websocket.isSocketRunning == true)
            {
                return;
            }
            if (websocket.isSocketRunning == false)
            {
                protector_x_v5.Properties.Settings.Default.port = Port.ToString();
                protector_x_v5.Properties.Settings.Default.Save();
                Task WebsocketTask = new Task(delegate { websocket.StartSocket(Port, @"\workspace"); }); // Start websocket
               // MessageBox.Show(Port.ToString());
                WebsocketTask.Start();
            }


            //MessageBox.Show(proai.regedits());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (protector_x_v5.Properties.Settings.Default.api == "1")
            {
                settings_api.SelectedIndex = Int32.Parse(protector_x_v5.Properties.Settings.Default.api);
            }
            else if (protector_x_v5.Properties.Settings.Default.api == "2")
            {
                settings_api.SelectedIndex = Int32.Parse(protector_x_v5.Properties.Settings.Default.api);
            }
            else if (protector_x_v5.Properties.Settings.Default.api == "3")
            {
                settings_api.SelectedIndex = Int32.Parse(protector_x_v5.Properties.Settings.Default.api);
            }
            else if (protector_x_v5.Properties.Settings.Default.api == "4")
            {
                settings_api.SelectedIndex = Int32.Parse(protector_x_v5.Properties.Settings.Default.api);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (protector_x_v5.Properties.Settings.Default.top_most == true)
            {
                topmost_box.IsChecked = protector_x_v5.Properties.Settings.Default.top_most;
            }
            else if (protector_x_v5.Properties.Settings.Default.top_most == false)
            {
                topmost_box.IsChecked = protector_x_v5.Properties.Settings.Default.top_most;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (protector_x_v5.Properties.Settings.Default.protectorcst == true)
            {
                protectorfnc_box.IsChecked = protector_x_v5.Properties.Settings.Default.protectorcst;
            }
            else if (protector_x_v5.Properties.Settings.Default.protectorcst == false)
            {
                protectorfnc_box.IsChecked = protector_x_v5.Properties.Settings.Default.protectorcst;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (protector_x_v5.Properties.Settings.Default.optimize == true)
            {
                protectoropt_box.IsChecked = protector_x_v5.Properties.Settings.Default.protectorcst;
            }
            else if (protector_x_v5.Properties.Settings.Default.optimize == false)
            {
                protectoropt_box.IsChecked = protector_x_v5.Properties.Settings.Default.protectorcst;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (protector_x_v5.Properties.Settings.Default.theme == 0)
            {
                settings_theme.SelectedIndex = protector_x_v5.Properties.Settings.Default.theme;
            }
            else if (protector_x_v5.Properties.Settings.Default.theme == 1)
            {
                settings_theme.SelectedIndex = protector_x_v5.Properties.Settings.Default.theme;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void home_button(object sender, RoutedEventArgs e)
        {
            home_grid.Visibility = Visibility.Visible;
            scripthub.Visibility = Visibility.Hidden;
            executer.Visibility = Visibility.Hidden;
            settings_grid.Visibility = Visibility.Hidden;
        }

        private void executer_button(object sender, RoutedEventArgs e)
        {
            settings_grid.Visibility = Visibility.Hidden;
            scripthub.Visibility = Visibility.Hidden;
            executer.Visibility = Visibility.Visible;
            home_grid.Visibility = Visibility.Hidden;
        }

        api proapi = new api();

        private void attach_executer_button(object sender, RoutedEventArgs e)
        {
            proapi.inject();
        }

        private void execute_scripthub_thub(object sender, RoutedEventArgs e)
        {
            Task.Run(() => proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/THUB/main/Main.lua', true))()"));
        }

        private void execute_scripthub_protectorxhub(object sender, RoutedEventArgs e)
        {
            Task.Run(() => proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/ProtectorX22dasd/Protector-X-script/main/Protector%20X%20Hub.lua', true))()"));
        }

        DispatcherTimer timer = new DispatcherTimer();

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (protector_x_v5.Properties.Settings.Default.optimize == true)
            {
                proai.optimize();
            }
            scriptbox.Items.Clear();//Clear Items in the LuaScriptList
            PopulateListBox(scriptbox, "./scripts", "*.txt");
            PopulateListBox(scriptbox, "./scripts", "*.lua");
        }


        private void executer_button_savefile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Lua file (*.lua)|*.lua|Text File (*.txt)|*.txt";
            saveFileDialog.Title = "Protector X";
            Nullable<bool> result = saveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(File.Create(saveFileDialog.FileName));

                    streamWriter.Write(CodeEditor.Text);
                    streamWriter.Dispose();

                }
                catch
                {

                }
            }

        }

        private void executer_button_clear(object sender, RoutedEventArgs e)
        {
            CodeEditor.Clear();
        }

        private void executer_button_execute(object sender, RoutedEventArgs e)
        {
            api.execute(CodeEditor.Text);
        }

        private void close_button(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void minimaze_button(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void settings_button(object sender, RoutedEventArgs e)
        {
            settings_grid.Visibility = Visibility.Visible;
            scripthub.Visibility = Visibility.Hidden;
            executer.Visibility = Visibility.Hidden;
            home_grid.Visibility = Visibility.Hidden;
        }

        private void scripthub_button(object sender, RoutedEventArgs e)
        {
            settings_grid.Visibility = Visibility.Hidden;
            scripthub.Visibility = Visibility.Visible;
            executer.Visibility = Visibility.Hidden;
            home_grid.Visibility = Visibility.Hidden;
        }


        private void news_button_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/button_link.txt"));
            }
            catch (Exception ex)
            {
                proai.upper(ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException);
            }
        }


        private void help_button_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coding 😼");
        }

        private void discord_click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/F9gzyMbSuP");
        }

        private void youtube_click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCNKzaiXEoPPqAZq0KkOkx8w");
        }

        private void tiktok_click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.tiktok.com/@protectorxyoutube?");
        }

        private void twitter_click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/berkaykarakas4");

        }

        private void help_button(object sender, RoutedEventArgs e)
        {
            proai.troubleshoter();
        }

        private void topmost_box_chechked(object sender, RoutedEventArgs e)
        {
            if (topmost_box.IsChecked == true)
            {
                protector_x_v5.Properties.Settings.Default.top_most = true;
                protector_x_v5.Properties.Settings.Default.Save();
                this.Topmost = true;
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom("#FF00FF97");
                brush.Freeze();

                topmost_box.Background = brush;

            }
            else
            {
                protector_x_v5.Properties.Settings.Default.top_most = false;
                protector_x_v5.Properties.Settings.Default.Save();
                this.Topmost = false;
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom("#FF18182A");
                brush.Freeze();

                topmost_box.Background = brush;
            }
        }

        private void settings_api_changed(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(settings_api.SelectedIndex.ToString());
            if (settings_api.SelectedIndex == 0)
            {
                //wrd
                protector_x_v5.Properties.Settings.Default.api = "1";
                protector_x_v5.Properties.Settings.Default.Save();
                //MessageBox.Show(Protector_X_v4.Properties.Settings.Default.api);
            }
            else if (settings_api.SelectedIndex == 1)
            {
                //krnl
                protector_x_v5.Properties.Settings.Default.api = "2";
                protector_x_v5.Properties.Settings.Default.Save();
                //MessageBox.Show(Protector_X_v4.Properties.Settings.Default.api);
            }
            else if (settings_api.SelectedIndex == 2)
            {
                //oxy
                protector_x_v5.Properties.Settings.Default.api = "3";
                protector_x_v5.Properties.Settings.Default.Save();
                //MessageBox.Show(Protector_X_v4.Properties.Settings.Default.api);
            }
            else if (settings_api.SelectedIndex == 3)
            {
                //fluxus
                protector_x_v5.Properties.Settings.Default.api = "4";
                protector_x_v5.Properties.Settings.Default.Save();
                //MessageBox.Show(Protector_X_v4.Properties.Settings.Default.api);
            }
        }

        private void scriptbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CodeEditor.Text = File.ReadAllText($"./scripts/{scriptbox.SelectedItem}");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void drag_mousedown(object sender, MouseButtonEventArgs e)
        {
            bool drag = Mouse.LeftButton == MouseButtonState.Pressed;
            if (drag)
            {
                this.DragMove();
            }
        }
        downloadmsg dwmsg = new downloadmsg();
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            proai.troubleshoter();

        }
      


        private void executer_button_openfile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfiledialog = new System.Windows.Forms.OpenFileDialog();
            openfiledialog.Filter = "Lua file (*.lua)|*.lua|Text File (*.txt)|*.txt";
            openfiledialog.FilterIndex = 2;
            openfiledialog.Title = "Protector X";
            openfiledialog.RestoreDirectory = true;
            if (openfiledialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            try
            {
                Stream stream;
                if ((stream = openfiledialog.OpenFile()) != null)
                {
                    using (stream)
                    {
                        CodeEditor.Text = (File.ReadAllText(openfiledialog.FileName));
                    }
                }
            }
            catch
            {

            }
        }


        private void protectorxlogoside_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coded By Protector X Development Team , discord : tuaxa.pro/prd , Web : tuaxa.pro/prt");
        }

        private void settings_theme_changeds(object sender, SelectionChangedEventArgs e)
        {
            if (settings_theme.SelectedIndex == 0)
            {
                //old
                protector_x_v5.Properties.Settings.Default.theme = 0;
                protector_x_v5.Properties.Settings.Default.Save();
            }
            if (settings_theme.SelectedIndex == 1)
            {
                //new
                protector_x_v5.Properties.Settings.Default.theme = 1;
                protector_x_v5.Properties.Settings.Default.Save();
                this.Hide();
                mainui mn = new mainui();
                mn.Show();
            }
        }

        private void prt_clicked(object sender, RoutedEventArgs e)
        {

            if (protectorfnc_box.IsChecked == true)
            {
                protector_x_v5.Properties.Settings.Default.protectorcst = true;

                protector_x_v5.Properties.Settings.Default.Save();
                proapi.executeprotectorx();
            }
            else
            {
                protector_x_v5.Properties.Settings.Default.protectorcst = false;
                protector_x_v5.Properties.Settings.Default.Save();

            }   
        }

        private void protoptclicked(object sender, RoutedEventArgs e)
        {
            if (protectoropt_box.IsChecked == true)
            {
                protector_x_v5.Properties.Settings.Default.optimize = true;

                protector_x_v5.Properties.Settings.Default.Save();
                //proapi.executeprotectorx();
            }
            else
            {
                protector_x_v5.Properties.Settings.Default.optimize = false;
                protector_x_v5.Properties.Settings.Default.Save();

            }
        }
    }

}

