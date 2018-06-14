// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.Impl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Internal.Runtime;

namespace Fabric.Runtime.Internal
{
  internal class Impl
  {
    protected const string Name = "Fabric";

    public static Impl Make()
    {
      return (Impl) new AndroidImpl();
    }

    public virtual string Initialize()
    {
      Utils.Log("Fabric", "Method Initialize () is unimplemented on this platform");
      return string.Empty;
    }
  }
}
