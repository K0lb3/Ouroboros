// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestBonusObjective
  {
    public string item;
    public int itemNum;
    public RewardType itemType;
    public EMissionType Type;
    public string TypeParam;

    public bool IsMissionTypeExecSkill()
    {
      return this.Type == EMissionType.UseTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetItem;
    }
  }
}
