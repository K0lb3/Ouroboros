namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class UnitGetParam
    {
        public List<Set> Params;

        public UnitGetParam(ItemData data)
        {
            this.Params = new List<Set>();
            base..ctor();
            this.Add(data.Param);
            return;
        }

        public UnitGetParam(ItemParam param)
        {
            this.Params = new List<Set>();
            base..ctor();
            this.Add(param);
            return;
        }

        public UnitGetParam(GiftData[] paramList)
        {
            MasterParam param;
            int num;
            ItemParam param2;
            this.Params = new List<Set>();
            base..ctor();
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = 0;
            goto Label_0056;
        Label_0023:
            if (paramList[num].CheckGiftTypeIncluded(0x80L) == null)
            {
                goto Label_0052;
            }
            param2 = param.GetItemParam(paramList[num].iname);
            if (param2 == null)
            {
                goto Label_0052;
            }
            this.Add(param2);
        Label_0052:
            num += 1;
        Label_0056:
            if (num < ((int) paramList.Length))
            {
                goto Label_0023;
            }
            return;
        }

        public UnitGetParam(ItemData[] paramLsit)
        {
            this.Params = new List<Set>();
            base..ctor();
            this.AddArary(paramLsit);
            return;
        }

        public UnitGetParam(ItemParam[] paramLsit)
        {
            this.Params = new List<Set>();
            base..ctor();
            this.AddArary(paramLsit);
            return;
        }

        public void Add(ItemParam param)
        {
            this.AddInternal(param, null);
            return;
        }

        public void AddArary(ItemData[] list)
        {
            List<UnitData> list2;
            int num;
            list2 = MonoSingleton<GameManager>.GetInstanceDirect().Player.Units;
            num = 0;
            goto Label_002A;
        Label_0017:
            this.AddInternal(list[num].Param, list2);
            num += 1;
        Label_002A:
            if (num < ((int) list.Length))
            {
                goto Label_0017;
            }
            return;
        }

        public void AddArary(ItemParam[] list)
        {
            List<UnitData> list2;
            int num;
            list2 = MonoSingleton<GameManager>.GetInstanceDirect().Player.Units;
            num = 0;
            goto Label_0025;
        Label_0017:
            this.AddInternal(list[num], list2);
            num += 1;
        Label_0025:
            if (num < ((int) list.Length))
            {
                goto Label_0017;
            }
            return;
        }

        private void AddInternal(ItemParam param, List<UnitData> units)
        {
            GameManager manager;
            List<UnitData> list;
            UnitData data;
            Set set;
            UnitParam param2;
            <AddInternal>c__AnonStorey3CB storeycb;
            storeycb = new <AddInternal>c__AnonStorey3CB();
            storeycb.param = param;
            if (storeycb.param.type == 0x10)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            list = (units != null) ? units : manager.Player.Units;
            data = list.Find(new Predicate<UnitData>(storeycb.<>m__462));
            set = new Set();
            set.ItemId = storeycb.param.iname;
            set.ItemType = storeycb.param.type;
            set.IsConvert = (data == null) == 0;
            if (data != null)
            {
                goto Label_00B3;
            }
            param2 = manager.MasterParam.GetUnitParam(storeycb.param.iname);
            set.UnitParam = param2;
        Label_00B3:
            this.Params.Add(set);
            return;
        }

        [CompilerGenerated]
        private sealed class <AddInternal>c__AnonStorey3CB
        {
            internal ItemParam param;

            public <AddInternal>c__AnonStorey3CB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__462(UnitData p)
            {
                return (p.UnitParam.iname == this.param.iname);
            }
        }

        public class Set
        {
            public string ItemId;
            public EItemType ItemType;
            public bool IsConvert;
            public SRPG.UnitParam UnitParam;

            public Set()
            {
                base..ctor();
                return;
            }
        }
    }
}

