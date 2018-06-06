// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPartyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(7, "ユニット配置へ", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(8, "同一グリッド", FlowNode.PinTypes.Output, 8)]
  public class VersusPartyWindow : PartyWindow2
  {
    protected override int AvailableMainMemberSlots
    {
      get
      {
        return this.mCurrentParty.PartyData.MAX_UNIT;
      }
    }

    protected override void OnItemSlotsChange()
    {
    }

    protected override void SetItemSlot(int slotIndex, ItemData item)
    {
      this.mCurrentItems[slotIndex] = item;
    }

    private void Update()
    {
    }

    protected override void PostForwardPressed()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyData = this.mCurrentParty.PartyData;
      List<int> intList = new List<int>();
      for (int index = 0; index < partyData.MAX_UNIT; ++index)
      {
        if ((this.mCurrentParty.Units[index] == null ? 0L : this.mCurrentParty.Units[index].UniqueID) != 0L)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          VersusPartyWindow.\u003CPostForwardPressed\u003Ec__AnonStorey28D pressedCAnonStorey28D = new VersusPartyWindow.\u003CPostForwardPressed\u003Ec__AnonStorey28D();
          // ISSUE: reference to a compiler-generated field
          pressedCAnonStorey28D.idx = player.GetVersusPlacement(PlayerData.VERSUS_ID_KEY + (object) index);
          // ISSUE: reference to a compiler-generated method
          if (intList.FindIndex(new Predicate<int>(pressedCAnonStorey28D.\u003C\u003Em__328)) != -1)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.MULTI_VERSUS_SAME_POS"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
            return;
          }
          // ISSUE: reference to a compiler-generated field
          intList.Add(pressedCAnonStorey28D.idx);
        }
      }
      base.PostForwardPressed();
    }

    public void OnClickEdit()
    {
      this.SaveAndActivatePin(7);
    }
  }
}
