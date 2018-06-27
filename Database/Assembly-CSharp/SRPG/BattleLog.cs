// Decompiled with JetBrains decompiler
// Type: SRPG.BattleLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public abstract class BattleLog
  {
    public virtual void Serialize(StringBuilder dst)
    {
    }

    public virtual void Deserialize(string log)
    {
    }
  }
}
