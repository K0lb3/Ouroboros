// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SkillPowerUpResult : MonoBehaviour
  {
    [SerializeField]
    private SkillPowerUpResultContent contentBase;
    [SerializeField]
    private Transform contanteParent;
    [SerializeField]
    private int onePageContentsHightMax;
    private List<SkillPowerUpResult.DispParam> dispParamList;
    private List<SkillPowerUpResultContent> contentList;

    public SkillPowerUpResult()
    {
      base.\u002Ector();
    }

    public bool IsEnd
    {
      get
      {
        return this.dispParamList.Count == 0;
      }
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentBase, (UnityEngine.Object) null) || !((Component) this.contentBase).get_gameObject().get_activeInHierarchy())
        return;
      ((Component) this.contentBase).get_gameObject().SetActive(false);
    }

    public void SetData(ConceptCardData currentCardData, int prevAwakeCount, int prevLevel, bool includeMaxPowerUp)
    {
      List<ConceptCardEquipEffect> cardSkills = currentCardData.GetCardSkills();
      int count = cardSkills.Count;
      int lvCap = (int) currentCardData.LvCap;
      int awakeCountCap = currentCardData.AwakeCountCap;
      List<string> stringList = new List<string>();
      for (int index = 0; index < count; ++index)
      {
        SkillData cardSkill = cardSkills[index].CardSkill;
        if (cardSkill != null && !stringList.Contains(cardSkill.Name))
        {
          stringList.Add(cardSkill.Name);
          SkillPowerUpResultContent.Param obj = new SkillPowerUpResultContent.Param(cardSkill, currentCardData, lvCap, awakeCountCap, prevLevel, prevAwakeCount, includeMaxPowerUp);
          if (obj.IsBonusParamChanged())
          {
            SkillPowerUpResult.DispParam dispParam = new SkillPowerUpResult.DispParam();
            dispParam.sourceParam = obj;
            foreach (SkillPowerUpResultContent.DispType dispType in obj.GetAllBonusChangeType())
            {
              if (!dispType.isScale)
                dispParam.dispTypeList.Add(dispType.type);
              else
                dispParam.dispTypeListMul.Add(dispType.type);
            }
            this.dispParamList.Add(dispParam);
          }
        }
      }
    }

    public void ApplyContent()
    {
      int num1 = 0;
      int count1 = 0;
      int count2 = this.dispParamList.Count;
      for (int index1 = 0; index1 < count2; ++index1)
      {
        int num2 = num1 + 1;
        if (num2 < this.onePageContentsHightMax)
        {
          ++count1;
          int count3 = this.dispParamList[index1].dispTypeList.Count;
          int count4 = this.dispParamList[index1].dispTypeListMul.Count;
          num1 = num2 + (count3 + count4);
          if (num1 > this.onePageContentsHightMax)
          {
            int num3 = num1 - this.onePageContentsHightMax;
            if (num3 > 0)
            {
              SkillPowerUpResult.DispParam dispParam1 = new SkillPowerUpResult.DispParam();
              SkillPowerUpResult.DispParam dispParam2 = new SkillPowerUpResult.DispParam();
              dispParam1.sourceParam = this.dispParamList[index1].sourceParam;
              dispParam2.sourceParam = this.dispParamList[index1].sourceParam;
              dispParam1.dispTypeList = new List<ParamTypes>();
              dispParam1.dispTypeListMul = new List<ParamTypes>();
              int num4 = count3 + count4 - num3;
              int num5 = 0;
              int length = Enum.GetValues(typeof (ParamTypes)).Length;
              for (int index2 = 0; index2 < length; ++index2)
              {
                if (index2 != 2)
                {
                  ParamTypes paramTypes = (ParamTypes) index2;
                  if (this.dispParamList[index1].dispTypeList.Contains(paramTypes))
                  {
                    if (num5 < num4)
                    {
                      dispParam1.dispTypeList.Add(paramTypes);
                      ++num5;
                    }
                    else
                      dispParam2.dispTypeList.Add(paramTypes);
                  }
                  if (this.dispParamList[index1].dispTypeListMul.Contains(paramTypes))
                  {
                    if (num5 < num4)
                    {
                      dispParam1.dispTypeListMul.Add(paramTypes);
                      ++num5;
                    }
                    else
                      dispParam2.dispTypeListMul.Add(paramTypes);
                  }
                }
              }
              this.dispParamList[index1] = dispParam1;
              this.dispParamList.Insert(index1 + 1, dispParam2);
              int count5 = this.dispParamList.Count;
              break;
            }
            break;
          }
        }
        else
          break;
      }
      using (List<SkillPowerUpResultContent>.Enumerator enumerator = this.contentList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) enumerator.Current).get_gameObject());
      }
      this.contentList.Clear();
      for (int index = 0; index < count1; ++index)
      {
        SkillPowerUpResultContent powerUpResultContent = (SkillPowerUpResultContent) UnityEngine.Object.Instantiate<SkillPowerUpResultContent>((M0) this.contentBase);
        ((Component) powerUpResultContent).get_transform().SetParent(this.contanteParent, false);
        ((Component) powerUpResultContent).get_gameObject().SetActive(true);
        this.contentList.Add(powerUpResultContent);
      }
      for (int index = 0; index < count1; ++index)
        this.contentList[index].SetData(this.dispParamList[index].sourceParam, this.dispParamList[index].dispTypeList, this.dispParamList[index].dispTypeListMul);
      this.dispParamList.RemoveRange(0, count1);
    }

    private class DispParam
    {
      public List<ParamTypes> dispTypeList = new List<ParamTypes>();
      public List<ParamTypes> dispTypeListMul = new List<ParamTypes>();
      public SkillPowerUpResultContent.Param sourceParam;
    }
  }
}
