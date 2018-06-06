// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyState
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TrophyState
  {
    public int[] Count = new int[0];
    public string iname;
    public bool IsEnded;
    public int StartYMD;
    public DateTime RewardedAt;
    public bool IsDirty;
    public TrophyParam Param;
    public bool IsSending;

    public bool IsCompleted
    {
      get
      {
        if (this.Param == null || this.Count.Length < this.Param.Objectives.Length)
          return false;
        for (int index = 0; index < this.Param.Objectives.Length && index < this.Count.Length; ++index)
        {
          if (this.Count[index] < this.Param.Objectives[index].RequiredCount)
            return false;
        }
        return true;
      }
    }
  }
}
