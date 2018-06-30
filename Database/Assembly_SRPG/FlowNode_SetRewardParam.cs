namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Reward/Set", 0x7fe5), Pin(1, "Assign", 0, 1), Pin(9, "Assigned", 1, 9)]
    public class FlowNode_SetRewardParam : FlowNode
    {
        public GameObject target;
        public Type type;

        public FlowNode_SetRewardParam()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ItemParam param;
            AwardParam param2;
            ArtifactParam param3;
            Type type;
            if (pinID != 1)
            {
                goto Label_00AB;
            }
            if ((this.target == null) == null)
            {
                goto Label_0024;
            }
            this.target = base.get_gameObject();
        Label_0024:
            switch (this.type)
            {
                case 0:
                    goto Label_0046;

                case 1:
                    goto Label_00A2;

                case 2:
                    goto Label_0085;

                case 3:
                    goto Label_0063;
            }
            goto Label_00A2;
        Label_0046:
            GlobalVars.SelectedItemID = DataSource.FindDataOfClass<ItemParam>(this.target, null).iname;
            goto Label_00A2;
        Label_0063:
            param2 = DataSource.FindDataOfClass<AwardParam>(this.target, null);
            FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", param2.iname);
            goto Label_00A2;
        Label_0085:
            GlobalVars.SelectedArtifactID = DataSource.FindDataOfClass<ArtifactParam>(this.target, null).iname;
        Label_00A2:
            base.ActivateOutputLinks(9);
        Label_00AB:
            return;
        }

        public enum Type
        {
            Item,
            Unit,
            Artifact,
            Award
        }
    }
}

