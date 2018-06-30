namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class CharacterQuestListItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject unitIcon1;
        [SerializeField]
        private GameObject unitIcon2;
        [SerializeField]
        private Text conditionText;
        [CompilerGenerated]
        private static Converter<QuestParam, string> <>f__am$cache3;

        public CharacterQuestListItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static string <SetUp>m__2B5(QuestParam q)
        {
            return q.name;
        }

        public void SetUp(UnitData unitData1, UnitData unitData2, QuestParam questParam)
        {
            List<QuestParam> list;
            string[] strArray;
            if ((this.unitIcon1 != null) == null)
            {
                goto Label_001D;
            }
            DataSource.Bind<UnitData>(this.unitIcon1, unitData1);
        Label_001D:
            if ((this.unitIcon2 != null) == null)
            {
                goto Label_003A;
            }
            DataSource.Bind<UnitData>(this.unitIcon2, unitData2);
        Label_003A:
            if (unitData1 == null)
            {
                goto Label_00B4;
            }
            if (unitData2 == null)
            {
                goto Label_00B4;
            }
            if (questParam == null)
            {
                goto Label_00B4;
            }
            if (questParam == null)
            {
                goto Label_00B4;
            }
            list = questParam.DetectNotClearConditionQuests();
            if (list == null)
            {
                goto Label_00B4;
            }
            if (list.Count <= 0)
            {
                goto Label_00B4;
            }
            if (<>f__am$cache3 != null)
            {
                goto Label_0084;
            }
            <>f__am$cache3 = new Converter<QuestParam, string>(CharacterQuestListItem.<SetUp>m__2B5);
        Label_0084:
            strArray = list.ConvertAll<string>(<>f__am$cache3).ToArray();
            this.conditionText.set_text(string.Join(",", strArray) + "をクリア");
        Label_00B4:
            return;
        }
    }
}

