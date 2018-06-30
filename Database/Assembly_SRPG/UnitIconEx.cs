namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitIconEx : UnitIcon
    {
        private const string DefaultTootTipPath = "UI/UnitTooltip.prefab";
        public string GeneralTooltipPath;

        public UnitIconEx()
        {
            base..ctor();
            return;
        }

        private void BindData(GameObject go, UnitData unitData)
        {
            PlayerPartyTypes types;
            UnitJobDropdown dropdown;
            bool flag;
            Selectable selectable;
            Image image;
            ArtifactSlots slots;
            AbilitySlots slots2;
            bool flag2;
            ConceptCardSlots slots3;
            bool flag3;
            types = DataSource.FindDataOfClass<PlayerPartyTypes>(go, 11);
            DataSource.Bind<UnitData>(go, unitData);
            DataSource.Bind<PlayerPartyTypes>(go, types);
            dropdown = go.GetComponentInChildren<UnitJobDropdown>();
            if ((dropdown != null) == null)
            {
                goto Label_00C5;
            }
            flag = (((unitData.TempFlags & 2) == null) || (base.AllowJobChange == null)) ? 0 : ((types == 11) == 0);
            dropdown.get_gameObject().SetActive(1);
            dropdown.UpdateValue = null;
            selectable = dropdown.get_gameObject().GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0080;
            }
            selectable.set_interactable(flag);
        Label_0080:
            image = dropdown.get_gameObject().GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00C5;
            }
            image.set_color((flag == null) ? new Color(0.5f, 0.5f, 0.5f) : Color.get_white());
        Label_00C5:
            slots = go.GetComponentInChildren<ArtifactSlots>();
            slots2 = go.GetComponentInChildren<AbilitySlots>();
            if (((slots != null) == null) || ((slots2 != null) == null))
            {
                goto Label_0126;
            }
            flag2 = (((unitData.TempFlags & 2) == null) || (base.AllowJobChange == null)) ? 0 : ((types == 11) == 0);
            slots.Refresh(flag2);
            slots2.Refresh(flag2);
        Label_0126:
            slots3 = go.GetComponentInChildren<ConceptCardSlots>();
            if ((slots3 != null) == null)
            {
                goto Label_0169;
            }
            flag3 = (((unitData.TempFlags & 2) == null) || (base.AllowJobChange == null)) ? 0 : ((types == 11) == 0);
            slots3.Refresh(flag3);
        Label_0169:
            return;
        }

        protected override void ShowTooltip(Vector2 screen)
        {
            UnitData data;
            string str;
            GameObject obj2;
            GameObject obj3;
            if (base.Tooltip == null)
            {
                goto Label_0055;
            }
            data = this.GetInstanceData();
            if (data == null)
            {
                goto Label_0055;
            }
            str = (string.IsNullOrEmpty(this.GeneralTooltipPath) == null) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab";
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(str));
            this.BindData(obj3, data);
            GameParameter.UpdateAll(obj3);
        Label_0055:
            return;
        }
    }
}

