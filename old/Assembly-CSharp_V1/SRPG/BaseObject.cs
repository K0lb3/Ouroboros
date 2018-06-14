// Decompiled with JetBrains decompiler
// Type: SRPG.BaseObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
