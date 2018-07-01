// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRoomIsDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Multi/MultiPlayRoomIsDraft", 32741)]
  public class FlowNode_MultiPlayRoomIsDraft : FlowNode
  {
    private const int PIN_INPUT_CHECK = 0;
    private const int PIN_OUTPUT_YES = 1;
    private const int PIN_OUTPUT_NO = 2;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Equality((Object) instance2, (Object) null))
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        MyPhoton.MyRoom myRoom = !instance1.AudienceMode ? instance2.GetCurrentRoom() : instance1.AudienceRoom;
        if (myRoom == null)
        {
          this.ActivateOutputLinks(2);
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = myRoom != null ? JSON_MyPhotonRoomParam.Parse(myRoom.json) : (JSON_MyPhotonRoomParam) null;
          if (myPhotonRoomParam == null || myPhotonRoomParam.draft_type == 0)
            this.ActivateOutputLinks(2);
          else
            this.ActivateOutputLinks(1);
        }
      }
    }
  }
}
