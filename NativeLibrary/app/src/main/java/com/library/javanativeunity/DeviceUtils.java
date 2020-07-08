package com.library.javanativeunity;

import android.app.ActivityManager;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.media.AudioManager;
import android.net.Uri;
import android.os.Build;
import android.os.PowerManager;
import android.preference.PreferenceManager;
import android.provider.Settings;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import android.telephony.TelephonyManager;
import android.text.TextUtils;

import androidx.annotation.NonNull;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Locale;

public final class DeviceUtils {
    @NonNull
    public static String getAndroidId(@NonNull Context context) {
        return Settings.Secure.getString(context.getContentResolver(), "android_id");
    }

    @NonNull
    public static String getDeviceType(@NonNull Context context) {
        Resources resources = context.getResources();
        Configuration configuration = resources.getConfiguration();
        int screenLayout = configuration.screenLayout;
        int screenSize = screenLayout & 0xF;

        switch (screenSize) {
            case 1:
            case 2:
                return "phone";
            case 3:
            case 4:
                return "tablet";
        }
        return "unknown";
    }

    @NonNull
    public static String getModelName() {
        return Build.MODEL;
    }

    @NonNull
    public static String getManufacturer() {
        return Build.MANUFACTURER;
    }

    @NonNull
    public static String getSystemName() {
        return "android";
    }


    @NonNull
    public static String getSystemVersion() {
        return Build.VERSION.RELEASE;
    }


    @NonNull
    public static String getApiLevel() {
        return "" + Build.VERSION.SDK_INT;
    }

    @NonNull
    public static String getAppName(@NonNull Context context) {
        ApplicationInfo applicationInfo = context.getApplicationInfo();
        int stringId = applicationInfo.labelRes;
        return (stringId == 0) ? applicationInfo.nonLocalizedLabel.toString() : context.getString(stringId);
    }

    @NonNull
    public static String getAppVersion(@NonNull Context context) {
        try {
            PackageManager pm = context.getPackageManager();
            String name = context.getPackageName();
            PackageInfo info = pm.getPackageInfo(name, 0);
            if (info.versionName != null) {
                return info.versionName + "." + info.versionCode;
            }
            return "unknown";
        } catch (android.content.pm.PackageManager.NameNotFoundException e) {
            return "unknown";
        }
    }


    @NonNull
    public static String getAppLanguage() {
        /* 185 */
        return Locale.getDefault().getDisplayLanguage();
    }


    @NonNull
    public static String getCountry() {
        return Locale.getDefault().getCountry();
    }


    @NonNull
    public static String getNetworkCarrierName(@NonNull Context context) {
        TelephonyManager manager = (TelephonyManager) context.getSystemService("phone");
        String carrierName = manager.getNetworkOperatorName();
        return (carrierName == null || carrierName.isEmpty()) ? "UNKNOWN" : carrierName;
    }


    public static int getScreenBrightness(@NonNull Context context) {
        ContentResolver contentResolver = context.getContentResolver();
        return Settings.System.getInt(contentResolver, "screen_brightness", -1);
    }

    public static boolean isPowerSaveModeEnabled(@NonNull PowerManager powerManager) {
        if (Build.VERSION.SDK_INT < 21) return false;

        try {
            return powerManager.isPowerSaveMode();
        } catch (Exception e) {
            return false;
        }
    }

    @NonNull
    public static  Long getMemorySizeInBytes(@NonNull Context context)
    {
        ActivityManager activityManager = (ActivityManager) context.getSystemService(context.ACTIVITY_SERVICE);

        ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();

        activityManager.getMemoryInfo(memoryInfo);

        long totalMemory = memoryInfo.totalMem;

        return totalMemory;

    }

    public static int getVolumeDevice(@NonNull Context context)
    {
        AudioManager mgr = (AudioManager)context.getSystemService(Context.AUDIO_SERVICE);
        int music_volume_level = mgr.getStreamVolume(AudioManager.STREAM_MUSIC);
        return music_volume_level;
    }


    //Get current app using memory here:
    //https://stackoverflow.com/questions/3170691/how-to-get-current-memory-usage-in-android/19267315#19267315
    //CAUTION: This function measures memory usage/available of the DEVICE. This is NOT what is available to your app.
    public static double getUsedMemory(@NonNull Context context)
    {
        ActivityManager.MemoryInfo mi = new ActivityManager.MemoryInfo();
        ActivityManager activityManager = (ActivityManager) context.getSystemService(context.ACTIVITY_SERVICE);
        activityManager.getMemoryInfo(mi);
        double availableMegs = mi.availMem / 0x100000L;

        //Percentage can be calculated for API 16+
        double percentAvail = mi.availMem / (double)mi.totalMem * 100.0;
        return percentAvail;
    }


    public static long uptimeMillis() {
        return android.os.SystemClock.uptimeMillis();

    }

    public static float getCpuTemperature() {

        Process process;
        try {
            process = Runtime.getRuntime().exec("cat sys/class/thermal/thermal_zone0/temp");
            process.waitFor();
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
            String line = reader.readLine();
            if(line!=null) {
                float temp = Float.parseFloat(line);
                return temp / 1000.0f;
            }else{
                return 51.0f;
            }
        } catch (Exception e) {
            e.printStackTrace();
            return 0.0f;
        }
    }

    /** Returns the consumer friendly device name */
    public static String getDeviceName() {
        String manufacturer = Build.MANUFACTURER;
        String model = Build.MODEL;
        if (model.startsWith(manufacturer)) {
            return capitalize(model);
        }
        return capitalize(manufacturer) + " " + model;
    }

    private static String capitalize(String str) {
        if (TextUtils.isEmpty(str)) {
            return str;
        }
        char[] arr = str.toCharArray();
        boolean capitalizeNext = true;

        StringBuilder phrase = new StringBuilder();
        for (char c : arr) {
            if (capitalizeNext && Character.isLetter(c)) {
                phrase.append(Character.toUpperCase(c));
                capitalizeNext = false;
                continue;
            } else if (Character.isWhitespace(c)) {
                capitalizeNext = true;
            }
            phrase.append(c);
        }

        return phrase.toString();
    }

}
