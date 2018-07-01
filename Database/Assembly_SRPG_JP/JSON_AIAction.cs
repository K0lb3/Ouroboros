// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_AIAction
  {
    public string skill;
    public int type;
    public int turn;
    public int notBlock;
    public int noExecAct;
    public int nextActIdx;
    public int nextTurnAct;
    public int turnActIdx;
    public JSON_SkillLockCondition cond;
  }
}
