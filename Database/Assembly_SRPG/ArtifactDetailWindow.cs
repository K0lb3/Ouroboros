namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "アビリティ詳細画面の表示", 1, 100)]
    public class ArtifactDetailWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private GameObject mAbilityContentTemplate;
        [SerializeField]
        private GameObject mJobContentTemplate;
        [SerializeField]
        private GameObject mAbilityEmptyTextObject;
        [SerializeField]
        private Toggle mHideDisplayAbilityToggle;
        [HeaderBar("▼セット効果確認用のボタン"), SerializeField]
        private Button mSetEffectsButton;
        private ArtifactParam mCurrentArtifactParam;
        private ArtifactData mCurrentArtifactData;
        private UnitData mCurrentUnit;
        private JobData mCurrentJob;
        private ArtifactTypes mCurrentEquipSlot;
        private Dictionary<string, ViewAbilityData> mViewAbilities;
        private List<ArtifactDetailAbilityContent> mAbilityContents;
        private static ArtifactParam s_ArtifactParam;

        public ArtifactDetailWindow()
        {
            this.mAbilityContents = new List<ArtifactDetailAbilityContent>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void ChangeDisplayBaseAbility(bool is_display)
        {
            ArtifactDetailAbilityItem[] itemArray;
            int num;
            int num2;
            itemArray = base.GetComponentsInChildren<ArtifactDetailAbilityItem>(1);
            num = 0;
            goto Label_0036;
        Label_000F:
            if (itemArray[num].IsEnable != null)
            {
                goto Label_0032;
            }
            if (itemArray[num].HasDeriveAbility == null)
            {
                goto Label_0032;
            }
            itemArray[num].SetActive(is_display);
        Label_0032:
            num += 1;
        Label_0036:
            if (num < ((int) itemArray.Length))
            {
                goto Label_000F;
            }
            num2 = 0;
            goto Label_0053;
        Label_0046:
            itemArray[num2].SetActiveLine(is_display);
            num2 += 1;
        Label_0053:
            if (num2 < ((int) itemArray.Length))
            {
                goto Label_0046;
            }
            return;
        }

        private unsafe void CreateInstance(Dictionary<string, ViewAbilityData> view_datas)
        {
            string str;
            Dictionary<string, ViewAbilityData>.KeyCollection.Enumerator enumerator;
            GameObject obj2;
            ArtifactDetailAbilityContent content;
            if ((this.mAbilityContentTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            enumerator = view_datas.Keys.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_0073;
            Label_0023:
                str = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.mAbilityContentTemplate);
                obj2.get_transform().SetParent(this.mAbilityContentTemplate.get_transform().get_parent(), 0);
                content = obj2.GetComponent<ArtifactDetailAbilityContent>();
                content.Init(view_datas[str]);
                this.mAbilityContents.Add(content);
            Label_0073:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_0090;
            }
            finally
            {
            Label_0084:
                ((Dictionary<string, ViewAbilityData>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0090:
            this.mAbilityContentTemplate.SetActive(0);
            if ((this.mJobContentTemplate != null) == null)
            {
                goto Label_00B9;
            }
            this.mJobContentTemplate.SetActive(0);
        Label_00B9:
            return;
        }

        private Dictionary<string, ViewAbilityData> CreateViewData(List<AbilityParam> artifact_all_abilities, List<AbilityData> weapon_abilities, List<AbilityDeriveParam> derive_params)
        {
            Dictionary<string, ViewAbilityData> dictionary;
            bool flag;
            bool flag2;
            int num;
            ViewAbilityData data;
            int num2;
            string str;
            bool flag3;
            AbilityParam param;
            <CreateViewData>c__AnonStorey2F3 storeyf;
            <CreateViewData>c__AnonStorey2F4 storeyf2;
            storeyf = new <CreateViewData>c__AnonStorey2F3();
            storeyf.artifact_all_abilities = artifact_all_abilities;
            dictionary = new Dictionary<string, ViewAbilityData>();
            if (storeyf.artifact_all_abilities == null)
            {
                goto Label_0138;
            }
            storeyf2 = new <CreateViewData>c__AnonStorey2F4();
            storeyf2.<>f__ref$755 = storeyf;
            storeyf2.i = 0;
            goto Label_0120;
        Label_003E:
            if (dictionary.ContainsKey(storeyf.artifact_all_abilities[storeyf2.i].iname) == null)
            {
                goto Label_0066;
            }
            goto Label_0110;
        Label_0066:
            flag = 1;
            if (this.IsNeedCheck == null)
            {
                goto Label_009D;
            }
            flag = storeyf.artifact_all_abilities[storeyf2.i].CheckEnableUseAbility(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
        Label_009D:
            flag2 = 0;
            if (this.IsNeedCheck == null)
            {
                goto Label_00CD;
            }
            if (weapon_abilities == null)
            {
                goto Label_00CD;
            }
            if (weapon_abilities.FindIndex(new Predicate<AbilityData>(storeyf2.<>m__27B)) > -1)
            {
                goto Label_00CD;
            }
            flag2 = 1;
        Label_00CD:
            data = new ViewAbilityData();
            data.AddAbility(storeyf.artifact_all_abilities[storeyf2.i], flag, flag2);
            dictionary.Add(storeyf.artifact_all_abilities[storeyf2.i].iname, data);
        Label_0110:
            storeyf2.i += 1;
        Label_0120:
            if (storeyf2.i < storeyf.artifact_all_abilities.Count)
            {
                goto Label_003E;
            }
        Label_0138:
            if (derive_params == null)
            {
                goto Label_01E6;
            }
            num2 = 0;
            goto Label_01D9;
        Label_0146:
            str = derive_params[num2].BaseAbilityIname;
            if (dictionary.ContainsKey(str) != null)
            {
                goto Label_0167;
            }
            goto Label_01D3;
        Label_0167:
            flag3 = 1;
            if (this.IsNeedCheck == null)
            {
                goto Label_01AD;
            }
            flag3 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(derive_params[num2].DeriveAbilityIname).CheckEnableUseAbility(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
        Label_01AD:
            dictionary[str].AddAbility(str, derive_params[num2], derive_params[num2].DeriveAbilityIname, flag3);
        Label_01D3:
            num2 += 1;
        Label_01D9:
            if (num2 < derive_params.Count)
            {
                goto Label_0146;
            }
        Label_01E6:
            return dictionary;
        }

        public void OnChangeDisplayBaseAbility()
        {
            this.ChangeDisplayBaseAbility(this.mHideDisplayAbilityToggle.get_isOn());
            return;
        }

        public void OnCheckJobButton()
        {
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            GlobalVars.ConditionJobs = this.mCurrentArtifactParam.condition_jobs;
            return;
        }

        public void OpenAbilityDetail()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void RefreshView()
        {
            List<AbilityParam> list;
            int num;
            AbilityParam param;
            List<AbilityData> list2;
            List<AbilityDeriveParam> list3;
            SkillAbilityDeriveData data;
            bool flag;
            ArtifactDetailAbilityItem[] itemArray;
            int num2;
            list = new List<AbilityParam>();
            if (this.mCurrentArtifactParam == null)
            {
                goto Label_0085;
            }
            if (this.mCurrentArtifactParam.abil_inames == null)
            {
                goto Label_0085;
            }
            num = 0;
            goto Label_0072;
        Label_0028:
            if (string.IsNullOrEmpty(this.mCurrentArtifactParam.abil_inames[num]) == null)
            {
                goto Label_0044;
            }
            goto Label_006E;
        Label_0044:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.mCurrentArtifactParam.abil_inames[num]);
            if (param == null)
            {
                goto Label_006E;
            }
            list.Add(param);
        Label_006E:
            num += 1;
        Label_0072:
            if (num < ((int) this.mCurrentArtifactParam.abil_inames.Length))
            {
                goto Label_0028;
            }
        Label_0085:
            list2 = null;
            if (this.IsNeedCheck == null)
            {
                goto Label_009E;
            }
            list2 = this.mCurrentArtifactData.LearningAbilities;
        Label_009E:
            list3 = null;
            if (this.IsNeedCheck == null)
            {
                goto Label_00DB;
            }
            data = this.mCurrentUnit.GetSkillAbilityDeriveData(this.mCurrentJob, this.mCurrentEquipSlot, this.mCurrentArtifactParam);
            if (data == null)
            {
                goto Label_00DB;
            }
            list3 = data.GetAvailableAbilityDeriveParams();
        Label_00DB:
            this.mViewAbilities = this.CreateViewData(list, list2, list3);
            this.mAbilityEmptyTextObject.SetActive((this.mViewAbilities.Count > 0) == 0);
            this.CreateInstance(this.mViewAbilities);
            flag = 0;
            itemArray = base.GetComponentsInChildren<ArtifactDetailAbilityItem>(1);
            num2 = 0;
            goto Label_0144;
        Label_0127:
            if (itemArray[num2].HasDeriveAbility == null)
            {
                goto Label_013E;
            }
            flag = 1;
            goto Label_014F;
        Label_013E:
            num2 += 1;
        Label_0144:
            if (num2 < ((int) itemArray.Length))
            {
                goto Label_0127;
            }
        Label_014F:
            this.mHideDisplayAbilityToggle.set_interactable(flag);
            if ((this.mSetEffectsButton != null) == null)
            {
                goto Label_019D;
            }
            if (this.mCurrentArtifactParam == null)
            {
                goto Label_019D;
            }
            this.mSetEffectsButton.set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifactParam.iname));
        Label_019D:
            this.Reposition();
            return;
        }

        private void Reposition()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0043;
        Label_0009:
            if (this.mAbilityContents[num2].IsExistEnableAbility != null)
            {
                goto Label_0024;
            }
            goto Label_003F;
        Label_0024:
            this.mAbilityContents[num2].get_transform().SetSiblingIndex(num);
            num += 1;
        Label_003F:
            num2 += 1;
        Label_0043:
            if (num2 < this.mAbilityContents.Count)
            {
                goto Label_0009;
            }
            return;
        }

        public static void SetArtifactParam(ArtifactParam artifactParam)
        {
            s_ArtifactParam = artifactParam;
            return;
        }

        private void Start()
        {
            this.mCurrentArtifactParam = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
            this.mCurrentArtifactData = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            this.mCurrentEquipSlot = DataSource.FindDataOfClass<ArtifactTypes>(base.get_gameObject(), 0);
            this.mCurrentJob = DataSource.FindDataOfClass<JobData>(base.get_gameObject(), null);
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_0092;
            }
            this.mCurrentArtifactParam = s_ArtifactParam;
            s_ArtifactParam = null;
            if (this.mCurrentArtifactParam == null)
            {
                goto Label_0092;
            }
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), this.mCurrentArtifactParam);
        Label_0092:
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_00B9;
            }
            if (this.mCurrentArtifactData == null)
            {
                goto Label_00B9;
            }
            this.mCurrentArtifactParam = this.mCurrentArtifactData.ArtifactParam;
        Label_00B9:
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_00C5;
            }
            return;
        Label_00C5:
            ArtifactSetList.SetSelectedArtifactParam(this.mCurrentArtifactParam);
            this.RefreshView();
            return;
        }

        private bool IsNeedCheck
        {
            get
            {
                if (this.mCurrentUnit == null)
                {
                    goto Label_0018;
                }
                if (this.mCurrentArtifactData == null)
                {
                    goto Label_0018;
                }
                return 1;
            Label_0018:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateViewData>c__AnonStorey2F3
        {
            internal List<AbilityParam> artifact_all_abilities;

            public <CreateViewData>c__AnonStorey2F3()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateViewData>c__AnonStorey2F4
        {
            internal int i;
            internal ArtifactDetailWindow.<CreateViewData>c__AnonStorey2F3 <>f__ref$755;

            public <CreateViewData>c__AnonStorey2F4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__27B(AbilityData abil_data)
            {
                return (abil_data.Param.iname == this.<>f__ref$755.artifact_all_abilities[this.i].iname);
            }
        }
    }
}

