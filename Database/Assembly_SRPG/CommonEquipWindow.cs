namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class CommonEquipWindow : MonoBehaviour
    {
        public Text CommonName;
        public Text CommonAmount;
        public Text CommonDescription;
        public Text CommonDescriptionPieceNotEnough;
        public Text CommonCost;
        public GameObject NotEnough;
        public Button ButtonCommonEquip;
        [CompilerGenerated]
        private static Predicate<JobData> <>f__am$cache7;

        public CommonEquipWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__2C7(JobData x)
        {
            return (GlobalVars.SelectedJobUniqueID == x.UniqueID);
        }

        private unsafe void Refresh()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            GameManager manager;
            UnitData data;
            List<JobData> list;
            JobData data2;
            int num;
            EquipData data3;
            ItemParam param;
            ItemData data4;
            int num2;
            int num3;
            int num4;
            bool flag;
            bool flag2;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (data != null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            if ((manager.MasterParam.FixParam.EquipCommonPieceNum != null) && (manager.MasterParam.FixParam.EquipCommonPieceCost != null))
            {
                goto Label_004E;
            }
            return;
        Label_004E:
            list = new List<JobData>(data.Jobs);
            if (<>f__am$cache7 != null)
            {
                goto Label_0073;
            }
            <>f__am$cache7 = new Predicate<JobData>(CommonEquipWindow.<Refresh>m__2C7);
        Label_0073:
            data2 = list.Find(<>f__am$cache7);
            if (data2 != null)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            num = list.IndexOf(data2);
            data3 = data.GetRankupEquipData(num, GlobalVars.SelectedEquipmentSlot);
            param = manager.MasterParam.GetCommonEquip(data3.ItemParam, data2.Rank == 0);
            if (param == null)
            {
                goto Label_00C7;
            }
            return;
        Label_00C7:
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            data4 = manager.Player.FindItemDataByItemParam(param);
            num2 = 0;
            if (data4 == null)
            {
                goto Label_0108;
            }
            num2 = data4.Num;
            DataSource.Bind<ItemData>(base.get_gameObject(), data4);
            goto Label_0115;
        Label_0108:
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
        Label_0115:
            num3 = *(&(manager.MasterParam.FixParam.EquipCommonPieceNum[param.rare]));
            objArray1 = new object[] { param.name, (int) num3 };
            this.CommonName.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NAME", objArray1));
            objArray2 = new object[] { (int) num2 };
            this.CommonAmount.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NUM", objArray2));
            objArray3 = new object[] { param.name, (int) num3 };
            this.CommonDescription.set_text(LocalizedText.Get("sys.COMMON_EQUIP_DESCRIPT", objArray3));
            num4 = *(&(manager.MasterParam.FixParam.EquipCommonPieceCost[data3.Rarity]));
            this.CommonCost.set_text(&num4.ToString());
            flag = (num4 > manager.Player.Gold) == 0;
            flag2 = (num2 < num3) == 0;
            this.NotEnough.SetActive(flag == 0);
            this.CommonDescription.get_gameObject().SetActive(flag2);
            this.CommonDescriptionPieceNotEnough.get_gameObject().SetActive(flag2 == 0);
            this.ButtonCommonEquip.set_interactable((flag == null) ? 0 : flag2);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

