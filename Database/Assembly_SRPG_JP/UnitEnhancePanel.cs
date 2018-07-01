// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhancePanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitEnhancePanel : MonoBehaviour
  {
    public UnitEquipmentSlotEvents[] EquipmentSlots;
    public SRPG_Button JobRankUpButton;
    public SRPG_Button JobUnlockButton;
    public SRPG_Button AllEquipButton;
    public GameObject JobRankCapCaution;
    public SRPG_Button JobRankupAllIn;
    [Space(10f)]
    public GenericSlot ArtifactSlot;
    [Space(10f)]
    public GenericSlot ArtifactSlot2;
    [Space(10f)]
    public GenericSlot ArtifactSlot3;
    [Space(10f)]
    public RectTransform ExpItemList;
    public ListItemEvents ExpItemTemplate;
    public SRPG_Button UnitLevelupButton;
    [Space(10f)]
    public UnitAbilityList AbilityList;
    [Space(10f)]
    public UnitAbilityList AbilitySlots;
    [Space(10f)]
    public GenericSlot mConceptCardSlot;
    public ConceptCardIcon mEquipConceptCardIcon;

    public UnitEnhancePanel()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      Canvas component = (Canvas) ((Component) this).GetComponent<Canvas>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).set_enabled(false);
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.ExpItemTemplate, (Object) null))
        return;
      ((Component) this.ExpItemTemplate).get_gameObject().SetActive(false);
    }
  }
}
