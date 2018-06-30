namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), AddComponentMenu("UI/リスト/メール")]
    public class MailList : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("リストアイテムとして使用するゲームオブジェクト(受け取り期限なし)")]
        public GameObject ItemForeverTemplate;
        public Toggle ToggleRead;
        public Toggle ToggleUnRead;
        public Button ButtonReadAll;
        public int MaxReadCount;
        public ListExtras ScrollView;
        private List<GameObject> mUnreadMails;
        private List<GameObject> mReadMails;
        private List<GameObject> mItems;
        private Dictionary<GiftTypes, int> currentNums;

        public MailList()
        {
            this.MaxReadCount = 10;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.OnRefresh();
            return;
        }

        private void AddCurrentNum(GiftRecieveItemData data)
        {
            Dictionary<GiftTypes, int> dictionary;
            GiftTypes types;
            int num;
            if (this.currentNums == null)
            {
                goto Label_0046;
            }
            if (this.currentNums.ContainsKey(data.type) == null)
            {
                goto Label_0046;
            }
            num = dictionary[types];
            (dictionary = this.currentNums)[types = data.type] = num + data.num;
        Label_0046:
            return;
        }

        private void Awake()
        {
            Button button;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            if ((this.ItemForeverTemplate != null) == null)
            {
                goto Label_005A;
            }
            if (this.ItemForeverTemplate.get_activeInHierarchy() == null)
            {
                goto Label_005A;
            }
            this.ItemForeverTemplate.SetActive(0);
        Label_005A:
            if ((this.ToggleRead != null) == null)
            {
                goto Label_0087;
            }
            this.ToggleRead.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowRead));
        Label_0087:
            if ((this.ToggleUnRead != null) == null)
            {
                goto Label_00B4;
            }
            this.ToggleUnRead.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowUnRead));
        Label_00B4:
            if ((this.ButtonReadAll != null) == null)
            {
                goto Label_00F4;
            }
            button = this.ButtonReadAll.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_00F4;
            }
            button.get_onClick().AddListener(new UnityAction(this, this.OnAllReadAccept));
        Label_00F4:
            return;
        }

        private unsafe bool CheckRecievable(RewardData reward)
        {
            int num;
            GiftRecieveItemData data;
            Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator;
            num = 0;
            enumerator = reward.GiftRecieveItemDataDic.Values.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_0046;
            Label_0018:
                data = &enumerator.Current;
                if (data.type != 0x40L)
                {
                    goto Label_0046;
                }
                num += this.currentNums[0x40L] + data.num;
            Label_0046:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_0063;
            }
            finally
            {
            Label_0057:
                ((Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_0063:
            if (num < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
            {
                goto Label_0084;
            }
            return 0;
        Label_0084:
            return 1;
        }

        private ArtifactData CreateArtifactData(string iname)
        {
            ArtifactParam param;
            ArtifactData data;
            Json_Artifact artifact;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
            if (param != null)
            {
                goto Label_0019;
            }
            return null;
        Label_0019:
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            artifact.rare = param.rareini;
            data.Deserialize(artifact);
            return data;
        }

        private RewardData GiftDataToRewardData(GiftData[] giftDatas)
        {
            GiftRecieveItemData local1;
            RewardData data;
            GameManager manager;
            int num;
            GiftData data2;
            ArtifactParam param;
            GiftRecieveItemData data3;
            ItemData data4;
            ItemData data5;
            int num2;
            data = new RewardData();
            data.Exp = 0;
            data.Stamina = 0;
            data.MultiCoin = 0;
            data.KakeraCoin = 0;
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_01B6;
        Label_002F:
            data2 = giftDatas[num];
            data.Coin += data2.coin;
            data.Gold += data2.gold;
            data.ArenaMedal += data2.arenacoin;
            data.MultiCoin += data2.multicoin;
            data.KakeraCoin += data2.kakeracoin;
            if (data2.iname != null)
            {
                goto Label_00A2;
            }
            goto Label_01B2;
        Label_00A2:
            if (data2.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_0148;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data2.iname);
            if (param == null)
            {
                goto Label_01B2;
            }
            if (data.GiftRecieveItemDataDic.ContainsKey(data2.iname) == null)
            {
                goto Label_010C;
            }
            local1 = data.GiftRecieveItemDataDic[data2.iname];
            local1.num += data2.num;
            goto Label_0143;
        Label_010C:
            data3 = new GiftRecieveItemData();
            data3.Set(data2.iname, 0x40L, param.rareini, data2.num);
            data.GiftRecieveItemDataDic.Add(data2.iname, data3);
        Label_0143:
            goto Label_01B2;
        Label_0148:
            data4 = new ItemData();
            if (data4.Setup(0L, data2.iname, data2.num) == null)
            {
                goto Label_01B2;
            }
            data.Items.Add(data4);
            data5 = manager.Player.FindItemDataByItemID(data4.Param.iname);
            num2 = (data5 == null) ? 0 : data5.Num;
            data.ItemsBeforeAmount.Add(num2);
        Label_01B2:
            num += 1;
        Label_01B6:
            if (num < ((int) giftDatas.Length))
            {
                goto Label_002F;
            }
            return data;
        }

        private unsafe void OnAllReadAccept()
        {
            ItemListEntity local2;
            GiftRecieveItemData local1;
            RewardData data;
            List<ItemListEntity> list;
            int num;
            int num2;
            MailData data2;
            RewardData data3;
            GiftRecieveItemData data4;
            Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator;
            int num3;
            ItemData data5;
            bool flag;
            int num4;
            GameManager manager;
            int num5;
            ItemListEntity entity;
            ItemData data6;
            int num6;
            int num7;
            ItemData data7;
            if (this.mUnreadMails.Count >= 1)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.RefreshCurrentNums();
            data = new RewardData();
            list = new List<ItemListEntity>();
            num = 0;
            num2 = 0;
            goto Label_027F;
        Label_002D:
            if (num < this.MaxReadCount)
            {
                goto Label_003E;
            }
            goto Label_0290;
        Label_003E:
            data2 = DataSource.FindDataOfClass<MailData>(this.mUnreadMails[num2], null);
            if (data2 == null)
            {
                goto Label_027B;
            }
            data3 = this.GiftDataToRewardData(data2.gifts);
            if (this.CheckRecievable(data3) != null)
            {
                goto Label_007A;
            }
            goto Label_027B;
        Label_007A:
            num += 1;
            enumerator = data3.GiftRecieveItemDataDic.Values.GetEnumerator();
        Label_0091:
            try
            {
                goto Label_00FC;
            Label_0096:
                data4 = &enumerator.Current;
                if (data.GiftRecieveItemDataDic.ContainsKey(data4.iname) == null)
                {
                    goto Label_00E0;
                }
                local1 = data.GiftRecieveItemDataDic[data4.iname];
                local1.num += data4.num;
                goto Label_00F4;
            Label_00E0:
                data.GiftRecieveItemDataDic.Add(data4.iname, data4);
            Label_00F4:
                this.AddCurrentNum(data4);
            Label_00FC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0096;
                }
                goto Label_011A;
            }
            finally
            {
            Label_010D:
                ((Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_011A:
            data.Exp += data3.Exp;
            data.Stamina += data3.Stamina;
            data.Coin += data3.Coin;
            data.Gold += data3.Gold;
            data.ArenaMedal += data3.ArenaMedal;
            data.MultiCoin += data3.MultiCoin;
            data.KakeraCoin += data3.KakeraCoin;
            num3 = 0;
            goto Label_0268;
        Label_01AE:
            data5 = data3.Items[num3];
            if (list.Count <= 0)
            {
                goto Label_024E;
            }
            flag = 0;
            num4 = 0;
            goto Label_0221;
        Label_01D5:
            if ((list[num4].Item.ItemID == data5.ItemID) == null)
            {
                goto Label_021B;
            }
            local2 = list[num4];
            local2.Num += data5.Num;
            flag = 1;
            goto Label_022E;
        Label_021B:
            num4 += 1;
        Label_0221:
            if (num4 < list.Count)
            {
                goto Label_01D5;
            }
        Label_022E:
            if (flag != null)
            {
                goto Label_0262;
            }
            list.Add(new ItemListEntity(data5.Num, data5));
            goto Label_0262;
        Label_024E:
            list.Add(new ItemListEntity(data5.Num, data5));
        Label_0262:
            num3 += 1;
        Label_0268:
            if (num3 < data3.Items.Count)
            {
                goto Label_01AE;
            }
        Label_027B:
            num2 += 1;
        Label_027F:
            if (num2 < this.mUnreadMails.Count)
            {
                goto Label_002D;
            }
        Label_0290:
            manager = MonoSingleton<GameManager>.Instance;
            num5 = 0;
            goto Label_03CC;
        Label_029F:
            entity = list[num5];
            data6 = manager.Player.FindItemDataByItemID(entity.Item.Param.iname);
            num6 = (data6 == null) ? 0 : data6.Num;
            data.ItemsBeforeAmount.Add(num6);
            if (entity.Item.HaveCap >= entity.Num)
            {
                goto Label_0394;
            }
            entity.Item.Gain(entity.Item.HaveCap);
            data.Items.Add(entity.Item);
            num7 = entity.Num - entity.Item.HaveCap;
            data7 = new ItemData();
            if (data7.Setup(entity.Item.UniqueID, entity.Item.Param, num7) == null)
            {
                goto Label_03C6;
            }
            data.Items.Add(data7);
            data.ItemsBeforeAmount.Add(data7.HaveCap);
            goto Label_03C6;
        Label_0394:
            entity.Item.Gain(entity.Num - entity.Item.Num);
            data.Items.Add(entity.Item);
        Label_03C6:
            num5 += 1;
        Label_03CC:
            if (num5 < list.Count)
            {
                goto Label_029F;
            }
            GlobalVars.LastReward.Set(data);
            this.UpdateItems();
            return;
        }

        private void OnRefresh()
        {
            this.UpdateItems();
            this.ToggleReadAll();
            return;
        }

        private void OnSelect(GameObject go)
        {
            MailData data;
            RewardData data2;
            FlowNode_OnMailSelect select;
            data = DataSource.FindDataOfClass<MailData>(go, null);
            if (data == null)
            {
                goto Label_0071;
            }
            GlobalVars.SelectedMailUniqueID.Set(data.mid);
            GlobalVars.SelectedMailPeriod.Set(data.period);
            data2 = this.GiftDataToRewardData(data.gifts);
            select = base.GetComponentInParent<FlowNode_OnMailSelect>();
            if ((select == null) == null)
            {
                goto Label_0054;
            }
            select = Object.FindObjectOfType<FlowNode_OnMailSelect>();
        Label_0054:
            if ((select != null) == null)
            {
                goto Label_0066;
            }
            select.Selected();
        Label_0066:
            GlobalVars.LastReward.Set(data2);
        Label_0071:
            this.UpdateItems();
            return;
        }

        private unsafe void OnShowRead(bool isActive)
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            MailData data;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            MailData data2;
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            enumerator = this.mUnreadMails.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_003F;
            Label_0018:
                obj2 = &enumerator.Current;
                if (DataSource.FindDataOfClass<MailData>(obj2.get_gameObject(), null) != null)
                {
                    goto Label_0038;
                }
                goto Label_003F;
            Label_0038:
                obj2.SetActive(0);
            Label_003F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_005C;
            }
            finally
            {
            Label_0050:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_005C:
            enumerator2 = this.mReadMails.GetEnumerator();
        Label_0069:
            try
            {
                goto Label_0097;
            Label_006E:
                obj3 = &enumerator2.Current;
                if (DataSource.FindDataOfClass<MailData>(obj3.get_gameObject(), null) != null)
                {
                    goto Label_0090;
                }
                goto Label_0097;
            Label_0090:
                obj3.SetActive(1);
            Label_0097:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_006E;
                }
                goto Label_00B5;
            }
            finally
            {
            Label_00A8:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_00B5:
            if ((this.ScrollView != null) == null)
            {
                goto Label_00D6;
            }
            this.ScrollView.SetScrollPos(1f);
        Label_00D6:
            this.ButtonReadAll.get_gameObject().SetActive(0);
            return;
        }

        private unsafe void OnShowUnRead(bool isActive)
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            MailData data;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            MailData data2;
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            enumerator = this.mReadMails.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_003F;
            Label_0018:
                obj2 = &enumerator.Current;
                if (DataSource.FindDataOfClass<MailData>(obj2.get_gameObject(), null) != null)
                {
                    goto Label_0038;
                }
                goto Label_003F;
            Label_0038:
                obj2.SetActive(0);
            Label_003F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_005C;
            }
            finally
            {
            Label_0050:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_005C:
            enumerator2 = this.mUnreadMails.GetEnumerator();
        Label_0069:
            try
            {
                goto Label_0097;
            Label_006E:
                obj3 = &enumerator2.Current;
                if (DataSource.FindDataOfClass<MailData>(obj3.get_gameObject(), null) != null)
                {
                    goto Label_0090;
                }
                goto Label_0097;
            Label_0090:
                obj3.SetActive(1);
            Label_0097:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_006E;
                }
                goto Label_00B5;
            }
            finally
            {
            Label_00A8:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_00B5:
            if ((this.ScrollView != null) == null)
            {
                goto Label_00D6;
            }
            this.ScrollView.SetScrollPos(1f);
        Label_00D6:
            this.ToggleReadAll();
            return;
        }

        private void RefreshCurrentNums()
        {
            this.currentNums = new Dictionary<GiftTypes, int>();
            this.currentNums.Add(0x40L, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
            return;
        }

        private unsafe void Start()
        {
            GiftData[] dataArray1;
            List<MailData> list;
            int num;
            MailData data;
            GiftData data2;
            int num2;
            if (Network.Mode != 1)
            {
                goto Label_0113;
            }
            list = MonoSingleton<GameManager>.Instance.Player.Mails;
            list.Clear();
            num = 0;
            goto Label_010B;
        Label_0028:
            data = new MailData();
            data2 = new GiftData();
            num2 = num % 3;
            switch (num2)
            {
                case 0:
                    goto Label_0051;

                case 1:
                    goto Label_0068;

                case 2:
                    goto Label_0078;
            }
            goto Label_0085;
        Label_0051:
            data2.iname = "IT_US_POTION";
            data2.num = 3;
            goto Label_0085;
        Label_0068:
            data2.gold = 0x3e8;
            goto Label_0085;
        Label_0078:
            data2.coin = 10;
        Label_0085:
            dataArray1 = new GiftData[] { data2 };
            data.gifts = dataArray1;
            data.mid = (long) num;
            data.msg = "てすと" + &num.ToString();
            data.msg = data.msg + (((num % 2) != null) ? "既読" : "未読");
            data.read = (long) (((num % 2) != null) ? 1 : 0);
            data.post_at = (long) (0x2710 + num);
            list.Add(data);
            num += 1;
        Label_010B:
            if (num < 50)
            {
                goto Label_0028;
            }
        Label_0113:
            this.OnRefresh();
            return;
        }

        private void ToggleReadAll()
        {
            if (this.mUnreadMails.Count >= 1)
            {
                goto Label_0027;
            }
            this.ButtonReadAll.get_gameObject().SetActive(0);
            goto Label_0038;
        Label_0027:
            this.ButtonReadAll.get_gameObject().SetActive(1);
        Label_0038:
            return;
        }

        private void UpdateItems()
        {
            int num;
            int num2;
            int num3;
            List<MailData> list;
            Transform transform;
            int num4;
            MailData data;
            GameObject obj2;
            GameObject obj3;
            int num5;
            int num6;
            GiftData data2;
            MailIcon icon;
            int num7;
            GiftData data3;
            ArtifactData data4;
            MailIcon icon2;
            ItemData data5;
            MailIcon icon3;
            MailIcon icon4;
            MailIcon icon5;
            MailIcon icon6;
            MailIcon icon7;
            MailIcon icon8;
            GameObject obj4;
            Button button;
            ListItemEvents events;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mItems == null)
            {
                goto Label_00C1;
            }
            num = 0;
            goto Label_003A;
        Label_0024:
            this.mReadMails[num].SetActive(0);
            num += 1;
        Label_003A:
            if (num < this.mReadMails.Count)
            {
                goto Label_0024;
            }
            num2 = 0;
            goto Label_0068;
        Label_0052:
            this.mUnreadMails[num2].SetActive(0);
            num2 += 1;
        Label_0068:
            if (num2 < this.mUnreadMails.Count)
            {
                goto Label_0052;
            }
            this.mReadMails.Clear();
            this.mUnreadMails.Clear();
            num3 = 0;
            goto Label_00B0;
        Label_0096:
            Object.Destroy(this.mItems[num3].get_gameObject());
            num3 += 1;
        Label_00B0:
            if (num3 < this.mItems.Count)
            {
                goto Label_0096;
            }
        Label_00C1:
            list = MonoSingleton<GameManager>.Instance.Player.Mails;
            transform = base.get_transform();
            this.mItems = new List<GameObject>(list.Count);
            this.mReadMails = new List<GameObject>();
            this.mUnreadMails = new List<GameObject>();
            num4 = list.Count - 1;
            goto Label_055F;
        Label_010F:
            data = list[num4];
            obj2 = this.ItemTemplate;
            if (data.IsPeriod == null)
            {
                goto Label_013A;
            }
            obj2 = this.ItemTemplate;
            goto Label_0153;
        Label_013A:
            if ((this.ItemForeverTemplate != null) == null)
            {
                goto Label_0153;
            }
            obj2 = this.ItemForeverTemplate;
        Label_0153:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(transform, 0);
            num5 = 0;
            num6 = 0;
            goto Label_01FA;
        Label_0176:
            data2 = data.gifts[num6];
            if (data2.num <= 0)
            {
                goto Label_0195;
            }
            num5 += 1;
        Label_0195:
            if (data2.coin <= 0)
            {
                goto Label_01A8;
            }
            num5 += 1;
        Label_01A8:
            if (data2.gold <= 0)
            {
                goto Label_01BB;
            }
            num5 += 1;
        Label_01BB:
            if (data2.arenacoin <= 0)
            {
                goto Label_01CE;
            }
            num5 += 1;
        Label_01CE:
            if (data2.multicoin <= 0)
            {
                goto Label_01E1;
            }
            num5 += 1;
        Label_01E1:
            if (data2.kakeracoin <= 0)
            {
                goto Label_01F4;
            }
            num5 += 1;
        Label_01F4:
            num6 += 1;
        Label_01FA:
            if (num6 < ((int) data.gifts.Length))
            {
                goto Label_0176;
            }
            if (num5 < 2)
            {
                goto Label_0248;
            }
            icon = obj3.GetComponent<MailIcon>();
            if ((icon != null) == null)
            {
                goto Label_0499;
            }
            icon.CurrentIcon = icon.SetIconTemplate;
            icon.CurrentIcon.SetActive(1);
            goto Label_0499;
        Label_0248:
            num7 = 0;
            goto Label_0489;
        Label_0250:
            data3 = data.gifts[num7];
            if (data3.num <= 0)
            {
                goto Label_0339;
            }
            if (data3.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_02CD;
            }
            data4 = this.CreateArtifactData(data3.iname);
            if (data4 == null)
            {
                goto Label_0297;
            }
            DataSource.Bind<ArtifactData>(obj3, data4);
        Label_0297:
            icon2 = obj3.GetComponent<MailIcon>();
            if ((icon2 != null) == null)
            {
                goto Label_0499;
            }
            icon2.CurrentIcon = icon2.ArtifactIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0334;
        Label_02CD:
            if (data3.CheckGiftTypeIncluded(1L) == null)
            {
                goto Label_0499;
            }
            data5 = new ItemData();
            data5.Setup(0L, data3.iname, data3.num);
            DataSource.Bind<ItemData>(obj3, data5);
            icon3 = obj3.GetComponent<MailIcon>();
            if ((icon3 != null) == null)
            {
                goto Label_0499;
            }
            icon3.CurrentIcon = icon3.ItemIconTemplate;
            icon3.CurrentIcon.SetActive(1);
        Label_0334:
            goto Label_0499;
        Label_0339:
            if (data3.coin <= 0)
            {
                goto Label_037C;
            }
            icon4 = obj3.GetComponent<MailIcon>();
            if ((icon4 != null) == null)
            {
                goto Label_0499;
            }
            icon4.CurrentIcon = icon4.CoinIconTemplate;
            icon4.CurrentIcon.SetActive(1);
            goto Label_0499;
        Label_037C:
            if (data3.gold <= 0)
            {
                goto Label_03BF;
            }
            icon5 = obj3.GetComponent<MailIcon>();
            if ((icon5 != null) == null)
            {
                goto Label_0483;
            }
            icon5.CurrentIcon = icon5.GoldIconTemplate;
            icon5.CurrentIcon.SetActive(1);
            goto Label_0483;
        Label_03BF:
            if (data3.arenacoin <= 0)
            {
                goto Label_0402;
            }
            icon6 = obj3.GetComponent<MailIcon>();
            if ((icon6 != null) == null)
            {
                goto Label_0483;
            }
            icon6.CurrentIcon = icon6.ArenaCoinIconTemplate;
            icon6.CurrentIcon.SetActive(1);
            goto Label_0483;
        Label_0402:
            if (data3.multicoin <= 0)
            {
                goto Label_0445;
            }
            icon7 = obj3.GetComponent<MailIcon>();
            if ((icon7 != null) == null)
            {
                goto Label_0483;
            }
            icon7.CurrentIcon = icon7.MultiCoinIconTemplate;
            icon7.CurrentIcon.SetActive(1);
            goto Label_0483;
        Label_0445:
            if (data3.kakeracoin <= 0)
            {
                goto Label_0483;
            }
            icon8 = obj3.GetComponent<MailIcon>();
            if ((icon8 != null) == null)
            {
                goto Label_0483;
            }
            icon8.CurrentIcon = icon8.KakeraCoinIconTemplate;
            icon8.CurrentIcon.SetActive(1);
        Label_0483:
            num7 += 1;
        Label_0489:
            if (num7 < ((int) data.gifts.Length))
            {
                goto Label_0250;
            }
        Label_0499:
            DataSource.Bind<MailData>(obj3, data);
            obj4 = obj3.get_transform().FindChild("btn_read").get_gameObject();
            if ((obj4 != null) == null)
            {
                goto Label_050F;
            }
            DataSource.Bind<MailData>(obj4, data);
            button = obj4.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_050F;
            }
            events = button.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_050F;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_050F:
            if (data.read <= 0L)
            {
                goto Label_0537;
            }
            this.mReadMails.Add(obj3);
            obj3.SetActive(0);
            goto Label_054C;
        Label_0537:
            this.mUnreadMails.Add(obj3);
            obj3.SetActive(1);
        Label_054C:
            this.mItems.Add(obj3);
            num4 -= 1;
        Label_055F:
            if (num4 >= 0)
            {
                goto Label_010F;
            }
            return;
        }

        private class ItemListEntity
        {
            public int Num;
            public ItemData Item;

            public ItemListEntity(int Num, ItemData Item)
            {
                base..ctor();
                this.Num = Num;
                this.Item = Item;
                return;
            }
        }
    }
}

