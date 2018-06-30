namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class QuestDropParam : MonoBehaviour
    {
        [SerializeField]
        public bool IsWarningPopupDisable;
        private static QuestDropParam mQuestDropParam;
        private List<SimpleLocalMapsParam> mSimpleLocalMaps;
        private Dictionary<string, EnemyDropList> mSimpleLocalMapsDict;
        private List<SimpleDropTableParam> mSimpleDropTables;
        private Dictionary<string, SimpleDropTableList> mSimpleDropTableDict;
        private List<SimpleQuestDropParam> mSimpleQuestDrops;
        private readonly string MASTER_PATH;
        private readonly float LOAD_ASYNC_OWN_TIME_LIMIT;
        private bool mIsLoaded;
        private IEnumerator mStartLoadAsyncIEnumerator;

        static QuestDropParam()
        {
        }

        public QuestDropParam()
        {
            this.IsWarningPopupDisable = 1;
            this.mSimpleLocalMaps = new List<SimpleLocalMapsParam>();
            this.mSimpleLocalMapsDict = new Dictionary<string, EnemyDropList>();
            this.mSimpleDropTables = new List<SimpleDropTableParam>();
            this.mSimpleDropTableDict = new Dictionary<string, SimpleDropTableList>();
            this.mSimpleQuestDrops = new List<SimpleQuestDropParam>();
            this.MASTER_PATH = "Data/QuestDropParam";
            this.LOAD_ASYNC_OWN_TIME_LIMIT = 0.03333334f;
            base..ctor();
            return;
        }

        protected void Awake()
        {
            mQuestDropParam = this;
            return;
        }

        private void CompleteLoading()
        {
            if (this.mIsLoaded == null)
            {
                goto Label_001D;
            }
            return;
            goto Label_001D;
        Label_0011:
            this.mStartLoadAsyncIEnumerator.MoveNext();
        Label_001D:
            if (this.mIsLoaded == null)
            {
                goto Label_0011;
            }
            return;
        }

        public void Deserialize(JSON_QuestDropParam json)
        {
            int num;
            SimpleDropTableParam param;
            int num2;
            SimpleLocalMapsParam param2;
            int num3;
            SimpleQuestDropParam param3;
            this.mSimpleDropTables.Clear();
            this.mSimpleLocalMaps.Clear();
            this.mSimpleQuestDrops.Clear();
            if (json.simpleDropTable == null)
            {
                goto Label_006A;
            }
            num = 0;
            goto Label_005C;
        Label_0033:
            param = new SimpleDropTableParam();
            if (param.Deserialize(json.simpleDropTable[num]) == null)
            {
                goto Label_0058;
            }
            this.mSimpleDropTables.Add(param);
        Label_0058:
            num += 1;
        Label_005C:
            if (num < ((int) json.simpleDropTable.Length))
            {
                goto Label_0033;
            }
        Label_006A:
            if (json.simpleLocalMaps == null)
            {
                goto Label_00B3;
            }
            num2 = 0;
            goto Label_00A5;
        Label_007C:
            param2 = new SimpleLocalMapsParam();
            if (param2.Deserialize(json.simpleLocalMaps[num2]) == null)
            {
                goto Label_00A1;
            }
            this.mSimpleLocalMaps.Add(param2);
        Label_00A1:
            num2 += 1;
        Label_00A5:
            if (num2 < ((int) json.simpleLocalMaps.Length))
            {
                goto Label_007C;
            }
        Label_00B3:
            if (json.simpleQuestDrops == null)
            {
                goto Label_0104;
            }
            num3 = 0;
            goto Label_00F5;
        Label_00C6:
            param3 = new SimpleQuestDropParam();
            if (param3.Deserialize(json.simpleQuestDrops[num3]) == null)
            {
                goto Label_00EF;
            }
            this.mSimpleQuestDrops.Add(param3);
        Label_00EF:
            num3 += 1;
        Label_00F5:
            if (num3 < ((int) json.simpleQuestDrops.Length))
            {
                goto Label_00C6;
            }
        Label_0104:
            this.mIsLoaded = 1;
            return;
        }

        private void DeserializeAsync(JSON_QuestDropParam json)
        {
            this.mStartLoadAsyncIEnumerator = this.StartLoadAsync(json);
            base.StartCoroutine(this.mStartLoadAsyncIEnumerator);
            return;
        }

        public unsafe SimpleDropTableList FindSimpleDropTables(string iname)
        {
            SimpleDropTableList list;
            SimpleDropTableList list2;
            int num;
            string str;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            this.CompleteLoading();
            if (this.mSimpleDropTableDict.TryGetValue(iname, &list) == null)
            {
                goto Label_0028;
            }
            return list;
        Label_0028:
            list2 = new SimpleDropTableList();
            num = this.mSimpleDropTables.Count - 1;
            goto Label_007A;
        Label_0041:
            if ((this.mSimpleDropTables[num].GetCommonName == iname) == null)
            {
                goto Label_0076;
            }
            list2.smp_drp_tbls.Add(this.mSimpleDropTables[num]);
        Label_0076:
            num -= 1;
        Label_007A:
            if (num >= 0)
            {
                goto Label_0041;
            }
            this.mSimpleDropTableDict.Add(iname, list2);
            return list2;
        }

        public unsafe EnemyDropList FindSimpleLocalMaps(string iname)
        {
            EnemyDropList list;
            EnemyDropList list2;
            int num;
            int num2;
            SimpleDropTableList list3;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            this.CompleteLoading();
            if (this.mSimpleLocalMapsDict.TryGetValue(iname, &list) == null)
            {
                goto Label_0028;
            }
            return list;
        Label_0028:
            list2 = new EnemyDropList();
            num = this.mSimpleLocalMaps.Count - 1;
            goto Label_00EF;
        Label_0041:
            if ((this.mSimpleLocalMaps[num].iname != iname) == null)
            {
                goto Label_0062;
            }
            goto Label_00EB;
        Label_0062:
            if (this.mSimpleLocalMaps[num].droplist != null)
            {
                goto Label_007D;
            }
            goto Label_00EB;
        Label_007D:
            num2 = 0;
            goto Label_00D2;
        Label_0084:
            if (string.IsNullOrEmpty(this.mSimpleLocalMaps[num].droplist[num2]) == null)
            {
                goto Label_00A6;
            }
            goto Label_00CE;
        Label_00A6:
            list3 = this.FindSimpleDropTables(this.mSimpleLocalMaps[num].droplist[num2]);
            list2.drp_tbls.Add(list3);
        Label_00CE:
            num2 += 1;
        Label_00D2:
            if (num2 < ((int) this.mSimpleLocalMaps[num].droplist.Length))
            {
                goto Label_0084;
            }
        Label_00EB:
            num -= 1;
        Label_00EF:
            if (num >= 0)
            {
                goto Label_0041;
            }
            this.mSimpleLocalMapsDict.Add(iname, list2);
            return list2;
        }

        private unsafe List<BattleCore.DropItemParam> GetCurrTimeDropItemParams(List<SimpleDropTableList> drop_tbls, DateTime date_time)
        {
            List<string> list;
            List<string> list2;
            DateTime time;
            SimpleDropTableList list3;
            List<SimpleDropTableList>.Enumerator enumerator;
            string[] strArray;
            string[] strArray2;
            string[] strArray3;
            string[] strArray4;
            SimpleDropTableParam param;
            List<SimpleDropTableParam>.Enumerator enumerator2;
            string[] strArray5;
            string[] strArray6;
            List<BattleCore.DropItemParam> list4;
            int num;
            ItemParam param2;
            BattleCore.DropItemParam param3;
            int num2;
            ConceptCardParam param4;
            BattleCore.DropItemParam param5;
            list = new List<string>();
            list2 = new List<string>();
            time = DateTime.MinValue;
            enumerator = drop_tbls.GetEnumerator();
        Label_001A:
            try
            {
                goto Label_014B;
            Label_001F:
                list3 = &enumerator.Current;
                if (list3.smp_drp_tbls.Count != null)
                {
                    goto Label_003C;
                }
                goto Label_014B;
            Label_003C:
                strArray = null;
                strArray2 = null;
                strArray3 = null;
                strArray4 = null;
                enumerator2 = list3.smp_drp_tbls.GetEnumerator();
            Label_0055:
                try
                {
                    goto Label_00EB;
                Label_005A:
                    param = &enumerator2.Current;
                    if (param.IsSuffix != null)
                    {
                        goto Label_0086;
                    }
                    strArray = param.dropList;
                    strArray2 = param.dropcards;
                    goto Label_00EB;
                Label_0086:
                    if (param.IsAvailablePeriod(date_time) == null)
                    {
                        goto Label_00EB;
                    }
                    if (strArray3 == null)
                    {
                        goto Label_00B2;
                    }
                    if (0 < DateTime.Compare(time, param.beginAt))
                    {
                        goto Label_00B2;
                    }
                    goto Label_00EB;
                Label_00B2:
                    if (strArray4 == null)
                    {
                        goto Label_00D1;
                    }
                    if (0 < DateTime.Compare(time, param.beginAt))
                    {
                        goto Label_00D1;
                    }
                    goto Label_00EB;
                Label_00D1:
                    strArray3 = param.dropList;
                    strArray4 = param.dropcards;
                    time = param.beginAt;
                Label_00EB:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_005A;
                    }
                    goto Label_0109;
                }
                finally
                {
                Label_00FC:
                    ((List<SimpleDropTableParam>.Enumerator) enumerator2).Dispose();
                }
            Label_0109:
                strArray5 = (strArray3 != null) ? strArray3 : strArray;
                if (strArray5 == null)
                {
                    goto Label_012A;
                }
                list.AddRange(strArray5);
            Label_012A:
                strArray6 = (strArray4 != null) ? strArray4 : strArray2;
                if (strArray6 == null)
                {
                    goto Label_014B;
                }
                list2.AddRange(strArray6);
            Label_014B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001F;
                }
                goto Label_0169;
            }
            finally
            {
            Label_015C:
                ((List<SimpleDropTableList>.Enumerator) enumerator).Dispose();
            }
        Label_0169:
            if (list.Count != null)
            {
                goto Label_0181;
            }
            if (list2.Count != null)
            {
                goto Label_0181;
            }
            return null;
        Label_0181:
            list4 = new List<BattleCore.DropItemParam>();
            num = 0;
            goto Label_01BC;
        Label_0190:
            param3 = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.GetItemParam(list[num]));
            list4.Add(param3);
            num += 1;
        Label_01BC:
            if (num < list.Count)
            {
                goto Label_0190;
            }
            num2 = 0;
            goto Label_0202;
        Label_01D1:
            param5 = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(list2[num2]));
            list4.Add(param5);
            num2 += 1;
        Label_0202:
            if (num2 < list2.Count)
            {
                goto Label_01D1;
            }
            return list4;
        }

        private unsafe List<ItemParam> GetCurrTimeDropItems(List<SimpleDropTableList> drop_tbls, DateTime date_time)
        {
            List<string> list;
            DateTime time;
            SimpleDropTableList list2;
            List<SimpleDropTableList>.Enumerator enumerator;
            string[] strArray;
            string[] strArray2;
            SimpleDropTableParam param;
            List<SimpleDropTableParam>.Enumerator enumerator2;
            string[] strArray3;
            List<ItemParam> list3;
            int num;
            ItemParam param2;
            list = new List<string>();
            time = DateTime.MinValue;
            enumerator = drop_tbls.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_00EC;
            Label_0018:
                list2 = &enumerator.Current;
                if (list2.smp_drp_tbls.Count != null)
                {
                    goto Label_0035;
                }
                goto Label_00EC;
            Label_0035:
                strArray = null;
                strArray2 = null;
                enumerator2 = list2.smp_drp_tbls.GetEnumerator();
            Label_0048:
                try
                {
                    goto Label_00AD;
                Label_004D:
                    param = &enumerator2.Current;
                    if (param.IsSuffix != null)
                    {
                        goto Label_0070;
                    }
                    strArray = param.dropList;
                    goto Label_00AD;
                Label_0070:
                    if (param.IsAvailablePeriod(date_time) == null)
                    {
                        goto Label_00AD;
                    }
                    if (strArray2 == null)
                    {
                        goto Label_009C;
                    }
                    if (0 < DateTime.Compare(time, param.beginAt))
                    {
                        goto Label_009C;
                    }
                    goto Label_00AD;
                Label_009C:
                    strArray2 = param.dropList;
                    time = param.beginAt;
                Label_00AD:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_004D;
                    }
                    goto Label_00CB;
                }
                finally
                {
                Label_00BE:
                    ((List<SimpleDropTableParam>.Enumerator) enumerator2).Dispose();
                }
            Label_00CB:
                strArray3 = (strArray2 != null) ? strArray2 : strArray;
                if (strArray3 == null)
                {
                    goto Label_00EC;
                }
                list.AddRange(strArray3);
            Label_00EC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_0109;
            }
            finally
            {
            Label_00FD:
                ((List<SimpleDropTableList>.Enumerator) enumerator).Dispose();
            }
        Label_0109:
            if (list.Count != null)
            {
                goto Label_0116;
            }
            return null;
        Label_0116:
            list3 = new List<ItemParam>();
            num = 0;
            goto Label_0148;
        Label_0125:
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(list[num]);
            list3.Add(param2);
            num += 1;
        Label_0148:
            if (num < list.Count)
            {
                goto Label_0125;
            }
            return list3;
        }

        private List<BattleCore.DropItemParam> GetEnemyDropItemParams(string quest_iname, DateTime date_time)
        {
            QuestParam param;
            EnemyDropList list;
            param = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
            if (param == null)
            {
                goto Label_0023;
            }
            if (param.map.Count > 0)
            {
                goto Label_0025;
            }
        Label_0023:
            return null;
        Label_0025:
            list = this.FindSimpleLocalMaps(param.map[0].mapSetName);
            if (list != null)
            {
                goto Label_0045;
            }
            return null;
        Label_0045:
            return this.GetCurrTimeDropItemParams(list.drp_tbls, date_time);
        }

        private List<ItemParam> GetEnemyDropItems(string quest_iname, DateTime date_time)
        {
            QuestParam param;
            EnemyDropList list;
            param = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
            if (param == null)
            {
                goto Label_0023;
            }
            if (param.map.Count > 0)
            {
                goto Label_0025;
            }
        Label_0023:
            return null;
        Label_0025:
            list = this.FindSimpleLocalMaps(param.map[0].mapSetName);
            if (list != null)
            {
                goto Label_0045;
            }
            return null;
        Label_0045:
            return this.GetCurrTimeDropItems(list.drp_tbls, date_time);
        }

        public unsafe ItemParam GetHardDropPiece(string quest_iname, DateTime date_time)
        {
            List<ItemParam> list;
            ItemParam param;
            List<ItemParam>.Enumerator enumerator;
            ItemParam param2;
            list = this.GetEnemyDropItems(quest_iname, date_time);
            if (list != null)
            {
                goto Label_0011;
            }
            return null;
        Label_0011:
            enumerator = list.GetEnumerator();
        Label_0018:
            try
            {
                goto Label_003E;
            Label_001D:
                param = &enumerator.Current;
                if (param == null)
                {
                    goto Label_003E;
                }
                if (param.type != 1)
                {
                    goto Label_003E;
                }
                param2 = param;
                goto Label_005D;
            Label_003E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_005B;
            }
            finally
            {
            Label_004F:
                ((List<ItemParam>.Enumerator) enumerator).Dispose();
            }
        Label_005B:
            return null;
        Label_005D:
            return param2;
        }

        public unsafe List<QuestParam> GetItemDropQuestList(ItemParam item, DateTime date_time)
        {
            List<QuestParam> list;
            List<QuestParam> list2;
            SimpleQuestDropParam param;
            List<SimpleQuestDropParam>.Enumerator enumerator;
            string str;
            string[] strArray;
            int num;
            QuestParam param2;
            QuestParam param3;
            List<QuestParam>.Enumerator enumerator2;
            List<ItemParam> list3;
            ItemParam param4;
            List<ItemParam>.Enumerator enumerator3;
            QuestTypes types;
            list = new List<QuestParam>();
            list2 = new List<QuestParam>();
            this.CompleteLoading();
            enumerator = this.mSimpleQuestDrops.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_008B;
            Label_0023:
                param = &enumerator.Current;
                if ((param.item_iname == item.iname) == null)
                {
                    goto Label_008B;
                }
                strArray = param.questlist;
                num = 0;
                goto Label_007B;
            Label_0051:
                str = strArray[num];
                param2 = MonoSingleton<GameManager>.Instance.FindQuest(str);
                if (param2 == null)
                {
                    goto Label_0075;
                }
                list2.Add(param2);
            Label_0075:
                num += 1;
            Label_007B:
                if (num < ((int) strArray.Length))
                {
                    goto Label_0051;
                }
                goto Label_0097;
            Label_008B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
            Label_0097:
                goto Label_00A8;
            }
            finally
            {
            Label_009C:
                ((List<SimpleQuestDropParam>.Enumerator) enumerator).Dispose();
            }
        Label_00A8:
            enumerator2 = list2.GetEnumerator();
        Label_00B0:
            try
            {
                goto Label_017A;
            Label_00B5:
                param3 = &enumerator2.Current;
                if (param3.notSearch == null)
                {
                    goto Label_00CF;
                }
                goto Label_017A;
            Label_00CF:
                types = param3.type;
                switch (types)
                {
                    case 0:
                        goto Label_0116;

                    case 1:
                        goto Label_00F7;

                    case 2:
                        goto Label_00F7;

                    case 3:
                        goto Label_00F7;

                    case 4:
                        goto Label_0116;

                    case 5:
                        goto Label_0116;
                }
            Label_00F7:
                switch ((types - 10))
                {
                    case 0:
                        goto Label_0116;

                    case 1:
                        goto Label_0116;

                    case 2:
                        goto Label_011B;

                    case 3:
                        goto Label_0116;
                }
                goto Label_011B;
            Label_0116:
                goto Label_0120;
            Label_011B:
                goto Label_017A;
            Label_0120:
                enumerator3 = this.GetQuestDropList(param3.iname, date_time).GetEnumerator();
            Label_0139:
                try
                {
                    goto Label_015C;
                Label_013E:
                    param4 = &enumerator3.Current;
                    if (param4 != item)
                    {
                        goto Label_015C;
                    }
                    list.Add(param3);
                    goto Label_0168;
                Label_015C:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_013E;
                    }
                Label_0168:
                    goto Label_017A;
                }
                finally
                {
                Label_016D:
                    ((List<ItemParam>.Enumerator) enumerator3).Dispose();
                }
            Label_017A:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00B5;
                }
                goto Label_0198;
            }
            finally
            {
            Label_018B:
                ((List<QuestParam>.Enumerator) enumerator2).Dispose();
            }
        Label_0198:
            return list;
        }

        public unsafe List<BattleCore.DropItemParam> GetQuestDropItemParamList(string quest_iname, DateTime date_time)
        {
            List<BattleCore.DropItemParam> list;
            SimpleDropTableList list2;
            List<SimpleDropTableList> list3;
            List<BattleCore.DropItemParam> list4;
            List<BattleCore.DropItemParam> list5;
            List<BattleCore.DropItemParam>.Enumerator enumerator;
            <GetQuestDropItemParamList>c__AnonStorey24B storeyb;
            list = new List<BattleCore.DropItemParam>();
            list2 = this.FindSimpleDropTables(quest_iname);
            if (list2 == null)
            {
                goto Label_0037;
            }
            list3 = new List<SimpleDropTableList>();
            list3.Add(list2);
            list4 = this.GetCurrTimeDropItemParams(list3, date_time);
            if (list4 == null)
            {
                goto Label_0037;
            }
            list.AddRange(list4);
        Label_0037:
            list5 = this.GetEnemyDropItemParams(quest_iname, date_time);
            if (list5 == null)
            {
                goto Label_00AE;
            }
            storeyb = new <GetQuestDropItemParamList>c__AnonStorey24B();
            enumerator = list5.GetEnumerator();
        Label_0058:
            try
            {
                goto Label_0090;
            Label_005D:
                storeyb.param = &enumerator.Current;
                if (list.Exists(new Predicate<BattleCore.DropItemParam>(storeyb.<>m__15B)) != null)
                {
                    goto Label_0090;
                }
                list.Add(storeyb.param);
            Label_0090:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005D;
                }
                goto Label_00AE;
            }
            finally
            {
            Label_00A1:
                ((List<BattleCore.DropItemParam>.Enumerator) enumerator).Dispose();
            }
        Label_00AE:
            return list;
        }

        public unsafe List<ItemParam> GetQuestDropList(string quest_iname, DateTime date_time)
        {
            List<ItemParam> list;
            SimpleDropTableList list2;
            List<SimpleDropTableList> list3;
            List<ItemParam> list4;
            List<ItemParam> list5;
            ItemParam param;
            List<ItemParam>.Enumerator enumerator;
            list = new List<ItemParam>();
            list2 = this.FindSimpleDropTables(quest_iname);
            if (list2 == null)
            {
                goto Label_0037;
            }
            list3 = new List<SimpleDropTableList>();
            list3.Add(list2);
            list4 = this.GetCurrTimeDropItems(list3, date_time);
            if (list4 == null)
            {
                goto Label_0037;
            }
            list.AddRange(list4);
        Label_0037:
            list5 = this.GetEnemyDropItems(quest_iname, date_time);
            if (list5 == null)
            {
                goto Label_0092;
            }
            enumerator = list5.GetEnumerator();
        Label_0051:
            try
            {
                goto Label_0074;
            Label_0056:
                param = &enumerator.Current;
                if (list.Contains(param) != null)
                {
                    goto Label_0074;
                }
                list.Add(param);
            Label_0074:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0056;
                }
                goto Label_0092;
            }
            finally
            {
            Label_0085:
                ((List<ItemParam>.Enumerator) enumerator).Dispose();
            }
        Label_0092:
            return list;
        }

        public bool IsChangedQuestDrops(QuestParam quest)
        {
            bool flag;
            QuestTypes types;
            flag = 0;
            switch (quest.type)
            {
                case 0:
                    goto Label_0050;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_006F;

                case 3:
                    goto Label_006F;

                case 4:
                    goto Label_0050;

                case 5:
                    goto Label_0050;

                case 6:
                    goto Label_0050;

                case 7:
                    goto Label_006F;

                case 8:
                    goto Label_006F;

                case 9:
                    goto Label_006F;

                case 10:
                    goto Label_0050;

                case 11:
                    goto Label_0050;

                case 12:
                    goto Label_006F;

                case 13:
                    goto Label_0050;

                case 14:
                    goto Label_0050;
            }
            goto Label_006F;
        Label_0050:
            flag = this.IsEqualsDropList(quest.iname, GlobalVars.GetDropTableGeneratedDateTime(), TimeManager.ServerTime) == 0;
        Label_006F:
            return flag;
        }

        public bool IsEqualsDropList(string quest_iname, DateTime time1, DateTime time2)
        {
            List<BattleCore.DropItemParam> list;
            List<BattleCore.DropItemParam> list2;
            int num;
            if ((time1 == DateTime.MinValue) != null)
            {
                goto Label_0020;
            }
            if ((time2 == DateTime.MinValue) == null)
            {
                goto Label_0022;
            }
        Label_0020:
            return 1;
        Label_0022:
            list = this.GetQuestDropItemParamList(quest_iname, time1);
            list2 = this.GetQuestDropItemParamList(quest_iname, time2);
            if (list.Count == list2.Count)
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            num = 0;
            goto Label_0076;
        Label_004E:
            if ((list[num].Iname != list2[num].Iname) == null)
            {
                goto Label_0072;
            }
            return 0;
        Label_0072:
            num += 1;
        Label_0076:
            if (num < list.Count)
            {
                goto Label_004E;
            }
            return 1;
        }

        public bool Load()
        {
            return this.LoadJson(this.MASTER_PATH, 0);
        }

        private void LoadAsync()
        {
            this.LoadJson(this.MASTER_PATH, 1);
            return;
        }

        private bool LoadJson(string path, bool isAsync)
        {
            string str;
            JSON_QuestDropParam param;
            Exception exception;
            bool flag;
            if (this.mIsLoaded == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            str = AssetManager.LoadTextData(path);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            try
            {
                param = JSONParser.parseJSONObject<JSON_QuestDropParam>(str);
                if (param != null)
                {
                    goto Label_0041;
                }
                throw new InvalidJSONException();
            Label_0041:
                if (isAsync == null)
                {
                    goto Label_0053;
                }
                this.DeserializeAsync(param);
                goto Label_0061;
            Label_0053:
                this.Deserialize(param);
                this.mIsLoaded = 1;
            Label_0061:
                goto Label_0079;
            }
            catch (Exception exception1)
            {
            Label_0066:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_007B;
            }
        Label_0079:
            return 1;
        Label_007B:
            return flag;
        }

        protected void OnDestroy()
        {
            mQuestDropParam = null;
            if (this.mIsLoaded != null)
            {
                goto Label_002F;
            }
            if (this.mStartLoadAsyncIEnumerator == null)
            {
                goto Label_002F;
            }
            base.StopCoroutine(this.mStartLoadAsyncIEnumerator);
            this.mStartLoadAsyncIEnumerator = null;
        Label_002F:
            return;
        }

        protected void Start()
        {
            this.LoadAsync();
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartLoadAsync(JSON_QuestDropParam json)
        {
            <StartLoadAsync>c__Iterator7F iteratorf;
            iteratorf = new <StartLoadAsync>c__Iterator7F();
            iteratorf.json = json;
            iteratorf.<$>json = json;
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public static QuestDropParam Instance
        {
            get
            {
                return mQuestDropParam;
            }
        }

        [CompilerGenerated]
        private sealed class <GetQuestDropItemParamList>c__AnonStorey24B
        {
            internal BattleCore.DropItemParam param;

            public <GetQuestDropItemParamList>c__AnonStorey24B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__15B(BattleCore.DropItemParam drop)
            {
                return (drop.Iname == this.param.Iname);
            }
        }

        [CompilerGenerated]
        private sealed class <StartLoadAsync>c__Iterator7F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal JSON_QuestDropParam json;
            internal float <startTime>__0;
            internal int <i>__1;
            internal SimpleLocalMapsParam <param>__2;
            internal float <subTime>__3;
            internal float <startTime>__4;
            internal int <i>__5;
            internal SimpleDropTableParam <param>__6;
            internal float <subTime>__7;
            internal float <startTime>__8;
            internal int <i>__9;
            internal SimpleQuestDropParam <param>__10;
            internal float <subTime>__11;
            internal int $PC;
            internal object $current;
            internal JSON_QuestDropParam <$>json;
            internal QuestDropParam <>f__this;

            public <StartLoadAsync>c__Iterator7F()
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
                        goto Label_0029;

                    case 1:
                        goto Label_010A;

                    case 2:
                        goto Label_01E0;

                    case 3:
                        goto Label_02B6;
                }
                goto Label_02FA;
            Label_0029:
                this.<>f__this.mIsLoaded = 0;
                this.<>f__this.mSimpleDropTables.Clear();
                this.<>f__this.mSimpleLocalMaps.Clear();
                this.<>f__this.mSimpleQuestDrops.Clear();
                if (this.json.simpleLocalMaps == null)
                {
                    goto Label_013B;
                }
                this.<startTime>__0 = Time.get_realtimeSinceStartup();
                this.<i>__1 = 0;
                goto Label_0123;
            Label_008C:
                this.<param>__2 = new SimpleLocalMapsParam();
                if (this.<param>__2.Deserialize(this.json.simpleLocalMaps[this.<i>__1]) == null)
                {
                    goto Label_00CF;
                }
                this.<>f__this.mSimpleLocalMaps.Add(this.<param>__2);
            Label_00CF:
                this.<subTime>__3 = Time.get_realtimeSinceStartup() - this.<startTime>__0;
                if (this.<subTime>__3 <= this.<>f__this.LOAD_ASYNC_OWN_TIME_LIMIT)
                {
                    goto Label_0115;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_02FC;
            Label_010A:
                this.<startTime>__0 = Time.get_realtimeSinceStartup();
            Label_0115:
                this.<i>__1 += 1;
            Label_0123:
                if (this.<i>__1 < ((int) this.json.simpleLocalMaps.Length))
                {
                    goto Label_008C;
                }
            Label_013B:
                if (this.json.simpleDropTable == null)
                {
                    goto Label_0211;
                }
                this.<startTime>__4 = Time.get_realtimeSinceStartup();
                this.<i>__5 = 0;
                goto Label_01F9;
            Label_0162:
                this.<param>__6 = new SimpleDropTableParam();
                if (this.<param>__6.Deserialize(this.json.simpleDropTable[this.<i>__5]) == null)
                {
                    goto Label_01A5;
                }
                this.<>f__this.mSimpleDropTables.Add(this.<param>__6);
            Label_01A5:
                this.<subTime>__7 = Time.get_realtimeSinceStartup() - this.<startTime>__4;
                if (this.<subTime>__7 <= this.<>f__this.LOAD_ASYNC_OWN_TIME_LIMIT)
                {
                    goto Label_01EB;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_02FC;
            Label_01E0:
                this.<startTime>__4 = Time.get_realtimeSinceStartup();
            Label_01EB:
                this.<i>__5 += 1;
            Label_01F9:
                if (this.<i>__5 < ((int) this.json.simpleDropTable.Length))
                {
                    goto Label_0162;
                }
            Label_0211:
                if (this.json.simpleQuestDrops == null)
                {
                    goto Label_02E7;
                }
                this.<startTime>__8 = Time.get_realtimeSinceStartup();
                this.<i>__9 = 0;
                goto Label_02CF;
            Label_0238:
                this.<param>__10 = new SimpleQuestDropParam();
                if (this.<param>__10.Deserialize(this.json.simpleQuestDrops[this.<i>__9]) == null)
                {
                    goto Label_027B;
                }
                this.<>f__this.mSimpleQuestDrops.Add(this.<param>__10);
            Label_027B:
                this.<subTime>__11 = Time.get_realtimeSinceStartup() - this.<startTime>__8;
                if (this.<subTime>__11 <= this.<>f__this.LOAD_ASYNC_OWN_TIME_LIMIT)
                {
                    goto Label_02C1;
                }
                this.$current = null;
                this.$PC = 3;
                goto Label_02FC;
            Label_02B6:
                this.<startTime>__8 = Time.get_realtimeSinceStartup();
            Label_02C1:
                this.<i>__9 += 1;
            Label_02CF:
                if (this.<i>__9 < ((int) this.json.simpleQuestDrops.Length))
                {
                    goto Label_0238;
                }
            Label_02E7:
                this.<>f__this.mIsLoaded = 1;
                this.$PC = -1;
            Label_02FA:
                return 0;
            Label_02FC:
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
    }
}

