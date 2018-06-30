namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class SkillPowerUpResultContentStatus : MonoBehaviour
    {
        [SerializeField]
        private Text paramNameText;
        [SerializeField]
        private Text prevParamText;
        [SerializeField]
        private Text resultParamText;
        [SerializeField]
        private Text resultAddedParamText;

        public SkillPowerUpResultContentStatus()
        {
            base..ctor();
            return;
        }

        public unsafe void SetData(SkillPowerUpResultContent.Param param, ParamTypes type, bool isScale)
        {
            object[] objArray2;
            object[] objArray1;
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            str = (isScale == null) ? string.Empty : "%";
            num = (isScale == null) ? param.currentParam[type] : param.currentParamMul[type];
            num2 = (isScale == null) ? param.prevParam[type] : param.prevParamMul[type];
            num3 = (isScale == null) ? param.currentParamBonus[type] : param.currentParamBonusMul[type];
            num4 = (isScale == null) ? param.prevParamBonus[type] : param.prevParamBonusMul[type];
            num5 = num2 + num4;
            num6 = num + num3;
            this.paramNameText.set_text(LocalizedText.Get("sys." + ((ParamTypes) type)));
            this.prevParamText.set_text(&num5.ToString() + str);
            this.resultParamText.set_text(&num6.ToString() + str);
            num7 = num3;
            if (num7 < 0)
            {
                goto Label_015A;
            }
            objArray1 = new object[] { "(+", (int) num7, str, ")" };
            this.resultAddedParamText.set_text(string.Concat(objArray1));
            goto Label_018E;
        Label_015A:
            objArray2 = new object[] { "(", (int) num7, str, ")" };
            this.resultAddedParamText.set_text(string.Concat(objArray2));
        Label_018E:
            return;
        }
    }
}

