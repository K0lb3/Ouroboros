namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(0, "更新", 0, 0)]
    public class RewardWindow : MonoBehaviour, IFlowInterface
    {
        public bool RefreshOnStart;
        public bool UseGlobalVar;
        public bool UseBindDataOnly;
        public GameObject ExpRow;
        public GameObject GoldRow;
        public GameObject CoinRow;
        public GameObject ArenaMedalRow;
        public GameObject MultiCoinRow;
        public GameObject KakeraCoinRow;
        public GameObject StaminaRow;
        public GameObject ItemSeparator;
        public GameObject ItemList;
        public GameObject ItemTemplate;
        public GameObject EventCoinTemplate;
        public GameObject ArtifactTemplate;
        public GameObject ArtifactTemplate2;
        public GameObject UnitTemplate;
        public GameObject ConceptCardTemplate;
        public GameObject Caution;
        protected List<GameObject> mItems;

        public RewardWindow()
        {
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.EventCoinTemplate != null) == null)
            {
                goto Label_0044;
            }
            this.EventCoinTemplate.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.ArtifactTemplate2 != null) == null)
            {
                goto Label_0066;
            }
            this.ArtifactTemplate2.get_gameObject().SetActive(0);
        Label_0066:
            if ((this.ConceptCardTemplate != null) == null)
            {
                goto Label_0088;
            }
            this.ConceptCardTemplate.get_gameObject().SetActive(0);
        Label_0088:
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_00AA;
            }
            this.UnitTemplate.get_gameObject().SetActive(0);
        Label_00AA:
            if ((this.Caution != null) == null)
            {
                goto Label_00C7;
            }
            this.Caution.SetActive(0);
        Label_00C7:
            return;
        }

        public virtual unsafe void Refresh()
        {
            RewardData data;
            Transform transform;
            GiftRecieveItemData data2;
            Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator;
            GameObject obj2;
            Transform transform2;
            Transform transform3;
            Transform transform4;
            Transform transform5;
            int num;
            if (this.UseGlobalVar == null)
            {
                goto Label_0020;
            }
            DataSource.Bind<RewardData>(base.get_gameObject(), GlobalVars.LastReward);
        Label_0020:
            data = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            if (data != null)
            {
                goto Label_004A;
            }
            return;
        Label_004A:
            if ((this.ArenaMedalRow != null) == null)
            {
                goto Label_006F;
            }
            this.ArenaMedalRow.SetActive(data.ArenaMedal > 0);
        Label_006F:
            if ((this.MultiCoinRow != null) == null)
            {
                goto Label_0094;
            }
            this.MultiCoinRow.SetActive(data.MultiCoin > 0);
        Label_0094:
            if ((this.KakeraCoinRow != null) == null)
            {
                goto Label_00B9;
            }
            this.KakeraCoinRow.SetActive(data.KakeraCoin > 0);
        Label_00B9:
            if ((this.ExpRow != null) == null)
            {
                goto Label_00DE;
            }
            this.ExpRow.SetActive(data.Exp > 0);
        Label_00DE:
            if ((this.GoldRow != null) == null)
            {
                goto Label_0103;
            }
            this.GoldRow.SetActive(data.Gold > 0);
        Label_0103:
            if ((this.CoinRow != null) == null)
            {
                goto Label_0128;
            }
            this.CoinRow.SetActive(data.Coin > 0);
        Label_0128:
            if ((this.StaminaRow != null) == null)
            {
                goto Label_014D;
            }
            this.StaminaRow.SetActive(data.Stamina > 0);
        Label_014D:
            GameParameter.UpdateAll(base.get_gameObject());
            if ((this.ItemSeparator != null) == null)
            {
                goto Label_0182;
            }
            this.ItemSeparator.SetActive(data.Items.Count > 0);
        Label_0182:
            if ((this.ArtifactTemplate != null) == null)
            {
                goto Label_0238;
            }
            transform = ((this.ItemList != null) == null) ? this.ArtifactTemplate.get_transform().get_parent() : this.ItemList.get_transform();
            enumerator = data.GiftRecieveItemDataDic.Values.GetEnumerator();
        Label_01D6:
            try
            {
                goto Label_021B;
            Label_01DB:
                data2 = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.ArtifactTemplate);
                this.mItems.Add(obj2);
                DataSource.Bind<GiftRecieveItemData>(obj2, data2);
                obj2.get_transform().SetParent(transform, 0);
                obj2.SetActive(1);
            Label_021B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01DB;
                }
                goto Label_0238;
            }
            finally
            {
            Label_022C:
                ((Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_0238:
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_028B;
            }
            transform2 = ((this.ItemList != null) == null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemList.get_transform();
            this.RefreshItems(data, transform2, this.ItemTemplate);
        Label_028B:
            if ((this.ArtifactTemplate2 != null) == null)
            {
                goto Label_02BD;
            }
            transform3 = this.ArtifactTemplate2.get_transform().get_parent();
            this.RefreshArtifacts(data, transform3, this.ArtifactTemplate2);
        Label_02BD:
            if ((this.ConceptCardTemplate != null) == null)
            {
                goto Label_02EF;
            }
            transform4 = this.ConceptCardTemplate.get_transform().get_parent();
            this.RefreshConceptCards(data, transform4, this.ConceptCardTemplate);
        Label_02EF:
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_0321;
            }
            transform5 = this.UnitTemplate.get_transform().get_parent();
            this.RefreshUnits(data, transform5, this.UnitTemplate);
        Label_0321:
            if ((this.Caution != null) == null)
            {
                goto Label_0377;
            }
            if (data.IsOverLimit == null)
            {
                goto Label_0377;
            }
            num = this.Caution.get_transform().get_parent().get_childCount();
            this.Caution.get_transform().SetSiblingIndex(num);
            this.Caution.get_gameObject().SetActive(1);
        Label_0377:
            return;
        }

        private unsafe void RefreshArtifacts(RewardData reward, Transform itemParent, GameObject template)
        {
            ArtifactRewardData data;
            List<ArtifactRewardData>.Enumerator enumerator;
            GameObject obj2;
            if (reward.Artifacts == null)
            {
                goto Label_001C;
            }
            if (reward.Artifacts.Count > 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            enumerator = reward.Artifacts.GetEnumerator();
        Label_0029:
            try
            {
                goto Label_0064;
            Label_002E:
                data = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(template);
                obj2.get_transform().SetParent(itemParent, 0);
                this.mItems.Add(obj2);
                DataSource.Bind<ArtifactRewardData>(obj2, data);
                obj2.SetActive(1);
            Label_0064:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002E;
                }
                goto Label_0081;
            }
            finally
            {
            Label_0075:
                ((List<ArtifactRewardData>.Enumerator) enumerator).Dispose();
            }
        Label_0081:
            return;
        }

        private unsafe void RefreshConceptCards(RewardData reward, Transform itemParent, GameObject template)
        {
            KeyValuePair<string, GiftRecieveItemData> pair;
            Dictionary<string, GiftRecieveItemData>.Enumerator enumerator;
            GameObject obj2;
            GiftRecieveItem item;
            enumerator = reward.GiftRecieveItemDataDic.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0071;
            Label_0011:
                pair = &enumerator.Current;
                if (&pair.Value.type != 0x1000L)
                {
                    goto Label_0071;
                }
                obj2 = Object.Instantiate<GameObject>(template);
                obj2.get_transform().SetParent(itemParent, 0);
                this.mItems.Add(obj2);
                item = obj2.GetComponentInChildren<GiftRecieveItem>();
                DataSource.Bind<GiftRecieveItemData>(obj2, &pair.Value);
                obj2.SetActive(1);
                item.UpdateValue();
            Label_0071:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_008E;
            }
            finally
            {
            Label_0082:
                ((Dictionary<string, GiftRecieveItemData>.Enumerator) enumerator).Dispose();
            }
        Label_008E:
            return;
        }

        private void RefreshItems(RewardData reward, Transform itemParent, GameObject template)
        {
            Transform transform;
            List<ItemParam> list;
            int num;
            ItemData data;
            GameObject obj2;
            if ((reward.Items != null) && (reward.Items.Count > 0))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            transform = null;
            if ((this.EventCoinTemplate != null) == null)
            {
                goto Label_0062;
            }
            transform = ((this.ItemList != null) == null) ? this.EventCoinTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        Label_0062:
            list = null;
            num = 0;
            goto Label_010B;
        Label_006B:
            data = reward.Items[num];
            if (data.ItemType != 0x11)
            {
                goto Label_0091;
            }
            if ((transform == null) == null)
            {
                goto Label_00AC;
            }
        Label_0091:
            obj2 = Object.Instantiate<GameObject>(template);
            obj2.get_transform().SetParent(itemParent, 0);
            goto Label_00C7;
        Label_00AC:
            obj2 = Object.Instantiate<GameObject>(this.EventCoinTemplate);
            obj2.get_transform().SetParent(transform, 0);
        Label_00C7:
            this.mItems.Add(obj2);
            DataSource.Bind<ItemData>(obj2, data);
            obj2.SetActive(1);
            if (this.UseBindDataOnly != null)
            {
                goto Label_0107;
            }
            if (list != null)
            {
                goto Label_00FB;
            }
            list = new List<ItemParam>();
        Label_00FB:
            list.Add(data.Param);
        Label_0107:
            num += 1;
        Label_010B:
            if (num < reward.Items.Count)
            {
                goto Label_006B;
            }
            return;
        }

        private unsafe void RefreshUnits(RewardData reward, Transform itemParent, GameObject template)
        {
            KeyValuePair<string, GiftRecieveItemData> pair;
            Dictionary<string, GiftRecieveItemData>.Enumerator enumerator;
            GameObject obj2;
            GiftRecieveItem item;
            enumerator = reward.GiftRecieveItemDataDic.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0071;
            Label_0011:
                pair = &enumerator.Current;
                if (&pair.Value.type != 0x80L)
                {
                    goto Label_0071;
                }
                obj2 = Object.Instantiate<GameObject>(template);
                obj2.get_transform().SetParent(itemParent, 0);
                this.mItems.Add(obj2);
                item = obj2.GetComponentInChildren<GiftRecieveItem>();
                DataSource.Bind<GiftRecieveItemData>(obj2, &pair.Value);
                obj2.SetActive(1);
                item.UpdateValue();
            Label_0071:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_008E;
            }
            finally
            {
            Label_0082:
                ((Dictionary<string, GiftRecieveItemData>.Enumerator) enumerator).Dispose();
            }
        Label_008E:
            return;
        }

        private void Start()
        {
            if (this.RefreshOnStart == null)
            {
                goto Label_0011;
            }
            this.Refresh();
        Label_0011:
            return;
        }
    }
}

