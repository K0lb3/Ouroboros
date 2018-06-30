namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardDetailSkin : ConceptCardDetailBase
    {
        [SerializeField]
        private Text mCardNextSkinDesc;
        [SerializeField]
        private RawImage mCardSkinIcon;
        private ConceptCardEquipEffect mConceptCardEquipEffect;

        public ConceptCardDetailSkin()
        {
            base..ctor();
            return;
        }

        public override void Refresh()
        {
            object[] objArray1;
            ConceptCardConditionsParam param;
            ArtifactParam param2;
            UnitGroupParam param3;
            UnitParam param4;
            param = base.Master.GetConceptCardConditions(this.mConceptCardEquipEffect.ConditionsIname);
            param2 = base.Master.GetArtifactParam(this.mConceptCardEquipEffect.Skin);
            param3 = base.Master.GetUnitGroup(param.unit_group);
            if (param3.units == null)
            {
                goto Label_00AE;
            }
            if (((int) param3.units.Length) != 1)
            {
                goto Label_00AE;
            }
            param4 = base.Master.GetUnitParam(param3.units[0]);
            objArray1 = new object[] { param4.name, param2.name };
            this.mCardNextSkinDesc.set_text(LocalizedText.Get("sys.CONCEPT_CARD_SKIN_DESCRIPTION", objArray1));
            base.LoadImage(AssetPath.UnitSkinIconSmall(param4, param2, null), this.mCardSkinIcon);
        Label_00AE:
            return;
        }

        public void SetEquipEffect(ConceptCardEquipEffect effect)
        {
            this.mConceptCardEquipEffect = effect;
            return;
        }
    }
}

