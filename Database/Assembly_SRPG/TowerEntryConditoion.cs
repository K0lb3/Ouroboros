namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TowerEntryConditoion : MonoBehaviour, IGameParameter
    {
        public TowerEntryConditoion()
        {
            base..ctor();
            return;
        }

        public void UpdateValue()
        {
            QuestParam param;
            List<string> list;
            int num;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            list = param.GetEntryQuestConditions(1, 1, 1);
            num = 0;
            if (param.EntryCondition == null)
            {
                goto Label_004E;
            }
            if (param.EntryCondition.plvmin <= 0)
            {
                goto Label_0039;
            }
            num += 1;
        Label_0039:
            if (param.EntryCondition.ulvmin <= 0)
            {
                goto Label_004E;
            }
            num += 1;
        Label_004E:
            base.get_gameObject().SetActive(list.Count > num);
            return;
        }
    }
}

