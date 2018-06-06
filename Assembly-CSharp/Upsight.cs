// Decompiled with JetBrains decompiler
// Type: Upsight
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UpsightMiniJSON;

public class Upsight
{
  private static bool Initialized;
  private static AndroidJavaObject _pluginBase;
  private static AndroidJavaObject _pluginPushExtension;
  private static AndroidJavaObject _pluginMarketingExtension;

  static Upsight()
  {
    Upsight.init();
  }

  public static void init()
  {
    if (Application.get_platform() != 11)
      return;
    try
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.upsight.android.unity.UpsightPlugin"))
      {
        if (((AndroidJavaObject) androidJavaClass).CallStatic<bool>("isEnabled", new object[0]) == null)
        {
          Debug.LogWarning((object) "Upsight.init() was called but the SDK is currently disabled from the Upsight SDK Management Extension. Upsight will not initialize.");
          return;
        }
      }
    }
    catch
    {
      Debug.LogError((object) "Error while checking if the Upsight SDK is enabled. Unable to initialize");
      return;
    }
    if (Upsight.Initialized)
      return;
    Upsight.Initialized = true;
    UpsightManager.init();
    try
    {
      Upsight._pluginBase = new AndroidJavaObject("com.upsight.android.unity.UpsightPlugin", new object[0]);
      if (Upsight._pluginBase != null)
      {
        if (!(AndroidJNI.ExceptionOccurred() != IntPtr.Zero))
          goto label_15;
      }
      Debug.LogError((object) "Upsight initialization failed! JNI Exception thrown:");
      AndroidJNI.ExceptionDescribe();
      Upsight._pluginBase = (AndroidJavaObject) null;
      AndroidJNI.ExceptionClear();
      return;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Upsight initialization failed!");
      Debug.LogException(ex);
      Upsight._pluginBase = (AndroidJavaObject) null;
      return;
    }
label_15:
    try
    {
      Upsight._pluginMarketingExtension = new AndroidJavaObject("com.upsight.android.unity.UpsightMarketingManager", new object[0]);
      if (Upsight._pluginMarketingExtension != null)
        Upsight._pluginBase.Call("registerExtension", new object[1]
        {
          (object) Upsight._pluginMarketingExtension
        });
    }
    catch
    {
      Debug.LogWarning((object) "Upsight Marketing Extension not included.");
      Upsight._pluginMarketingExtension = (AndroidJavaObject) null;
    }
    try
    {
      Upsight._pluginPushExtension = new AndroidJavaObject("com.upsight.android.unity.UpsightPushManager", new object[0]);
      if (Upsight._pluginPushExtension == null)
        return;
      Upsight._pluginBase.Call("registerExtension", new object[1]
      {
        (object) Upsight._pluginPushExtension
      });
    }
    catch
    {
      Debug.LogWarning((object) "Upsight Push Extension not included.");
      Upsight._pluginPushExtension = (AndroidJavaObject) null;
    }
  }

  internal static void terminate()
  {
    if (Upsight._pluginBase != null)
      Upsight._pluginBase.Dispose();
    if (Upsight._pluginMarketingExtension != null)
      Upsight._pluginMarketingExtension.Dispose();
    if (Upsight._pluginPushExtension != null)
      Upsight._pluginPushExtension.Dispose();
    Upsight._pluginBase = (AndroidJavaObject) null;
    Upsight._pluginMarketingExtension = (AndroidJavaObject) null;
    Upsight._pluginPushExtension = (AndroidJavaObject) null;
    Upsight.Initialized = false;
  }

  public static string getAppToken()
  {
    if (Application.get_platform() != 11)
      return "UnityEditor-Token";
    if (Upsight._pluginBase == null || !Upsight.Initialized)
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>(nameof (getAppToken), new object[0]);
  }

  public static string getPublicKey()
  {
    if (Application.get_platform() != 11)
      return "UnityEditor-Key";
    if (Upsight._pluginBase == null || !Upsight.Initialized)
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>(nameof (getPublicKey), new object[0]);
  }

  public static string getSid()
  {
    if (!Upsight.initSuccessful())
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>(nameof (getSid), new object[0]);
  }

  public static void setLoggerLevel(UpsightLoggerLevel loggerLevel)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (setLoggerLevel), new object[1]
    {
      (object) loggerLevel.ToString().ToUpper()
    });
  }

  public static string getPluginVersion()
  {
    if (Application.get_platform() != 11)
      return "UnityEditor";
    if (Upsight._pluginBase == null || !Upsight.Initialized)
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>(nameof (getPluginVersion), new object[0]);
  }

  public static bool getOptOutStatus()
  {
    if (!Upsight.initSuccessful())
      return false;
    return (bool) Upsight._pluginBase.Call<bool>(nameof (getOptOutStatus), new object[0]);
  }

  public static void setOptOutStatus(bool optOutStatus)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (setOptOutStatus), new object[1]
    {
      (object) optOutStatus
    });
  }

  public static void setUserAttributeString(string key, string value)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("setUserAttributesString", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void setUserAttributeFloat(string key, float value)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("setUserAttributesFloat", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void setUserAttributeInt(string key, int value)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("setUserAttributesInt", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void setUserAttributeBool(string key, bool value)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("setUserAttributesBool", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void setUserAttributeDate(string key, DateTime value)
  {
    if (!Upsight.initSuccessful())
      return;
    long unixTimestamp = value.ToUnixTimestamp();
    Upsight._pluginBase.Call("setUserAttributesDatetime", new object[2]
    {
      (object) key,
      (object) unixTimestamp
    });
  }

  public static string getUserAttributeString(string key)
  {
    if (!Upsight.initSuccessful())
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>("getUserAttributesString", new object[1]
    {
      (object) key
    });
  }

  public static float getUserAttributeFloat(string key)
  {
    if (!Upsight.initSuccessful())
      return 0.0f;
    return (float) Upsight._pluginBase.Call<float>("getUserAttributesFloat", new object[1]
    {
      (object) key
    });
  }

  public static int getUserAttributeInt(string key)
  {
    if (!Upsight.initSuccessful())
      return 0;
    return (int) Upsight._pluginBase.Call<int>("getUserAttributesInt", new object[1]
    {
      (object) key
    });
  }

  public static bool getUserAttributeBool(string key)
  {
    if (!Upsight.initSuccessful())
      return false;
    return (bool) Upsight._pluginBase.Call<bool>("getUserAttributesBool", new object[1]
    {
      (object) key
    });
  }

  public static DateTime getUserAttributeDate(string key)
  {
    if (!Upsight.initSuccessful())
      return new DateTime();
    return ((long) Upsight._pluginBase.Call<long>("getUserAttributesDatetime", new object[1]
    {
      (object) key
    })).ToDateTime();
  }

  public static void setLocation(double lat, double lon)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (setLocation), new object[2]
    {
      (object) lat,
      (object) lon
    });
  }

  public static void purgeLocation()
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (purgeLocation), new object[0]);
  }

  public static int getLatestSessionNumber()
  {
    if (!Upsight.initSuccessful())
      return 0;
    return (int) Upsight._pluginBase.Call<int>(nameof (getLatestSessionNumber), new object[0]);
  }

  public static long getLatestSessionStartTimestamp()
  {
    if (!Upsight.initSuccessful())
      return 0;
    return (long) Upsight._pluginBase.Call<long>(nameof (getLatestSessionStartTimestamp), new object[0]);
  }

  public static void recordSessionlessCustomEvent(string eventName, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("recordSessionlessAnalyticsEvent", new object[2]
    {
      (object) eventName,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static void recordCustomEvent(string eventName, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("recordAnalyticsEvent", new object[2]
    {
      (object) eventName,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static void recordMilestoneEvent(string scope, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (recordMilestoneEvent), new object[2]
    {
      (object) scope,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static bool isContentReadyForBillboardWithScope(string scope)
  {
    if (!Upsight.initSuccessful() || Upsight._pluginMarketingExtension == null)
      return false;
    return (bool) Upsight._pluginMarketingExtension.Call<bool>(nameof (isContentReadyForBillboardWithScope), new object[1]
    {
      (object) scope
    });
  }

  public static void prepareBillboard(string scope)
  {
    if (!Upsight.initSuccessful() || Upsight._pluginMarketingExtension == null)
      return;
    Upsight._pluginMarketingExtension.Call(nameof (prepareBillboard), new object[1]
    {
      (object) scope
    });
  }

  public static void destroyBillboard(string scope)
  {
    if (!Upsight.initSuccessful() || Upsight._pluginMarketingExtension == null)
      return;
    Upsight._pluginMarketingExtension.Call(nameof (destroyBillboard), new object[1]
    {
      (object) scope
    });
  }

  public static void recordMonetizationEvent(double totalPrice, string currency, UpsightPurchaseResolution resolution, string product = null, double price = -1f, int quantity = -1, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (recordMonetizationEvent), new object[7]
    {
      (object) totalPrice,
      (object) currency,
      (object) product,
      (object) price,
      (object) resolution.ToString().ToLower(),
      (object) quantity,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static void recordGooglePlayPurchase(int quantity, string currency, double price, double totalPrice, string product, int responseCode, string inAppPurchaseData, string inAppDataSignature, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (recordGooglePlayPurchase), new object[9]
    {
      (object) quantity,
      (object) currency,
      (object) price,
      (object) totalPrice,
      (object) product,
      (object) responseCode,
      (object) inAppPurchaseData,
      (object) inAppDataSignature,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static void recordAppleStorePurchase(int quantity, string currency, double price, string transactionIdentifier, string product, UpsightPurchaseResolution resolution, Dictionary<string, object> properties = null)
  {
  }

  public static void recordAttributionEvent(string campaign, string creative, string source, Dictionary<string, object> properties = null)
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call(nameof (recordAttributionEvent), new object[4]
    {
      (object) campaign,
      (object) creative,
      (object) source,
      properties == null ? (object) (string) null : (object) Json.Serialize((object) properties)
    });
  }

  public static void registerForPushNotifications()
  {
    if (!Upsight.initSuccessful() || Upsight._pluginPushExtension == null)
      return;
    Upsight._pluginPushExtension.Call(nameof (registerForPushNotifications), new object[0]);
  }

  public static void unregisterForPushNotifications()
  {
    if (!Upsight.initSuccessful() || Upsight._pluginPushExtension == null)
      return;
    Upsight._pluginPushExtension.Call(nameof (unregisterForPushNotifications), new object[0]);
  }

  public static void setShouldSynchronizeManagedVariables(bool shouldSynchronize)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.upsight.android.unity.UnitySessionCallbacks"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (setShouldSynchronizeManagedVariables), new object[1]
      {
        (object) shouldSynchronize
      });
  }

  public static string getManagedString(string key)
  {
    if (!Upsight.initSuccessful())
      return (string) null;
    return (string) Upsight._pluginBase.Call<string>(nameof (getManagedString), new object[1]
    {
      (object) key
    });
  }

  public static float getManagedFloat(string key)
  {
    if (!Upsight.initSuccessful())
      return 0.0f;
    return (float) Upsight._pluginBase.Call<float>(nameof (getManagedFloat), new object[1]
    {
      (object) key
    });
  }

  public static int getManagedInt(string key)
  {
    if (!Upsight.initSuccessful())
      return 0;
    return (int) Upsight._pluginBase.Call<int>(nameof (getManagedInt), new object[1]
    {
      (object) key
    });
  }

  public static bool getManagedBool(string key)
  {
    if (!Upsight.initSuccessful())
      return false;
    return (bool) Upsight._pluginBase.Call<bool>(nameof (getManagedBool), new object[1]
    {
      (object) key
    });
  }

  public static void onPause()
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("onApplicationPaused", new object[0]);
  }

  public static void onResume()
  {
    if (!Upsight.initSuccessful())
      return;
    Upsight._pluginBase.Call("onApplicationResumed", new object[0]);
  }

  private static bool initSuccessful()
  {
    if (Application.get_platform() == 11 && Upsight.Initialized)
      return Upsight._pluginBase != null;
    return false;
  }
}
