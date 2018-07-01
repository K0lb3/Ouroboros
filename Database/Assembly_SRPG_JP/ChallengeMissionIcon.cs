// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "進捗表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Input, 2)]
  public class ChallengeMissionIcon : MonoBehaviour, IFlowInterface
  {
    public GameObject Badge;

    public ChallengeMissionIcon()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          this.ShowImages(true);
          break;
        case 2:
          this.ShowImages(false);
          break;
      }
    }

    public void ShowImages(bool value)
    {
      Image component1 = (Image) ((Component) this).GetComponent<Image>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        ((Behaviour) component1).set_enabled(value);
      Button component2 = (Button) ((Component) this).GetComponent<Button>();
      if (Object.op_Inequality((Object) component2, (Object) null))
        ((Behaviour) component2).set_enabled(value);
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      Image component3 = (Image) this.Badge.GetComponent<Image>();
      if (!Object.op_Inequality((Object) component3, (Object) null))
        return;
      ((Behaviour) component3).set_enabled(value);
    }

    private void Start()
    {
    }

    [DebuggerHidden]
    private IEnumerator SetAsLastSibling()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMissionIcon.\u003CSetAsLastSibling\u003Ec__IteratorEB()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void Refresh()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null))
        return;
      bool flag1 = false;
      bool flag2 = true;
      foreach (TrophyParam trophyParam in ChallengeMission.GetRootTrophiesSortedByPriority())
      {
        if (!ChallengeMission.GetTrophyCounter(trophyParam).IsEnded)
        {
          if (this.IsNotReceiveRewards(trophyParam))
            flag1 = true;
          flag2 = false;
          break;
        }
      }
      this.Badge.SetActive(flag1);
      ((Component) this).get_gameObject().SetActive(!flag2);
      if (flag2)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private bool IsNotReceiveRewards(TrophyParam rootTrophy)
    {
      foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(rootTrophy))
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
        if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
          return true;
      }
      return false;
    }
  }
}
