// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BindMultiUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Multi/BindMultiUnit", 32741)]
  [FlowNode.Pin(2, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BindMultiUnit : FlowNode
  {
    [FlowNode.DropTarget(typeof (GameObject[]), false)]
    public GameObject[] Targets;
    [FlowNode.ShowInInfo]
    public FlowNode_BindMultiUnit.TargetType Type;
    [FlowNode.DropTarget(typeof (GameObject[]), false)]
    public GameObject Image;

    public override void OnActivate(int pinID)
    {
      MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) pt, (UnityEngine.Object) null))
      {
        string roomParam = pt.GetRoomParam("started");
        if (roomParam != null)
        {
          JSON_MyPhotonPlayerParam[] players = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam).players;
          if (players.Length > 0)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = this.Type != FlowNode_BindMultiUnit.TargetType.Player ? Array.Find<JSON_MyPhotonPlayerParam>(players, (Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID != pt.GetMyPlayer().playerID)) : Array.Find<JSON_MyPhotonPlayerParam>(players, (Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID == pt.GetMyPlayer().playerID));
            if (photonPlayerParam != null)
            {
              PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(PlayerPartyTypes.RankMatch);
              for (int index = 0; index < this.Targets.Length && index < photonPlayerParam.units.Length && (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.RANKMATCH || partyOfType == null || index < partyOfType.VSWAITMEMBER_START); ++index)
              {
                photonPlayerParam.units[index].unit = new UnitData();
                photonPlayerParam.units[index].unit.Deserialize(photonPlayerParam.units[index].unitJson);
                DataSource.Bind<UnitData>(this.Targets[index], photonPlayerParam.units[index].unit);
                GameParameter.UpdateAll(this.Targets[index]);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Image, (UnityEngine.Object) null) && index == 0)
                {
                  DataSource.Bind<UnitData>(this.Image, photonPlayerParam.units[index].unit);
                  GameParameter.UpdateAll(this.Image);
                }
              }
            }
          }
        }
      }
      this.ActivateOutputLinks(2);
    }

    public enum TargetType
    {
      Player,
      Enemy,
    }
  }
}
