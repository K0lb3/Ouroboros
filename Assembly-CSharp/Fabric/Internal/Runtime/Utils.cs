// Decompiled with JetBrains decompiler
// Type: Fabric.Internal.Runtime.Utils
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
