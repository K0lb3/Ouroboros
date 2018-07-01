// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScrollListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "背景ロード完了", FlowNode.PinTypes.Output, 0)]
  public class TowerScrollListController : MonoBehaviour
  {
    private static string BGTexturePath = "Tower/TowerBGs";
    private static string FloorBGTexturePath = "Tower/TowerFloors";
    private static string LockFloorBGTexturePath = "Tower/TowerLockFloors";
    [SerializeField]
    private RectTransform m_ItemBase;
    [SerializeField]
    internal TowerScrollListController.ScrollMode m_ScrollMode;
    [Range(0.0f, 30f)]
    [SerializeField]
    protected int m_ItemCnt;
    [SerializeField]
    private float m_Margin;
    public List<RectTransform> m_ItemList;
    private float m_PrevPosition;
    private int m_CurrentItemID;
    public TowerScrollListController.Direction m_Direction;
    private RectTransform m_RectTransform;
    public TowerScrollListController.OnItemPositionChange OnItemUpdate;
    [SerializeField]
    private RectTransform Cursor;
    [SerializeField]
    private ScrollAutoFit m_ScrollAutoFit;
    [SerializeField]
    private SyncScroll m_ScrollBG;
    public Selectable PageUpButton;
    public Selectable PageDownButton;
    public TowerScrollListController.ListItemFocusEvent OnListItemFocus;
    [SerializeField]
    private Button mChallengeButton;
    [SerializeField]
    private Animator FadeAnimator;
    [SerializeField]
    private RawImage Bg;
    private float m_ItemScale;

    public TowerScrollListController()
    {
      base.\u002Ector();
    }

    public float Margin
    {
      get
      {
        return this.m_Margin;
      }
    }

    protected RectTransform GetRectTransForm
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_RectTransform, (UnityEngine.Object) null))
          this.m_RectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    public float ItemScale
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ItemBase, (UnityEngine.Object) null) && (double) this.m_ItemScale == -1.0)
          this.m_ItemScale = this.m_Direction != TowerScrollListController.Direction.Vertical ? (float) this.m_ItemBase.get_sizeDelta().x : (float) this.m_ItemBase.get_sizeDelta().y;
        return this.m_ItemScale;
      }
    }

    public float ItemScaleMargin
    {
      get
      {
        return this.ItemScale * this.Margin;
      }
    }

    internal static void SetAnchor(RectTransform rt, TowerScrollListController.ScrollMode scrollMode)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) rt, (UnityEngine.Object) null))
        return;
      if (scrollMode == TowerScrollListController.ScrollMode.Normal)
      {
        Vector2 anchoredPosition = rt.get_anchoredPosition();
        float x1 = (float) rt.get_anchorMin().x;
        float x2 = (float) rt.get_anchorMax().x;
        rt.set_anchorMin(new Vector2(x1, 1f));
        rt.set_anchorMax(new Vector2(x2, 1f));
        rt.set_pivot(new Vector2(0.0f, 1f));
        rt.set_anchoredPosition(anchoredPosition);
      }
      else
      {
        Vector2 anchoredPosition = rt.get_anchoredPosition();
        float x1 = (float) rt.get_anchorMin().x;
        float x2 = (float) rt.get_anchorMax().x;
        rt.set_anchorMin(new Vector2(x1, 0.0f));
        rt.set_anchorMax(new Vector2(x2, 0.0f));
        rt.set_pivot(new Vector2(0.0f, 0.0f));
        rt.set_anchoredPosition(anchoredPosition);
      }
    }

    internal static void SetItemAnchor(RectTransform rt, TowerScrollListController.ScrollMode scrollMode)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) rt, (UnityEngine.Object) null))
        return;
      if (scrollMode == TowerScrollListController.ScrollMode.Normal)
      {
        Vector2 anchoredPosition = rt.get_anchoredPosition();
        anchoredPosition.y = -anchoredPosition.y;
        float x1 = (float) rt.get_anchorMin().x;
        float x2 = (float) rt.get_anchorMax().x;
        rt.set_anchorMin(new Vector2(x1, 1f));
        rt.set_anchorMax(new Vector2(x2, 1f));
        rt.set_anchoredPosition(anchoredPosition);
      }
      else
      {
        Vector2 anchoredPosition = rt.get_anchoredPosition();
        anchoredPosition.y = -anchoredPosition.y;
        float x1 = (float) rt.get_anchorMin().x;
        float x2 = (float) rt.get_anchorMax().x;
        rt.set_anchorMin(new Vector2(x1, 0.0f));
        rt.set_anchorMax(new Vector2(x2, 0.0f));
        rt.set_anchoredPosition(anchoredPosition);
      }
    }

    internal void SetAnchor(TowerScrollListController.ScrollMode scrollMode)
    {
      if (!Application.get_isPlaying())
      {
        TowerScrollListController.SetAnchor(this.GetRectTransForm, scrollMode);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollBG, (UnityEngine.Object) null))
        {
          TowerScrollListController.SetAnchor((RectTransform) ((Component) this.m_ScrollBG).GetComponent<RectTransform>(), scrollMode);
          this.m_ScrollBG.isNormal = scrollMode == TowerScrollListController.ScrollMode.Normal;
        }
        TowerScrollListController.SetItemAnchor(this.m_ItemBase, scrollMode);
      }
      else
      {
        TowerScrollListController.SetAnchor(this.GetRectTransForm, scrollMode);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollBG, (UnityEngine.Object) null))
        {
          TowerScrollListController.SetAnchor((RectTransform) ((Component) this.m_ScrollBG).GetComponent<RectTransform>(), scrollMode);
          this.m_ScrollBG.isNormal = scrollMode == TowerScrollListController.ScrollMode.Normal;
        }
        using (List<RectTransform>.Enumerator enumerator = this.m_ItemList.GetEnumerator())
        {
          while (enumerator.MoveNext())
            TowerScrollListController.SetItemAnchor(enumerator.Current, scrollMode);
        }
      }
    }

    protected virtual void Start()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollAutoFit, (UnityEngine.Object) null))
      {
        this.m_ScrollAutoFit.set_content(this.GetRectTransForm);
        this.m_ScrollAutoFit.ItemScale = this.ItemScaleMargin;
        // ISSUE: method pointer
        this.m_ScrollAutoFit.OnScrollStop.AddListener(new UnityAction((object) this, __methodptr(OnScrollStop)));
      }
      ((Component) this.m_ItemBase).get_gameObject().SetActive(false);
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      List<TowerQuestListItem> towerQuestListItemList = new List<TowerQuestListItem>();
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = (RectTransform) UnityEngine.Object.Instantiate<RectTransform>((M0) this.m_ItemBase);
        ((Transform) rectTransform).SetParent(((Component) this).get_transform(), false);
        rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Margin * (double) index + (double) this.ItemScale * 0.5) * num));
        this.m_ItemList.Add(rectTransform);
        TowerQuestListItem component = (TowerQuestListItem) ((Component) rectTransform).GetComponent<TowerQuestListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          towerQuestListItemList.Add(component);
        ((Component) rectTransform).get_gameObject().SetActive(true);
      }
      using (List<ScrollListSetUp>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          current.OnSetUpItems();
          for (int idx = 0; idx < this.m_ItemCnt; ++idx)
            current.OnUpdateItems(idx, ((Component) this.m_ItemList[idx]).get_gameObject());
        }
      }
      this.m_ScrollMode = !MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID).is_down ? TowerScrollListController.ScrollMode.Reverse : TowerScrollListController.ScrollMode.Normal;
      this.ChangeScrollMode(this.m_ScrollMode);
      this.StartCoroutine(this.LoadTowerBG(towerQuestListItemList.ToArray()));
    }

    private float AnchoredPosition
    {
      get
      {
        if (this.m_ScrollMode == TowerScrollListController.ScrollMode.Normal)
        {
          if (this.m_Direction == TowerScrollListController.Direction.Vertical)
            return (float) -this.GetRectTransForm.get_anchoredPosition().y;
          return (float) this.GetRectTransForm.get_anchoredPosition().x;
        }
        if (this.m_Direction == TowerScrollListController.Direction.Vertical)
          return (float) this.GetRectTransForm.get_anchoredPosition().y;
        return (float) this.GetRectTransForm.get_anchoredPosition().x;
      }
    }

    public void SetAnchoredPosition(float position)
    {
      if (this.m_Direction == TowerScrollListController.Direction.Vertical)
      {
        Vector2 anchoredPosition = this.GetRectTransForm.get_anchoredPosition();
        anchoredPosition.y = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? (__Null) -(double) position : (__Null) (double) position;
        this.GetRectTransForm.set_anchoredPosition(anchoredPosition);
      }
      else
      {
        Vector2 anchoredPosition = this.GetRectTransForm.get_anchoredPosition();
        anchoredPosition.x = (__Null) (double) position;
        this.GetRectTransForm.set_anchoredPosition(anchoredPosition);
      }
    }

    private void OnScrollStop()
    {
      float scrollDir = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      this.MovePosition(scrollDir);
      Rect rect = this.Cursor.get_rect();
      // ISSUE: explicit reference operation
      ((Rect) @rect).set_center(new Vector2(0.0f, (float) ((double) this.ItemScaleMargin * 3.0 - (double) this.ItemScaleMargin * 0.5) * scrollDir));
      // ISSUE: explicit reference operation
      ((Rect) @rect).set_size(this.Cursor.get_sizeDelta());
      using (List<RectTransform>.Enumerator enumerator = this.m_ItemList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          RectTransform current = enumerator.Current;
          if (((Component) current).get_gameObject().get_activeInHierarchy())
          {
            Vector2 anchoredPosition = current.get_anchoredPosition();
            anchoredPosition.y = this.GetRectTransForm.get_anchoredPosition().y + anchoredPosition.y;
            // ISSUE: explicit reference operation
            if (((Rect) @rect).Contains(anchoredPosition))
              this.OnListItemFocus.Invoke(((Component) current).get_gameObject());
          }
        }
      }
    }

    private void FocusUpdate()
    {
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      Rect rect = this.Cursor.get_rect();
      // ISSUE: explicit reference operation
      ((Rect) @rect).set_center(new Vector2(0.0f, (float) ((double) this.ItemScaleMargin * 3.0 - (double) this.ItemScaleMargin * 0.5) * num));
      // ISSUE: explicit reference operation
      ((Rect) @rect).set_size(this.Cursor.get_sizeDelta());
      using (List<TowerQuestListItem>.Enumerator enumerator = this.m_ItemList.ConvertAll<TowerQuestListItem>((Converter<RectTransform, TowerQuestListItem>) (item => (TowerQuestListItem) ((Component) item).GetComponent<TowerQuestListItem>())).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TowerQuestListItem current = enumerator.Current;
          if (((Component) current).get_gameObject().get_activeInHierarchy())
          {
            Vector2 anchoredPosition = current.rectTransform.get_anchoredPosition();
            anchoredPosition.y = this.GetRectTransForm.get_anchoredPosition().y + anchoredPosition.y;
            // ISSUE: explicit reference operation
            current.OnFocus(((Rect) @rect).Contains(anchoredPosition));
          }
        }
      }
    }

    private void LateUpdate()
    {
      this.MovePosition(this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f);
      float num1 = Mathf.Abs(Vector2.Dot(this.m_ScrollAutoFit.get_normalizedPosition(), this.ScrollDir));
      RectTransform transform1 = ((Component) this.m_ScrollAutoFit).get_transform() as RectTransform;
      RectTransform transform2 = ((Component) this.m_ScrollAutoFit.get_content()).get_transform() as RectTransform;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ScrollAutoFit.get_content(), (UnityEngine.Object) null))
      {
        Rect rect1 = transform1.get_rect();
        // ISSUE: explicit reference operation
        float num2 = Mathf.Abs(Vector2.Dot(((Rect) @rect1).get_size(), this.ScrollDir));
        Rect rect2 = transform2.get_rect();
        // ISSUE: explicit reference operation
        float num3 = Mathf.Abs(Vector2.Dot(((Rect) @rect2).get_size(), this.ScrollDir));
        if (this.m_ScrollAutoFit.get_horizontal())
          num1 = 1f - num1;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageUpButton, (UnityEngine.Object) null))
          this.PageUpButton.set_interactable((double) num1 < 0.999000012874603 && (double) num2 < (double) num3);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageDownButton, (UnityEngine.Object) null))
          this.PageDownButton.set_interactable((double) num1 > 1.0 / 1000.0 && (double) num2 < (double) num3);
      }
      this.FocusUpdate();
    }

    private void MovePosition(float scrollDir)
    {
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition < -((double) this.ItemScaleMargin + (double) this.ItemScale * 0.5))
      {
        this.m_PrevPosition -= this.ItemScaleMargin;
        RectTransform rectTransform1 = this.m_ItemList[0];
        RectTransform rectTransform2 = ((IEnumerable<RectTransform>) this.m_ItemList).Last<RectTransform>();
        this.m_ItemList.RemoveAt(0);
        this.m_ItemList.Add(rectTransform1);
        float num = (float) (rectTransform2.get_anchoredPosition().y + (double) this.ItemScaleMargin * (double) scrollDir);
        rectTransform1.set_anchoredPosition(new Vector2(0.0f, num));
        this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, ((Component) rectTransform1).get_gameObject());
        ++this.m_CurrentItemID;
      }
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition > -(double) this.ItemScale * 0.5)
      {
        this.m_PrevPosition += this.ItemScaleMargin;
        int index = this.m_ItemCnt - 1;
        RectTransform rectTransform1 = this.m_ItemList[index];
        RectTransform rectTransform2 = this.m_ItemList[0];
        this.m_ItemList.RemoveAt(index);
        this.m_ItemList.Insert(0, rectTransform1);
        --this.m_CurrentItemID;
        float num = (float) (rectTransform2.get_anchoredPosition().y - (double) this.ItemScaleMargin * (double) scrollDir);
        rectTransform1.set_anchoredPosition(new Vector2(0.0f, num));
        this.OnItemUpdate.Invoke(this.m_CurrentItemID, ((Component) rectTransform1).get_gameObject());
      }
    }

    public void UpdateList()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_content(this.GetRectTransForm);
      ((Component) this.m_ItemBase).get_gameObject().SetActive(false);
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = this.m_ItemList[index];
        ((Transform) rectTransform).SetParent(((Component) this).get_transform(), false);
        rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Margin * (double) index + (double) this.ItemScale * 0.5) * num));
        ((Component) rectTransform).get_gameObject().SetActive(true);
      }
      using (List<ScrollListSetUp>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          current.OnSetUpItems();
          for (int idx = 0; idx < this.m_ItemCnt; ++idx)
            current.OnUpdateItems(idx, ((Component) this.m_ItemList[idx]).get_gameObject());
        }
      }
      this.m_PrevPosition = 0.0f;
      this.m_CurrentItemID = 0;
      RectTransform component = (RectTransform) ((Component) ((Component) this).get_transform()).GetComponent<RectTransform>();
      Vector2 anchoredPosition = component.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      component.set_anchoredPosition(anchoredPosition);
    }

    public void ChangeScrollMode(TowerScrollListController.ScrollMode scrollMode)
    {
      this.m_ScrollMode = scrollMode;
      this.SetAnchor(scrollMode);
      this.m_ItemList.Reverse();
      if (!Application.get_isPlaying())
        return;
      this.UpdateList();
    }

    private void _SetScrollTo(float pos)
    {
      Rect rect1 = this.m_ScrollAutoFit.get_viewport().get_rect();
      // ISSUE: explicit reference operation
      float y = (float) ((Rect) @rect1).get_size().y;
      Rect rect2 = this.m_ScrollAutoFit.rect;
      // ISSUE: explicit reference operation
      float num1 = (float) ((double) (float) ((Rect) @rect2).get_size().y * 0.5 - (double) y * 0.5);
      float num2 = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      float num3 = num1;
      float num4 = (float) ((double) num1 + (double) y - this.m_RectTransform.get_sizeDelta().y);
      float num5 = num3 * num2;
      float num6 = num4 * num2;
      float num7 = Mathf.Min(num5, num6);
      float num8 = Mathf.Max(num5, num6);
      pos = Mathf.Clamp(pos, num7, num8);
      this.m_ScrollAutoFit.SetScrollTo(pos);
    }

    public void SetScrollTo(float pos)
    {
      if (this.m_Direction != TowerScrollListController.Direction.Vertical)
        return;
      if (this.m_ScrollMode == TowerScrollListController.ScrollMode.Normal)
        this._SetScrollTo(pos);
      else
        this._SetScrollTo(-pos);
    }

    public void PageUp(int value)
    {
      int num = Mathf.RoundToInt((float) this.GetRectTransForm.get_anchoredPosition().y / this.ItemScale);
      ((Selectable) this.mChallengeButton).set_interactable(false);
      this._SetScrollTo((float) ((double) num * (double) this.ItemScale - (double) value * (double) this.ItemScale));
    }

    public void PageDown(int value)
    {
      int num = Mathf.RoundToInt((float) this.GetRectTransForm.get_anchoredPosition().y / this.ItemScale);
      ((Selectable) this.mChallengeButton).set_interactable(false);
      this._SetScrollTo((float) ((double) num * (double) this.ItemScale + (double) value * (double) this.ItemScale));
    }

    private Vector2 ScrollDir
    {
      get
      {
        if (this.m_ScrollAutoFit.get_vertical())
          return Vector2.op_UnaryNegation(Vector2.get_up());
        return Vector2.get_right();
      }
    }

    [DebuggerHidden]
    public IEnumerator LoadTowerBG(TowerQuestListItem[] tower_quest_list)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerScrollListController.\u003CLoadTowerBG\u003Ec__Iterator145()
      {
        tower_quest_list = tower_quest_list,
        \u003C\u0024\u003Etower_quest_list = tower_quest_list,
        \u003C\u003Ef__this = this
      };
    }

    public void SetTowerImage(LoadRequest floor_req, int index, string image_name, TowerQuestListItem[] tower_quest_list)
    {
      GachaTabSprites asset = floor_req.asset as GachaTabSprites;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) asset, (UnityEngine.Object) null))
        return;
      for (int index1 = 0; index1 < asset.Sprites.Length; ++index1)
      {
        if (!(((UnityEngine.Object) asset.Sprites[index1]).get_name() != image_name))
        {
          for (int index2 = 0; index2 < tower_quest_list.Length; ++index2)
          {
            tower_quest_list[index2].Banner[0].Images[index] = asset.Sprites[index1];
            tower_quest_list[index2].Banner[1].Images[index] = asset.Sprites[index1];
          }
        }
      }
    }

    [Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject>
    {
      public OnItemPositionChange()
      {
        base.\u002Ector();
      }
    }

    public enum Direction
    {
      Vertical,
      Horizontal,
    }

    public enum ScrollMode
    {
      Normal,
      Reverse,
    }

    [SerializeField]
    public class ListItemFocusEvent : UnityEvent<GameObject>
    {
      public ListItemFocusEvent()
      {
        base.\u002Ector();
      }
    }
  }
}
