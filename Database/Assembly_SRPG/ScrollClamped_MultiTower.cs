// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_MultiTower
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
  public class ScrollClamped_MultiTower : MonoBehaviour, ScrollListSetUp
  {
    private readonly float OFFSET;
    private readonly int MARGIN;
    private int mMax;
    public float Space;
    public ScrollAutoFit AutoFit;
    public MultiTowerInfo TowerInfo;

    public ScrollClamped_MultiTower()
    {
      base.\u002Ector();
    }

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      List<MultiTowerFloorParam> mtAllFloorParam = MonoSingleton<GameManager>.Instance.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      if (mtAllFloorParam != null)
        this.mMax = mtAllFloorParam.Count;
      this.mMax += this.MARGIN;
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = component1.OnItemUpdate;
      ScrollClamped_MultiTower clampedMultiTower = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) clampedMultiTower, __vmethodptr(clampedMultiTower, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      Vector2 anchoredPosition = component2.get_anchoredPosition();
      float num1 = component1.ItemScale * this.Space;
      float num2 = num1 - component1.ItemScale;
      anchoredPosition.y = (__Null) ((double) component1.ItemScale * (double) this.OFFSET);
      sizeDelta.y = (__Null) ((double) num1 * (double) (this.mMax - this.MARGIN) - (double) num2);
      component2.set_sizeDelta(sizeDelta);
      component2.set_anchoredPosition(anchoredPosition);
      if (Object.op_Inequality((Object) this.AutoFit, (Object) null))
        this.AutoFit.ItemScale = component1.ItemScale * this.Space;
      this.TowerInfo.Init();
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (idx < 0 || idx >= this.mMax)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx + 1);
        if (mtFloorParam != null)
        {
          DataSource.Bind<MultiTowerFloorParam>(obj, mtFloorParam);
        }
        else
        {
          DataSource component = (DataSource) obj.GetComponent<DataSource>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Clear();
        }
        MultiTowerFloorInfo component1 = (MultiTowerFloorInfo) obj.GetComponent<MultiTowerFloorInfo>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          return;
        component1.Refresh();
      }
    }
  }
}
