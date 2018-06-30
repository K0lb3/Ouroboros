namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class NeedEquipItemDictionary
    {
        public List<NeedEquipItem> list;
        private int need_picec;
        private ItemData data;
        public ItemParam CommonItemParam;

        public NeedEquipItemDictionary(ItemParam item_param, bool is_soul)
        {
            this.list = new List<NeedEquipItem>();
            base..ctor();
            this.CommonItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(item_param, is_soul);
            this.data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.CommonItemParam.iname);
            return;
        }

        public void Add(ItemParam _param, int _need_picec)
        {
            this.list.Add(new NeedEquipItem(_param, _need_picec));
            this.need_picec += _need_picec;
            return;
        }

        public void Remove(ItemParam _param)
        {
            NeedEquipItem item;
            item = this.list[this.list.Count - 1];
            if (item != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            this.need_picec -= item.NeedPiece;
            this.list.RemoveAt(this.list.Count - 1);
            return;
        }

        public int CommonEquipItemNum
        {
            get
            {
                return ((this.data == null) ? 0 : this.data.Num);
            }
        }

        public bool IsEnough
        {
            get
            {
                return ((this.CommonEquipItemNum < this.need_picec) == 0);
            }
        }

        public int NeedPicec
        {
            get
            {
                return this.need_picec;
            }
        }
    }
}

