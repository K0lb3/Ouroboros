namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(100, "完了", 1, 100), Pin(10, "魂の欠片に変換", 0, 10)]
    public class KakeraShopWindow : MonoBehaviour, IFlowInterface
    {
        public KakeraShopWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_000F;
            }
            goto Label_001A;
        Label_000F:
            this.OnConvert();
        Label_001A:
            return;
        }

        private void OnConvert()
        {
            if (GlobalVars.ConvertAwakePieceList != null)
            {
                goto Label_0019;
            }
            GlobalVars.ConvertAwakePieceList = new List<SellItem>();
            goto Label_0023;
        Label_0019:
            GlobalVars.ConvertAwakePieceList.Clear();
        Label_0023:
            GlobalVars.ConvertAwakePieceList.AddRange(GlobalVars.SellItemList);
            GlobalVars.SellItemList.Clear();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }
    }
}

