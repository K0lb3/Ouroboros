// Decompiled with JetBrains decompiler
// Type: SRPG.JobIconScrollListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class JobIconScrollListController : MonoBehaviour
  {
    private float ITEM_DISTANCE;
    private float SINGLE_ICON_ZERO_MERGIN;
    private float SINGLE_ICON_ONE_MERGIN;
    private float SINGLE_ICON_TWO_MERGIN;
    private float SINGLE_ICON_THREE_MERGIN;
    [SerializeField]
    private GameObject mTemplateItem;
    [Range(0.0f, 30f)]
    [SerializeField]
    protected int m_ItemCnt;
    public JobIconScrollListController.OnItemPositionChange OnItemUpdate;
    public JobIconScrollListController.OnAfterStartUpEvent OnAfterStartup;
    public JobIconScrollListController.OnUpdateEvent OnUpdateItemEvent;
    public JobIconScrollListController.OnItemPositionAreaOverEvent OnItemPositionAreaOver;
    public JobIconScrollListController.Direction m_Direction;
    public JobIconScrollListController.Mode m_ScrollMode;
    private RectTransform m_RectTransform;
    private List<JobIconScrollListController.ItemData> mItems;
    private Rect mViewArea;
    private float mPreAnchoredPositionX;
    private bool IsInitialized;
    [SerializeField]
    private RectTransform mViewPort;

    public JobIconScrollListController()
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
        if (this.m_ScrollMode == JobIconScrollListController.Mode.Normal)
        {
          if (this.m_Direction == JobIconScrollListController.Direction.Vertical)
            return (float) -this.GetRectTransForm.get_anchoredPosition().y;
          return (float) this.GetRectTransForm.get_anchoredPosition().x;
        }
        if (this.m_Direction == JobIconScrollListController.Direction.Vertical)
          return (float) this.GetRectTransForm.get_anchoredPosition().y;
        return (float) this.GetRectTransForm.get_anchoredPosition().x;
      }
    }

    public float ScrollDir
    {
      get
      {
        return this.m_ScrollMode == JobIconScrollListController.Mode.Normal ? -1f : 1f;
      }
    }

    public List<JobIconScrollListController.ItemData> Items
    {
      get
      {
        return this.mItems;
      }
    }

    private void Start()
    {
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_content(this.GetRectTransForm);
    }

    public void Init()
    {
      if (this.OnAfterStartup == null)
        return;
      this.OnAfterStartup.Invoke(true);
    }

    public void CreateInstance()
    {
      this.mItems = new List<JobIconScrollListController.ItemData>();
      this.mTemplateItem.SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mTemplateItem);
        gameObject.get_transform().SetParent(((Component) this).get_transform(), false);
        gameObject.SetActive(true);
        this.mItems.Add(new JobIconScrollListController.ItemData(gameObject));
      }
      using (List<ScrollListSetUp>.Enumerator enumerator = ((IEnumerable<MonoBehaviour>) ((Component) this).GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          current.OnSetUpItems();
          for (int idx = 0; idx < this.m_ItemCnt; ++idx)
            current.OnUpdateItems(idx, this.mItems[idx].gameObject);
        }
      }
    }

    public void Repotision()
    {
      this.GetRectTransForm.set_anchoredPosition(Vector2.get_zero());
      this.mPreAnchoredPositionX = this.AnchoredPosition;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitInventoryJobIcon jobIcon = this.mItems[index].job_icon;
        float num4;
        if (index <= 0)
        {
          num4 = num3 + jobIcon.HalfWidth;
          num1 = jobIcon.HalfWidth;
        }
        else
        {
          num4 = num3 + (jobIcon.HalfWidth + this.ITEM_DISTANCE);
          num2 = jobIcon.HalfWidth;
        }
        this.mItems[index].rectTransform.set_anchoredPosition(new Vector2(num4 * this.ScrollDir, 0.0f));
        this.mItems[index].position = this.mItems[index].rectTransform.get_anchoredPosition();
        num3 = num4 + jobIcon.HalfWidth;
      }
      this.mViewArea = new Rect((float) this.mItems[0].rectTransform.get_anchoredPosition().x - num1, 0.0f, (float) this.mItems[this.mItems.Count - 1].rectTransform.get_anchoredPosition().x + num2, 0.0f);
      int num5 = 0;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (!stringList.Contains(((UnityEngine.Object) this.mItems[index].job_icon.BaseJobIconButton).get_name()))
        {
          stringList.Add(((UnityEngine.Object) this.mItems[index].job_icon.BaseJobIconButton).get_name());
          if (this.mItems[index].job_icon.IsSingleIcon)
            ++num5;
        }
      }
      float num6 = this.SINGLE_ICON_ZERO_MERGIN;
      if (num5 == 1)
        num6 = this.SINGLE_ICON_ONE_MERGIN;
      if (num5 == 2)
        num6 = this.SINGLE_ICON_TWO_MERGIN;
      if (num5 >= 3)
        num6 = this.SINGLE_ICON_THREE_MERGIN;
      this.mViewPort.set_offsetMin(new Vector2(num6, 0.0f));
      this.mViewPort.set_offsetMax(new Vector2(-num6, 0.0f));
      this.IsInitialized = true;
    }

    private bool CheckRightAreaOut(JobIconScrollListController.ItemData item)
    {
      // ISSUE: explicit reference operation
      return (double) ((Rect) @this.mViewArea).get_width() < (((Component) this).get_transform() as RectTransform).get_anchoredPosition().x + item.position.x + (double) item.job_icon.HalfWidth;
    }

    private bool CheckLeftAreaOut(JobIconScrollListController.ItemData item)
    {
      // ISSUE: explicit reference operation
      return (double) ((Rect) @this.mViewArea).get_x() > (((Component) this).get_transform() as RectTransform).get_anchoredPosition().x + item.position.x + (double) item.job_icon.HalfWidth;
    }

    private void Update()
    {
      if (!this.IsInitialized)
        return;
      switch (this.m_ScrollMode)
      {
        case JobIconScrollListController.Mode.Normal:
          this.UpdateModeNormal();
          break;
        case JobIconScrollListController.Mode.Reverse:
          this.UpdateModeReverse();
          break;
      }
    }

    private void UpdateModeNormal()
    {
      if ((double) this.mPreAnchoredPositionX != (double) this.AnchoredPosition)
      {
        this.UpdateItemsPositionNormal((double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX > 0.0, (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX < 0.0);
        this.mPreAnchoredPositionX = this.AnchoredPosition;
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.mItems);
    }

    private void UpdateModeReverse()
    {
      if ((double) this.mPreAnchoredPositionX != (double) this.AnchoredPosition)
      {
        bool is_move_right = (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX > 0.0;
        bool is_move_left = (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX < 0.0;
        this.mPreAnchoredPositionX = this.AnchoredPosition;
        this.UpdateItemsPositionReverse(is_move_right, is_move_left);
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.mItems);
    }

    private void UpdateItemsPositionReverse(bool is_move_right, bool is_move_left)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitInventoryJobIcon jobIcon1 = this.mItems[index].job_icon;
        UnitInventoryJobIcon jobIcon2 = this.mItems[0].job_icon;
        UnitInventoryJobIcon jobIcon3 = this.mItems[this.mItems.Count - 1].job_icon;
        if (is_move_right && this.CheckRightAreaOut(this.mItems[index]))
        {
          this.OnItemUpdate.Invoke(int.Parse(((UnityEngine.Object) this.mItems[0].gameObject).get_name()) - 1, this.mItems[index].gameObject);
          float num = (float) this.mItems[0].position.x - jobIcon2.HalfWidth - this.ITEM_DISTANCE - jobIcon1.HalfWidth;
          this.mItems[index].position = new Vector2(num, (float) this.mItems[index].position.y);
          JobIconScrollListController.ItemData mItem = this.mItems[index];
          this.mItems.RemoveAt(index);
          this.mItems.Insert(0, mItem);
          index = -1;
        }
        else if (is_move_left && this.CheckLeftAreaOut(this.mItems[index]))
        {
          this.OnItemUpdate.Invoke(int.Parse(((UnityEngine.Object) this.mItems[this.mItems.Count - 1].gameObject).get_name()) + 1, this.mItems[index].gameObject);
          float num = (float) this.mItems[this.mItems.Count - 1].position.x + jobIcon3.HalfWidth + this.ITEM_DISTANCE + jobIcon1.HalfWidth;
          this.mItems[index].position = new Vector2(num, (float) this.mItems[index].position.y);
          JobIconScrollListController.ItemData mItem = this.mItems[index];
          this.mItems.RemoveAt(index);
          this.mItems.Add(mItem);
          index = -1;
        }
      }
    }

    private void UpdateItemsPositionNormal(bool is_move_right, bool is_move_left)
    {
      RectTransform transform = ((Component) this).get_transform() as RectTransform;
      if (is_move_right)
      {
        for (int index = this.mItems.Count - 1; index >= 0; --index)
        {
          if (this.mItems[index].gameObject.get_activeSelf())
          {
            float num1 = (float) (this.mItems[index].rectTransform.get_sizeDelta().x * ((Transform) this.mItems[index].rectTransform).get_localScale().x * 0.5);
            Rect rect1 = this.mViewPort.get_rect();
            // ISSUE: explicit reference operation
            if ((double) ((Rect) @rect1).get_width() < transform.get_anchoredPosition().x + this.mItems[index].gameObject.get_transform().get_localPosition().x + (double) num1)
            {
              Rect rect2 = this.mViewPort.get_rect();
              // ISSUE: explicit reference operation
              float num2 = ((Rect) @rect2).get_width() - ((float) (transform.get_anchoredPosition().x + this.mItems[index].gameObject.get_transform().get_localPosition().x) + num1);
              float num3 = (float) transform.get_anchoredPosition().x + num2;
              transform.set_anchoredPosition(new Vector2(num3, (float) transform.get_anchoredPosition().y));
              if (this.OnItemPositionAreaOver != null)
              {
                this.OnItemPositionAreaOver.Invoke(this.mItems[index].gameObject);
                break;
              }
              break;
            }
          }
        }
      }
      if (!is_move_left)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (this.mItems[index].gameObject.get_activeSelf())
        {
          float num1 = (float) (this.mItems[index].rectTransform.get_sizeDelta().x * ((Transform) this.mItems[index].rectTransform).get_localScale().x * 0.5);
          if (0.0 > transform.get_anchoredPosition().x + this.mItems[index].gameObject.get_transform().get_localPosition().x - (double) num1)
          {
            float num2 = (float) (0.0 - (transform.get_anchoredPosition().x + this.mItems[index].gameObject.get_transform().get_localPosition().x - (double) num1));
            float num3 = (float) transform.get_anchoredPosition().x + num2;
            transform.set_anchoredPosition(new Vector2(num3, (float) transform.get_anchoredPosition().y));
            if (this.OnItemPositionAreaOver == null)
              break;
            this.OnItemPositionAreaOver.Invoke(this.mItems[index].gameObject);
            break;
          }
        }
      }
    }

    public void Step()
    {
      this.Update();
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
    public class OnUpdateEvent : UnityEvent<List<JobIconScrollListController.ItemData>>
    {
      public OnUpdateEvent()
      {
        base.\u002Ector();
      }
    }

    [Serializable]
    public class OnItemPositionAreaOverEvent : UnityEvent<GameObject>
    {
      public OnItemPositionAreaOverEvent()
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

    public class ItemData
    {
      public GameObject gameObject;
      public RectTransform rectTransform;
      public Vector2 position;
      public UnitInventoryJobIcon job_icon;

      public ItemData(GameObject obj)
      {
        this.gameObject = obj;
        this.rectTransform = obj.get_transform() as RectTransform;
        this.position = this.rectTransform.get_anchoredPosition();
        this.job_icon = (UnitInventoryJobIcon) obj.GetComponent<UnitInventoryJobIcon>();
      }
    }
  }
}
