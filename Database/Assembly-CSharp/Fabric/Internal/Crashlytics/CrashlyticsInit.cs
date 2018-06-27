// Decompiled with JetBrains decompiler
// Type: Fabric.Internal.Crashlytics.CrashlyticsInit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Internal.Runtime;
using System;
using UnityEngine;

namespace Fabric.Internal.Crashlytics
{
  public class CrashlyticsInit : MonoBehaviour
  {
    private static readonly string kitName = "Crashlytics";
    private static CrashlyticsInit instance;

    public CrashlyticsInit()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Equality((Object) CrashlyticsInit.instance, (Object) null))
      {
        this.AwakeOnce();
        CrashlyticsInit.instance = this;
        Object.DontDestroyOnLoad((Object) this);
      }
      else
      {
        if (!Object.op_Inequality((Object) CrashlyticsInit.instance, (Object) this))
          return;
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
    }

    private void AwakeOnce()
    {
      CrashlyticsInit.RegisterExceptionHandlers();
    }

    private static void RegisterExceptionHandlers()
    {
      if (CrashlyticsInit.IsSDKInitialized())
      {
        Utils.Log(CrashlyticsInit.kitName, "Registering exception handlers");
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashlyticsInit.HandleException);
        // ISSUE: method pointer
        Application.add_logMessageReceived(new Application.LogCallback((object) null, __methodptr(HandleLog)));
      }
      else
        Utils.Log(CrashlyticsInit.kitName, "Did not register exception handlers: Crashlytics SDK was not initialized");
    }

    private static bool IsSDKInitialized()
    {
      AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.crashlytics.android.Crashlytics");
      AndroidJavaObject androidJavaObject;
      try
      {
        androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).CallStatic<AndroidJavaObject>("getInstance", new object[0]);
      }
      catch
      {
        androidJavaObject = (AndroidJavaObject) null;
      }
      return androidJavaObject != null;
    }

    private static void HandleException(object sender, UnhandledExceptionEventArgs eArgs)
    {
      Exception exceptionObject = (Exception) eArgs.ExceptionObject;
      CrashlyticsInit.HandleLog(exceptionObject.Message.ToString(), exceptionObject.StackTrace.ToString(), (LogType) 4);
    }

    private static void HandleLog(string message, string stackTraceString, LogType type)
    {
      if (type != 4)
        return;
      Utils.Log(CrashlyticsInit.kitName, "Recording exception: " + message);
      Utils.Log(CrashlyticsInit.kitName, "Exception stack trace: " + stackTraceString);
      string[] messageParts = CrashlyticsInit.getMessageParts(message);
      Fabric.Crashlytics.Crashlytics.RecordCustomException(messageParts[0], messageParts[1], stackTraceString);
    }

    private static string[] getMessageParts(string message)
    {
      char[] separator = new char[1]{ ':' };
      string[] strArray = message.Split(separator, 2, StringSplitOptions.None);
      foreach (string str in strArray)
        str.Trim();
      if (strArray.Length == 2)
        return strArray;
      return new string[2]{ "Exception", message };
    }
  }
}
