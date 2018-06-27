// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpWindow
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
  public class ArtifactLevelUpWindow : MonoBehaviour, IFlowInterface
  {
    public static readonly string CONFIRM_PATH = "UI/ArtifactLevelUpConfirmWindow";
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
    private ArtifactData mOriginArtifact;
    private List<GameObject> mItems;
    private List<ArtifactLevelUpListItem> mArtifactLevelupLists;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private Dictionary<string, int> mSelectExpItems;
    public ArtifactLevelUpWindow.OnArtifactLevelupEvent OnDecideEvent;
    private List<ItemData> mCacheExpItemList;

    public ArtifactLevelUpWindow()
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
      ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      if (dataOfClass1 != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
          this.CurrentLevel.set_text(dataOfClass1.Lv.ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
          this.FinishedLevel.set_text(this.CurrentLevel.get_text());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxLevel, (UnityEngine.Object) null))
          this.MaxLevel.set_text("/" + dataOfClass1.GetLevelCap().ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
          this.NextExp.set_text(dataOfClass1.GetNextExp().ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LevelGauge, (UnityEngine.Object) null))
        {
          int totalExpFromLevel = dataOfClass1.GetTotalExpFromLevel((int) dataOfClass1.Lv);
          float num1 = (float) (dataOfClass1.GetTotalExpFromLevel((int) dataOfClass1.Lv + 1) - totalExpFromLevel);
          float num2 = (float) (dataOfClass1.Exp - totalExpFromLevel);
          if ((double) num1 <= 0.0)
            num1 = 1f;
          this.LevelGauge.AnimateValue(Mathf.Clamp01(num2 / num1), 0.0f);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
          this.GetAllExp.set_text("0");
        this.mOriginArtifact = dataOfClass1;
      }
      List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, string.Empty).Split('|'));
      List<ItemData> itemDataList = this.player.Items;
      ArtifactWindow dataOfClass2 = DataSource.FindDataOfClass<ArtifactWindow>(((Component) this).get_gameObject(), (ArtifactWindow) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) dataOfClass2, (UnityEngine.Object) null))
        itemDataList = dataOfClass2.TmpItems;
      for (int index = 0; index < itemDataList.Count; ++index)
      {
        ItemData data = itemDataList[index];
        if (data != null && data.Param != null && (data.Param.type == EItemType.ExpUpArtifact && data.Num > 0))
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItemTemplate);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            DataSource.Bind<ItemData>(gameObject, data);
            gameObject.get_transform().SetParent((Transform) this.ListParent, false);
            ArtifactLevelUpListItem component = (ArtifactLevelUpListItem) gameObject.GetComponent<ArtifactLevelUpListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.OnSelect = new ArtifactLevelUpListItem.SelectExpItem(this.RefreshExpSelectItems);
              component.ChangeUseMax = new ArtifactLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
              component.OnCheck = new ArtifactLevelUpListItem.CheckSliderValue(this.OnCheck);
              this.mArtifactLevelupLists.Add(component);
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
      GameObject gameObject1 = AssetManager.Load<GameObject>(ArtifactLevelUpWindow.CONFIRM_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      ArtifactLevelUpConfirmWindow componentInChildren = (ArtifactLevelUpConfirmWindow) gameObject2.GetComponentInChildren<ArtifactLevelUpConfirmWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.Refresh(this.mSelectExpItems);
      componentInChildren.OnDecideEvent = new ArtifactLevelUpConfirmWindow.ConfirmDecideEvent(this.OnDecide);
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
        ArtifactLevelUpListItem component = (ArtifactLevelUpListItem) this.mItems[index].GetComponent<ArtifactLevelUpListItem>();
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
        ArtifactLevelUpListItem component = (ArtifactLevelUpListItem) this.mItems[index].GetComponent<ArtifactLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      this.CalcCanArtifactLevelUpMax();
    }

    private int OnCheck(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num == 0 || this.mSelectExpItems.ContainsKey(iname) && this.mSelectExpItems[iname] > num)
        return -1;
      long num1 = (long) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp);
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
      int exp1 = 0;
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
              int num = (int) itemDataByItemId.Param.value * mSelectExpItem;
              exp1 += num;
            }
          }
        }
      }
      int gainExpCap = this.mOriginArtifact.GetGainExpCap();
      int num1 = Math.Min(this.mOriginArtifact.Exp + exp1, gainExpCap);
      ArtifactData artifactData = this.mOriginArtifact.Copy();
      artifactData.GainExp(exp1);
      using (List<ArtifactLevelUpListItem>.Enumerator enumerator = this.mArtifactLevelupLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetInputLock(num1 < gainExpCap);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
      {
        this.FinishedLevel.set_text(artifactData.Lv.ToString());
        if ((int) artifactData.Lv >= this.mOriginArtifact.GetLevelCap())
          ((Graphic) this.FinishedLevel).set_color(Color.get_red());
        else if ((int) artifactData.Lv > (int) this.mOriginArtifact.Lv)
          ((Graphic) this.FinishedLevel).set_color(Color.get_green());
        else
          ((Graphic) this.FinishedLevel).set_color(Color.get_white());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (num1 == this.mOriginArtifact.Exp || exp1 == 0)
        {
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        }
        else
        {
          int totalExpFromLevel = this.mOriginArtifact.GetTotalExpFromLevel((int) this.mOriginArtifact.Lv);
          float num2 = (float) (this.mOriginArtifact.GetTotalExpFromLevel((int) this.mOriginArtifact.Lv + 1) - totalExpFromLevel);
          float num3 = (float) (num1 - totalExpFromLevel);
          if ((double) num2 <= 0.0)
            num2 = 1f;
          this.AddLevelGauge.AnimateValue(Mathf.Clamp01(num3 / num2), 0.0f);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
      {
        int num2 = 0;
        if (num1 < this.mOriginArtifact.GetGainExpCap())
        {
          int exp2 = artifactData.Exp;
          int nextExp = artifactData.GetNextExp();
          int num3 = num1 - exp2;
          num2 = Math.Max(0, nextExp <= num3 ? 0 : nextExp - num3);
        }
        this.NextExp.set_text(num2.ToString());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        this.GetAllExp.set_text(exp1.ToString());
      ((Selectable) this.DecideBtn).set_interactable(exp1 > 0);
    }

    private void CalcCanArtifactLevelUpMax()
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
      long num3 = (long) Mathf.Min((float) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp), (float) num1);
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
              num3 -= (long) num5;
              this.mSelectExpItems.Add(mCacheExpItem1.Param.iname, num4);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ItemData mCacheExpItem1 = this.mCacheExpItemList[index1];
        if (mCacheExpItem1 != null && mCacheExpItem1.Num > 0)
        {
          if (this.mSelectExpItems.ContainsKey(mCacheExpItem1.Param.iname))
          {
            if (mCacheExpItem1.Num - this.mSelectExpItems[mCacheExpItem1.Param.iname] > 0)
            {
              Dictionary<string, int> mSelectExpItems;
              string iname;
              (mSelectExpItems = this.mSelectExpItems)[iname = mCacheExpItem1.Param.iname] = mSelectExpItems[iname] + 1;
            }
            else
            {
              for (int index2 = this.mCacheExpItemList.Count - 2; index2 >= 0; --index2)
              {
                ItemData mCacheExpItem2 = this.mCacheExpItemList[index2];
                bool flag = this.mSelectExpItems.ContainsKey(mCacheExpItem2.ItemID);
                int num2 = !flag ? 0 : this.mSelectExpItems[mCacheExpItem2.ItemID];
                if (mCacheExpItem2.Num - (num2 + 1) > 0)
                {
                  if (flag)
                    this.mSelectExpItems[mCacheExpItem2.ItemID] = num2 + 1;
                  else
                    this.mSelectExpItems.Add(mCacheExpItem2.ItemID, 1);
                  this.mSelectExpItems.Remove(this.mCacheExpItemList[this.mCacheExpItemList.Count - 1].ItemID);
                  break;
                }
              }
            }
          }
          else
            this.mSelectExpItems.Add(mCacheExpItem1.Param.iname, 1);
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
            ArtifactLevelUpListItem component = (ArtifactLevelUpListItem) mItem.GetComponent<ArtifactLevelUpListItem>();
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
      ArtifactLevelUpWindow.\u003CRefreshUseMaxItems\u003Ec__AnonStorey2F7 itemsCAnonStorey2F7 = new ArtifactLevelUpWindow.\u003CRefreshUseMaxItems\u003Ec__AnonStorey2F7();
      if (string.IsNullOrEmpty(iname))
        return;
      // ISSUE: reference to a compiler-generated field
      itemsCAnonStorey2F7.item = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
      if (!is_on)
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(itemsCAnonStorey2F7.\u003C\u003Em__2F9)) != -1)
        {
          // ISSUE: reference to a compiler-generated method
          this.mCacheExpItemList.RemoveAt(this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(itemsCAnonStorey2F7.\u003C\u003Em__2FA)));
        }
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mCacheExpItemList.Find(new Predicate<ItemData>(itemsCAnonStorey2F7.\u003C\u003Em__2FB)) == null)
        {
          // ISSUE: reference to a compiler-generated field
          this.mCacheExpItemList.Add(itemsCAnonStorey2F7.item);
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
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, str, true);
    }

    private void Close()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    public void Activated(int pinID)
    {
    }

    public delegate void OnArtifactLevelupEvent(Dictionary<string, int> dict);
  }
}
