namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardDetailGetUnit : ConceptCardDetailBase
    {
        [SerializeField]
        private RawImage UnitIcon;
        [SerializeField]
        private Text UnitName;
        [SerializeField]
        private ButtonEvent UnitDetailBtn;

        public ConceptCardDetailGetUnit()
        {
            base..ctor();
            return;
        }

        public override void Refresh()
        {
            string str;
            UnitParam param;
            ButtonEvent.Event event2;
            if (base.mConceptCardData == null)
            {
                goto Label_00CF;
            }
            str = base.mConceptCardData.Param.first_get_unit;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00CF;
            }
            param = base.GM.GetUnitParam(str);
            if (param == null)
            {
                goto Label_00CF;
            }
            if ((this.UnitIcon != null) == null)
            {
                goto Label_006F;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.UnitIcon, (param == null) ? null : AssetPath.UnitSkinIconSmall(param, null, null));
        Label_006F:
            if ((this.UnitName != null) == null)
            {
                goto Label_0091;
            }
            this.UnitName.set_text(param.name);
        Label_0091:
            if ((this.UnitDetailBtn != null) == null)
            {
                goto Label_00CF;
            }
            event2 = this.UnitDetailBtn.GetEvent("CONCEPT_CARD_DETAIL_BTN_UNIT_DETAIL");
            if (event2 == null)
            {
                goto Label_00CF;
            }
            event2.valueList.SetField("select_unit", param.iname);
        Label_00CF:
            return;
        }
    }
}

