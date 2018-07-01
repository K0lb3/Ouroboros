// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraConditionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitTobiraConditionWindow : MonoBehaviour
  {
    private static readonly string HAS_UNIT_OBJECT_NAME = "enable";
    private static readonly string NOT_HAS_UNIT_OBJECT_NAME = "none_unit";
    private static readonly string NOT_OPEN_JOB_OBJECT_NAME = "none_job";
    private static readonly string NOT_OPEN_TOBIRA_OBJECT_NAME = "none_tobira";
    private static readonly string VALUE_TEXT_NAME = "txt_current";
    private static readonly string VALUE_MAX_TEXT_NAME = "txt_max";
    private static readonly string JOB_LEVEL_TEXT_PAREANT_NAME = "level";
    private static readonly string JOB_LEVEL_TEXT_NAME = "txt_current";
    private static readonly string JOB_LEVEL_MAX_TEXT_NAME = "txt_max";
    private static readonly string STRING_FORMAT_CONDS_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_CLEAR";
    private static readonly string STRING_FORMAT_CONDS_NOT_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_NOT_CLEAR";
    [SerializeField]
    private Transform mConditionObjectParent;
    [SerializeField]
    private GameObject mConditionObjectTemplate;
    [SerializeField]
    private GameObject mConditionLayoutParent;
    [SerializeField]
    private GameObject mTitleTextObject;
    [SerializeField]
    private Transform mTitleTextObjectParent;
    [SerializeField]
    private ImageArray mIconImageArray;
    [SerializeField]
    [HeaderBar("▼条件表示用のテンプレート")]
    private GameObject mLayout_UnitLevel;
    [SerializeField]
    private GameObject mLayout_UnitAwake;
    [SerializeField]
    private GameObject mLayout_JobLevel;
    [SerializeField]
    private GameObject mLayout_TobiraLevel;
    [SerializeField]
    private GameObject mLayout_TobiraOpen;
    [SerializeField]
    private GameObject mLayout_None;
    private UnitTobiraConditionWindow.ViewParam mViewParam;
    private GameObject[] mLayoutObjects;

    public UnitTobiraConditionWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionObjectTemplate, (UnityEngine.Object) null))
        return;
      this.mConditionObjectTemplate.SetActive(false);
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      TobiraParam.Category unitTobiraCategory = (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory;
      if (unitDataByUniqueId == null || unitTobiraCategory <= TobiraParam.Category.START || unitTobiraCategory >= TobiraParam.Category.MAX)
        return;
      this.mLayoutObjects = new GameObject[6];
      this.mLayoutObjects[1] = this.mLayout_UnitLevel;
      this.mLayoutObjects[2] = this.mLayout_UnitAwake;
      this.mLayoutObjects[3] = this.mLayout_JobLevel;
      this.mLayoutObjects[4] = this.mLayout_TobiraLevel;
      this.mLayoutObjects[5] = this.mLayout_TobiraOpen;
      this.mLayoutObjects[0] = this.mLayout_None;
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unitDataByUniqueId.UnitID, unitTobiraCategory);
      List<ConditionsResult> conditionsResultList = TobiraUtility.TobiraConditionsCheck(unitDataByUniqueId, conditionsForUnit);
      for (int index = 0; index < conditionsResultList.Count; ++index)
        this.CreateLayout(conditionsResultList[index]);
    }

    private void CreateLayout(ConditionsResult conds)
    {
      if (conds.isConditionsQuestClear)
        return;
      if (conds.isConditionsUnitLv)
      {
        ConditionsResult_UnitLv conditionsResultUnitLv = (ConditionsResult_UnitLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.UnitLevel;
        this.mViewParam.title = conds.text;
        this.mViewParam.value_max = conditionsResultUnitLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultUnitLv.isClear;
        this.mViewParam.has_unit = conditionsResultUnitLv.hasUnitData;
        this.mViewParam.unit_data = conditionsResultUnitLv.unitData;
        if (conditionsResultUnitLv.hasUnitData)
          this.mViewParam.value = conditionsResultUnitLv.currentValue.ToString();
      }
      if (conds.isConditionsAwake)
      {
        ConditionsResult_AwakeLv conditionsResultAwakeLv = (ConditionsResult_AwakeLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.UnitAwake;
        this.mViewParam.title = conditionsResultAwakeLv.text;
        this.mViewParam.value_max = conditionsResultAwakeLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultAwakeLv.isClear;
        this.mViewParam.has_unit = conditionsResultAwakeLv.hasUnitData;
        this.mViewParam.unit_data = conditionsResultAwakeLv.unitData;
        if (conditionsResultAwakeLv.hasUnitData)
          this.mViewParam.value = conditionsResultAwakeLv.currentValue.ToString();
      }
      if (conds.isConditionsJobLv)
      {
        ConditionsResult_JobLv conditionsResultJobLv = (ConditionsResult_JobLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.JobLevel;
        this.mViewParam.title = conditionsResultJobLv.text;
        this.mViewParam.value_max = conditionsResultJobLv.targetValue.ToString();
        this.mViewParam.is_clear = conditionsResultJobLv.isClear;
        this.mViewParam.has_unit = conditionsResultJobLv.hasUnitData;
        this.mViewParam.job_param = conditionsResultJobLv.mJobParam;
        this.mViewParam.value = conditionsResultJobLv.currentValue.ToString();
      }
      if (conds.isConditionsTobiraLv)
      {
        ConditionsResult_TobiraLv conditionsResultTobiraLv = (ConditionsResult_TobiraLv) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.TobiraLevel;
        this.mViewParam.title = conditionsResultTobiraLv.text;
        this.mViewParam.value_max = (conditionsResultTobiraLv.targetValue - 1).ToString();
        this.mViewParam.is_clear = conditionsResultTobiraLv.isClear;
        this.mViewParam.has_unit = conditionsResultTobiraLv.hasUnitData;
        this.mViewParam.tobira_data = conditionsResultTobiraLv.mTobiraData;
        this.mViewParam.value = Mathf.Max(0, conditionsResultTobiraLv.currentValue - 1).ToString();
      }
      if (conds.isConditionsTobiraNoConditions)
      {
        ConditionsResult_TobiraNoConditions tobiraNoConditions = (ConditionsResult_TobiraNoConditions) conds;
        this.mViewParam = new UnitTobiraConditionWindow.ViewParam();
        this.mViewParam.type = UnitTobiraConditionWindow.eLayoutType.None;
        this.mViewParam.title = tobiraNoConditions.text;
        this.mViewParam.is_clear = tobiraNoConditions.isClear;
      }
      this.CreateLayoutObject(this.mViewParam);
    }

    private void CreateLayoutObject(UnitTobiraConditionWindow.ViewParam view_param)
    {
      if (view_param == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionObjectTemplate, (UnityEngine.Object) null) || (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionObjectParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionLayoutParent, (UnityEngine.Object) null)))
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mConditionObjectTemplate);
      gameObject.get_transform().SetParent(this.mConditionObjectParent, false);
      gameObject.SetActive(true);
      Transform transform1 = (Transform) null;
      Transform transform2 = (Transform) null;
      IEnumerator enumerator1 = gameObject.get_transform().GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          Transform current = (Transform) enumerator1.Current;
          if (((UnityEngine.Object) current).get_name() == ((UnityEngine.Object) this.mConditionLayoutParent).get_name())
            transform2 = current;
          if (((UnityEngine.Object) current).get_name() == ((UnityEngine.Object) this.mTitleTextObjectParent).get_name())
            transform1 = current;
        }
      }
      finally
      {
        IDisposable disposable = enumerator1 as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
        return;
      string format = LocalizedText.Get(!view_param.is_clear ? UnitTobiraConditionWindow.STRING_FORMAT_CONDS_NOT_CLEAR : UnitTobiraConditionWindow.STRING_FORMAT_CONDS_CLEAR);
      IEnumerator enumerator2 = transform2.GetEnumerator();
      try
      {
        while (enumerator2.MoveNext())
        {
          Transform current1 = (Transform) enumerator2.Current;
          bool flag = ((UnityEngine.Object) current1).get_name() == ((UnityEngine.Object) this.mLayoutObjects[(int) view_param.type]).get_name();
          ((Component) current1).get_gameObject().SetActive(flag);
          if (flag)
          {
            IEnumerator enumerator3 = current1.GetEnumerator();
            try
            {
              while (enumerator3.MoveNext())
              {
                Transform current2 = (Transform) enumerator3.Current;
                ((Component) current2).get_gameObject().SetActive(false);
                if (((UnityEngine.Object) current2).get_name() == UnitTobiraConditionWindow.HAS_UNIT_OBJECT_NAME)
                {
                  ((Component) current2).get_gameObject().SetActive(view_param.has_unit);
                  IEnumerator enumerator4 = current2.GetEnumerator();
                  try
                  {
                    while (enumerator4.MoveNext())
                    {
                      Transform current3 = (Transform) enumerator4.Current;
                      if (((UnityEngine.Object) current3).get_name() == UnitTobiraConditionWindow.VALUE_TEXT_NAME)
                        ((Text) ((Component) current3).GetComponent<Text>()).set_text(string.Format(format, (object) view_param.value));
                      if (((UnityEngine.Object) current3).get_name() == UnitTobiraConditionWindow.VALUE_MAX_TEXT_NAME)
                        ((Text) ((Component) current3).GetComponent<Text>()).set_text(view_param.value_max);
                      if (((UnityEngine.Object) current3).get_name() == UnitTobiraConditionWindow.JOB_LEVEL_TEXT_PAREANT_NAME)
                      {
                        IEnumerator enumerator5 = current3.GetEnumerator();
                        try
                        {
                          while (enumerator5.MoveNext())
                          {
                            Transform current4 = (Transform) enumerator5.Current;
                            if (((UnityEngine.Object) current4).get_name() == UnitTobiraConditionWindow.JOB_LEVEL_TEXT_NAME)
                              ((Text) ((Component) current4).GetComponent<Text>()).set_text(string.Format(format, (object) view_param.value));
                            if (((UnityEngine.Object) current4).get_name() == UnitTobiraConditionWindow.JOB_LEVEL_MAX_TEXT_NAME)
                              ((Text) ((Component) current4).GetComponent<Text>()).set_text(view_param.value_max);
                          }
                        }
                        finally
                        {
                          IDisposable disposable = enumerator5 as IDisposable;
                          if (disposable != null)
                            disposable.Dispose();
                        }
                      }
                    }
                  }
                  finally
                  {
                    IDisposable disposable = enumerator4 as IDisposable;
                    if (disposable != null)
                      disposable.Dispose();
                  }
                  if (view_param.type == UnitTobiraConditionWindow.eLayoutType.JobLevel && int.Parse(view_param.value) <= 0)
                    ((Component) current2).get_gameObject().SetActive(false);
                  if (view_param.type == UnitTobiraConditionWindow.eLayoutType.TobiraLevel && view_param.tobira_data == null)
                    ((Component) current2).get_gameObject().SetActive(false);
                }
                if (((UnityEngine.Object) current2).get_name() == UnitTobiraConditionWindow.NOT_HAS_UNIT_OBJECT_NAME)
                  ((Component) current2).get_gameObject().SetActive(!view_param.has_unit);
                if (((UnityEngine.Object) current2).get_name() == UnitTobiraConditionWindow.NOT_OPEN_JOB_OBJECT_NAME && view_param.has_unit)
                  ((Component) current2).get_gameObject().SetActive(int.Parse(view_param.value) <= 0);
                if (((UnityEngine.Object) current2).get_name() == UnitTobiraConditionWindow.NOT_OPEN_TOBIRA_OBJECT_NAME && view_param.has_unit)
                  ((Component) current2).get_gameObject().SetActive(view_param.tobira_data == null);
              }
            }
            finally
            {
              IDisposable disposable = enumerator3 as IDisposable;
              if (disposable != null)
                disposable.Dispose();
            }
            if (view_param.unit_data != null)
              DataSource.Bind<UnitData>(((Component) current1).get_gameObject(), view_param.unit_data);
            if (view_param.job_param != null)
              DataSource.Bind<JobParam>(((Component) current1).get_gameObject(), view_param.job_param);
            if (view_param.tobira_data != null)
              DataSource.Bind<TobiraData>(((Component) current1).get_gameObject(), view_param.tobira_data);
            GameParameter.UpdateAll(((Component) current1).get_gameObject());
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator2 as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      IEnumerator enumerator6 = transform1.GetEnumerator();
      try
      {
        while (enumerator6.MoveNext())
        {
          Transform current = (Transform) enumerator6.Current;
          if (((UnityEngine.Object) current).get_name() == ((UnityEngine.Object) this.mTitleTextObject).get_name())
          {
            Text component = (Text) ((Component) current).GetComponent<Text>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.set_text(view_param.title);
          }
          if (((UnityEngine.Object) current).get_name() == ((UnityEngine.Object) this.mIconImageArray).get_name())
          {
            int num = !view_param.is_clear ? 0 : 1;
            ImageArray component = (ImageArray) ((Component) current).GetComponent<ImageArray>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.ImageIndex = num;
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator6 as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }

    private enum eLayoutType
    {
      None,
      UnitLevel,
      UnitAwake,
      JobLevel,
      TobiraLevel,
      TobiraOpen,
      MAX,
    }

    private class ViewParam
    {
      public UnitTobiraConditionWindow.eLayoutType type;
      public string title;
      public string value;
      public string value_max;
      public bool is_clear;
      public bool has_unit;
      public UnitData unit_data;
      public JobParam job_param;
      public TobiraData tobira_data;
    }
  }
}
