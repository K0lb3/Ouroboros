// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobRankUpConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitJobRankUpConfirm : MonoBehaviour
  {
    public Text AllEquipConfirm;
    public GameObject RankMaxEquipAttention;
    public Text CostText;
    public Transform ListTransform;
    public GameObject ListItem;
    public Transform CommonListTransform;
    public GameObject CommonListItem;
    public SRPG_Button YesButton;
    public Text NoGoldWarningText;
    public UnitJobRankUpConfirm.OnAccept OnAllEquipAccept;
    public UnitJobRankUpConfirm.AllInAccept OnAllInAccept;
    private int target_rank;
    private bool can_jobmaster;
    private bool can_jobmax;
    public UnitJobRankUpConfirm.SetFlag SetCommonFlag;
    private UnitData mCurrentUnit;
    private NeedEquipItemList NeedEquipList;
    public Scrollbar Scroll;
    private bool IsSoul;
    public RectTransform ListRectTranceform;
    public ScrollRect ScrollParent;
    private float DecelerationRate;
    public GameObject JobInfo;
    public Text TargetJobLv;
    public Text MaxJobLv;

    public UnitJobRankUpConfirm()
    {
      base.\u002Ector();
    }

    public bool IsAllIn { get; set; }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListTransform, (UnityEngine.Object) null))
        return;
      this.ListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CostText, (UnityEngine.Object) null))
        this.CostText.set_text("0");
      this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (this.mCurrentUnit == null)
        return;
      bool flag1 = this.mCurrentUnit.CurrentJob.Rank == 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RankMaxEquipAttention, (UnityEngine.Object) null))
        this.RankMaxEquipAttention.SetActive(!flag1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobInfo, (UnityEngine.Object) null))
      {
        this.JobInfo.SetActive(!flag1);
        DataSource.Bind<JobData>(this.JobInfo, this.mCurrentUnit.CurrentJob);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AllEquipConfirm, (UnityEngine.Object) null))
      {
        if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
          this.AllEquipConfirm.set_text(LocalizedText.Get("sys.UNIT_ALLEQUIP_CHANGE_CONFIRM"));
        else
          this.AllEquipConfirm.set_text(!flag1 ? LocalizedText.Get("sys.UNIT_ALLEQUIP_CONFIRM") : LocalizedText.Get("sys.UNIT_ALLEQUIP_UNLOCK_CONFIRM"));
      }
      int cost = 0;
      Dictionary<string, int> equips = new Dictionary<string, int>();
      Dictionary<string, int> consumes = new Dictionary<string, int>();
      this.NeedEquipList = new NeedEquipItemList();
      if (!this.mCurrentUnit.CurrentJob.CanAllEquip(ref cost, ref equips, ref consumes, ref this.target_rank, ref this.can_jobmaster, ref this.can_jobmax, this.NeedEquipList, this.IsAllIn))
        return;
      this.target_rank = Mathf.Min(this.mCurrentUnit.GetJobRankCap(), Mathf.Max(this.target_rank, this.mCurrentUnit.CurrentJob.Rank + 1));
      if (!this.IsAllIn)
        this.can_jobmaster = this.mCurrentUnit.GetJobRankCap() == JobParam.MAX_JOB_RANK && this.target_rank == this.mCurrentUnit.GetJobRankCap() && this.mCurrentUnit.CurrentJob.Rank == this.mCurrentUnit.GetJobRankCap();
      this.SetCommonFlag(this.NeedEquipList.IsEnoughCommon());
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey397 startCAnonStorey397 = new UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey397();
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = equips.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey397.key = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = items.Find(new Predicate<ItemParam>(startCAnonStorey397.\u003C\u003Em__45A));
          if (itemParam != null)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItem);
            gameObject.get_gameObject().SetActive(true);
            gameObject.get_transform().SetParent(this.ListTransform, false);
            // ISSUE: reference to a compiler-generated field
            ItemData itemData = this.CreateItemData(itemParam.iname, equips[startCAnonStorey397.key]);
            DataSource.Bind<ItemData>(gameObject, itemData);
          }
        }
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey398 startCAnonStorey398 = new UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey398();
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = consumes.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey398.key = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = items.Find(new Predicate<ItemParam>(startCAnonStorey398.\u003C\u003Em__45B));
          if (itemParam != null)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItem);
            gameObject.get_gameObject().SetActive(true);
            gameObject.get_transform().SetParent(this.ListTransform, false);
            // ISSUE: reference to a compiler-generated field
            ItemData itemData = this.CreateItemData(itemParam.iname, consumes[startCAnonStorey398.key]);
            DataSource.Bind<ItemData>(gameObject, itemData);
          }
        }
      }
      if (this.NeedEquipList.IsEnoughCommon())
      {
        using (Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator = this.NeedEquipList.CommonNeedNum.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NeedEquipItemDictionary equipItemDictionary = this.NeedEquipList.CommonNeedNum[enumerator.Current];
            ItemParam commonItemParam = equipItemDictionary.CommonItemParam;
            if (commonItemParam != null)
            {
              bool flag2 = true;
              for (int index = 0; index < equipItemDictionary.list.Count; ++index)
              {
                ItemParam itemParam = equipItemDictionary.list[index].Param;
                if (itemParam != null && (int) itemParam.cmn_type - 1 != 2)
                {
                  flag2 = false;
                  GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.CommonListItem);
                  gameObject.get_gameObject().SetActive(true);
                  gameObject.get_transform().SetParent(this.CommonListTransform, false);
                  ItemData data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname) ?? this.CreateItemData(itemParam.iname, 0);
                  ItemData cmmon_data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(commonItemParam.iname) ?? this.CreateItemData(commonItemParam.iname, 0);
                  ((CommonConvertItem) gameObject.GetComponent<CommonConvertItem>()).Bind(data, cmmon_data, equipItemDictionary.list[index].NeedPiece);
                }
              }
              if (flag2)
              {
                this.IsSoul = true;
                GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItem);
                gameObject.get_gameObject().SetActive(true);
                gameObject.get_transform().SetParent(this.ListTransform, false);
                ItemData itemData = this.CreateItemData(commonItemParam.iname, equipItemDictionary.list.Count);
                DataSource.Bind<ItemData>(gameObject, itemData);
              }
            }
          }
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag3 = cost > instance.Player.Gold;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.YesButton, (UnityEngine.Object) null))
        ((Selectable) this.YesButton).set_interactable(!flag3);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoGoldWarningText, (UnityEngine.Object) null))
        ((Component) this.NoGoldWarningText).get_gameObject().SetActive(flag3);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CostText, (UnityEngine.Object) null))
        this.CostText.set_text(cost.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxJobLv, (UnityEngine.Object) null))
        this.MaxJobLv.set_text("/" + this.mCurrentUnit.GetJobRankCap().ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetJobLv, (UnityEngine.Object) null))
        this.TargetJobLv.set_text(this.target_rank.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollParent, (UnityEngine.Object) null))
      {
        this.DecelerationRate = this.ScrollParent.get_decelerationRate();
        this.ScrollParent.set_decelerationRate(0.0f);
      }
      this.ListRectTranceform.set_anchoredPosition(new Vector2((float) this.ListRectTranceform.get_anchoredPosition().x, 0.0f));
      this.StartCoroutine(this.ScrollInit());
    }

    [DebuggerHidden]
    public IEnumerator ScrollInit()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitJobRankUpConfirm.\u003CScrollInit\u003Ec__Iterator139() { \u003C\u003Ef__this = this };
    }

    public ItemData CreateItemData(string iname, int num)
    {
      Json_Item json = new Json_Item();
      json.iname = iname;
      json.num = num;
      ItemData itemData = new ItemData();
      itemData.Deserialize(json);
      return itemData;
    }

    public void OnAllAccept()
    {
      if (this.OnAllEquipAccept == null)
        return;
      if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
      {
        JobData baseJob = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
        if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, baseJob) < 0)
          return;
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), baseJob == null ? (object) string.Empty : (object) baseJob.Name, (object) this.mCurrentUnit.CurrentJob.Name), (UIUtility.DialogResultEvent) (go => this.OnNeedEquip()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.OnNeedEquip();
    }

    public void OnNeedEquip()
    {
      if (this.NeedEquipList.IsEnoughCommon())
        UIUtility.ConfirmBox(LocalizedText.Get(!this.IsSoul ? "sys.COMMON_EQUIP_CHECK" : "sys.COMMON_EQUIP_CHECK_SOUL", new object[1]
        {
          (object) this.NeedEquipList.GetCommonItemListString()
        }), (UIUtility.DialogResultEvent) (go => this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax)), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax);
    }

    public delegate void OnAccept(int target_rank, bool can_jobmaster, bool can_jobmax);

    public delegate void AllInAccept(int current_rank, int target_rank);

    public delegate void SetFlag(bool flag);
  }
}
