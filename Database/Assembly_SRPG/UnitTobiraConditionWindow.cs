namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitTobiraConditionWindow : MonoBehaviour
    {
        private static readonly string HAS_UNIT_OBJECT_NAME;
        private static readonly string NOT_HAS_UNIT_OBJECT_NAME;
        private static readonly string NOT_OPEN_JOB_OBJECT_NAME;
        private static readonly string NOT_OPEN_TOBIRA_OBJECT_NAME;
        private static readonly string VALUE_TEXT_NAME;
        private static readonly string VALUE_MAX_TEXT_NAME;
        private static readonly string JOB_LEVEL_TEXT_PAREANT_NAME;
        private static readonly string JOB_LEVEL_TEXT_NAME;
        private static readonly string JOB_LEVEL_MAX_TEXT_NAME;
        private static readonly string STRING_FORMAT_CONDS_CLEAR;
        private static readonly string STRING_FORMAT_CONDS_NOT_CLEAR;
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
        [SerializeField, HeaderBar("▼条件表示用のテンプレート")]
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
        private ViewParam mViewParam;
        private GameObject[] mLayoutObjects;

        static UnitTobiraConditionWindow()
        {
            HAS_UNIT_OBJECT_NAME = "enable";
            NOT_HAS_UNIT_OBJECT_NAME = "none_unit";
            NOT_OPEN_JOB_OBJECT_NAME = "none_job";
            NOT_OPEN_TOBIRA_OBJECT_NAME = "none_tobira";
            VALUE_TEXT_NAME = "txt_current";
            VALUE_MAX_TEXT_NAME = "txt_max";
            JOB_LEVEL_TEXT_PAREANT_NAME = "level";
            JOB_LEVEL_TEXT_NAME = "txt_current";
            JOB_LEVEL_MAX_TEXT_NAME = "txt_max";
            STRING_FORMAT_CONDS_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_CLEAR";
            STRING_FORMAT_CONDS_NOT_CLEAR = "sys.TOBIRA_CONDITIONS_TEXT_COLOR_NOT_CLEAR";
            return;
        }

        public UnitTobiraConditionWindow()
        {
            base..ctor();
            return;
        }

        private unsafe void CreateLayout(ConditionsResult conds)
        {
            ConditionsResult_UnitLv lv;
            ConditionsResult_AwakeLv lv2;
            ConditionsResult_JobLv lv3;
            ConditionsResult_TobiraLv lv4;
            ConditionsResult_TobiraNoConditions conditions;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            if (conds.isConditionsQuestClear == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (conds.isConditionsUnitLv == null)
            {
                goto Label_00B8;
            }
            lv = (ConditionsResult_UnitLv) conds;
            this.mViewParam = new ViewParam();
            this.mViewParam.type = 1;
            this.mViewParam.title = conds.text;
            this.mViewParam.value_max = &lv.targetValue.ToString();
            this.mViewParam.is_clear = lv.isClear;
            this.mViewParam.has_unit = lv.hasUnitData;
            this.mViewParam.unit_data = lv.unitData;
            if (lv.hasUnitData == null)
            {
                goto Label_00B8;
            }
            this.mViewParam.value = &lv.currentValue.ToString();
        Label_00B8:
            if (conds.isConditionsAwake == null)
            {
                goto Label_0164;
            }
            lv2 = (ConditionsResult_AwakeLv) conds;
            this.mViewParam = new ViewParam();
            this.mViewParam.type = 2;
            this.mViewParam.title = lv2.text;
            this.mViewParam.value_max = &lv2.targetValue.ToString();
            this.mViewParam.is_clear = lv2.isClear;
            this.mViewParam.has_unit = lv2.hasUnitData;
            this.mViewParam.unit_data = lv2.unitData;
            if (lv2.hasUnitData == null)
            {
                goto Label_0164;
            }
            this.mViewParam.value = &lv2.currentValue.ToString();
        Label_0164:
            if (conds.isConditionsJobLv == null)
            {
                goto Label_0205;
            }
            lv3 = (ConditionsResult_JobLv) conds;
            this.mViewParam = new ViewParam();
            this.mViewParam.type = 3;
            this.mViewParam.title = lv3.text;
            this.mViewParam.value_max = &lv3.targetValue.ToString();
            this.mViewParam.is_clear = lv3.isClear;
            this.mViewParam.has_unit = lv3.hasUnitData;
            this.mViewParam.job_param = lv3.mJobParam;
            this.mViewParam.value = &lv3.currentValue.ToString();
        Label_0205:
            if (conds.isConditionsTobiraLv == null)
            {
                goto Label_02B0;
            }
            lv4 = (ConditionsResult_TobiraLv) conds;
            this.mViewParam = new ViewParam();
            this.mViewParam.type = 4;
            this.mViewParam.title = lv4.text;
            num7 = lv4.targetValue - 1;
            this.mViewParam.value_max = &num7.ToString();
            this.mViewParam.is_clear = lv4.isClear;
            this.mViewParam.has_unit = lv4.hasUnitData;
            this.mViewParam.tobira_data = lv4.mTobiraData;
            this.mViewParam.value = &Mathf.Max(0, lv4.currentValue - 1).ToString();
        Label_02B0:
            if (conds.isConditionsTobiraNoConditions == null)
            {
                goto Label_02FE;
            }
            conditions = (ConditionsResult_TobiraNoConditions) conds;
            this.mViewParam = new ViewParam();
            this.mViewParam.type = 0;
            this.mViewParam.title = conditions.text;
            this.mViewParam.is_clear = conditions.isClear;
        Label_02FE:
            this.CreateLayoutObject(this.mViewParam);
            return;
        }

        private void CreateLayoutObject(ViewParam view_param)
        {
            GameObject obj2;
            Transform transform;
            Transform transform2;
            Transform transform3;
            IEnumerator enumerator;
            string str;
            string str2;
            Transform transform4;
            IEnumerator enumerator2;
            bool flag;
            Transform transform5;
            IEnumerator enumerator3;
            Transform transform6;
            IEnumerator enumerator4;
            Transform transform7;
            IEnumerator enumerator5;
            Transform transform8;
            IEnumerator enumerator6;
            Text text;
            int num;
            ImageArray array;
            IDisposable disposable;
            IDisposable disposable2;
            IDisposable disposable3;
            IDisposable disposable4;
            IDisposable disposable5;
            IDisposable disposable6;
            if (view_param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((this.mConditionObjectTemplate == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if ((this.mConditionObjectParent == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            if ((this.mConditionLayoutParent == null) == null)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            obj2 = Object.Instantiate<GameObject>(this.mConditionObjectTemplate);
            obj2.get_transform().SetParent(this.mConditionObjectParent, 0);
            obj2.SetActive(1);
            transform = null;
            transform2 = null;
            enumerator = obj2.get_transform().GetEnumerator();
        Label_0073:
            try
            {
                goto Label_00BF;
            Label_0078:
                transform3 = (Transform) enumerator.Current;
                if ((transform3.get_name() == this.mConditionLayoutParent.get_name()) == null)
                {
                    goto Label_00A2;
                }
                transform2 = transform3;
            Label_00A2:
                if ((transform3.get_name() == this.mTitleTextObjectParent.get_name()) == null)
                {
                    goto Label_00BF;
                }
                transform = transform3;
            Label_00BF:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0078;
                }
                goto Label_00E6;
            }
            finally
            {
            Label_00D0:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_00DE;
                }
            Label_00DE:
                disposable.Dispose();
            }
        Label_00E6:
            if ((transform2 == null) == null)
            {
                goto Label_00F3;
            }
            return;
        Label_00F3:
            str = (view_param.is_clear == null) ? STRING_FORMAT_CONDS_NOT_CLEAR : STRING_FORMAT_CONDS_CLEAR;
            str2 = LocalizedText.Get(str);
            enumerator2 = transform2.GetEnumerator();
        Label_0120:
            try
            {
                goto Label_047A;
            Label_0125:
                transform4 = (Transform) enumerator2.Current;
                flag = transform4.get_name() == this.mLayoutObjects[view_param.type].get_name();
                transform4.get_gameObject().SetActive(flag);
                if (flag == null)
                {
                    goto Label_047A;
                }
                enumerator3 = transform4.GetEnumerator();
            Label_0171:
                try
                {
                    goto Label_03F0;
                Label_0176:
                    transform5 = (Transform) enumerator3.Current;
                    transform5.get_gameObject().SetActive(0);
                    if ((transform5.get_name() == HAS_UNIT_OBJECT_NAME) == null)
                    {
                        goto Label_0351;
                    }
                    transform5.get_gameObject().SetActive(view_param.has_unit);
                    enumerator4 = transform5.GetEnumerator();
                Label_01C2:
                    try
                    {
                        goto Label_02DC;
                    Label_01C7:
                        transform6 = (Transform) enumerator4.Current;
                        if ((transform6.get_name() == VALUE_TEXT_NAME) == null)
                        {
                            goto Label_0204;
                        }
                        transform6.GetComponent<Text>().set_text(string.Format(str2, view_param.value));
                    Label_0204:
                        if ((transform6.get_name() == VALUE_MAX_TEXT_NAME) == null)
                        {
                            goto Label_022C;
                        }
                        transform6.GetComponent<Text>().set_text(view_param.value_max);
                    Label_022C:
                        if ((transform6.get_name() == JOB_LEVEL_TEXT_PAREANT_NAME) == null)
                        {
                            goto Label_02DC;
                        }
                        enumerator5 = transform6.GetEnumerator();
                    Label_024B:
                        try
                        {
                            goto Label_02B5;
                        Label_0250:
                            transform7 = (Transform) enumerator5.Current;
                            if ((transform7.get_name() == JOB_LEVEL_TEXT_NAME) == null)
                            {
                                goto Label_028D;
                            }
                            transform7.GetComponent<Text>().set_text(string.Format(str2, view_param.value));
                        Label_028D:
                            if ((transform7.get_name() == JOB_LEVEL_MAX_TEXT_NAME) == null)
                            {
                                goto Label_02B5;
                            }
                            transform7.GetComponent<Text>().set_text(view_param.value_max);
                        Label_02B5:
                            if (enumerator5.MoveNext() != null)
                            {
                                goto Label_0250;
                            }
                            goto Label_02DC;
                        }
                        finally
                        {
                        Label_02C6:
                            disposable2 = enumerator5 as IDisposable;
                            if (disposable2 != null)
                            {
                                goto Label_02D4;
                            }
                        Label_02D4:
                            disposable2.Dispose();
                        }
                    Label_02DC:
                        if (enumerator4.MoveNext() != null)
                        {
                            goto Label_01C7;
                        }
                        goto Label_0303;
                    }
                    finally
                    {
                    Label_02ED:
                        disposable3 = enumerator4 as IDisposable;
                        if (disposable3 != null)
                        {
                            goto Label_02FB;
                        }
                    Label_02FB:
                        disposable3.Dispose();
                    }
                Label_0303:
                    if (view_param.type != 3)
                    {
                        goto Label_032D;
                    }
                    if (int.Parse(view_param.value) > 0)
                    {
                        goto Label_032D;
                    }
                    transform5.get_gameObject().SetActive(0);
                Label_032D:
                    if (view_param.type != 4)
                    {
                        goto Label_0351;
                    }
                    if (view_param.tobira_data != null)
                    {
                        goto Label_0351;
                    }
                    transform5.get_gameObject().SetActive(0);
                Label_0351:
                    if ((transform5.get_name() == NOT_HAS_UNIT_OBJECT_NAME) == null)
                    {
                        goto Label_037C;
                    }
                    transform5.get_gameObject().SetActive(view_param.has_unit == 0);
                Label_037C:
                    if ((transform5.get_name() == NOT_OPEN_JOB_OBJECT_NAME) == null)
                    {
                        goto Label_03BA;
                    }
                    if (view_param.has_unit == null)
                    {
                        goto Label_03BA;
                    }
                    transform5.get_gameObject().SetActive((int.Parse(view_param.value) > 0) == 0);
                Label_03BA:
                    if ((transform5.get_name() == NOT_OPEN_TOBIRA_OBJECT_NAME) == null)
                    {
                        goto Label_03F0;
                    }
                    if (view_param.has_unit == null)
                    {
                        goto Label_03F0;
                    }
                    transform5.get_gameObject().SetActive(view_param.tobira_data == null);
                Label_03F0:
                    if (enumerator3.MoveNext() != null)
                    {
                        goto Label_0176;
                    }
                    goto Label_0417;
                }
                finally
                {
                Label_0401:
                    disposable4 = enumerator3 as IDisposable;
                    if (disposable4 != null)
                    {
                        goto Label_040F;
                    }
                Label_040F:
                    disposable4.Dispose();
                }
            Label_0417:
                if (view_param.unit_data == null)
                {
                    goto Label_0434;
                }
                DataSource.Bind<UnitData>(transform4.get_gameObject(), view_param.unit_data);
            Label_0434:
                if (view_param.job_param == null)
                {
                    goto Label_0451;
                }
                DataSource.Bind<JobParam>(transform4.get_gameObject(), view_param.job_param);
            Label_0451:
                if (view_param.tobira_data == null)
                {
                    goto Label_046E;
                }
                DataSource.Bind<TobiraData>(transform4.get_gameObject(), view_param.tobira_data);
            Label_046E:
                GameParameter.UpdateAll(transform4.get_gameObject());
            Label_047A:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_0125;
                }
                goto Label_04A1;
            }
            finally
            {
            Label_048B:
                disposable5 = enumerator2 as IDisposable;
                if (disposable5 != null)
                {
                    goto Label_0499;
                }
            Label_0499:
                disposable5.Dispose();
            }
        Label_04A1:
            enumerator6 = transform.GetEnumerator();
        Label_04A9:
            try
            {
                goto Label_054A;
            Label_04AE:
                transform8 = (Transform) enumerator6.Current;
                if ((transform8.get_name() == this.mTitleTextObject.get_name()) == null)
                {
                    goto Label_04FB;
                }
                text = transform8.GetComponent<Text>();
                if ((text != null) == null)
                {
                    goto Label_04FB;
                }
                text.set_text(view_param.title);
            Label_04FB:
                if ((transform8.get_name() == this.mIconImageArray.get_name()) == null)
                {
                    goto Label_054A;
                }
                num = (view_param.is_clear == null) ? 0 : 1;
                array = transform8.GetComponent<ImageArray>();
                if ((array != null) == null)
                {
                    goto Label_054A;
                }
                array.ImageIndex = num;
            Label_054A:
                if (enumerator6.MoveNext() != null)
                {
                    goto Label_04AE;
                }
                goto Label_0571;
            }
            finally
            {
            Label_055B:
                disposable6 = enumerator6 as IDisposable;
                if (disposable6 != null)
                {
                    goto Label_0569;
                }
            Label_0569:
                disposable6.Dispose();
            }
        Label_0571:
            return;
        }

        private void Start()
        {
            UnitData data;
            TobiraParam.Category category;
            TobiraConditionParam[] paramArray;
            List<ConditionsResult> list;
            int num;
            if ((this.mConditionObjectTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mConditionObjectTemplate.SetActive(0);
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            category = GlobalVars.PreBattleUnitTobiraCategory;
            if (data != null)
            {
                goto Label_004A;
            }
            return;
        Label_004A:
            if (category > 0)
            {
                goto Label_0052;
            }
            return;
        Label_0052:
            if (category < 8)
            {
                goto Label_005A;
            }
            return;
        Label_005A:
            this.mLayoutObjects = new GameObject[6];
            this.mLayoutObjects[1] = this.mLayout_UnitLevel;
            this.mLayoutObjects[2] = this.mLayout_UnitAwake;
            this.mLayoutObjects[3] = this.mLayout_JobLevel;
            this.mLayoutObjects[4] = this.mLayout_TobiraLevel;
            this.mLayoutObjects[5] = this.mLayout_TobiraOpen;
            this.mLayoutObjects[0] = this.mLayout_None;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(data.UnitID, category);
            list = TobiraUtility.TobiraConditionsCheck(data, paramArray);
            num = 0;
            goto Label_00F5;
        Label_00E1:
            this.CreateLayout(list[num]);
            num += 1;
        Label_00F5:
            if (num < list.Count)
            {
                goto Label_00E1;
            }
            return;
        }

        private enum eLayoutType
        {
            None,
            UnitLevel,
            UnitAwake,
            JobLevel,
            TobiraLevel,
            TobiraOpen,
            MAX
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

            public ViewParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

