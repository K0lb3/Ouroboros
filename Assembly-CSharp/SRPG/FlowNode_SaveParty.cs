// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SaveParty
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(50, "SaveWithID", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(51, "ResetWithID", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(201, "OutResetWithID", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(200, "OutSaveWithID", FlowNode.PinTypes.Output, 200)]
  [FlowNode.NodeType("UI/SaveParty", 32741)]
  [FlowNode.Pin(1, "Save", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_SaveParty : FlowNode
  {
    private ItemData[] mSavedInventory = new ItemData[5];
    private long[,] mSavedUnits;
    private int mSavedPartyID;

    private void Save()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
      for (int index1 = 0; index1 < 8; ++index1)
      {
        for (int index2 = 0; index2 < partyCurrent.MAX_UNIT; ++index2)
          this.mSavedUnits[index1, index2] = player.Partys[index1].GetUnitUniqueID(index2);
      }
    }

    private void Reset()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
      for (int index1 = 0; index1 < 8; ++index1)
      {
        for (int index2 = 0; index2 < partyCurrent.MAX_UNIT; ++index2)
          player.Partys[index1].SetUnitUniqueID(index2, this.mSavedUnits[index1, index2]);
      }
    }

    private void SaveInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < 5; ++index)
        this.mSavedInventory[index] = player.Inventory[index];
    }

    private void ResetInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < 5; ++index)
        player.SetInventory(index, this.mSavedInventory[index]);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Save();
          this.ActivateOutputLinks(100);
          break;
        case 2:
          this.Reset();
          this.ActivateOutputLinks(100);
          FlowNodeEvent<FlowNode_OnPartyChange>.Invoke();
          break;
        case 50:
          this.Save();
          this.mSavedPartyID = (int) GlobalVars.SelectedPartyIndex;
          this.SaveInventory();
          this.ActivateOutputLinks(200);
          break;
        case 51:
          this.Reset();
          GlobalVars.SelectedPartyIndex.Set(this.mSavedPartyID);
          this.ResetInventory();
          this.ActivateOutputLinks(201);
          break;
      }
    }
  }
}
