# Call Java Native Code (arr) in Unity

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

When you working with Unity, sometimes we need using function from Native Java Code. This repository will help you do it.

  - Sample Unity Itegration Java native code
  - Plugin/Library Java on Android Studio
  - Template Unity Package using anymore

# [Config Android Studio to build Java Library](https://github.com/iletai/JavaNativeUnity/tree/develop/NativeLibrary)

  - Create blank application on Android Studio

  - Config build.gradel(Module:app)
   ```apply plugin: 'com.android.library'```         

  - Config Appmanifest.xml
    ```
        <application
            android:allowBackup="true"
            android:label="@string/app_name"
            android:supportsRtl="true"
        />
    ```        
    
 - Create a new class to call native code:
 ```public final class DeviceUtils```


List function support call native java

> Brightness
> AndroidId
> DeviceType
> ModelName
.........



# Create Library C# to using Native Code

Library C# will compile to DLL file. Only import dll to Unity to using.

Solution have:
* [JniCommonUtils] - Implement for Template of call Android Java API of Unity.
* [DeviceInfo] - Deliver of call Method from Native Java
 

After build dll file. We need copy it to Unity. I recommend using Batscripts to save time
 

### Installation

If you don't understand these step. Could you find release package on release repository.

Install by hold package to Unity.
 

### DeviceInfo Support Function:

DeviceInfo is currently support some sample function on JavaNative. You can add anything you think neccesary for your project. 

| C# Library Code | Java Native Code |Todo |
| ------ | ------ |------ |
| N/A | getDeviceType |Get type mobie or tablet |
| N/A | getAppVersion |Get app version |
| N/A | getAppLanguage |Get app language use |
| GetBrightness | getScreenBrightness |Get brightness screen |
| GetUpTime | uptimeMillis |Get time working device |
| GetVolumeDevice | getVolumeDevice |Get Volume Device |
| GetNetworkCarrierName | getNetworkCarrierName |Get carrier name network |
| GetISOCountryCode | getLanguage + getCountry + getDefaul |Get ISO country device |
| GetMobieNetworkCode | getSimOperator |Get Mobie network code |
| IsPowerSaveMode | isPowerSaveModeEnabled |Check powerbattery mode enable? |
| GetMemorySizeInBytes | getMemorySizeInBytes |Get size of Memory |
| GetUsedMemory | getUsedMemory |Get Ram used of device |
| GetCpuTemperature | getCpuTemperature |Get temprature device |
| Your implement | Your Implement |Your descripts |
| Your implement | Your Implement |Your descripts |
| ... | ... |... |

# Using on Sample Unity 

Want to contribute? Great!

Dillinger uses Gulp + Webpack for fast developing.
Make a change in your file and instantaneously see your updates!

Open your favorite Terminal and run these commands.

First Tab:
```sh
$ node app
```

Second Tab:
```sh
$ gulp watch
```

(optional) Third:
```sh
$ karma test
```
For production release:
```sh
$ gulp build --prod
```
Generating pre-built zip archives for distribution:
```sh
$ gulp build dist --prod
