// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhancePanel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
    [Space(10f)]
    public GenericSlot ArtifactSlot;
    [Space(10f)]
    public GenericSlot ArtifactSlot2;
    [Space(10f)]
    public GenericSlot ArtifactSlot3;
    [Space(10f)]
    public RectTransform ExpItemList;
    public ListItemEvents ExpItemTemplate;
    [Space(10f)]
    public UnitAbilityList AbilityList;
    [Space(10f)]
    public UnitAbilityList AbilitySlots;

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
