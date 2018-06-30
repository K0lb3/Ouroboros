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
    using UnityEngine.UI;

    [Pin(250, "Setuped CardUnitAnim", 1, 250), Pin(1, "AllSkip", 0, 1), Pin(100, "InputNextBtn", 0, 100), Pin(0x65, "NextBtnEnable", 1, 0x65), Pin(0x66, "NextBtnDisable", 1, 0x66), Pin(200, "Setup CardUnitRarity", 0, 200), Pin(0xc9, "Finish CardUnitAnim", 0, 0xc9), Pin(0xca, "Next Gacha Phase", 0, 0xca), Pin(0xfb, "Started CardUnitAnim", 1, 0xfb), Pin(0xfc, "Finished CardUnitAnim", 1, 0xfc)]
    public class GachaController : MonoSingleton<GachaController>, IFlowInterface
    {
        private const float MinWaitBeforMoveDropStone = 2f;
        public GameObject DropStone;
        public GameObject DropStone_CCard;
        public GameObject DropMaterial;
        public RawImage DropMaterialImage;
        public RawImage DropMaterialBlurImage;
        public RawImage DropMaterialIconImage;
        public Image DropMaterialIconFrameImage;
        public GameObject ItemThumbnailPrefab;
        public GameObject UnitThumbnailPrefab;
        public GameObject ArtifactThumbnailPrefab;
        public Transform ThumbnailPool;
        public GameObject StartArrowB;
        public GameObject StartArrowTop;
        public GameObject[] StartArrowTopMasks;
        public GameObject StartStone;
        public GameObject StartStoneMask;
        public GameObject StartStoneEff01;
        public GameObject StartStoneEff02;
        public GameObject StartStoneEff03;
        public Text MaterialName;
        public Text MaterialComment;
        public Text MaterialCount;
        private Color[] FlickEffectColor01;
        private Color[] FlickEffectColor02;
        private Color[] FlickEffectColor03;
        public GameObject[] ResetMaterials;
        public Sprite[] StartArrowSprite;
        public Sprite[] StartArrowTopSprite;
        public Sprite[] StartStoneSprite;
        private static TouchController mTouchController;
        public GameObject GaugeObject;
        public bool OnShardEffect;
        private StateMachine<GachaController> mState;
        private bool isSkipping;
        private List<GameObject> mDropStones;
        private GameObject mDropMaterial;
        public GameObject OpenMaterial;
        public GameObject OpenItem;
        private bool mIgnoreDragVelocity;
        private bool mDraged;
        private bool mDraging;
        private bool mClicked;
        private float mDragY;
        private float mDragX;
        private float mDragEndX;
        private float mDragEndY;
        public float MIN_SWIPE_DIST_X;
        public float MIN_SWIPE_DIST_Y;
        public float StoneRadius;
        public float StoneAppear;
        public Texture[] StoneBases;
        public Texture[] StoneHand01s;
        public Texture[] StoneHand02s;
        public Texture[] StoneEye01s;
        public Texture[] StoneEye02s;
        public Texture stoneBaseN;
        public Sprite[] LithographBases;
        public float StoneRotateTime;
        public Text ConceptCardNameText;
        public ImageArray ConceptCardFrame;
        public Texture[] ConceptCardStoneBases;
        private List<GameObject> mUseThumbnailList;
        private MySound.VolumeHandle mBGMVolume;
        private MySound.PlayHandle mJingleHandle;
        private bool AllAnimSkip;
        private bool mLithograph;
        private static readonly int MAX_VIEW_STONE;
        private int mViewStoneCount;
        private int mMaxPage;
        private int mCurrentPage;
        private bool IsOverMaxView;
        private bool IsNextDropSet;
        private GachaFlowType mFlowType;
        private GameObject item_root;
        private int thumbnail_count;
        private List<GameObject> mUnitTempList;
        private List<GameObject> mItemTempList;
        private List<GameObject> mArtifactTempList;
        public float StoneCenterHeight;
        private List<GameObject> m_TempList;
        [SerializeField]
        private GameObject ThumbnailTemlate;
        [SerializeField]
        private Animator GetUnitAnimator;
        [SerializeField]
        private RawImage GetUnitImage;
        [SerializeField]
        private RawImage GetUnitBlurImage;
        [SerializeField]
        private Text GetUnitDescriptionText;
        [CompilerGenerated]
        private DropInfo <DropCurrent>k__BackingField;
        [CompilerGenerated]
        private bool <FinishedCardUnitAnimation>k__BackingField;
        [CompilerGenerated]
        private bool <NextGachaPhase>k__BackingField;

        static GachaController()
        {
            MAX_VIEW_STONE = 10;
            return;
        }

        public unsafe GachaController()
        {
            Color[] colorArray3;
            Color[] colorArray2;
            Color[] colorArray1;
            colorArray1 = new Color[3];
            *(&(colorArray1[0])) = new Color(75f, 166f, 255f);
            *(&(colorArray1[1])) = new Color(255f, 250f, 128f);
            *(&(colorArray1[2])) = new Color(255f, 30f, 13f);
            this.FlickEffectColor01 = colorArray1;
            colorArray2 = new Color[3];
            *(&(colorArray2[0])) = new Color(30f, 250f, 255f);
            *(&(colorArray2[1])) = new Color(255f, 250f, 128f);
            *(&(colorArray2[2])) = new Color(255f, 0f, 0f);
            this.FlickEffectColor02 = colorArray2;
            colorArray3 = new Color[3];
            *(&(colorArray3[0])) = new Color(0f, 83f, 218f);
            *(&(colorArray3[1])) = new Color(255f, 226f, 138f);
            *(&(colorArray3[2])) = new Color(255f, 24f, 15f);
            this.FlickEffectColor03 = colorArray3;
            this.OnShardEffect = 1;
            this.mDropStones = new List<GameObject>();
            this.MIN_SWIPE_DIST_X = 400f;
            this.MIN_SWIPE_DIST_Y = 400f;
            this.mUseThumbnailList = new List<GameObject>(10);
            this.mLithograph = 1;
            this.mUnitTempList = new List<GameObject>(10);
            this.mItemTempList = new List<GameObject>(10);
            this.mArtifactTempList = new List<GameObject>(10);
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0013;
            }
            this.AllAnimSkip = 1;
            goto Label_0066;
        Label_0013:
            if (pinID != 100)
            {
                goto Label_0027;
            }
            this.IsNextDropSet = 1;
            goto Label_0066;
        Label_0027:
            if (pinID != 200)
            {
                goto Label_003D;
            }
            this.StartGetCardUnitAnim();
            goto Label_0066;
        Label_003D:
            if (pinID != 0xc9)
            {
                goto Label_0054;
            }
            this.FinishedCardUnitAnimation = 1;
            goto Label_0066;
        Label_0054:
            if (pinID != 0xca)
            {
                goto Label_0066;
            }
            this.NextGachaPhase = 1;
        Label_0066:
            return;
        }

        private bool CheckSkip()
        {
            return ((this.AllAnimSkip != null) ? 0 : mTouchController.IsTouching);
        }

        private unsafe Color ConvertColor(Color color)
        {
            return new Color(&color.r / 255f, &color.g / 255f, &color.b / 255f);
        }

        [DebuggerHidden]
        private IEnumerator CreateDropInfo()
        {
            <CreateDropInfo>c__Iterator6D iteratord;
            iteratord = new <CreateDropInfo>c__Iterator6D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        public static ArtifactData CreateTempArtifactData(ArtifactParam param, int rarity)
        {
            ArtifactData data;
            Json_Artifact artifact;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.iname = param.iname;
            artifact.exp = 0;
            artifact.fav = 0;
            artifact.rare = rarity;
            data.Deserialize(artifact);
            return data;
        }

        [DebuggerHidden]
        private IEnumerator CreateThumbnailObject(GachaDropData.Type type)
        {
            <CreateThumbnailObject>c__Iterator70 iterator;
            iterator = new <CreateThumbnailObject>c__Iterator70();
            iterator.type = type;
            iterator.<$>type = type;
            iterator.<>f__this = this;
            return iterator;
        }

        private void CreateTouchArea()
        {
            Type[] typeArray1;
            GameObject obj2;
            Canvas canvas;
            if ((null != mTouchController) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            typeArray1 = new Type[] { typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasStack), typeof(NullGraphic), typeof(TouchController), typeof(SRPG_CanvasScaler) };
            obj2 = new GameObject("TouchArea", typeArray1);
            mTouchController = obj2.GetComponent<TouchController>();
            mTouchController.OnClick = new TouchController.ClickEvent(this.OnClick);
            mTouchController.OnDragDelegate = (TouchController.DragEvent) Delegate.Combine(mTouchController.OnDragDelegate, new TouchController.DragEvent(this.OnDrag));
            mTouchController.OnDragEndDelegate = (TouchController.DragEvent) Delegate.Combine(mTouchController.OnDragEndDelegate, new TouchController.DragEvent(this.OnDragEnd));
            obj2.GetComponent<Canvas>().set_renderMode(0);
            obj2.GetComponent<CanvasStack>().Priority = 0;
            obj2.GetComponent<GraphicRaycaster>().set_enabled(1);
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

        private void DestroyTouchArea()
        {
            if ((null != mTouchController) == null)
            {
                goto Label_0025;
            }
            Object.Destroy(mTouchController.get_gameObject());
            mTouchController = null;
        Label_0025:
            return;
        }

        public GameObject GetDropStone(GachaDropData _drop)
        {
            GameObject obj2;
            obj2 = null;
            if (_drop != null)
            {
                goto Label_000A;
            }
            return null;
        Label_000A:
            if (_drop.conceptcard == null)
            {
                goto Label_002D;
            }
            if (this.mFlowType == 1)
            {
                goto Label_002D;
            }
            obj2 = this.DropStone_CCard;
            goto Label_0034;
        Label_002D:
            obj2 = this.DropStone;
        Label_0034:
            return obj2;
        }

        private int GetRarityTextureIndex(int rarity)
        {
            if (rarity < 5)
            {
                goto Label_0009;
            }
            return 2;
        Label_0009:
            if (rarity < 4)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            return 0;
        }

        private GameObject GetThumbnailObject(int _index)
        {
            return (((this.m_TempList == null) || (this.m_TempList.Count <= _index)) ? null : this.m_TempList[_index]);
        }

        private GameObject GetThumbnailTypeObject(GameObject _thumbnail, GachaDropData.Type _type)
        {
            GameObject obj2;
            SerializeValueBehaviour behaviour;
            obj2 = null;
            if ((_thumbnail != null) == null)
            {
                goto Label_0090;
            }
            behaviour = _thumbnail.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0090;
            }
            if (_type != 1)
            {
                goto Label_003E;
            }
            obj2 = behaviour.list.GetGameObject("item");
            goto Label_0090;
        Label_003E:
            if (_type != 2)
            {
                goto Label_005B;
            }
            obj2 = behaviour.list.GetGameObject("unit");
            goto Label_0090;
        Label_005B:
            if (_type != 3)
            {
                goto Label_0078;
            }
            obj2 = behaviour.list.GetGameObject("artifact");
            goto Label_0090;
        Label_0078:
            if (_type != 4)
            {
                goto Label_0090;
            }
            obj2 = behaviour.list.GetGameObject("concept_card");
        Label_0090:
            return obj2;
        }

        [DebuggerHidden]
        private IEnumerator InitTempList()
        {
            <InitTempList>c__Iterator6F iteratorf;
            iteratorf = new <InitTempList>c__Iterator6F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        private bool InitThumbnailTemplateList()
        {
            int num;
            GameObject obj2;
            if ((this.ThumbnailTemlate == null) == null)
            {
                goto Label_001D;
            }
            DebugUtility.LogError("サムネイルのテンプレートが指定されていません");
            return 0;
        Label_001D:
            this.m_TempList = new List<GameObject>(MAX_VIEW_STONE);
            num = 0;
            goto Label_0062;
        Label_0034:
            obj2 = Object.Instantiate<GameObject>(this.ThumbnailTemlate);
            obj2.get_transform().SetParent(this.ThumbnailPool, 0);
            this.m_TempList.Add(obj2);
            num += 1;
        Label_0062:
            if (num < MAX_VIEW_STONE)
            {
                goto Label_0034;
            }
            return 1;
        }

        public bool IsAssetDownloadDone()
        {
            if (GlobalVars.IsTutorialEnd != null)
            {
                goto Label_000C;
            }
            return 1;
        Label_000C:
            return AssetDownloader.isDone;
        }

        private void OnClick(Vector2 screenPosition)
        {
            if ((((this.mState.IsInState<State_WaitDropMaterial>() == null) && (this.mState.IsInState<State_WaitDropmaterialT>() == null)) && ((this.mState.IsInState<State_WaitDropmaterialShard>() == null) && (this.mState.IsInState<State_WaitEndInput>() == null))) && (((this.mState.IsInState<State_WaitGaugeAnimation>() == null) && (this.mState.IsInState<State_WaitBeforeSummons>() == null)) && (this.mState.IsInState<State_WaitCardAnim>() == null)))
            {
                goto Label_0088;
            }
            this.mClicked = (this.AllAnimSkip == null) ? 1 : 0;
        Label_0088:
            return;
        }

        private void OnDestroy()
        {
            this.DestroyTouchArea();
            if (this.mBGMVolume == null)
            {
                goto Label_0023;
            }
            this.mBGMVolume.Discard();
            this.mBGMVolume = null;
        Label_0023:
            return;
        }

        private unsafe void OnDrag()
        {
            if (this.mIgnoreDragVelocity != null)
            {
                goto Label_0051;
            }
            this.mDraged = 0;
            this.mDraging = 1;
            this.mDragX += &mTouchController.DragDelta.x;
            this.mDragY += &mTouchController.DragDelta.y;
        Label_0051:
            return;
        }

        private unsafe void OnDragEnd()
        {
            Vector2 vector;
            Vector2 vector2;
            this.mDragEndX = &mTouchController.DragStart.x + this.mDragX;
            this.mDragEndY = &mTouchController.DragStart.y + this.mDragY;
            this.mDraged = 1;
            this.mDraging = 0;
            this.mIgnoreDragVelocity = 0;
            return;
        }

        public void PlayJINGLE0010()
        {
            this.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0010", "JIN_0010", 1, 0f);
            return;
        }

        public unsafe void RefreshGachaImageSize(RectTransform _rect_tf, GachaDropData.Type _type)
        {
            Vector2 vector;
            if ((_rect_tf == null) == null)
            {
                goto Label_0017;
            }
            DebugUtility.LogError("演出表示に必要なオブジェクトが設定されていません");
            return;
        Label_0017:
            &vector..ctor(1024f, 1024f);
            if (_type != 4)
            {
                goto Label_0040;
            }
            &vector..ctor(1024f, 612f);
        Label_0040:
            _rect_tf.set_sizeDelta(vector);
            return;
        }

        public unsafe void RefreshThumbnailList()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            if (this.mUseThumbnailList != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            enumerator = this.mUseThumbnailList.GetEnumerator();
        Label_0018:
            try
            {
                goto Label_007B;
            Label_001D:
                obj2 = &enumerator.Current;
                if ((obj2 == null) == null)
                {
                    goto Label_0036;
                }
                goto Label_007B;
            Label_0036:
                DataSource.Bind<UnitData>(obj2, null);
                DataSource.Bind<ItemData>(obj2, null);
                DataSource.Bind<ArtifactData>(obj2, null);
                GameParameter.UpdateAll(obj2);
                obj2.SetActive(0);
                if ((this.ThumbnailPool != null) == null)
                {
                    goto Label_007B;
                }
                obj2.get_transform().SetParent(this.ThumbnailPool, 0);
            Label_007B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_0098;
            }
            finally
            {
            Label_008C:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0098:
            this.mUseThumbnailList.Clear();
            return;
        }

        private void ResetGetCardUnitAnim()
        {
            if ((this.GetUnitAnimator != null) == null)
            {
                goto Label_002D;
            }
            this.GetUnitAnimator.SetInteger("rariry", 0);
            FlowNode_GameObject.ActivateOutputLinks(this, 0xfc);
        Label_002D:
            return;
        }

        private void ResetGetUnitAnim()
        {
            if ((this.GetUnitImage != null) == null)
            {
                goto Label_0026;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitImage, string.Empty);
        Label_0026:
            if ((this.GetUnitBlurImage != null) == null)
            {
                goto Label_004C;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitBlurImage, string.Empty);
        Label_004C:
            if ((this.GetUnitDescriptionText != null) == null)
            {
                goto Label_006D;
            }
            this.GetUnitDescriptionText.set_text(string.Empty);
        Label_006D:
            return;
        }

        private unsafe bool ResetThumbnailList()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            SerializeValueBehaviour behaviour;
            GameObject obj3;
            ConceptCardIcon icon;
            if (this.m_TempList != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            enumerator = this.m_TempList.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_00D8;
            Label_001E:
                obj2 = &enumerator.Current;
                if ((obj2 == null) == null)
                {
                    goto Label_0037;
                }
                goto Label_00D8;
            Label_0037:
                DataSource.Bind<ItemData>(obj2, null);
                DataSource.Bind<UnitData>(obj2, null);
                DataSource.Bind<ArtifactData>(obj2, null);
                behaviour = obj2.GetComponent<SerializeValueBehaviour>();
                if ((behaviour != null) == null)
                {
                    goto Label_00BF;
                }
                this.SetThumbnailValid(behaviour, "item", 0);
                this.SetThumbnailValid(behaviour, "unit", 0);
                this.SetThumbnailValid(behaviour, "artifact", 0);
                obj3 = this.SetThumbnailValid(behaviour, "concept_card", 0);
                if ((obj3 != null) == null)
                {
                    goto Label_00BF;
                }
                icon = obj3.GetComponent<ConceptCardIcon>();
                if ((icon != null) == null)
                {
                    goto Label_00BF;
                }
                icon.ResetIcon();
            Label_00BF:
                obj2.SetActive(0);
                obj2.get_transform().SetParent(this.ThumbnailPool, 0);
            Label_00D8:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_00F5;
            }
            finally
            {
            Label_00E9:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00F5:
            return 1;
        }

        private void SetGetUnitParameter(UnitParam _unit_param, string _ccard_name)
        {
            object[] objArray1;
            string str;
            if (_unit_param != null)
            {
                goto Label_0011;
            }
            DebugUtility.LogError("[SetGetUnitParameter]UnitParamがありません.");
            return;
        Label_0011:
            str = AssetPath.UnitImage(_unit_param, _unit_param.GetJobId(0));
            if ((this.GetUnitImage != null) == null)
            {
                goto Label_0041;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitImage, str);
        Label_0041:
            if ((this.GetUnitBlurImage != null) == null)
            {
                goto Label_0063;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitBlurImage, str);
        Label_0063:
            if ((this.GetUnitDescriptionText != null) == null)
            {
                goto Label_009C;
            }
            objArray1 = new object[] { _ccard_name, _unit_param.name };
            this.GetUnitDescriptionText.set_text(LocalizedText.Get("sys.CONCEPT_CARD_UNIT_GET_DESCRIPTION", objArray1));
        Label_009C:
            return;
        }

        private GameObject SetThumbnailValid(SerializeValueBehaviour _valuelist, string _name, bool _active)
        {
            GameObject obj2;
            if ((_valuelist != null) == null)
            {
                goto Label_002E;
            }
            obj2 = _valuelist.list.GetGameObject(_name);
            if ((obj2 != null) == null)
            {
                goto Label_002E;
            }
            obj2.SetActive(_active);
            return obj2;
        Label_002E:
            return null;
        }

        private void SetupGetCardUnitAnim()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 250);
            return;
        }

        private bool SetupThumbnail(GameObject _thumbnail, GachaDropData _data)
        {
            SerializeValueBehaviour behaviour;
            GameObject obj2;
            ItemData data;
            UnitData data2;
            ArtifactData data3;
            ConceptCardData data4;
            ConceptCardIcon icon;
            SerializeValueBehaviour behaviour2;
            GameObject obj3;
            if ((_thumbnail != null) == null)
            {
                goto Label_018E;
            }
            if (_data == null)
            {
                goto Label_018E;
            }
            behaviour = _thumbnail.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_018E;
            }
            obj2 = null;
            if (_data.type != 1)
            {
                goto Label_0074;
            }
            obj2 = this.SetThumbnailValid(behaviour, "item", 1);
            data = new ItemData();
            data.Setup(0L, _data.item, _data.num);
            data.IsNew = _data.isNew;
            DataSource.Bind<ItemData>(_thumbnail, data);
            goto Label_013E;
        Label_0074:
            if (_data.type != 2)
            {
                goto Label_00A7;
            }
            obj2 = this.SetThumbnailValid(behaviour, "unit", 1);
            data2 = this.CreateUnitData(_data.unit);
            DataSource.Bind<UnitData>(_thumbnail, data2);
            goto Label_013E;
        Label_00A7:
            if (_data.type != 3)
            {
                goto Label_00E1;
            }
            obj2 = this.SetThumbnailValid(behaviour, "artifact", 1);
            data3 = CreateTempArtifactData(_data.artifact, _data.Rare);
            DataSource.Bind<ArtifactData>(_thumbnail, data3);
            goto Label_013E;
        Label_00E1:
            if (_data.type != 4)
            {
                goto Label_013E;
            }
            obj2 = this.SetThumbnailValid(behaviour, "concept_card", 1);
            if ((obj2 != null) == null)
            {
                goto Label_013E;
            }
            data4 = ConceptCardData.CreateConceptCardDataForDisplay(_data.conceptcard.iname);
            if (data4 == null)
            {
                goto Label_013E;
            }
            icon = obj2.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_013E;
            }
            icon.Setup(data4);
        Label_013E:
            if ((obj2 != null) == null)
            {
                goto Label_018C;
            }
            behaviour2 = obj2.GetComponent<SerializeValueBehaviour>();
            if ((behaviour2 != null) == null)
            {
                goto Label_018C;
            }
            obj3 = behaviour2.list.GetGameObject("new");
            if ((obj3 != null) == null)
            {
                goto Label_018C;
            }
            obj3.SetActive(_data.isNew);
        Label_018C:
            return 1;
        Label_018E:
            return 0;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator6E iteratore;
            iteratore = new <Start>c__Iterator6E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void StartGetCardUnitAnim()
        {
            if ((this.GetUnitAnimator == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("GetUnitAnimatorが指定されていません.");
            return;
        Label_001C:
            this.GetUnitAnimator.SetInteger("rariry", this.DropCurrent.CardUnitRarity + 1);
            FlowNode_GameObject.ActivateOutputLinks(this, 0xfb);
            return;
        }

        private void Update()
        {
            Animator animator;
            if (this.mState == null)
            {
                goto Label_0044;
            }
            if (this.AllAnimSkip == null)
            {
                goto Label_0039;
            }
            base.get_gameObject().GetComponent<Animator>().SetTrigger("all_anime_skip");
            this.mState.GotoState<State_EndSetting>();
            return;
        Label_0039:
            this.mState.Update();
        Label_0044:
            return;
        }

        public int GachaSequence
        {
            get
            {
                return (int) GachaResultData.drops.Length;
            }
        }

        private Canvas OverlayCanvas
        {
            get
            {
                return (((mTouchController != null) == null) ? null : mTouchController.GetComponent<Canvas>());
            }
        }

        private DropInfo DropCurrent
        {
            [CompilerGenerated]
            get
            {
                return this.<DropCurrent>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<DropCurrent>k__BackingField = value;
                return;
            }
        }

        public int Rarity
        {
            get
            {
                return (this.DropCurrent.Rarity + 1);
            }
        }

        public bool Shard
        {
            get
            {
                return this.DropCurrent.IsShard;
            }
        }

        public bool Item
        {
            get
            {
                return this.DropCurrent.IsItem;
            }
        }

        public bool IsConceptCard
        {
            get
            {
                return this.DropCurrent.IsConceptCard;
            }
        }

        private int mFirstSeq
        {
            get
            {
                return GachaResultData.excites[0];
            }
        }

        private int mSecondSeq
        {
            get
            {
                return GachaResultData.excites[1];
            }
        }

        private int mThirdSeq
        {
            get
            {
                return GachaResultData.excites[2];
            }
        }

        private int mFourthSeq
        {
            get
            {
                return GachaResultData.excites[3];
            }
        }

        private int mFifthSeq
        {
            get
            {
                return GachaResultData.excites[4];
            }
        }

        public bool IsLithograph
        {
            get
            {
                return this.mLithograph;
            }
            set
            {
                this.mLithograph = value;
                return;
            }
        }

        public int DropIndex
        {
            get
            {
                return (this.thumbnail_count + (this.mCurrentPage * this.mViewStoneCount));
            }
        }

        public bool FinishedCardUnitAnimation
        {
            [CompilerGenerated]
            get
            {
                return this.<FinishedCardUnitAnimation>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<FinishedCardUnitAnimation>k__BackingField = value;
                return;
            }
        }

        public bool NextGachaPhase
        {
            [CompilerGenerated]
            get
            {
                return this.<NextGachaPhase>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<NextGachaPhase>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateDropInfo>c__Iterator6D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaController <>f__this;

            public <CreateDropInfo>c__Iterator6D()
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
                        goto Label_0039;
                }
                goto Label_0066;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0068;
            Label_0039:
                if (this.<>f__this.IsAssetDownloadDone() == null)
                {
                    goto Label_0026;
                }
                this.<>f__this.DropCurrent = GachaController.DropInfo.Create(this.<>f__this);
                this.$PC = -1;
            Label_0066:
                return 0;
            Label_0068:
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
        private sealed class <CreateThumbnailObject>c__Iterator70 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GachaDropData.Type type;
            internal GameObject <thumbnail>__0;
            internal int $PC;
            internal object $current;
            internal GachaDropData.Type <$>type;
            internal GachaController <>f__this;

            public <CreateThumbnailObject>c__Iterator70()
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
                bool flag;
                this.$PC = -1;
                if (this.$PC != null)
                {
                    goto Label_0185;
                }
                if (this.type != null)
                {
                    goto Label_002C;
                }
                DebugUtility.LogError("排出物のタイプが指定されていません");
                goto Label_0185;
            Label_002C:
                this.<thumbnail>__0 = null;
                if (this.type != 2)
                {
                    goto Label_009C;
                }
                this.<thumbnail>__0 = Object.Instantiate<GameObject>(this.<>f__this.UnitThumbnailPrefab);
                this.<thumbnail>__0.set_name(this.<>f__this.UnitThumbnailPrefab.get_name());
                if ((this.<thumbnail>__0 != null) == null)
                {
                    goto Label_0169;
                }
                this.<>f__this.mUnitTempList.Add(this.<thumbnail>__0);
                goto Label_0169;
            Label_009C:
                if (this.type != 1)
                {
                    goto Label_0105;
                }
                this.<thumbnail>__0 = Object.Instantiate<GameObject>(this.<>f__this.ItemThumbnailPrefab);
                this.<thumbnail>__0.set_name(this.<>f__this.ItemThumbnailPrefab.get_name());
                if ((this.<thumbnail>__0 != null) == null)
                {
                    goto Label_0169;
                }
                this.<>f__this.mItemTempList.Add(this.<thumbnail>__0);
                goto Label_0169;
            Label_0105:
                if (this.type != 3)
                {
                    goto Label_0169;
                }
                this.<thumbnail>__0 = Object.Instantiate<GameObject>(this.<>f__this.ArtifactThumbnailPrefab);
                this.<thumbnail>__0.set_name(this.<>f__this.ArtifactThumbnailPrefab.get_name());
                if ((this.<thumbnail>__0 != null) == null)
                {
                    goto Label_0169;
                }
                this.<>f__this.mArtifactTempList.Add(this.<thumbnail>__0);
            Label_0169:
                this.<thumbnail>__0.get_transform().SetParent(this.<>f__this.ThumbnailPool, 0);
            Label_0185:
                return 0;
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
        private sealed class <InitTempList>c__Iterator6F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal GachaController <>f__this;

            public <InitTempList>c__Iterator6F()
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
                        goto Label_0029;

                    case 1:
                        goto Label_005E;

                    case 2:
                        goto Label_0087;

                    case 3:
                        goto Label_00B0;
                }
                goto Label_00D5;
            Label_0029:
                this.<i>__0 = 0;
                goto Label_00BE;
            Label_0035:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateThumbnailObject(2));
                this.$PC = 1;
                goto Label_00D7;
            Label_005E:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateThumbnailObject(1));
                this.$PC = 2;
                goto Label_00D7;
            Label_0087:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateThumbnailObject(3));
                this.$PC = 3;
                goto Label_00D7;
            Label_00B0:
                this.<i>__0 += 1;
            Label_00BE:
                if (this.<i>__0 < GachaController.MAX_VIEW_STONE)
                {
                    goto Label_0035;
                }
                this.$PC = -1;
            Label_00D5:
                return 0;
            Label_00D7:
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
        private sealed class <Start>c__Iterator6E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <gobj>__0;
            internal Animator <atr>__1;
            internal int <i>__2;
            internal GachaVoice <gv>__3;
            internal int $PC;
            internal object $current;
            internal GachaController <>f__this;

            public <Start>c__Iterator6E()
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

            public unsafe bool MoveNext()
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
                        goto Label_025E;
                }
                goto Label_06CE;
            Label_0021:
                if ((this.<>f__this.DropStone != null) == null)
                {
                    goto Label_0048;
                }
                this.<>f__this.DropStone.SetActive(0);
            Label_0048:
                if ((this.<>f__this.DropStone_CCard != null) == null)
                {
                    goto Label_006F;
                }
                this.<>f__this.DropStone_CCard.SetActive(0);
            Label_006F:
                if ((this.<>f__this.UnitThumbnailPrefab != null) == null)
                {
                    goto Label_0096;
                }
                this.<>f__this.UnitThumbnailPrefab.SetActive(0);
            Label_0096:
                if ((this.<>f__this.ItemThumbnailPrefab != null) == null)
                {
                    goto Label_00BD;
                }
                this.<>f__this.ItemThumbnailPrefab.SetActive(0);
            Label_00BD:
                if ((this.<>f__this.ArtifactThumbnailPrefab != null) == null)
                {
                    goto Label_00E4;
                }
                this.<>f__this.ArtifactThumbnailPrefab.SetActive(0);
            Label_00E4:
                if ((this.<>f__this.ThumbnailTemlate != null) == null)
                {
                    goto Label_010B;
                }
                this.<>f__this.ThumbnailTemlate.SetActive(0);
            Label_010B:
                if (this.<>f__this.GachaSequence > 0)
                {
                    goto Label_012B;
                }
                DebugUtility.LogError("排出結果が存在しません");
                goto Label_06CE;
            Label_012B:
                this.<>f__this.mViewStoneCount = (this.<>f__this.GachaSequence <= GachaController.MAX_VIEW_STONE) ? this.<>f__this.GachaSequence : GachaController.MAX_VIEW_STONE;
                this.<>f__this.mCurrentPage = 0;
                this.<>f__this.mMaxPage = Mathf.CeilToInt(((float) this.<>f__this.GachaSequence) / ((float) GachaController.MAX_VIEW_STONE));
                this.<>f__this.IsOverMaxView = this.<>f__this.mMaxPage > 1;
                if (this.<>f__this.IsOverMaxView == null)
                {
                    goto Label_01C9;
                }
                this.<>f__this.mFlowType = 2;
                goto Label_01F0;
            Label_01C9:
                if (GachaResultData.IsCoin == null)
                {
                    goto Label_01E4;
                }
                this.<>f__this.mFlowType = 0;
                goto Label_01F0;
            Label_01E4:
                this.<>f__this.mFlowType = 1;
            Label_01F0:
                this.<>f__this.mBGMVolume = new MySound.VolumeHandle(0);
                this.<>f__this.mBGMVolume.SetVolume(0f, 1f);
                if ((HomeWindow.Current != null) == null)
                {
                    goto Label_0236;
                }
                HomeWindow.Current.SetVisible(0);
            Label_0236:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.CreateDropInfo());
                this.$PC = 1;
                goto Label_06D0;
            Label_025E:
                if ((this.<>f__this.DropMaterial == null) == null)
                {
                    goto Label_0283;
                }
                DebugUtility.LogError("演出用オブジェクトが存在しません");
                goto Label_06CE;
            Label_0283:
                this.<>f__this.mDropMaterial = this.<>f__this.DropMaterial;
                this.<>f__this.mDropMaterial.SetActive(0);
                this.<gobj>__0 = this.<>f__this.get_gameObject();
                this.<atr>__1 = this.<gobj>__0.GetComponent<Animator>();
                this.<atr>__1.SetInteger("seqF_root", this.<>f__this.mFirstSeq);
                this.<atr>__1.SetInteger("seqS_root", this.<>f__this.mSecondSeq);
                this.<atr>__1.SetInteger("seqT_root", this.<>f__this.mThirdSeq);
                this.<atr>__1.SetInteger("seqFo_root", this.<>f__this.mFourthSeq);
                this.<atr>__1.SetInteger("seqFi_root", this.<>f__this.mFifthSeq);
                if (this.<>f__this.mFlowType != 1)
                {
                    goto Label_03AD;
                }
                this.<>f__this.StartStone.GetComponent<Image>().set_sprite(this.<>f__this.StartStoneSprite[0]);
                this.<>f__this.StartStoneMask.GetComponent<Image>().set_sprite(this.<>f__this.StartStoneSprite[0]);
                goto Label_0646;
            Label_03AD:
                this.<>f__this.StartArrowB.GetComponent<Image>().set_sprite(this.<>f__this.StartArrowSprite[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]);
                this.<>f__this.StartArrowTop.GetComponent<Image>().set_sprite(this.<>f__this.StartArrowTopSprite[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]);
                this.<i>__2 = 0;
                goto Label_049D;
            Label_0443:
                this.<>f__this.StartArrowTopMasks[this.<i>__2].GetComponent<Image>().set_sprite(this.<>f__this.StartArrowTopSprite[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]);
                this.<i>__2 += 1;
            Label_049D:
                if (this.<i>__2 < ((int) this.<>f__this.StartArrowTopMasks.Length))
                {
                    goto Label_0443;
                }
                this.<>f__this.StartStone.GetComponent<Image>().set_sprite(this.<>f__this.StartStoneSprite[(this.<>f__this.mFirstSeq <= 0) ? 0 : this.<>f__this.mFirstSeq]);
                this.<>f__this.StartStoneMask.GetComponent<Image>().set_sprite(this.<>f__this.StartStoneSprite[(this.<>f__this.mFirstSeq <= 0) ? 0 : this.<>f__this.mFirstSeq]);
                this.<>f__this.StartStoneEff01.GetComponent<ParticleSystem>().set_startColor(this.<>f__this.ConvertColor(*(&(this.<>f__this.FlickEffectColor01[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]))));
                this.<>f__this.StartStoneEff02.GetComponent<ParticleSystem>().set_startColor(this.<>f__this.ConvertColor(*(&(this.<>f__this.FlickEffectColor02[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]))));
                this.<>f__this.StartStoneEff03.GetComponent<ParticleSystem>().set_startColor(this.<>f__this.ConvertColor(*(&(this.<>f__this.FlickEffectColor03[(this.<>f__this.mFirstSeq <= 0) ? 0 : (this.<>f__this.mFirstSeq - 1)]))));
            Label_0646:
                this.<gv>__3 = this.<>f__this.GetComponent<GachaVoice>();
                if ((this.<gv>__3 != null) == null)
                {
                    goto Label_067E;
                }
                this.<gv>__3.Excites = this.<>f__this.mFifthSeq;
            Label_067E:
                this.<>f__this.CreateTouchArea();
                this.<>f__this.InitThumbnailTemplateList();
                this.<>f__this.ResetThumbnailList();
                this.<>f__this.mState = new StateMachine<GachaController>(this.<>f__this);
                this.<>f__this.mState.GotoState<GachaController.State_Init>();
                this.$PC = -1;
            Label_06CE:
                return 0;
            Label_06D0:
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

        private class DropInfo
        {
            [CompilerGenerated]
            private int <Index>k__BackingField;
            [CompilerGenerated]
            private string <Name>k__BackingField;
            [CompilerGenerated]
            private int <Rarity>k__BackingField;
            [CompilerGenerated]
            private bool <IsShard>k__BackingField;
            [CompilerGenerated]
            private bool <IsItem>k__BackingField;
            [CompilerGenerated]
            private string <OName>k__BackingField;
            [CompilerGenerated]
            private int <Num>k__BackingField;
            [CompilerGenerated]
            private int[] <Excites>k__BackingField;
            [CompilerGenerated]
            private bool <IsConceptCard>k__BackingField;
            [CompilerGenerated]
            private bool <IsCardUnit>k__BackingField;
            [CompilerGenerated]
            private int <CardUnitRarity>k__BackingField;

            public DropInfo()
            {
                base..ctor();
                return;
            }

            public static GachaController.DropInfo Create(GachaController self)
            {
                GachaController.DropInfo info;
                info = new GachaController.DropInfo();
                info.Reflesh(self, 0);
                return info;
            }

            public void Next(GachaController self)
            {
                int num;
                this.Index = num = this.Index + 1;
                this.Reflesh(self, num);
                return;
            }

            public unsafe void Reflesh(GachaController self, int index)
            {
                GachaDropData data;
                GameManager manager;
                string str;
                UnitParam param;
                GameObject obj2;
                UnitData data2;
                int num;
                UnitParam param2;
                GameObject obj3;
                UnitData data3;
                int num2;
                GameSettings settings;
                string str2;
                int num3;
                int num4;
                int num5;
                int num6;
                if (index < self.GachaSequence)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                data = GachaResultData.drops[this.Index];
                manager = MonoSingleton<GameManager>.Instance;
                self.IsLithograph = 1;
                this.IsItem = 0;
                this.IsShard = 0;
                this.IsConceptCard = 0;
                this.IsCardUnit = 0;
                this.CardUnitRarity = 0;
                if ((self.ConceptCardNameText != null) == null)
                {
                    goto Label_006B;
                }
                self.ConceptCardNameText.set_text(string.Empty);
            Label_006B:
                if ((self.ConceptCardFrame != null) == null)
                {
                    goto Label_008D;
                }
                self.ConceptCardFrame.get_gameObject().SetActive(0);
            Label_008D:
                self.ResetGetUnitAnim();
                if (data.type != 2)
                {
                    goto Label_016F;
                }
                this.Name = data.unit.name;
                this.Rarity = data.Rare;
                this.OName = string.Empty;
                manager.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(data.unit, data.unit.GetJobId(0)));
                manager.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(data.unit, data.unit.GetJobId(0)));
                manager.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.UnitIconSmall(data.unit, data.unit.GetJobId(0)));
                self.RefreshGachaImageSize(self.DropMaterialImage.get_rectTransform(), data.type);
                self.RefreshGachaImageSize(self.DropMaterialBlurImage.get_rectTransform(), data.type);
                this.Excites = data.excites;
                goto Label_07BA;
            Label_016F:
                if (data.type != 1)
                {
                    goto Label_0518;
                }
                this.Name = data.item.name;
                this.Rarity = data.Rare;
                this.OName = string.Empty;
                this.Num = data.num;
                this.Excites = data.excites;
                self.MaterialCount.set_text(&this.Num.ToString());
                str = this.Name + "x" + &this.Num.ToString();
                self.MaterialName.set_text(str);
                self.MaterialComment.set_text((string.IsNullOrEmpty(data.item.Expr) == null) ? data.item.Expr : data.item.Flavor);
                self.IsLithograph = data.item.type == 1;
                if (data.unitOrigin == null)
                {
                    goto Label_03D7;
                }
                this.IsShard = 1;
                this.IsItem = 0;
                param = manager.MasterParam.GetUnitParamForPiece(data.item.iname, 1);
                this.OName = (param == null) ? string.Empty : param.iname;
                manager.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(data.unitOrigin, data.unitOrigin.GetJobId(0)));
                manager.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(data.unitOrigin, data.unitOrigin.GetJobId(0)));
                manager.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(data.item));
                self.RefreshGachaImageSize(self.DropMaterialImage.get_rectTransform(), data.type);
                self.RefreshGachaImageSize(self.DropMaterialBlurImage.get_rectTransform(), data.type);
                self.DropMaterialIconFrameImage.set_sprite(GameSettings.Instance.GetItemFrame(data.item));
                if (param == null)
                {
                    goto Label_07BA;
                }
                obj2 = self.GaugeObject.get_transform().FindChild("UnitShard_gauge").get_gameObject();
                data2 = manager.Player.FindUnitDataByUnitID(param.iname);
                if (data2 == null)
                {
                    goto Label_03B7;
                }
                num = data2.AwakeLv;
                if (num >= data2.GetAwakeLevelCap())
                {
                    goto Label_07BA;
                }
                obj2.GetComponent<GachaUnitShard>().Refresh(param, this.Name, num, data.num, index);
                goto Label_03D2;
            Label_03B7:
                obj2.GetComponent<GachaUnitShard>().Refresh(param, this.Name, 0, data.num, index);
            Label_03D2:
                goto Label_0513;
            Label_03D7:
                this.IsShard = 0;
                this.IsItem = 1;
                self.DropMaterialImage.set_texture(null);
                self.DropMaterialBlurImage.set_texture(null);
                manager.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(data.item));
                self.DropMaterialIconFrameImage.set_sprite(GameSettings.Instance.GetItemFrame(data.item));
                if (data.item.type != 1)
                {
                    goto Label_07BA;
                }
                param2 = manager.MasterParam.GetUnitParamForPiece(data.item.iname, 1);
                this.OName = (param2 == null) ? string.Empty : param2.iname;
                if (string.IsNullOrEmpty(this.OName) != null)
                {
                    goto Label_07BA;
                }
                obj3 = self.GaugeObject.get_transform().FindChild("UnitShard_gauge").get_gameObject();
                data3 = manager.Player.FindUnitDataByUnitID(param2.iname);
                if (data3 == null)
                {
                    goto Label_04F7;
                }
                num2 = data3.AwakeLv;
                if (num2 >= data3.GetAwakeLevelCap())
                {
                    goto Label_07BA;
                }
                obj3.GetComponent<GachaUnitShard>().Refresh(param2, this.Name, num2, data.num, index);
                goto Label_0513;
            Label_04F7:
                obj3.GetComponent<GachaUnitShard>().Refresh(param2, this.Name, 0, data.num, index);
            Label_0513:
                goto Label_07BA;
            Label_0518:
                if (data.type != 3)
                {
                    goto Label_0671;
                }
                settings = GameSettings.Instance;
                this.Name = data.artifact.name;
                this.Rarity = data.Rare;
                this.OName = string.Empty;
                this.Num = data.num;
                this.Excites = data.excites;
                self.MaterialCount.set_text(&this.Num.ToString());
                str2 = this.Name + "x" + &this.Num.ToString();
                self.MaterialName.set_text(str2);
                self.MaterialComment.set_text((string.IsNullOrEmpty(data.artifact.Expr) == null) ? data.artifact.Expr : data.artifact.Flavor);
                self.IsLithograph = 0;
                this.IsShard = 0;
                this.IsItem = 1;
                self.DropMaterialImage.set_texture(null);
                self.DropMaterialBlurImage.set_texture(null);
                manager.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ArtifactIcon(data.artifact));
                if ((settings != null) == null)
                {
                    goto Label_07BA;
                }
                if (this.Rarity >= ((int) settings.ArtifactIcon_Frames.Length))
                {
                    goto Label_07BA;
                }
                self.DropMaterialIconFrameImage.set_sprite(settings.ArtifactIcon_Frames[this.Rarity]);
                goto Label_07BA;
            Label_0671:
                if (data.type != 4)
                {
                    goto Label_07BA;
                }
                this.Name = data.conceptcard.name;
                this.Rarity = data.Rare;
                this.OName = string.Empty;
                manager.ApplyTextureAsync(self.DropMaterialImage, AssetPath.ConceptCard(data.conceptcard));
                manager.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.ConceptCard(data.conceptcard));
                manager.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ConceptCardIcon(data.conceptcard));
                self.RefreshGachaImageSize(self.DropMaterialImage.get_rectTransform(), data.type);
                self.RefreshGachaImageSize(self.DropMaterialBlurImage.get_rectTransform(), data.type);
                this.Excites = data.excites;
                this.IsConceptCard = 1;
                if ((self.ConceptCardNameText != null) == null)
                {
                    goto Label_074D;
                }
                self.ConceptCardNameText.set_text(this.Name);
            Label_074D:
                if ((self.ConceptCardFrame != null) == null)
                {
                    goto Label_0780;
                }
                self.ConceptCardFrame.get_gameObject().SetActive(1);
                self.ConceptCardFrame.ImageIndex = this.Rarity;
            Label_0780:
                if (data.cardunit == null)
                {
                    goto Label_07BA;
                }
                self.SetGetUnitParameter(data.cardunit, data.conceptcard.name);
                this.IsCardUnit = 1;
                this.CardUnitRarity = data.cardunit.rare;
            Label_07BA:
                return;
            }

            public int Index
            {
                [CompilerGenerated]
                get
                {
                    return this.<Index>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<Index>k__BackingField = value;
                    return;
                }
            }

            public string Name
            {
                [CompilerGenerated]
                get
                {
                    return this.<Name>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<Name>k__BackingField = value;
                    return;
                }
            }

            public int Rarity
            {
                [CompilerGenerated]
                get
                {
                    return this.<Rarity>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<Rarity>k__BackingField = value;
                    return;
                }
            }

            public bool IsShard
            {
                [CompilerGenerated]
                get
                {
                    return this.<IsShard>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<IsShard>k__BackingField = value;
                    return;
                }
            }

            public bool IsItem
            {
                [CompilerGenerated]
                get
                {
                    return this.<IsItem>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<IsItem>k__BackingField = value;
                    return;
                }
            }

            public string OName
            {
                [CompilerGenerated]
                get
                {
                    return this.<OName>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<OName>k__BackingField = value;
                    return;
                }
            }

            public int Num
            {
                [CompilerGenerated]
                get
                {
                    return this.<Num>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<Num>k__BackingField = value;
                    return;
                }
            }

            public int[] Excites
            {
                [CompilerGenerated]
                get
                {
                    return this.<Excites>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<Excites>k__BackingField = value;
                    return;
                }
            }

            public bool IsConceptCard
            {
                [CompilerGenerated]
                get
                {
                    return this.<IsConceptCard>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<IsConceptCard>k__BackingField = value;
                    return;
                }
            }

            public bool IsCardUnit
            {
                [CompilerGenerated]
                get
                {
                    return this.<IsCardUnit>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<IsCardUnit>k__BackingField = value;
                    return;
                }
            }

            public int CardUnitRarity
            {
                [CompilerGenerated]
                get
                {
                    return this.<CardUnitRarity>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<CardUnitRarity>k__BackingField = value;
                    return;
                }
            }
        }

        private enum GachaFlowType : byte
        {
            Rare = 0,
            Normal = 1,
            Special = 2
        }

        private class State_ActionRevolver : State<GachaController>
        {
            private Vector3 mNewAngle;
            private float mMoveSpeed;
            private float mTheta;

            public State_ActionRevolver()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(GachaController self)
            {
                Transform transform;
                Vector3 vector;
                Vector3 vector2;
                Vector3 vector3;
                this.mTheta = 360f / ((float) self.GachaSequence);
                transform = self.DropStone.get_transform().get_parent();
                this.mNewAngle = new Vector3(&transform.get_localEulerAngles().x, &transform.get_localEulerAngles().y + this.mTheta, &transform.get_localEulerAngles().z);
                this.mMoveSpeed = this.mTheta / self.StoneRotateTime;
                return;
            }

            public override unsafe void Update(GachaController self)
            {
                Transform transform;
                Vector3 vector;
                transform = self.DropStone.get_transform().get_parent();
                if (self.CheckSkip() == null)
                {
                    goto Label_003B;
                }
                self.isSkipping = 1;
                transform.set_localEulerAngles(this.mNewAngle);
                self.mState.GotoState<GachaController.State_CheckDropStone>();
                return;
            Label_003B:
                if (1f >= Mathf.DeltaAngle(&transform.get_localEulerAngles().y, &this.mNewAngle.y))
                {
                    goto Label_0084;
                }
                transform.Rotate((this.mMoveSpeed * Time.get_deltaTime()) * Vector3.get_up());
                goto Label_008F;
            Label_0084:
                self.mState.GotoState<GachaController.State_CheckDropStone>();
            Label_008F:
                return;
            }
        }

        private class State_CheckCardUnit : State<GachaController>
        {
            public State_CheckCardUnit()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                if (self.DropCurrent.IsConceptCard == null)
                {
                    goto Label_0030;
                }
                if (self.DropCurrent.IsCardUnit == null)
                {
                    goto Label_0030;
                }
                self.mState.GotoState<GachaController.State_CloseAnimDropMaterial>();
                goto Label_003B;
            Label_0030:
                self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
            Label_003B:
                return;
            }
        }

        private class State_CheckDropStone : State<GachaController>
        {
            public State_CheckDropStone()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                if (self.mDropStones.Count <= 0)
                {
                    goto Label_0021;
                }
                self.mState.GotoState<GachaController.State_MoveDropStone>();
                goto Label_002C;
            Label_0021:
                self.mState.GotoState<GachaController.State_EndSetting>();
            Label_002C:
                return;
            }
        }

        private class State_CheckNextDropSet : State<GachaController>
        {
            public State_CheckNextDropSet()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                if ((self.mCurrentPage + 1) >= self.mMaxPage)
                {
                    goto Label_002D;
                }
                self.mCurrentPage += 1;
                self.mState.GotoState<GachaController.State_WaitForInput_NextDropSet>();
                return;
            Label_002D:
                self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            }
        }

        private class State_CheckThumbnail : State<GachaController>
        {
            public State_CheckThumbnail()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GachaDropData data;
                UnitParam param;
                UnitData data2;
                if (self.OnShardEffect == null)
                {
                    goto Label_010F;
                }
                if (self.DropCurrent.IsItem != null)
                {
                    goto Label_002B;
                }
                if (self.DropCurrent.IsShard == null)
                {
                    goto Label_010F;
                }
            Label_002B:
                data = GachaResultData.drops[self.DropCurrent.Index];
                if (data == null)
                {
                    goto Label_010F;
                }
                if (data.item == null)
                {
                    goto Label_010F;
                }
                if (data.item.type != 1)
                {
                    goto Label_010F;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(data.item.iname, 1);
                data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(param.iname);
                if (data2 == null)
                {
                    goto Label_0103;
                }
                if (data2.GetAwakeLevelCap() > data2.AwakeLv)
                {
                    goto Label_0103;
                }
                self.DropCurrent.Next(self);
                self.thumbnail_count += 1;
                if (self.thumbnail_count >= self.mViewStoneCount)
                {
                    goto Label_00DF;
                }
                self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
            Label_00DF:
                if (self.mFlowType != 2)
                {
                    goto Label_00F7;
                }
                self.mState.GotoState<GachaController.State_CheckNextDropSet>();
                return;
            Label_00F7:
                self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            Label_0103:
                self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
                return;
            Label_010F:
                self.DropCurrent.Next(self);
                self.thumbnail_count += 1;
                if (self.thumbnail_count >= self.mViewStoneCount)
                {
                    goto Label_0146;
                }
                self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
            Label_0146:
                if (self.mFlowType != 2)
                {
                    goto Label_015E;
                }
                self.mState.GotoState<GachaController.State_CheckNextDropSet>();
                return;
            Label_015E:
                self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            }
        }

        private class State_CloseAnimDropMaterial : State<GachaController>
        {
            private Animator m_OpenMaterial;

            public State_CloseAnimDropMaterial()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                Animator animator;
                animator = self.OpenMaterial.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_0037;
                }
                animator.SetBool("cardunit", 1);
                animator.SetInteger("rariry", 0);
                this.m_OpenMaterial = animator;
            Label_0037:
                return;
            }

            public override void Update(GachaController self)
            {
                if ((this.m_OpenMaterial != null) == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.IsAnimatorRunning(this.m_OpenMaterial) != null)
                {
                    goto Label_0032;
                }
                self.SetupGetCardUnitAnim();
                self.mState.GotoState<GachaController.State_WaitCardAnim>();
            Label_0032:
                return;
            }
        }

        private class State_DestroyDropStone : State<GachaController>
        {
            public State_DestroyDropStone()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                if (self.mDropStones.Count > 0)
                {
                    goto Label_001D;
                }
                self.mState.GotoState<GachaController.State_CheckDropStone>();
                return;
            Label_001D:
                obj2 = self.mDropStones[0];
                obj2.SetActive(0);
                self.mDropStones.RemoveAt(0);
                self.mState.GotoState<GachaController.State_EnableDropMaterial>();
                return;
            }
        }

        private class State_DisableDropMaterial : State<GachaController>
        {
            public State_DisableDropMaterial()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                Animator animator;
                self.mDropMaterial.SetActive(0);
                obj2 = self.OpenMaterial;
                if ((obj2 != null) == null)
                {
                    goto Label_006E;
                }
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_006E;
                }
                animator.SetInteger("rariry", 0);
                animator.SetBool("shard", 0);
                animator.SetBool("item", 0);
                animator.SetBool("reset", 0);
                animator.SetBool("ccard", 0);
            Label_006E:
                self.DropCurrent.Next(self);
                self.mState.GotoState<GachaController.State_ActionRevolver>();
                return;
            }
        }

        private class State_DisableDropMaterialShard : State<GachaController>
        {
            public State_DisableDropMaterialShard()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                Animator animator;
                obj2 = self.OpenMaterial;
                if ((obj2 != null) == null)
                {
                    goto Label_0051;
                }
                obj2.SetActive(0);
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_0051;
                }
                animator.SetInteger("rariry", 0);
                animator.SetBool("shard", 0);
                animator.SetBool("item", 0);
            Label_0051:
                obj2.SetActive(0);
                self.DropCurrent.Next(self);
                self.thumbnail_count += 1;
                if (self.thumbnail_count >= self.mViewStoneCount)
                {
                    goto Label_008F;
                }
                self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
            Label_008F:
                self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            }
        }

        private class State_DisableDropMaterialT : State<GachaController>
        {
            public State_DisableDropMaterialT()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                int num;
                GameObject obj2;
                Animator animator;
                num = 0;
                goto Label_002C;
            Label_0007:
                if ((self.ResetMaterials[num] != null) == null)
                {
                    goto Label_0028;
                }
                self.ResetMaterials[num].SetActive(0);
            Label_0028:
                num += 1;
            Label_002C:
                if (num < ((int) self.ResetMaterials.Length))
                {
                    goto Label_0007;
                }
                obj2 = self.OpenMaterial;
                if ((obj2 != null) == null)
                {
                    goto Label_0097;
                }
                obj2.SetActive(0);
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_0097;
                }
                animator.SetInteger("rariry", 0);
                animator.SetBool("shard", 0);
                animator.SetBool("item", 0);
                animator.SetBool("reset", 0);
            Label_0097:
                self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
                return;
            }
        }

        private class State_EnableDropMaterial : State<GachaController>
        {
            public State_EnableDropMaterial()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                GameObject obj3;
                GameObject obj4;
                Image image;
                Image image2;
                Animator animator;
                self.mDropMaterial.SetActive(1);
                if (self.mFlowType == null)
                {
                    goto Label_0023;
                }
                self.mState.GotoState<GachaController.State_InitDropThumbnail>();
                return;
            Label_0023:
                obj2 = self.OpenMaterial;
                obj2.SetActive(1);
                if ((obj2 != null) == null)
                {
                    goto Label_011F;
                }
                obj3 = obj2.get_transform().FindChild("lithograph_col").get_gameObject();
                obj4 = obj2.get_transform().FindChild("lithograph_eff").get_gameObject();
                if ((obj3 != null) == null)
                {
                    goto Label_00C2;
                }
                if ((obj4 != null) == null)
                {
                    goto Label_00C2;
                }
                image = obj3.GetComponent<Image>();
                image2 = obj4.GetComponent<Image>();
                image.set_sprite(self.LithographBases[self.GetRarityTextureIndex(self.Rarity)]);
                image.set_enabled(self.IsLithograph);
                image2.set_enabled(self.IsLithograph);
            Label_00C2:
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_011F;
                }
                animator.SetInteger("rariry", self.Rarity);
                animator.SetBool("shard", self.Shard);
                animator.SetBool("item", self.Item);
                animator.SetBool("ccard", self.IsConceptCard);
            Label_011F:
                if (self.isSkipping == null)
                {
                    goto Label_0131;
                }
                self.isSkipping = 0;
            Label_0131:
                self.mState.GotoState<GachaController.State_OpenDropMaterial>();
                return;
            }
        }

        private class State_EndSetting : State<GachaController>
        {
            public State_EndSetting()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                FlowNode_Variable.Set("GACHA_TYPE", null);
                FlowNode_Variable.Set("SEQ_FIRST", null);
                FlowNode_Variable.Set("SEQ_SECOND", null);
                FlowNode_Variable.Set("SEQ_THIRD", null);
                FlowNode_Variable.Set("GACHA_INPUT", null);
                FlowNode_Variable.Set("GACHA_ANIMATION_END", null);
                FlowNode_Variable.Set("GACHA_CIRCLE_SET", null);
                if (self.mJingleHandle == null)
                {
                    goto Label_0068;
                }
                self.mJingleHandle.Stop(1f);
            Label_0068:
                self.AllAnimSkip = 0;
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(self, "GACHA_ANIM_FINISH");
                return;
            }
        }

        private class State_Init : State<GachaController>
        {
            public State_Init()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.isSkipping = 0;
                self.StartCoroutine(this.MoveNextState());
                return;
            }

            [DebuggerHidden]
            private IEnumerator MoveNextState()
            {
                <MoveNextState>c__Iterator71 iterator;
                iterator = new <MoveNextState>c__Iterator71();
                iterator.<>f__this = this;
                return iterator;
            }

            [CompilerGenerated]
            private sealed class <MoveNextState>c__Iterator71 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal int $PC;
                internal object $current;
                internal GachaController.State_Init <>f__this;

                public <MoveNextState>c__Iterator71()
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
                            goto Label_0053;
                    }
                    goto Label_006F;
                Label_0021:
                    this.$current = this.<>f__this.self.StartCoroutine(this.<>f__this.self.InitTempList());
                    this.$PC = 1;
                    goto Label_0071;
                Label_0053:
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitInputFlick>();
                    this.$PC = -1;
                Label_006F:
                    return 0;
                Label_0071:
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

        private class State_InitDropThumbnail : State<GachaController>
        {
            public State_InitDropThumbnail()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(GachaController self)
            {
                GameObject obj2;
                int num;
                int num2;
                string str;
                GameObject obj3;
                int num3;
                GachaDropData data;
                GameObject obj4;
                string str2;
                Transform transform;
                SerializeValueBehaviour behaviour;
                int num4;
                obj2 = self.OpenItem;
                if ((obj2 == null) == null)
                {
                    goto Label_0014;
                }
                return;
            Label_0014:
                obj2.SetActive(1);
                num = self.mCurrentPage * self.mViewStoneCount;
                num2 = self.mViewStoneCount;
                str = "item_" + &num2.ToString();
                obj3 = obj2.get_transform().Find(str).get_gameObject();
                if ((obj3 != null) == null)
                {
                    goto Label_0150;
                }
                num3 = 0;
                goto Label_012D;
            Label_006A:
                if (((int) GachaResultData.drops.Length) > (num3 + num))
                {
                    goto Label_007F;
                }
                goto Label_0135;
            Label_007F:
                data = GachaResultData.drops[num3 + num];
                obj4 = self.GetThumbnailObject(num3);
                if ((obj4 == null) == null)
                {
                    goto Label_00A7;
                }
                goto Label_0127;
            Label_00A7:
                num4 = num3 + 1;
                str2 = "item_" + &num4.ToString();
                transform = obj3.get_transform().Find(str2);
                if ((transform != null) == null)
                {
                    goto Label_0127;
                }
                self.SetupThumbnail(obj4, data);
                behaviour = transform.GetComponent<SerializeValueBehaviour>();
                if ((behaviour != null) == null)
                {
                    goto Label_0111;
                }
                behaviour.list.SetField("thumbnail", obj4);
            Label_0111:
                GameParameter.UpdateAll(obj4);
                obj4.get_transform().SetParent(transform, 0);
            Label_0127:
                num3 += 1;
            Label_012D:
                if (num3 < num2)
                {
                    goto Label_006A;
                }
            Label_0135:
                obj3.SetActive(1);
                self.item_root = obj3;
                self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
            Label_0150:
                return;
            }
        }

        private class State_InitNextDropSet : State<GachaController>
        {
            public State_InitNextDropSet()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                FlowNode_GameObject.ActivateOutputLinks(self, 0x66);
                self.ResetThumbnailList();
                self.thumbnail_count = 0;
                self.mState.GotoState<GachaController.State_InitDropThumbnail>();
                return;
            }
        }

        private class State_MoveDropStone : State<GachaController>
        {
            private GameObject mStone;
            private float spd;
            private Vector3 mDestination;

            public State_MoveDropStone()
            {
                this.spd = 80f;
                this.mDestination = Vector3.get_zero();
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                if (self.mDropStones.Count > 0)
                {
                    goto Label_001D;
                }
                self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            Label_001D:
                this.mDestination = new Vector3(0f, self.StoneCenterHeight, 0f);
                this.mStone = self.mDropStones[0];
                if (self.isSkipping == null)
                {
                    goto Label_0077;
                }
                this.mStone.get_transform().set_localPosition(this.mDestination);
                self.mState.GotoState<GachaController.State_OpenDropStone>();
                return;
            Label_0077:
                return;
            }

            public override unsafe void Update(GachaController self)
            {
                Transform transform1;
                Vector3 vector;
                Vector3 vector2;
                if ((this.mStone == null) == null)
                {
                    goto Label_0012;
                }
                return;
            Label_0012:
                if (self.CheckSkip() == null)
                {
                    goto Label_0046;
                }
                self.isSkipping = 1;
                this.mStone.get_transform().set_localPosition(this.mDestination);
                self.mState.GotoState<GachaController.State_OpenDropStone>();
                return;
            Label_0046:
                if (Vector3.Distance(this.mStone.get_transform().get_localPosition(), this.mDestination) >= 0.1f)
                {
                    goto Label_007B;
                }
                self.mState.GotoState<GachaController.State_OpenDropStone>();
                goto Label_00D0;
            Label_007B:
                vector2 = this.mDestination - this.mStone.get_transform().get_localPosition();
                vector = &vector2.get_normalized();
                transform1 = this.mStone.get_transform();
                transform1.set_localPosition(transform1.get_localPosition() + ((vector * this.spd) * Time.get_deltaTime()));
            Label_00D0:
                return;
            }
        }

        private class State_OpenDropMaterial : State<GachaController>
        {
            private GameObject go;
            private string[] ShardAnimList;

            public State_OpenDropMaterial()
            {
                string[] textArray1;
                textArray1 = new string[] { "DropMaterial_rare1_Shard", "DropMaterial_rare2_Shard", "DropMaterial_rare3_Shard", "DropMaterial_rare4_Shard", "DropMaterial_rare5_Shard", "DropMaterial_item1", "DropMaterial_item2", "DropMaterial_item3", "DropMaterial_item4", "DropMaterial_item5" };
                this.ShardAnimList = textArray1;
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                UnitData data;
                if (self.OnShardEffect == null)
                {
                    goto Label_0020;
                }
                if (string.IsNullOrEmpty(self.DropCurrent.OName) == null)
                {
                    goto Label_002C;
                }
            Label_0020:
                self.mState.GotoState<GachaController.State_WaitDropMaterial>();
                return;
            Label_002C:
                data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(self.DropCurrent.OName);
                if (data == null)
                {
                    goto Label_006A;
                }
                if (data.GetAwakeLevelCap() > data.AwakeLv)
                {
                    goto Label_006A;
                }
                self.mState.GotoState<GachaController.State_WaitDropMaterial>();
                return;
            Label_006A:
                this.go = self.OpenMaterial;
                return;
            }

            public override void Update(GachaController self)
            {
                string str;
                string[] strArray;
                int num;
                if (string.IsNullOrEmpty(self.DropCurrent.OName) != null)
                {
                    goto Label_006A;
                }
                if (GameUtility.IsAnimatorRunning(this.go) != null)
                {
                    goto Label_0075;
                }
                strArray = this.ShardAnimList;
                num = 0;
                goto Label_005C;
            Label_0033:
                str = strArray[num];
                if (GameUtility.CompareAnimatorStateName(this.go, str) == null)
                {
                    goto Label_0058;
                }
                self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
                goto Label_0065;
            Label_0058:
                num += 1;
            Label_005C:
                if (num < ((int) strArray.Length))
                {
                    goto Label_0033;
                }
            Label_0065:
                goto Label_0075;
            Label_006A:
                self.mState.GotoState<GachaController.State_WaitDropMaterial>();
            Label_0075:
                return;
            }
        }

        private class State_OpenDropMaterialShard : State<GachaController>
        {
            public State_OpenDropMaterialShard()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                string str;
                UnitParam param;
                Animator animator;
                int num;
                bool flag;
                bool flag2;
                obj2 = self.OpenMaterial;
                str = GachaResultData.drops[self.thumbnail_count].item.iname;
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(str, 1);
                MonoSingleton<GameManager>.Instance.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(param, param.GetJobId(0)));
                obj2.SetActive(1);
                if ((obj2 != null) == null)
                {
                    goto Label_00A8;
                }
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_00A8;
                }
                num = param.raremax;
                flag = 0;
                flag2 = 0;
                animator.SetInteger("rariry", num);
                animator.SetBool("shard", flag);
                animator.SetBool("item", flag2);
            Label_00A8:
                self.mState.GotoState<GachaController.State_WaitDropmaterialShard>();
                return;
            }
        }

        private class State_OpenDropMaterialT : State<GachaController>
        {
            public State_OpenDropMaterialT()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                Animator animator;
                int num;
                bool flag;
                bool flag2;
                obj2 = self.OpenMaterial;
                obj2.SetActive(1);
                if ((obj2 != null) == null)
                {
                    goto Label_00BA;
                }
                animator = obj2.GetComponent<Animator>();
                if (((animator != null) == null) || (self.DropIndex >= ((int) GachaResultData.drops.Length)))
                {
                    goto Label_00BA;
                }
                num = GachaResultData.drops[self.DropIndex].unit.rare + 1;
                flag = (GachaResultData.drops[self.DropIndex].unitOrigin == null) ? 0 : 1;
                flag2 = (GachaResultData.drops[self.DropIndex].item == null) ? 0 : 1;
                animator.SetInteger("rariry", num);
                animator.SetBool("shard", flag);
                animator.SetBool("item", flag2);
            Label_00BA:
                self.mState.GotoState<GachaController.State_WaitDropmaterialT>();
                return;
            }
        }

        private class State_OpenDropStone : State<GachaController>
        {
            private GameObject mStone;
            private Animator at;

            public State_OpenDropStone()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                this.mStone = self.mDropStones[0].get_transform().FindChild("stone_3d2").get_gameObject();
                if (this.mStone == null)
                {
                    goto Label_0191;
                }
                this.at = this.mStone.GetComponent<Animator>();
                if ((this.at != null) == null)
                {
                    goto Label_0191;
                }
                this.at.SetTrigger("trigger_break");
                if (self.mFlowType == 1)
                {
                    goto Label_0180;
                }
                this.at.SetBool("is_coin", 1);
                if (self.DropCurrent.Excites[0] == self.DropCurrent.Excites[1])
                {
                    goto Label_0118;
                }
                this.at.SetInteger("first_break", self.DropCurrent.Excites[1]);
                if (self.DropCurrent.Excites[1] == self.DropCurrent.Excites[2])
                {
                    goto Label_0102;
                }
                this.at.SetInteger("second_break", self.DropCurrent.Excites[2]);
                goto Label_0113;
            Label_0102:
                this.at.SetInteger("second_break", 0);
            Label_0113:
                goto Label_017B;
            Label_0118:
                if (self.DropCurrent.Excites[0] == self.DropCurrent.Excites[2])
                {
                    goto Label_0159;
                }
                this.at.SetInteger("first_break", self.DropCurrent.Excites[2]);
                goto Label_016A;
            Label_0159:
                this.at.SetInteger("first_break", 0);
            Label_016A:
                this.at.SetInteger("second_break", 0);
            Label_017B:
                goto Label_0191;
            Label_0180:
                this.at.SetBool("is_coin", 0);
            Label_0191:
                if (self.isSkipping == null)
                {
                    goto Label_01A3;
                }
                self.isSkipping = 0;
            Label_01A3:
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.CheckSkip() == null)
                {
                    goto Label_001E;
                }
                self.isSkipping = 1;
                self.mState.GotoState<GachaController.State_DestroyDropStone>();
                return;
            Label_001E:
                if (GameUtility.IsAnimatorRunning(this.mStone) != null)
                {
                    goto Label_0039;
                }
                self.mState.GotoState<GachaController.State_DestroyDropStone>();
            Label_0039:
                return;
            }
        }

        private class State_OpenDropThumbnail : State<GachaController>
        {
            public State_OpenDropThumbnail()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(GachaController self)
            {
                int num;
                string str;
                Transform transform;
                int num2;
                num = self.mCurrentPage * self.mViewStoneCount;
                num2 = self.thumbnail_count + 1;
                str = "item_" + &num2.ToString();
                if ((self.item_root.get_transform().Find(str) != null) == null)
                {
                    goto Label_0090;
                }
                if ((self.thumbnail_count + num) >= ((int) GachaResultData.drops.Length))
                {
                    goto Label_0085;
                }
                if (GachaResultData.drops[self.thumbnail_count + num].type != 2)
                {
                    goto Label_0090;
                }
                self.mState.GotoState<GachaController.State_OpenDropMaterialT>();
                return;
                goto Label_0090;
            Label_0085:
                DebugUtility.LogError("参照しようとしているIndexが不正です");
                return;
            Label_0090:
                self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
                return;
            }
        }

        private class State_ResetCardAnim : State<GachaController>
        {
            public State_ResetCardAnim()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.ResetGetCardUnitAnim();
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.NextGachaPhase == null)
                {
                    goto Label_001D;
                }
                self.NextGachaPhase = 0;
                self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
            Label_001D:
                return;
            }
        }

        private class State_SettingDisableDropMaterial : State<GachaController>
        {
            public State_SettingDisableDropMaterial()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                Animator animator;
                int num;
                if (self.mDropStones.Count > 0)
                {
                    goto Label_001D;
                }
                self.mState.GotoState<GachaController.State_EndSetting>();
                return;
            Label_001D:
                self.mDropMaterial.SetActive(0);
                obj2 = self.OpenMaterial;
                if ((obj2 != null) == null)
                {
                    goto Label_005B;
                }
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_005B;
                }
                animator.SetBool("reset", 1);
            Label_005B:
                num = 0;
                goto Label_0087;
            Label_0062:
                if ((self.ResetMaterials[num] != null) == null)
                {
                    goto Label_0083;
                }
                self.ResetMaterials[num].SetActive(0);
            Label_0083:
                num += 1;
            Label_0087:
                if (num < ((int) self.ResetMaterials.Length))
                {
                    goto Label_0062;
                }
                self.mState.GotoState<GachaController.State_DisableDropMaterial>();
                return;
            }
        }

        private class State_SettingDisableDropMaterialShard : State<GachaController>
        {
            public State_SettingDisableDropMaterialShard()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                Animator animator;
                int num;
                obj2 = self.OpenMaterial;
                if ((obj2 != null) == null)
                {
                    goto Label_0032;
                }
                animator = obj2.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_0032;
                }
                animator.SetBool("reset", 1);
            Label_0032:
                num = 0;
                goto Label_005E;
            Label_0039:
                if ((self.ResetMaterials[num] != null) == null)
                {
                    goto Label_005A;
                }
                self.ResetMaterials[num].SetActive(0);
            Label_005A:
                num += 1;
            Label_005E:
                if (num < ((int) self.ResetMaterials.Length))
                {
                    goto Label_0039;
                }
                self.mState.GotoState<GachaController.State_DisableDropMaterialShard>();
                return;
            }
        }

        private class State_SettingDropStone : State<GachaController>
        {
            private float mRadius;
            private float mAppear;

            public State_SettingDropStone()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                if (self.GachaSequence > 0)
                {
                    goto Label_0017;
                }
                DebugUtility.LogError("排出結果が存在しません");
                return;
            Label_0017:
                this.mRadius = self.StoneRadius;
                this.mAppear = self.StoneAppear;
                self.StartCoroutine(this.SetDropStone(self.DropStone, self.mViewStoneCount));
                return;
            }

            [DebuggerHidden]
            private IEnumerator CreateDropStone(GameObject obj, int count)
            {
                <CreateDropStone>c__Iterator73 iterator;
                iterator = new <CreateDropStone>c__Iterator73();
                iterator.obj = obj;
                iterator.count = count;
                iterator.<$>obj = obj;
                iterator.<$>count = count;
                iterator.<>f__this = this;
                return iterator;
            }

            private void CreateDropStone(GameObject gobj, Vector3 pos, int num)
            {
                GameObject obj2;
                if ((gobj == null) == null)
                {
                    goto Label_0017;
                }
                DebugUtility.LogError(string.Empty);
                return;
            Label_0017:
                if (num >= 0)
                {
                    goto Label_0029;
                }
                DebugUtility.LogError(string.Empty);
                return;
            Label_0029:
                if (GachaResultData.drops == null)
                {
                    goto Label_0040;
                }
                if (((int) GachaResultData.drops.Length) > num)
                {
                    goto Label_004B;
                }
            Label_0040:
                DebugUtility.LogError(string.Empty);
                return;
            Label_004B:
                obj2 = Object.Instantiate<GameObject>(gobj);
                obj2.get_transform().SetParent(gobj.get_transform().get_parent(), 0);
                obj2.get_transform().set_localPosition(pos);
                this.SetDropStoneTexture(obj2, GachaResultData.drops[num], (base.self.mFlowType == 1) == 0);
                base.self.mDropStones.Add(obj2);
                return;
            }

            [DebuggerHidden]
            private IEnumerator SetDropStone(GameObject obj, int count)
            {
                <SetDropStone>c__Iterator72 iterator;
                iterator = new <SetDropStone>c__Iterator72();
                iterator.obj = obj;
                iterator.count = count;
                iterator.<$>obj = obj;
                iterator.<$>count = count;
                iterator.<>f__this = this;
                return iterator;
            }

            private void SetDropStoneTexture(GameObject obj, GachaDropData drop, bool isCoin)
            {
                GameObject obj2;
                GameObject obj3;
                int num;
                GameObject obj4;
                GameObject obj5;
                GameObject obj6;
                GameObject obj7;
                GameObject obj8;
                GameObject obj9;
                GameObject obj10;
                GameObject obj11;
                GameObject obj12;
                GameObject obj13;
                obj2 = obj.get_transform().FindChild("stone_3d2").get_gameObject();
                if ((obj2 == null) == null)
                {
                    goto Label_0023;
                }
                return;
            Label_0023:
                obj3 = obj2.get_transform().FindChild("stone_root").get_gameObject();
                if ((obj3 == null) == null)
                {
                    goto Label_0046;
                }
                return;
            Label_0046:
                if (drop.type != null)
                {
                    goto Label_0052;
                }
                return;
            Label_0052:
                num = 0;
                obj4 = obj3.get_transform().FindChild("stone_base").get_gameObject();
                if ((drop.type != 4) || (isCoin == null))
                {
                    goto Label_00D8;
                }
                num = ((drop.excites[0] - 1) < 0) ? drop.excites[0] : (drop.excites[0] - 1);
                obj3.get_transform().FindChild("stone_base").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.ConceptCardStoneBases[num]);
                return;
            Label_00D8:
                if (isCoin != null)
                {
                    goto Label_0190;
                }
                obj3.get_transform().FindChild("stone_base").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.stoneBaseN);
                obj3.get_transform().FindChild("stone_hand01_move").get_gameObject().SetActive(0);
                obj3.get_transform().FindChild("stone_hand02_move").get_gameObject().SetActive(0);
                obj3.get_transform().FindChild("stone_eye01").get_gameObject().GetComponent<MeshRenderer>().set_enabled(0);
                obj3.get_transform().FindChild("stone_eye02").get_gameObject().GetComponent<MeshRenderer>().set_enabled(0);
                return;
            Label_0190:
                num = ((drop.excites[0] - 1) < 0) ? drop.excites[0] : (drop.excites[0] - 1);
                obj3.get_transform().FindChild("stone_base").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.StoneBases[num]);
                obj3.get_transform().FindChild("stone_hand01_move").get_gameObject().get_transform().FindChild("stone_hand01").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.StoneHand01s[num]);
                obj3.get_transform().FindChild("stone_hand02_move").get_gameObject().get_transform().FindChild("stone_hand02").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.StoneHand02s[num]);
                obj3.get_transform().FindChild("stone_eye01").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.StoneEye01s[num]);
                obj3.get_transform().FindChild("stone_eye02").get_gameObject().GetComponent<Renderer>().get_material().set_mainTexture(base.self.StoneEye02s[num]);
                return;
            }

            [CompilerGenerated]
            private sealed class <CreateDropStone>c__Iterator73 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal Vector3 <pos>__0;
                internal GameObject obj;
                internal GameObject <item>__1;
                internal int count;
                internal int <i>__2;
                internal float <radian>__3;
                internal float <x>__4;
                internal float <z>__5;
                internal Vector3 <pos>__6;
                internal GameObject <item>__7;
                internal Vector3 <pos>__8;
                internal GameObject <item>__9;
                internal int $PC;
                internal object $current;
                internal GameObject <$>obj;
                internal int <$>count;
                internal GachaController.State_SettingDropStone <>f__this;

                public <CreateDropStone>c__Iterator73()
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
                            goto Label_0038;
                    }
                    goto Label_032B;
                Label_0021:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 1;
                    goto Label_032D;
                Label_0038:
                    if (GachaResultData.IsCoin != null)
                    {
                        goto Label_0109;
                    }
                    this.<pos>__0 = new Vector3(0f, 0f, -this.<>f__this.mAppear);
                    this.<item>__1 = Object.Instantiate<GameObject>(this.obj);
                    this.<item>__1.get_transform().SetParent(this.obj.get_transform().get_parent(), 0);
                    this.<item>__1.get_transform().set_localPosition(this.<pos>__0);
                    this.<item>__1.SetActive(0);
                    this.<>f__this.SetDropStoneTexture(this.<item>__1, GachaResultData.drops[0], GachaResultData.IsCoin);
                    this.<>f__this.self.mDropStones.Add(this.<item>__1);
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitDropStoneS>();
                    goto Label_032B;
                Label_0109:
                    if (this.count <= 1)
                    {
                        goto Label_0262;
                    }
                    this.<i>__2 = 0;
                    goto Label_0237;
                Label_0121:
                    this.<radian>__3 = ((6.283185f / ((float) this.count)) * ((float) this.<i>__2)) - 1.396263f;
                    this.<x>__4 = Mathf.Cos(this.<radian>__3) * this.<>f__this.mRadius;
                    this.<z>__5 = Mathf.Sin(this.<radian>__3) * this.<>f__this.mRadius;
                    this.<pos>__6 = new Vector3(this.<x>__4, 0f, this.<z>__5);
                    this.<item>__7 = Object.Instantiate<GameObject>(this.obj);
                    this.<item>__7.get_transform().SetParent(this.obj.get_transform().get_parent(), 0);
                    this.<item>__7.get_transform().set_localPosition(this.<pos>__6);
                    this.<item>__7.SetActive(0);
                    this.<>f__this.SetDropStoneTexture(this.<item>__7, GachaResultData.drops[this.<i>__2], GachaResultData.IsCoin);
                    this.<>f__this.self.mDropStones.Add(this.<item>__7);
                    this.<i>__2 += 1;
                Label_0237:
                    if (this.<i>__2 < this.count)
                    {
                        goto Label_0121;
                    }
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitDropStone>();
                    goto Label_0324;
                Label_0262:
                    this.<pos>__8 = new Vector3(0f, 0f, -this.<>f__this.mAppear);
                    this.<item>__9 = Object.Instantiate<GameObject>(this.obj);
                    this.<item>__9.get_transform().SetParent(this.obj.get_transform().get_parent(), 0);
                    this.<item>__9.get_transform().set_localPosition(this.<pos>__8);
                    this.<item>__9.SetActive(0);
                    this.<>f__this.SetDropStoneTexture(this.<item>__9, GachaResultData.drops[0], GachaResultData.IsCoin);
                    this.<>f__this.self.mDropStones.Add(this.<item>__9);
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitDropStoneS>();
                Label_0324:
                    this.$PC = -1;
                Label_032B:
                    return 0;
                Label_032D:
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
            private sealed class <SetDropStone>c__Iterator72 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal GameObject obj;
                internal int count;
                internal int <i>__0;
                internal float <radian>__1;
                internal float <x>__2;
                internal float <z>__3;
                internal Vector3 <pos>__4;
                internal Vector3 <pos>__5;
                internal int $PC;
                internal object $current;
                internal GameObject <$>obj;
                internal int <$>count;
                internal GachaController.State_SettingDropStone <>f__this;

                public <SetDropStone>c__Iterator72()
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
                            goto Label_01C3;
                    }
                    goto Label_01CA;
                Label_0021:
                    if ((this.obj == null) == null)
                    {
                        goto Label_0041;
                    }
                    DebugUtility.LogError(string.Empty);
                    goto Label_01CA;
                Label_0041:
                    if (this.<>f__this.self.mFlowType != null)
                    {
                        goto Label_0151;
                    }
                    if (this.count <= 1)
                    {
                        goto Label_0151;
                    }
                    this.<i>__0 = 0;
                    goto Label_0126;
                Label_006E:
                    this.<radian>__1 = ((6.283185f / ((float) this.count)) * ((float) this.<i>__0)) - 1.396263f;
                    this.<x>__2 = Mathf.Cos(this.<radian>__1) * this.<>f__this.mRadius;
                    this.<z>__3 = Mathf.Sin(this.<radian>__1) * this.<>f__this.mRadius;
                    this.<pos>__4 = new Vector3(this.<x>__2, 0f, this.<z>__3);
                    this.<>f__this.CreateDropStone(this.<>f__this.self.GetDropStone(GachaResultData.drops[this.<i>__0]), this.<pos>__4, this.<i>__0);
                    this.<i>__0 += 1;
                Label_0126:
                    if (this.<i>__0 < this.count)
                    {
                        goto Label_006E;
                    }
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitDropStone>();
                    goto Label_01B0;
                Label_0151:
                    this.<pos>__5 = new Vector3(0f, 0f, -this.<>f__this.mAppear);
                    this.<>f__this.CreateDropStone(this.<>f__this.self.GetDropStone(GachaResultData.drops[0]), this.<pos>__5, 0);
                    this.<>f__this.self.mState.GotoState<GachaController.State_WaitDropStoneS>();
                Label_01B0:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_01CC;
                Label_01C3:
                    this.$PC = -1;
                Label_01CA:
                    return 0;
                Label_01CC:
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

        private class State_SettingDropStoneSkip : State<GachaController>
        {
            public State_SettingDropStoneSkip()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.mState.GotoState<GachaController.State_EnableDropMaterial>();
                return;
            }
        }

        private class State_WaitBeforeSummons : State<GachaController>
        {
            public State_WaitBeforeSummons()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
            }

            public override void Update(GachaController self)
            {
                Animator animator;
                GachaVoice voice;
                if (self.mClicked == null)
                {
                    goto Label_00B1;
                }
                self.mClicked = 0;
                animator = self.get_gameObject().GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_0036;
                }
                animator.SetBool("is_skip", 1);
            Label_0036:
                if (self.mJingleHandle == null)
                {
                    goto Label_0071;
                }
                self.mJingleHandle.Stop(0f);
                self.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0016", "JIN_0016", 1, 0f);
            Label_0071:
                voice = self.GetComponent<GachaVoice>();
                if ((voice != null) == null)
                {
                    goto Label_008A;
                }
                voice.Stop();
            Label_008A:
                if (self.mFlowType == null)
                {
                    goto Label_00A5;
                }
                self.mState.GotoState<GachaController.State_EnableDropMaterial>();
                goto Label_00B0;
            Label_00A5:
                self.mState.GotoState<GachaController.State_SettingDropStone>();
            Label_00B0:
                return;
            Label_00B1:
                if (GameUtility.CompareAnimatorStateName(self.get_gameObject(), "SequenceAnim7_Low") != null)
                {
                    goto Label_00F0;
                }
                if (GameUtility.CompareAnimatorStateName(self.get_gameObject(), "SequenceAnim7_Middle") != null)
                {
                    goto Label_00F0;
                }
                if (GameUtility.CompareAnimatorStateName(self.get_gameObject(), "SequenceAnim7_High") == null)
                {
                    goto Label_00FB;
                }
            Label_00F0:
                self.mState.GotoState<GachaController.State_SettingDropStone>();
            Label_00FB:
                return;
            }
        }

        private class State_WaitCardAnim : State<GachaController>
        {
            public State_WaitCardAnim()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.FinishedCardUnitAnimation == null)
                {
                    goto Label_0034;
                }
                if (self.mClicked == null)
                {
                    goto Label_0034;
                }
                self.mClicked = 0;
                self.FinishedCardUnitAnimation = 0;
                self.mState.GotoState<GachaController.State_ResetCardAnim>();
                goto Label_003B;
            Label_0034:
                self.mClicked = 0;
            Label_003B:
                return;
            }
        }

        private class State_WaitDropMaterial : State<GachaController>
        {
            public State_WaitDropMaterial()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.mClicked = 0;
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.IsAnimatorRunning(self.OpenMaterial) != null)
                {
                    goto Label_0032;
                }
                self.mClicked = 0;
                self.mState.GotoState<GachaController.State_CheckCardUnit>();
                goto Label_0039;
            Label_0032:
                self.mClicked = 0;
            Label_0039:
                return;
            }
        }

        private class State_WaitDropmaterialShard : State<GachaController>
        {
            public State_WaitDropmaterialShard()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.IsAnimatorRunning(self.OpenMaterial) != null)
                {
                    goto Label_0032;
                }
                self.mClicked = 0;
                self.mState.GotoState<GachaController.State_SettingDisableDropMaterialShard>();
                goto Label_0039;
            Label_0032:
                self.mClicked = 0;
            Label_0039:
                return;
            }
        }

        private class State_WaitDropmaterialT : State<GachaController>
        {
            public State_WaitDropmaterialT()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.IsAnimatorRunning(self.OpenMaterial) != null)
                {
                    goto Label_0032;
                }
                self.mClicked = 0;
                self.mState.GotoState<GachaController.State_DisableDropMaterialT>();
                goto Label_0039;
            Label_0032:
                self.mClicked = 0;
            Label_0039:
                return;
            }
        }

        private class State_WaitDropStone : State<GachaController>
        {
            private float mNextStateTime;
            private bool mAllActivated;
            private float mWaitTime;

            public State_WaitDropStone()
            {
                this.mWaitTime = 0.1f;
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.StartCoroutine(this.SetActiveDropStone());
                return;
            }

            [DebuggerHidden]
            private IEnumerator SetActiveDropStone()
            {
                <SetActiveDropStone>c__Iterator74 iterator;
                iterator = new <SetActiveDropStone>c__Iterator74();
                iterator.<>f__this = this;
                return iterator;
            }

            public override void Update(GachaController self)
            {
                if (this.mAllActivated == null)
                {
                    goto Label_0044;
                }
                if (self.IsAssetDownloadDone() == null)
                {
                    goto Label_0044;
                }
                if (this.mNextStateTime <= 0f)
                {
                    goto Label_0039;
                }
                this.mNextStateTime -= Time.get_deltaTime();
                return;
            Label_0039:
                self.mState.GotoState<GachaController.State_MoveDropStone>();
            Label_0044:
                return;
            }

            [CompilerGenerated]
            private sealed class <SetActiveDropStone>c__Iterator74 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal int <i>__0;
                internal GameObject <stone>__1;
                internal Animator <at>__2;
                internal int $PC;
                internal object $current;
                internal GachaController.State_WaitDropStone <>f__this;

                public <SetActiveDropStone>c__Iterator74()
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
                            goto Label_0124;
                    }
                    goto Label_0161;
                Label_0021:
                    this.<i>__0 = this.<>f__this.self.mDropStones.Count - 1;
                    goto Label_0132;
                Label_0043:
                    this.<>f__this.self.mDropStones[this.<i>__0].SetActive(1);
                    this.<>f__this.self.mDropStones[this.<i>__0].get_transform().SetAsLastSibling();
                    this.<stone>__1 = this.<>f__this.self.mDropStones[this.<i>__0].get_transform().FindChild("stone_3d2").get_gameObject();
                    if ((this.<stone>__1 != null) == null)
                    {
                        goto Label_0102;
                    }
                    this.<at>__2 = this.<stone>__1.GetComponent<Animator>();
                    if ((this.<at>__2 != null) == null)
                    {
                        goto Label_0102;
                    }
                    this.<at>__2.SetBool("loop_flag", 1);
                Label_0102:
                    this.$current = new WaitForSeconds(this.<>f__this.mWaitTime);
                    this.$PC = 1;
                    goto Label_0163;
                Label_0124:
                    this.<i>__0 -= 1;
                Label_0132:
                    if (this.<i>__0 >= 0)
                    {
                        goto Label_0043;
                    }
                    this.<>f__this.mAllActivated = 1;
                    this.<>f__this.mNextStateTime = 2f;
                    this.$PC = -1;
                Label_0161:
                    return 0;
                Label_0163:
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

        private class State_WaitDropStoneS : State<GachaController>
        {
            public State_WaitDropStoneS()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                self.mDropStones[0].SetActive(1);
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.IsAssetDownloadDone() == null)
                {
                    goto Label_0016;
                }
                self.mState.GotoState<GachaController.State_OpenDropStone>();
            Label_0016:
                return;
            }
        }

        private class State_WaitEndInput : State<GachaController>
        {
            public State_WaitEndInput()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.mClicked == null)
                {
                    goto Label_001D;
                }
                self.mClicked = 0;
                self.mState.GotoState<GachaController.State_EndSetting>();
            Label_001D:
                return;
            }
        }

        private class State_WaitForInput_NextDropSet : State<GachaController>
        {
            public State_WaitForInput_NextDropSet()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                FlowNode_GameObject.ActivateOutputLinks(self, 0x65);
                return;
            }

            public override void Update(GachaController self)
            {
                if (self.IsNextDropSet == null)
                {
                    goto Label_001E;
                }
                self.IsNextDropSet = 0;
                self.mState.GotoState<GachaController.State_InitNextDropSet>();
                return;
            Label_001E:
                return;
            }
        }

        private class State_WaitGaugeAnimation : State<GachaController>
        {
            private GachaUnitShard unitshard;

            public State_WaitGaugeAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                Transform transform;
                transform = self.GaugeObject.get_transform().FindChild("UnitShard_gauge");
                this.unitshard = transform.GetComponent<GachaUnitShard>();
                if ((this.unitshard != null) == null)
                {
                    goto Label_0065;
                }
                if (this.unitshard.IsReachingAwakelv() != null)
                {
                    goto Label_0065;
                }
                this.unitshard.Reset();
                self.GaugeObject.SetActive(1);
                this.unitshard.Restart();
            Label_0065:
                return;
            }

            private void MoveNextPhase()
            {
                base.self.GaugeObject.SetActive(0);
                if (base.self.mFlowType == null)
                {
                    goto Label_00CA;
                }
                if (this.unitshard.IsReachingUnlockUnit() == null)
                {
                    goto Label_0042;
                }
                base.self.mState.GotoState<GachaController.State_OpenDropMaterialShard>();
                return;
            Label_0042:
                base.self.DropCurrent.Next(base.self);
                base.self.thumbnail_count += 1;
                if (base.self.thumbnail_count >= base.self.mViewStoneCount)
                {
                    goto Label_0097;
                }
                base.self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
            Label_0097:
                if (base.self.mFlowType != 2)
                {
                    goto Label_00B9;
                }
                base.self.mState.GotoState<GachaController.State_CheckNextDropSet>();
                return;
            Label_00B9:
                base.self.mState.GotoState<GachaController.State_WaitEndInput>();
                return;
            Label_00CA:
                base.self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
                return;
            }

            public override void Update(GachaController self)
            {
                Transform transform;
                if (this.unitshard.IsReachingAwakelv() == null)
                {
                    goto Label_0029;
                }
                if (self.mClicked == null)
                {
                    goto Label_0028;
                }
                self.mClicked = 0;
                this.MoveNextPhase();
            Label_0028:
                return;
            Label_0029:
                if (self.mClicked == null)
                {
                    goto Label_0046;
                }
                self.mClicked = 0;
                this.unitshard.OnClicked();
            Label_0046:
                if ((this.unitshard != null) == null)
                {
                    goto Label_0072;
                }
                if (this.unitshard.IsRunningAnimator != null)
                {
                    goto Label_00BB;
                }
                this.MoveNextPhase();
                goto Label_00BB;
            Label_0072:
                transform = self.GaugeObject.get_transform().FindChild("UnitShard_gauge");
                this.unitshard = transform.GetComponent<GachaUnitShard>();
                if ((this.unitshard != null) == null)
                {
                    goto Label_00BB;
                }
                this.unitshard.Reset();
                this.unitshard.Restart();
            Label_00BB:
                return;
            }
        }

        private class State_WaitInputFlick : State<GachaController>
        {
            private bool mSuccessDistX;
            private bool mSuccessDistY;
            private Animator atr;

            public State_WaitInputFlick()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaController self)
            {
                GameObject obj2;
                obj2 = self.get_gameObject();
                this.atr = obj2.GetComponent<Animator>();
                return;
            }

            public override unsafe void Update(GachaController self)
            {
                float num;
                float num2;
                float num3;
                float num4;
                Vector2 vector;
                Vector3 vector2;
                Vector2 vector3;
                Vector2 vector4;
                Vector3 vector5;
                Vector2 vector6;
                if (self.mDraged == null)
                {
                    goto Label_01F9;
                }
                this.atr.SetTrigger("is_flick_action");
                this.atr.ResetTrigger("back_sequence");
                vector2 = new Vector3(self.mDragEndX, 0f, 0f) - new Vector3(&GachaController.mTouchController.DragStart.x, 0f, 0f);
                num = &vector2.get_magnitude();
                if (Mathf.Sign(self.mDragEndX - &GachaController.mTouchController.DragStart.x) >= 0f)
                {
                    goto Label_00AF;
                }
                if (num <= self.MIN_SWIPE_DIST_X)
                {
                    goto Label_00AF;
                }
                this.mSuccessDistX = 1;
            Label_00AF:
                vector5 = new Vector3(0f, self.mDragEndY, 0f) - new Vector3(0f, &GachaController.mTouchController.DragStart.y, 0f);
                num3 = &vector5.get_magnitude();
                if (Mathf.Sign(self.mDragEndY - &GachaController.mTouchController.DragStart.y) >= 0f)
                {
                    goto Label_0133;
                }
                if (num3 <= self.MIN_SWIPE_DIST_Y)
                {
                    goto Label_0133;
                }
                this.mSuccessDistY = 1;
            Label_0133:
                self.mDraged = 0;
                if (this.mSuccessDistX == null)
                {
                    goto Label_01D9;
                }
                if (this.mSuccessDistY == null)
                {
                    goto Label_01D9;
                }
                this.mSuccessDistX = 0;
                this.mSuccessDistY = 0;
                self.mIgnoreDragVelocity = 1;
                this.atr.SetTrigger("is_flick_finish");
                this.atr.SetInteger("seqF_root", self.mFirstSeq);
                this.atr.SetInteger("seqS_root", self.mSecondSeq);
                this.atr.SetInteger("seqT_root", self.mThirdSeq);
                this.atr.SetInteger("seqFo_root", self.mFourthSeq);
                self.mState.GotoState<GachaController.State_WaitBeforeSummons>();
                return;
            Label_01D9:
                this.atr.ResetTrigger("is_flick_action");
                this.atr.SetTrigger("back_sequence");
            Label_01F9:
                if (self.mDraging == null)
                {
                    goto Label_0214;
                }
                this.atr.SetTrigger("is_flick_action");
            Label_0214:
                return;
            }
        }

        private class State_WaitThumbnailAnimation : State<GachaController>
        {
            private bool isSetup;
            private Animation anim;

            public State_WaitThumbnailAnimation()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(GachaController self)
            {
                string str;
                Transform transform;
                GachaDropData.Type type;
                GameObject obj2;
                GameObject obj3;
                SerializeValueBehaviour behaviour;
                GameObject obj4;
                int num;
                num = self.thumbnail_count + 1;
                str = "item_" + &num.ToString();
                transform = self.item_root.get_transform().Find(str);
                type = GachaResultData.drops[self.thumbnail_count].type;
                if ((transform != null) == null)
                {
                    goto Label_00FC;
                }
                obj2 = transform.get_gameObject();
                obj2.SetActive(1);
                obj3 = null;
                behaviour = obj2.GetComponent<SerializeValueBehaviour>();
                if ((behaviour == null) == null)
                {
                    goto Label_007D;
                }
                DebugUtility.LogError("SerializeValueBehaviourがアタッチされていません");
                return;
            Label_007D:
                obj4 = behaviour.list.GetGameObject("thumbnail");
                if ((obj4 == null) == null)
                {
                    goto Label_00A8;
                }
                DebugUtility.LogError("SerializeValueにサムネイルのGameObjectが指定されていません.");
                return;
            Label_00A8:
                obj3 = self.GetThumbnailTypeObject(obj4, type);
                if ((obj3 == null) == null)
                {
                    goto Label_00CB;
                }
                DebugUtility.LogError("対象のサムネイルオブジェクトがありません.");
                return;
            Label_00CB:
                if ((obj3 != null) == null)
                {
                    goto Label_00E5;
                }
                this.anim = obj3.GetComponent<Animation>();
            Label_00E5:
                obj3.SetActive(1);
                obj4.SetActive(1);
                this.isSetup = 1;
            Label_00FC:
                return;
            }

            public override void Update(GachaController self)
            {
                if (this.isSetup == null)
                {
                    goto Label_0047;
                }
                if ((this.anim != null) == null)
                {
                    goto Label_003C;
                }
                if (this.anim.get_isPlaying() != null)
                {
                    goto Label_0047;
                }
                self.mState.GotoState<GachaController.State_CheckThumbnail>();
                goto Label_0047;
            Label_003C:
                self.mState.GotoState<GachaController.State_CheckThumbnail>();
            Label_0047:
                return;
            }
        }
    }
}

