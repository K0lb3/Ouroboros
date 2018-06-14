// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
