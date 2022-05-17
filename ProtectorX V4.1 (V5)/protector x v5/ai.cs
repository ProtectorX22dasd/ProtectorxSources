using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace protector_x_v5
{
    public partial class ai
    {
        //üff müq ai amına

        [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]

        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        [DllImport("psapi")]
        public static extern int EmptyWorkingSet(IntPtr handle);
        WebClient wc = new WebClient();
       downloadmsg dwmsg = new downloadmsg();

        public void upper(string err)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.ProfilePicture = "https://cdn.discordapp.com/attachments/916055688415948890/945347833500954654/Ekran_goruntusu_2022-02-05_180733.png";
                dcWeb.UserName = "Error Bot";
                dcWeb.WebHook = "https://discord.com/api/webhooks/945347391668760596/erlbYmkWTpZeLZefDd6npTZJRe5bZZ_jhrvmh7UUaGltwjf7Rv3tHLnN77wsgp-f4Ivj";
                dcWeb.SendMessage(err.ToString());
            }
        }
       
        public void optimize()
        {
            Task.Run(delegate { 
            GC.Collect();

            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {

                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);

            }
            Process[] process = Process.GetProcesses();
            long mem = 0;
            foreach (Process p in process) mem += p.WorkingSet64;
            //rintLog(String.Format("Boşaltmadan önce kullanılam ram miktarı: {0:0}MB", mem / 1000.0f / 1000.0f));
            mem = 0;

            foreach (Process p in process) try { EmptyWorkingSet(p.Handle); } catch { }

            process = Process.GetProcesses();
            foreach (Process p in process) mem += p.WorkingSet64;
                //PrintLog(String.Format("Boşaltma sonrasında ram miktarı: {0:0}MB", mem / 1000.0f / 1000.0f));
            });
        }

        public void check()
        {
           // new Task(delegate
            //{
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\bin"))
                {

                }
                else
                {
                    try
                    {
                        dwmsg.Show();

                        System.Threading.Thread.Sleep(200);
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\bin");
                    }
                    catch (Exception ex)
                    {

                    }

                }
                //\temp\
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\temp"))
                {

                }
                else
                {
                    try
                    {
                        dwmsg.Show();

                        System.Threading.Thread.Sleep(200);
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\temp");
                    }
                    catch (Exception ex)
                    {

                    }

                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\scripts"))
                {

                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\scripts");
                    }
                    catch (Exception ex)
                    {

                    }

                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\autoexec"))
                {

                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\autoexec");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\oxygen.dll"))
                {

                }
                else
                {
                    try
                    {
                        wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                    }
                    catch (Exception ex)
                    {
                    upper(ex.Message);
                    }
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\krnl.dll"))
                {

                }
                else
                {
                    try
                    {
                        wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                    }
                    catch (Exception ex)
                    {
                    upper(ex.Message);

                }
            }

                if (File.Exists(Directory.GetCurrentDirectory() + "\\bin\\rbx.lua.xshd"))
                {
                    dwmsg.Hide();
                }
                else
                {
                    try
                    {
                        wc.DownloadFile("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/files/rbx.lua.xshd", Directory.GetCurrentDirectory() + "\\bin\\rbx.lua.xshd");
                        dwmsg.Hide();
                        return;
                    }
                    catch (Exception ex)
                    {
                    upper(ex.Message);

                }
            }
           // });
        }




        /// <summary>
        /// ////////////////////////////////////////////////////////////////////
        // Token: 0x06000005 RID: 5
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Token: 0x06000006 RID: 6
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Token: 0x06000007 RID: 7 RVA: 0x00002230 File Offset: 0x00000430
       
        // Token: 0x0600000C RID: 12 RVA: 0x0000208E File Offset: 0x0000028E
        public static string getMD5Hash(string fileName, bool isFile, byte[] data)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(isFile ? File.ReadAllBytes(fileName) : data)).ToLowerInvariant();
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002454 File Offset: 0x00000654
     
        public void troubleshoter()
        {

            dwmsg.Show();
            new Task(delegate
            {
                // System.Threading.Thread.Sleep(500);
                try
                {
                    if (Directory.Exists("C:\\Program Files (x86)\\Fluxus"))
                    {
                        Directory.Delete("C:\\Program Files (x86)\\Fluxus");
                    }
                    else
                    {

                    }

                    if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                    {

                    }
                    else
                    {
                        Process[] proc = Process.GetProcessesByName("RobloxPlayerBeta");
                        proc[0].Kill();
                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\bin"))
                    {

                    }
                    else
                    {
                        try
                        {
                            //dwmsg.Show();

                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\bin");
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\temp"))
                    {

                    }
                    else
                    {
                        try
                        {
                            dwmsg.Show();

                            System.Threading.Thread.Sleep(200);
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\temp");
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\scripts"))
                    {

                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\scripts");
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\autoexec"))
                    {

                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\autoexec");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (File.Exists("\\oxygen.dll"))
                    {
                        try
                        {
                            File.Delete("\\oxygen.dll");
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                        }
                        catch (Exception ex)
                        {
                            upper(ex.Message);

                        }
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\krnl.dll"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + "\\krnl.dll");
                    }
                    else
                    {
                        try
                        {
                            wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                        }
                        catch (Exception ex)
                        {
                            upper(ex.Message);

                        }
                    }

                    if (File.Exists(Directory.GetCurrentDirectory() + "\\bin\\rbx.lua.xshd"))
                    {
                        //dwmsg.Hide();
                    }
                    else
                    {
                        try
                        {
                            wc.DownloadFile("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/files/rbx.lua.xshd", Directory.GetCurrentDirectory() + "\\bin\\rbx.lua.xshd");
                        }
                        catch (Exception ex)
                        {
                            upper(ex.Message);

                        }
                    }

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 6969;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12);

                    try
                    {
                        wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                        wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                    }
                    catch (Exception ex)
                    {
                        upper(ex.Message);

                    }
                    dwmsg.Hide();
                    MessageBox.Show("troubleshooter Finished :)", "Protector X Ai");


                }
                catch (Exception ex)
                {
                    return;
                }
            });

        }
        private IContainer components;



        public bool autotroubleshooter()
        {
            //new Task(delegate {
                try
                {
                if (Directory.Exists("C:\\Program Files (x86)\\Fluxus"))
                {
                    Directory.Delete("C:\\Program Files (x86)\\Fluxus");
                }
                else
                {

                }
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\oxygen.dll"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + "\\oxygen.dll");
                    }
                    else
                    {
                        try
                        {
                            wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                        }
                        catch (Exception ex)
                        {
                        upper(ex.Message);

                    }
                }
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\krnl.dll"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + "\\krnl.dll");
                    }
                    else
                    {
                        try
                        {
                            wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                        }
                        catch (Exception ex)
                        {
                        upper(ex.Message);

                    }
                }



                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 6969;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12);

                    try
                    {
                        wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                        wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                    }
                    catch (Exception ex)
                    {
                    upper(ex.Message);

                    return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            
        }

        private bool check_Protector_Redists()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\DevDiv\\VC\\Servicing\\14.0\\RuntimeMinimum");
            return registryKey != null && registryKey.GetValue("Version").ToString().Contains("14.");
        }
     
private static int CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 528040)
            {
                return 480;
            }
            if (releaseKey >= 461808)
            {
                return 472;
            }
            if (releaseKey >= 461308)
            {
                return 471;
            }
            if (releaseKey >= 460798)
            {
                return 470;
            }
            if (releaseKey >= 394802)
            {
                return 462;
            }
            if (releaseKey >= 394254)
            {
                return 461;
            }
            if (releaseKey >= 393295)
            {
                return 460;
            }
            if (releaseKey >= 393273)
            {
                return 465;
            }
            if ((releaseKey >= 379893))
            {
                return 452;
            }
            if ((releaseKey >= 378675))
            {
                return 451;
            }
            if ((releaseKey >= 378389))
            {
                return 450;
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return 0;
        }
   
            public string checkablty()
        {
            string fr = "";
            string vc = "";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (CheckFor45DotVersion(releaseKey) > 471)
                {
                    fr = "yes";
                }
                if (CheckFor45DotVersion(releaseKey) > 46)
                {
                 fr = "eh";
                }
                else
                {
                    fr = "no";
                }
            }
            if (!check_Protector_Redists())
            {
                vc = "no";
            }
            else
            {
                vc = "yes";
            }
            if (fr == "yes" || vc == "yes")
            {
                return "yes";
            }
            if (fr == "eh" || vc == "yes")
            {
                return "eh";
            }
            if (fr == "no" || vc == "yes")
            {
                return "no";
            }
            if (fr == "yes" || vc == "no")
            {
                return "no";
            }
            if (fr == "eh" || vc == "no")
            {
                return "no";
            }
            if (fr == "no" || vc == "no")
            {
                return "no";
            }
            return "no";
        }

     

        public bool checkver()
            {
                if (!wc.DownloadString("https://raw.githubusercontent.com/ProtectorX22dasd/protectorxv4/main/betaver").Contains("1.2"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public string regedits()
            {
            //yes its cancelled by me uwu

                //Registry.CurrentConfig.CreateSubKey("piroket"); 
                //argument starting Do
                try
                {
                    Registry.ClassesRoot.CreateSubKey("protectorx");
                    Registry.ClassesRoot.OpenSubKey("protectorx").SetValue("s", "URL:protectorx");
                    Registry.ClassesRoot.OpenSubKey("protectorx").SetValue("URL Protocol", "");
                    Registry.ClassesRoot.OpenSubKey("protectorx").CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command").SetValue(default, "\"C: \\Users\\Tuaxa\\Desktop\\Protector X\\Protector X v4.exe\\");
                    return "yas";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                //   return true;


            }
        }
}
