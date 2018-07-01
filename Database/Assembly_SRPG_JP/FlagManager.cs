// Decompiled with JetBrains decompiler
// Type: SRPG.FlagManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public struct FlagManager
  {
    private int box;

    private bool Check(int id)
    {
      if (id < 32)
        return true;
      DebugUtility.LogError("BoolManager: over is max id [" + (object) id + "]");
      return false;
    }

    public void Set(int id, bool flag)
    {
      if (flag)
        this.True(id);
      else
        this.False(id);
    }

    private void True(int id)
    {
      if (!this.Check(id))
        return;
      this.box |= 1 << id;
    }

    private void False(int id)
    {
      if (!this.Check(id))
        return;
      this.box &= ~(1 << id);
    }

    public bool Is(int id)
    {
      return (this.box & 1 << id) != 0;
    }
  }
}
