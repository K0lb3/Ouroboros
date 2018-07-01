// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitIconEx : UnitIcon
  {
    private const string DefaultTootTipPath = "UI/UnitTooltip.prefab";
    public string GeneralTooltipPath;

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab"));
      this.BindData(gameObject, instanceData);
      GameParameter.UpdateAll(gameObject);
    }

    private void BindData(GameObject go, UnitData unitData)
    {
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(go, PlayerPartyTypes.Max);
      DataSource.Bind<UnitData>(go, unitData);
      DataSource.Bind<PlayerPartyTypes>(go, dataOfClass);
      UnitJobDropdown componentInChildren1 = (UnitJobDropdown) go.GetComponentInChildren<UnitJobDropdown>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
      {
        bool flag = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        ((Component) componentInChildren1).get_gameObject().SetActive(true);
        componentInChildren1.UpdateValue = (UnitJobDropdown.ParentObjectEvent) null;
        Selectable component1 = (Selectable) ((Component) componentInChildren1).get_gameObject().GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.set_interactable(flag);
        Image component2 = (Image) ((Component) componentInChildren1).get_gameObject().GetComponent<Image>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Graphic) component2).set_color(!flag ? new Color(0.5f, 0.5f, 0.5f) : Color.get_white());
      }
      ArtifactSlots componentInChildren2 = (ArtifactSlots) go.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = (AbilitySlots) go.GetComponentInChildren<AbilitySlots>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null) && Object.op_Inequality((Object) componentInChildren3, (Object) null))
      {
        bool enable = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      ConceptCardSlots componentInChildren4 = (ConceptCardSlots) go.GetComponentInChildren<ConceptCardSlots>();
      if (!Object.op_Inequality((Object) componentInChildren4, (Object) null))
        return;
      bool enable1 = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
      componentInChildren4.Refresh(enable1);
    }
  }
}
