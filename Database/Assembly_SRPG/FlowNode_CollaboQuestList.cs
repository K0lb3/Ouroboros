namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("UI/CollaboQuestList")]
    public class FlowNode_CollaboQuestList : FlowNode_GUI
    {
        public FlowNode_CollaboQuestList()
        {
            base..ctor();
            return;
        }

        private void OnBack(GameObject go, bool visible)
        {
            CollaboSkillQuestList list;
            WindowController controller;
            if (visible == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            list = base.Instance.GetComponentInChildren<CollaboSkillQuestList>();
            if ((list == null) != null)
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
            list.GetComponent<WindowController>().SetCollision(1);
            list.GetComponent<WindowController>().OnWindowStateChange = null;
            Object.Destroy(list.get_gameObject());
            return;
        }

        protected override void OnInstanceCreate()
        {
            CollaboSkillQuestList list;
            CollaboSkillParam.Pair pair;
            base.OnInstanceCreate();
            list = base.Instance.GetComponentInChildren<CollaboSkillQuestList>();
            if ((list == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            pair = GlobalVars.SelectedCollaboSkillPair;
            if (pair != null)
            {
                goto Label_0036;
            }
            DebugUtility.LogError("CollaboSkillParam.Pair が セットされていない");
            return;
        Label_0036:
            list.CurrentUnit1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(pair.UnitParam1);
            list.CurrentUnit2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(pair.UnitParam2);
            if (list.CurrentUnit1 != null)
            {
                goto Label_0082;
            }
            DebugUtility.LogError("window.CurrentUnit1 == null");
            return;
        Label_0082:
            if (list.CurrentUnit2 != null)
            {
                goto Label_0098;
            }
            DebugUtility.LogError("window.CurrentUnit2 == null");
            return;
        Label_0098:
            list.GetComponent<WindowController>().SetCollision(0);
            list.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
            WindowController.OpenIfAvailable(list);
            return;
        }
    }
}

