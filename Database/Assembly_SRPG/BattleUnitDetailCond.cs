namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleUnitDetailCond : MonoBehaviour
    {
        public ImageArray ImageCond;
        public Text TextValue;
        private string[] mStrShieldDesc;

        public BattleUnitDetailCond()
        {
            string[] textArray1;
            textArray1 = new string[] { string.Empty, "quest.BUD_COND_SHIELD_DETAIL_COUNT", "quest.BUD_COND_SHIELD_DETAIL_HP", "quest.BUD_COND_SHIELD_DETAIL_LIMIT" };
            this.mStrShieldDesc = textArray1;
            base..ctor();
            return;
        }

        public void SetCond(EUnitCondition cond)
        {
            List<EUnitCondition> list;
            int num;
            list = new List<EUnitCondition>((EUnitCondition[]) Enum.GetValues(typeof(EUnitCondition)));
            num = list.IndexOf(cond);
            if (this.ImageCond == null)
            {
                goto Label_0058;
            }
            if (num < 0)
            {
                goto Label_0058;
            }
            if (num >= ((int) this.ImageCond.Images.Length))
            {
                goto Label_0058;
            }
            this.ImageCond.ImageIndex = num;
        Label_0058:
            if (this.TextValue == null)
            {
                goto Label_008E;
            }
            if (num < 0)
            {
                goto Label_008E;
            }
            if (num >= ((int) Unit.StrNameUnitConds.Length))
            {
                goto Label_008E;
            }
            this.TextValue.set_text(Unit.StrNameUnitConds[num]);
        Label_008E:
            return;
        }

        public void SetCondShield(ShieldTypes s_type, int val)
        {
            List<EUnitCondition> list;
            int num;
            string str;
            list = new List<EUnitCondition>((EUnitCondition[]) Enum.GetValues(typeof(EUnitCondition)));
            num = list.Count;
            if (this.ImageCond == null)
            {
                goto Label_0057;
            }
            if (num < 0)
            {
                goto Label_0057;
            }
            if (num >= ((int) this.ImageCond.Images.Length))
            {
                goto Label_0057;
            }
            this.ImageCond.ImageIndex = num;
        Label_0057:
            if (this.TextValue == null)
            {
                goto Label_009B;
            }
            str = string.Format(LocalizedText.Get(this.mStrShieldDesc[s_type]), (int) val);
            this.TextValue.set_text(string.Format(LocalizedText.Get("quest.BUD_COND_SHIELD_DETAIL"), str));
        Label_009B:
            return;
        }
    }
}

