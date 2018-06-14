// Decompiled with JetBrains decompiler
// Type: Gsc.Core.RootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.Core
{
  public class RootObject : MonoBehaviour
  {
    private static RootObject _instance;

    public RootObject()
    {
      base.\u002Ector();
    }

    public static RootObject Instance
    {
      get
      {
        if (Object.op_Equality((Object) RootObject._instance, (Object) null))
        {
          GameObject gameObject = new GameObject("GSCC.RootObject");
          ((Object) gameObject).set_hideFlags((HideFlags) 61);
          Object.DontDestroyOnLoad((Object) gameObject);
          RootObject._instance = (RootObject) gameObject.AddComponent<RootObject>();
        }
        return RootObject._instance;
      }
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) RootObject._instance, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this).get_gameObject());
      Debug.LogError((object) "Already instantiated.");
    }

    public static void Initialize()
    {
      if (!Object.op_Inequality((Object) RootObject._instance, (Object) null))
        return;
      Object.Destroy((Object) ((Component) RootObject._instance).get_gameObject());
      RootObject._instance = (RootObject) null;
    }

    public void DelayInvoke(Action f, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke(f, seconds));
    }

    public void DelayInvoke<T1>(Action<T1> f, T1 arg1, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1>(f, arg1, seconds));
    }

    public void DelayInvoke<T1, T2>(Action<T1, T2> f, T1 arg1, T2 arg2, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1, T2>(f, arg1, arg2, seconds));
    }

    public void DelayInvoke<T1, T2, T3>(Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3, float seconds)
    {
      this.StartCoroutine(RootObject._DelayInvoke<T1, T2, T3>(f, arg1, arg2, arg3, seconds));
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke(Action f, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__IteratorF() { seconds = seconds, f = f, \u003C\u0024\u003Eseconds = seconds, \u003C\u0024\u003Ef = f };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1>(Action<T1> f, T1 arg1, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator10<T1>() { seconds = seconds, f = f, arg1 = arg1, \u003C\u0024\u003Eseconds = seconds, \u003C\u0024\u003Ef = f, \u003C\u0024\u003Earg1 = arg1 };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2>(Action<T1, T2> f, T1 arg1, T2 arg2, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator11<T1, T2>() { seconds = seconds, f = f, arg1 = arg1, arg2 = arg2, \u003C\u0024\u003Eseconds = seconds, \u003C\u0024\u003Ef = f, \u003C\u0024\u003Earg1 = arg1, \u003C\u0024\u003Earg2 = arg2 };
    }

    [DebuggerHidden]
    private static IEnumerator _DelayInvoke<T1, T2, T3>(Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3, float seconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RootObject.\u003C_DelayInvoke\u003Ec__Iterator12<T1, T2, T3>() { seconds = seconds, f = f, arg1 = arg1, arg2 = arg2, arg3 = arg3, \u003C\u0024\u003Eseconds = seconds, \u003C\u0024\u003Ef = f, \u003C\u0024\u003Earg1 = arg1, \u003C\u0024\u003Earg2 = arg2, \u003C\u0024\u003Earg3 = arg3 };
    }
  }
}
