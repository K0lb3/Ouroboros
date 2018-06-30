namespace SRPG
{
    using GR;
    using System;

    [Pin(11, "一括強化による合成可能かチェック", 0, 11), Pin(0x3e9, "ゼニー不足", 1, 0x3e9), NodeType("UI/ConceptCardMixCheck", 0x7fe5), Pin(10, "合成可能かチェック", 0, 10), Pin(0x3e8, "合成可能", 1, 0x3e8)]
    public class FlowNode_ConceptCardMixCheck : FlowNode
    {
        private const int INPUT_MIX_CHECK = 10;
        private const int INPUT_BULK_MIX_CHECK = 11;
        private const int OUTPUT_MIX_OK = 0x3e8;
        private const int OUTPUT_MIX_ZENY_NG = 0x3e9;

        public FlowNode_ConceptCardMixCheck()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            ConceptCardManager manager;
            int num;
            if (pinID == 10)
            {
                goto Label_0010;
            }
            if (pinID != 11)
            {
                goto Label_0074;
            }
        Label_0010:
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            num = 0;
            if (pinID != 10)
            {
                goto Label_003F;
            }
            ConceptCardManager.GalcTotalMixZeny(manager.SelectedMaterials, &num);
            goto Label_0046;
        Label_003F:
            ConceptCardManager.GalcTotalMixZenyMaterialData(&num);
        Label_0046:
            if (num <= MonoSingleton<GameManager>.Instance.Player.Gold)
            {
                goto Label_0068;
            }
            base.ActivateOutputLinks(0x3e9);
            return;
        Label_0068:
            base.ActivateOutputLinks(0x3e8);
        Label_0074:
            return;
        }
    }
}

