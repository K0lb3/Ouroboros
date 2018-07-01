// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRankList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class RankingQuestRankList : MonoBehaviour, ScrollListSetUp
  {
    private float Space;
    private int m_Max;
    private RankingQuestUserData[] m_UserDatas;
    private RankingQuestRankWindow m_RankingWindow;

    public RankingQuestRankList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.m_RankingWindow = (RankingQuestRankWindow) ((Component) this).GetComponentInParent<RankingQuestRankWindow>();
    }

    public void SetData(RankingQuestUserData[] data)
    {
      this.m_UserDatas = data;
    }

    public void OnSetUpItems()
    {
      if (this.m_UserDatas == null)
        return;
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate1 = component1.OnItemUpdate;
      RankingQuestRankList rankingQuestRankList1 = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction1 = new UnityAction<int, GameObject>((object) rankingQuestRankList1, __vmethodptr(rankingQuestRankList1, OnUpdateItems));
      onItemUpdate1.RemoveListener(unityAction1);
      ScrollListController.OnItemPositionChange onItemUpdate2 = component1.OnItemUpdate;
      RankingQuestRankList rankingQuestRankList2 = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction2 = new UnityAction<int, GameObject>((object) rankingQuestRankList2, __vmethodptr(rankingQuestRankList2, OnUpdateItems));
      onItemUpdate2.AddListener(unityAction2);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      Vector2 anchoredPosition = component2.get_anchoredPosition();
      this.m_Max = this.m_UserDatas.Length;
      component1.Space = (component1.ItemScale + this.Space) / component1.ItemScale;
      anchoredPosition.y = (__Null) 0.0;
      sizeDelta.y = (__Null) ((double) component1.ItemScale * (double) component1.Space * (double) this.m_Max);
      component2.set_sizeDelta(sizeDelta);
      component2.set_anchoredPosition(anchoredPosition);
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (this.m_UserDatas == null || idx < 0 || idx >= this.m_UserDatas.Length)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        ListItemEvents component1 = (ListItemEvents) obj.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        DataSource.Bind<RankingQuestUserData>(obj, this.m_UserDatas[idx]);
        DataSource.Bind<UnitData>(obj, this.m_UserDatas[idx].m_UnitData);
        RankingQuestInfo component2 = (RankingQuestInfo) obj.GetComponent<RankingQuestInfo>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          return;
        component2.UpdateValue();
      }
    }

    private void OnItemSelect(GameObject go)
    {
      this.m_RankingWindow.OnItemSelect(go);
    }
  }
}
