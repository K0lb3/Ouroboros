namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(2, "Start(Mission)", 0, 2), Pin(12, "Chara(Restore or Mission)", 1, 12), Pin(1, "Start(Restore)", 0, 1), Pin(0, "Start", 0, 0), Pin(3, "Refresh", 0, 3), Pin(13, "Collabo(Restore or Mission)", 1, 13), Pin(10, "Chara", 1, 10), Pin(11, "Collabo", 1, 11)]
    public class CharacterQuestController : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_START = 0;
        private const int PIN_ID_START_RESTORE = 1;
        private const int PIN_ID_START_MISSION = 2;
        private const int PIN_ID_REFRESH = 3;
        private const int PIN_ID_CHARA = 10;
        private const int PIN_ID_COLLABO = 11;
        private const int PIN_ID_CHARA_RESTORE_OR_MISSION = 12;
        private const int PIN_ID_COLLABO_RESTORE_OR_MISSION = 13;
        public string VARIABLE_KEY;
        public string VARIABLE_VALUE_CHARA;
        public string VARIABLE_VALUE_COLLABO;

        public CharacterQuestController()
        {
            this.VARIABLE_KEY = "CHARA_QUEST_TYPE";
            this.VARIABLE_VALUE_CHARA = "CHARA";
            this.VARIABLE_VALUE_COLLABO = "COLLABO";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            string str;
            string str2;
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_003B;

                case 2:
                    goto Label_003B;

                case 3:
                    goto Label_00AC;
            }
            goto Label_011D;
        Label_001D:
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_011D;
        Label_003B:
            str = FlowNode_Variable.Get(this.VARIABLE_KEY);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0070;
            }
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_00A7;
        Label_0070:
            if (string.Equals(str, this.VARIABLE_VALUE_CHARA) == null)
            {
                goto Label_008E;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            goto Label_00A7;
        Label_008E:
            if (string.Equals(str, this.VARIABLE_VALUE_COLLABO) == null)
            {
                goto Label_011D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 13);
        Label_00A7:
            goto Label_011D;
        Label_00AC:
            str2 = FlowNode_Variable.Get(this.VARIABLE_KEY);
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_00E1;
            }
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0118;
        Label_00E1:
            if (string.Equals(str2, this.VARIABLE_VALUE_CHARA) == null)
            {
                goto Label_00FF;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0118;
        Label_00FF:
            if (string.Equals(str2, this.VARIABLE_VALUE_COLLABO) == null)
            {
                goto Label_011D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
        Label_0118:;
        Label_011D:
            return;
        }
    }
}

