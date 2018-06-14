// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UsageRateRanking : MonoBehaviour, IFlowInterface
  {
    public static readonly string[] ViewInfo = new string[3]{ "quest", "arena", "tower_match" };
    public GameObject ItemBase;
    public GameObject Parent;
    public GameObject Aggregating;
    private List<UsageRateRankingItem> Items;
    public Scrollbar ItemScrollBar;
    private UsageRateRanking.ViewInfoType mNowViewInfoType;
    public Toggle[] RankingToggle;

    public UsageRateRanking()
    {
      base.\u002Ector();
    }

    public byte NowViewInfoIndex
    {
      get
      {
        return (byte) this.mNowViewInfoType;
      }
    }

    public string NowViewInfo
    {
      get
      {
        return UsageRateRanking.ViewInfo[(int) this.NowViewInfoIndex];
      }
    }

    public void Start()
    {
      if (this.RankingToggle == null)
        return;
      for (int index = 0; index < this.RankingToggle.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UsageRateRanking.\u003CStart\u003Ec__AnonStorey3A4 startCAnonStorey3A4 = new UsageRateRanking.\u003CStart\u003Ec__AnonStorey3A4();
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey3A4.\u003C\u003Ef__this = this;
        if (!Object.op_Equality((Object) this.RankingToggle[index], (Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey3A4.index = index;
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.RankingToggle[index].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey3A4, __methodptr(\u003C\u003Em__470)));
        }
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      Dictionary<string, RankingData> unitRanking = MonoSingleton<GameManager>.Instance.UnitRanking;
      if (!unitRanking.ContainsKey(this.NowViewInfo))
      {
        this.Aggregating.SetActive(true);
      }
      else
      {
        RankingData rankingData = unitRanking[this.NowViewInfo];
        this.Aggregating.SetActive(rankingData.isReady != 1);
        if (rankingData.isReady != 1)
          return;
        for (int index = 0; index < rankingData.ranking.Length; ++index)
        {
          if (this.Items.Count <= index)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemBase);
            gameObject.get_transform().SetParent(this.Parent.get_transform(), false);
            UsageRateRankingItem component = (UsageRateRankingItem) gameObject.GetComponent<UsageRateRankingItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              ((Component) component).get_gameObject().SetActive(true);
              this.Items.Add(component);
            }
          }
          this.Items[index].Refresh(index + 1, rankingData.ranking[index]);
        }
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        if (rankingData.ranking.Length >= this.Items.Count)
          return;
        for (int length = rankingData.ranking.Length; length < this.Items.Count; ++length)
          ((Component) this.Items[length]).get_gameObject().SetActive(false);
      }
    }

    private void OnChangedToggle(int index)
    {
      this.OnChangedToggle((UsageRateRanking.ViewInfoType) index);
    }

    public void OnChangedToggle(UsageRateRanking.ViewInfoType index)
    {
      this.mNowViewInfoType = index;
      if (Object.op_Inequality((Object) this.ItemScrollBar, (Object) null))
        this.ItemScrollBar.set_value(1f);
      for (int index1 = 0; index1 < this.RankingToggle.Length; ++index1)
        this.RankingToggle[index1].set_isOn((int) this.NowViewInfoIndex == index1);
      this.Refresh();
    }

    public enum ViewInfoType : byte
    {
      Quest,
      Arena,
      TowerMatch,
      Num,
    }
  }
}
