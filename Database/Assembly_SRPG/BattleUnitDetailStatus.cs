namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleUnitDetailStatus : MonoBehaviour
    {
        public Text StatusValue;
        public Text StatusValueUp;
        public Text StatusValueDown;
        public GameObject GoUpBG;
        public GameObject GoDownBG;
        public Text UpDownValue;

        public BattleUnitDetailStatus()
        {
            base..ctor();
            return;
        }

        public unsafe void SetStatus(eBudStat stat, int val, int add)
        {
            int num;
            ImageArray array;
            num = stat;
            if (num < 0)
            {
                goto Label_0031;
            }
            array = base.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_0031;
            }
            if (num >= ((int) array.Images.Length))
            {
                goto Label_0031;
            }
            array.ImageIndex = num;
        Label_0031:
            if (this.StatusValue == null)
            {
                goto Label_0052;
            }
            this.StatusValue.get_gameObject().SetActive(0);
        Label_0052:
            if (this.StatusValueUp == null)
            {
                goto Label_0073;
            }
            this.StatusValueUp.get_gameObject().SetActive(0);
        Label_0073:
            if (this.StatusValueDown == null)
            {
                goto Label_0094;
            }
            this.StatusValueDown.get_gameObject().SetActive(0);
        Label_0094:
            if (this.GoUpBG == null)
            {
                goto Label_00B0;
            }
            this.GoUpBG.SetActive(0);
        Label_00B0:
            if (this.GoDownBG == null)
            {
                goto Label_00CC;
            }
            this.GoDownBG.SetActive(0);
        Label_00CC:
            if (this.UpDownValue == null)
            {
                goto Label_00ED;
            }
            this.UpDownValue.get_gameObject().SetActive(0);
        Label_00ED:
            if (add <= 0)
            {
                goto Label_0185;
            }
            if (this.StatusValueUp == null)
            {
                goto Label_0127;
            }
            this.StatusValueUp.set_text(&val.ToString());
            this.StatusValueUp.get_gameObject().SetActive(1);
        Label_0127:
            if (this.UpDownValue == null)
            {
                goto Label_0164;
            }
            this.UpDownValue.set_text("+" + &add.ToString());
            this.UpDownValue.get_gameObject().SetActive(1);
        Label_0164:
            if (this.GoUpBG == null)
            {
                goto Label_0246;
            }
            this.GoUpBG.SetActive(1);
            goto Label_0246;
        Label_0185:
            if (add >= 0)
            {
                goto Label_0213;
            }
            if (this.StatusValueDown == null)
            {
                goto Label_01BF;
            }
            this.StatusValueDown.set_text(&val.ToString());
            this.StatusValueDown.get_gameObject().SetActive(1);
        Label_01BF:
            if (this.UpDownValue == null)
            {
                goto Label_01F2;
            }
            this.UpDownValue.set_text(&add.ToString());
            this.UpDownValue.get_gameObject().SetActive(1);
        Label_01F2:
            if (this.GoDownBG == null)
            {
                goto Label_0246;
            }
            this.GoDownBG.SetActive(1);
            goto Label_0246;
        Label_0213:
            if (this.StatusValue == null)
            {
                goto Label_0246;
            }
            this.StatusValue.set_text(&val.ToString());
            this.StatusValue.get_gameObject().SetActive(1);
        Label_0246:
            return;
        }

        public enum eBudStat
        {
            MHP,
            MMP,
            ATK,
            DEF,
            MAG,
            MND,
            DEX,
            SPD,
            CRI,
            LUK,
            CMB,
            MOV,
            JMP,
            MAX
        }
    }
}

