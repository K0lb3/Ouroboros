namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ToggleAlchemicPower : AnimEvent
    {
        private static readonly Color SceneFadeColor;
        public bool Invert;

        static ToggleAlchemicPower()
        {
            SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
            return;
        }

        public ToggleAlchemicPower()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            this.SetRenderMode(go, (float) ((this.Invert == null) ? 1 : 0));
            return;
        }

        public override void OnStart(GameObject go)
        {
            this.SetRenderMode(go, (float) ((this.Invert == null) ? 0 : 1));
            return;
        }

        public override void OnTick(GameObject go, float ratio)
        {
            this.SetRenderMode(go, (this.Invert == null) ? ratio : (1f - ratio));
            return;
        }

        private unsafe void SetRenderMode(GameObject go, float strength)
        {
            UnitController controller;
            TacticsUnitController[] controllerArray;
            SceneBattle battle;
            controller = go.GetComponentInParent<UnitController>();
            if ((controller != null) == null)
            {
                goto Label_001F;
            }
            controller.AnimateVessel(strength, 0f);
        Label_001F:
            controllerArray = null;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0057;
            }
            controllerArray = SceneBattle.Instance.GetActiveUnits();
            Array.Resize<TacticsUnitController>(&controllerArray, ((int) controllerArray.Length) + 1);
            controllerArray[((int) controllerArray.Length) - 1] = (TacticsUnitController) controller;
        Label_0057:
            FadeController.Instance.BeginSceneFade(Color.Lerp(Color.get_white(), SceneFadeColor, strength), 0f, controllerArray, null);
            return;
        }
    }
}

