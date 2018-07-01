// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResultContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SkillPowerUpResultContent : MonoBehaviour
  {
    [SerializeField]
    private SkillPowerUpResultContentStatus statusBase;
    [SerializeField]
    private Text skillNameText;
    private List<SkillPowerUpResultContentStatus> contentsList;

    public SkillPowerUpResultContent()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      ((Component) this.statusBase).get_gameObject().SetActive(false);
    }

    public void SetData(SkillPowerUpResultContent.Param param, List<ParamTypes> dispTypeList, List<ParamTypes> dispTypeListMul)
    {
      using (List<SkillPowerUpResultContentStatus>.Enumerator enumerator = this.contentsList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) enumerator.Current).get_gameObject());
      }
      this.contentsList.Clear();
      this.skillNameText.set_text(param.skilName);
      foreach (SkillPowerUpResultContent.DispType dispType in param.GetAllBonusChangeType())
      {
        if (!dispType.isScale && dispTypeList.Contains(dispType.type))
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.statusBase).get_gameObject());
          gameObject.get_transform().SetParent(((Component) this.statusBase).get_transform().get_parent(), false);
          SkillPowerUpResultContentStatus component = (SkillPowerUpResultContentStatus) gameObject.GetComponent<SkillPowerUpResultContentStatus>();
          component.SetData(param, dispType.type, false);
          gameObject.SetActive(true);
          this.contentsList.Add(component);
        }
        if (dispType.isScale && dispTypeListMul.Contains(dispType.type))
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.statusBase).get_gameObject());
          gameObject.get_transform().SetParent(((Component) this.statusBase).get_transform().get_parent(), false);
          SkillPowerUpResultContentStatus component = (SkillPowerUpResultContentStatus) gameObject.GetComponent<SkillPowerUpResultContentStatus>();
          component.SetData(param, dispType.type, true);
          gameObject.SetActive(true);
          this.contentsList.Add(component);
        }
      }
    }

    public class DispType
    {
      public bool isScale;
      public ParamTypes type;

      public DispType(ParamTypes inType, bool inIsScale)
      {
        this.type = inType;
        this.isScale = inIsScale;
      }
    }

    public class Param
    {
      private List<ParamTypes> typeList = new List<ParamTypes>();
      private List<ParamTypes> typeListMul = new List<ParamTypes>();
      public BaseStatus prevParam;
      public BaseStatus prevParamMul;
      public BaseStatus currentParam;
      public BaseStatus currentParamMul;
      public BaseStatus prevParamBonus;
      public BaseStatus prevParamBonusMul;
      public BaseStatus currentParamBonus;
      public BaseStatus currentParamBonusMul;

      public Param(SkillData groupSkill, ConceptCardData currentCardData, int levelCap, int awakeCountCap, int prevLevel, int prevAwakeCount, bool includeMaxPowerUp)
      {
        this.skilName = groupSkill.Name;
        this.prevParam = new BaseStatus();
        this.prevParamMul = new BaseStatus();
        this.currentParam = new BaseStatus();
        this.currentParamMul = new BaseStatus();
        this.prevParamBonus = new BaseStatus();
        this.prevParamBonusMul = new BaseStatus();
        this.currentParamBonus = new BaseStatus();
        this.currentParamBonusMul = new BaseStatus();
        ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, (int) currentCardData.Lv, ref this.currentParam, ref this.currentParamMul);
        ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, prevLevel, ref this.prevParam, ref this.currentParamMul);
        SkillPowerUpResultContent.Param.GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, (int) currentCardData.Lv, (int) currentCardData.AwakeCount, ref this.currentParamBonus, ref this.currentParamBonusMul, includeMaxPowerUp);
        SkillPowerUpResultContent.Param.GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, prevLevel, prevAwakeCount, ref this.prevParamBonus, ref this.currentParamBonusMul, false);
        int length = Enum.GetValues(typeof (ParamTypes)).Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          if (index1 != 2)
          {
            ParamTypes index2 = (ParamTypes) index1;
            if ((int) this.prevParamBonus[index2] != (int) this.currentParamBonus[index2])
              this.typeList.Add(index2);
            if ((int) this.prevParamBonusMul[index2] != (int) this.currentParamBonusMul[index2])
              this.typeListMul.Add(index2);
          }
        }
      }

      public string skilName { get; private set; }

      public int GetBonusChangeTypeNum(bool scalingStatus)
      {
        if (scalingStatus)
          return this.typeListMul.Count;
        return this.typeList.Count;
      }

      public bool IsBonusParamChanged()
      {
        if (this.typeListMul.Count <= 0)
          return this.typeList.Count > 0;
        return true;
      }

      [DebuggerHidden]
      public IEnumerable<SkillPowerUpResultContent.DispType> GetAllBonusChangeType()
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SkillPowerUpResultContent.Param.\u003CGetAllBonusChangeType\u003Ec__Iterator13C typeCIterator13C = new SkillPowerUpResultContent.Param.\u003CGetAllBonusChangeType\u003Ec__Iterator13C()
        {
          \u003C\u003Ef__this = this
        };
        // ISSUE: reference to a compiler-generated field
        typeCIterator13C.\u0024PC = -2;
        return (IEnumerable<SkillPowerUpResultContent.DispType>) typeCIterator13C;
      }

      private static void GetBonusStatus(SkillData groupSkill, ConceptCardData currentCardData, int levelCap, int awakeCountCap, int level, int awakeCount, ref BaseStatus bonusAdd, ref BaseStatus bonusScale, bool includeMaxPowerUp)
      {
        if (currentCardData.EquipEffects == null)
          return;
        using (List<ConceptCardEquipEffect>.Enumerator enumerator = currentCardData.EquipEffects.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ConceptCardEquipEffect current = enumerator.Current;
            if (current.CardSkill != null && current.CardSkill.Name == groupSkill.Name)
            {
              current.GetAddCardSkillBuffStatusAwake(awakeCount, awakeCountCap, ref bonusAdd, ref bonusScale);
              if (!includeMaxPowerUp || level != levelCap || awakeCount != awakeCountCap)
                break;
              BaseStatus total_add = new BaseStatus();
              BaseStatus total_scale = new BaseStatus();
              current.GetAddCardSkillBuffStatusLvMax(level, levelCap, awakeCount, ref total_add, ref total_scale);
              bonusAdd.Add(total_add);
              bonusScale.Add(total_scale);
              break;
            }
          }
        }
      }
    }
  }
}
