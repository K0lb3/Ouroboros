namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Pin(0x3e9, "非売品は無い", 1, 0x3e9), Pin(0x3e8, "非売品が存在する", 1, 0x3e8), Pin(10, "入力", 0, 10), NodeType("UI/ConceptCardNoSaleCheck", 0x7fe5)]
    public class FlowNode_ConceptCardNoSaleCheck : FlowNode
    {
        private const int INPUT_CHECK = 10;
        private const int OUTPUT_EXIST = 0x3e8;
        private const int OUTPUT_NO_EXIST = 0x3e9;

        public FlowNode_ConceptCardNoSaleCheck()
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
                goto Label_0083;
            }
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            enumerator = manager.SelectedMaterials.GetList().GetEnumerator();
        Label_002C:
            try
            {
                goto Label_005A;
            Label_0031:
                data = &enumerator.Current;
                if (data.Param.not_sale == null)
                {
                    goto Label_005A;
                }
                base.ActivateOutputLinks(0x3e8);
                goto Label_0083;
            Label_005A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0031;
                }
                goto Label_0077;
            }
            finally
            {
            Label_006B:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_0077:
            base.ActivateOutputLinks(0x3e9);
        Label_0083:
            return;
        }
    }
}

