// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class RaidResult
  {
    public List<RaidQuestResult> results = new List<RaidQuestResult>(10);
    public QuestParam[] chquest = new QuestParam[0];
    public QuestParam quest;
    public int pexp;
    public int uexp;
    public int gold;
    public List<UnitData> members;
    public string[] campaignIds;

    public RaidResult(PlayerPartyTypes type)
    {
      this.members = new List<UnitData>(MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type).MAX_UNIT);
    }
  }
}
