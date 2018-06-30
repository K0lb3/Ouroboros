namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class GachaResultUnitDetail : MonoBehaviour
    {
        public string PreviewParentID;
        public string PreviewBaseID;
        private string BGUnitImageID;
        public GameObject UnitInfo;
        public GameObject JobInfo;
        public GameObject LeaderSkillInfo;
        public GameObject AbilityTemplate;
        public Text UnitLv;
        public Text UnitLvMax;
        public Text Status_HP;
        public Text Status_Atk;
        public Text Status_Def;
        public Text Status_Mag;
        public Text Status_Mnd;
        public Text Status_Rec;
        public Text Status_Dex;
        public Text Status_Speed;
        public Text Status_Cri;
        public Text Status_Luck;
        public Text Status_Renkei;
        [SerializeField]
        private Text Status_Move;
        [SerializeField]
        private Text Status_Jump;
        [SerializeField]
        private GameObject JobData01;
        [SerializeField]
        private GameObject JobData02;
        [SerializeField]
        private GameObject JobData03;
        private UnitData mCurrentUnit;
        private int mCurrentUnitIndex;
        private Text[] mStatusParamSlots;
        private Transform mPreviewParent;
        private GameObject mPreviewBase;
        private UnitPreview mCurrentPreview;
        private List<UnitPreview> mPreviewControllers;
        private RawImage mBGUnitImage;
        public Button LeaderSkillDetailButton;
        private GameObject mLeaderSkillDetail;
        [SerializeField]
        private GameObject Prefab_LeaderSkillDetail;
        [SerializeField]
        private string Prefab_LeaderSkillDetailPath;
        [SerializeField]
        private Text LeaderSkillName;
        private bool mDesiredPreviewVisibility;
        private bool mUpdatePreviewVisibility;
        private float mBGUnitImgAlphaStart;
        private float mBGUnitImgAlphaEnd;
        private float mBGUnitImgFadeTime;
        private float mBGUnitImgFadeTimeMax;
        private List<GameObject> mAbilits;

        public GachaResultUnitDetail()
        {
            this.PreviewParentID = "GACHARESULTUNITPREVIEW";
            this.PreviewBaseID = "GACHARESULTUNITPREVIEWBASE";
            this.BGUnitImageID = "GACHA_UNIT_IMG";
            this.mPreviewControllers = new List<UnitPreview>();
            this.Prefab_LeaderSkillDetailPath = "UI/";
            this.mAbilits = new List<GameObject>();
            base..ctor();
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
            if (uparam.jobsets == null)
            {
                goto Label_0117;
            }
            if (((int) uparam.jobsets.Length) <= 0)
            {
                goto Label_0117;
            }
            list = new List<Json_Job>((int) uparam.jobsets.Length);
            num = 1;
            num2 = 0;
            goto Label_00FC;
        Label_0093:
            param = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[num2]);
            if (param != null)
            {
                goto Label_00B4;
            }
            goto Label_00F6;
        Label_00B4:
            job = new Json_Job();
            job.iid = (long) num++;
            job.iname = param.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            list.Add(job);
        Label_00F6:
            num2 += 1;
        Label_00FC:
            if (num2 < ((int) uparam.jobsets.Length))
            {
                goto Label_0093;
            }
            unit.jobs = list.ToArray();
        Label_0117:
            data.Deserialize(unit);
            data.SetUniqueID(1L);
            data.JobRankUp(0);
            return data;
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

        private unsafe void OnDisable()
        {
            Vector3 vector;
            Vector3 vector2;
            this.SetPreviewVisible(0);
            if ((this.mPreviewBase != null) == null)
            {
                goto Label_006E;
            }
            this.mPreviewBase.get_transform().set_position(new Vector3(0.2f, &this.mPreviewBase.get_transform().get_position().y, &this.mPreviewBase.get_transform().get_position().z));
            this.mPreviewBase.SetActive(0);
        Label_006E:
            this.FadeUnitImage(0f, 0f, 0f);
            return;
        }

        private void OnEnable()
        {
            if ((this.JobData01 != null) == null)
            {
                goto Label_001D;
            }
            this.JobData01.SetActive(0);
        Label_001D:
            if ((this.JobData02 != null) == null)
            {
                goto Label_003A;
            }
            this.JobData02.SetActive(0);
        Label_003A:
            if ((this.JobData03 != null) == null)
            {
                goto Label_0057;
            }
            this.JobData03.SetActive(0);
        Label_0057:
            if ((this.LeaderSkillDetailButton != null) == null)
            {
                goto Label_0074;
            }
            this.LeaderSkillDetailButton.set_interactable(0);
        Label_0074:
            this.Refresh();
            return;
        }

        private void OpenLeaderSkillDetail()
        {
            Canvas canvas;
            if ((this.mLeaderSkillDetail == null) == null)
            {
                goto Label_0064;
            }
            if ((this.Prefab_LeaderSkillDetail != null) == null)
            {
                goto Label_0064;
            }
            this.mLeaderSkillDetail = Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
            DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit);
            canvas = this.mLeaderSkillDetail.GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_0064;
            }
            canvas.set_sortingOrder(10);
        Label_0064:
            return;
        }

        private unsafe void Refresh()
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3 vector4;
            this.mStatusParamSlots = new Text[StatusParam.MAX_STATUS];
            this.mStatusParamSlots[0] = this.Status_HP;
            this.mStatusParamSlots[3] = this.Status_Atk;
            this.mStatusParamSlots[4] = this.Status_Def;
            this.mStatusParamSlots[5] = this.Status_Mag;
            this.mStatusParamSlots[6] = this.Status_Mnd;
            this.mStatusParamSlots[7] = this.Status_Rec;
            this.mStatusParamSlots[8] = this.Status_Dex;
            this.mStatusParamSlots[9] = this.Status_Speed;
            this.mStatusParamSlots[10] = this.Status_Cri;
            this.mStatusParamSlots[11] = this.Status_Luck;
            if (this.mCurrentUnit != null)
            {
                goto Label_00AB;
            }
            return;
        Label_00AB:
            if (string.IsNullOrEmpty(this.PreviewParentID) != null)
            {
                goto Label_0116;
            }
            this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PreviewParentID);
            this.mPreviewParent.get_transform().set_position(new Vector3(-0.2f, &this.mPreviewParent.get_transform().get_position().y, &this.mPreviewParent.get_transform().get_position().z));
        Label_0116:
            if (string.IsNullOrEmpty(this.PreviewBaseID) != null)
            {
                goto Label_01AF;
            }
            this.mPreviewBase = GameObjectID.FindGameObject(this.PreviewBaseID);
            if ((this.mPreviewBase != null) == null)
            {
                goto Label_01AF;
            }
            GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerUI, 1);
            this.mPreviewBase.get_transform().set_position(new Vector3(-0.2f, &this.mPreviewBase.get_transform().get_position().y, &this.mPreviewBase.get_transform().get_position().z));
            this.mPreviewBase.SetActive(0);
        Label_01AF:
            if (string.IsNullOrEmpty(this.BGUnitImageID) != null)
            {
                goto Label_01D0;
            }
            this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
        Label_01D0:
            base.StartCoroutine(this.RefreshAsync(0));
            return;
        }

        private unsafe void RefreshAbilitList()
        {
            List<AbilityData> list;
            int num;
            AbilityData data;
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
            if ((this.AbilityTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GameUtility.DestroyGameObjects(this.mAbilits);
            this.mAbilits.Clear();
            list = this.mCurrentUnit.GetAllLearnedAbilities(0);
            num = 0;
            goto Label_00EC;
        Label_003C:
            data = list[num];
            if (this.mCurrentUnit.MapEffectAbility != data)
            {
                goto Label_005A;
            }
            goto Label_00E8;
        Label_005A:
            obj2 = Object.Instantiate<GameObject>(this.AbilityTemplate);
            obj3 = obj2.get_transform().FindChild("icon").get_gameObject();
            obj2.get_transform().FindChild("locked").get_gameObject().SetActive(0);
            obj3.GetComponent<ImageArray>().ImageIndex = data.SlotType;
            obj2.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), 0);
            DataSource.Bind<AbilityData>(obj2, data);
            obj2.SetActive(1);
            this.mAbilits.Add(obj2);
        Label_00E8:
            num += 1;
        Label_00EC:
            if (num < list.Count)
            {
                goto Label_003C;
            }
            param = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentUnit.UnitParam.raremax);
            num2 = this.mCurrentUnit.CurrentJob.Rank + 1;
            goto Label_0234;
        Label_012D:
            strArray = this.mCurrentUnit.CurrentJob.Param.GetLearningAbilitys(num2);
            if (strArray != null)
            {
                goto Label_0152;
            }
            goto Label_022E;
        Label_0152:
            if (param.UnitJobLvCap >= num2)
            {
                goto Label_016A;
            }
            goto Label_022E;
        Label_016A:
            num3 = 0;
            goto Label_0223;
        Label_0172:
            str = *(&(strArray[num3]));
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0198;
            }
            goto Label_021D;
        Label_0198:
            param2 = MonoSingleton<GameManager>.Instance.GetAbilityParam(str);
            obj5 = Object.Instantiate<GameObject>(this.AbilityTemplate);
            obj5.get_transform().FindChild("icon").get_gameObject().GetComponent<ImageArray>().ImageIndex = param2.slot;
            obj5.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), 0);
            DataSource.Bind<AbilityParam>(obj5, param2);
            obj5.SetActive(1);
            this.mAbilits.Add(obj5);
        Label_021D:
            num3 += 1;
        Label_0223:
            if (num3 < ((int) strArray.Length))
            {
                goto Label_0172;
            }
        Label_022E:
            num2 += 1;
        Label_0234:
            if (num2 < JobParam.MAX_JOB_RANK)
            {
                goto Label_012D;
            }
            GameParameter.UpdateAll(this.AbilityTemplate.get_transform().get_parent().get_gameObject());
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshAsync(bool immediate)
        {
            <RefreshAsync>c__Iterator110 iterator;
            iterator = new <RefreshAsync>c__Iterator110();
            iterator.immediate = immediate;
            iterator.<$>immediate = immediate;
            iterator.<>f__this = this;
            return iterator;
        }

        private void RefreshJobInfo()
        {
            JobData[] dataArray;
            if ((this.JobInfo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            dataArray = this.mCurrentUnit.Jobs;
            DataSource.Bind<JobParam>(this.JobData01, dataArray[0].Param);
            this.JobData01.SetActive(1);
            if (((int) dataArray.Length) < 2)
            {
                goto Label_0065;
            }
            DataSource.Bind<JobParam>(this.JobData02, dataArray[1].Param);
            this.JobData02.SetActive(1);
        Label_0065:
            if (((int) dataArray.Length) < 3)
            {
                goto Label_008D;
            }
            DataSource.Bind<JobParam>(this.JobData03, dataArray[2].Param);
            this.JobData03.SetActive(1);
        Label_008D:
            GameParameter.UpdateAll(this.JobInfo);
            return;
        }

        private void RefreshLeaderSkillInfo()
        {
            if ((this.LeaderSkillInfo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mCurrentUnit.LeaderSkill == null)
            {
                goto Label_004E;
            }
            this.LeaderSkillDetailButton.set_interactable(1);
            this.LeaderSkillName.set_text(this.mCurrentUnit.LeaderSkill.Name);
            goto Label_0063;
        Label_004E:
            this.LeaderSkillName.set_text(LocalizedText.Get("sys.UNIT_LEADERSKILL_NOTHAVE_MESSAGE"));
        Label_0063:
            return;
        }

        private unsafe void RefreshStatus()
        {
            RarityParam param;
            int num;
            JobData data;
            OInt num2;
            int num3;
            DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit);
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.mCurrentUnit.Rarity);
            this.UnitLv.set_text("1");
            this.UnitLvMax.set_text(&param.UnitLvCap.ToString());
            num = 0;
            goto Label_009B;
        Label_0059:
            if ((this.mStatusParamSlots[num] != null) == null)
            {
                goto Label_0097;
            }
            num2 = this.mCurrentUnit.Status.param[num];
            this.mStatusParamSlots[num].set_text(&num2.ToString());
        Label_0097:
            num += 1;
        Label_009B:
            if (num < StatusParam.MAX_STATUS)
            {
                goto Label_0059;
            }
            this.Status_Renkei.set_text(&this.mCurrentUnit.GetCombination().ToString());
            data = this.mCurrentUnit.GetJobData(0);
            this.Status_Move.set_text(&data.Param.mov.ToString());
            this.Status_Jump.set_text(&data.Param.jmp.ToString());
            GameParameter.UpdateAll(this.UnitInfo);
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshUnitImage()
        {
            <RefreshUnitImage>c__Iterator111 iterator;
            iterator = new <RefreshUnitImage>c__Iterator111();
            iterator.<>f__this = this;
            return iterator;
        }

        private void ReloadPreviewModels()
        {
            Type[] typeArray1;
            GameObject obj2;
            if (this.mCurrentUnit == null)
            {
                goto Label_001C;
            }
            if ((this.mPreviewParent == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
            this.mPreviewControllers.Clear();
            this.mCurrentPreview = null;
            if (this.mCurrentPreview != null)
            {
                goto Label_00B9;
            }
            typeArray1 = new Type[] { typeof(UnitPreview) };
            obj2 = new GameObject("Preview", typeArray1);
            this.mCurrentPreview = obj2.GetComponent<UnitPreview>();
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
            this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
            obj2.get_transform().SetParent(this.mPreviewParent, 0);
            this.mPreviewControllers.Add(this.mCurrentPreview);
        Label_00B9:
            return;
        }

        private unsafe void SetPreviewVisible(bool visible)
        {
            Vector3 vector;
            Vector3 vector2;
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_00B8;
            }
            this.mDesiredPreviewVisibility = visible;
            if (visible != null)
            {
                goto Label_007E;
            }
            this.mPreviewParent.get_transform().set_position(new Vector3(0.2f, &this.mPreviewParent.get_transform().get_position().y, &this.mPreviewParent.get_transform().get_position().z));
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerHidden, 1);
            goto Label_0085;
        Label_007E:
            this.mUpdatePreviewVisibility = 1;
        Label_0085:
            if ((this.mPreviewBase != null) == null)
            {
                goto Label_00B8;
            }
            if (this.mPreviewBase.get_activeSelf() != null)
            {
                goto Label_00B8;
            }
            if (visible == null)
            {
                goto Label_00B8;
            }
            this.mPreviewBase.SetActive(1);
        Label_00B8:
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

        public void Setup(UnitData _data)
        {
            this.mCurrentUnit = _data;
            return;
        }

        public void Setup(int index)
        {
            UnitParam param;
            UnitData data;
            param = GachaResultData.drops[index].unit;
            if (param == null)
            {
                goto Label_0022;
            }
            data = this.CreateUnitData(param);
            this.Setup(data);
        Label_0022:
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowLeaderSkillDetail(string _path)
        {
            <ShowLeaderSkillDetail>c__Iterator10F iteratorf;
            iteratorf = new <ShowLeaderSkillDetail>c__Iterator10F();
            iteratorf._path = _path;
            iteratorf.<$>_path = _path;
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        private void Start()
        {
            if ((this.LeaderSkillDetailButton != null) == null)
            {
                goto Label_002D;
            }
            this.LeaderSkillDetailButton.get_onClick().AddListener(new UnityAction(this, this.OpenLeaderSkillDetail));
        Label_002D:
            return;
        }

        private void Update()
        {
            float num;
            if (this.mUpdatePreviewVisibility == null)
            {
                goto Label_004F;
            }
            if (this.mDesiredPreviewVisibility == null)
            {
                goto Label_004F;
            }
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_004F;
            }
            if (this.mCurrentPreview.IsLoading != null)
            {
                goto Label_004F;
            }
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerUI, 1);
            this.mUpdatePreviewVisibility = 0;
        Label_004F:
            if (this.mBGUnitImgFadeTime >= this.mBGUnitImgFadeTimeMax)
            {
                goto Label_00CF;
            }
            if ((this.mBGUnitImage != null) == null)
            {
                goto Label_00CF;
            }
            this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
            num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
            this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
            if (num < 1f)
            {
                goto Label_00CF;
            }
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = 0f;
        Label_00CF:
            return;
        }

        [CompilerGenerated]
        private sealed class <RefreshAsync>c__Iterator110 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool immediate;
            internal int $PC;
            internal object $current;
            internal bool <$>immediate;
            internal GachaResultUnitDetail <>f__this;

            public <RefreshAsync>c__Iterator110()
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
                        goto Label_0021;

                    case 1:
                        goto Label_00C2;
                }
                goto Label_0104;
            Label_0021:
                this.<>f__this.FadeUnitImage(0f, 0f, 0f);
                this.<>f__this.StartCoroutine(this.<>f__this.RefreshUnitImage());
                this.<>f__this.RefreshStatus();
                this.<>f__this.RefreshJobInfo();
                this.<>f__this.RefreshLeaderSkillInfo();
                this.<>f__this.RefreshAbilitList();
                this.<>f__this.ReloadPreviewModels();
                if ((this.<>f__this.mCurrentPreview != null) == null)
                {
                    goto Label_00E3;
                }
                if (this.immediate != null)
                {
                    goto Label_00D7;
                }
                goto Label_00C2;
            Label_00AF:
                this.$current = null;
                this.$PC = 1;
                goto Label_0106;
            Label_00C2:
                if (this.<>f__this.mCurrentPreview.IsLoading != null)
                {
                    goto Label_00AF;
                }
            Label_00D7:
                this.<>f__this.SetPreviewVisible(1);
            Label_00E3:
                this.<>f__this.FadeUnitImage(0f, 1f, 1f);
                this.$PC = -1;
            Label_0104:
                return 0;
            Label_0106:
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
        private sealed class <RefreshUnitImage>c__Iterator111 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal GachaResultUnitDetail <>f__this;

            public <RefreshUnitImage>c__Iterator111()
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
                        goto Label_009D;

                    case 2:
                        goto Label_00F1;
                }
                goto Label_00F8;
            Label_0025:
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_00DE;
                }
                this.<req>__0 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitImage2(this.<>f__this.mCurrentUnit.UnitParam, this.<>f__this.mCurrentUnit.CurrentJob.JobID));
                if (this.<req>__0.isDone != null)
                {
                    goto Label_009D;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00FA;
            Label_009D:
                if (this.<req>__0 == null)
                {
                    goto Label_00DE;
                }
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_00DE;
                }
                this.<>f__this.mBGUnitImage.set_texture(this.<req>__0.asset as Texture2D);
            Label_00DE:
                this.$current = null;
                this.$PC = 2;
                goto Label_00FA;
            Label_00F1:
                this.$PC = -1;
            Label_00F8:
                return 0;
            Label_00FA:
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
        private sealed class <ShowLeaderSkillDetail>c__Iterator10F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string _path;
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal string <$>_path;
            internal GachaResultUnitDetail <>f__this;

            public <ShowLeaderSkillDetail>c__Iterator10F()
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
                        goto Label_0021;

                    case 1:
                        goto Label_0083;
                }
                goto Label_00F6;
            Label_0021:
                if (string.IsNullOrEmpty(this._path) == null)
                {
                    goto Label_0040;
                }
                DebugUtility.LogError("リーダースキル詳細のprefab指定がありません");
                goto Label_00F6;
            Label_0040:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_LeaderSkillDetailPath);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0083;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00F8;
            Label_0083:
                if ((this.<>f__this.mLeaderSkillDetail == null) == null)
                {
                    goto Label_00D4;
                }
                this.<>f__this.mLeaderSkillDetail = Object.Instantiate(this.<req>__0.asset) as GameObject;
                if ((this.<>f__this.mLeaderSkillDetail == null) == null)
                {
                    goto Label_00D4;
                }
                goto Label_00F6;
            Label_00D4:
                DataSource.Bind<UnitData>(this.<>f__this.mLeaderSkillDetail, this.<>f__this.mCurrentUnit);
                this.$PC = -1;
            Label_00F6:
                return 0;
            Label_00F8:
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

