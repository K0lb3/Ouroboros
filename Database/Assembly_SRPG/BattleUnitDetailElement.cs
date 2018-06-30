namespace SRPG
{
    using System;
    using UnityEngine;

    public class BattleUnitDetailElement : MonoBehaviour
    {
        public ImageArray ImageElement;
        public ImageArray ImageFluct;

        public BattleUnitDetailElement()
        {
            base..ctor();
            return;
        }

        public void SetElement(EElement elem, BattleUnitDetail.eBudFluct fluct)
        {
            int num;
            int num2;
            if (this.ImageElement == null)
            {
                goto Label_0038;
            }
            num = elem;
            if (num < 0)
            {
                goto Label_0038;
            }
            if (num >= ((int) this.ImageElement.Images.Length))
            {
                goto Label_0038;
            }
            this.ImageElement.ImageIndex = num;
        Label_0038:
            if (this.ImageFluct == null)
            {
                goto Label_0086;
            }
            this.ImageFluct.get_gameObject().SetActive((fluct == 0) == 0);
            if (fluct == null)
            {
                goto Label_0086;
            }
            num2 = fluct;
            if (num2 >= ((int) this.ImageFluct.Images.Length))
            {
                goto Label_0086;
            }
            this.ImageFluct.ImageIndex = num2;
        Label_0086:
            return;
        }
    }
}

