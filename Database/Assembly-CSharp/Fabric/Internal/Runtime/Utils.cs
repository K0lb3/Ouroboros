// Decompiled with JetBrains decompiler
// Type: Fabric.Internal.Runtime.Utils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Internal.Runtime
{
  public static class Utils
  {
    public static void Log(string kit, string message)
    {
      ((AndroidJavaObject) new AndroidJavaClass("android.util.Log")).CallStatic<int>("d", new object[2]
      {
        (object) kit,
        (object) message
      });
    }
  }
}
