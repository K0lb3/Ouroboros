// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLevelUpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class UnitLevelUpWindow : MonoBehaviour, IFlowInterface
  {
    public static readonly string CONFIRM_PATH = "UI/UnitLevelUpConfirmWindow";
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private Text CurrentLevel;
    [SerializeField]
    private Text FinishedLevel;
    [SerializeField]
    private Text MaxLevel;
    [SerializeField]
    private Text NextExp;
    [SerializeField]
    private SliderAnimator LevelGauge;
    [SerializeField]
    private Text GetAllExp;
    [SerializeField]
    private Button DecideBtn;
    [SerializeField]
    private Button CancelBtn;
    [SerializeField]
    private Button MaxBtn;
    [SerializeField]
    private SliderAnimator AddLevelGauge;
    private MasterParam master;
    private PlayerData player;
    private UnitData mOriginUnit;
    private int mLv;
    private int mExp;
    private List<GameObject> mItems;
    private List<UnitLevelUpListItem> mUnitLevelupLists;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private Dictionary<string, int> mSelectExpItems;
    public UnitLevelUpWindow.OnUnitLevelupEvent OnDecideEvent;
    private List<ItemData> mCacheExpItemList;

    public UnitLevelUpWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null))
        this.ListItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecideConfirm)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CancelBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MaxBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnMax)));
      }
      this.Init();
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null))
        return;
      this.master = MonoSingleton<GameManager>.Instance.MasterParam;
      if (this.master == null)
        return;
      this.player = MonoSingleton<GameManager>.Instance.Player;
      if (this.player == null)
        return;
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass1 != null)
      {
        int exp = dataOfClass1.GetExp();
        int num = exp + dataOfClass1.GetNextExp();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
          this.CurrentLevel.set_text(dataOfClass1.Lv.ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
          this.FinishedLevel.set_text(this.CurrentLevel.get_text());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxLevel, (UnityEngine.Object) null))
          this.MaxLevel.set_text("/" + dataOfClass1.GetLevelCap(false).ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
          this.NextExp.set_text(dataOfClass1.GetNextExp().ToString());
        if (num <= 0)
          num = 1;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LevelGauge, (UnityEngine.Object) null))
          this.LevelGauge.AnimateValue(Mathf.Clamp01((float) exp / (float) num), 0.0f);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
          this.GetAllExp.set_text("0");
        this.mOriginUnit = dataOfClass1;
        this.mExp = dataOfClass1.Exp;
        this.mLv = dataOfClass1.Lv;
      }
      List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.UNIT_LEVELUP_EXPITEM_CHECKS, string.Empty).Split('|'));
      List<ItemData> itemDataList = this.player.Items;
      UnitEnhanceV3 dataOfClass2 = DataSource.FindDataOfClass<UnitEnhanceV3>(((Component) this).get_gameObject(), (UnitEnhanceV3) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) dataOfClass2, (UnityEngine.Object) null))
        itemDataList = dataOfClass2.TmpExpItems;
      for (int index = 0; index < itemDataList.Count; ++index)
      {
        ItemData data = itemDataList[index];
        if (data != null && data.Param != null && (data.Param.type == EItemType.ExpUpUnit && data.Num > 0))
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItemTemplate);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            DataSource.Bind<ItemData>(gameObject, data);
            gameObject.get_transform().SetParent((Transform) this.ListParent, false);
            UnitLevelUpListItem component = (UnitLevelUpListItem) gameObject.GetComponent<UnitLevelUpListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.OnSelect = new UnitLevelUpListItem.SelectExpItem(this.RefreshExpSelectItems);
              component.ChangeUseMax = new UnitLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
              component.OnCheck = new UnitLevelUpListItem.CheckSliderValue(this.OnCheck);
              this.mUnitLevelupLists.Add(component);
              if (stringList != null && stringList.Count > 0)
                component.SetUseMax(stringList.IndexOf(data.Param.iname) != -1);
              if (component.IsUseMax())
                this.mCacheExpItemList.Add(data);
              gameObject.SetActive(true);
            }
            this.mItems.Add(gameObject);
          }
        }
      }
      if (this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0)
        this.mCacheExpItemList.Sort((Comparison<ItemData>) ((a, b) => (int) b.Param.value - (int) a.Param.value));
      ((Selectable) this.MaxBtn).set_interactable(this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0);
      ((Selectable) this.DecideBtn).set_interactable(this.mSelectExpItems != null && this.mSelectExpItems.Count > 0);
    }

    private void OnDecideConfirm()
    {
      GameObject gameObject1 = AssetManager.Load<GameObject>(UnitLevelUpWindow.CONFIRM_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      UnitLevelUpConfirmWindow componentInChildren = (UnitLevelUpConfirmWindow) gameObject2.GetComponentInChildren<UnitLevelUpConfirmWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.Refresh(this.mSelectExpItems);
      componentInChildren.OnDecideEvent = new UnitLevelUpConfirmWindow.ConfirmDecideEvent(this.OnDecide);
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent != null)
        this.OnDecideEvent(this.mSelectExpItems);
      this.Close();
    }

    private void OnCancel()
    {
      if (this.mSelectExpItems.Count <= 0)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitLevelUpListItem component = (UnitLevelUpListItem) this.mItems[index].GetComponent<UnitLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      this.mSelectExpItems.Clear();
      this.RefreshFinishedStatus();
    }

    private void OnMax()
    {
      if (this.mCacheExpItemList == null || this.mCacheExpItemList.Count < 0)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitLevelUpListItem component = (UnitLevelUpListItem) this.mItems[index].GetComponent<UnitLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      this.CalcCanUnitLevelUpMax();
    }

    private int OnCheck(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num == 0 || this.mSelectExpItems.ContainsKey(iname) && this.mSelectExpItems[iname] > num)
        return -1;
      long num1 = (long) (this.mOriginUnit.GetGainExpCap() - this.mOriginUnit.Exp);
      long num2 = 0;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.mSelectExpItems.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          if (!(current == iname))
          {
            ItemParam itemParam = this.master.GetItemParam(current);
            if (itemParam != null)
              num2 += (long) ((int) itemParam.value * this.mSelectExpItems[current]);
          }
        }
      }
      long num3 = num1 - num2;
      ItemParam itemParam1 = this.master.GetItemParam(iname);
      if (itemParam1 == null || this.player.GetItemAmount(iname) == 0)
        return -1;
      long num4 = (long) ((int) itemParam1.value * num);
      if (num3 < num4)
        return Mathf.CeilToInt((float) num3 / (float) (int) itemParam1.value);
      return num;
    }

    private void RefreshExpSelectItems(string iname, int value)
    {
      if (string.IsNullOrEmpty(iname) || this.player.GetItemAmount(iname) == 0)
        return;
      if (!this.mSelectExpItems.ContainsKey(iname))
        this.mSelectExpItems.Add(iname, value);
      else
        this.mSelectExpItems[iname] = value;
      this.RefreshFinishedStatus();
    }

    private void RefreshFinishedStatus()
    {
      if (this.mSelectExpItems == null || this.mSelectExpItems.Count <= 0)
        return;
      int num1 = 0;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.mSelectExpItems.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ItemData itemDataByItemId = this.player.FindItemDataByItemID(current);
          if (itemDataByItemId != null)
          {
            int mSelectExpItem = this.mSelectExpItems[current];
            if (mSelectExpItem != 0 && mSelectExpItem <= itemDataByItemId.Num)
            {
              int num2 = (int) itemDataByItemId.Param.value * mSelectExpItem;
              num1 += num2;
            }
          }
        }
      }
      int gainExpCap = this.mOriginUnit.GetGainExpCap(this.player.Lv);
      this.mExp = Math.Min(this.mOriginUnit.Exp + num1, gainExpCap);
      this.mLv = this.master.CalcUnitLevel(this.mExp, this.mOriginUnit.GetLevelCap(false));
      using (List<UnitLevelUpListItem>.Enumerator enumerator = this.mUnitLevelupLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetInputLock(this.mExp < gainExpCap);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
      {
        this.FinishedLevel.set_text(this.mLv.ToString());
        if (this.mLv >= this.mOriginUnit.GetLevelCap(false))
          ((Graphic) this.FinishedLevel).set_color(Color.get_red());
        else if (this.mLv > this.mOriginUnit.Lv)
          ((Graphic) this.FinishedLevel).set_color(Color.get_green());
        else
          ((Graphic) this.FinishedLevel).set_color(Color.get_white());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (this.mExp == this.mOriginUnit.GetExp() || num1 == 0)
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        else
          this.AddLevelGauge.AnimateValue(Mathf.Min(1f, Mathf.Clamp01((float) (this.mExp - this.master.GetUnitLevelExp(this.mOriginUnit.Lv)) / (float) this.master.GetUnitNextExp(this.mOriginUnit.Lv))), 0.0f);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
      {
        int num2 = 0;
        if (this.mExp < this.mOriginUnit.GetGainExpCap(this.player.Lv))
        {
          int unitLevelExp = this.master.GetUnitLevelExp(this.mLv);
          int unitNextExp = this.master.GetUnitNextExp(this.mLv);
          if (this.mExp > unitLevelExp)
            unitNextExp = this.master.GetUnitNextExp(Math.Min(this.mOriginUnit.GetLevelCap(false), this.mLv + 1));
          int num3 = this.mExp - unitLevelExp;
          num2 = Math.Max(0, unitNextExp <= num3 ? 0 : unitNextExp - num3);
        }
        this.NextExp.set_text(num2.ToString());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        this.GetAllExp.set_text(num1.ToString());
      ((Selectable) this.DecideBtn).set_interactable(num1 > 0);
    }

    private void CalcCanUnitLevelUpMax()
    {
      if (this.mCacheExpItemList == null)
        return;
      long num1 = 0;
      using (List<ItemData>.Enumerator enumerator = this.mCacheExpItemList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ItemData current = enumerator.Current;
          int num2 = (int) current.Param.value * current.Num;
          num1 += (long) num2;
        }
      }
      long num3 = (long) Mathf.Min((float) (this.mOriginUnit.GetGainExpCap() - this.mExp), (float) num1);
      this.mSelectExpItems.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheExpItemList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ItemData mCacheExpItem1 = this.mCacheExpItemList[index2];
        if (mCacheExpItem1 != null || mCacheExpItem1.Num > 0)
        {
          ItemData mCacheExpItem2 = this.mCacheExpItemList[index1];
          if (index2 == index1 || mCacheExpItem2 != null || mCacheExpItem2.Num > 0)
          {
            if ((long) (int) mCacheExpItem1.Param.value > num3)
            {
              index1 = index2;
            }
            else
            {
              int num2 = (int) (num3 / (long) (int) mCacheExpItem1.Param.value);
              int num4 = Mathf.Min(mCacheExpItem1.Num, num2);
              int num5 = (int) mCacheExpItem1.Param.value * num4;
              int num6 = (int) mCacheExpItem2.Param.value;
              if ((long) Mathf.Abs((float) (num3 - (long) num5)) > (long) Mathf.Abs((float) (num3 - (long) num6)))
              {
                if (this.mSelectExpItems.ContainsKey(mCacheExpItem2.Param.iname))
                {
                  if (mCacheExpItem2.Num - this.mSelectExpItems[mCacheExpItem2.Param.iname] > 0)
                  {
                    Dictionary<string, int> mSelectExpItems;
                    string iname;
                    (mSelectExpItems = this.mSelectExpItems)[iname = mCacheExpItem2.Param.iname] = mSelectExpItems[iname] + 1;
                    num3 = 0L;
                    break;
                  }
                }
                else
                {
                  this.mSelectExpItems.Add(mCacheExpItem2.Param.iname, 1);
                  num3 = 0L;
                  break;
                }
              }
              num3 -= (long) num5;
              this.mSelectExpItems.Add(mCacheExpItem1.Param.iname, num4);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ItemData mCacheExpItem = this.mCacheExpItemList[index1];
        if (mCacheExpItem != null && mCacheExpItem.Num > 0)
        {
          if (this.mSelectExpItems.ContainsKey(mCacheExpItem.Param.iname))
          {
            if (mCacheExpItem.Num - this.mSelectExpItems[mCacheExpItem.Param.iname] > 0)
            {
              Dictionary<string, int> mSelectExpItems;
              string iname;
              (mSelectExpItems = this.mSelectExpItems)[iname = mCacheExpItem.Param.iname] = mSelectExpItems[iname] + 1;
            }
          }
          else
            this.mSelectExpItems.Add(mCacheExpItem.Param.iname, 1);
        }
      }
      if (this.mSelectExpItems.Count > 0)
      {
        for (int index2 = 0; index2 < this.mItems.Count; ++index2)
        {
          GameObject mItem = this.mItems[index2];
          ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(mItem, (ItemData) null);
          if (dataOfClass != null && this.mSelectExpItems.ContainsKey(dataOfClass.Param.iname))
          {
            UnitLevelUpListItem component = (UnitLevelUpListItem) mItem.GetComponent<UnitLevelUpListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetUseExpItemSliderValue(this.mSelectExpItems[dataOfClass.Param.iname]);
          }
        }
      }
      this.RefreshFinishedStatus();
    }

    private void RefreshUseMaxItems(string iname, bool is_on)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitLevelUpWindow.\u003CRefreshUseMaxItems\u003Ec__AnonStorey39D itemsCAnonStorey39D = new UnitLevelUpWindow.\u003CRefreshUseMaxItems\u003Ec__AnonStorey39D();
      if (string.IsNullOrEmpty(iname))
        return;
      // ISSUE: reference to a compiler-generated field
      itemsCAnonStorey39D.item = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
      if (!is_on)
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(itemsCAnonStorey39D.\u003C\u003Em__463)) != -1)
        {
          // ISSUE: reference to a compiler-generated method
          this.mCacheExpItemList.RemoveAt(this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(itemsCAnonStorey39D.\u003C\u003Em__464)));
        }
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mCacheExpItemList.Find(new Predicate<ItemData>(itemsCAnonStorey39D.\u003C\u003Em__465)) == null)
        {
          // ISSUE: reference to a compiler-generated field
          this.mCacheExpItemList.Add(itemsCAnonStorey39D.item);
        }
      }
      this.mCacheExpItemList.Sort((Comparison<ItemData>) ((a, b) => (int) b.Param.value - (int) a.Param.value));
      this.SaveSelectUseMax();
      ((Selectable) this.MaxBtn).set_interactable(this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0);
    }

    private void SaveSelectUseMax()
    {
      string[] strArray = new string[this.mCacheExpItemList.Count];
      for (int index = 0; index < this.mCacheExpItemList.Count; ++index)
        strArray[index] = this.mCacheExpItemList[index].Param.iname;
      string str = strArray == null ? string.Empty : string.Join("|", strArray);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.UNIT_LEVELUP_EXPITEM_CHECKS, str, true);
    }

    private void Close()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    public void Activated(int pinID)
    {
    }

    public delegate void OnUnitLevelupEvent(Dictionary<string, int> dict);
  }
}
