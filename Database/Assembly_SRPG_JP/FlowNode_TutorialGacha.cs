// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
    private const int PIN_IN_TUTORIAL_GACHA_START = 0;
    private const int PIN_OU_TUTORIAL_GACHA_FINISHED = 1;
    public int UnitIndex;
    [StringIsResourcePath(typeof (GachaController))]
    public string Prefab_GachaController;
    [StringIsResourcePath(typeof (TutorialGacha))]
    [SerializeField]
    private string Prefab_TutorialGacha;
    private GachaController mGachaController;
    private TutorialGacha m_TutorialGacha;

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
      ((Behaviour) this).set_enabled(true);
      this.StartCoroutine(this.PlayGachaAsync());
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialGacha.\u003CPlayGachaAsync\u003Ec__IteratorCD()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void Finished()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
