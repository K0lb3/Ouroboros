// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal abstract class PlatformConfiguration : BaseReferenceHolder
  {
    protected PlatformConfiguration(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal HandleRef AsHandle()
    {
      return this.SelfPtr();
    }
  }
}
