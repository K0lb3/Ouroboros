namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SupportList : UnitListV2
    {
        public RectTransform UnitListHilit;
        [CompilerGenerated]
        private static Predicate<UnitData> <>f__am$cache1;
        [CompilerGenerated]
        private static Predicate<UnitData> <>f__am$cache2;

        public SupportList()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <SetData>m__41B(UnitData x)
        {
            return (x.UniqueID == GlobalVars.SelectedSupportUnitUniqueID);
        }

        [CompilerGenerated]
        private static bool <SetData>m__41C(UnitData x)
        {
            return (x.UniqueID == GlobalVars.SelectedSupportUnitUniqueID);
        }

        protected override void RefreshItems()
        {
            base.RefreshItems();
            if (base.mItems.Count <= 0)
            {
                goto Label_003E;
            }
            this.UnitListHilit.SetParent(base.mItems[0].GetComponent<UnitIcon>().Frame.get_transform(), 0);
        Label_003E:
            this.UnitListHilit.get_gameObject().SetActive(base.mPage == 0);
            return;
        }

        public override unsafe void SetData(object[] src, Type type)
        {
            UnitData[] dataArray;
            List<UnitData> list;
            UnitData data;
            int num;
            UnitData data2;
            List<UnitData> list2;
            UnitData data3;
            List<int> list3;
            int num2;
            if (GlobalVars.SelectedSupportUnitUniqueID <= 0L)
            {
                goto Label_0119;
            }
            dataArray = src as UnitData[];
            list = new List<UnitData>(dataArray);
            if (<>f__am$cache1 != null)
            {
                goto Label_0038;
            }
            <>f__am$cache1 = new Predicate<UnitData>(SupportList.<SetData>m__41B);
        Label_0038:
            data = list.Find(<>f__am$cache1);
            num = 0;
            if (data != null)
            {
                goto Label_00AF;
            }
            if (<>f__am$cache2 != null)
            {
                goto Label_0069;
            }
            <>f__am$cache2 = new Predicate<UnitData>(SupportList.<SetData>m__41C);
        Label_0069:
            data2 = this.mOwnUnits.Find(<>f__am$cache2);
            if (data2 == null)
            {
                goto Label_00B7;
            }
            list.Add(data2);
            num = list.Count - 1;
            list2 = new List<UnitData>(list);
            GameUtility.SortUnits(list2, base.mUnitSortMode, 0, &this.mSortValues, 0);
            goto Label_00B7;
        Label_00AF:
            num = list.IndexOf(data);
        Label_00B7:
            data3 = list[num];
            list.RemoveAt(num);
            list.Insert(0, data3);
            if (base.mSortValues == null)
            {
                goto Label_0111;
            }
            list3 = new List<int>(base.mSortValues);
            num2 = list3[num];
            list3.RemoveAt(num);
            list3.Insert(0, num2);
            base.mSortValues = list3.ToArray();
        Label_0111:
            src = list.ToArray();
        Label_0119:
            base.SetData(src, type);
            GameParameter.UpdateAll(base.get_gameObject());
            base.mPage = 0;
            return;
        }

        private List<UnitData> mOwnUnits
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.Units;
            }
        }
    }
}

