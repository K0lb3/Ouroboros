// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEntryTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class UnitEntryTrigger
  {
    public string unit = string.Empty;
    public string skill = string.Empty;
    public int type;
    public int value;
    public int x;
    public int y;
    [NonSerialized]
    public bool on;

    public void Clear()
    {
      this.unit = string.Empty;
      this.skill = string.Empty;
      this.value = 0;
      this.x = 0;
      this.y = 0;
    }
  }
}
