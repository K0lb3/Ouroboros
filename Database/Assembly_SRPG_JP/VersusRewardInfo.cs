// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusRewardInfo : MonoBehaviour
  {
    private readonly float SPACE_SCALE;
    public Toggle arrivedTgl;
    public Toggle seasonTgl;
    public GameObject ArrivalView;
    public GameObject SeasonView;
    public ScrollRect Scroll;
    public RectTransform ListParent;

    public VersusRewardInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.ArrivalView, (Object) null) || Object.op_Equality((Object) this.SeasonView, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.arrivedTgl, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.arrivedTgl.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeArrival)));
      }
      if (!Object.op_Inequality((Object) this.seasonTgl, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.seasonTgl.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeSeason)));
    }

    public void OnChangeArrival(bool flg)
    {
      if (!flg || !Object.op_Inequality((Object) this.SeasonView, (Object) null) || !Object.op_Inequality((Object) this.ArrivalView, (Object) null))
        return;
      this.SeasonView.SetActive(false);
      this.ArrivalView.SetActive(true);
      this.SetScrollRect((RectTransform) this.ArrivalView.GetComponent<RectTransform>());
      ScrollListController component = (ScrollListController) this.ArrivalView.GetComponent<ScrollListController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
      float num1 = (float) ((RectTransform) this.ArrivalView.GetComponent<RectTransform>()).get_sizeDelta().y;
      if (Object.op_Inequality((Object) this.ListParent, (Object) null))
      {
        double num2 = (double) num1;
        Rect rect = this.ListParent.get_rect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @rect).get_height();
        num1 = (float) (num2 - height);
      }
      int num3 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num4 = component.ItemScale * this.SPACE_SCALE;
      float num5 = Mathf.Min(num1 - num4 * (float) num3, num1);
      component.MovePos(num5, num5);
    }

    public void OnChangeSeason(bool flg)
    {
      if (!flg || !Object.op_Inequality((Object) this.SeasonView, (Object) null) || !Object.op_Inequality((Object) this.ArrivalView, (Object) null))
        return;
      this.ArrivalView.SetActive(false);
      this.SeasonView.SetActive(true);
      this.SetScrollRect((RectTransform) this.SeasonView.GetComponent<RectTransform>());
      ScrollListController component = (ScrollListController) this.SeasonView.GetComponent<ScrollListController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
      float num1 = (float) ((RectTransform) this.SeasonView.GetComponent<RectTransform>()).get_sizeDelta().y;
      if (Object.op_Inequality((Object) this.ListParent, (Object) null))
      {
        double num2 = (double) num1;
        Rect rect = this.ListParent.get_rect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @rect).get_height();
        num1 = (float) (num2 - height);
      }
      int num3 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num4 = component.ItemScale * this.SPACE_SCALE;
      float num5 = Mathf.Min(num1 - num4 * (float) num3, num1);
      component.MovePos(num5, num5);
    }

    private void SetScrollRect(RectTransform rect)
    {
      if (!Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) rect, (Object) null))
        return;
      Vector2 anchoredPosition = rect.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      rect.set_anchoredPosition(anchoredPosition);
      this.Scroll.set_content(rect);
    }
  }
}
