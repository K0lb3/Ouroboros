// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultUnitDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
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
      base.\u002Ector();
    }

    [DebuggerHidden]
    private IEnumerator ShowLeaderSkillDetail(string _path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CShowLeaderSkillDetail\u003Ec__Iterator10F()
      {
        _path = _path,
        \u003C\u0024\u003E_path = _path,
        \u003C\u003Ef__this = this
      };
    }

    private void OpenLeaderSkillDetail()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_LeaderSkillDetail, (UnityEngine.Object) null))
        return;
      this.mLeaderSkillDetail = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit);
      Canvas component = (Canvas) this.mLeaderSkillDetail.GetComponent<Canvas>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_sortingOrder(10);
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.LeaderSkillDetailButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OpenLeaderSkillDetail)));
    }

    private void Refresh()
    {
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
      if (this.mCurrentUnit == null)
        return;
      if (!string.IsNullOrEmpty(this.PreviewParentID))
      {
        this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PreviewParentID);
        ((Component) this.mPreviewParent).get_transform().set_position(new Vector3(-0.2f, (float) ((Component) this.mPreviewParent).get_transform().get_position().y, (float) ((Component) this.mPreviewParent).get_transform().get_position().z));
      }
      if (!string.IsNullOrEmpty(this.PreviewBaseID))
      {
        this.mPreviewBase = GameObjectID.FindGameObject(this.PreviewBaseID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
        {
          GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerUI, true);
          this.mPreviewBase.get_transform().set_position(new Vector3(-0.2f, (float) this.mPreviewBase.get_transform().get_position().y, (float) this.mPreviewBase.get_transform().get_position().z));
          this.mPreviewBase.SetActive(false);
        }
      }
      if (!string.IsNullOrEmpty(this.BGUnitImageID))
        this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
      this.StartCoroutine(this.RefreshAsync(false));
    }

    private void OnEnable()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData01, (UnityEngine.Object) null))
        this.JobData01.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData02, (UnityEngine.Object) null))
        this.JobData02.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobData03, (UnityEngine.Object) null))
        this.JobData03.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillDetailButton, (UnityEngine.Object) null))
        ((Selectable) this.LeaderSkillDetailButton).set_interactable(false);
      this.Refresh();
    }

    private void OnDisable()
    {
      this.SetPreviewVisible(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
      {
        this.mPreviewBase.get_transform().set_position(new Vector3(0.2f, (float) this.mPreviewBase.get_transform().get_position().y, (float) this.mPreviewBase.get_transform().get_position().z));
        this.mPreviewBase.SetActive(false);
      }
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
    }

    [DebuggerHidden]
    private IEnumerator RefreshAsync(bool immediate = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CRefreshAsync\u003Ec__Iterator110()
      {
        immediate = immediate,
        \u003C\u0024\u003Eimmediate = immediate,
        \u003C\u003Ef__this = this
      };
    }

    private void Update()
    {
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null) && !this.mCurrentPreview.IsLoading))
      {
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerUI, true);
        this.mUpdatePreviewVisibility = false;
      }
      if ((double) this.mBGUnitImgFadeTime >= (double) this.mBGUnitImgFadeTimeMax || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
      float num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
      this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
      if ((double) num < 1.0)
        return;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = 0.0f;
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
        this.mCurrentPreview = (UnitPreview) null;
      }
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mLeaderSkillDetail, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mLeaderSkillDetail.get_gameObject());
      GameUtility.DestroyGameObjects(this.mAbilits);
    }

    public void Setup(int index)
    {
      UnitParam unit = GachaResultData.drops[index].unit;
      if (unit == null)
        return;
      this.Setup(this.CreateUnitData(unit));
    }

    public void Setup(UnitData _data)
    {
      this.mCurrentUnit = _data;
    }

    private void SetPreviewVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
        return;
      this.mDesiredPreviewVisibility = visible;
      if (!visible)
      {
        ((Component) this.mPreviewParent).get_transform().set_position(new Vector3(0.2f, (float) ((Component) this.mPreviewParent).get_transform().get_position().y, (float) ((Component) this.mPreviewParent).get_transform().get_position().z));
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      }
      else
        this.mUpdatePreviewVisibility = true;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null) || this.mPreviewBase.get_activeSelf() || !visible)
        return;
      this.mPreviewBase.SetActive(true);
    }

    private void ReloadPreviewModels()
    {
      if (this.mCurrentUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (UnitPreview) null;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCurrentPreview))
        return;
      GameObject gameObject = new GameObject("Preview", new System.Type[1]
      {
        typeof (UnitPreview)
      });
      this.mCurrentPreview = (UnitPreview) gameObject.GetComponent<UnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
      gameObject.get_transform().SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
    }

    private void RefreshStatus()
    {
      DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit);
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.mCurrentUnit.Rarity);
      this.UnitLv.set_text("1");
      this.UnitLvMax.set_text(rarityParam.UnitLvCap.ToString());
      for (int index = 0; index < StatusParam.MAX_STATUS; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStatusParamSlots[index], (UnityEngine.Object) null))
          this.mStatusParamSlots[index].set_text(this.mCurrentUnit.Status.param[(StatusTypes) index].ToString());
      }
      this.Status_Renkei.set_text(this.mCurrentUnit.GetCombination().ToString());
      JobData jobData = this.mCurrentUnit.GetJobData(0);
      this.Status_Move.set_text(jobData.Param.mov.ToString());
      this.Status_Jump.set_text(jobData.Param.jmp.ToString());
      GameParameter.UpdateAll(this.UnitInfo);
    }

    private void RefreshLeaderSkillInfo()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.LeaderSkillInfo, (UnityEngine.Object) null))
        return;
      if (this.mCurrentUnit.LeaderSkill != null)
      {
        ((Selectable) this.LeaderSkillDetailButton).set_interactable(true);
        this.LeaderSkillName.set_text(this.mCurrentUnit.LeaderSkill.Name);
      }
      else
        this.LeaderSkillName.set_text(LocalizedText.Get("sys.UNIT_LEADERSKILL_NOTHAVE_MESSAGE"));
    }

    private void RefreshJobInfo()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.JobInfo, (UnityEngine.Object) null))
        return;
      JobData[] jobs = this.mCurrentUnit.Jobs;
      DataSource.Bind<JobParam>(this.JobData01, jobs[0].Param);
      this.JobData01.SetActive(true);
      if (jobs.Length >= 2)
      {
        DataSource.Bind<JobParam>(this.JobData02, jobs[1].Param);
        this.JobData02.SetActive(true);
      }
      if (jobs.Length >= 3)
      {
        DataSource.Bind<JobParam>(this.JobData03, jobs[2].Param);
        this.JobData03.SetActive(true);
      }
      GameParameter.UpdateAll(this.JobInfo);
    }

    private void RefreshAbilitList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AbilityTemplate, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects(this.mAbilits);
      this.mAbilits.Clear();
      List<AbilityData> learnedAbilities = this.mCurrentUnit.GetAllLearnedAbilities(false);
      for (int index = 0; index < learnedAbilities.Count; ++index)
      {
        AbilityData data = learnedAbilities[index];
        if (this.mCurrentUnit.MapEffectAbility != data)
        {
          GameObject gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.AbilityTemplate);
          GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild("icon")).get_gameObject();
          ((Component) gameObject1.get_transform().FindChild("locked")).get_gameObject().SetActive(false);
          ((ImageArray) gameObject2.GetComponent<ImageArray>()).ImageIndex = (int) data.SlotType;
          gameObject1.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), false);
          DataSource.Bind<AbilityData>(gameObject1, data);
          gameObject1.SetActive(true);
          this.mAbilits.Add(gameObject1);
        }
      }
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) this.mCurrentUnit.UnitParam.raremax);
      for (int lv = this.mCurrentUnit.CurrentJob.Rank + 1; lv < JobParam.MAX_JOB_RANK; ++lv)
      {
        OString[] learningAbilitys = this.mCurrentUnit.CurrentJob.Param.GetLearningAbilitys(lv);
        if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
        {
          for (int index = 0; index < learningAbilitys.Length; ++index)
          {
            string key = (string) learningAbilitys[index];
            if (!string.IsNullOrEmpty(key))
            {
              AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
              GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.AbilityTemplate);
              ((ImageArray) ((Component) gameObject.get_transform().FindChild("icon")).get_gameObject().GetComponent<ImageArray>()).ImageIndex = (int) abilityParam.slot;
              gameObject.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), false);
              DataSource.Bind<AbilityParam>(gameObject, abilityParam);
              gameObject.SetActive(true);
              this.mAbilits.Add(gameObject);
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) this.AbilityTemplate.get_transform().get_parent()).get_gameObject());
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
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[index]);
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

    private void FadeUnitImage(float alphastart, float alphaend, float duration)
    {
      this.mBGUnitImgAlphaStart = alphastart;
      this.mBGUnitImgAlphaEnd = alphaend;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).get_color();
      color.a = (__Null) (double) alpha;
      ((Graphic) this.mBGUnitImage).set_color(color);
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultUnitDetail.\u003CRefreshUnitImage\u003Ec__Iterator111()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
