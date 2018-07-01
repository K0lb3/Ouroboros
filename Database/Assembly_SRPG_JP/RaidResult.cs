// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
