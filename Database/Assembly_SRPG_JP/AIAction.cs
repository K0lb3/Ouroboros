// Decompiled with JetBrains decompiler
// Type: SRPG.AIAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class AIAction
  {
    public OString skill;
    public OInt type;
    public OInt turn;
    public OBool notBlock;
    public eAIActionNoExecAct noExecAct;
    public int nextActIdx;
    public eAIActionNextTurnAct nextTurnAct;
    public int turnActIdx;
    public SkillLockCondition cond;
  }
}
