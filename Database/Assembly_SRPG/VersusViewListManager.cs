// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewListManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Search", FlowNode.PinTypes.Input, 2)]
  public class VersusViewListManager : MonoBehaviour, IFlowInterface
  {
    public ScrollListController Scroll;
    public ScrollClamped_VersusViewList ViewList;

    public VersusViewListManager()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) this.ViewList, (Object) null))
            break;
          this.ViewList.OnSetUpItems();
          this.Scroll.Refresh();
          break;
        case 2:
          this.Search();
          break;
      }
    }

    private void Search()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      int selectedMultiPlayRoomId = GlobalVars.SelectedMultiPlayRoomID;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      instance1.AudienceRoom = instance2.SearchRoom(selectedMultiPlayRoomId);
      if (instance1.AudienceRoom != null)
      {
        if (instance1.AudienceRoom.start)
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "AlreadyStartFriendMode");
        else
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FindRoom");
      }
      else
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "NotFindRoom");
    }
  }
}
