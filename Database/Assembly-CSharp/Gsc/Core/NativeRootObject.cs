// Decompiled with JetBrains decompiler
// Type: Gsc.Core.NativeRootObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Gsc.Core
{
  public class NativeRootObject : MonoBehaviour
  {
    private static NativeRootObject _instance;

    public NativeRootObject()
    {
      base.\u002Ector();
    }

    public static NativeRootObject Instance
    {
      get
      {
        if (Object.op_Equality((Object) NativeRootObject._instance, (Object) null))
        {
          GameObject gameObject = new GameObject("GSCC.NativeRootObject");
          ((Object) gameObject).set_hideFlags((HideFlags) 61);
          Object.DontDestroyOnLoad((Object) gameObject);
          gameObject.AddComponent<NativeRootObject>();
        }
        return NativeRootObject._instance;
      }
    }

    private void Awake()
    {
      NativeRootObject._instance = this;
    }
  }
}
