// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusUnitDownload
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/VersusUnitDownload", 32741)]
  [FlowNode.Pin(101, "Error", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_VersusUnitDownload : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_VersusUnitDownload.\u003COnActivate\u003Ec__AnonStorey219 activateCAnonStorey219 = new FlowNode_VersusUnitDownload.\u003COnActivate\u003Ec__AnonStorey219();
      if (!GameUtility.Config_UseAssetBundles.Value)
      {
        this.ActivateOutputLinks(100);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey219.pt = PunMonoSingleton<MyPhoton>.Instance;
        JSON_MyPhotonPlayerParam photonPlayerParam = (JSON_MyPhotonPlayerParam) null;
        // ISSUE: reference to a compiler-generated field
        if (Object.op_Inequality((Object) activateCAnonStorey219.pt, (Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          List<MyPhoton.MyPlayer> roomPlayerList = activateCAnonStorey219.pt.GetRoomPlayerList();
          if (roomPlayerList != null && roomPlayerList.Count > 1)
          {
            // ISSUE: reference to a compiler-generated method
            MyPhoton.MyPlayer myPlayer = roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(activateCAnonStorey219.\u003C\u003Em__216));
            if (myPlayer != null)
              photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
          }
        }
        if (photonPlayerParam != null)
          AssetManager.PrepareAssets(AssetPath.UnitSkinImage(photonPlayerParam.units[0].unit.UnitParam, photonPlayerParam.units[0].unit.GetSelectedSkin(-1), photonPlayerParam.units[0].unit.CurrentJobId));
        this.StartCoroutine(this.AsyncDownload());
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncDownload()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_VersusUnitDownload.\u003CAsyncDownload\u003Ec__Iterator95() { \u003C\u003Ef__this = this };
    }
  }
}
