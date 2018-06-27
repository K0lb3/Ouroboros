// Decompiled with JetBrains decompiler
// Type: SRPG.LogDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class LogDead : BattleLog
  {
    public List<LogDead.Param> list_normal = new List<LogDead.Param>();
    public List<LogDead.Param> list_sentence = new List<LogDead.Param>();

    public void Add(Unit unit, DeadTypes type)
    {
      LogDead.Param obj = new LogDead.Param();
      obj.self = unit;
      obj.type = type;
      if (type == DeadTypes.DeathSentence)
        this.list_sentence.Add(obj);
      else
        this.list_normal.Add(obj);
    }

    public struct Param
    {
      public Unit self;
      public DeadTypes type;
    }
  }
}
