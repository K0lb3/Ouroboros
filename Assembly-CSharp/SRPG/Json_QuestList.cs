// Decompiled with JetBrains decompiler
// Type: SRPG.Json_QuestList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_QuestList
  {
    public JSON_SectionParam[] worlds;
    public JSON_ChapterParam[] areas;
    public JSON_QuestParam[] quests;
    public JSON_ObjectiveParam[] objectives;
    public JSON_MagnificationParam[] magnifications;
    public JSON_QuestCondParam[] conditions;
    public JSON_QuestCampaignParentParam[] CampaignParents;
    public JSON_QuestCampaignChildParam[] CampaignChildren;
    public JSON_TowerFloorParam[] towerFloors;
    public JSON_TowerRewardParam[] towerRewards;
    public JSON_TowerRoundRewardParam[] towerRoundRewards;
    public JSON_TowerParam[] towers;
    public JSON_VersusTowerParam[] versusTowerFloor;
  }
}
