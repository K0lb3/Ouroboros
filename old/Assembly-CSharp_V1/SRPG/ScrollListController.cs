// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollListController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ScrollListController : MonoBehaviour
  {
    [SerializeField]
    private RectTransform m_ItemBase;
    [SerializeField]
    [Range(0.0f, 30f)]
    protected int m_ItemCnt;
    public ScrollListController.OnItemPositionChange OnItemUpdate;
    public List<RectTransform> m_ItemList;
    private float m_PrevPosition;
    private int m_CurrentItemID;
    public ScrollListController.Direction m_Direction;
    private RectTransform m_RectTransform;
    private float m_ItemScale;

    public ScrollListController()
    {
      base.\u002Ector();
    }

    protected RectTransform GetRectTransForm
    {
      get
      {
        if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          this.m_RectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private float AnchoredPosition
    {
      get
      {
        if (this.m_Direction == ScrollListController.Direction.Vertical)
          return (float) -this.GetRectTransForm.get_anchoredPosition().y;
        return (float) this.GetRectTransForm.get_anchoredPosition().x;
      }
    }

    public float ItemScale
    {
      get
      {
        if (Object.op_Inequality((Object) this.m_ItemBase, (Object) null) && (double) this.m_ItemScale == -1.0)
          this.m_ItemScale = this.m_Direction != ScrollListController.Direction.Vertical ? (float) this.m_ItemBase.get_sizeDelta().x : (float) this.m_ItemBase.get_sizeDelta().y;
        return this.m_ItemScale;
      }
    }

    protected virtual void Start()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_content(this.GetRectTransForm);
      ((Component) this.m_ItemBase).get_gameObject().SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = (RectTransform) Object.Instantiate<RectTransform>((M0) this.m_ItemBase);
        ((Transform) rectTransform).SetParent(((Component) this).get_transform(), false);
        rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) (-(double) this.ItemScale * 1.20000004768372 * (double) index - (double) this.ItemScale * 0.5)));
        this.m_ItemList.Add(rectTransform);
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
    }

    private void Update()
    {
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition < -((double) this.ItemScale * 1.20000004768372 + (double) this.ItemScale * 0.5))
      {
        this.m_PrevPosition -= this.ItemScale * 1.2f;
        RectTransform rectTransform1 = this.m_ItemList[0];
        RectTransform rectTransform2 = ((IEnumerable<RectTransform>) this.m_ItemList).Last<RectTransform>();
        this.m_ItemList.RemoveAt(0);
        this.m_ItemList.Add(rectTransform1);
        float num = (float) (rectTransform2.get_anchoredPosition().y - (double) this.ItemScale * 1.20000004768372);
        rectTransform1.set_anchoredPosition(new Vector2(0.0f, num));
        this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, ((Component) rectTransform1).get_gameObject());
        ++this.m_CurrentItemID;
      }
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition > -(double) this.ItemScale * 0.5)
      {
        this.m_PrevPosition += this.ItemScale * 1.2f;
        int index = this.m_ItemCnt - 1;
        RectTransform rectTransform1 = this.m_ItemList[index];
        RectTransform rectTransform2 = this.m_ItemList[0];
        this.m_ItemList.RemoveAt(index);
        this.m_ItemList.Insert(0, rectTransform1);
        --this.m_CurrentItemID;
        float num = (float) (rectTransform2.get_anchoredPosition().y + (double) this.ItemScale * 1.20000004768372);
        rectTransform1.set_anchoredPosition(new Vector2(0.0f, num));
        this.OnItemUpdate.Invoke(this.m_CurrentItemID, ((Component) rectTransform1).get_gameObject());
      }
    }

    public void UpdateList()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_content(this.GetRectTransForm);
      ((Component) this.m_ItemBase).get_gameObject().SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = this.m_ItemList[index];
        ((Transform) rectTransform).SetParent(((Component) this).get_transform(), false);
        rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) (-(double) this.ItemScale * 1.20000004768372 * (double) index - (double) this.ItemScale * 0.5)));
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
  }
}
