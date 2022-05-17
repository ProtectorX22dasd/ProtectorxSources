using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace protector_x_v5
{
    internal class websocket
    {


        private static string Workspace = "";
        public static bool isSocketRunning = false;
        private static HttpListener Listener;
        private static string WebData = string.Empty;
        static api apimodule = new api();


        private static async void HandleRequest(string Path, HttpListenerRequest Request, HttpListenerResponse Response)
        {
            if (IsSite(Path, "/wrapper/hwid")) // GETHWID
            {
                WebData = WebsocketFunctions.GetMachineIdentifier();
            }
            else if (IsSite(Path, "/wrapper/fingerprint")) // GETFINGERPRINT
            {
                WebData = WebsocketFunctions.GetFingerprint();
            }
            else if (IsSite(Path, "/wrapper/crypt/md5")) // CRYPT.MD5
            {
                string Data = GetParameter(Request, "data");
                if (Data != null)
                {
                    WebData = WebsocketFunctions.CreateMD5(Data);
                }
                else
                {
                    WebData = "Missing parameter 'data'";
                }
            }
            else if (IsSite(Path, "/wrapper/crypt/sha256")) // CRYPT.SHA256
            {
                string Data = GetParameter(Request, "data");
                if (Data != null)
                {
                    WebData = WebsocketFunctions.CreateSHA256(Data);
                }
                else
                {
                    WebData = "Missing parameter 'data'";
                }
            }
            else if (IsSite(Path, "/wrapper/crypt/base64_encode"))
            {
                string Data = GetParameter(Request, "data");
                if (Data != null)
                {
                    WebData = WebsocketFunctions.CreateBase64(Data);
                }
                else
                {
                    WebData = "Missing parameter 'data'";
                }
            }
            else if (IsSite(Path, "/wrapper/crypt/base64_decode"))
            {
                string Data = GetParameter(Request, "data");
                if (Data != null)
                {
                    WebData = WebsocketFunctions.Base64Decode(Data);
                }
                else
                {
                    WebData = "Missing parameter 'data'";
                }
            }
            else if (IsSite(Path, "/wrapper/getapi"))
            {
                WebData = apimodule.attachedapi().ToString();
            }
            else if (IsSite(Path, "/wrapper/messagebox"))
            {
                string Title = GetParameter(Request, "title");
                string Message = GetParameter(Request, "message");

                var NewForm = new Form { TopMost = true };
                MessageBox.Show(NewForm, Title, Message);
                NewForm.Dispose();
                WebData = "Done";
            }
            else if (IsSite(Path, "/wrapper/pirtyapanserdar31jekentold3123"))
            {
                string Title = GetParameter(Request, "data1");
                string Message = GetParameter(Request, "data2");

                if (Title == "piroket=pirotuaxa=pirogergeee=ebeninami=nublar")
                {
                    if (Message == "pirolarolmazmfokfodghfhdsfhgudfshygusdfhgusdfugfsduhgdsfhjgfsdhjgsdfuhfsdhu")
                    {
                        WebData = "god";
                    }
                    else
                    {
                        WebData = "nono";
                    }
                }
                else
                {
                    WebData = "nono";
                }
            }
            else
            {
                WebData = "<h1>404 File Not Found.</h1><p>The specified file was not found!</p>";
            }

            #region Handle Response
            byte[] WebDataByte = Encoding.UTF8.GetBytes(WebData);
            Response.ContentType = "text/html";
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentLength64 = WebDataByte.LongLength;
            await Response.OutputStream.WriteAsync(WebDataByte, 0, WebDataByte.Length);
            Response.Close();
            #endregion
        }

        #region Websocket Code
        private static bool IsSite(string PathVisit, string Path)
        {
            if (PathVisit == Path) return true;
            else if (PathVisit == Path + "/") return true;
            else return false;
        }

        private static string GetParameter(HttpListenerRequest Request, string Name, bool Decode = true)
        {
            var ParameterValue = HttpUtility.ParseQueryString(Request.Url.Query).Get(Name);
            if (Decode)
            {
                return HttpUtility.UrlDecode(ParameterValue);
            }
            else
            {
                return ParameterValue;
            }
        }

        private static async Task HandleIncomingConnections()
        {
            while (isSocketRunning)
            {
                // Console.WriteLine("Socket Running: " + isSocketRunning);
                HttpListenerContext Context = await Listener.GetContextAsync();
                HttpListenerResponse Response = Context.Response;
                HttpListenerRequest Request = Context.Request;

                HandleRequest(Request.Url.AbsolutePath, Request, Response);
            }
        }



        public static async void StartSocket(int Port, string WorkspaceFolder)
        {
            Workspace = AppDomain.CurrentDomain.BaseDirectory + WorkspaceFolder + "\\";
            isSocketRunning = true;
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://localhost:" + Port.ToString() + "/");
            Listener.Start();

            await HandleIncomingConnections();
            Listener.Close();
        }
        #endregion
    }

}

