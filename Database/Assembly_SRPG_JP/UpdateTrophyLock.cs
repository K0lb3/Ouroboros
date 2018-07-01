// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UpdateTrophyLock
  {
    private int lock_count = 1;

    public bool IsLock
    {
      get
      {
        return 0 < this.lock_count;
      }
    }

    public void LockClear()
    {
      this.lock_count = 0;
    }

    public void Lock()
    {
      ++this.lock_count;
    }

    public void Unlock()
    {
      if (0 >= this.lock_count)
        return;
      --this.lock_count;
    }
  }
}
