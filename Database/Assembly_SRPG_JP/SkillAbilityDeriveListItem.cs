// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SkillAbilityDeriveListItem : MonoBehaviour
  {
    [HeaderBar("▼派生アビリティ関連")]
    [SerializeField]
    private RectTransform m_AbilityDeriveListRoot;
    [SerializeField]
    private AbilityDeriveList m_AbilityDeriveListTemplate;
    [HeaderBar("▼派生スキル関連")]
    [SerializeField]
    private RectTransform m_SkillDeriveListRoot;
    [SerializeField]
    private SkillDeriveList m_SkillDeriveListTemplate;
    private List<SkillAbilityDeriveListItem.ViewContentSkillParam> m_ViewContentSkillParams;
    private List<SkillAbilityDeriveListItem.ViewContentAbilityParam> m_ViewContentAbilityParams;

    public SkillAbilityDeriveListItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_SkillDeriveListTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_AbilityDeriveListTemplate, false);
    }

    public void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam)
    {
      this.m_ViewContentSkillParams = SkillAbilityDeriveListItem.CreateViewContentSkillParams(skillAbilityDeriveParam.SkillDeriveParams);
      this.m_ViewContentAbilityParams = SkillAbilityDeriveListItem.CreateViewContentAbilityParams(skillAbilityDeriveParam.AbilityDeriveParams);
      using (List<SkillAbilityDeriveListItem.ViewContentSkillParam>.Enumerator enumerator = this.m_ViewContentSkillParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.CreateListItem(enumerator.Current);
      }
      using (List<SkillAbilityDeriveListItem.ViewContentAbilityParam>.Enumerator enumerator = this.m_ViewContentAbilityParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.CreateListItem(enumerator.Current);
      }
      this.UpdateUIActive();
    }

    public void Setup(SkillAbilityDeriveData skillAbilityDeriveData)
    {
      List<SkillDeriveParam> skillDeriveParams = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
      List<AbilityDeriveParam> abilityDeriveParams = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
      this.m_ViewContentSkillParams = SkillAbilityDeriveListItem.CreateViewContentSkillParams(skillDeriveParams);
      this.m_ViewContentAbilityParams = SkillAbilityDeriveListItem.CreateViewContentAbilityParams(abilityDeriveParams);
      using (List<SkillAbilityDeriveListItem.ViewContentSkillParam>.Enumerator enumerator = this.m_ViewContentSkillParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.CreateListItem(enumerator.Current);
      }
      using (List<SkillAbilityDeriveListItem.ViewContentAbilityParam>.Enumerator enumerator = this.m_ViewContentAbilityParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.CreateListItem(enumerator.Current);
      }
      this.UpdateUIActive();
    }

    private static List<SkillAbilityDeriveListItem.ViewContentAbilityParam> CreateViewContentAbilityParams(List<AbilityDeriveParam> deriveAbilityParams)
    {
      List<SkillAbilityDeriveListItem.ViewContentAbilityParam> contentAbilityParamList = new List<SkillAbilityDeriveListItem.ViewContentAbilityParam>();
      using (List<AbilityDeriveParam>.Enumerator enumerator = deriveAbilityParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AbilityDeriveParam abilityDeriveParam = enumerator.Current;
          SkillAbilityDeriveListItem.ViewContentAbilityParam contentAbilityParam = contentAbilityParamList.Find((Predicate<SkillAbilityDeriveListItem.ViewContentAbilityParam>) (content => content.m_BaseAbilityParam == abilityDeriveParam.m_BaseParam));
          if (contentAbilityParam == null)
          {
            contentAbilityParam = new SkillAbilityDeriveListItem.ViewContentAbilityParam();
            contentAbilityParam.m_AbilityDeriveParam = new List<AbilityDeriveParam>();
            contentAbilityParamList.Add(contentAbilityParam);
          }
          contentAbilityParam.m_BaseAbilityParam = abilityDeriveParam.m_BaseParam;
          contentAbilityParam.m_AbilityDeriveParam.Add(abilityDeriveParam);
        }
      }
      return contentAbilityParamList;
    }

    private static List<SkillAbilityDeriveListItem.ViewContentSkillParam> CreateViewContentSkillParams(List<SkillDeriveParam> deriveSkillParams)
    {
      List<SkillAbilityDeriveListItem.ViewContentSkillParam> contentSkillParamList = new List<SkillAbilityDeriveListItem.ViewContentSkillParam>();
      using (List<SkillDeriveParam>.Enumerator enumerator = deriveSkillParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillDeriveParam skillDeriveParam = enumerator.Current;
          SkillAbilityDeriveListItem.ViewContentSkillParam contentSkillParam = contentSkillParamList.Find((Predicate<SkillAbilityDeriveListItem.ViewContentSkillParam>) (content => content.m_BaseSkillParam == skillDeriveParam.m_BaseParam));
          if (contentSkillParam == null)
          {
            contentSkillParam = new SkillAbilityDeriveListItem.ViewContentSkillParam();
            contentSkillParam.m_SkillDeriveParams = new List<SkillDeriveParam>();
            contentSkillParamList.Add(contentSkillParam);
          }
          contentSkillParam.m_BaseSkillParam = skillDeriveParam.m_BaseParam;
          contentSkillParam.m_SkillDeriveParams.Add(skillDeriveParam);
        }
      }
      return contentSkillParamList;
    }

    private void CreateListItem(SkillAbilityDeriveListItem.ViewContentSkillParam viewContentSkillParam)
    {
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.m_SkillDeriveListTemplate).get_gameObject());
      gameObject.get_transform().SetParent((Transform) this.m_SkillDeriveListRoot, false);
      gameObject.SetActive(true);
      ((SkillDeriveList) gameObject.GetComponent<SkillDeriveList>()).Setup(viewContentSkillParam.m_BaseSkillParam, viewContentSkillParam.m_SkillDeriveParams);
    }

    private void CreateListItem(SkillAbilityDeriveListItem.ViewContentAbilityParam viewContentAbilityParam)
    {
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.m_AbilityDeriveListTemplate).get_gameObject());
      gameObject.get_transform().SetParent((Transform) this.m_AbilityDeriveListRoot, false);
      gameObject.SetActive(true);
      ((AbilityDeriveList) gameObject.GetComponent<AbilityDeriveList>()).SetupWithAbilityParam(viewContentAbilityParam.m_BaseAbilityParam, viewContentAbilityParam.m_AbilityDeriveParam);
    }

    private void UpdateUIActive()
    {
      if (this.m_ViewContentAbilityParams.Count < 1)
        GameUtility.SetGameObjectActive((Component) this.m_AbilityDeriveListRoot, false);
      if (this.m_ViewContentSkillParams.Count >= 1)
        return;
      GameUtility.SetGameObjectActive((Component) this.m_SkillDeriveListRoot, false);
    }

    private class ViewContentAbilityParam
    {
      public AbilityParam m_BaseAbilityParam;
      public List<AbilityDeriveParam> m_AbilityDeriveParam;
    }

    private class ViewContentSkillParam
    {
      public SkillParam m_BaseSkillParam;
      public List<SkillDeriveParam> m_SkillDeriveParams;
    }
  }
}
