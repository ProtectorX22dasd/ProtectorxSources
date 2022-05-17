using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using ICSharpCode.AvalonEdit;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using Microsoft.Win32;
using System.Xml;

namespace protector_x_v5
{
    /// <summary>
    /// mainui.xaml etkileşim mantığı
    /// </summary>
    public partial class mainui : Window
    {
        public mainui()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                proai.upper(ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException);
            }
            proai.check();
            this.EditTabs.Loaded += delegate (object source, RoutedEventArgs e)
            {
                this.EditTabs.GetTemplateItem<Button>("AddTabButton").Click += delegate (object s, RoutedEventArgs f)
                {
                    this.MakeTab("", "New Tab");
                };

                TabItem ti = EditTabs.SelectedItem as TabItem;
                ti.GetTemplateItem<Button>("CloseButton").Visibility = Visibility.Hidden;
                ti.GetTemplateItem<Button>("CloseButton").Width = 0;
                ti.Header = "Main Tab";

                this.tabScroller = this.EditTabs.GetTemplateItem<ScrollViewer>("TabScrollViewer");
            };
        }

        private ScrollViewer tabScroller;



        private void AnimasyonuOlustur()
        {
            mainuiis.Height = 22;
            DoubleAnimation anim_main_he = new DoubleAnimation();
            anim_main_he.From = 0;
            anim_main_he.To = 350;
            anim_main_he.Duration = TimeSpan.FromSeconds(3);

            this.BeginAnimation(HeightProperty, anim_main_he);
            ///main_grid.BeginAnimation(HeightProperty, anim_main_he);
            //// Thread.Sleep(500);

            DoubleAnimation anim_main_wid = new DoubleAnimation();
            anim_main_wid.From = 0;
            anim_main_wid.To = 705;
            anim_main_wid.Duration = TimeSpan.FromSeconds(2);


            this.BeginAnimation(WidthProperty, anim_main_wid);
           // main_grid.BeginAnimation(WidthProperty, anim_main_wid);
            // Thread.Sleep(500);

            // Storyboard oluşturulur
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 360;
            anim.Duration = TimeSpan.FromSeconds(1);
            ooooohmaygad.BeginAnimation(HeightProperty,anim);

            DoubleAnimation anim_drgbrd_he = new DoubleAnimation();
            anim_drgbrd_he.From = 0;
            anim_drgbrd_he.To = 29;
            anim_drgbrd_he.Duration = TimeSpan.FromSeconds(3);

            drgbrddd.BeginAnimation(HeightProperty, anim_drgbrd_he);
            ///main_grid.BeginAnimation(HeightProperty, anim_main_he);
            //// Thread.Sleep(500);

            DoubleAnimation anim_drgbrd_wid = new DoubleAnimation();
            anim_drgbrd_wid.From = 0;
            anim_drgbrd_wid.To = 132;
            anim_drgbrd_wid.Duration = TimeSpan.FromSeconds(2);


            drgbrddd.BeginAnimation(WidthProperty, anim_drgbrd_wid);
            //Thickness margin = drgbrddd.Margin;

            drgbrddd.Margin = new Thickness(568, 0, 0, 301);
            //ooooohmaygad.BeginAnimation(OpacityProperty,anim);

            //Thread.Sleep(500);



        }
        //some shitty codes bla bla
        private void DropTab(object sender, DragEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;
            if (tabItem != null)
            {
                TabItem tabItem2 = e.Data.GetData(typeof(TabItem)) as TabItem;
                if (tabItem2 != null)
                {
                    if (!tabItem.Equals(tabItem2))
                    {
                        TabControl tabControl = tabItem.Parent as TabControl;
                        int insertIndex = tabControl.Items.IndexOf(tabItem2);
                        int num = tabControl.Items.IndexOf(tabItem);
                        tabControl.Items.Remove(tabItem2);
                        tabControl.Items.Insert(num, tabItem2);
                        tabControl.Items.Remove(tabItem);
                        tabControl.Items.Insert(insertIndex, tabItem);
                        tabControl.SelectedIndex = num;
                    }
                    return;
                }
            }
        }

        private void ScrollTabs(object sender, MouseWheelEventArgs e)
        {
            this.tabScroller.ScrollToHorizontalOffset(this.tabScroller.HorizontalOffset + (double)(e.Delta / 10));
        }

        private void MoveTab(object sender, MouseEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;
            if (tabItem == null)
            {
                return;
            }
            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                if (VisualTreeHelper.HitTest(tabItem, Mouse.GetPosition(tabItem)).VisualHit is Button)
                {
                    return;
                }
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.Move);
            }
        }

        private TextEditor current;

        public TextEditor GetCurrent()
        {
            if (this.EditTabs.Items.Count == 0)
            {
                return AvalonText;
            }
            else
            {
                return this.current = (this.EditTabs.SelectedContent as TextEditor);
            }
        }

        public TextEditor MakeEditor()
        {
            TextEditor textEditor = new TextEditor
            {
                ShowLineNumbers = true,
                Background = new SolidColorBrush(Color.FromRgb(33, 33, 33)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, byte.MaxValue)),
                Margin = new Thickness(0,0,0,0),
                FontFamily = new FontFamily("Consolas"),
                Style = (this.TryFindResource("TextEditorStyle1") as Style),
                HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            };
            textEditor.Options.EnableEmailHyperlinks = false;
            textEditor.Options.EnableHyperlinks = false;
            textEditor.Options.AllowScrollBelowDocument = true;
            using (Stream s = File.OpenRead($"{Directory.GetCurrentDirectory()}\\bin\\rbx.lua.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    textEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load
                        (reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                }
            }
            return textEditor;
        }


        public TabItem MakeTab(string text = "", string title = "Tab")
        {
            title = title + " " + EditTabs.Items.Count.ToString();
            bool loaded = false;
            TextEditor textEditor = MakeEditor();
            textEditor.Text = text;
            
            TabItem tab = new TabItem
            {
                Content = textEditor,
                Style = (base.TryFindResource("Tab") as Style),
                AllowDrop = true,
                Header = title
            };
            tab.MouseWheel += this.ScrollTabs;
            tab.Loaded += delegate (object source, RoutedEventArgs e)
            {
                if (loaded)
                {
                    return;
                }
                this.tabScroller.ScrollToRightEnd();
                loaded = true;
            };
            tab.MouseDown += delegate (object sender, MouseButtonEventArgs e)
            {
                if (e.OriginalSource is Border)
                {
                    if (e.MiddleButton == MouseButtonState.Pressed)
                    {
                        this.EditTabs.Items.Remove(tab);
                        return;
                    }
                }
            };

            tab.Loaded += delegate (object s, RoutedEventArgs e)
            {
                tab.GetTemplateItem<Button>("CloseButton").Click += delegate (object r, RoutedEventArgs f)
                {
                    this.EditTabs.Items.Remove(tab);
                };

                this.tabScroller.ScrollToRightEnd();
                loaded = true;
            };

            tab.MouseMove += this.MoveTab;
            tab.Drop += this.DropTab;
            string oldHeader = title;
            this.EditTabs.SelectedIndex = this.EditTabs.Items.Add(tab);
            return tab;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        websocket websocket = new websocket();

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
        ai proai = new ai();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Nesneleri birden çok kez atmayın")]
        WebClient wc = new WebClient();
        //theme thm = new theme();
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
            //thm.ApplyButton();
            if (protector_x_v5.Properties.Settings.Default.theme == 0)
            {

                //old
                this.Hide();
                MainWindow mfn = new MainWindow();
                mfn.Show();
            }
            //MessageBox.Show(Base64Decode());

            //modernuiiw.Show();

            //websocket.StartSocket();

            proai.check();

            bool flag = this.GetCurrent() != null;
            if (flag)
            {
                using (Stream s = File.OpenRead($"{Directory.GetCurrentDirectory()}\\bin\\rbx.lua.xshd"))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        this.GetCurrent().SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load
                            (reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                    }
                }
            }

            timer.Interval = TimeSpan.FromSeconds(2);
            timer.IsEnabled = true;
            timer.Tick += dispatcherTimer_Tick;

            timer.Start();



            try
            {

               stsapi.Content = "Api :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/api_status.txt");
               stsmain.Content = "Main :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/main_status.txt");
               stsnote.Content = "Status Note :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/status_note.txt");
               stsweb.Content = "Web :" + wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/status/web_status.txt");
            }
            catch (Exception excxws)
            {
                proai.upper(excxws.Message+"\n"+excxws.Data + "\n"+ excxws.StackTrace + "\n" + excxws.Source + "\n" + excxws.InnerException);
                return;
            }

            try
            {

              var fullFilePath = wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/image_link.txt");

              BitmapImage bitmap = new BitmapImage();
              bitmap.BeginInit();
               bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                eventimg.Source = bitmap;
                eventbtnd.Content = wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/button_name.txt");

            }
            catch (Exception excxws)
            {
                proai.upper(excxws.Message + "\n" + excxws.Data + "\n" + excxws.StackTrace + "\n" + excxws.Source + "\n" + excxws.InnerException);
                return;

            }
            if (proai.checkver() == true)
            {
                //proai.upper("Zort");
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
                //MessageBox.Show("ok");
                Task WebsocketTask = new Task(delegate { websocket.StartSocket(Port, @"\workspace"); }); // Start websocket
                //MessageBox.Show(Port.ToString());
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



        private void piro_Click(object sender, RoutedEventArgs e)
        {
           //AnimasyonuOlustur();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.GetCurrent() != null;
            if (flag)
            {
                MessageBox.Show(this.GetCurrent().Text);
            }
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
            lastbox.Items.Clear();//Clear Items in the LuaScriptList
            PopulateListBox(lastbox, "./scripts", "*.txt");
            PopulateListBox(lastbox, "./scripts", "*.lua");
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bool flag = this.GetCurrent() != null;
                if (flag)
                {
                    //MessageBox.Show(this.GetCurrent().Text);
                    this.GetCurrent().Text = File.ReadAllText($"./scripts/{lastbox.SelectedItem}");
                }
                //CodeEditor.Text = File.ReadAllText($"./scripts/{scriptbox.SelectedItem}");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void ProtectorLogos(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coded By Protector X Development Team , discord : tuaxa.pro/prd , Web : tuaxa.pro/prt");
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
        private void settings_theme_changeds(object sender, SelectionChangedEventArgs e)
        {

            if (settings_theme.SelectedIndex == 0)
            {
            
                //old
                protector_x_v5.Properties.Settings.Default.theme = 0;
                protector_x_v5.Properties.Settings.Default.Save();
                this.Hide();
                MainWindow mfn = new MainWindow();
                mfn.Show();
            }
            if (settings_theme.SelectedIndex == 1)
            {
                //new
                protector_x_v5.Properties.Settings.Default.theme = 1;
                protector_x_v5.Properties.Settings.Default.Save();
            }
        }
        private void eventbtn(object sender, RoutedEventArgs e)
        {
            Process.Start(wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/news/button_link.txt"));
        }

        private void wndw_mdown(object sender, MouseButtonEventArgs e)
        {
            bool drag = Mouse.LeftButton == MouseButtonState.Pressed;
            if (drag)
            {
                this.DragMove();
            }
        }

        private void redownpro(object sender, RoutedEventArgs e)
        {
            //wait for boots

            string pathv2 = Directory.GetCurrentDirectory() + @"\ProtectorXDownload.exe";
            if (Directory.Exists(pathv2) == true)
            {
                File.Delete(pathv2);
                MessageBox.Show("Download Started Please Wait");
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
                MessageBox.Show("Download Started Please Wait");
                using (WebClient Client = new WebClient())
                {


                        Client.DownloadFile("https://tuaxa.pro/Files/ProtectorX/PortectorXDownload.exe", pathv2);
                    Process.Start(pathv2);
                    Environment.Exit(0);
                    //MessageBox.Show("Downloaded");

                }
            }
        }

        private void redownfiles(object sender, RoutedEventArgs e)
        {
            proai.troubleshoter();
        }
        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private static void CheckDefender()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();

                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $true"); //real-time protection

                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $true"); //behavior monitoring

                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIOAVProtection $true"); //scans all downloaded files and attachments

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisablePrivacyMode $true"); //displaying threat history

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); //definition updates on startup

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableArchiveScanning $true"); //scan archive files, such as .zip and .cab files

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true"); // network protection 

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableScriptScanning $true"); //scanning of scripts during scans

                else if (line.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2"))
                    RunPS("Set-MpPreference -SubmitSamplesConsent 2"); //MAPSReporting 

                else if (line.StartsWith(@"MAPSReporting") && !line.EndsWith("0"))
                    RunPS("Set-MpPreference -MAPSReporting 0"); //MAPSReporting 

                else if (line.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force"); // high level threat // Allow

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 6"); // moderate level threat

                else if (line.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -LowThreatDefaultAction 6"); // low level threat

                else if (line.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 6"); // severe level threat
            }
        }

        private static void RunPS(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
    private void disdef_cl(object sender, RoutedEventArgs e)
        {
            RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0"); //Windows 10 1903 Redstone 6
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");

            CheckDefender();
            Process proc = new Process
            {
                StartInfo =
                           {
                               FileName  = "powershell",
                               Arguments = "-command \"Set-Service -Name WinDefend -StartupType Disabled\"",
                               UseShellExecute = true,
                               RedirectStandardError = true,
                               RedirectStandardOutput = true, //true
                               Verb = "runas"
                           }

            };
            Process procsd = new Process
            {
                StartInfo =
                           {
                               FileName  = "CMD.exe",
                               Arguments = "/c net stop WinDefend",
                               UseShellExecute = true,
                               RedirectStandardError = true,
                               RedirectStandardOutput = true, //true
                               Verb = "runas"
                           }

            };
            //                Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\WinDefend", "Start", 4);

        }

        private void reroblox_cl(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
            string ass = "";
            foreach (string subdirectory in Directory.GetDirectories(@"C:\Users\"+Environment.UserName+@"\AppData\Local\Roblox\Versions"))

            {
                if (subdirectory == "RobloxStudioLauncherBeta.exe")
                {

                }
                else { ass = subdirectory; }
                MessageBox.Show("ok1");

                /*/
                //Define path
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Roblox";
                // if path exist then
                if (Directory.Exists(path) == true)
                {
                    //Delete Roblox directory from %localappdata%
                    //Directory.Delete(path, true);
                    //Downloads RobloxPlayerLauncher and attempts to open to install
                    using (WebClient Client = new WebClient())
                    {

                        //FileInfo file = new FileInfo("RobloxPlayerBeta.exe");
                        Client.DownloadFile("http://tuaxascript.com/codesv2/RobloxPlayerBeta.exe", Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe");
                        File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", sa + @"\RobloxPlayerBeta.exe");
                        MessageBox.Show("Downloaded");

                        //Process.Start(file.FullName);


                        //Directory.GetFiles()
                    }
                }
                // if path dont exist
                else
                {
                    MessageBox.Show("Roblox directory does not exist, will attempt to download");
                    using (WebClient Client = new WebClient())
                    {
                        // MessageBox.Show(sa+@"\");
                        //FileInfo file = new FileInfo("RobloxPlayerBeta.exe");
                        Client.DownloadFile("http://tuaxascript.com/codesv2/RobloxPlayerBeta.exe", Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe");
                        File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", sa + @"\RobloxPlayerBeta.exe");
                        MessageBox.Show("Downloaded");

                        //Process.Start(file.FullName);


                        //Directory.GetFiles()
                    }
                }
                /*/
                //new Task(delegate
                //{
                MessageBox.Show("ok2");
                if (File.Exists(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe");
                    }
                    if (File.Exists(ass + @"\RobloxPlayerBeta.exe"))
                    {
                        File.Delete(ass + @"\RobloxPlayerBeta.exe");
                    }
                    //////////////////////////////////////////////////
                    string pathv2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Local" + @"\Roblox";
                    // if path exist then
                    if (Directory.Exists(pathv2) == true)
                    {
                        MessageBox.Show("Download Started Please Wait");
                        //Delete Roblox directory from %localappdata%
                        // Directory.Delete(pathv2, true);
                        //Downloads RobloxPlayerLauncher and attempts to open to install
                        using (WebClient Client = new WebClient())
                        {

                            //FileInfo file = new FileInfo("RobloxPlayerBeta.exe");
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe"))
                            {
                                File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", ass + @"\RobloxPlayerBeta.exe");

                            }
                            else
                            {
                                Client.DownloadFile("http://tuaxascript.com/codesv2/RobloxPlayerBeta.exe", Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe");

                                File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", ass + @"\RobloxPlayerBeta.exe");

                            }
                            MessageBox.Show("Downloaded");

                        }
                    }
                    // if path dont exist
                    else
                {
                        MessageBox.Show("Download Started Please Wait");
                        using (WebClient Client = new WebClient())
                        {

                            //FileInfo file = new FileInfo("RobloxPlayerBeta.exe");
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe"))
                            {
                                File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", ass + @"\RobloxPlayerBeta.exe");

                            }
                            else
                            {
                                Client.DownloadFile("http://tuaxascript.com/codesv2/RobloxPlayerBeta.exe", Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe");

                                File.Move(Directory.GetCurrentDirectory() + @"\temp\RobloxPlayerBeta.exe", ass + @"\RobloxPlayerBeta.exe");

                            }
                            MessageBox.Show("Downloaded");
                        }
                    }
                MessageBox.Show("ok4");

                // });
            }
        }

        private void starttrob(object sender, RoutedEventArgs e)
        {
            proai.troubleshoter();
        }
        /// <summary>
        /// ////////////////////////////////////////////////

        private void settings_clck(object sender, RoutedEventArgs e)
        {
            Home_Grid.Visibility = Visibility.Hidden;
            ExecutorGrid.Visibility = Visibility.Hidden;
            shubgrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;

        }

        private void shub_clck(object sender, RoutedEventArgs e)
        {
            Home_Grid.Visibility = Visibility.Hidden;
            ExecutorGrid.Visibility = Visibility.Hidden;
            shubgrid.Visibility = Visibility.Visible;
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        private void executorbtn_clck(object sender, RoutedEventArgs e)
        {
            Home_Grid.Visibility = Visibility.Hidden;
            ExecutorGrid.Visibility = Visibility.Visible;
            shubgrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        private void homclck(object sender, RoutedEventArgs e)
        {
            Home_Grid.Visibility = Visibility.Visible;
            ExecutorGrid.Visibility = Visibility.Hidden;
            shubgrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        private void execute_scripthub_thubs(object sender, RoutedEventArgs e)
        {
        //new Task(delegate { proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/THUB/main/Main.lua', true))()"); });
            Task.Run(() => proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/THUB/main/Main.lua', true))()"));
        }

        private void execute_scripthub_protectorxhubs(object sender, RoutedEventArgs e)
        {
            //new Task(delegate { proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/ProtectorX22dasd/Protector-X-script/main/Protector%20X%20Hub.lua', true))()"); });
            Task.Run(() => proapi.execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/ProtectorX22dasd/Protector-X-script/main/Protector%20X%20Hub.lua', true))()"));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        api proapi = new api();
        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.GetCurrent() != null;
            if (flag)
            {
                proapi.execute(this.GetCurrent().Text);
            }
        }

        private void savefile_Button_Click(object sender, RoutedEventArgs e)
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
                    bool flag = this.GetCurrent() != null;
                    if (flag)
                    {
                        streamWriter.Write(this.GetCurrent().Text);
                        streamWriter.Dispose();
                        //this.GetCurrent().Text = "";

                    }



                }
                catch
                {

                }
            }
        }

        private void openfile_Button_Click(object sender, RoutedEventArgs e)
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
                        bool flag = this.GetCurrent() != null;
                        if (flag)
                        {
                            //MessageBox.Show(this.GetCurrent().Text);
                            this.GetCurrent().Text = (File.ReadAllText(openfiledialog.FileName));
                        }
                        //CodeEditor.Text = (File.ReadAllText(openfiledialog.FileName));
                    }
                }
            }
            catch
            {

            }
        }
            private void clear_Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.GetCurrent() != null;
            if (flag)
            {
                //MessageBox.Show(this.GetCurrent().Text);
                this.GetCurrent().Text = "";
            }
        }

        private void Attach_Button_Click(object sender, RoutedEventArgs e)
        {
            proapi.inject();
        }

        private void mainuiis_Initialized(object sender, EventArgs e)
        {
            proai.check();

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

        private void prtoptclicked(object sender, RoutedEventArgs e)
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

        private void protectoropt_box_Checked()
        {

        }
    }
}
