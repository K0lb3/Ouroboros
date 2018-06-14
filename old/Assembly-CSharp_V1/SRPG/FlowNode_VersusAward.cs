// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusAward
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/Award", 32741)]
  [FlowNode.Pin(200, "Out", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_VersusAward : FlowNode
  {
    public GameObject BindObj;
    public bool MyPlayer;

    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        FlowNode_VersusAward.\u003COnActivate\u003Ec__AnonStorey218 activateCAnonStorey218 = new FlowNode_VersusAward.\u003COnActivate\u003Ec__AnonStorey218();
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey218.pt = PunMonoSingleton<MyPhoton>.Instance;
        JSON_MyPhotonPlayerParam data = (JSON_MyPhotonPlayerParam) null;
        // ISSUE: reference to a compiler-generated field
        List<MyPhoton.MyPlayer> roomPlayerList = activateCAnonStorey218.pt.GetRoomPlayerList();
        if (roomPlayerList != null)
        {
          if (this.MyPlayer)
          {
            data = JSON_MyPhotonPlayerParam.Create(0, 0);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            MyPhoton.MyPlayer myPlayer = roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(activateCAnonStorey218.\u003C\u003Em__215));
            if (myPlayer != null)
              data = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
          }
        }
        if (data != null)
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.BindObj, data);
      }
      this.ActivateOutputLinks(200);
    }
  }
}
