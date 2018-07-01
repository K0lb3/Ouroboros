// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailSkin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailSkin : ConceptCardDetailBase
  {
    [SerializeField]
    private Text mCardNextSkinDesc;
    [SerializeField]
    private RawImage mCardSkinIcon;
    private ConceptCardEquipEffect mConceptCardEquipEffect;

    public void SetEquipEffect(ConceptCardEquipEffect effect)
    {
      this.mConceptCardEquipEffect = effect;
    }

    public override void Refresh()
    {
      ConceptCardConditionsParam conceptCardConditions = this.Master.GetConceptCardConditions(this.mConceptCardEquipEffect.ConditionsIname);
      ArtifactParam artifactParam = this.Master.GetArtifactParam(this.mConceptCardEquipEffect.Skin);
      UnitGroupParam unitGroup = this.Master.GetUnitGroup(conceptCardConditions.unit_group);
      if (unitGroup.units == null || unitGroup.units.Length != 1)
        return;
      UnitParam unitParam = this.Master.GetUnitParam(unitGroup.units[0]);
      this.mCardNextSkinDesc.set_text(LocalizedText.Get("sys.CONCEPT_CARD_SKIN_DESCRIPTION", (object) unitParam.name, (object) artifactParam.name));
      this.LoadImage(AssetPath.UnitSkinIconSmall(unitParam, artifactParam, (string) null), this.mCardSkinIcon);
    }
  }
}
