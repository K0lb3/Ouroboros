// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResultContentStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
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
      base.\u002Ector();
    }

    public void SetData(SkillPowerUpResultContent.Param param, ParamTypes type, bool isScale)
    {
      string str = !isScale ? string.Empty : "%";
      int num1 = (int) (!isScale ? param.currentParam[type] : param.currentParamMul[type]);
      int num2 = (int) (!isScale ? param.prevParam[type] : param.prevParamMul[type]);
      int num3 = (int) (!isScale ? param.currentParamBonus[type] : param.currentParamBonusMul[type]);
      int num4 = (int) (!isScale ? param.prevParamBonus[type] : param.prevParamBonusMul[type]);
      int num5 = num2 + num4;
      int num6 = num1 + num3;
      this.paramNameText.set_text(LocalizedText.Get("sys." + (object) type));
      this.prevParamText.set_text(num5.ToString() + str);
      this.resultParamText.set_text(num6.ToString() + str);
      int num7 = num3;
      if (num7 >= 0)
        this.resultAddedParamText.set_text("(+" + (object) num7 + str + ")");
      else
        this.resultAddedParamText.set_text("(" + (object) num7 + str + ")");
    }
  }
}
