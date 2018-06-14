// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetDetail
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
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
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.mLeaderSkillDetailButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.mLeaderSkillDetailButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OpenLeaderSkillDetail)));
      }
      this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.mPreviewParentID);
      this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.mBGUnitImageID);
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.UnitName = GlobalVars.UnlockUnitID;
      this.DummyBind();
      this.UpdateUI();
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mCurrentPreview, (Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
        this.mCurrentPreview = (UnitPreview) null;
      }
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      if (Object.op_Inequality((Object) this.mLeaderSkillDetail, (Object) null))
        Object.Destroy((Object) this.mLeaderSkillDetail.get_gameObject());
      GameUtility.DestroyGameObjects(this.mAbilits);
    }

    private void Update()
    {
      if ((double) this.mBGUnitImgFadeTime >= (double) this.mBGUnitImgFadeTimeMax || !Object.op_Inequality((Object) this.mBGUnitImage, (Object) null))
        return;
      this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
      float num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
      this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
      if ((double) num < 1.0)
        return;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = 0.0f;
    }

    internal void DummyBind()
    {
      UnitData unitData = this.CreateUnitData(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.UnitName));
      long num = 0;
      if (num != 0L)
      {
        for (int jobNo = 0; jobNo < unitData.Jobs.Length; ++jobNo)
        {
          if (unitData.Jobs[jobNo].UniqueID == num)
          {
            unitData.SetJobIndex(jobNo);
            break;
          }
        }
      }
      JobData[] jobs = unitData.Jobs;
      for (int index = 0; index < this.mJobRoot.Length; ++index)
      {
        JobParam data = jobs == null || index >= jobs.Length ? (JobParam) null : jobs[index].Param;
        DataSource.Bind<JobParam>(this.mJobRoot[index], data);
        this.mJobRoot[index].SetActive(data != null);
      }
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unitData);
    }

    internal void UpdateUI()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      JobData jobData = dataOfClass.GetJobData(0);
      this.mRenkeiText.set_text(dataOfClass.GetCombination().ToString());
      if (jobData != null)
      {
        this.mMoveText.set_text(jobData.Param.mov.ToString());
        this.mJumpText.set_text(jobData.Param.jmp.ToString());
      }
      this.RefreshAbilitList();
      this.RefreshLeaderSkillInfo();
      this.ReloadPreviewModels();
      if (jobData != null)
      {
        for (int index = 0; index < this.mPreviewControllers.Count; ++index)
        {
          if (this.mPreviewControllers[index].UnitData != null)
            this.mCurrentPreview.DefaultLayer = this.mPreviewControllers[index].UnitData.JobIndex != 0 ? GameUtility.LayerHidden : GameUtility.LayerCH1;
        }
      }
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
    }

    private void OpenLeaderSkillDetail()
    {
      if (!Object.op_Equality((Object) this.mLeaderSkillDetail, (Object) null) || !Object.op_Inequality((Object) this.Prefab_LeaderSkillDetail, (Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      this.mLeaderSkillDetail = (GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(this.mLeaderSkillDetail, dataOfClass);
    }

    private void RefreshAbilitList()
    {
      if (Object.op_Equality((Object) this.mAbilityTemplate, (Object) null))
        return;
      GameUtility.DestroyGameObjects(this.mAbilits);
      this.mAbilits.Clear();
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      Transform parent = this.mAbilityTemplate.get_transform().get_parent();
      List<AbilityData> learnedAbilities = dataOfClass.GetAllLearnedAbilities();
      for (int index = 0; index < learnedAbilities.Count; ++index)
      {
        GameObject gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.mAbilityTemplate);
        AbilityData data = learnedAbilities[index];
        GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild("icon")).get_gameObject();
        ((Component) gameObject1.get_transform().FindChild("locked")).get_gameObject().SetActive(false);
        ((ImageArray) gameObject2.GetComponent<ImageArray>()).ImageIndex = (int) data.SlotType;
        gameObject1.get_transform().SetParent(parent, false);
        DataSource.Bind<AbilityData>(gameObject1, data);
        gameObject1.SetActive(true);
        this.mAbilits.Add(gameObject1);
      }
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) dataOfClass.UnitParam.raremax);
      for (int lv = dataOfClass.CurrentJob.Rank + 1; lv < JobParam.MAX_JOB_RANK; ++lv)
      {
        OString[] learningAbilitys = dataOfClass.CurrentJob.Param.GetLearningAbilitys(lv);
        if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
        {
          for (int index = 0; index < learningAbilitys.Length; ++index)
          {
            string key = (string) learningAbilitys[index];
            if (!string.IsNullOrEmpty(key))
            {
              AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mAbilityTemplate);
              ((ImageArray) ((Component) gameObject.get_transform().FindChild("icon")).get_gameObject().GetComponent<ImageArray>()).ImageIndex = (int) abilityParam.slot;
              gameObject.get_transform().SetParent(parent, false);
              DataSource.Bind<AbilityParam>(gameObject, abilityParam);
              gameObject.SetActive(true);
              this.mAbilits.Add(gameObject);
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) parent).get_gameObject());
    }

    private void RefreshLeaderSkillInfo()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.LeaderSkill != null)
      {
        ((Component) this.mLeaderSkillDetailButton).get_gameObject().SetActive(true);
        ((Selectable) this.mLeaderSkillDetailButton).set_interactable(true);
        this.mLeaderSkillText.set_text(dataOfClass.LeaderSkill.Name);
      }
      else
      {
        ((Component) this.mLeaderSkillDetailButton).get_gameObject().SetActive(false);
        this.mLeaderSkillText.set_text(LocalizedText.Get("sys.UNIT_LEADERSKILL_NOTHAVE_MESSAGE"));
      }
    }

    private void ReloadPreviewModels()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null || Object.op_Equality((Object) this.mPreviewParent, (Object) null))
        return;
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (UnitPreview) null;
      for (int index = 0; index < dataOfClass.Jobs.Length; ++index)
      {
        UnitPreview unitPreview = (UnitPreview) null;
        if (dataOfClass.Jobs[index] != null && dataOfClass.Jobs[index].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
          unitPreview = (UnitPreview) gameObject.GetComponent<UnitPreview>();
          unitPreview.DefaultLayer = GameUtility.LayerHidden;
          unitPreview.SetupUnit(dataOfClass.UnitParam.iname, dataOfClass.Jobs[index].JobID);
          gameObject.get_transform().SetParent(this.mPreviewParent, false);
          if (index == dataOfClass.JobIndex)
            this.mCurrentPreview = unitPreview;
        }
        this.mPreviewControllers.Add(unitPreview);
      }
      if (!Object.op_Implicit((Object) this.mCurrentPreview))
        return;
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
      this.mCurrentPreview = (UnitPreview) gameObject1.GetComponent<UnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(dataOfClass, -1);
      gameObject1.get_transform().SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
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
      if (Object.op_Equality((Object) this.mBGUnitImage, (Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).get_color();
      color.a = (__Null) (double) alpha;
      ((Graphic) this.mBGUnitImage).set_color(color);
    }

    [DebuggerHidden]
    private IEnumerator UpdateFadeUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitGetDetail.\u003CUpdateFadeUnitImage\u003Ec__IteratorEE() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitGetDetail.\u003CRefreshUnitImage\u003Ec__IteratorEF() { \u003C\u003Ef__this = this };
    }
  }
}
