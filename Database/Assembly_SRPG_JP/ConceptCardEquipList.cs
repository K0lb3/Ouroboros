// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardEquipList : UIBehaviour
  {
    private bool mIsInitialized;
    private bool mIsChangeRectSize;
    private bool mIsKeepCurrentPage;
    private RectTransform mRectTransform;
    private GridLayoutGroup mGrid;
    private List<ConceptCardData> mCardDatas;
    private List<ConceptCardIcon> mCardIcons;
    private int mFirstCardIndex;
    private int mCurrentPage;
    private int mLastPage;
    private ConceptCardData mSelectedConceptCardData;
    private ConceptCardData mReservedSelectConceptCardData;
    [SerializeField]
    private GameObject mNoEquipButtonObject;
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private Text mCardEmptyMessageText;

    public ConceptCardEquipList()
    {
      base.\u002Ector();
    }

    public int CurrentPage
    {
      get
      {
        return this.mCurrentPage;
      }
    }

    public int LastPage
    {
      get
      {
        return this.mLastPage;
      }
    }

    private RectTransform RT
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRectTransform, (UnityEngine.Object) null))
          this.mRectTransform = (RectTransform) ((Component) this).get_transform();
        return this.mRectTransform;
      }
    }

    private GridLayoutGroup Grid
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGrid, (UnityEngine.Object) null))
          this.mGrid = (GridLayoutGroup) ((Component) this).GetComponent<GridLayoutGroup>();
        return this.mGrid;
      }
    }

    public ConceptCardData SelectedConceptCardData
    {
      get
      {
        return this.mSelectedConceptCardData;
      }
    }

    public bool IsIgnoreEquipedConceptCard
    {
      get
      {
        return PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, 0) != 0;
      }
      set
      {
        int num = !value ? 0 : 1;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, num, false);
      }
    }

    private void LateUpdate()
    {
      if (!this.mIsInitialized || !this.mIsChangeRectSize)
        return;
      this.mLastPage = this.CalcLastPage();
      int num = -1;
      if (this.mIsKeepCurrentPage)
      {
        this.mCurrentPage = Mathf.Min(this.mCurrentPage, this.LastPage);
      }
      else
      {
        num = this.GetCardPage(this.mReservedSelectConceptCardData);
        if (num >= 0)
          this.mCurrentPage = num;
      }
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.mIsChangeRectSize = false;
      if (num < 0)
        return;
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card =>
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) card, (UnityEngine.Object) null) && card.ConceptCard != null)
          return (long) card.ConceptCard.UniqueID == (long) this.mReservedSelectConceptCardData.UniqueID;
        return false;
      }));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
        this.SelectCardIcon(selected_icon);
      this.mReservedSelectConceptCardData = (ConceptCardData) null;
    }

    public void HideTemplateObject()
    {
      this.mCardObjectTemplate.SetActive(false);
      this.mNoEquipButtonObject.SetActive(false);
    }

    public void Init(List<ConceptCardData> card_datas, UnitData selected_unit, bool is_keep_page = false)
    {
      bool equipedConceptCard = this.IsIgnoreEquipedConceptCard;
      List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>();
      for (int i = 0; i < card_datas.Count; ++i)
      {
        UnitData unitData = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u =>
        {
          if (u.ConceptCard != null)
            return (long) u.ConceptCard.UniqueID == (long) card_datas[i].UniqueID;
          return false;
        }));
        if (!equipedConceptCard || unitData == null || unitData.UniqueID == selected_unit.UniqueID)
          conceptCardDataList.Add(card_datas[i]);
      }
      this.mIsChangeRectSize = true;
      this.mIsKeepCurrentPage = is_keep_page;
      ((Component) this.mCardEmptyMessageText).get_gameObject().SetActive(conceptCardDataList.Count <= 0);
      this.mCardDatas.Clear();
      this.mCardDatas.Add((ConceptCardData) null);
      this.mCardDatas.AddRange((IEnumerable<ConceptCardData>) conceptCardDataList);
      this.Sort(ConceptCardListSortWindow.LoadDataType(), ConceptCardListSortWindow.LoadDataOrderType());
      this.mCurrentPage = !is_keep_page ? 0 : this.mCurrentPage;
      this.mIsInitialized = true;
    }

    public void PageNext()
    {
      if (this.mCurrentPage >= this.mLastPage)
        return;
      ++this.mCurrentPage;
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    public void PageBack()
    {
      if (this.mCurrentPage <= 0)
        return;
      --this.mCurrentPage;
      this.CreateIcon();
      this.RefreshNoEquipButtonObject();
      this.RefreshFirstIconIndex();
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    private void CreateIcon()
    {
      int num1 = this.CellCount;
      while (this.mCardIcons.Count < num1)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mCardObjectTemplate);
        gameObject.get_transform().SetParent((Transform) this.mCardObjectParent, false);
        gameObject.SetActive(true);
        this.mCardIcons.Add((ConceptCardIcon) gameObject.GetComponent<ConceptCardIcon>());
      }
      if (this.mCurrentPage == 0)
        num1 = Mathf.Max(0, num1 - 1);
      if (this.mCardIcons.Count > num1)
      {
        int num2 = this.mCardIcons.Count - num1;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          int index2 = this.mCardIcons.Count - 1 - index1;
          if (this.mCardIcons.Count > index2 && index2 >= 0)
            ((Component) this.mCardIcons[index2]).get_gameObject().SetActive(false);
        }
      }
      int activeIconCount = this.GetActiveIconCount();
      if (activeIconCount >= num1)
        return;
      int num3 = num1 - activeIconCount;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (!((Component) this.mCardIcons[index]).get_gameObject().get_activeSelf())
        {
          ((Component) this.mCardIcons[index]).get_gameObject().SetActive(true);
          --num3;
          if (num3 <= 0)
            break;
        }
      }
    }

    private void RefreshIcon()
    {
      int activeIconCount = this.GetActiveIconCount();
      for (int index1 = 0; index1 < activeIconCount; ++index1)
      {
        int index2 = this.mFirstCardIndex + index1;
        if (index2 >= this.mCardDatas.Count)
        {
          this.mCardIcons[index1].ResetIcon();
          ((Component) this.mCardIcons[index1]).get_gameObject().SetActive(false);
        }
        else
          this.mCardIcons[index1].Setup(this.mCardDatas[index2]);
      }
    }

    public void Sort(ConceptCardListSortWindow.Type SortType, ConceptCardListSortWindow.Type SortOrderType)
    {
      bool flag = false;
      for (int index = 0; index < this.mCardDatas.Count; ++index)
      {
        if (this.mCardDatas[index] == null)
          flag = true;
      }
      ConceptCardListSortWindow.Sort(SortType, SortOrderType, this.mCardDatas);
      if (flag)
        this.mCardDatas.Insert(0, (ConceptCardData) null);
      this.RefreshIcon();
      this.ResetSelectCardAnimation();
      this.SelectedCardIconActive();
    }

    private int CalcLastPage()
    {
      int cellCount = this.CellCount;
      if (cellCount == 0)
        return 0;
      int num = this.mCardDatas.Count % cellCount != 0 ? 1 : 0;
      return Mathf.Max(this.mCardDatas.Count / cellCount + num - 1, 0);
    }

    private void RefreshFirstIconIndex()
    {
      this.mFirstCardIndex = this.mCurrentPage * this.CellCount;
      if (this.mCurrentPage > 0)
        return;
      this.mFirstCardIndex = 1;
    }

    private void RefreshNoEquipButtonObject()
    {
      this.mNoEquipButtonObject.SetActive(this.mCurrentPage == 0);
    }

    public void SelectCardIcon(ConceptCardIcon selected_icon)
    {
      this.ResetSelectCardAnimation();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
      {
        this.mSelectedConceptCardData = (ConceptCardData) null;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon((ConceptCardIcon) null);
      }
      else
      {
        this.mSelectedConceptCardData = selected_icon.ConceptCard;
        ConceptCardEquipWindow.Instance.SetSelectedCardIcon(selected_icon);
        Animator component = (Animator) ((Component) selected_icon).GetComponent<Animator>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.SetInteger("st", 1);
      }
    }

    private void ResetSelectCardAnimation()
    {
      int activeIconCount = this.GetActiveIconCount();
      for (int index = 0; index < activeIconCount; ++index)
      {
        Animator component = (Animator) ((Component) this.mCardIcons[index]).GetComponent<Animator>();
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetInteger("st", 0);
      }
    }

    private void SelectedCardIconActive()
    {
      if (this.mSelectedConceptCardData == null)
        return;
      ConceptCardIcon selected_icon = this.mCardIcons.Find((Predicate<ConceptCardIcon>) (card =>
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) card, (UnityEngine.Object) null) && card.ConceptCard != null)
          return (long) card.ConceptCard.UniqueID == (long) this.mSelectedConceptCardData.UniqueID;
        return false;
      }));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) selected_icon, (UnityEngine.Object) null))
        return;
      this.SelectCardIcon(selected_icon);
    }

    private int GetCardPage(ConceptCardData target)
    {
      if (target == null)
        return -1;
      int index1 = this.mCardDatas.FindIndex((Predicate<ConceptCardData>) (data =>
      {
        if (data != null)
          return (long) data.UniqueID == (long) target.UniqueID;
        return false;
      }));
      if (index1 <= -1)
        return -1;
      int num = -1;
      int mCurrentPage = this.mCurrentPage;
      this.mCurrentPage = 0;
      for (int index2 = 0; index2 <= this.LastPage; ++index2)
      {
        if (index1 < this.mFirstCardIndex + this.CellCount)
        {
          num = Mathf.Min(this.mCurrentPage, this.LastPage);
          break;
        }
        ++this.mCurrentPage;
        this.RefreshFirstIconIndex();
      }
      this.mCurrentPage = mCurrentPage;
      this.RefreshFirstIconIndex();
      return num;
    }

    public void OpenSelectIconExistPage(ConceptCardData card)
    {
      if (card == null)
        return;
      this.mReservedSelectConceptCardData = card;
    }

    private int GetActiveIconCount()
    {
      int num = 0;
      for (int index = 0; index < this.mCardIcons.Count; ++index)
      {
        if (((Component) this.mCardIcons[index]).get_gameObject().get_activeSelf())
          ++num;
      }
      return num;
    }

    private int CellCount
    {
      get
      {
        int num1 = 64;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Grid, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("ERROR!! : Dont Setup GridLayoutGroup");
          return 0;
        }
        float x1 = (float) this.Grid.get_cellSize().x;
        float y1 = (float) this.Grid.get_cellSize().y;
        float x2 = (float) this.Grid.get_spacing().x;
        float y2 = (float) this.Grid.get_spacing().y;
        float horizontal = (float) ((LayoutGroup) this.Grid).get_padding().get_horizontal();
        float vertical = (float) ((LayoutGroup) this.Grid).get_padding().get_vertical();
        Rect rect1 = this.RT.get_rect();
        float num2 = ((Rect) @rect1).get_width() - horizontal + x2;
        Rect rect2 = this.RT.get_rect();
        float num3 = ((Rect) @rect2).get_height() - vertical + y2;
        return Mathf.Clamp(Mathf.FloorToInt(num2 / (x1 + x2)) * Mathf.FloorToInt(num3 / (y1 + y2)), 0, num1);
      }
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.mIsChangeRectSize = true;
    }
  }
}
