namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class QuestDropItemList : MonoBehaviour
    {
        public GameObject ItemTemplate;
        protected List<GameObject> mItems;

        public QuestDropItemList()
        {
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        protected virtual void Refresh()
        {
            int num;
            QuestParam param;
            List<BattleCore.DropItemParam> list;
            int num2;
            BattleCore.DropItemParam param2;
            GameObject obj2;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.mItems.Count - 1;
            goto Label_003A;
        Label_0025:
            Object.Destroy(this.mItems[num]);
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_0025;
            }
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0114;
            }
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_0114;
            }
            list = QuestDropParam.Instance.GetQuestDropItemParamList(param.iname, GlobalVars.GetDropTableGeneratedDateTime());
            if (list == null)
            {
                goto Label_0114;
            }
            num2 = 0;
            goto Label_0108;
        Label_0087:
            param2 = list[num2];
            if (param2 != null)
            {
                goto Label_009C;
            }
            goto Label_0104;
        Label_009C:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            if (param2.IsItem == null)
            {
                goto Label_00C8;
            }
            DataSource.Bind<ItemParam>(obj2, param2.itemParam);
            goto Label_00E2;
        Label_00C8:
            if (param2.IsConceptCard == null)
            {
                goto Label_00E2;
            }
            DataSource.Bind<ConceptCardParam>(obj2, param2.conceptCardParam);
        Label_00E2:
            obj2.get_transform().SetParent(base.get_transform(), 0);
            obj2.SetActive(1);
            GameParameter.UpdateAll(obj2);
        Label_0104:
            num2 += 1;
        Label_0108:
            if (num2 < list.Count)
            {
                goto Label_0087;
            }
        Label_0114:
            return;
        }

        private void Start()
        {
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
            this.Refresh();
            return;
        }
    }
}

