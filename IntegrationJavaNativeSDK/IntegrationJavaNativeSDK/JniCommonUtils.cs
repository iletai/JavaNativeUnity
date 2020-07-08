using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IntegrationJavaNativeSDK.CommonUtils
{
       
    public class JniCommonUtils
    {
        /* ------------------------------------------------------------------------------------------------------------------------------- 
            Sample using: 
            int result = CaptiveReality.Jni.Util.StaticCall<int>("getAMethodWhichReturnsInt", 1, "com.yourandroidlib.example.ClassName");
            string result = CaptiveReality.Jni.Util.StaticCall<string>("getAMethodWhichReturnsString", "UNKNOWN", "com.yourandroidlib.example.ClassName");
        -------------------------------------------------------------------------------------------------------------------------------    */

        private static AndroidJavaObject activity;

        /// <summary>
        /// Template for call Native in Unity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="androidJavaClass"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T StaticCall<T>(string methodName, T defaultValue, string androidJavaClass, object[] args = null)
        {
            T result;

            // Only works on Android!
            if (Application.platform != RuntimePlatform.Android)
            {
                return defaultValue;
            }

            try
            {
                using (AndroidJavaClass androidClass = new AndroidJavaClass(androidJavaClass))
                {
                    if (null != androidClass)
                    {
                        if (args != null)
                        {
                            result = androidClass.CallStatic<T>(methodName, args);
                        }
                        else
                        {
                            result = androidClass.CallStatic<T>(methodName);
                        }
                    }
                    else
                    {
                        result = defaultValue;
                    }

                }
            }
            catch (System.Exception ex)
            {
                 return defaultValue;
            }

            return result;

        
        }

        public static AndroidJavaObject AndroidApplication
        {
            get
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject application = activity.Call<AndroidJavaObject>("getApplication");
                return application;
            }
        }

        private static AndroidJavaObject GetActivity()
        {
            using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return actClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }



        public static AndroidJavaObject Activity
        {
            get
            {
                if (activity == null)
                {
                    activity = GetActivity();
                }

                return activity;
            }
        }

    }
}
