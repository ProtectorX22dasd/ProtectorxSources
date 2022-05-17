using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using protector_x_v5.Properties;
using WeAreDevs_API;
using KrnlAPI;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.CSharp.RuntimeBinder;
//using Newtonsoft.Json;
using System.Net.Sockets;
using System.IO.Pipes;
using WeAreDevs_API;

namespace protector_x_v5
{
    class api
    {
        //interesting.

        private class BasicInject
        {

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool WaitNamedPipe(string name, int timeout);

            // Token: 0x06000034 RID: 52
            [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern IntPtr LoadLibraryA(string lpFileName);

            // Token: 0x06000035 RID: 53
            [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
            internal static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

            // Token: 0x06000036 RID: 54
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            // Token: 0x06000037 RID: 55
            [DllImport("kernel32.dll")]
            internal static extern IntPtr OpenProcess(BasicInject.ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

            // Token: 0x06000038 RID: 56
            [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
            internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

            // Token: 0x06000039 RID: 57
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

            // Token: 0x0600003A RID: 58
            [DllImport("kernel32.dll")]
            internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

            // Token: 0x0600003B RID: 59
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

            // Token: 0x0600003C RID: 60 RVA: 0x000026AC File Offset: 0x000008AC
            WeAreDevs_API.ExploitAPI wrd = new WeAreDevs_API.ExploitAPI();
            public bool InjectDLL(string dll, string proecessname)
            {
                if (dll == null)
                {
                    return false;
                }
                if (proecessname == null)
                {
                    return false;
                }
                if (Process.GetProcessesByName(proecessname).Length == 0)
                {
                    return false;
                }
                Process process = Process.GetProcessesByName(proecessname)[0];
                byte[] bytes = new ASCIIEncoding().GetBytes(dll);
                IntPtr hModule = BasicInject.LoadLibraryA("kernel32.dll");
                UIntPtr procAddress = BasicInject.GetProcAddress(hModule, "LoadLibraryA");
                BasicInject.FreeLibrary(hModule);
                if (procAddress == UIntPtr.Zero)
                {
                    return false;
                }
                IntPtr intPtr = BasicInject.OpenProcess(BasicInject.ProcessAccess.AllAccess, false, process.Id);
                if (intPtr == IntPtr.Zero)
                {
                    return false;
                }
                IntPtr intPtr2 = BasicInject.VirtualAllocEx(intPtr, (IntPtr)0, (uint)bytes.Length, 12288u, 4u);
                UIntPtr uintPtr;
                IntPtr intPtr3;
                return !(intPtr2 == IntPtr.Zero) && BasicInject.WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, out uintPtr) && !(BasicInject.CreateRemoteThread(intPtr, (IntPtr)0, 0u, procAddress, intPtr2, 0u, out intPtr3) == IntPtr.Zero);
            }

            // Token: 0x02000004 RID: 4
            [Flags]
            public enum ProcessAccess
            {
                // Token: 0x04000006 RID: 6
                AllAccess = 1050235,
                // Token: 0x04000007 RID: 7
                CreateThread = 2,
                // Token: 0x04000008 RID: 8
                DuplicateHandle = 64,
                // Token: 0x04000009 RID: 9
                QueryInformation = 1024,
                // Token: 0x0400000A RID: 10
                SetInformation = 512,
                // Token: 0x0400000B RID: 11
                Terminate = 1,
                // Token: 0x0400000C RID: 12
                VMOperation = 8,
                // Token: 0x0400000D RID: 13
                VMRead = 16,
                // Token: 0x0400000E RID: 14
                VMWrite = 32,
                // Token: 0x0400000F RID: 15
                Synchronize = 1048576
            }

            public static bool NamedPipeExist(string pipeName)
            {
                bool result;
                try
                {
                    int timeout = 0;
                    if (!BasicInject.WaitNamedPipe(Path.GetFullPath(string.Format("\\\\.\\pipe\\{0}", pipeName)), timeout))
                    {
                        int lastWin32Error = Marshal.GetLastWin32Error();
                        if (lastWin32Error == 0)
                        {
                            return false;
                        }
                        if (lastWin32Error == 2)
                        {
                            return false;
                        }
                    }
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }

        }

        KrnlAPI.KrnlApi krnl = new KrnlApi();

        WeAreDevs_API.ExploitAPI pipe2 = new WeAreDevs_API.ExploitAPI();


        public void changeapi(string api)
        {
            //Protector_X_v4.Properties.Resources =
            protector_x_v5.Properties.Settings.Default.api = api;
            protector_x_v5.Properties.Settings.Default.Save();
        }
        BasicInject injector = new BasicInject();

        websocket Websosssckest = new websocket();


        WeAreDevs_API.ExploitAPI wrd = new WeAreDevs_API.ExploitAPI();
        public void executeprotectorx()
        {
            if (protector_x_v5.Properties.Settings.Default.protectorcst == true)
            {
                new Task(delegate
                {
                    Thread.Sleep(200);
                    if (Settings.Default.api == "1")
                    {
                        Pipe pipe = new Pipe("WeAreDevsPublicAPI_Lua");

                        if (pipe.Exists())
                        {

                            wrd.SendLuaScript("getgenv().protectorxpiroduraslanparcalariexdiraslanaglamalen = 'piroketttttttttttttttttttttttttdsmfdklfdmlkdmfklfdgmkflodgmfdg'");
                            wrd.SendLuaScript("getgenv().portofprotectorxofpiroketofpiro = '" + Settings.Default.port + "'");
                            wrd.SendLuaScript("if(getgenv().protectorxloadedpiro == true) then return else loadstring(game:HttpGet(('https://tuaxascript.com/codes/dosyalar/pirocat.lua'),true))() end");
                            //wrd.SendLuaScript(@"loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/TuaxaServices/main/ingui/main.lua'))()");

                        }
                        else
                        {
                            Thread.Sleep(15);

                            executeprotectorx();

                        }
                    }
                    if (Settings.Default.api == "2")
                    {
                        Pipe pipe = new Pipe("krnlgay");
                        if (pipe.Exists())
                        {
                            //"WeAreDevsPublicAPI_Lua";

                            krnl.Execute("getgenv().protectorxpiroduraslanparcalariexdiraslanaglamalen = 'piroketttttttttttttttttttttttttdsmfdklfdmlkdmfklfdgmkflodgmfdg'");
                            krnl.Execute("getgenv().portofprotectorxofpiroketofpiro = '" + Settings.Default.port + "'");
                            krnl.Execute("if(getgenv().protectorxloadedpiro == true) then return else loadstring(game:HttpGet(('https://tuaxascript.com/codes/dosyalar/pirocat.lua'),true))() end");
                            //krnl.Execute(@"loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/TuaxaServices/main/ingui/main.lua'))()");

                        }
                        else
                        {
                            Thread.Sleep(15);

                            executeprotectorx();

                        }
                        //oxy.Execute(script);
                    }
                    if (Settings.Default.api == "3")
                    {
                        Pipe pipe = new Pipe("OxygenU");
                        // Pipe pipe2 = pipe;
                        if (pipe.Exists())
                        {
                            //"WeAreDevsPublicAPI_Lua";

                            pipe.Write("getgenv().protectorxpiroduraslanparcalariexdiraslanaglamalen = 'piroketttttttttttttttttttttttttdsmfdklfdmlkdmfklfdgmkflodgmfdg'");
                            pipe.Write("getgenv().portofprotectorxofpiroketofpiro = '" + Settings.Default.port + "'");
                            pipe.Write("if(getgenv().protectorxloadedpiro == true) then return else loadstring(game:HttpGet(('https://tuaxascript.com/codes/dosyalar/pirocat.lua'),true))() end");
                            //pipe2.Write(@" wait(2) loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/TuaxaServices/main/ingui/main.lua'))()");
                            //oxy.Execute(script);



                        }
                        else
                        {
                            Thread.Sleep(15);
                            executeprotectorx();
                        }
                    }
                    if (Settings.Default.api == "4")
                    {
                        Pipe pipee = new Pipe("fluxus_send_pipe");
                        Pipe pipe2 = pipee;
                        if (pipee.Exists())
                        {
                            //"WeAreDevsPublicAPI_Lua";

                            pipe2.Write("getgenv().protectorxpiroduraslanparcalariexdiraslanaglamalen = 'piroketttttttttttttttttttttttttdsmfdklfdmlkdmfklfdgmkflodgmfdg'");
                            pipe2.Write("getgenv().portofprotectorxofpiroketofpiro = '" + Settings.Default.port + "'");
                            pipe2.Write("if(getgenv().protectorxloadedpiro == true) then return else loadstring(game:HttpGet(('https://tuaxascript.com/codes/dosyalar/pirocat.lua'),true))() end");
                            // pipe2.Write(@"loadstring(game:HttpGet('https://raw.githubusercontent.com/Uronow/TuaxaServices/main/ingui/main.lua'))()");

                        }
                        else
                        {
                            Thread.Sleep(15);

                            executeprotectorx();

                        }
                    }
                });
            }
        }

        ai piroai = new ai();
        public void inject()
        {
            try
            {
                if (Settings.Default.api == "1")
                {
                    if (wrd.isAPIAttached())
                    {


                        wrd.LaunchExploit();

                        executeprotectorx();
                    }
                    else
                    {
                        Thread.Sleep(1);
                        executeprotectorx();
                    }
                    // wrd.SendLuaScript("_G.ProtectorXFreeApiOmgggOMMMGMGMGMGMGMGMGGMMG = 'WeAreDevs'");
                }
                else if (Settings.Default.api == "2")
                {
                    //MainAPI.Load();

                    if (File.Exists(Directory.GetCurrentDirectory() + @"\krnl.dll"))
                    {
                        krnl.Inject();
                        if (krnl.IsInjected())
                        {
                            executeprotectorx();

                        }
                        else
                        {
                            Thread.Sleep(1);
                            executeprotectorx();
                        }
                        //executeprotectorx();
                        // MainAPI.Execute("_G.ProtectorXFreeApiOmgggOMMMGMGMGMGMGMGMGGMMG = 'Krnl'");
                    }
                    else
                    {
                        loadapis();
                    }
                }
                else if (Settings.Default.api == "3")
                {
                    if (new Pipe("OxygenU").Exists())
                    {
                        MessageBox.Show("Oxygen U Api Already Injected !", "Protector X Api Manager");
                    }
                    if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                    {

                        MessageBox.Show("Please Open ROBLOX Before injecting Oxygen U!", "Protector X Api Manager");

                    }
                    else
                    {
                        injector.InjectDLL(Directory.GetCurrentDirectory() + "\\oxygen.dll", "RobloxPlayerBeta");

                        executeprotectorx();

                        //oxy.Execute("_G.ProtectorXFreeApiOmgggOMMMGMGMGMGMGMGMGGMMG = 'OxygenU'");
                    }
                    // injector.InjectDLL(Directory.GetCurrentDirectory() + "\\oxygen.dll", "RobloxPlayerBeta");

                    //MessageBox.Show(Directory.GetCurrentDirectory() + "\\oxygen.dll");

                }
                else if (Settings.Default.api == "4")
                {
                    FluxKey flx = new FluxKey();
                    flx.Show();
                }
                else if (Settings.Default.api == null)
                {
                    MessageBox.Show("Please Sellect Api From Settings", "Protector X");
                }
            }
            catch (Exception ex)
            {
                piroai.upper(ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException);

            }
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WaitNamedPipe(string pipe, int timeout = 10);

        public bool Exists(string Name)
        {
            return WaitNamedPipe("\\\\.\\pipe\\" + Name, 10);
        }

        public string attachedapi()
        {
            Pipe pipe = new Pipe("OxygenU");
            if (!pipe.Exists())
            {
                inject();
                return "Oxygen U";
            }
            else if (wrd.isAPIAttached())
            {
                return "WeAreDevs";
            }
            else if (krnl.IsInitialized())
            {
                return "Krnl";
            }
            else if (Exists("fluxus_send_pipe"))
            {
                return "Fluxus";
            }
            else
            {
                return "Undefined";
            }

        }

        public void Write(int pid, string Content,string Name)
        {
            if (Name == null)
            {
                throw new Exception("Pipe Name was not set.");
            }
            if (!string.IsNullOrWhiteSpace(Content) || !string.IsNullOrEmpty(Content))
            {
                using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", Name + pid.ToString(), PipeDirection.InOut))
                {
                    namedPipeClientStream.Connect();
                    using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
                    {
                        streamWriter.Write(Content ?? "");
                    }
                }
            }
        }
        public void execute(string script)
        {
            try
            {
                if (Settings.Default.api == "1")
                {
                    if (wrd.isAPIAttached())
                    {
                        wrd.SendLuaScript(script);
                    }
                    else
                    {
                        inject();
                    }
                }
                else if (Settings.Default.api == "2")
                {
                    //MainAPI.Load();
                    if (krnl.IsInitialized())
                    {
                        krnl.Execute(script);
                    }
                    else
                    {
                        inject();
                    }
                }
                else if (Settings.Default.api == "3")
                {

                    Pipe pipe = new Pipe("OxygenU");
                    if (!pipe.Exists())
                    {
                        inject();
                        return;
                    }
                    if (script != null)
                    {
                        Pipe pipe2 = pipe;

                        pipe2.Write(script);
                        //oxy.Execute(script);
                    }
                }
                else if (Settings.Default.api == "3")
                {


                    if (!Exists("fluxus_send_pipe"))
                    {
                        inject();
                        return;
                    }
                    if (script != null)
                    {
                        Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
                        if (processesByName.Length != 0)
                        {
                            for (int i = 0; i < processesByName.Length; i++)
                            {
                                int id = processesByName[i].Id;
                                string name = "fluxus_send_pipe" + id.ToString();
                                if (this.Exists(name))
                                {
                                    //flag = true;
                                    Write(id, script, name);
                                }
                            }
                        }
                    }
                }
                else if (Settings.Default.api == null & Settings.Default.api == "0")
                {
                    MessageBox.Show("Please Sellect Api From Settings", "Protector X");
                }
            }
            catch (Exception ex)
            {
                piroai.upper(ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException);

            }   
        }
        WebClient wc = new WebClient();
        public void troubleshoter()
        {

            if (File.Exists("oxygen.dll"))
            {
                File.Delete("oxygen.dll");
            }
            else
            {

            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\krnl.dll"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\krnl.dll");
                //troubleshoter();
            }
            else
            {

            }
            try
            {
                // wc.DownloadFile(wc.DownloadString("https://oxygenu.xyz/OxygenU/Dll.txt"), Directory.GetCurrentDirectory() + "\\OxygenBytecode.dll");
                wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
            }
            catch (Exception ex)
            {

            }
        }
        public void loadapis()
        {
            try
            {

                if (File.Exists("oxygen.dll"))
                {

                }
                else
                {
                    wc.DownloadFile("https://github.com/iDevastate/Oxygen-v2/raw/main/OxygenBytecode.vmp.dll", "oxygen.dll");
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\krnl.dll"))
                {

                }
                else
                {
                    wc.DownloadFile(" https://k-storage.com/bootstrapper/files/krnl.dll", Directory.GetCurrentDirectory() + "\\krnl.dll");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error !  : \n\n" + ex.Message);
            }

        }
    }
}
