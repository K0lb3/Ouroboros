// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusUnitDownload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/VersusUnitDownload", 32741)]
  [FlowNode.Pin(1, "StartAudience", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Error", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_VersusUnitDownload : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (!GameUtility.Config_UseAssetBundles.Value)
      {
        this.ActivateOutputLinks(100);
      }
      else
      {
        switch (pinID)
        {
          case 0:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            FlowNode_VersusUnitDownload.\u003COnActivate\u003Ec__AnonStorey2D7 activateCAnonStorey2D7 = new FlowNode_VersusUnitDownload.\u003COnActivate\u003Ec__AnonStorey2D7();
            // ISSUE: reference to a compiler-generated field
            activateCAnonStorey2D7.pt = PunMonoSingleton<MyPhoton>.Instance;
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) activateCAnonStorey2D7.pt, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              List<MyPhoton.MyPlayer> roomPlayerList = activateCAnonStorey2D7.pt.GetRoomPlayerList();
              if (roomPlayerList != null && roomPlayerList.Count > 1)
              {
                // ISSUE: reference to a compiler-generated method
                MyPhoton.MyPlayer myPlayer = roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(activateCAnonStorey2D7.\u003C\u003Em__2CC));
                if (myPlayer != null)
                {
                  this.AddAssets(JSON_MyPhotonPlayerParam.Parse(myPlayer.json));
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 1:
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (instance.AudienceRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(instance.AudienceRoom.json);
              if (myPhotonRoomParam != null)
              {
                for (int index = 0; index < myPhotonRoomParam.players.Length; ++index)
                {
                  if (myPhotonRoomParam.players[index] != null)
                  {
                    myPhotonRoomParam.players[index].SetupUnits();
                    this.AddAssets(myPhotonRoomParam.players[index]);
                  }
                }
                break;
              }
              break;
            }
            break;
        }
        this.StartCoroutine(this.AsyncDownload());
      }
    }

    private void AddAssets(JSON_MyPhotonPlayerParam param)
    {
      if (param == null)
        return;
      AssetManager.PrepareAssets(AssetPath.UnitSkinImage(param.units[0].unit.UnitParam, param.units[0].unit.GetSelectedSkin(-1), param.units[0].unit.CurrentJobId));
    }

    [DebuggerHidden]
    private IEnumerator AsyncDownload()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_VersusUnitDownload.\u003CAsyncDownload\u003Ec__IteratorD7() { \u003C\u003Ef__this = this };
    }
  }
}
