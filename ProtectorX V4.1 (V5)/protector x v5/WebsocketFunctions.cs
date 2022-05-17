using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace protector_x_v5
{
    class WebsocketFunctions
    {

        public static string Base64Decode(string str)
        {
            var sifreliMetinBaytlari = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(sifreliMetinBaytlari);
        }

        public static string GetMachineIdentifier()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }
        public static string CreateMD5(string input)
        {
            using (MD5 _MD5 = MD5.Create())
            {
                byte[] HashBytes = _MD5.ComputeHash(Encoding.ASCII.GetBytes(input));

                StringBuilder SBuilder = new StringBuilder();
                for (int i = 0; i < HashBytes.Length; i++)
                {
                    SBuilder.Append(HashBytes[i].ToString("X2"));
                }

                return SBuilder.ToString();
            }
        }
        public static string GetFingerprint()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string ProcessorID = "";
            foreach (ManagementObject mo in mbsList)
            {
                ProcessorID = mo["ProcessorID"].ToString();
            }

            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string HardDriveID = dsk["VolumeSerialNumber"].ToString();

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            string MotherboardID = "";
            foreach (ManagementObject mo in moc)
            {
                MotherboardID = (string)mo["SerialNumber"];
            }

            string OS_HWID = GetMachineIdentifier();

            String FirstNetworkMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();

            ProcessorID = CreateMD5(ProcessorID);
            HardDriveID = CreateMD5(HardDriveID);
            MotherboardID = CreateMD5(MotherboardID);
            OS_HWID = CreateMD5(OS_HWID);
            FirstNetworkMacAddress = CreateMD5(FirstNetworkMacAddress);

            var Fingerprint = ProcessorID.Substring(0, 6) + "-" + HardDriveID.Substring(0, 6) + "-" + MotherboardID.Substring(0, 6) + "-" + OS_HWID.Substring(0, 6) + "-" + FirstNetworkMacAddress.Substring(0, 6);
            return Fingerprint.ToLower();
        }
        public static string CreateSHA256(string input)
        {
            using (SHA256 _SHA256 = SHA256.Create())
            {
                byte[] StringBytes = _SHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder SBuilder = new StringBuilder();
                for (int i = 0; i < StringBytes.Length; i++)
                {
                    SBuilder.Append(StringBytes[i].ToString("x2"));
                }
                return SBuilder.ToString();
            }
        }
        public static string CreateBase64(string input)
        {
            byte[] StringBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(StringBytes);
        }



    }
}
