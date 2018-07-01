// Decompiled with JetBrains decompiler
// Type: SRPG.LogAutoHeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class LogAutoHeal : BattleLog
  {
    public Unit self;
    public LogAutoHeal.HealType type;
    public int value;
    public int beforeHp;
    public int beforeMp;

    public enum HealType
    {
      Hp,
      Jewel,
    }
  }
}
