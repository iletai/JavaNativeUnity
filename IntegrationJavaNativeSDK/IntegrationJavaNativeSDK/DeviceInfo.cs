using IntegrationJavaNativeSDK.CommonUtils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IntegrationJavaNativeSDK
{
    public static class DeviceInfo
    {
       


        #region SOME_FUNCTION_NONE_NATIVE_SUGGEST
        [DllImport("kernel32")]
        extern static UInt64 GetTickCount64();



        public static float GetBatteryLevel()
        {
            return SystemInfo.batteryLevel;
        }
        public static string GetLocalIPAddress()
        {
            //Check internet connection
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return "No internet connection";

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No internet connection";
        }

        public static string GetTimeZone()
        {
            TimeZone curTimeZone = TimeZone.CurrentTimeZone;
            return curTimeZone.StandardName;
        }

        public static string GetDeviceModel()
        {
            return SystemInfo.deviceModel;
        }

        public static string GetOperatingSystem()
        {
            return SystemInfo.operatingSystem;
        }

        public static string GetManufacturer()
        {
            return SystemInfo.deviceModel;
        }

        public static int GetSysMemorySize()
        {
            return SystemInfo.systemMemorySize;
        }

        public static string GetSystemName()
        {
            return SystemInfo.deviceName;
        }

        public static string GetSystemVersion()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer
                || Application.platform == RuntimePlatform.WindowsEditor)
            {
                return Environment.OSVersion.ToString();
            }
            string osInfo = SystemInfo.operatingSystem;
            osInfo = osInfo.Split(' ')[osInfo.Split(' ').Length - 1];

            return osInfo;
        }

        public static string GetRegion()
        {
            return RegionInfo.CurrentRegion.ToString();
        }

        public static string GetOrientation()
        {
            ScreenOrientation orientation = Screen.orientation;
            switch (orientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    return "Portrait orientation";
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    return "Landscape orientation";
            }

            return "Auto-rotates the screen";
        }

        public static int GetIdentifierForVendor()
        {
            return SystemInfo.graphicsDeviceVendorID;
        }

        public static string GetBatteryStatus()
        {
            BatteryStatus status = SystemInfo.batteryStatus;
            switch (status)
            {
                case BatteryStatus.Charging:
                    return "Device is plugged in and charging";
                case BatteryStatus.Discharging:
                    return "Device is unplugged and discharging";
                case BatteryStatus.NotCharging:
                    return "Device is plugged in, but is not charging";
                case BatteryStatus.Full:
                    return "Device is plugged in and the battery is full";
                default:
                    return "The device's battery status cannot be determined";
            }
        }

        public static int GetScreenWidth()
        {
            return Screen.width;
        }

        public static int GetScreenHeight()
        {
            return Screen.height;
        }

        public static int GetProcessorCount()
        {
            return SystemInfo.processorCount;
        }
#endregion
        public static string GetNetworkCarrierName()
        {
            return JniCommonUtils.StaticCall<string>("getNetworkCarrierName", "Can't get network carier name", "com.library.javanativeunity.DeviceUtils", new object[] { JniCommonUtils.AndroidApplication });

        }

        public static string GetISOCountryCode()
        {
            {
                using (AndroidJavaClass cls = new AndroidJavaClass("java.util.Locale"))
                {
                    if (cls != null)
                    {
                        using (AndroidJavaObject locale = cls.CallStatic<AndroidJavaObject>("getDefault"))
                        {
                            if (locale != null)
                            {
                                string localeVal = locale.Call<string>("getLanguage") + "_" + locale.Call<string>("getCountry");
                                return localeVal;
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }

            return "";
        }

        public static string GetMobieCountryCode()
        {
            return "";
        }

        public static string GetMobieNetworkCode()
        {
            {
                AndroidJavaObject jo = JniCommonUtils.Activity.Call<AndroidJavaObject>("getSystemService", "phone");
                return jo.Call<string>("getSimOperator");
            }

        }

        public static string GetSupportedInterfaceOrientations()
        {
            return GetOrientation();
        }

        public static string GetOperatingSystemVersionString()
        {

            return SystemInfo.operatingSystem;
        }

        public static bool IsPowerSaveMode()
        {
            return JniCommonUtils.StaticCall<bool>("isPowerSaveModeEnabled", false, "com.library.javanativeunity.DeviceUtils");


        }

        public static long GetMemorySizeInBytes()
        {
            long a = 0;
            return JniCommonUtils.StaticCall<long>("getMemorySizeInBytes", a, "com.library.javanativeunity.DeviceUtils", new object[] { JniCommonUtils.AndroidApplication });

        }

        public static int GetVolumeDevice()
        {
            {
                return JniCommonUtils.StaticCall<int>("getVolumeDevice", 0, "com.library.javanativeunity.DeviceUtils", new object[] { JniCommonUtils.AndroidApplication });
            }

        }

        public static double GetUsedMemory()
        {
            return JniCommonUtils.StaticCall<double>("getUsedMemory", 0, "com.library.javanativeunity.DeviceUtils", new object[] { JniCommonUtils.AndroidApplication });

        }

        public static float GetScreenScale()
        {
            {
                var currentDPI = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96);
                return 96 / (float)currentDPI;
            }

        }

        public static TimeSpan GetUpTime()
        {
            long timeUpDeviceAndroid = JniCommonUtils.StaticCall<long>("uptimeMillis", 1, "com.library.javanativeunity.DeviceUtils");
            TimeSpan time = TimeSpan.FromMilliseconds(timeUpDeviceAndroid);
            return time;
        }

        public static float GetCpuTemperature()
        {
            return JniCommonUtils.StaticCall<float>("getCpuTemperature", 0, "com.library.javanativeunity.DeviceUtils");
        }

        public static string GetLocalizeModelDevice()
        {
            return JniCommonUtils.StaticCall<string>("getDeviceName", "Null", "com.library.javanativeunity.DeviceUtils");
        }

        public static int GetBrightness()
        {
            //We using method get from Java native. But in Unity 2019 or newer, I recommend using "Screen.brightness".
            return JniCommonUtils.StaticCall<int>("getScreenBrightness", 0, "com.library.javanativeunity.DeviceUtils", new object[] { JniCommonUtils.AndroidApplication });
        }

    }

}
