namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

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
        public OnAccept OnAllEquipAccept;
        public AllInAccept OnAllInAccept;
        private int target_rank;
        private bool can_jobmaster;
        private bool can_jobmax;
        public SetFlag SetCommonFlag;
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
        [CompilerGenerated]
        private bool <IsAllIn>k__BackingField;

        public UnitJobRankUpConfirm()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnAllAccept>m__466(GameObject go)
        {
            this.OnNeedEquip();
            return;
        }

        [CompilerGenerated]
        private void <OnNeedEquip>m__467(GameObject go)
        {
            this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax);
            return;
        }

        public ItemData CreateItemData(string iname, int num)
        {
            Json_Item item;
            ItemData data;
            item = new Json_Item();
            item.iname = iname;
            item.num = num;
            data = new ItemData();
            data.Deserialize(item);
            return data;
        }

        public void OnAllAccept()
        {
            JobData data;
            int num;
            string str;
            string str2;
            if (this.OnAllEquipAccept == null)
            {
                goto Label_00B8;
            }
            if (this.mCurrentUnit.JobIndex < this.mCurrentUnit.NumJobsAvailable)
            {
                goto Label_00B2;
            }
            data = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
            if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, data) >= 0)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), (data == null) ? string.Empty : data.Name, this.mCurrentUnit.CurrentJob.Name), new UIUtility.DialogResultEvent(this.<OnAllAccept>m__466), null, null, 0, -1, null, null);
            goto Label_00B8;
        Label_00B2:
            this.OnNeedEquip();
        Label_00B8:
            return;
        }

        public void OnNeedEquip()
        {
            object[] objArray1;
            string str;
            string str2;
            if (this.NeedEquipList.IsEnoughCommon() == null)
            {
                goto Label_0064;
            }
            str = this.NeedEquipList.GetCommonItemListString();
            objArray1 = new object[] { str };
            UIUtility.ConfirmBox(LocalizedText.Get((this.IsSoul == null) ? "sys.COMMON_EQUIP_CHECK" : "sys.COMMON_EQUIP_CHECK_SOUL", objArray1), new UIUtility.DialogResultEvent(this.<OnNeedEquip>m__467), null, null, 0, -1, null, null);
            goto Label_0081;
        Label_0064:
            this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax);
        Label_0081:
            return;
        }

        [DebuggerHidden]
        public IEnumerator ScrollInit()
        {
            <ScrollInit>c__Iterator16A iteratora;
            iteratora = new <ScrollInit>c__Iterator16A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private unsafe void Start()
        {
            bool flag;
            int num;
            Dictionary<string, int> dictionary;
            Dictionary<string, int> dictionary2;
            List<ItemParam> list;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ItemParam param;
            GameObject obj2;
            ItemData data;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator2;
            ItemParam param2;
            GameObject obj3;
            ItemData data2;
            byte num2;
            Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator3;
            NeedEquipItemDictionary dictionary3;
            ItemParam param3;
            bool flag2;
            int num3;
            ItemParam param4;
            GameObject obj4;
            ItemData data3;
            ItemData data4;
            CommonConvertItem item;
            GameObject obj5;
            ItemData data5;
            GameManager manager;
            bool flag3;
            <Start>c__AnonStorey3CC storeycc;
            <Start>c__AnonStorey3CD storeycd;
            int num4;
            Vector2 vector;
            if (((this.ListItem == null) == null) && ((this.ListTransform == null) == null))
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            this.ListItem.SetActive(0);
            if ((this.CostText != null) == null)
            {
                goto Label_0050;
            }
            this.CostText.set_text("0");
        Label_0050:
            this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (this.mCurrentUnit != null)
            {
                goto Label_006E;
            }
            return;
        Label_006E:
            flag = this.mCurrentUnit.CurrentJob.Rank == 0;
            if ((this.RankMaxEquipAttention != null) == null)
            {
                goto Label_00A2;
            }
            this.RankMaxEquipAttention.SetActive(flag == 0);
        Label_00A2:
            if ((this.JobInfo != null) == null)
            {
                goto Label_00D8;
            }
            this.JobInfo.SetActive(flag == 0);
            DataSource.Bind<JobData>(this.JobInfo, this.mCurrentUnit.CurrentJob);
        Label_00D8:
            if ((this.AllEquipConfirm != null) == null)
            {
                goto Label_0148;
            }
            if (this.mCurrentUnit.JobIndex < this.mCurrentUnit.NumJobsAvailable)
            {
                goto Label_011E;
            }
            this.AllEquipConfirm.set_text(LocalizedText.Get("sys.UNIT_ALLEQUIP_CHANGE_CONFIRM"));
            goto Label_0148;
        Label_011E:
            this.AllEquipConfirm.set_text((flag == null) ? LocalizedText.Get("sys.UNIT_ALLEQUIP_CONFIRM") : LocalizedText.Get("sys.UNIT_ALLEQUIP_UNLOCK_CONFIRM"));
        Label_0148:
            num = 0;
            dictionary = new Dictionary<string, int>();
            dictionary2 = new Dictionary<string, int>();
            this.NeedEquipList = new NeedEquipItemList();
            if (this.mCurrentUnit.CurrentJob.CanAllEquip(&num, &dictionary, &dictionary2, &this.target_rank, &this.can_jobmaster, &this.can_jobmax, this.NeedEquipList, this.IsAllIn) != null)
            {
                goto Label_019B;
            }
            return;
        Label_019B:
            this.target_rank = Mathf.Min(this.mCurrentUnit.GetJobRankCap(), Mathf.Max(this.target_rank, this.mCurrentUnit.CurrentJob.Rank + 1));
            if (this.IsAllIn != null)
            {
                goto Label_022A;
            }
            this.can_jobmaster = ((this.mCurrentUnit.GetJobRankCap() != JobParam.MAX_JOB_RANK) || (this.target_rank != this.mCurrentUnit.GetJobRankCap())) ? 0 : (this.mCurrentUnit.CurrentJob.Rank == this.mCurrentUnit.GetJobRankCap());
        Label_022A:
            this.SetCommonFlag(this.NeedEquipList.IsEnoughCommon());
            list = MonoSingleton<GameManager>.Instance.MasterParam.Items;
            storeycc = new <Start>c__AnonStorey3CC();
            enumerator = dictionary.Keys.GetEnumerator();
        Label_0265:
            try
            {
                goto Label_02E7;
            Label_026A:
                storeycc.key = &enumerator.Current;
                param = list.Find(new Predicate<ItemParam>(storeycc.<>m__464));
                if (param == null)
                {
                    goto Label_02E7;
                }
                obj2 = Object.Instantiate<GameObject>(this.ListItem);
                obj2.get_gameObject().SetActive(1);
                obj2.get_transform().SetParent(this.ListTransform, 0);
                data = this.CreateItemData(param.iname, dictionary[storeycc.key]);
                DataSource.Bind<ItemData>(obj2, data);
            Label_02E7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_026A;
                }
                goto Label_0305;
            }
            finally
            {
            Label_02F8:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0305:
            storeycd = new <Start>c__AnonStorey3CD();
            enumerator2 = dictionary2.Keys.GetEnumerator();
        Label_0319:
            try
            {
                goto Label_039B;
            Label_031E:
                storeycd.key = &enumerator2.Current;
                param2 = list.Find(new Predicate<ItemParam>(storeycd.<>m__465));
                if (param2 == null)
                {
                    goto Label_039B;
                }
                obj3 = Object.Instantiate<GameObject>(this.ListItem);
                obj3.get_gameObject().SetActive(1);
                obj3.get_transform().SetParent(this.ListTransform, 0);
                data2 = this.CreateItemData(param2.iname, dictionary2[storeycd.key]);
                DataSource.Bind<ItemData>(obj3, data2);
            Label_039B:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_031E;
                }
                goto Label_03B9;
            }
            finally
            {
            Label_03AC:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator2).Dispose();
            }
        Label_03B9:
            if (this.NeedEquipList.IsEnoughCommon() == null)
            {
                goto Label_0598;
            }
            enumerator3 = this.NeedEquipList.CommonNeedNum.Keys.GetEnumerator();
        Label_03E0:
            try
            {
                goto Label_057A;
            Label_03E5:
                num2 = &enumerator3.Current;
                dictionary3 = this.NeedEquipList.CommonNeedNum[num2];
                param3 = dictionary3.CommonItemParam;
                if (param3 == null)
                {
                    goto Label_057A;
                }
                flag2 = 1;
                num3 = 0;
                goto Label_0508;
            Label_041D:
                param4 = dictionary3.list[num3].Param;
                if (param4 != null)
                {
                    goto Label_043E;
                }
                goto Label_0502;
            Label_043E:
                if ((param4.cmn_type - 1) == 2)
                {
                    goto Label_0502;
                }
                flag2 = 0;
                obj4 = Object.Instantiate<GameObject>(this.CommonListItem);
                obj4.get_gameObject().SetActive(1);
                obj4.get_transform().SetParent(this.CommonListTransform, 0);
                data3 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param4.iname);
                if (data3 != null)
                {
                    goto Label_04AC;
                }
                data3 = this.CreateItemData(param4.iname, 0);
            Label_04AC:
                data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param3.iname);
                if (data4 != null)
                {
                    goto Label_04DB;
                }
                data4 = this.CreateItemData(param3.iname, 0);
            Label_04DB:
                obj4.GetComponent<CommonConvertItem>().Bind(data3, data4, dictionary3.list[num3].NeedPiece);
            Label_0502:
                num3 += 1;
            Label_0508:
                if (num3 < dictionary3.list.Count)
                {
                    goto Label_041D;
                }
                if (flag2 == null)
                {
                    goto Label_057A;
                }
                this.IsSoul = 1;
                obj5 = Object.Instantiate<GameObject>(this.ListItem);
                obj5.get_gameObject().SetActive(1);
                obj5.get_transform().SetParent(this.ListTransform, 0);
                data5 = this.CreateItemData(param3.iname, dictionary3.list.Count);
                DataSource.Bind<ItemData>(obj5, data5);
            Label_057A:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_03E5;
                }
                goto Label_0598;
            }
            finally
            {
            Label_058B:
                ((Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator) enumerator3).Dispose();
            }
        Label_0598:
            manager = MonoSingleton<GameManager>.Instance;
            flag3 = num > manager.Player.Gold;
            if ((this.YesButton != null) == null)
            {
                goto Label_05D1;
            }
            this.YesButton.set_interactable(flag3 == 0);
        Label_05D1:
            if ((this.NoGoldWarningText != null) == null)
            {
                goto Label_05F4;
            }
            this.NoGoldWarningText.get_gameObject().SetActive(flag3);
        Label_05F4:
            if ((this.CostText != null) == null)
            {
                goto Label_0617;
            }
            this.CostText.set_text(&num.ToString());
        Label_0617:
            if ((this.MaxJobLv != null) == null)
            {
                goto Label_0651;
            }
            this.MaxJobLv.set_text("/" + &this.mCurrentUnit.GetJobRankCap().ToString());
        Label_0651:
            if ((this.TargetJobLv != null) == null)
            {
                goto Label_0678;
            }
            this.TargetJobLv.set_text(&this.target_rank.ToString());
        Label_0678:
            if ((this.ScrollParent != null) == null)
            {
                goto Label_06AA;
            }
            this.DecelerationRate = this.ScrollParent.get_decelerationRate();
            this.ScrollParent.set_decelerationRate(0f);
        Label_06AA:
            this.ListRectTranceform.set_anchoredPosition(new Vector2(&this.ListRectTranceform.get_anchoredPosition().x, 0f));
            base.StartCoroutine(this.ScrollInit());
            return;
        }

        public bool IsAllIn
        {
            [CompilerGenerated]
            get
            {
                return this.<IsAllIn>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsAllIn>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <ScrollInit>c__Iterator16A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitJobRankUpConfirm <>f__this;

            public <ScrollInit>c__Iterator16A()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0034;
                }
                goto Label_006C;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0034:
                if ((this.<>f__this.ScrollParent != null) == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.ScrollParent.set_decelerationRate(this.<>f__this.DecelerationRate);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3CC
        {
            internal string key;

            public <Start>c__AnonStorey3CC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__464(ItemParam eq)
            {
                return (eq.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3CD
        {
            internal string key;

            public <Start>c__AnonStorey3CD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__465(ItemParam eq)
            {
                return (eq.iname == this.key);
            }
        }

        public delegate void AllInAccept(int current_rank, int target_rank);

        public delegate void OnAccept(int target_rank, bool can_jobmaster, bool can_jobmax);

        public delegate void SetFlag(bool flag);
    }
}

