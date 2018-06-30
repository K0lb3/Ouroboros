namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(4, "Unit Select End", 1, 4), Pin(1, "Unit Selected", 1, 1), Pin(2, "EndRefresh", 1, 2), Pin(3, "Unit Select", 0, 3), Pin(0, "Refresh", 0, 0)]
    public class SupportSet : MonoBehaviour, IFlowInterface
    {
        public SupportList UnitList;
        public GameObject Parent;
        public RectTransform UnitListHilit;
        private long SelectedUniqueId;

        public SupportSet()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0018;
            }
            this.UnitList.RefreshData();
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_0018:
            if (pinID != 3)
            {
                goto Label_0036;
            }
            GlobalVars.SelectedSupportUnitUniqueID.Set(this.SelectedUniqueId);
            FlowNode_GameObject.ActivateOutputLinks(this, 4);
        Label_0036:
            return;
        }

        private void Start()
        {
            this.UnitList.OnUnitSelect = new UnitListV2.UnitSelectEvent(this.UnitSelect);
            return;
        }

        public void UnitSelect(long uniqueID)
        {
            UnitData data;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqueID);
            DataSource.Bind<UnitData>(this.Parent, data);
            this.SelectedUniqueId = uniqueID;
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            GameParameter.UpdateAll(this.Parent);
            return;
        }

        private void Update()
        {
        }

        private List<UnitData> mOwnUnits
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.Units;
            }
        }
    }
}

