// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.AndroidImpl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Runtime.Internal
{
  internal class AndroidImpl : Impl
  {
    private static readonly AndroidJavaClass FabricInitializer = new AndroidJavaClass("io.fabric.unity.android.FabricInitializer");

    public override string Initialize()
    {
      return (string) ((AndroidJavaObject) AndroidImpl.FabricInitializer).CallStatic<string>("JNI_InitializeFabric", new object[0]);
    }
  }
}
