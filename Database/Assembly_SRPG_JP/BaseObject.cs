// Decompiled with JetBrains decompiler
// Type: SRPG.BaseObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public abstract class BaseObject
  {
    private bool mInitialized;
    private bool mPaused;

    public bool IsInitialized
    {
      set
      {
        this.mInitialized = value;
      }
      get
      {
        return this.mInitialized;
      }
    }

    public bool IsPaused
    {
      set
      {
        this.mPaused = value;
      }
      get
      {
        return this.mPaused;
      }
    }

    public virtual bool Load()
    {
      return true;
    }

    public virtual void Release()
    {
    }

    public virtual void Update()
    {
    }
  }
}
