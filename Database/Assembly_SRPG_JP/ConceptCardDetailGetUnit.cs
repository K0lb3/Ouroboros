// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailGetUnit : ConceptCardDetailBase
  {
    [SerializeField]
    private RawImage UnitIcon;
    [SerializeField]
    private Text UnitName;
    [SerializeField]
    private ButtonEvent UnitDetailBtn;

    public override void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      string firstGetUnit = this.mConceptCardData.Param.first_get_unit;
      if (string.IsNullOrEmpty(firstGetUnit))
        return;
      UnitParam unitParam = this.GM.GetUnitParam(firstGetUnit);
      if (unitParam == null)
        return;
      if (Object.op_Inequality((Object) this.UnitIcon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.UnitIcon, unitParam == null ? (string) null : AssetPath.UnitSkinIconSmall(unitParam, (ArtifactParam) null, (string) null));
      if (Object.op_Inequality((Object) this.UnitName, (Object) null))
        this.UnitName.set_text(unitParam.name);
      if (!Object.op_Inequality((Object) this.UnitDetailBtn, (Object) null))
        return;
      ButtonEvent.Event @event = this.UnitDetailBtn.GetEvent("CONCEPT_CARD_DETAIL_BTN_UNIT_DETAIL");
      if (@event == null)
        return;
      @event.valueList.SetField("select_unit", unitParam.iname);
    }
  }
}
