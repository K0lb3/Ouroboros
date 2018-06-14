// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusReward
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
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusReward : MonoBehaviour, ScrollListSetUp
  {
    public float Space;
    public bool Arrival;
    private int m_Max;
    private List<VersusTowerParam> m_Param;

    public ScrollClamped_VersusReward()
    {
      base.\u002Ector();
    }

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam != null)
      {
        for (int index = versusTowerParam.Length - 1; index >= 0; --index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
          {
            if (this.Arrival)
            {
              if (string.IsNullOrEmpty((string) versusTowerParam[index].ArrivalIteminame))
                continue;
            }
            else if (versusTowerParam[index].SeasonIteminame == null || versusTowerParam[index].SeasonIteminame.Length == 0)
              continue;
            this.m_Param.Add(versusTowerParam[index]);
          }
        }
        this.m_Max = this.m_Param.Count;
      }
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = component1.OnItemUpdate;
      ScrollClamped_VersusReward clampedVersusReward = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) clampedVersusReward, __vmethodptr(clampedVersusReward, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      Vector2 anchoredPosition = component2.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      sizeDelta.y = (__Null) ((double) component1.ItemScale * (double) this.Space * (double) this.m_Max);
      component2.set_sizeDelta(sizeDelta);
      component2.set_anchoredPosition(anchoredPosition);
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max || this.m_Param == null)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<VersusTowerParam>(obj, this.m_Param[idx]);
        if (this.Arrival)
        {
          VersusTowerRewardItem component = (VersusTowerRewardItem) obj.GetComponent<VersusTowerRewardItem>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Arrival, 0);
        }
        else
        {
          VersusSeasonRewardInfo component = (VersusSeasonRewardInfo) obj.GetComponent<VersusSeasonRewardInfo>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.Refresh();
        }
      }
    }
  }
}
