// Decompiled with JetBrains decompiler
// Type: Fabric.Crashlytics.Internal.AndroidImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Fabric.Crashlytics.Internal
{
  internal class AndroidImpl : Impl
  {
    private readonly List<IntPtr> references = new List<IntPtr>();
    private AndroidJavaObject native;
    private AndroidJavaClass crashWrapper;
    private AndroidJavaObject instance;

    private AndroidJavaObject Native
    {
      get
      {
        if (this.native == null)
          this.native = new AndroidJavaObject("com.crashlytics.android.Crashlytics", new object[0]);
        return this.native;
      }
    }

    private AndroidJavaClass CrashWrapper
    {
      get
      {
        if (this.crashWrapper == null)
          this.crashWrapper = new AndroidJavaClass("io.fabric.unity.crashlytics.android.CrashlyticsAndroidWrapper");
        return this.crashWrapper;
      }
    }

    private AndroidJavaObject Instance
    {
      get
      {
        if (this.instance == null)
          this.instance = (AndroidJavaObject) this.Native.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
        if (this.instance == null)
          throw new AndroidImpl.JavaInteropException("Couldn't get an instance of the Crashlytics class!");
        return this.instance;
      }
    }

    public override void Crash()
    {
      ((AndroidJavaObject) this.CrashWrapper).CallStatic("crash", new object[0]);
    }

    public override void Log(string message)
    {
      this.Instance.CallStatic("log", new object[1]
      {
        (object) message
      });
    }

    public override void SetKeyValue(string key, string value)
    {
      this.Instance.CallStatic("setString", new object[2]
      {
        (object) key,
        (object) value
      });
    }

    public override void SetUserIdentifier(string identifier)
    {
      this.Instance.CallStatic("setUserIdentifier", new object[1]
      {
        (object) identifier
      });
    }

    public override void SetUserEmail(string email)
    {
      this.Instance.CallStatic("setUserEmail", new object[1]
      {
        (object) email
      });
    }

    public override void SetUserName(string name)
    {
      this.Instance.CallStatic("setUserName", new object[1]
      {
        (object) name
      });
    }

    public override void RecordCustomException(string name, string reason, StackTrace stackTrace)
    {
      this.RecordCustomException(name, reason, stackTrace.ToString());
    }

    public override void RecordCustomException(string name, string reason, string stackTraceString)
    {
      this.references.Clear();
      IntPtr num1 = AndroidJNI.FindClass("java/lang/Exception");
      IntPtr methodId1 = AndroidJNI.GetMethodID(num1, "<init>", "(Ljava/lang/String;)V");
      jvalue[] jvalueArray1 = new jvalue[1];
      jvalueArray1[0].l = (__Null) AndroidJNI.NewStringUTF(name + " : " + reason);
      IntPtr num2 = AndroidJNI.NewObject(num1, methodId1, jvalueArray1);
      this.references.Add((IntPtr) jvalueArray1[0].l);
      this.references.Add(num2);
      IntPtr num3 = AndroidJNI.FindClass("java/lang/StackTraceElement");
      IntPtr methodId2 = AndroidJNI.GetMethodID(num3, "<init>", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V");
      Dictionary<string, string>[] stackTraceString1 = Impl.ParseStackTraceString(stackTraceString);
      IntPtr num4 = AndroidJNI.NewObjectArray(stackTraceString1.Length, num3, IntPtr.Zero);
      this.references.Add(num4);
      for (int index = 0; index < stackTraceString1.Length; ++index)
      {
        Dictionary<string, string> dictionary = stackTraceString1[index];
        jvalue[] jvalueArray2 = new jvalue[4];
        jvalueArray2[0].l = (__Null) AndroidJNI.NewStringUTF(dictionary["class"]);
        jvalueArray2[1].l = (__Null) AndroidJNI.NewStringUTF(dictionary["method"]);
        jvalueArray2[2].l = (__Null) AndroidJNI.NewStringUTF(dictionary["file"]);
        this.references.Add((IntPtr) jvalueArray2[0].l);
        this.references.Add((IntPtr) jvalueArray2[1].l);
        this.references.Add((IntPtr) jvalueArray2[2].l);
        jvalueArray2[3].i = (__Null) int.Parse(dictionary["line"]);
        IntPtr num5 = AndroidJNI.NewObject(num3, methodId2, jvalueArray2);
        this.references.Add(num5);
        AndroidJNI.SetObjectArrayElement(num4, index, num5);
      }
      IntPtr methodId3 = AndroidJNI.GetMethodID(num1, "setStackTrace", "([Ljava/lang/StackTraceElement;)V");
      jvalue[] jvalueArray3 = new jvalue[1];
      jvalueArray3[0].l = (__Null) num4;
      AndroidJNI.CallVoidMethod(num2, methodId3, jvalueArray3);
      IntPtr num6 = AndroidJNI.FindClass("com/crashlytics/android/Crashlytics");
      IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(num6, "logException", "(Ljava/lang/Throwable;)V");
      jvalue[] jvalueArray4 = new jvalue[1];
      jvalueArray4[0].l = (__Null) num2;
      AndroidJNI.CallStaticVoidMethod(num6, staticMethodId, jvalueArray4);
      using (List<IntPtr>.Enumerator enumerator = this.references.GetEnumerator())
      {
        while (enumerator.MoveNext())
          AndroidJNI.DeleteLocalRef(enumerator.Current);
      }
    }

    public class JavaInteropException : Exception
    {
      public JavaInteropException(string message)
        : base(message)
      {
      }
    }
  }
}
