using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace protector_x_v5
{
    internal class startup_loc
    {
        public bool startuploccheck()
        {
            try
            {
                string pathToExe = Directory.GetCurrentDirectory() + System.AppDomain.CurrentDomain.FriendlyName;
                string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "TuaxaSoftwares");


                if (!Directory.Exists(appStartMenuPath))
                {
                    Directory.CreateDirectory(appStartMenuPath);
                }

                string shortcutLocation = Path.Combine(appStartMenuPath, "Protector X" + ".lnk");
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                shortcut.Description = "Protector X Roblox Lua/Luac Executor";
                //shortcut.IconLocation = @"C:\Program Files (x86)\TestApp\TestApp.ico"; //uncomment to set the icon of the shortcut
                shortcut.TargetPath = pathToExe;
                shortcut.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
