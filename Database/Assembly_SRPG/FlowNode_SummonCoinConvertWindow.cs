namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("UI/SummonCoinConvertWindow")]
    public class FlowNode_SummonCoinConvertWindow : FlowNode_GUI
    {
        [SerializeField]
        private GachaCoinChangeWindow.CoinType coinType;

        public FlowNode_SummonCoinConvertWindow()
        {
            base..ctor();
            return;
        }

        protected override void OnCreatePinActive()
        {
            GachaCoinChangeWindow window;
            base.OnCreatePinActive();
            if ((base.Instance != null) == null)
            {
                goto Label_003D;
            }
            window = base.Instance.GetComponentInChildren<GachaCoinChangeWindow>(1);
            if ((window != null) == null)
            {
                goto Label_003C;
            }
            window.Refresh(this.coinType);
        Label_003C:
            return;
        Label_003D:
            return;
        }
    }
}

