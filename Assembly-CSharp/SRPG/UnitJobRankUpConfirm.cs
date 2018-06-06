// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobRankUpConfirm
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
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
    public SRPG_Button YesButton;
    public Text NoGoldWarningText;
    public UnitJobRankUpConfirm.OnAccept OnAllEquipAccept;
    private UnitData mCurrentUnit;

    public UnitJobRankUpConfirm()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.ListItem, (Object) null) || Object.op_Equality((Object) this.ListTransform, (Object) null))
        return;
      this.ListItem.SetActive(false);
      if (Object.op_Inequality((Object) this.CostText, (Object) null))
        this.CostText.set_text("0");
      this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (this.mCurrentUnit == null)
        return;
      bool flag1 = this.mCurrentUnit.CurrentJob.Rank == 0;
      if (Object.op_Inequality((Object) this.RankMaxEquipAttention, (Object) null))
        this.RankMaxEquipAttention.SetActive(!flag1);
      if (Object.op_Inequality((Object) this.AllEquipConfirm, (Object) null))
      {
        if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
          this.AllEquipConfirm.set_text(LocalizedText.Get("sys.UNIT_ALLEQUIP_CHANGE_CONFIRM"));
        else
          this.AllEquipConfirm.set_text(!flag1 ? LocalizedText.Get("sys.UNIT_ALLEQUIP_CONFIRM") : LocalizedText.Get("sys.UNIT_ALLEQUIP_UNLOCK_CONFIRM"));
      }
      int cost = 0;
      Dictionary<string, int> equips = new Dictionary<string, int>();
      Dictionary<string, int> consumes = new Dictionary<string, int>();
      if (!this.mCurrentUnit.CurrentJob.CanAllEquip(ref cost, ref equips, ref consumes))
        return;
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey285 startCAnonStorey285 = new UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey285();
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = equips.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey285.key = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = items.Find(new Predicate<ItemParam>(startCAnonStorey285.\u003C\u003Em__31F));
          if (itemParam != null)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ListItem);
            gameObject.get_gameObject().SetActive(true);
            gameObject.get_transform().SetParent(this.ListTransform, false);
            Json_Item json = new Json_Item();
            json.iname = itemParam.iname;
            // ISSUE: reference to a compiler-generated field
            json.num = equips[startCAnonStorey285.key];
            ItemData data = new ItemData();
            data.Deserialize(json);
            DataSource.Bind<ItemData>(gameObject, data);
          }
        }
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey286 startCAnonStorey286 = new UnitJobRankUpConfirm.\u003CStart\u003Ec__AnonStorey286();
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = consumes.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey286.key = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = items.Find(new Predicate<ItemParam>(startCAnonStorey286.\u003C\u003Em__320));
          if (itemParam != null)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ListItem);
            gameObject.get_gameObject().SetActive(true);
            gameObject.get_transform().SetParent(this.ListTransform, false);
            Json_Item json = new Json_Item();
            json.iname = itemParam.iname;
            // ISSUE: reference to a compiler-generated field
            json.num = consumes[startCAnonStorey286.key];
            ItemData data = new ItemData();
            data.Deserialize(json);
            DataSource.Bind<ItemData>(gameObject, data);
          }
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag2 = cost > instance.Player.Gold;
      if (Object.op_Inequality((Object) this.YesButton, (Object) null))
        ((Selectable) this.YesButton).set_interactable(!flag2);
      if (Object.op_Inequality((Object) this.NoGoldWarningText, (Object) null))
        ((Component) this.NoGoldWarningText).get_gameObject().SetActive(flag2);
      if (!Object.op_Inequality((Object) this.CostText, (Object) null))
        return;
      this.CostText.set_text(cost.ToString());
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
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), baseJob == null ? (object) string.Empty : (object) baseJob.Name, (object) this.mCurrentUnit.CurrentJob.Name), (UIUtility.DialogResultEvent) (go => this.OnAllEquipAccept()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
        this.OnAllEquipAccept();
    }

    public delegate void OnAccept();
  }
}
