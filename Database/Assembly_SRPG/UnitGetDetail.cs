namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Refresh", 0, 0)]
    public class UnitGetDetail : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Text mRenkeiText;
        [SerializeField]
        private Text mMoveText;
        [SerializeField]
        private Text mJumpText;
        [SerializeField]
        private Text mLeaderSkillText;
        [SerializeField]
        private Button mLeaderSkillDetailButton;
        [SerializeField]
        private GameObject Prefab_LeaderSkillDetail;
        private GameObject mLeaderSkillDetail;
        [SerializeField]
        internal string UnitName;
        [SerializeField]
        private GameObject[] mJobRoot;
        [SerializeField]
        private GameObject mAbilityTemplate;
        private Transform mPreviewParent;
        private RawImage mBGUnitImage;
        [SerializeField]
        private string mPreviewParentID;
        [SerializeField]
        private string mPreviewBaseID;
        [SerializeField]
        private string mBGUnitImageID;
        private float mBGUnitImgAlphaStart;
        private float mBGUnitImgAlphaEnd;
        private float mBGUnitImgFadeTime;
        private float mBGUnitImgFadeTimeMax;
        private UnitPreview mCurrentPreview;
        private List<UnitPreview> mPreviewControllers;
        private List<GameObject> mAbilits;

        public UnitGetDetail()
        {
            this.UnitName = "UN_V2_LOGI";
            this.mPreviewControllers = new List<UnitPreview>();
            this.mAbilits = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void Awake()
        {
            if ((this.mLeaderSkillDetailButton != null) == null)
            {
                goto Label_002D;
            }
            this.mLeaderSkillDetailButton.get_onClick().AddListener(new UnityAction(this, this.OpenLeaderSkillDetail));
        Label_002D:
            this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.mPreviewParentID);
            this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.mBGUnitImageID);
            return;
        }

        private UnitData CreateUnitData(UnitParam uparam)
        {
            UnitData data;
            Json_Unit unit;
            List<Json_Job> list;
            int num;
            int num2;
            JobSetParam param;
            Json_Job job;
            data = new UnitData();
            unit = new Json_Unit();
            unit.iid = 1L;
            unit.iname = uparam.iname;
            unit.exp = 0;
            unit.lv = 1;
            unit.plus = 0;
            unit.rare = 0;
            unit.select = new Json_UnitSelectable();
            unit.select.job = 0L;
            unit.jobs = null;
            unit.abil = null;
            unit.abil = null;
            if (uparam.jobsets == null)
            {
                goto Label_011E;
            }
            if (((int) uparam.jobsets.Length) <= 0)
            {
                goto Label_011E;
            }
            list = new List<Json_Job>((int) uparam.jobsets.Length);
            num = 1;
            num2 = 0;
            goto Label_0103;
        Label_009A:
            param = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[num2]);
            if (param != null)
            {
                goto Label_00BB;
            }
            goto Label_00FD;
        Label_00BB:
            job = new Json_Job();
            job.iid = (long) num++;
            job.iname = param.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            list.Add(job);
        Label_00FD:
            num2 += 1;
        Label_0103:
            if (num2 < ((int) uparam.jobsets.Length))
            {
                goto Label_009A;
            }
            unit.jobs = list.ToArray();
        Label_011E:
            data.Deserialize(unit);
            data.SetUniqueID(1L);
            data.JobRankUp(0);
            return data;
        }

        internal void DummyBind()
        {
            UnitParam param;
            UnitData data;
            long num;
            int num2;
            JobData[] dataArray;
            int num3;
            JobParam param2;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.UnitName);
            data = this.CreateUnitData(param);
            num = 0L;
            if (num == null)
            {
                goto Label_005F;
            }
            num2 = 0;
            goto Label_0051;
        Label_002E:
            if (data.Jobs[num2].UniqueID != num)
            {
                goto Label_004D;
            }
            data.SetJobIndex(num2);
            goto Label_005F;
        Label_004D:
            num2 += 1;
        Label_0051:
            if (num2 < ((int) data.Jobs.Length))
            {
                goto Label_002E;
            }
        Label_005F:
            dataArray = data.Jobs;
            num3 = 0;
            goto Label_00BF;
        Label_006F:
            param2 = ((dataArray == null) || (num3 >= ((int) dataArray.Length))) ? null : dataArray[num3].Param;
            DataSource.Bind<JobParam>(this.mJobRoot[num3], param2);
            this.mJobRoot[num3].SetActive((param2 == null) == 0);
            num3 += 1;
        Label_00BF:
            if (num3 < ((int) this.mJobRoot.Length))
            {
                goto Label_006F;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
            return;
        }

        private void FadeUnitImage(float alphastart, float alphaend, float duration)
        {
            this.mBGUnitImgAlphaStart = alphastart;
            this.mBGUnitImgAlphaEnd = alphaend;
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = duration;
            if (duration > 0f)
            {
                goto Label_0037;
            }
            this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
        Label_0037:
            return;
        }

        private void OnDestroy()
        {
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_0023;
            }
            GameUtility.DestroyGameObject(this.mCurrentPreview);
            this.mCurrentPreview = null;
        Label_0023:
            GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
            if ((this.mLeaderSkillDetail != null) == null)
            {
                goto Label_004F;
            }
            Object.Destroy(this.mLeaderSkillDetail.get_gameObject());
        Label_004F:
            GameUtility.DestroyGameObjects(this.mAbilits);
            return;
        }

        private void OpenLeaderSkillDetail()
        {
            UnitData data;
            if ((this.mLeaderSkillDetail == null) == null)
            {
                goto Label_0053;
            }
            if ((this.Prefab_LeaderSkillDetail != null) == null)
            {
                goto Label_0053;
            }
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            this.mLeaderSkillDetail = Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
            DataSource.Bind<UnitData>(this.mLeaderSkillDetail, data);
        Label_0053:
            return;
        }

        private void Refresh()
        {
            this.UnitName = GlobalVars.UnlockUnitID;
            this.DummyBind();
            this.UpdateUI();
            return;
        }

        private unsafe void RefreshAbilitList()
        {
            UnitData data;
            Transform transform;
            List<AbilityData> list;
            int num;
            AbilityData data2;
            GameObject obj2;
            GameObject obj3;
            GameObject obj4;
            ImageArray array;
            RarityParam param;
            int num2;
            OString[] strArray;
            int num3;
            string str;
            AbilityParam param2;
            GameObject obj5;
            GameObject obj6;
            ImageArray array2;
            if ((this.mAbilityTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GameUtility.DestroyGameObjects(this.mAbilits);
            this.mAbilits.Clear();
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            transform = this.mAbilityTemplate.get_transform().get_parent();
            list = data.GetAllLearnedAbilities(0);
            num = 0;
            goto Label_0103;
        Label_005C:
            data2 = list[num];
            if (data.MapEffectAbility != data2)
            {
                goto Label_0077;
            }
            goto Label_00FF;
        Label_0077:
            obj2 = Object.Instantiate<GameObject>(this.mAbilityTemplate);
            obj3 = obj2.get_transform().FindChild("icon").get_gameObject();
            obj2.get_transform().FindChild("locked").get_gameObject().SetActive(0);
            obj3.GetComponent<ImageArray>().ImageIndex = data2.SlotType;
            obj2.get_transform().SetParent(transform, 0);
            DataSource.Bind<AbilityData>(obj2, data2);
            obj2.SetActive(1);
            this.mAbilits.Add(obj2);
        Label_00FF:
            num += 1;
        Label_0103:
            if (num < list.Count)
            {
                goto Label_005C;
            }
            param = MonoSingleton<GameManager>.Instance.GetRarityParam(data.UnitParam.raremax);
            num2 = data.CurrentJob.Rank + 1;
            goto Label_022D;
        Label_013A:
            strArray = data.CurrentJob.Param.GetLearningAbilitys(num2);
            if (strArray != null)
            {
                goto Label_015A;
            }
            goto Label_0227;
        Label_015A:
            if (param.UnitJobLvCap >= num2)
            {
                goto Label_0172;
            }
            goto Label_0227;
        Label_0172:
            num3 = 0;
            goto Label_021C;
        Label_017A:
            str = *(&(strArray[num3]));
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_01A0;
            }
            goto Label_0216;
        Label_01A0:
            param2 = MonoSingleton<GameManager>.Instance.GetAbilityParam(str);
            obj5 = Object.Instantiate<GameObject>(this.mAbilityTemplate);
            obj5.get_transform().FindChild("icon").get_gameObject().GetComponent<ImageArray>().ImageIndex = param2.slot;
            obj5.get_transform().SetParent(transform, 0);
            DataSource.Bind<AbilityParam>(obj5, param2);
            obj5.SetActive(1);
            this.mAbilits.Add(obj5);
        Label_0216:
            num3 += 1;
        Label_021C:
            if (num3 < ((int) strArray.Length))
            {
                goto Label_017A;
            }
        Label_0227:
            num2 += 1;
        Label_022D:
            if (num2 < JobParam.MAX_JOB_RANK)
            {
                goto Label_013A;
            }
            GameParameter.UpdateAll(transform.get_gameObject());
            return;
        }

        private void RefreshLeaderSkillInfo()
        {
            UnitData data;
            bool flag;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (((data.LeaderSkill == null) == 0) == null)
            {
                goto Label_005F;
            }
            this.mLeaderSkillDetailButton.get_gameObject().SetActive(1);
            this.mLeaderSkillDetailButton.set_interactable(1);
            this.mLeaderSkillText.set_text(data.LeaderSkill.Name);
            goto Label_0085;
        Label_005F:
            this.mLeaderSkillDetailButton.get_gameObject().SetActive(0);
            this.mLeaderSkillText.set_text(LocalizedText.Get("sys.UNIT_LEADERSKILL_NOTHAVE_MESSAGE"));
        Label_0085:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshUnitImage()
        {
            <RefreshUnitImage>c__Iterator168 iterator;
            iterator = new <RefreshUnitImage>c__Iterator168();
            iterator.<>f__this = this;
            return iterator;
        }

        private void ReloadPreviewModels()
        {
            Type[] typeArray2;
            Type[] typeArray1;
            UnitData data;
            int num;
            UnitPreview preview;
            GameObject obj2;
            GameObject obj3;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0024;
            }
            if ((this.mPreviewParent == null) == null)
            {
                goto Label_0025;
            }
        Label_0024:
            return;
        Label_0025:
            GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
            this.mPreviewControllers.Clear();
            this.mCurrentPreview = null;
            num = 0;
            goto Label_00ED;
        Label_0049:
            preview = null;
            if (data.Jobs[num] == null)
            {
                goto Label_00DD;
            }
            if (data.Jobs[num].Param == null)
            {
                goto Label_00DD;
            }
            typeArray1 = new Type[] { typeof(UnitPreview) };
            obj2 = new GameObject("Preview", typeArray1);
            preview = obj2.GetComponent<UnitPreview>();
            preview.DefaultLayer = GameUtility.LayerHidden;
            preview.SetupUnit(data.UnitParam.iname, data.Jobs[num].JobID);
            obj2.get_transform().SetParent(this.mPreviewParent, 0);
            if (num != data.JobIndex)
            {
                goto Label_00DD;
            }
            this.mCurrentPreview = preview;
        Label_00DD:
            this.mPreviewControllers.Add(preview);
            num += 1;
        Label_00ED:
            if (num < ((int) data.Jobs.Length))
            {
                goto Label_0049;
            }
            if (this.mCurrentPreview == null)
            {
                goto Label_0178;
            }
            typeArray2 = new Type[] { typeof(UnitPreview) };
            obj3 = new GameObject("Preview", typeArray2);
            this.mCurrentPreview = obj3.GetComponent<UnitPreview>();
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
            this.mCurrentPreview.SetupUnit(data, -1);
            obj3.get_transform().SetParent(this.mPreviewParent, 0);
            this.mPreviewControllers.Add(this.mCurrentPreview);
        Label_0178:
            return;
        }

        private unsafe void SetUnitImageAlpha(float alpha)
        {
            Color color;
            if ((this.mBGUnitImage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            color = this.mBGUnitImage.get_color();
            &color.a = alpha;
            this.mBGUnitImage.set_color(color);
            return;
        }

        private void Update()
        {
            float num;
            if (this.mBGUnitImgFadeTime >= this.mBGUnitImgFadeTimeMax)
            {
                goto Label_0080;
            }
            if ((this.mBGUnitImage != null) == null)
            {
                goto Label_0080;
            }
            this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
            num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
            this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
            if (num < 1f)
            {
                goto Label_0080;
            }
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = 0f;
        Label_0080:
            return;
        }

        [DebuggerHidden]
        private IEnumerator UpdateFadeUnitImage()
        {
            <UpdateFadeUnitImage>c__Iterator167 iterator;
            iterator = new <UpdateFadeUnitImage>c__Iterator167();
            iterator.<>f__this = this;
            return iterator;
        }

        internal unsafe void UpdateUI()
        {
            UnitData data;
            JobData data2;
            int num;
            int num2;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            GameParameter.UpdateAll(base.get_gameObject());
            data2 = data.GetJobData(0);
            this.mRenkeiText.set_text(&data.GetCombination().ToString());
            if (data2 == null)
            {
                goto Label_007C;
            }
            this.mMoveText.set_text(&data2.Param.mov.ToString());
            this.mJumpText.set_text(&data2.Param.jmp.ToString());
        Label_007C:
            this.RefreshAbilitList();
            this.RefreshLeaderSkillInfo();
            this.ReloadPreviewModels();
            if (data2 == null)
            {
                goto Label_010B;
            }
            num = 0;
            goto Label_00FA;
        Label_009B:
            if (this.mPreviewControllers[num].UnitData != null)
            {
                goto Label_00B6;
            }
            goto Label_00F6;
        Label_00B6:
            if (this.mPreviewControllers[num].UnitData.JobIndex != null)
            {
                goto Label_00E6;
            }
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerCH1;
            goto Label_00F6;
        Label_00E6:
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
        Label_00F6:
            num += 1;
        Label_00FA:
            if (num < this.mPreviewControllers.Count)
            {
                goto Label_009B;
            }
        Label_010B:
            this.FadeUnitImage(0f, 0f, 0f);
            base.StartCoroutine(this.RefreshUnitImage());
            return;
        }

        [CompilerGenerated]
        private sealed class <RefreshUnitImage>c__Iterator168 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitData <unitData>__0;
            internal LoadRequest <req>__1;
            internal int $PC;
            internal object $current;
            internal UnitGetDetail <>f__this;

            public <RefreshUnitImage>c__Iterator168()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0095;

                    case 2:
                        goto Label_0107;
                }
                goto Label_010E;
            Label_0025:
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_00DF;
                }
                this.<unitData>__0 = DataSource.FindDataOfClass<UnitData>(this.<>f__this.get_gameObject(), null);
                this.<req>__1 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitImage2(this.<unitData>__0.UnitParam, this.<unitData>__0.CurrentJob.JobID));
                goto Label_0095;
            Label_0082:
                this.$current = null;
                this.$PC = 1;
                goto Label_0110;
            Label_0095:
                if (this.<req>__1.isDone == null)
                {
                    goto Label_0082;
                }
                this.<>f__this.mBGUnitImage.set_texture(this.<req>__1.asset as Texture2D);
                this.<>f__this.FadeUnitImage(0f, 1f, 1f);
            Label_00DF:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.UpdateFadeUnitImage());
                this.$PC = 2;
                goto Label_0110;
            Label_0107:
                this.$PC = -1;
            Label_010E:
                return 0;
            Label_0110:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateFadeUnitImage>c__Iterator167 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <t>__0;
            internal int $PC;
            internal object $current;
            internal UnitGetDetail <>f__this;

            public <UpdateFadeUnitImage>c__Iterator167()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00D9;

                    case 2:
                        goto Label_011C;
                }
                goto Label_0123;
            Label_0025:
                if (this.<>f__this.mBGUnitImgFadeTime >= this.<>f__this.mBGUnitImgFadeTimeMax)
                {
                    goto Label_0109;
                }
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_0109;
                }
                this.<t>__0 = 0f;
            Label_0061:
                this.<>f__this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
                this.<t>__0 = Mathf.Clamp01(this.<>f__this.mBGUnitImgFadeTime / this.<>f__this.mBGUnitImgFadeTimeMax);
                this.<>f__this.SetUnitImageAlpha(Mathf.Lerp(this.<>f__this.mBGUnitImgAlphaStart, this.<>f__this.mBGUnitImgAlphaEnd, this.<t>__0));
                this.$current = null;
                this.$PC = 1;
                goto Label_0125;
            Label_00D9:
                if (this.<t>__0 < 1f)
                {
                    goto Label_0061;
                }
                this.<>f__this.mBGUnitImgFadeTime = 0f;
                this.<>f__this.mBGUnitImgFadeTimeMax = 0f;
            Label_0109:
                this.$current = null;
                this.$PC = 2;
                goto Label_0125;
            Label_011C:
                this.$PC = -1;
            Label_0123:
                return 0;
            Label_0125:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

