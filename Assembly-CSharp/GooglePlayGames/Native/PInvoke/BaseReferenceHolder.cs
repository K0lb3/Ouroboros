// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.BaseReferenceHolder
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal abstract class BaseReferenceHolder : IDisposable
  {
    private static Dictionary<HandleRef, BaseReferenceHolder> _refs = new Dictionary<HandleRef, BaseReferenceHolder>();
    private HandleRef mSelfPointer;

    public BaseReferenceHolder(IntPtr pointer)
    {
      this.mSelfPointer = PInvokeUtilities.CheckNonNull(new HandleRef((object) this, pointer));
    }

    protected bool IsDisposed()
    {
      return PInvokeUtilities.IsNull(this.mSelfPointer);
    }

    protected HandleRef SelfPtr()
    {
      if (this.IsDisposed())
        throw new InvalidOperationException("Attempted to use object after it was cleaned up");
      return this.mSelfPointer;
    }

    protected abstract void CallDispose(HandleRef selfPointer);

    ~BaseReferenceHolder()
    {
      this.Dispose(true);
    }

    public void Dispose()
    {
      this.Dispose(false);
      GC.SuppressFinalize((object) this);
    }

    internal IntPtr AsPointer()
    {
      return this.SelfPtr().Handle;
    }

    private void Dispose(bool fromFinalizer)
    {
      if (!fromFinalizer && BaseReferenceHolder._refs.ContainsKey(this.mSelfPointer) || PInvokeUtilities.IsNull(this.mSelfPointer))
        return;
      this.CallDispose(this.mSelfPointer);
      this.mSelfPointer = new HandleRef((object) this, IntPtr.Zero);
    }

    internal void ReferToMe()
    {
      BaseReferenceHolder._refs[this.SelfPtr()] = this;
    }

    internal void ForgetMe()
    {
      if (!BaseReferenceHolder._refs.ContainsKey(this.SelfPtr()))
        return;
      BaseReferenceHolder._refs.Remove(this.SelfPtr());
      this.Dispose(false);
    }
  }
}
