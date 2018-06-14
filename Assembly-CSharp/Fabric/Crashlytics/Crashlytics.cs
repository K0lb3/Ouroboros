// Decompiled with JetBrains decompiler
// Type: Fabric.Crashlytics.Crashlytics
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Crashlytics.Internal;
using System.Diagnostics;

namespace Fabric.Crashlytics
{
  public class Crashlytics
  {
    private static readonly Impl impl = Impl.Make();

    public static void SetDebugMode(bool debugMode)
    {
      Fabric.Crashlytics.Crashlytics.impl.SetDebugMode(debugMode);
    }

    public static void Crash()
    {
      Fabric.Crashlytics.Crashlytics.impl.Crash();
    }

    public static void ThrowNonFatal()
    {
      Fabric.Crashlytics.Crashlytics.impl.ThrowNonFatal();
    }

    public static void Log(string message)
    {
      Fabric.Crashlytics.Crashlytics.impl.Log(message);
    }

    public static void SetKeyValue(string key, string value)
    {
      Fabric.Crashlytics.Crashlytics.impl.SetKeyValue(key, value);
    }

    public static void SetUserIdentifier(string identifier)
    {
      Fabric.Crashlytics.Crashlytics.impl.SetUserIdentifier(identifier);
    }

    public static void SetUserEmail(string email)
    {
      Fabric.Crashlytics.Crashlytics.impl.SetUserEmail(email);
    }

    public static void SetUserName(string name)
    {
      Fabric.Crashlytics.Crashlytics.impl.SetUserName(name);
    }

    public static void RecordCustomException(string name, string reason, StackTrace stackTrace)
    {
      Fabric.Crashlytics.Crashlytics.impl.RecordCustomException(name, reason, stackTrace);
    }

    public static void RecordCustomException(string name, string reason, string stackTraceString)
    {
      Fabric.Crashlytics.Crashlytics.impl.RecordCustomException(name, reason, stackTraceString);
    }
  }
}
