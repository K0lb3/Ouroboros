// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionIcon
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "進捗表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class ChallengeMissionIcon : MonoBehaviour, IFlowInterface
  {
    public GameObject Badge;
    public Text BadgeCount;

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
          if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
            break;
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
      this.StartCoroutine(this.SetAsLastSibling());
    }

    [DebuggerHidden]
    private IEnumerator SetAsLastSibling()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMissionIcon.\u003CSetAsLastSibling\u003Ec__IteratorA2() { \u003C\u003Ef__this = this };
    }

    private void Refresh()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null))
        return;
      bool flag1 = false;
      bool flag2 = true;
      foreach (TrophyParam rootTropy in ChallengeMission.GetRootTropies())
      {
        if (!ChallengeMission.GetTrophyCounter(rootTropy).IsEnded)
        {
          if (this.IsNotReceiveRewards(rootTropy))
          {
            flag1 = true;
            this.UpdateTrophyCount(rootTropy);
          }
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
      foreach (TrophyParam currentTrophy in ChallengeMission.GetCurrentTrophies(rootTrophy))
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(currentTrophy);
        if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
          return true;
      }
      return false;
    }

    private void UpdateTrophyCount(TrophyParam rootTrophy)
    {
      TrophyParam[] currentTrophies = ChallengeMission.GetCurrentTrophies(rootTrophy);
      int num = 0;
      foreach (TrophyParam trophy in currentTrophies)
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(trophy);
        if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
          ++num;
      }
      if (!Object.op_Inequality((Object) this.BadgeCount, (Object) null))
        return;
      this.BadgeCount.set_text(num.ToString());
    }
  }
}
