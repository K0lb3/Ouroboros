// Decompiled with JetBrains decompiler
// Type: SRPG.LogAutoHeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
