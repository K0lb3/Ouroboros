// Decompiled with JetBrains decompiler
// Type: Gsc.Core.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using UnityEngine;

namespace Gsc.Core
{
  public class Logger
  {
    private static bool initialized;

    public static event Application.LogCallback Callback;

    public static void Init()
    {
      if (Logger.initialized)
        return;
      // ISSUE: method pointer
      Application.remove_logMessageReceived(new Application.LogCallback((object) null, __methodptr(_HandleLog)));
      // ISSUE: method pointer
      Application.add_logMessageReceived(new Application.LogCallback((object) null, __methodptr(_HandleLog)));
      Logger.initialized = true;
    }

    public static void HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      switch ((int) logType)
      {
        case 0:
        case 1:
        case 4:
          UnityErrorLogSender.Instance.Send(logMessage, stackTrace, logType);
          break;
      }
    }

    private static void _HandleLog(string logMessage, string stackTrace, LogType logType)
    {
      Logger.HandleLog(logMessage, stackTrace, logType);
      if (Logger.Callback == null)
        return;
      Logger.Callback.Invoke(logMessage, stackTrace, logType);
    }
  }
}
