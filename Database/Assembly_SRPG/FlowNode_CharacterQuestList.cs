namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("UI/CharacterQuestList")]
    public class FlowNode_CharacterQuestList : FlowNode_GUI
    {
        public FlowNode_CharacterQuestList()
        {
            base..ctor();
            return;
        }

        private void OnBack(GameObject go, bool visible)
        {
            UnitCharacterQuestWindow window;
            WindowController controller;
            if (visible == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            window = base.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
            if ((window == null) != null)
            {
                goto Label_0025;
            }
            if (visible == null)
            {
                goto Label_0026;
            }
        Label_0025:
            return;
        Label_0026:
            window.GetComponent<WindowController>().SetCollision(1);
            window.GetComponent<WindowController>().OnWindowStateChange = null;
            Object.Destroy(window.get_gameObject());
            return;
        }

        protected override void OnInstanceCreate()
        {
            UnitCharacterQuestWindow window;
            base.OnInstanceCreate();
            window = base.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
            if ((window == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            window.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.PreBattleUnitUniqueID);
            window.GetComponent<WindowController>().SetCollision(0);
            window.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
            WindowController.OpenIfAvailable(window);
            return;
        }
    }
}

