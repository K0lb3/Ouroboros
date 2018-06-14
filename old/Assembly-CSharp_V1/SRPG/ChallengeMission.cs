// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMission
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(103, "終了", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "詳細", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(99, "メッセージ非表示", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(98, "メッセージ表示", FlowNode.PinTypes.Output, 98)]
  [FlowNode.Pin(101, "報酬受け取り", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "コンプリート報酬受け取り", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(104, "コンプリート報酬受け取りとレビュー表示", FlowNode.PinTypes.Output, 104)]
  public class ChallengeMission : MonoBehaviour, IFlowInterface
  {
    private const string CHALLENGE_NAME_TO_REVIEW = "CHALLENGE_01";
    public const int PIN_REFRESH = 0;
    public const int PIN_MESSAGE_OPENED = 98;
    public const int PIN_MESSAGE_CLOSE = 99;
    public const int PIN_DETAIL = 100;
    public const int PIN_REWARD = 101;
    public const int PIN_COMPLETE = 102;
    public const int PIN_END = 103;
    public const int PIN_COMPLETE_REVIEW = 104;
    public Text Index;
    public RawImage ImageReward;
    public List<ChallengeMissionItem> Items;
    public ChallengeMissionDetail DetailWindow;
    public GameObject MessageWindow;
    public Text MessageText;

    public ChallengeMission()
    {
      base.\u002Ector();
    }

    public static TrophyParam[] GetTropies()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Trophies;
    }

    public static TrophyParam GetTrophy(string key)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrophy(key);
    }

    public static TrophyState GetTrophyCounter(TrophyParam trophy)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Player.GetTrophyCounter(trophy);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 99:
          if (!Object.op_Inequality((Object) this.MessageWindow, (Object) null))
            break;
          this.StartCoroutine(this.CloseMessageWindow());
          break;
      }
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.DetailWindow, (Object) null))
        return;
      ((Component) this.DetailWindow).get_gameObject().SetActive(false);
    }

    private void Refresh()
    {
      TrophyParam currentRoot = ChallengeMission.GetCurrentRoot();
      if (currentRoot == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        TrophyParam[] currentTrophies = ChallengeMission.GetCurrentTrophies(currentRoot);
        if (currentTrophies.Length != this.Items.Count)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
        }
        else
        {
          if (Object.op_Inequality((Object) this.Index, (Object) null))
          {
            int currentRootIndex = ChallengeMission.GetCurrentRootIndex();
            ((Component) this.Index).get_gameObject().SetActive(true);
            this.Index.set_text((currentRootIndex + 1).ToString());
          }
          bool flag1 = false;
          bool flag2 = true;
          for (int index = 0; index < this.Items.Count; ++index)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ChallengeMission.\u003CRefresh\u003Ec__AnonStorey236 refreshCAnonStorey236 = new ChallengeMission.\u003CRefresh\u003Ec__AnonStorey236();
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey236.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey236.trophy = currentTrophies[index];
            // ISSUE: reference to a compiler-generated field
            TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(refreshCAnonStorey236.trophy);
            if (trophyCounter.IsEnded)
            {
              ((Component) this.Items[index]).get_gameObject().SetActive(false);
            }
            else
            {
              if (trophyCounter.IsCompleted)
                flag1 = true;
              // ISSUE: method pointer
              this.Items[index].OnClick = new UnityAction((object) refreshCAnonStorey236, __methodptr(\u003C\u003Em__246));
              // ISSUE: reference to a compiler-generated field
              DataSource.Bind<TrophyParam>(((Component) this.Items[index]).get_gameObject(), refreshCAnonStorey236.trophy);
              this.Items[index].Refresh();
              flag2 = false;
            }
          }
          if (Object.op_Inequality((Object) this.MessageText, (Object) null))
          {
            string str1;
            if (flag1)
            {
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_CLEAR");
            }
            else
            {
              string str2 = string.Empty;
              if (currentRoot.Gold != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) currentRoot.Gold);
              else if (currentRoot.Exp != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) currentRoot.Exp);
              else if (currentRoot.Coin != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) currentRoot.Coin);
              else if (currentRoot.Stamina != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) currentRoot.Stamina);
              else if (currentRoot.Items != null && currentRoot.Items.Length > 0)
              {
                ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(currentRoot.Items[0].iname);
                if (itemParam != null)
                {
                  if (itemParam.type == EItemType.Unit)
                  {
                    UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
                    if (unitParam != null)
                      str2 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
                  }
                  else
                    str2 = LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM", (object) itemParam.name, (object) currentRoot.Items[0].Num);
                }
              }
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_INFO", new object[1]
              {
                (object) str2
              });
            }
            this.MessageText.set_text(str1);
          }
          if (Object.op_Inequality((Object) this.MessageWindow, (Object) null))
          {
            this.MessageWindow.SetActive(Object.op_Inequality((Object) this.MessageText, (Object) null));
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 98);
          }
          TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(currentRoot);
          if (flag2)
          {
            if (!trophyCounter1.IsEnded)
            {
              MonoSingleton<GameManager>.GetInstanceDirect().Player.OnChallengeMissionComplete(currentRoot.iname);
              GlobalVars.SelectedChallengeMissionTrophy = currentRoot.iname;
              GlobalVars.SelectedTrophy.Set(currentRoot.iname);
              if (currentRoot.iname == "CHALLENGE_01")
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
              else
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
            }
            else
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          }
          if (this.IsInvoking("WaitLoadTexture"))
            return;
          this.StartCoroutine(this.WaitLoadTexture(currentRoot));
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator CloseMessageWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMission.\u003CCloseMessageWindow\u003Ec__IteratorA0() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadTexture(TrophyParam trophy)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMission.\u003CWaitLoadTexture\u003Ec__IteratorA1() { trophy = trophy, \u003C\u0024\u003Etrophy = trophy, \u003C\u003Ef__this = this };
    }

    private void OnSelectItem(TrophyParam selectTrophy)
    {
      if (ChallengeMission.GetTrophyCounter(selectTrophy).IsCompleted)
      {
        GlobalVars.SelectedChallengeMissionTrophy = selectTrophy.iname;
        GlobalVars.SelectedTrophy.Set(selectTrophy.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
          return;
        DataSource.Bind<TrophyParam>(((Component) this.DetailWindow).get_gameObject(), selectTrophy);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public static TrophyParam[] GetRootTropies()
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMissionRoot)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    public static TrophyParam GetCurrentRootTrophy()
    {
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies();
      int currentRootIndex = ChallengeMission.GetCurrentRootIndex(rootTropies);
      if (currentRootIndex >= 0)
        return rootTropies[currentRootIndex];
      return (TrophyParam) null;
    }

    public static TrophyParam[] GetCurrentTrophies(TrophyParam root)
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMission && root.iname == tropy.ParentTrophy)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    private static int GetCurrentRootIndex()
    {
      return ChallengeMission.GetCurrentRootIndex(ChallengeMission.GetRootTropies());
    }

    private static int GetCurrentRootIndex(TrophyParam[] trophies)
    {
      if (trophies == null || trophies.Length == 0)
        return -1;
      int num = 0;
      for (int index = 0; index < trophies.Length; ++index)
      {
        if (ChallengeMission.GetTrophyCounter(trophies[index]).IsEnded)
          ++num;
      }
      return num;
    }

    private static TrophyParam GetCurrentRoot()
    {
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies();
      int currentRootIndex = ChallengeMission.GetCurrentRootIndex(rootTropies);
      if (currentRootIndex >= 0 && currentRootIndex < rootTropies.Length)
        return rootTropies[currentRootIndex];
      return (TrophyParam) null;
    }
  }
}
