// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class ConceptCardSlots : MonoBehaviour
  {
    private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
    [SerializeField]
    private GenericSlot mConceptCardSlot;
    [SerializeField]
    private ConceptCardIcon mConceptCardIcon;
    [SerializeField]
    private GameObject mToolTipRoot;
    private UnitData mUnit;
    private bool mIsButtonEnable;

    public ConceptCardSlots()
    {
      base.\u002Ector();
    }

    public void Refresh(bool enable)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) ((Component) this).get_transform().get_parent()).get_gameObject(), (UnitData) null);
      if (dataOfClass != null)
        this.mUnit = dataOfClass;
      if (this.mUnit == null)
        return;
      bool is_locked = false;
      this.mIsButtonEnable = enable;
      this.RefreshSlots(this.mConceptCardSlot, this.mConceptCardIcon, this.mUnit.ConceptCard, is_locked, this.mIsButtonEnable);
    }

    private void RefreshSlots(GenericSlot slot, ConceptCardIcon icon, ConceptCardData card, bool is_locked, bool enable)
    {
      if (Object.op_Equality((Object) slot, (Object) null) || Object.op_Equality((Object) icon, (Object) null))
        return;
      slot.SetLocked(is_locked);
      slot.SetSlotData<ConceptCardData>(card);
      icon.Setup(card);
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) slot).get_gameObject().GetComponentInChildren<SRPG_Button>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      ((Behaviour) componentInChildren).set_enabled(enable);
      ((UnityEventBase) componentInChildren.get_onClick()).RemoveAllListeners();
      // ISSUE: method pointer
      ((UnityEvent) componentInChildren.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnEquipCardSlot)));
    }

    private void OnEquipCardSlot()
    {
      if (this.mUnit == null)
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(ConceptCardSlots.CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
      if (Object.op_Equality((Object) gameObject1, (Object) null))
        return;
      GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
      ConceptCardEquipWindow component = (ConceptCardEquipWindow) gameObject2.GetComponent<ConceptCardEquipWindow>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Init(this.mUnit);
      ((DestroyEventListener) gameObject2.AddComponent<DestroyEventListener>()).Listeners += (DestroyEventListener.DestroyEvent) (go => this.OnCloseEquipConceptCardWindow());
    }

    private void OnCloseEquipConceptCardWindow()
    {
      if (Object.op_Equality((Object) this.mToolTipRoot, (Object) null))
        return;
      this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnit.UniqueID);
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.mToolTipRoot, (UnitData) null);
      dataOfClass.ConceptCard = this.mUnit.ConceptCard;
      dataOfClass.CalcStatus();
      this.Refresh(this.mIsButtonEnable);
      GameParameter.UpdateAll(this.mToolTipRoot);
    }
  }
}
