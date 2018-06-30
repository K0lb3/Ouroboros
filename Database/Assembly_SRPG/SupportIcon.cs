namespace SRPG
{
    using System;
    using UnityEngine;

    public class SupportIcon : UnitIcon
    {
        private const string TooltipPath = "UI/SupportTooltip";
        public bool UseSelection;

        public SupportIcon()
        {
            base..ctor();
            return;
        }

        protected override UnitData GetInstanceData()
        {
            SupportData data;
            data = this.GetSupportData();
            if (data == null)
            {
                goto Label_0018;
            }
            if (data.Unit != null)
            {
                goto Label_001A;
            }
        Label_0018:
            return null;
        Label_001A:
            return data.Unit;
        }

        private SupportData GetSupportData()
        {
            if (this.UseSelection == null)
            {
                goto Label_0016;
            }
            return GlobalVars.SelectedSupport;
        Label_0016:
            return DataSource.FindDataOfClass<SupportData>(base.get_gameObject(), null);
        }

        protected override void ShowTooltip(Vector2 screen)
        {
            SupportData data;
            GameObject obj2;
            GameObject obj3;
            if (base.Tooltip == null)
            {
                goto Label_0048;
            }
            data = this.GetSupportData();
            if (data == null)
            {
                goto Label_0048;
            }
            if (data.Unit == null)
            {
                goto Label_0048;
            }
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/SupportTooltip"));
            DataSource.Bind<UnitData>(obj3, data.Unit);
            DataSource.Bind<SupportData>(obj3, data);
        Label_0048:
            return;
        }
    }
}

