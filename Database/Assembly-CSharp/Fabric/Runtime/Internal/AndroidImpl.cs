// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.AndroidImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
