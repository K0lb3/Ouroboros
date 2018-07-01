// Decompiled with JetBrains decompiler
// Type: SRPG.SkillLockCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class SkillLockCondition
  {
    public List<int> x = new List<int>();
    public List<int> y = new List<int>();
    public int type;
    public int value;
    [NonSerialized]
    public bool unlock;

    public void Clear()
    {
      this.value = 0;
      this.x.Clear();
      this.y.Clear();
    }

    public void CopyTo(SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = this.x;
      dsc.y = this.y;
      dsc.unlock = this.unlock;
    }

    public void CopyTo(JSON_SkillLockCondition dsc)
    {
      dsc.type = this.type;
      dsc.value = this.value;
      dsc.x = this.x.ToArray();
      dsc.y = this.y.ToArray();
    }
  }
}
