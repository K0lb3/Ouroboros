namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TowerQuestDropItemList : QuestDropItemList
    {
        public TowerQuestDropItemList()
        {
            base..ctor();
            return;
        }

        protected override void Refresh()
        {
            int num;
            QuestParam param;
            Transform transform;
            TowerFloorParam param2;
            TowerRewardParam param3;
            List<TowerRewardItem> list;
            int num2;
            GameObject obj2;
            GameParameter[] parameterArray;
            int num3;
            TowerRewardUI dui;
            ArtifactParam param4;
            ArtifactIcon icon;
            ArtifactData data;
            <Refresh>c__AnonStorey3AD storeyad;
            if ((base.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = base.mItems.Count - 1;
            goto Label_003A;
        Label_0025:
            Object.Destroy(base.mItems[num]);
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_0025;
            }
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0218;
            }
            transform = base.get_transform();
            param2 = MonoSingleton<GameManager>.Instance.FindTowerFloor(param.iname);
            list = MonoSingleton<GameManager>.Instance.FindTowerReward(param2.reward_id).GetTowerRewardItem();
            num2 = 0;
            goto Label_020A;
        Label_008F:
            storeyad = new <Refresh>c__AnonStorey3AD();
            storeyad.item = list[num2];
            if (storeyad.item.visible == null)
            {
                goto Label_0204;
            }
            if (storeyad.item.type != 1)
            {
                goto Label_00CE;
            }
            goto Label_0204;
        Label_00CE:
            obj2 = Object.Instantiate<GameObject>(base.ItemTemplate);
            obj2.get_transform().SetParent(transform, 0);
            obj2.get_transform().set_localScale(base.ItemTemplate.get_transform().get_localScale());
            DataSource.Bind<TowerRewardItem>(obj2, storeyad.item);
            obj2.SetActive(1);
            parameterArray = obj2.GetComponentsInChildren<GameParameter>();
            num3 = 0;
            goto Label_013E;
        Label_012C:
            parameterArray[num3].Index = num2;
            num3 += 1;
        Label_013E:
            if (num3 < ((int) parameterArray.Length))
            {
                goto Label_012C;
            }
            dui = obj2.GetComponentInChildren<TowerRewardUI>();
            if ((dui != null) == null)
            {
                goto Label_0166;
            }
            dui.Refresh();
        Label_0166:
            if (storeyad.item.type != 6)
            {
                goto Label_0204;
            }
            param4 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(storeyad.item.iname);
            DataSource.Bind<ArtifactParam>(obj2, param4);
            icon = obj2.GetComponentInChildren<ArtifactIcon>();
            if ((icon == null) == null)
            {
                goto Label_01B9;
            }
            goto Label_0218;
        Label_01B9:
            icon.set_enabled(1);
            icon.UpdateValue();
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(storeyad.<>m__427)) != null)
            {
                goto Label_0218;
            }
            storeyad.item.is_new = 1;
            goto Label_0218;
        Label_0204:
            num2 += 1;
        Label_020A:
            if (num2 < list.Count)
            {
                goto Label_008F;
            }
        Label_0218:
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3AD
        {
            internal TowerRewardItem item;

            public <Refresh>c__AnonStorey3AD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__427(ArtifactData x)
            {
                return (x.ArtifactParam.iname == this.item.iname);
            }
        }
    }
}

