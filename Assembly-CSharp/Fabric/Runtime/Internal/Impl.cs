// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.Impl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
