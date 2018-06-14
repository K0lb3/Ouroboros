// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.JavaUtils
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Reflection;
using UnityEngine;

namespace GooglePlayGames.Native
{
  internal static class JavaUtils
  {
    private static ConstructorInfo IntPtrConstructor = typeof (AndroidJavaObject).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[1]{ typeof (IntPtr) }, (ParameterModifier[]) null);

    internal static AndroidJavaObject JavaObjectFromPointer(IntPtr jobject)
    {
      if (jobject == IntPtr.Zero)
        return (AndroidJavaObject) null;
      return (AndroidJavaObject) JavaUtils.IntPtrConstructor.Invoke(new object[1]{ (object) jobject });
    }

    internal static AndroidJavaObject NullSafeCall(this AndroidJavaObject target, string methodName, params object[] args)
    {
      try
      {
        return (AndroidJavaObject) target.Call<AndroidJavaObject>(methodName, args);
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("null"))
          return (AndroidJavaObject) null;
        Logger.w("CallObjectMethod exception: " + (object) ex);
        return (AndroidJavaObject) null;
      }
    }
  }
}
