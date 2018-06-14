// Decompiled with JetBrains decompiler
// Type: VirtualList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualList : UIBehaviour
{
  public ScrollRect ScrollRect;
  public Vector2 ItemSize;
  private List<int> mItems;
  private List<VirtualList.ItemContainer> mItemObjects;
  private bool mBoundsChanging;
  private bool mDestroyed;
  private float mLastScrollPosition;
  private int mNumVisibleItems;
  private bool mInitialized;
  public VirtualList.GetItemObjectEvent OnGetItemObject;
  public VirtualList.ListEvent OnPostListUpdate;

  public VirtualList()
  {
    base.\u002Ector();
  }

  public void AddItem(int id)
  {
    if (id < 0 || this.mItems.Contains(id))
      return;
    this.mItems.Add(id);
  }

  public void RemoveItem(int id)
  {
    this.mItems.Add(id);
  }

  public void ClearItems()
  {
    this.mItems.Clear();
  }

  public int NumVisibleItems
  {
    get
    {
      return this.mNumVisibleItems;
    }
  }

  public int NumItems
  {
    get
    {
      return this.mItems.Count;
    }
  }

  private float HorizontalNormalizedPosition
  {
    get
    {
      if (Object.op_Equality((Object) this.ScrollRect, (Object) null))
        return 0.0f;
      return Mathf.Clamp(this.ScrollRect.get_horizontalNormalizedPosition(), 0.0f, 1f);
    }
  }

  public void Refresh(bool resetScrollPos = false)
  {
    this.RecalcBounds();
    if (resetScrollPos && Vector2.op_Inequality(this.ScrollRect.get_normalizedPosition(), Vector2.get_zero()))
      this.ScrollRect.set_normalizedPosition(Vector2.get_zero());
    this.Rebuild();
  }

  public RectTransform FindItem(int itemID)
  {
    VirtualList.ItemContainer itemContainer = this.FindItemContainer(itemID);
    if (Object.op_Inequality((Object) itemContainer, (Object) null))
      return itemContainer.Body;
    return (RectTransform) null;
  }

  protected virtual void Awake()
  {
    base.Awake();
  }

  protected virtual void Start()
  {
    base.Start();
    RectTransform transform = ((Component) this).get_transform() as RectTransform;
    transform.set_pivot(new Vector2(0.0f, 1f));
    RectTransform rectTransform = transform;
    Vector2 pivot = transform.get_pivot();
    transform.set_anchorMax(pivot);
    Vector2 vector2 = pivot;
    rectTransform.set_anchorMin(vector2);
    this.RecalcBounds();
    if (this.ScrollRect.get_horizontal() && this.ScrollRect.get_vertical())
      this.ScrollRect.set_vertical(false);
    this.mLastScrollPosition = 0.0f;
    // ISSUE: method pointer
    ((UnityEvent<Vector2>) this.ScrollRect.get_onValueChanged()).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnSrollRectChange)));
    this.mInitialized = true;
    this.RecalcBounds();
    this.Rebuild();
  }

  protected virtual void OnDestroy()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (Object.op_Inequality((Object) this.mItemObjects[index].Body, (Object) null) && Object.op_Equality((Object) ((Transform) this.mItemObjects[index].Body).get_parent(), (Object) this.mItemObjects[index].Body))
      {
        ((Transform) this.mItemObjects[index].Body).SetParent((Transform) null, false);
        this.mItemObjects[index].Body = (RectTransform) null;
      }
    }
    this.mDestroyed = true;
    base.OnDestroy();
  }

  protected virtual void OnRectTransformDimensionsChange()
  {
    base.OnRectTransformDimensionsChange();
    if (this.mBoundsChanging || !this.mInitialized)
      return;
    this.ScrollRect.set_horizontalNormalizedPosition(this.HorizontalNormalizedPosition);
    this.RecalcBounds();
    this.Rebuild();
  }

  private void OnSrollRectChange(Vector2 pos)
  {
    if (!this.mInitialized)
      return;
    this.Rebuild();
  }

  private bool IsDiscarding()
  {
    return this.mDestroyed;
  }

  private bool IsHorizontal
  {
    get
    {
      return this.ScrollRect.get_horizontal();
    }
  }

  private int CalcPitch()
  {
    RectTransform transform = ((Component) this.ScrollRect).get_transform() as RectTransform;
    int num;
    if (this.IsHorizontal)
    {
      Rect rect = transform.get_rect();
      // ISSUE: explicit reference operation
      num = (int) ((double) ((Rect) @rect).get_height() / this.ItemSize.y);
    }
    else
    {
      Rect rect = transform.get_rect();
      // ISSUE: explicit reference operation
      num = (int) ((double) ((Rect) @rect).get_width() / this.ItemSize.x);
    }
    return num;
  }

  private void RecalcBounds()
  {
    if (this.IsDiscarding())
      return;
    RectTransform transform = ((Component) this).get_transform() as RectTransform;
    Vector2 sizeDelta = transform.get_sizeDelta();
    Rect rect = (((Component) this.ScrollRect).get_transform() as RectTransform).get_rect();
    // ISSUE: explicit reference operation
    Vector2 size = ((Rect) @rect).get_size();
    int num1 = this.CalcPitch();
    if (num1 <= 0)
      return;
    int num2 = (this.mItems.Count + num1 - 1) / num1;
    if (this.IsHorizontal)
    {
      sizeDelta.x = (__Null) ((double) num2 * this.ItemSize.x);
      sizeDelta.y = size.y;
    }
    else
    {
      sizeDelta.x = size.x;
      sizeDelta.y = (__Null) ((double) num2 * this.ItemSize.y);
    }
    this.mBoundsChanging = true;
    RectTransform rectTransform = transform;
    Vector2 vector2_1 = new Vector2(0.0f, 1f);
    transform.set_pivot(vector2_1);
    Vector2 vector2_2 = vector2_1;
    transform.set_anchorMax(vector2_2);
    Vector2 vector2_3 = vector2_2;
    rectTransform.set_anchorMin(vector2_3);
    transform.set_sizeDelta(sizeDelta);
    this.mBoundsChanging = false;
  }

  private void ReserveItems(int maxItems)
  {
    Transform transform = ((Component) this).get_transform();
    this.mNumVisibleItems = maxItems;
    for (int count = this.mItemObjects.Count; count < maxItems; ++count)
    {
      VirtualList.ItemContainer component = (VirtualList.ItemContainer) new GameObject(count.ToString(), new System.Type[2]
      {
        typeof (RectTransform),
        typeof (VirtualList.ItemContainer)
      }).GetComponent<VirtualList.ItemContainer>();
      RectTransform rectTr = component.RectTr;
      Vector2 vector2_1 = new Vector2(0.0f, 1f);
      component.RectTr.set_anchorMax(vector2_1);
      Vector2 vector2_2 = vector2_1;
      rectTr.set_anchorMin(vector2_2);
      component.RectTr.set_sizeDelta(this.ItemSize);
      ((Transform) component.RectTr).SetParent(transform, false);
      this.mItemObjects.Add(component);
    }
  }

  private void DisableAllItems()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
      this.mItemObjects[index].SetBodyActive(false);
  }

  public void ForceUpdateItems()
  {
    if (this.OnGetItemObject == null)
      return;
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (this.mItemObjects[index].ItemID >= 0)
      {
        RectTransform rectTransform = this.OnGetItemObject(this.mItemObjects[index].ItemID, this.mItemObjects[index].ItemID, this.mItemObjects[index].Body);
      }
    }
  }

  private void Rebuild()
  {
    if (this.IsDiscarding())
      return;
    Rect rect1 = (((Component) this).get_transform() as RectTransform).get_rect();
    int num1 = this.CalcPitch();
    if (num1 <= 0)
      return;
    Rect rect2 = (((Component) this.ScrollRect).get_transform() as RectTransform).get_rect();
    bool isHorizontal = this.IsHorizontal;
    int num2;
    int num3;
    int num4;
    int num5;
    if (isHorizontal)
    {
      // ISSUE: explicit reference operation
      this.ReserveItems(Mathf.Max(Mathf.CeilToInt(((Rect) @rect2).get_width() / (float) this.ItemSize.x) + 1, 1) * num1);
      // ISSUE: explicit reference operation
      if ((double) ((Rect) @rect1).get_width() <= 0.0)
      {
        this.DisableAllItems();
        return;
      }
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      num2 = Mathf.FloorToInt(-this.HorizontalNormalizedPosition * ((double) ((Rect) @rect1).get_width() >= (double) ((Rect) @rect2).get_width() ? ((Rect) @rect2).get_width() - ((Rect) @rect1).get_width() : 0.0f) / (float) this.ItemSize.x);
      if ((double) this.mLastScrollPosition <= (double) this.HorizontalNormalizedPosition)
      {
        num3 = 0;
        num4 = this.mItemObjects.Count;
        num5 = 1;
      }
      else
      {
        num3 = this.mItemObjects.Count - 1;
        num4 = -1;
        num5 = -1;
      }
      this.mLastScrollPosition = this.HorizontalNormalizedPosition;
    }
    else
    {
      // ISSUE: explicit reference operation
      this.ReserveItems(num1 * Mathf.Max(Mathf.CeilToInt(((Rect) @rect2).get_width() / (float) this.ItemSize.x), 1));
      // ISSUE: explicit reference operation
      if ((double) ((Rect) @rect1).get_height() <= 0.0)
      {
        this.DisableAllItems();
        return;
      }
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      num2 = Mathf.FloorToInt((float) -(1.0 - (double) this.ScrollRect.get_verticalNormalizedPosition()) * (((Rect) @rect2).get_height() - ((Rect) @rect1).get_height()) / (float) this.ItemSize.y);
      if ((double) this.mLastScrollPosition >= (double) this.ScrollRect.get_verticalNormalizedPosition())
      {
        num3 = 0;
        num4 = this.mItemObjects.Count;
        num5 = 1;
      }
      else
      {
        num3 = this.mItemObjects.Count - 1;
        num4 = -1;
        num5 = -1;
      }
      this.mLastScrollPosition = this.ScrollRect.get_verticalNormalizedPosition();
    }
    bool flag = false;
    int index1 = num3;
    while (index1 != num4)
    {
      RectTransform rectTr = this.mItemObjects[index1].RectTr;
      int num6;
      int num7;
      int index2;
      if (isHorizontal)
      {
        num6 = index1 / num1 + num2;
        num7 = index1 % num1;
        index2 = num6 * num1 + num7;
      }
      else
      {
        num6 = index1 % num1;
        num7 = index1 / num1 + num2;
        index2 = num6 + num7 * num1;
      }
      Vector2 sizeDelta = rectTr.get_sizeDelta();
      Vector2 pivot = rectTr.get_pivot();
      float num8 = (float) ((double) num6 * this.ItemSize.x + sizeDelta.x * pivot.x);
      float num9 = (float) ((double) -num7 * this.ItemSize.y - sizeDelta.y * pivot.y);
      if (0 <= index2 && index2 < this.mItems.Count && index1 < this.mItems.Count)
      {
        VirtualList.ItemContainer mItemObject = this.mItemObjects[index1];
        mItemObject.SetBodyActive(true);
        this.FillContainer(mItemObject, this.mItems[index2]);
      }
      else
        this.mItemObjects[index1].SetBodyActive(false);
      rectTr.set_anchoredPosition(new Vector2(num8, num9));
      index1 += num5;
    }
    if (flag)
      this.ReparentItems();
    if (this.OnPostListUpdate == null)
      return;
    this.OnPostListUpdate();
  }

  private int FindItemAtPosition(int itemID, float x, float y)
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      float num1 = Mathf.Abs(x - (float) this.mItemObjects[index].RectTr.get_anchoredPosition().x);
      float num2 = Mathf.Abs(y - (float) this.mItemObjects[index].RectTr.get_anchoredPosition().y);
      if (this.mItemObjects[index].ItemID == itemID && (double) num1 <= 0.00999999977648258 && (double) num2 <= 0.00999999977648258)
        return index;
    }
    return -1;
  }

  private VirtualList.ItemContainer FindItemContainer(int itemID)
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (this.mItemObjects[index].ItemID == itemID)
        return this.mItemObjects[index];
    }
    return (VirtualList.ItemContainer) null;
  }

  private void FillContainer(VirtualList.ItemContainer container, int itemID)
  {
    RectTransform rectTransform = (RectTransform) null;
    if (this.OnGetItemObject != null)
      rectTransform = this.OnGetItemObject(itemID, !Object.op_Inequality((Object) container.Body, (Object) null) ? -1 : container.ItemID, container.Body);
    if (Object.op_Equality((Object) rectTransform, (Object) null))
      return;
    if (Object.op_Inequality((Object) container.Body, (Object) null) && Object.op_Inequality((Object) container.Body, (Object) rectTransform) && Object.op_Equality((Object) ((Transform) container.Body).get_parent(), (Object) container.RectTr))
      ((Transform) container.Body).SetParent((Transform) null, false);
    container.Body = rectTransform;
    container.ItemID = itemID;
    ((Transform) rectTransform).SetParent((Transform) container.RectTr, false);
    Vector2 anchoredPosition = rectTransform.get_anchoredPosition();
    // ISSUE: explicit reference operation
    if ((double) ((Vector2) @anchoredPosition).get_sqrMagnitude() <= 0.0)
      return;
    rectTransform.set_anchoredPosition(Vector2.get_zero());
  }

  private void ReparentItems()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (Object.op_Inequality((Object) this.mItemObjects[index].Body, (Object) null) && Object.op_Inequality((Object) ((Transform) this.mItemObjects[index].Body).get_parent(), (Object) this.mItemObjects[index].RectTr))
        ((Transform) this.mItemObjects[index].Body).SetParent((Transform) this.mItemObjects[index].RectTr, false);
    }
  }

  private void SwapFast(VirtualList.ItemContainer a, VirtualList.ItemContainer b)
  {
    int itemId = a.ItemID;
    a.ItemID = b.ItemID;
    b.ItemID = itemId;
    RectTransform body = a.Body;
    a.Body = b.Body;
    b.Body = body;
  }

  private class ItemContainer : MonoBehaviour
  {
    public RectTransform RectTr;
    public int ItemID;
    public RectTransform Body;

    public ItemContainer()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.RectTr = ((Component) this).get_transform() as RectTransform;
    }

    public void SetBodyActive(bool active)
    {
      if (!Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      ((Component) this.Body).get_gameObject().SetActive(active);
    }
  }

  public delegate RectTransform GetItemObjectEvent(int item, int old, RectTransform current);

  public delegate void ListEvent();
}
