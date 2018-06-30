namespace SRPG
{
    using System;

    [NodeType("UI/PartyWindow")]
    public class FlowNode_PartyWindow : FlowNode_GUI
    {
        public PartyWindow2.EditPartyTypes PartyType;
        public bool ShowQuestInfo;
        public bool UseQuest;
        public bool ForceRefresh;
        public TriBool BackButton;
        public TriBool ForwardButton;
        public TriBool ShowRaidInfo;
        public TriBool EnableTeamAssign;

        public FlowNode_PartyWindow()
        {
            this.ShowQuestInfo = 1;
            this.UseQuest = 1;
            base..ctor();
            return;
        }

        private void OffCanvas(PartyWindow2 pw)
        {
            PartyWindow2 window;
            if (this.PartyType != 9)
            {
                goto Label_0031;
            }
            window = pw.GetComponent<PartyWindow2>();
            if ((window != null) == null)
            {
                goto Label_0031;
            }
            window.MainRect.get_gameObject().SetActive(0);
        Label_0031:
            return;
        }

        protected override void OnCreatePinActive()
        {
            PartyWindow2 window;
            if ((base.Instance != null) == null)
            {
                goto Label_003D;
            }
            window = base.Instance.GetComponent<PartyWindow2>();
            if ((window != null) == null)
            {
                goto Label_003C;
            }
            this.OffCanvas(window);
            window.Reopen(this.ForceRefresh);
        Label_003C:
            return;
        Label_003D:
            base.OnCreatePinActive();
            return;
        }

        protected override void OnInstanceCreate()
        {
            PartyWindow2 window;
            base.OnInstanceCreate();
            window = base.Instance.GetComponentInChildren<PartyWindow2>();
            if ((window == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            window.PartyType = this.PartyType;
            window.ShowQuestInfo = this.ShowQuestInfo;
            window.UseQuestInfo = this.UseQuest;
            if (this.BackButton == null)
            {
                goto Label_005D;
            }
            window.ShowBackButton = this.BackButton == 2;
        Label_005D:
            if (this.ForwardButton == null)
            {
                goto Label_0077;
            }
            window.ShowForwardButton = this.ForwardButton == 2;
        Label_0077:
            if (this.ShowRaidInfo == null)
            {
                goto Label_0091;
            }
            window.ShowRaidInfo = this.ShowRaidInfo == 2;
        Label_0091:
            if (this.EnableTeamAssign == null)
            {
                goto Label_00AB;
            }
            window.EnableTeamAssign = this.EnableTeamAssign == 2;
        Label_00AB:
            this.OffCanvas(window);
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

