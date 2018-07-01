// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    public ScrollListController.OnAfterStartUpEvent OnAfterStartup;
    public ScrollListController.OnUpdateEvent OnUpdateItemEvent;
    public List<RectTransform> m_ItemList;
    private List<Vector2> m_ItemPos;
    private float m_PrevPosition;
    private int m_CurrentItemID;
    public ScrollListController.Direction m_Direction;
    public ScrollListController.Mode m_ScrollMode;
    public float Space;
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
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_RectTransform, (UnityEngine.Object) null))
          this.m_RectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    public float AnchoredPosition
    {
      get
      {
        if (this.m_ScrollMode == ScrollListController.Mode.Normal)
        {
          if (this.m_Direction == ScrollListController.Direction.Vertical)
            return (float) -this.GetRectTransForm.get_anchoredPosition().y;
          return (float) this.GetRectTransForm.get_anchoredPosition().x;
        }
        if (this.m_Direction == ScrollListController.Direction.Vertical)
          return (float) this.GetRectTransForm.get_anchoredPosition().y;
        return (float) this.GetRectTransForm.get_anchoredPosition().x;
      }
    }

    public float ItemScale
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ItemBase, (UnityEngine.Object) null) && (double) this.m_ItemScale == -1.0)
          this.m_ItemScale = this.m_Direction != ScrollListController.Direction.Vertical ? (float) this.m_ItemBase.get_sizeDelta().x : (float) this.m_ItemBase.get_sizeDelta().y;
        return this.m_ItemScale;
      }
    }

    public float ScrollDir
    {
      get
      {
        return this.m_ScrollMode == ScrollListController.Mode.Normal ? -1f : 1f;
      }
    }

    public List<RectTransform> ItemList
    {
      get
      {
        return this.m_ItemList;
      }
    }

    public List<Vector2> ItemPosList
    {
      get
      {
        return this.m_ItemPos;
      }
    }

    protected virtual void Start()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_content(this.GetRectTransForm);
      ((Component) this.m_ItemBase).get_gameObject().SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = (RectTransform) UnityEngine.Object.Instantiate<RectTransform>((M0) this.m_ItemBase);
        ((Transform) rectTransform).SetParent(((Component) this).get_transform(), false);
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
          rectTransform.set_anchoredPosition(new Vector2((float) ((double) this.ItemScale * (double) this.Space * (double) index + (double) this.ItemScale * 0.5) * this.ScrollDir, 0.0f));
        else
          rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Space * (double) index + (double) this.ItemScale * 0.5) * this.ScrollDir));
        this.m_ItemList.Add(rectTransform);
        this.m_ItemPos.Add(rectTransform.get_anchoredPosition());
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
      if (this.OnAfterStartup == null)
        return;
      this.OnAfterStartup.Invoke(true);
    }

    private void Update()
    {
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition < -((double) this.ItemScale * (double) this.Space + (double) this.ItemScale * 0.5))
      {
        this.m_PrevPosition -= this.ItemScale * this.Space;
        RectTransform rectTransform = this.m_ItemList[0];
        this.m_ItemList.RemoveAt(0);
        this.m_ItemList.Add(rectTransform);
        Vector2 vector2 = ((IEnumerable<Vector2>) this.m_ItemPos).Last<Vector2>();
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
        {
          float num = (float) (vector2.x + (double) this.ItemScale * (double) this.Space * (double) this.ScrollDir);
          rectTransform.set_anchoredPosition(new Vector2(num, 0.0f));
        }
        else
        {
          float num = (float) (vector2.y + (double) this.ItemScale * (double) this.Space * (double) this.ScrollDir);
          rectTransform.set_anchoredPosition(new Vector2(0.0f, num));
        }
        this.m_ItemPos.RemoveAt(0);
        this.m_ItemPos.Add(rectTransform.get_anchoredPosition());
        this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, ((Component) rectTransform).get_gameObject());
        ++this.m_CurrentItemID;
      }
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition > -(double) this.ItemScale * 0.5)
      {
        this.m_PrevPosition += this.ItemScale * this.Space;
        int index = this.m_ItemCnt - 1;
        RectTransform rectTransform = this.m_ItemList[index];
        this.m_ItemList.RemoveAt(index);
        this.m_ItemList.Insert(0, rectTransform);
        --this.m_CurrentItemID;
        Vector2 itemPo = this.m_ItemPos[0];
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
        {
          float num = (float) (itemPo.x - (double) this.ItemScale * (double) this.Space * (double) this.ScrollDir);
          rectTransform.set_anchoredPosition(new Vector2(num, 0.0f));
        }
        else
        {
          float num = (float) (itemPo.y - (double) this.ItemScale * (double) this.Space * (double) this.ScrollDir);
          rectTransform.set_anchoredPosition(new Vector2(0.0f, num));
        }
        this.m_ItemPos.RemoveAt(index);
        this.m_ItemPos.Insert(0, rectTransform.get_anchoredPosition());
        this.OnItemUpdate.Invoke(this.m_CurrentItemID, ((Component) rectTransform).get_gameObject());
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.m_ItemList);
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
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
          rectTransform.set_anchoredPosition(new Vector2((float) (-(double) this.ItemScale * (double) this.Space * (double) index - (double) this.ItemScale * 0.5), 0.0f));
        else
          rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) (-(double) this.ItemScale * (double) this.Space * (double) index - (double) this.ItemScale * 0.5)));
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

    public void Refresh()
    {
      using (List<ScrollListSetUp>.Enumerator enumerator = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          for (int idx = 0; idx < this.m_ItemList.Count; ++idx)
          {
            RectTransform rectTransform = this.m_ItemList[idx];
            if (this.m_Direction == ScrollListController.Direction.Horizontal)
              rectTransform.set_anchoredPosition(new Vector2((float) ((double) this.ItemScale * (double) this.Space * (double) idx + (double) this.ItemScale * 0.5) * this.ScrollDir, 0.0f));
            else
              rectTransform.set_anchoredPosition(new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Space * (double) idx + (double) this.ItemScale * 0.5) * this.ScrollDir));
            this.m_ItemPos[idx] = rectTransform.get_anchoredPosition();
            current.OnUpdateItems(idx, ((Component) rectTransform).get_gameObject());
          }
        }
      }
      this.m_PrevPosition = 0.0f;
      this.m_CurrentItemID = 0;
    }

    public void ClearItem()
    {
      if (this.m_ItemList == null)
        return;
      for (int index = 0; index < this.m_ItemList.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ItemList[index], (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) this.m_ItemList[index]);
      }
      this.m_ItemList.Clear();
    }

    public bool MovePos(float goal, float move)
    {
      bool flag1 = false;
      Vector2 anchoredPosition = this.GetRectTransForm.get_anchoredPosition();
      if (this.m_ScrollMode == ScrollListController.Mode.Normal)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @anchoredPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y * -1.0);
      }
      bool flag2 = anchoredPosition.y < (double) goal;
      float num = !flag2 ? -move : move;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @anchoredPosition;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).y = (__Null) ((^local1).y + (double) num);
      if (!flag2 ? anchoredPosition.y < (double) goal : anchoredPosition.y > (double) goal)
      {
        anchoredPosition.y = (__Null) (double) goal;
        flag1 = true;
      }
      this.GetRectTransForm.set_anchoredPosition(anchoredPosition);
      return flag1;
    }

    [Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject>
    {
      public OnItemPositionChange()
      {
        base.\u002Ector();
      }
    }

    [Serializable]
    public class OnAfterStartUpEvent : UnityEvent<bool>
    {
      public OnAfterStartUpEvent()
      {
        base.\u002Ector();
      }
    }

    [Serializable]
    public class OnUpdateEvent : UnityEvent<List<RectTransform>>
    {
      public OnUpdateEvent()
      {
        base.\u002Ector();
      }
    }

    public enum Direction
    {
      Vertical,
      Horizontal,
    }

    public enum Mode
    {
      Normal,
      Reverse,
    }
  }
}
