// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectParty
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(151, "SaveTeamID", FlowNode.PinTypes.Input, 151)]
  [FlowNode.Pin(1000, "ApplyToPlayerData", FlowNode.PinTypes.Input, 1000)]
  [FlowNode.NodeType("UI/SelectParty", 32741)]
  [FlowNode.Pin(1, "Select Team", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(150, "LoadTeamID", FlowNode.PinTypes.Input, 150)]
  public class FlowNode_SelectParty : FlowNode
  {
    public FlowNode_SelectParty.PartyTypes PartyType;
    public int PartyIndex;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          GlobalVars.SelectedPartyIndex.Set(this.PartyIndex);
          this.ActivateOutputLinks(100);
          break;
        case 150:
          FlowNode_SelectParty.LoadTeamID(this.PartyType);
          this.ActivateOutputLinks(100);
          break;
        case 151:
          this.SaveTeamID();
          this.ActivateOutputLinks(100);
          break;
        case 1000:
          MonoSingleton<GameManager>.Instance.Player.SetPartyCurrentIndex((int) GlobalVars.SelectedPartyIndex);
          this.ActivateOutputLinks(100);
          break;
      }
    }

    public static void LoadTeamID(FlowNode_SelectParty.PartyTypes type)
    {
      switch (type)
      {
        case FlowNode_SelectParty.PartyTypes.Normal:
          int num1 = !PlayerPrefs.HasKey(PlayerData.TEAM_ID_KEY) ? 0 : PlayerPrefs.GetInt(PlayerData.TEAM_ID_KEY);
          int num2 = num1 < 0 || num1 >= 8 ? 0 : num1;
          GlobalVars.SelectedPartyIndex.Set(num2);
          break;
        case FlowNode_SelectParty.PartyTypes.Multi:
          int num3 = !PlayerPrefs.HasKey(PlayerData.MULTI_PLAY_TEAM_ID_KEY) ? 0 : PlayerPrefs.GetInt(PlayerData.MULTI_PLAY_TEAM_ID_KEY);
          int num4 = num3 < 0 || num3 >= 8 ? 0 : num3;
          GlobalVars.SelectedPartyIndex.Set(num4);
          break;
        case FlowNode_SelectParty.PartyTypes.Arena:
          int num5 = !PlayerPrefs.HasKey(PlayerData.ARENA_TEAM_ID_KEY) ? 0 : PlayerPrefs.GetInt(PlayerData.ARENA_TEAM_ID_KEY);
          int num6 = num5 < 0 || num5 >= 8 ? 0 : num5;
          GlobalVars.SelectedPartyIndex.Set(num6);
          break;
        case FlowNode_SelectParty.PartyTypes.ArenaDefense:
          int defensePartyIndex = MonoSingleton<GameManager>.Instance.Player.GetDefensePartyIndex();
          GlobalVars.SelectedPartyIndex.Set(defensePartyIndex);
          break;
      }
    }

    private void SaveTeamID()
    {
      switch (this.PartyType)
      {
        case FlowNode_SelectParty.PartyTypes.Normal:
          PlayerPrefs.SetInt(PlayerData.TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex);
          break;
        case FlowNode_SelectParty.PartyTypes.Multi:
          PlayerPrefs.SetInt(PlayerData.MULTI_PLAY_TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex);
          break;
        case FlowNode_SelectParty.PartyTypes.Arena:
          PlayerPrefs.SetInt(PlayerData.ARENA_TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex);
          break;
        case FlowNode_SelectParty.PartyTypes.ArenaDefense:
          MonoSingleton<GameManager>.Instance.Player.SetDefenseParty((int) GlobalVars.SelectedPartyIndex);
          break;
      }
    }

    public enum PartyTypes
    {
      Normal,
      Multi,
      Arena,
      ArenaDefense,
    }
  }
}
