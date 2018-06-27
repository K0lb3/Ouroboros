// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1000, "ApplyToPlayerData", FlowNode.PinTypes.Input, 1000)]
  [FlowNode.Pin(150, "LoadTeamID", FlowNode.PinTypes.Input, 150)]
  [FlowNode.Pin(151, "SaveTeamID", FlowNode.PinTypes.Input, 151)]
  [FlowNode.NodeType("UI/SelectParty", 32741)]
  [FlowNode.Pin(1, "Select Team", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 1001)]
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
          int num1 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.TEAM_ID_KEY, 0);
          int num2 = num1 < 0 || num1 >= 9 ? 0 : num1;
          GlobalVars.SelectedPartyIndex.Set(num2);
          break;
        case FlowNode_SelectParty.PartyTypes.Multi:
          int num3 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.MULTI_PLAY_TEAM_ID_KEY, 0);
          int num4 = num3 < 0 || num3 >= 9 ? 0 : num3;
          GlobalVars.SelectedPartyIndex.Set(num4);
          break;
        case FlowNode_SelectParty.PartyTypes.Arena:
          int num5 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.ARENA_TEAM_ID_KEY, 0);
          int num6 = num5 < 0 || num5 >= 9 ? 0 : num5;
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
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex, false);
          break;
        case FlowNode_SelectParty.PartyTypes.Multi:
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTI_PLAY_TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex, false);
          break;
        case FlowNode_SelectParty.PartyTypes.Arena:
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ARENA_TEAM_ID_KEY, (int) GlobalVars.SelectedPartyIndex, false);
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
