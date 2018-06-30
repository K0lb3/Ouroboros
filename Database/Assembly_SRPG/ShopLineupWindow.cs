namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopLineupWindow : MonoBehaviour
    {
        [SerializeField]
        private Text title;
        [SerializeField]
        private Text lineuplist;

        public ShopLineupWindow()
        {
            base..ctor();
            return;
        }

        public void SetItemInames(Json_ShopLineupItem[] lineups)
        {
            GameManager manager;
            StringBuilder builder;
            int num;
            Json_ShopLineupItemDetail[] detailArray;
            int num2;
            Json_ShopLineupItemDetail detail;
            EShopItemType type;
            ItemParam param;
            ConceptCardParam param2;
            ArtifactParam param3;
            if (lineups == null)
            {
                goto Label_000F;
            }
            if (((int) lineups.Length) > 0)
            {
                goto Label_0010;
            }
        Label_000F:
            return;
        Label_0010:
            manager = MonoSingleton<GameManager>.Instance;
            builder = new StringBuilder();
            num = 0;
            goto Label_0159;
        Label_0023:
            detailArray = lineups[num].items;
            if (detailArray != null)
            {
                goto Label_0037;
            }
            goto Label_0155;
        Label_0037:
            num2 = 0;
            goto Label_013F;
        Label_003F:
            detail = detailArray[num2];
            if (detail != null)
            {
                goto Label_0051;
            }
            goto Label_0139;
        Label_0051:
            type = detail.GetShopItemTypeWithIType();
            if (type != null)
            {
                goto Label_0099;
            }
            param = manager.GetItemParam(detail.iname);
            if (param == null)
            {
                goto Label_0139;
            }
            builder.Append("・" + param.name + "\n");
            goto Label_0139;
        Label_0099:
            if (type != 2)
            {
                goto Label_00DE;
            }
            param2 = manager.MasterParam.GetConceptCardParam(detail.iname);
            if (param2 == null)
            {
                goto Label_0139;
            }
            builder.Append("・" + param2.name + "\n");
            goto Label_0139;
        Label_00DE:
            if (type != 1)
            {
                goto Label_0123;
            }
            param3 = manager.MasterParam.GetArtifactParam(detail.iname);
            if (param3 == null)
            {
                goto Label_0139;
            }
            builder.Append("・" + param3.name + "\n");
            goto Label_0139;
        Label_0123:
            DebugUtility.LogError(string.Format("不明な商品タイプです (item.itype => {0})", detail.itype));
        Label_0139:
            num2 += 1;
        Label_013F:
            if (num2 < ((int) detailArray.Length))
            {
                goto Label_003F;
            }
            builder.Append("\n");
        Label_0155:
            num += 1;
        Label_0159:
            if (num < ((int) lineups.Length))
            {
                goto Label_0023;
            }
            builder.Append(LocalizedText.Get("sys.MSG_SHOP_LINEUP_CAUTION"));
            this.lineuplist.set_text(builder.ToString());
            return;
        }

        private void Start()
        {
            string str;
            if ((this.lineuplist == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.title == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            str = MonoSingleton<GameManager>.Instance.Player.GetShopName(GlobalVars.ShopType);
            this.title.set_text(LocalizedText.Get(str) + " " + LocalizedText.Get("sys.TITLE_SHOP_LINEUP"));
            return;
        }
    }
}

