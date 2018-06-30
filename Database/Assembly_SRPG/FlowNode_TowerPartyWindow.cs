namespace SRPG
{
    using System;

    [NodeType("UI/TowerPartyWindow")]
    public class FlowNode_TowerPartyWindow : FlowNode_GUI
    {
        public PartyWindow2.EditPartyTypes PartyType;
        public bool ShowQuestInfo;
        public TriBool BackButton;
        public TriBool ForwardButton;
        public TriBool ShowRaidInfo;

        public FlowNode_TowerPartyWindow()
        {
            this.ShowQuestInfo = 1;
            base..ctor();
            return;
        }

        protected override void OnCreatePinActive()
        {
            TowerPartyWindow window;
            if ((base.Instance != null) == null)
            {
                goto Label_003C;
            }
            window = base.Instance.GetComponent<TowerPartyWindow>();
            if ((window != null) == null)
            {
                goto Label_003B;
            }
            window.Reopen(0);
            GameParameter.UpdateAll(window.get_gameObject());
        Label_003B:
            return;
        Label_003C:
            base.OnCreatePinActive();
            return;
        }

        protected override void OnInstanceCreate()
        {
            TowerPartyWindow window;
            base.OnInstanceCreate();
            window = base.Instance.GetComponentInChildren<TowerPartyWindow>();
            if ((window == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            window.PartyType = this.PartyType;
            window.ShowQuestInfo = this.ShowQuestInfo;
            if (this.BackButton == null)
            {
                goto Label_0051;
            }
            window.ShowBackButton = this.BackButton == 2;
        Label_0051:
            if (this.ForwardButton == null)
            {
                goto Label_006B;
            }
            window.ShowForwardButton = this.ForwardButton == 2;
        Label_006B:
            if (this.ShowRaidInfo == null)
            {
                goto Label_0085;
            }
            window.ShowRaidInfo = this.ShowRaidInfo == 2;
        Label_0085:
            return;
        }

        public enum TriBool
        {
            Unchanged,
            False,
            True
        }
    }
}

