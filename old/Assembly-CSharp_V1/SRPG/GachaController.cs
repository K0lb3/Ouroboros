// Decompiled with JetBrains decompiler
// Type: SRPG.GachaController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "AllSkip", FlowNode.PinTypes.Input, 1)]
  public class GachaController : MonoSingleton<GachaController>, IFlowInterface
  {
    private Color[] FlickEffectColor01 = new Color[3]{ new Color(75f, 166f, (float) byte.MaxValue), new Color((float) byte.MaxValue, 250f, 128f), new Color((float) byte.MaxValue, 30f, 13f) };
    private Color[] FlickEffectColor02 = new Color[3]{ new Color(30f, 250f, (float) byte.MaxValue), new Color((float) byte.MaxValue, 250f, 128f), new Color((float) byte.MaxValue, 0.0f, 0.0f) };
    private Color[] FlickEffectColor03 = new Color[3]{ new Color(0.0f, 83f, 218f), new Color((float) byte.MaxValue, 226f, 138f), new Color((float) byte.MaxValue, 24f, 15f) };
    public bool OnShardEffect = true;
    public float MIN_SWIPE_DIST_X = 400f;
    public float MIN_SWIPE_DIST_Y = 400f;
    private bool mLithograph = true;
    private const float MinWaitBeforMoveDropStone = 2f;
    public GameObject DropStone;
    public GameObject DropMaterial;
    public RawImage DropMaterialImage;
    public RawImage DropMaterialBlurImage;
    public RawImage DropMaterialIconImage;
    public Image DropMaterialIconFrameImage;
    public GameObject ItemThumbnailPrefab;
    public GameObject UnitThumbnailPrefab;
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
    public GameObject[] ResetMaterials;
    public Sprite[] StartArrowSprite;
    public Sprite[] StartArrowTopSprite;
    public Sprite[] StartStoneSprite;
    private static TouchController mTouchController;
    public GameObject GaugeObject;
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
    public float StoneRadius;
    public float StoneAppear;
    public Texture[] StoneBases;
    public Texture[] StoneHand01s;
    public Texture[] StoneHand02s;
    public Texture[] StoneEye01s;
    public Texture[] StoneEye02s;
    public Texture stoneBaseN;
    public Texture stoneNullParts;
    public Sprite[] LithographBases;
    public float StoneRotateTime;
    private MySound.VolumeHandle mBGMVolume;
    private MySound.PlayHandle mJingleHandle;
    private bool AllAnimSkip;
    public float StoneCenterHeight;
    private GameObject item_root;
    private int thumbnail_count;

    public int GachaSequence
    {
      get
      {
        return GachaResultData.drops.Length;
      }
    }

    private Canvas OverlayCanvas
    {
      get
      {
        if (Object.op_Inequality((Object) GachaController.mTouchController, (Object) null))
          return (Canvas) ((Component) GachaController.mTouchController).GetComponent<Canvas>();
        return (Canvas) null;
      }
    }

    public bool IsAssetDownloadDone()
    {
      if (!GlobalVars.IsTutorialEnd)
        return true;
      return AssetDownloader.isDone;
    }

    private GachaController.DropInfo DropCurrent { get; set; }

    public int Rarity
    {
      get
      {
        return this.DropCurrent.Rarity + 1;
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

    [DebuggerHidden]
    private IEnumerator CreateDropInfo()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CCreateDropInfo\u003Ec__Iterator4E() { \u003C\u003Ef__this = this };
    }

    public void PlayJINGLE0010()
    {
      this.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0010", "JIN_0010", MySound.EType.JINGLE, 0.0f);
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
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.AllAnimSkip = true;
    }

    private void Start()
    {
      if (this.GachaSequence > 0)
        this.mDropStones = new List<GameObject>(this.GachaSequence);
      this.mBGMVolume = new MySound.VolumeHandle(MySound.EType.BGM);
      this.mBGMVolume.SetVolume(0.0f, 1f);
      if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null))
        HomeWindow.Current.SetVisible(false);
      this.StartCoroutine(this.CreateDropInfo());
      this.mDropMaterial = this.DropMaterial;
      this.mDropMaterial.SetActive(false);
      Animator component1 = (Animator) ((Component) this).get_gameObject().GetComponent<Animator>();
      component1.SetInteger("seqF_root", this.mFirstSeq);
      component1.SetInteger("seqS_root", this.mSecondSeq);
      component1.SetInteger("seqT_root", this.mThirdSeq);
      component1.SetInteger("seqFo_root", this.mFourthSeq);
      component1.SetInteger("seqFi_root", this.mFifthSeq);
      if (!GachaResultData.IsCoin)
      {
        ((Image) this.StartStone.GetComponent<Image>()).set_sprite(this.StartStoneSprite[0]);
        ((Image) this.StartStoneMask.GetComponent<Image>()).set_sprite(this.StartStoneSprite[0]);
      }
      else
      {
        ((Image) this.StartArrowB.GetComponent<Image>()).set_sprite(this.StartArrowSprite[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]);
        ((Image) this.StartArrowTop.GetComponent<Image>()).set_sprite(this.StartArrowTopSprite[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]);
        for (int index = 0; index < this.StartArrowTopMasks.Length; ++index)
          ((Image) this.StartArrowTopMasks[index].GetComponent<Image>()).set_sprite(this.StartArrowTopSprite[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]);
        ((Image) this.StartStone.GetComponent<Image>()).set_sprite(this.StartStoneSprite[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq]);
        ((Image) this.StartStoneMask.GetComponent<Image>()).set_sprite(this.StartStoneSprite[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq]);
        ((ParticleSystem) this.StartStoneEff01.GetComponent<ParticleSystem>()).set_startColor(this.ConvertColor(this.FlickEffectColor01[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]));
        ((ParticleSystem) this.StartStoneEff02.GetComponent<ParticleSystem>()).set_startColor(this.ConvertColor(this.FlickEffectColor02[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]));
        ((ParticleSystem) this.StartStoneEff03.GetComponent<ParticleSystem>()).set_startColor(this.ConvertColor(this.FlickEffectColor03[this.mFirstSeq <= 0 ? 0 : this.mFirstSeq - 1]));
      }
      GachaVoice component2 = (GachaVoice) ((Component) this).GetComponent<GachaVoice>();
      if (Object.op_Inequality((Object) component2, (Object) null))
        component2.Excites = this.mFifthSeq;
      this.CreateTouchArea();
      this.mState = new StateMachine<GachaController>(this);
      this.mState.GotoState<GachaController.State_Init>();
    }

    private Color ConvertColor(Color color)
    {
      return new Color((float) (color.r / (double) byte.MaxValue), (float) (color.g / (double) byte.MaxValue), (float) (color.b / (double) byte.MaxValue));
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      if (this.AllAnimSkip)
      {
        ((Animator) ((Component) this).get_gameObject().GetComponent<Animator>()).SetTrigger("all_anime_skip");
        this.mState.GotoState<GachaController.State_EndSetting>();
      }
      else
        this.mState.Update();
    }

    private UnitData CreateUnitData(UnitParam uparam)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit();
      json.iid = 1L;
      json.iname = uparam.iname;
      json.exp = 0;
      json.lv = 1;
      json.plus = 0;
      json.rare = 0;
      json.select = new Json_UnitSelectable();
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam((string) uparam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    private void OnDestroy()
    {
      this.DestroyTouchArea();
      if (this.mBGMVolume == null)
        return;
      this.mBGMVolume.Discard();
      this.mBGMVolume = (MySound.VolumeHandle) null;
    }

    private void CreateTouchArea()
    {
      if (Object.op_Inequality((Object) null, (Object) GachaController.mTouchController))
        return;
      GameObject gameObject = new GameObject("TouchArea", new System.Type[6]{ typeof (Canvas), typeof (GraphicRaycaster), typeof (CanvasStack), typeof (NullGraphic), typeof (TouchController), typeof (SRPG_CanvasScaler) });
      GachaController.mTouchController = (TouchController) gameObject.GetComponent<TouchController>();
      GachaController.mTouchController.OnClick = new TouchController.ClickEvent(this.OnClick);
      GachaController.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
      GachaController.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
      ((Canvas) gameObject.GetComponent<Canvas>()).set_renderMode((RenderMode) 0);
      ((CanvasStack) gameObject.GetComponent<CanvasStack>()).Priority = 0;
    }

    private void DestroyTouchArea()
    {
      if (!Object.op_Inequality((Object) null, (Object) GachaController.mTouchController))
        return;
      Object.Destroy((Object) ((Component) GachaController.mTouchController).get_gameObject());
      GachaController.mTouchController = (TouchController) null;
    }

    private void OnClick(Vector2 screenPosition)
    {
      if (!this.mState.IsInState<GachaController.State_WaitDropMaterial>() && !this.mState.IsInState<GachaController.State_WaitDropmaterialT>() && (!this.mState.IsInState<GachaController.State_WaitDropmaterialShard>() && !this.mState.IsInState<GachaController.State_WaitEndInput>()) && (!this.mState.IsInState<GachaController.State_WaitGaugeAnimation>() && !this.mState.IsInState<GachaController.State_WaitBeforeSummons>()))
        return;
      this.mClicked = !this.AllAnimSkip;
    }

    private void OnDrag()
    {
      if (this.mIgnoreDragVelocity)
        return;
      this.mDraged = false;
      this.mDraging = true;
      this.mDragX += (float) GachaController.mTouchController.DragDelta.x;
      this.mDragY += (float) GachaController.mTouchController.DragDelta.y;
    }

    private void OnDragEnd()
    {
      this.mDragEndX = (float) GachaController.mTouchController.DragStart.x + this.mDragX;
      this.mDragEndY = (float) GachaController.mTouchController.DragStart.y + this.mDragY;
      this.mDraged = true;
      this.mDraging = false;
      this.mIgnoreDragVelocity = false;
    }

    private bool CheckSkip()
    {
      if (!this.AllAnimSkip)
        return GachaController.mTouchController.IsTouching;
      return false;
    }

    private int GetRarityTextureIndex(int rarity)
    {
      if (rarity >= 5)
        return 2;
      return rarity >= 4 ? 1 : 0;
    }

    private class DropInfo
    {
      public int Index { get; private set; }

      public string Name { get; private set; }

      public int Rarity { get; private set; }

      public bool IsShard { get; private set; }

      public bool IsItem { get; private set; }

      public string OName { get; private set; }

      public int Num { get; private set; }

      public int[] Excites { get; private set; }

      public static GachaController.DropInfo Create(GachaController self)
      {
        GachaController.DropInfo dropInfo = new GachaController.DropInfo();
        dropInfo.Reflesh(self, 0);
        return dropInfo;
      }

      public void Reflesh(GachaController self, int index = 0)
      {
        if (index >= self.GachaSequence)
          return;
        GachaDropData drop = GachaResultData.drops[this.Index];
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        self.IsLithograph = true;
        if (drop.type == GachaDropData.Type.Unit)
        {
          this.Name = drop.unit.name;
          this.Rarity = (int) drop.unit.rare;
          this.IsShard = false;
          this.IsItem = false;
          this.OName = string.Empty;
          instance1.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(drop.unit, drop.unit.GetJobId(0)));
          instance1.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(drop.unit, drop.unit.GetJobId(0)));
          instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.UnitIconSmall(drop.unit, drop.unit.GetJobId(0)));
          this.Excites = drop.excites;
        }
        else if (drop.type == GachaDropData.Type.Item)
        {
          this.Name = drop.item.name;
          this.Rarity = (int) drop.item.rare;
          this.OName = string.Empty;
          this.Num = drop.num;
          this.Excites = drop.excites;
          self.MaterialCount.set_text(this.Num.ToString());
          string str = this.Name + " x " + this.Num.ToString();
          self.MaterialName.set_text(str);
          self.MaterialComment.set_text(!string.IsNullOrEmpty(drop.item.expr) ? drop.item.expr : drop.item.flavor);
          self.IsLithograph = drop.item.type == EItemType.UnitPiece;
          if (drop.unitOrigin != null)
          {
            this.IsShard = true;
            this.IsItem = false;
            UnitParam unitParamForPiece = instance1.MasterParam.GetUnitParamForPiece(drop.item.iname, true);
            this.OName = unitParamForPiece == null ? string.Empty : unitParamForPiece.iname;
            instance1.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(drop.unitOrigin, drop.unitOrigin.GetJobId(0)));
            instance1.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(drop.unitOrigin, drop.unitOrigin.GetJobId(0)));
            instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(drop.item));
            self.DropMaterialIconFrameImage.set_sprite(GameSettings.Instance.GetItemFrame(drop.item));
            if (unitParamForPiece == null)
              return;
            GameObject gameObject = ((Component) self.GaugeObject.get_transform().FindChild("UnitShard_gauge")).get_gameObject();
            UnitData unitDataByUnitId = instance1.Player.FindUnitDataByUnitID(unitParamForPiece.iname);
            if (unitDataByUnitId != null)
            {
              int awakeLv = unitDataByUnitId.AwakeLv;
              if (awakeLv >= unitDataByUnitId.GetAwakeLevelCap())
                return;
              ((GachaUnitShard) gameObject.GetComponent<GachaUnitShard>()).Refresh(unitParamForPiece, this.Name, awakeLv, drop.num, index);
            }
            else
              ((GachaUnitShard) gameObject.GetComponent<GachaUnitShard>()).Refresh(unitParamForPiece, this.Name, 0, drop.num, index);
          }
          else
          {
            this.IsShard = false;
            this.IsItem = true;
            self.DropMaterialImage.set_texture((Texture) null);
            self.DropMaterialBlurImage.set_texture((Texture) null);
            instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(drop.item));
            self.DropMaterialIconFrameImage.set_sprite(GameSettings.Instance.GetItemFrame(drop.item));
            if (drop.item.type != EItemType.UnitPiece)
              return;
            UnitParam unitParamForPiece = instance1.MasterParam.GetUnitParamForPiece(drop.item.iname, true);
            this.OName = unitParamForPiece == null ? string.Empty : unitParamForPiece.iname;
            if (string.IsNullOrEmpty(this.OName))
              return;
            GameObject gameObject = ((Component) self.GaugeObject.get_transform().FindChild("UnitShard_gauge")).get_gameObject();
            UnitData unitDataByUnitId = instance1.Player.FindUnitDataByUnitID(unitParamForPiece.iname);
            if (unitDataByUnitId != null)
            {
              int awakeLv = unitDataByUnitId.AwakeLv;
              if (awakeLv >= unitDataByUnitId.GetAwakeLevelCap())
                return;
              ((GachaUnitShard) gameObject.GetComponent<GachaUnitShard>()).Refresh(unitParamForPiece, this.Name, awakeLv, drop.num, index);
            }
            else
              ((GachaUnitShard) gameObject.GetComponent<GachaUnitShard>()).Refresh(unitParamForPiece, this.Name, 0, drop.num, index);
          }
        }
        else
        {
          if (drop.type != GachaDropData.Type.Artifact)
            return;
          GameSettings instance2 = GameSettings.Instance;
          this.Name = drop.artifact.name;
          this.Rarity = drop.artifact.rareini;
          this.OName = string.Empty;
          this.Num = drop.num;
          this.Excites = drop.excites;
          self.MaterialCount.set_text(this.Num.ToString());
          string str = this.Name + " x " + this.Num.ToString();
          self.MaterialName.set_text(str);
          self.MaterialComment.set_text(!string.IsNullOrEmpty(drop.artifact.expr) ? drop.artifact.expr : drop.artifact.flavor);
          self.IsLithograph = false;
          this.IsShard = false;
          this.IsItem = true;
          self.DropMaterialImage.set_texture((Texture) null);
          self.DropMaterialBlurImage.set_texture((Texture) null);
          instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ArtifactIcon(drop.artifact));
          if (!Object.op_Inequality((Object) instance2, (Object) null) || this.Rarity >= instance2.ArtifactIcon_Frames.Length)
            return;
          self.DropMaterialIconFrameImage.set_sprite(instance2.ArtifactIcon_Frames[this.Rarity]);
        }
      }

      public void Next(GachaController self)
      {
        this.Reflesh(self, ++this.Index);
      }
    }

    private class State_InitDropThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openItem = self.OpenItem;
        if (Object.op_Equality((Object) openItem, (Object) null))
          return;
        openItem.SetActive(true);
        int gachaSequence = self.GachaSequence;
        string str1 = "item_" + gachaSequence.ToString();
        GameObject gameObject1 = ((Component) openItem.get_transform().Find(str1)).get_gameObject();
        if (!Object.op_Inequality((Object) gameObject1, (Object) null))
          return;
        for (int index = 0; index < gachaSequence; ++index)
        {
          string str2 = "item_" + (index + 1).ToString();
          GameObject gameObject2 = ((Component) gameObject1.get_transform().Find(str2)).get_gameObject();
          if (Object.op_Inequality((Object) gameObject2, (Object) null))
          {
            GameObject gameObject3;
            if (GachaResultData.drops[index].type == GachaDropData.Type.Item)
            {
              gameObject3 = (GameObject) Object.Instantiate<GameObject>((M0) self.ItemThumbnailPrefab);
              ((Object) gameObject3).set_name(((Object) self.ItemThumbnailPrefab).get_name());
              ItemData data = new ItemData();
              data.Setup(0L, GachaResultData.drops[index].item, GachaResultData.drops[index].num);
              data.IsNew = GachaResultData.drops[index].isNew;
              DataSource.Bind<ItemData>(gameObject3, data);
              ((ItemIcon) gameObject3.GetComponent<ItemIcon>()).UpdateValue();
            }
            else
            {
              gameObject3 = (GameObject) Object.Instantiate<GameObject>((M0) self.UnitThumbnailPrefab);
              ((Object) gameObject3).set_name(((Object) self.UnitThumbnailPrefab).get_name());
              UnitData unitData = self.CreateUnitData(GachaResultData.drops[index].unit);
              DataSource.Bind<UnitData>(gameObject3, unitData);
              ((UnitIcon) gameObject3.GetComponent<UnitIcon>()).UpdateValue();
            }
            gameObject3.get_transform().SetParent(gameObject2.get_transform(), false);
          }
        }
        GameParameter.UpdateAll(gameObject1);
        gameObject1.SetActive(true);
        self.item_root = gameObject1;
        self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
      }
    }

    private class State_OpenDropThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        string str = "item_" + (self.thumbnail_count + 1).ToString();
        if (Object.op_Inequality((Object) ((Component) self.item_root.get_transform().Find(str)).get_gameObject(), (Object) null) && GachaResultData.drops[self.thumbnail_count].type == GachaDropData.Type.Unit)
          self.mState.GotoState<GachaController.State_OpenDropMaterialT>();
        else
          self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
      }
    }

    private class State_WaitThumbnailAnimation : State<GachaController>
    {
      private GameObject item;
      private bool isSetup;
      private Animation anim;

      public override void Begin(GachaController self)
      {
        string str = "item_" + (self.thumbnail_count + 1).ToString();
        this.item = ((Component) self.item_root.get_transform().Find(str)).get_gameObject();
        if (Object.op_Equality((Object) this.item, (Object) null))
          return;
        this.item.SetActive(true);
        GameObject gameObject = (GameObject) null;
        if (Object.op_Implicit((Object) this.item.get_transform().FindChild(((Object) self.UnitThumbnailPrefab).get_name())))
          gameObject = ((Component) this.item.get_transform().FindChild(((Object) self.UnitThumbnailPrefab).get_name())).get_gameObject();
        else if (Object.op_Implicit((Object) this.item.get_transform().FindChild(((Object) self.ItemThumbnailPrefab).get_name())))
          gameObject = ((Component) this.item.get_transform().FindChild(((Object) self.ItemThumbnailPrefab).get_name())).get_gameObject();
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          this.anim = (Animation) gameObject.GetComponent<Animation>();
        gameObject.SetActive(true);
        this.isSetup = true;
      }

      public override void Update(GachaController self)
      {
        if (!this.isSetup)
          return;
        if (Object.op_Inequality((Object) this.anim, (Object) null))
        {
          if (this.anim.get_isPlaying())
            return;
          self.mState.GotoState<GachaController.State_CheckThumbnail>();
        }
        else
          self.mState.GotoState<GachaController.State_CheckThumbnail>();
      }
    }

    private class State_OpenDropMaterialT : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        openMaterial.SetActive(true);
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            int num = (int) GachaResultData.drops[self.thumbnail_count].unit.rare + 1;
            bool flag1 = GachaResultData.drops[self.thumbnail_count].unitOrigin != null;
            bool flag2 = GachaResultData.drops[self.thumbnail_count].item != null;
            component.SetInteger("rariry", num);
            component.SetBool("shard", flag1);
            component.SetBool("item", flag2);
          }
        }
        self.mState.GotoState<GachaController.State_WaitDropmaterialT>();
      }
    }

    private class State_WaitDropmaterialT : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_DisableDropMaterialT>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_DisableDropMaterialT : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        for (int index = 0; index < self.ResetMaterials.Length; ++index)
        {
          if (Object.op_Inequality((Object) self.ResetMaterials[index], (Object) null))
            self.ResetMaterials[index].SetActive(false);
        }
        GameObject openMaterial = self.OpenMaterial;
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          openMaterial.SetActive(false);
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
            component.SetBool("reset", false);
          }
        }
        string str = "item_" + (self.thumbnail_count + 1).ToString();
        GameObject gameObject1 = ((Component) self.item_root.get_transform().Find(str)).get_gameObject();
        if (Object.op_Inequality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild(((Object) self.UnitThumbnailPrefab).get_name())).get_gameObject();
          if (Object.op_Inequality((Object) gameObject2, (Object) null))
          {
            GameObject gameObject3 = ((Component) ((Component) gameObject2.get_transform().FindChild("Panel")).get_gameObject().get_transform().FindChild("item_eff")).get_gameObject();
            if (Object.op_Inequality((Object) gameObject3, (Object) null))
              gameObject3.SetActive(true);
          }
          gameObject2.SetActive(true);
          gameObject1.SetActive(true);
        }
        self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
      }
    }

    private class State_CheckThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.OnShardEffect && (self.DropCurrent.IsItem || self.DropCurrent.IsShard))
        {
          GachaDropData drop = GachaResultData.drops[self.DropCurrent.Index];
          if (drop.item.type == EItemType.UnitPiece)
          {
            UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(drop.item.iname, true).iname);
            if (unitDataByUnitId != null && unitDataByUnitId.GetAwakeLevelCap() <= unitDataByUnitId.AwakeLv)
            {
              self.DropCurrent.Next(self);
              ++self.thumbnail_count;
              if (self.thumbnail_count < self.GachaSequence)
              {
                self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
              }
              self.mState.GotoState<GachaController.State_WaitEndInput>();
              return;
            }
            self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
            return;
          }
        }
        self.DropCurrent.Next(self);
        ++self.thumbnail_count;
        if (self.thumbnail_count < self.GachaSequence)
          self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
        else
          self.mState.GotoState<GachaController.State_WaitEndInput>();
      }
    }

    private class State_OpenDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        UnitParam unitParamForPiece = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(GachaResultData.drops[self.thumbnail_count].item.iname, true);
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(unitParamForPiece, unitParamForPiece.GetJobId(0)));
        openMaterial.SetActive(true);
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            int raremax = (int) unitParamForPiece.raremax;
            bool flag1 = false;
            bool flag2 = false;
            component.SetInteger("rariry", raremax);
            component.SetBool("shard", flag1);
            component.SetBool("item", flag2);
          }
        }
        self.mState.GotoState<GachaController.State_WaitDropmaterialShard>();
      }
    }

    private class State_WaitDropmaterialShard : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_SettingDisableDropMaterialShard>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_SettingDisableDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetBool("reset", true);
        }
        for (int index = 0; index < self.ResetMaterials.Length; ++index)
        {
          if (Object.op_Inequality((Object) self.ResetMaterials[index], (Object) null))
            self.ResetMaterials[index].SetActive(false);
        }
        self.mState.GotoState<GachaController.State_DisableDropMaterialShard>();
      }
    }

    private class State_DisableDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          openMaterial.SetActive(false);
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
          }
        }
        openMaterial.SetActive(false);
        self.DropCurrent.Next(self);
        ++self.thumbnail_count;
        if (self.thumbnail_count < self.GachaSequence)
          self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
        else
          self.mState.GotoState<GachaController.State_WaitEndInput>();
      }
    }

    private class State_Init : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.isSkipping = false;
        self.mState.GotoState<GachaController.State_WaitInputFlick>();
      }
    }

    private class State_WaitInputFlick : State<GachaController>
    {
      private bool mSuccessDistX;
      private bool mSuccessDistY;
      private Animator atr;

      public override void Begin(GachaController self)
      {
        this.atr = (Animator) ((Component) self).get_gameObject().GetComponent<Animator>();
      }

      public override void Update(GachaController self)
      {
        if (self.mDraged)
        {
          this.atr.SetTrigger("is_flick_action");
          this.atr.ResetTrigger("back_sequence");
          Vector3 vector3_1 = Vector3.op_Subtraction(new Vector3(self.mDragEndX, 0.0f, 0.0f), new Vector3((float) GachaController.mTouchController.DragStart.x, 0.0f, 0.0f));
          // ISSUE: explicit reference operation
          float magnitude1 = ((Vector3) @vector3_1).get_magnitude();
          if ((double) Mathf.Sign(self.mDragEndX - (float) GachaController.mTouchController.DragStart.x) < 0.0 && (double) magnitude1 > (double) self.MIN_SWIPE_DIST_X)
            this.mSuccessDistX = true;
          Vector3 vector3_2 = Vector3.op_Subtraction(new Vector3(0.0f, self.mDragEndY, 0.0f), new Vector3(0.0f, (float) GachaController.mTouchController.DragStart.y, 0.0f));
          // ISSUE: explicit reference operation
          float magnitude2 = ((Vector3) @vector3_2).get_magnitude();
          if ((double) Mathf.Sign(self.mDragEndY - (float) GachaController.mTouchController.DragStart.y) < 0.0 && (double) magnitude2 > (double) self.MIN_SWIPE_DIST_Y)
            this.mSuccessDistY = true;
          self.mDraged = false;
          if (this.mSuccessDistX && this.mSuccessDistY)
          {
            this.mSuccessDistX = false;
            this.mSuccessDistY = false;
            self.mIgnoreDragVelocity = true;
            this.atr.SetTrigger("is_flick_finish");
            this.atr.SetInteger("seqF_root", self.mFirstSeq);
            this.atr.SetInteger("seqS_root", self.mSecondSeq);
            this.atr.SetInteger("seqT_root", self.mThirdSeq);
            this.atr.SetInteger("seqFo_root", self.mFourthSeq);
            self.mState.GotoState<GachaController.State_WaitBeforeSummons>();
            return;
          }
          this.atr.ResetTrigger("is_flick_action");
          this.atr.SetTrigger("back_sequence");
        }
        if (!self.mDraging)
          return;
        this.atr.SetTrigger("is_flick_action");
      }
    }

    private class State_WaitBeforeSummons : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
      }

      public override void Update(GachaController self)
      {
        if (self.mClicked)
        {
          self.mClicked = false;
          Animator component1 = (Animator) ((Component) self).get_gameObject().GetComponent<Animator>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.SetBool("is_skip", true);
          if (self.mJingleHandle != null)
          {
            self.mJingleHandle.Stop(0.0f);
            self.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0016", "JIN_0016", MySound.EType.JINGLE, 0.0f);
          }
          GachaVoice component2 = (GachaVoice) ((Component) self).GetComponent<GachaVoice>();
          if (Object.op_Inequality((Object) component2, (Object) null))
            component2.Stop();
          if (!GachaResultData.IsCoin)
            self.mState.GotoState<GachaController.State_EnableDropMaterial>();
          else
            self.mState.GotoState<GachaController.State_SettingDropStone>();
        }
        else
        {
          if (!GameUtility.CompareAnimatorStateName(((Component) self).get_gameObject(), "SequenceAnim7_Low") && !GameUtility.CompareAnimatorStateName(((Component) self).get_gameObject(), "SequenceAnim7_Middle") && !GameUtility.CompareAnimatorStateName(((Component) self).get_gameObject(), "SequenceAnim7_High"))
            return;
          self.mState.GotoState<GachaController.State_SettingDropStone>();
        }
      }
    }

    private class State_SettingDropStoneSkip : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mState.GotoState<GachaController.State_EnableDropMaterial>();
      }
    }

    private class State_SettingDropStone : State<GachaController>
    {
      private float mRadius;
      private float mAppear;

      public override void Begin(GachaController self)
      {
        if (self.GachaSequence <= 0)
          return;
        this.mRadius = self.StoneRadius;
        this.mAppear = self.StoneAppear;
        self.StartCoroutine(this.CreateDropStone(self.DropStone, self.GachaSequence));
      }

      [DebuggerHidden]
      private IEnumerator CreateDropStone(GameObject obj, int count)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_SettingDropStone.\u003CCreateDropStone\u003Ec__Iterator4F() { obj = obj, count = count, \u003C\u0024\u003Eobj = obj, \u003C\u0024\u003Ecount = count, \u003C\u003Ef__this = this };
      }

      private void SetDropStoneTexture(GameObject obj, GachaDropData drop, bool isCoin = true)
      {
        GameObject gameObject1 = ((Component) obj.get_transform().FindChild("stone_3d2")).get_gameObject();
        if (Object.op_Equality((Object) gameObject1, (Object) null))
          return;
        GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild("stone_root")).get_gameObject();
        if (Object.op_Equality((Object) gameObject2, (Object) null) || drop.type == GachaDropData.Type.None)
          return;
        if (!isCoin)
        {
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_base")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.stoneBaseN);
          ((Component) gameObject2.get_transform().FindChild("stone_hand01_move")).get_gameObject().SetActive(false);
          ((Component) gameObject2.get_transform().FindChild("stone_hand02_move")).get_gameObject().SetActive(false);
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_eye01")).get_gameObject().GetComponent<MeshRenderer>()).set_enabled(false);
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_eye02")).get_gameObject().GetComponent<MeshRenderer>()).set_enabled(false);
        }
        else
        {
          int index = drop.excites[0] - 1 < 0 ? drop.excites[0] : drop.excites[0] - 1;
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_base")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.StoneBases[index]);
          ((Renderer) ((Component) ((Component) gameObject2.get_transform().FindChild("stone_hand01_move")).get_gameObject().get_transform().FindChild("stone_hand01")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.StoneHand01s[index]);
          ((Renderer) ((Component) ((Component) gameObject2.get_transform().FindChild("stone_hand02_move")).get_gameObject().get_transform().FindChild("stone_hand02")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.StoneHand02s[index]);
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_eye01")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.StoneEye01s[index]);
          ((Renderer) ((Component) gameObject2.get_transform().FindChild("stone_eye02")).get_gameObject().GetComponent<Renderer>()).get_material().set_mainTexture(this.self.StoneEye02s[index]);
        }
      }
    }

    private class State_WaitDropStoneS : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mDropStones[0].SetActive(true);
      }

      public override void Update(GachaController self)
      {
        if (!self.IsAssetDownloadDone())
          return;
        self.mState.GotoState<GachaController.State_OpenDropStone>();
      }
    }

    private class State_WaitDropStone : State<GachaController>
    {
      private float mWaitTime = 0.1f;
      private float mNextStateTime;
      private bool mAllActivated;

      public override void Begin(GachaController self)
      {
        self.StartCoroutine(this.SetActiveDropStone());
      }

      [DebuggerHidden]
      private IEnumerator SetActiveDropStone()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_WaitDropStone.\u003CSetActiveDropStone\u003Ec__Iterator50() { \u003C\u003Ef__this = this };
      }

      public override void Update(GachaController self)
      {
        if (!this.mAllActivated || !self.IsAssetDownloadDone())
          return;
        if ((double) this.mNextStateTime > 0.0)
          this.mNextStateTime -= Time.get_deltaTime();
        else
          self.mState.GotoState<GachaController.State_MoveDropStone>();
      }
    }

    private class State_MoveDropStone : State<GachaController>
    {
      private float spd = 80f;
      private Vector3 mDestination = Vector3.get_zero();
      private GameObject mStone;

      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count <= 0)
        {
          self.mState.GotoState<GachaController.State_WaitEndInput>();
        }
        else
        {
          this.mDestination = new Vector3(0.0f, self.StoneCenterHeight, 0.0f);
          this.mStone = self.mDropStones[0];
          if (!self.isSkipping)
            return;
          this.mStone.get_transform().set_localPosition(this.mDestination);
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
      }

      public override void Update(GachaController self)
      {
        if (Object.op_Equality((Object) this.mStone, (Object) null))
          return;
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          this.mStone.get_transform().set_localPosition(this.mDestination);
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
        else if ((double) Vector3.Distance(this.mStone.get_transform().get_localPosition(), this.mDestination) < 0.100000001490116)
        {
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
        else
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.mDestination, this.mStone.get_transform().get_localPosition());
          // ISSUE: explicit reference operation
          Vector3 normalized = ((Vector3) @vector3).get_normalized();
          Transform transform = this.mStone.get_transform();
          transform.set_localPosition(Vector3.op_Addition(transform.get_localPosition(), Vector3.op_Multiply(Vector3.op_Multiply(normalized, this.spd), Time.get_deltaTime())));
        }
      }
    }

    private class State_OpenDropStone : State<GachaController>
    {
      private GameObject mStone;
      private Animator at;

      public override void Begin(GachaController self)
      {
        this.mStone = ((Component) self.mDropStones[0].get_transform().FindChild("stone_3d2")).get_gameObject();
        if (Object.op_Implicit((Object) this.mStone))
        {
          this.at = (Animator) this.mStone.GetComponent<Animator>();
          if (Object.op_Inequality((Object) this.at, (Object) null))
          {
            this.at.SetTrigger("trigger_break");
            if (GachaResultData.IsCoin)
            {
              this.at.SetBool("is_coin", true);
              if (self.DropCurrent.Excites[0] != self.DropCurrent.Excites[1])
              {
                this.at.SetInteger("first_break", self.DropCurrent.Excites[1]);
                if (self.DropCurrent.Excites[1] != self.DropCurrent.Excites[2])
                  this.at.SetInteger("second_break", self.DropCurrent.Excites[2]);
                else
                  this.at.SetInteger("second_break", 0);
              }
              else
              {
                if (self.DropCurrent.Excites[0] != self.DropCurrent.Excites[2])
                  this.at.SetInteger("first_break", self.DropCurrent.Excites[2]);
                else
                  this.at.SetInteger("first_break", 0);
                this.at.SetInteger("second_break", 0);
              }
            }
            else
              this.at.SetBool("is_coin", false);
          }
        }
        if (!self.isSkipping)
          return;
        self.isSkipping = false;
      }

      public override void Update(GachaController self)
      {
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          self.mState.GotoState<GachaController.State_DestroyDropStone>();
        }
        else
        {
          if (GameUtility.IsAnimatorRunning(this.mStone))
            return;
          self.mState.GotoState<GachaController.State_DestroyDropStone>();
        }
      }
    }

    private class State_DestroyDropStone : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count <= 0)
        {
          self.mState.GotoState<GachaController.State_CheckDropStone>();
        }
        else
        {
          self.mDropStones[0].SetActive(false);
          self.mDropStones.RemoveAt(0);
          self.mState.GotoState<GachaController.State_EnableDropMaterial>();
        }
      }
    }

    private class State_ActionRevolver : State<GachaController>
    {
      private Vector3 mNewAngle;
      private float mMoveSpeed;
      private float mTheta;

      public override void Begin(GachaController self)
      {
        this.mTheta = 360f / (float) self.GachaSequence;
        Transform parent = self.DropStone.get_transform().get_parent();
        this.mNewAngle = new Vector3((float) parent.get_localEulerAngles().x, (float) parent.get_localEulerAngles().y + this.mTheta, (float) parent.get_localEulerAngles().z);
        this.mMoveSpeed = this.mTheta / self.StoneRotateTime;
      }

      public override void Update(GachaController self)
      {
        Transform parent = self.DropStone.get_transform().get_parent();
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          parent.set_localEulerAngles(this.mNewAngle);
          self.mState.GotoState<GachaController.State_CheckDropStone>();
        }
        else if (1.0 < (double) Mathf.DeltaAngle((float) parent.get_localEulerAngles().y, (float) this.mNewAngle.y))
          parent.Rotate(Vector3.op_Multiply(this.mMoveSpeed * Time.get_deltaTime(), Vector3.get_up()));
        else
          self.mState.GotoState<GachaController.State_CheckDropStone>();
      }
    }

    private class State_EnableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mDropMaterial.SetActive(true);
        if (!GachaResultData.IsCoin)
        {
          self.mState.GotoState<GachaController.State_InitDropThumbnail>();
        }
        else
        {
          GameObject openMaterial = self.OpenMaterial;
          openMaterial.SetActive(true);
          if (Object.op_Inequality((Object) openMaterial, (Object) null))
          {
            GameObject gameObject1 = ((Component) openMaterial.get_transform().FindChild("lithograph_col")).get_gameObject();
            GameObject gameObject2 = ((Component) openMaterial.get_transform().FindChild("lithograph_eff")).get_gameObject();
            if (Object.op_Inequality((Object) gameObject1, (Object) null) && Object.op_Inequality((Object) gameObject2, (Object) null))
            {
              Image component1 = (Image) gameObject1.GetComponent<Image>();
              Image component2 = (Image) gameObject2.GetComponent<Image>();
              component1.set_sprite(self.LithographBases[self.GetRarityTextureIndex(self.Rarity)]);
              ((Behaviour) component1).set_enabled(self.IsLithograph);
              ((Behaviour) component2).set_enabled(self.IsLithograph);
            }
            Animator component = (Animator) openMaterial.GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              component.SetInteger("rariry", self.Rarity);
              component.SetBool("shard", self.Shard);
              component.SetBool("item", self.Item);
            }
          }
          if (self.isSkipping)
            self.isSkipping = false;
          self.mState.GotoState<GachaController.State_OpenDropMaterial>();
        }
      }
    }

    private class State_OpenDropMaterial : State<GachaController>
    {
      private string[] ShardAnimList = new string[10]{ "DropMaterial_rare1_Shard", "DropMaterial_rare2_Shard", "DropMaterial_rare3_Shard", "DropMaterial_rare4_Shard", "DropMaterial_rare5_Shard", "DropMaterial_item1", "DropMaterial_item2", "DropMaterial_item3", "DropMaterial_item4", "DropMaterial_item5" };
      private GameObject go;

      public override void Begin(GachaController self)
      {
        if (!self.OnShardEffect || string.IsNullOrEmpty(self.DropCurrent.OName))
        {
          self.mState.GotoState<GachaController.State_WaitDropMaterial>();
        }
        else
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(self.DropCurrent.OName);
          if (unitDataByUnitId != null && unitDataByUnitId.GetAwakeLevelCap() <= unitDataByUnitId.AwakeLv)
            self.mState.GotoState<GachaController.State_WaitDropMaterial>();
          else
            this.go = self.OpenMaterial;
        }
      }

      public override void Update(GachaController self)
      {
        if (!string.IsNullOrEmpty(self.DropCurrent.OName))
        {
          if (GameUtility.IsAnimatorRunning(this.go))
            return;
          foreach (string shardAnim in this.ShardAnimList)
          {
            if (GameUtility.CompareAnimatorStateName(this.go, shardAnim))
            {
              self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
              break;
            }
          }
        }
        else
          self.mState.GotoState<GachaController.State_WaitDropMaterial>();
      }
    }

    private class State_WaitGaugeAnimation : State<GachaController>
    {
      private GachaUnitShard unitshard;

      public override void Begin(GachaController self)
      {
        this.unitshard = (GachaUnitShard) ((Component) self.GaugeObject.get_transform().FindChild("UnitShard_gauge")).GetComponent<GachaUnitShard>();
        if (!Object.op_Inequality((Object) this.unitshard, (Object) null) || this.unitshard.IsReachingAwakelv())
          return;
        this.unitshard.Reset();
        self.GaugeObject.SetActive(true);
        this.unitshard.Restart();
      }

      private void MoveNextPhase()
      {
        this.self.GaugeObject.SetActive(false);
        if (!GachaResultData.IsCoin)
        {
          if (this.unitshard.IsReachingUnlockUnit())
          {
            this.self.mState.GotoState<GachaController.State_OpenDropMaterialShard>();
          }
          else
          {
            this.self.DropCurrent.Next(this.self);
            ++this.self.thumbnail_count;
            if (this.self.thumbnail_count < this.self.GachaSequence)
              this.self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
            else
              this.self.mState.GotoState<GachaController.State_WaitEndInput>();
          }
        }
        else
          this.self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
      }

      public override void Update(GachaController self)
      {
        if (this.unitshard.IsReachingAwakelv())
        {
          if (!self.mClicked)
            return;
          self.mClicked = false;
          this.MoveNextPhase();
        }
        else
        {
          if (self.mClicked)
          {
            self.mClicked = false;
            this.unitshard.OnClicked();
          }
          if (Object.op_Inequality((Object) this.unitshard, (Object) null))
          {
            if (this.unitshard.IsRunningAnimator)
              return;
            this.MoveNextPhase();
          }
          else
          {
            this.unitshard = (GachaUnitShard) ((Component) self.GaugeObject.get_transform().FindChild("UnitShard_gauge")).GetComponent<GachaUnitShard>();
            if (!Object.op_Inequality((Object) this.unitshard, (Object) null))
              return;
            this.unitshard.Reset();
            this.unitshard.Restart();
          }
        }
      }
    }

    private class State_WaitDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mClicked = false;
      }

      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_SettingDisableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count <= 0)
        {
          self.mState.GotoState<GachaController.State_EndSetting>();
        }
        else
        {
          self.mDropMaterial.SetActive(false);
          GameObject openMaterial = self.OpenMaterial;
          if (Object.op_Inequality((Object) openMaterial, (Object) null))
          {
            Animator component = (Animator) openMaterial.GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.SetBool("reset", true);
          }
          for (int index = 0; index < self.ResetMaterials.Length; ++index)
          {
            if (Object.op_Inequality((Object) self.ResetMaterials[index], (Object) null))
              self.ResetMaterials[index].SetActive(false);
          }
          self.mState.GotoState<GachaController.State_DisableDropMaterial>();
        }
      }
    }

    private class State_DisableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mDropMaterial.SetActive(false);
        GameObject openMaterial = self.OpenMaterial;
        if (Object.op_Inequality((Object) openMaterial, (Object) null))
        {
          Animator component = (Animator) openMaterial.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
            component.SetBool("reset", false);
          }
        }
        self.DropCurrent.Next(self);
        self.mState.GotoState<GachaController.State_ActionRevolver>();
      }
    }

    private class State_CheckDropStone : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count > 0)
          self.mState.GotoState<GachaController.State_MoveDropStone>();
        else
          self.mState.GotoState<GachaController.State_EndSetting>();
      }
    }

    private class State_WaitEndInput : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (!self.mClicked)
          return;
        self.mClicked = false;
        self.mState.GotoState<GachaController.State_EndSetting>();
      }
    }

    private class State_EndSetting : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        FlowNode_Variable.Set("GACHA_TYPE", (string) null);
        FlowNode_Variable.Set("SEQ_FIRST", (string) null);
        FlowNode_Variable.Set("SEQ_SECOND", (string) null);
        FlowNode_Variable.Set("SEQ_THIRD", (string) null);
        FlowNode_Variable.Set("GACHA_INPUT", (string) null);
        FlowNode_Variable.Set("GACHA_ANIMATION_END", (string) null);
        FlowNode_Variable.Set("GACHA_CIRCLE_SET", (string) null);
        if (self.mJingleHandle != null)
          self.mJingleHandle.Stop(1f);
        self.AllAnimSkip = false;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) self, "GACHA_ANIM_FINISH");
      }
    }
  }
}
