namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class CommonConvertItem : MonoBehaviour
    {
        public GameObject Obj;
        public GameObject CommonObj;
        public LText Amount;
        public LText ItemName;
        public Text ItemUseNum;
        public Text CommonItemUseNum;

        public CommonConvertItem()
        {
            base..ctor();
            return;
        }

        public unsafe void Bind(ItemData data, ItemData cmmon_data, int need_num)
        {
            object[] objArray2;
            object[] objArray1;
            string str;
            DataSource.Bind<ItemData>(this.Obj, data);
            DataSource.Bind<ItemData>(this.CommonObj, cmmon_data);
            objArray1 = new object[] { (int) cmmon_data.Num };
            this.Amount.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NUM", objArray1));
            objArray2 = new object[] { cmmon_data.Param.name, (int) need_num };
            this.ItemName.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NAME", objArray2));
            str = &need_num.ToString();
            this.CommonItemUseNum.set_text(str);
            this.ItemUseNum.set_text(str);
            return;
        }

        public void Refresh(ItemData data, ItemData cmmon_data)
        {
        }
    }
}

