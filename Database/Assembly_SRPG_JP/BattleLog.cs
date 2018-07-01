// Decompiled with JetBrains decompiler
// Type: SRPG.BattleLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
