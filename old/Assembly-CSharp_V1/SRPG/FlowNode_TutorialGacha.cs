// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialGacha
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("UI/Tutorial Gacha")]
  public class FlowNode_TutorialGacha : FlowNode
  {
    public int UnitIndex;
    [StringIsResourcePath(typeof (GachaController))]
    public string Prefab_GachaController;
    private GachaController mGachaController;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGachaController, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mGachaController).get_gameObject());
      this.mGachaController = (GachaController) null;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.UnitIndex < 0 || player.Units.Count <= this.UnitIndex)
        return;
      if (!GlobalVars.IsTutorialEnd)
        AnalyticsManager.TrackTutorialAnalyticsEvent("0_6b_2d.017", AnalyticsManager.TutorialTrackingEventType.EVENT_DIALOG_2D);
      ((Behaviour) this).set_enabled(true);
      this.StartCoroutine(this.PlayGachaAsync(player.Units[this.UnitIndex].UnitParam));
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync(UnitParam unit)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialGacha.\u003CPlayGachaAsync\u003Ec__Iterator93() { unit = unit, \u003C\u0024\u003Eunit = unit, \u003C\u003Ef__this = this };
    }
  }
}
