namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Pin(0x3e8, "指定レア値以上", 1, 0x3e8), Pin(0x3e9, "指定レア値より少ない", 1, 0x3e9), NodeType("UI/ConceptCardRarityCheck", 0x7fe5), Pin(10, "入力", 0, 10)]
    public class FlowNode_ConceptCardRarityCheck : FlowNode
    {
        private const int INPUT_CHECK = 10;
        private const int OUTPUT_HIGH = 0x3e8;
        private const int OUTPUT_LOW = 0x3e9;
        public int Rarity;

        public FlowNode_ConceptCardRarityCheck()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            ConceptCardManager manager;
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            if (pinID != 10)
            {
                goto Label_0097;
            }
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            manager.CostConceptCardRare = this.Rarity;
            enumerator = manager.SelectedMaterials.GetList().GetEnumerator();
        Label_0038:
            try
            {
                goto Label_006E;
            Label_003D:
                data = &enumerator.Current;
                if ((data.Rarity + 1) < this.Rarity)
                {
                    goto Label_006E;
                }
                base.ActivateOutputLinks(0x3e8);
                goto Label_0097;
            Label_006E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003D;
                }
                goto Label_008B;
            }
            finally
            {
            Label_007F:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_008B:
            base.ActivateOutputLinks(0x3e9);
        Label_0097:
            return;
        }
    }
}

