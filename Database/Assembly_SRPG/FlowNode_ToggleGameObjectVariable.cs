namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Output", 1, 2), Pin(10, "Variable", 0, 0), NodeType("Toggle/GameObjectVariable", 0x7fe5)]
    public class FlowNode_ToggleGameObjectVariable : FlowNode
    {
        [DropTarget(typeof(GameObject), true), ShowInInfo]
        public GameObject Target;
        public VariableType variable_type;
        public EnableType enable_type;

        public FlowNode_ToggleGameObjectVariable()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            this.SetupVisible();
            base.ActivateOutputLinks(1);
            return;
        }

        public void SetupVisible()
        {
            bool flag;
            VariableType type;
            flag = 0;
            switch (this.variable_type)
            {
                case 0:
                    goto Label_0024;

                case 1:
                    goto Label_0025;

                case 2:
                    goto Label_0045;

                case 3:
                    goto Label_004C;
            }
            goto Label_0053;
        Label_0024:
            return;
        Label_0025:
            if (GlobalVars.SelectedSection.Get().Equals("WD_DAILY") == null)
            {
                goto Label_0053;
            }
            flag = 1;
            goto Label_0053;
        Label_0045:
            flag = 0;
            goto Label_0053;
        Label_004C:
            flag = 1;
        Label_0053:
            if ((this.Target != null) == null)
            {
                goto Label_0070;
            }
            this.Target.SetActive(flag);
        Label_0070:
            return;
        }

        public enum EnableType
        {
            None,
            IsEventQuest,
            Disable,
            Enable
        }

        public enum VariableType
        {
            None,
            IsEventQuest,
            Hide,
            Show
        }
    }
}

