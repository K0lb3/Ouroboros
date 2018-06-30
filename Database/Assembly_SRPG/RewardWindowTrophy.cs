namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class RewardWindowTrophy : RewardWindow
    {
        public RewardWindowTrophy()
        {
            base..ctor();
            return;
        }

        private GameObject AddRewardList(GameObject copy_src, Transform parent)
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(copy_src);
            obj2.get_transform().SetParent(parent, 0);
            obj2.SetActive(1);
            base.mItems.Add(obj2);
            return obj2;
        }

        public override unsafe void Refresh()
        {
            RewardData data;
            Transform transform;
            Transform transform2;
            Transform transform3;
            Transform transform4;
            Transform transform5;
            Transform transform6;
            int num;
            ItemData data2;
            GameObject obj2;
            Transform transform7;
            ArtifactRewardData data3;
            List<ArtifactRewardData>.Enumerator enumerator;
            GameObject obj3;
            Transform transform8;
            KeyValuePair<string, GiftRecieveItemData> pair;
            Dictionary<string, GiftRecieveItemData>.Enumerator enumerator2;
            GameObject obj4;
            GiftRecieveItem item;
            data = DataSource.FindDataOfClass<RewardData>(base.get_gameObject(), null);
            GameUtility.DestroyGameObjects(base.mItems);
            base.mItems.Clear();
            if (data != null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            if ((base.ExpRow != null) == null)
            {
                goto Label_0087;
            }
            transform = ((base.ItemList != null) == null) ? base.ExpRow.get_transform().get_parent() : base.ItemList.get_transform();
            if (data.Exp <= 0)
            {
                goto Label_0087;
            }
            this.AddRewardList(base.ExpRow, transform);
        Label_0087:
            if ((base.GoldRow != null) == null)
            {
                goto Label_00E4;
            }
            transform2 = ((base.ItemList != null) == null) ? base.GoldRow.get_transform().get_parent() : base.ItemList.get_transform();
            if (data.Gold <= 0)
            {
                goto Label_00E4;
            }
            this.AddRewardList(base.GoldRow, transform2);
        Label_00E4:
            if ((base.CoinRow != null) == null)
            {
                goto Label_0141;
            }
            transform3 = ((base.ItemList != null) == null) ? base.CoinRow.get_transform().get_parent() : base.ItemList.get_transform();
            if (data.Coin <= 0)
            {
                goto Label_0141;
            }
            this.AddRewardList(base.CoinRow, transform3);
        Label_0141:
            if ((base.StaminaRow != null) == null)
            {
                goto Label_01A0;
            }
            transform4 = ((base.ItemList != null) == null) ? base.StaminaRow.get_transform().get_parent() : base.ItemList.get_transform();
            if (data.Stamina <= 0)
            {
                goto Label_01A0;
            }
            this.AddRewardList(base.StaminaRow, transform4);
        Label_01A0:
            if ((base.ItemTemplate != null) == null)
            {
                goto Label_02D7;
            }
            transform5 = ((base.ItemList != null) == null) ? base.ItemTemplate.get_transform().get_parent() : base.ItemList.get_transform();
            transform6 = null;
            if ((base.EventCoinTemplate != null) == null)
            {
                goto Label_022B;
            }
            transform6 = ((base.ItemList != null) == null) ? base.EventCoinTemplate.get_transform().get_parent() : base.ItemList.get_transform();
        Label_022B:
            num = 0;
            goto Label_02C5;
        Label_0233:
            data2 = data.Items[num];
            if ((data2.ItemType != 0x10) || ((base.UnitTemplate != null) == null))
            {
                goto Label_0276;
            }
            obj2 = this.AddRewardList(base.UnitTemplate, transform5);
            goto Label_02B6;
        Label_0276:
            if ((data2.ItemType == 0x11) && ((transform6 == null) == null))
            {
                goto Label_02A6;
            }
            obj2 = this.AddRewardList(base.ItemTemplate, transform5);
            goto Label_02B6;
        Label_02A6:
            obj2 = this.AddRewardList(base.EventCoinTemplate, transform6);
        Label_02B6:
            DataSource.Bind<ItemData>(obj2, data2);
            num += 1;
        Label_02C5:
            if (num < data.Items.Count)
            {
                goto Label_0233;
            }
        Label_02D7:
            if ((base.ArtifactTemplate != null) == null)
            {
                goto Label_036D;
            }
            transform7 = ((base.ItemList != null) == null) ? base.ArtifactTemplate.get_transform().get_parent() : base.ItemList.get_transform();
            enumerator = data.Artifacts.GetEnumerator();
        Label_0328:
            try
            {
                goto Label_034F;
            Label_032D:
                data3 = &enumerator.Current;
                DataSource.Bind<ArtifactRewardData>(this.AddRewardList(base.ArtifactTemplate, transform7), data3);
            Label_034F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_032D;
                }
                goto Label_036D;
            }
            finally
            {
            Label_0360:
                ((List<ArtifactRewardData>.Enumerator) enumerator).Dispose();
            }
        Label_036D:
            if ((base.ConceptCardTemplate != null) == null)
            {
                goto Label_0437;
            }
            transform8 = ((base.ItemList != null) == null) ? base.ConceptCardTemplate.get_transform().get_parent() : base.ItemList.get_transform();
            enumerator2 = data.GiftRecieveItemDataDic.GetEnumerator();
        Label_03BE:
            try
            {
                goto Label_0419;
            Label_03C3:
                pair = &enumerator2.Current;
                if (&pair.Value.type != 0x1000L)
                {
                    goto Label_0419;
                }
                obj4 = this.AddRewardList(base.ConceptCardTemplate, transform8);
                item = obj4.GetComponentInChildren<GiftRecieveItem>();
                DataSource.Bind<GiftRecieveItemData>(obj4, &pair.Value);
                obj4.SetActive(1);
                item.UpdateValue();
            Label_0419:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_03C3;
                }
                goto Label_0437;
            }
            finally
            {
            Label_042A:
                ((Dictionary<string, GiftRecieveItemData>.Enumerator) enumerator2).Dispose();
            }
        Label_0437:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

