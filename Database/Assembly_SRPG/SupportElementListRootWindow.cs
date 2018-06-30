namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SupportElementListRootWindow : MonoBehaviour
    {
        [SerializeField]
        private SupportElementList m_UnitElementList;
        private long[] m_UnitUniequeIDs;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map14;

        public SupportElementListRootWindow()
        {
            this.m_UnitUniequeIDs = new long[Enum.GetValues(typeof(EElement)).Length];
            base..ctor();
            return;
        }

        public void Clear()
        {
            int num;
            num = 1;
            goto Label_0015;
        Label_0007:
            this.m_UnitUniequeIDs[num] = 0L;
            num += 1;
        Label_0015:
            if (num < ((int) this.m_UnitUniequeIDs.Length))
            {
                goto Label_0007;
            }
            this.m_UnitElementList.Refresh(this.m_UnitUniequeIDs);
            return;
        }

        private void DebugRefreshSupportUnitList()
        {
            long[] numArray;
            List<UnitData> list;
            int num;
            int num2;
            numArray = new long[Enum.GetValues(typeof(EElement)).Length];
            numArray[0] = GlobalVars.SelectedSupportUnitUniqueID;
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            num = 1;
            goto Label_007F;
        Label_003E:
            num2 = 0;
            goto Label_006F;
        Label_0045:
            if (list[num2].Element != num)
            {
                goto Label_006B;
            }
            numArray[num] = list[num2].UniqueID;
            goto Label_007B;
        Label_006B:
            num2 += 1;
        Label_006F:
            if (num2 < list.Count)
            {
                goto Label_0045;
            }
        Label_007B:
            num += 1;
        Label_007F:
            if (num < ((int) numArray.Length))
            {
                goto Label_003E;
            }
            this.SetSupportUnitData(numArray);
            return;
        }

        public FlowNode_ReqSupportSet.OwnSupportData[] GetSupportUnitData()
        {
            int num;
            FlowNode_ReqSupportSet.OwnSupportData[] dataArray;
            int num2;
            dataArray = new FlowNode_ReqSupportSet.OwnSupportData[Enum.GetValues(typeof(EElement)).Length];
            num2 = 0;
            goto Label_005E;
        Label_0023:
            if (this.m_UnitUniequeIDs[num2] != null)
            {
                goto Label_0039;
            }
            dataArray[num2] = null;
            goto Label_005A;
        Label_0039:
            dataArray[num2] = new FlowNode_ReqSupportSet.OwnSupportData();
            dataArray[num2].m_Element = num2;
            dataArray[num2].m_UniqueID = this.m_UnitUniequeIDs[num2];
        Label_005A:
            num2 += 1;
        Label_005E:
            if (num2 < ((int) this.m_UnitUniequeIDs.Length))
            {
                goto Label_0023;
            }
            return dataArray;
        }

        public unsafe void OnEvent(string key, string value)
        {
            int num;
            int num2;
            string str;
            Dictionary<string, int> dictionary;
            int num3;
            str = key;
            if (str == null)
            {
                goto Label_00D0;
            }
            if (<>f__switch$map14 != null)
            {
                goto Label_0043;
            }
            dictionary = new Dictionary<string, int>(3);
            dictionary.Add("SET", 0);
            dictionary.Add("REMOVE", 1);
            dictionary.Add("REMOVEALL", 2);
            <>f__switch$map14 = dictionary;
        Label_0043:
            if (<>f__switch$map14.TryGetValue(str, &num3) == null)
            {
                goto Label_00D0;
            }
            switch (num3)
            {
                case 0:
                    goto Label_006D;

                case 1:
                    goto Label_009D;

                case 2:
                    goto Label_00C5;
            }
            goto Label_00D0;
        Label_006D:
            if (UnitListRootWindow.hasInstance == null)
            {
                goto Label_00D0;
            }
            num = UnitListRootWindow.instance.GetData<int>("data_element");
            this.SetSupportUnitData(num, GlobalVars.SelectedUnitUniqueID.Get());
            goto Label_00D0;
        Label_009D:
            if (UnitListRootWindow.hasInstance == null)
            {
                goto Label_00D0;
            }
            num2 = UnitListRootWindow.instance.GetData<int>("data_element");
            this.SetSupportUnitData(num2, 0L);
            goto Label_00D0;
        Label_00C5:
            this.Clear();
        Label_00D0:
            return;
        }

        public void SetSupportUnitData(UnitData[] units)
        {
            int num;
            int num2;
            if (units != null)
            {
                goto Label_002E;
            }
            num = 0;
            goto Label_001B;
        Label_000D:
            this.m_UnitUniequeIDs[num] = 0L;
            num += 1;
        Label_001B:
            if (num < ((int) this.m_UnitUniequeIDs.Length))
            {
                goto Label_000D;
            }
            goto Label_0069;
        Label_002E:
            num2 = 0;
            goto Label_0060;
        Label_0035:
            if (units[num2] != null)
            {
                goto Label_004C;
            }
            this.m_UnitUniequeIDs[num2] = 0L;
            goto Label_005C;
        Label_004C:
            this.m_UnitUniequeIDs[num2] = units[num2].UniqueID;
        Label_005C:
            num2 += 1;
        Label_0060:
            if (num2 < ((int) units.Length))
            {
                goto Label_0035;
            }
        Label_0069:
            if ((this.m_UnitElementList != null) == null)
            {
                goto Label_0086;
            }
            this.m_UnitElementList.Refresh(units);
        Label_0086:
            return;
        }

        public void SetSupportUnitData(long[] iids)
        {
            UnitData[] dataArray;
            int num;
            dataArray = new UnitData[Enum.GetValues(typeof(EElement)).Length];
            num = 0;
            goto Label_003A;
        Label_0021:
            dataArray[num] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iids[num]);
            num += 1;
        Label_003A:
            if (num < ((int) iids.Length))
            {
                goto Label_0021;
            }
            this.SetSupportUnitData(dataArray);
            return;
        }

        public void SetSupportUnitData(int element, long uniqId)
        {
            if (element < 0)
            {
                goto Label_004B;
            }
            if (element >= ((int) this.m_UnitUniequeIDs.Length))
            {
                goto Label_004B;
            }
            this.m_UnitUniequeIDs[element] = uniqId;
            if ((this.m_UnitElementList != null) == null)
            {
                goto Label_004B;
            }
            this.m_UnitElementList.Refresh(element, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqId));
        Label_004B:
            return;
        }
    }
}

