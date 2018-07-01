// Decompiled with JetBrains decompiler
// Type: SRPG.UnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public abstract class UnitController : AnimationPlayer
  {
    public Color32 VesselColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    private List<GameObject> mPrimaryEquipmentChangeLists = new List<GameObject>();
    private List<GameObject> mSecondaryEquipmentChangeLists = new List<GameObject>();
    private List<GameObject> mSubPrimaryEquipmentChangeLists = new List<GameObject>();
    private List<GameObject> mSubSecondaryEquipmentChangeLists = new List<GameObject>();
    protected List<Material> CharacterMaterials = new List<Material>(4);
    protected List<CharacterDB.Job> mCharacterDataLists = new List<CharacterDB.Job>();
    protected List<GameObject> mUnitObjectLists = new List<GameObject>();
    protected List<CharacterSettings> mCharacterSettingsLists = new List<CharacterSettings>();
    protected List<FaceAnimation> mFaceAnimationLists = new List<FaceAnimation>();
    public const string CharacterAnimationDir = "CHM/";
    public const string COLLABO_SKILL_ASSET_PREFIX = "D";
    private UnitData mUnitData;
    private string mCharacterID;
    private string mJobID;
    private string mJobResourceID;
    protected CharacterDB.Job mCharacterData;
    private bool mSetupStarted;
    private int mNumLoadRequests;
    private GameObject mUnitObject;
    protected CharacterSettings mCharacterSettings;
    private GameObject mPrimaryEquipment;
    private GameObject mSecondaryEquipment;
    private GameObject mPrimaryEquipmentDefault;
    private GameObject mSecondaryEquipmentDefault;
    private bool mUseSubEquipment;
    private GameObject mSubPrimaryEquipment;
    private GameObject mSubSecondaryEquipment;
    private GameObject mSubPrimaryEquipmentDefault;
    private GameObject mSubSecondaryEquipmentDefault;
    private FaceAnimation mFaceAnimation;
    private bool mPlayingFaceAnimation;
    public bool LoadEquipments;
    public bool KeepUnitHidden;
    private float mVesselStrength;
    private float mVesselAnimTime;
    private float mVesselAnimDuration;
    private float mVesselAnimStart;
    private float mVesselAnimEnd;

    public CharacterDB.Job CharacterData
    {
      get
      {
        return this.mCharacterData;
      }
    }

    protected RigSetup Rig
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null))
          return this.mCharacterSettings.Rig;
        return (RigSetup) null;
      }
    }

    public float Height
    {
      get
      {
        RigSetup rig = this.Rig;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rig, (UnityEngine.Object) null))
          return rig.Height * (float) ((Component) this).get_transform().get_localScale().y;
        return 0.0f;
      }
    }

    public Vector3 CenterPosition
    {
      get
      {
        return Vector3.op_Addition(((Component) this).get_transform().get_position(), Vector3.op_Multiply(Vector3.get_up(), this.Height * 0.5f));
      }
    }

    public GameObject UnitObject
    {
      get
      {
        return this.mUnitObject;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    protected bool UseSubEquipment
    {
      get
      {
        return this.mUseSubEquipment;
      }
    }

    public override bool IsLoading
    {
      get
      {
        if (!this.mSetupStarted || this.mNumLoadRequests > 0)
          return true;
        return base.IsLoading;
      }
    }

    protected void AddLoadThreadCount()
    {
      ++this.mNumLoadRequests;
    }

    protected void RemoveLoadThreadCount()
    {
      --this.mNumLoadRequests;
      if (this.mNumLoadRequests >= 0)
        return;
      Debug.LogError((object) "Invalid load request count");
      this.mNumLoadRequests = 0;
    }

    public bool SetActivateUnitObject(int idx)
    {
      if (idx < 0 || idx >= this.mUnitObjectLists.Count)
        return false;
      int index = 0;
      while (index < this.mUnitObjectLists.Count && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitObject, (UnityEngine.Object) this.mUnitObjectLists[index]))
        ++index;
      using (List<GameObject>.Enumerator enumerator = this.mUnitObjectLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetActive(false);
      }
      this.mUnitObject = this.mUnitObjectLists[idx];
      this.mUnitObject.SetActive(true);
      this.SetAnimationComponent((Animation) this.mUnitObject.GetComponent<Animation>());
      if (idx < this.mCharacterDataLists.Count)
        this.mCharacterData = this.mCharacterDataLists[idx];
      if (idx < this.mCharacterSettingsLists.Count)
        this.mCharacterSettings = this.mCharacterSettingsLists[idx];
      if (idx < this.mFaceAnimationLists.Count)
        this.mFaceAnimation = this.mFaceAnimationLists[idx];
      return idx != index;
    }

    protected override void Start()
    {
      base.Start();
      this.mCharacterDataLists.Clear();
      this.mUnitObjectLists.Clear();
      this.mCharacterSettingsLists.Clear();
      this.mFaceAnimationLists.Clear();
      CharacterDB.Character character = CharacterDB.FindCharacter(this.mCharacterID);
      if (character == null)
      {
        Debug.LogError((object) ("Unknown character '" + this.mCharacterID + "'"));
        this.SetLoadError();
      }
      else
      {
        this.mJobResourceID = !string.IsNullOrEmpty(this.mJobID) ? MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID).model : "none";
        string str = (string) null;
        if (this.mUnitData != null && this.mUnitData.Jobs != null)
        {
          ArtifactParam selectedSkin = this.mUnitData.GetSelectedSkin(Array.FindIndex<JobData>(this.mUnitData.Jobs, (Predicate<JobData>) (j => j.Param.iname == this.mJobID)));
          str = selectedSkin == null ? (string) null : selectedSkin.asset;
        }
        if (string.IsNullOrEmpty(str))
          str = this.mJobResourceID;
        int jobIndex = -1;
        for (int index = 0; index < character.Jobs.Count; ++index)
        {
          if (character.Jobs[index].JobID == str)
          {
            jobIndex = index;
            break;
          }
        }
        if (jobIndex < 0)
          jobIndex = 0;
        this.StartCoroutine(this.AsyncSetup(character, jobIndex));
      }
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      this.UpdateFaceAnimation();
      if ((double) this.mVesselAnimTime >= (double) this.mVesselAnimDuration)
        return;
      this.UpdateVesselAnimation();
    }

    private void createRootBoneList(Transform Root, ref List<Transform> Dest)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) Root, (UnityEngine.Object) null) || Dest == null || 0 >= Root.get_childCount())
        return;
      for (int index = 0; index < Root.get_childCount(); ++index)
      {
        Transform child = Root.GetChild(index);
        Dest.Add(child);
        this.createRootBoneList(child, ref Dest);
      }
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
            UnityEngine.Object.Destroy((UnityEngine.Object) current.get_gameObject());
        }
      }
      this.mPrimaryEquipmentChangeLists = new List<GameObject>();
      using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
            UnityEngine.Object.Destroy((UnityEngine.Object) current.get_gameObject());
        }
      }
      this.mSecondaryEquipmentChangeLists = new List<GameObject>();
    }

    private void UpdateFaceAnimation()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mFaceAnimation, (UnityEngine.Object) null))
        return;
      if (this.mPlayingFaceAnimation)
      {
        this.mFaceAnimation.PlayAnimation = true;
        this.mPlayingFaceAnimation = false;
        if (this.mFaceAnimation.Animation0.Curve == null)
          this.mFaceAnimation.Face0 = 0;
        if (this.mFaceAnimation.Animation1.Curve == null)
          this.mFaceAnimation.Face1 = 0;
      }
      float position;
      AnimDef activeAnimation = this.GetActiveAnimation(out position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) activeAnimation, (UnityEngine.Object) null))
        return;
      AnimationCurve customCurve1 = activeAnimation.FindCustomCurve("FAC0");
      if (customCurve1 != null)
      {
        this.mFaceAnimation.Face0 = Mathf.FloorToInt(customCurve1.Evaluate(position)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      AnimationCurve customCurve2 = activeAnimation.FindCustomCurve("FAC1");
      if (customCurve2 != null)
      {
        this.mFaceAnimation.Face1 = Mathf.FloorToInt(customCurve2.Evaluate(position)) - 1;
        this.mPlayingFaceAnimation = true;
      }
      if (!this.mPlayingFaceAnimation)
        return;
      this.mFaceAnimation.PlayAnimation = false;
    }

    public virtual void SetupUnit(UnitData unitData, int jobIndex = -1)
    {
      this.mUnitData = unitData;
      this.mCharacterID = unitData.UnitParam.model;
      if (jobIndex == -1)
        this.mJobID = unitData.CurrentJob == null ? (string) null : unitData.CurrentJob.JobID;
      else
        this.mJobID = unitData.Jobs[jobIndex].JobID;
    }

    public virtual void SetupUnit(string unitID, string jobID)
    {
      this.mCharacterID = MonoSingleton<GameManager>.Instance.GetUnitParam(unitID).model;
      this.mJobID = jobID;
    }

    protected Transform GetCharacterRoot()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCharacterSettings, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCharacterSettings.Rig, (UnityEngine.Object) null))
        return ((Component) this).get_transform();
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).get_transform(), this.mCharacterSettings.Rig.RootBoneName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        return childRecursively;
      return ((Component) this).get_transform();
    }

    protected virtual ESex Sex
    {
      get
      {
        return this.mUnitData.UnitParam.sex;
      }
    }

    public void SetVisible(bool visible)
    {
      GameUtility.SetLayer((Component) this, !visible ? GameUtility.LayerHidden : GameUtility.LayerCH, true);
      this.OnVisibilityChange(visible);
    }

    public bool IsVisible()
    {
      return ((Component) this).get_gameObject().get_layer() != GameUtility.LayerHidden;
    }

    protected virtual void OnVisibilityChange(bool visible)
    {
    }

    public void LoadUnitAnimationAsync(string id, string animationName, bool addJobName, bool is_collabo_skill = false)
    {
      string str = this.mCharacterData.AssetPrefix;
      if (is_collabo_skill)
        str = "D";
      if (addJobName)
        this.LoadAnimationAsync(id, "CHM/" + str + "_" + this.mJobResourceID + "_" + animationName);
      else
        this.LoadAnimationAsync(id, "CHM/" + str + "_" + animationName);
    }

    private LoadRequest LoadResource<T>(string path)
    {
      if (string.IsNullOrEmpty(path))
        return (LoadRequest) null;
      return AssetManager.LoadAsync(path, typeof (T));
    }

    public bool LoadAddModels(int job_index)
    {
      if (this.mNumLoadRequests > 0)
        return false;
      CharacterDB.Character character = CharacterDB.FindCharacter(this.mCharacterID);
      if (character == null)
      {
        Debug.LogError((object) ("Unknown character '" + this.mCharacterID + "'"));
        return false;
      }
      if (job_index < 0 || job_index >= character.Jobs.Count)
        return false;
      this.StartCoroutine(this.AsyncSetup(character, job_index));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator AsyncSetup(CharacterDB.Character ch, int jobIndex)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitController.\u003CAsyncSetup\u003Ec__Iterator4E()
      {
        ch = ch,
        jobIndex = jobIndex,
        \u003C\u0024\u003Ech = ch,
        \u003C\u0024\u003EjobIndex = jobIndex,
        \u003C\u003Ef__this = this
      };
    }

    private void FindCharacterMaterials()
    {
      this.CharacterMaterials.Clear();
      Renderer[] componentsInChildren = (Renderer[]) ((Component) this).GetComponentsInChildren<Renderer>(true);
      for (int index1 = componentsInChildren.Length - 1; index1 >= 0; --index1)
      {
        Material[] materials = componentsInChildren[index1].get_materials();
        if (materials != null)
        {
          for (int index2 = 0; index2 < materials.Length; ++index2)
          {
            Material material = componentsInChildren[index1].get_material();
            if (!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false)))
              this.CharacterMaterials.Add(material);
          }
        }
      }
    }

    protected virtual void PostSetup()
    {
    }

    public void SetEquipmentsVisible(bool visible)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        this.mPrimaryEquipment.get_gameObject().SetActive(visible);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      this.mSecondaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SetPrimaryEquipmentsVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        return;
      this.mPrimaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SetSecondaryEquipmentsVisible(bool visible)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      this.mSecondaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SwitchEquipmentLists(UnitController.EquipmentType type, int no)
    {
      if (no <= 0)
        return;
      int index = no - 1;
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (index >= this.mPrimaryEquipmentChangeLists.Count)
            break;
          bool flag1 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.get_activeSelf();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            this.mPrimaryEquipment.SetActive(false);
          this.mPrimaryEquipment = this.mPrimaryEquipmentChangeLists[index];
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            break;
          this.mPrimaryEquipment.SetActive(flag1);
          break;
        case UnitController.EquipmentType.SECONDARY:
          if (index >= this.mSecondaryEquipmentChangeLists.Count)
            break;
          bool flag2 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.get_activeSelf();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            this.mSecondaryEquipment.SetActive(false);
          this.mSecondaryEquipment = this.mSecondaryEquipmentChangeLists[index];
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            break;
          this.mSecondaryEquipment.SetActive(flag2);
          break;
      }
    }

    public void ResetEquipmentLists(UnitController.EquipmentType type)
    {
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) this.mPrimaryEquipmentDefault))
            break;
          bool flag1 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment) || this.mPrimaryEquipment.get_activeSelf();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            this.mPrimaryEquipment.SetActive(false);
          this.mPrimaryEquipment = this.mPrimaryEquipmentDefault;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mPrimaryEquipment))
            break;
          this.mPrimaryEquipment.SetActive(flag1);
          break;
        case UnitController.EquipmentType.SECONDARY:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) this.mSecondaryEquipmentDefault))
            break;
          bool flag2 = !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment) || this.mSecondaryEquipment.get_activeSelf();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            this.mSecondaryEquipment.SetActive(false);
          this.mSecondaryEquipment = this.mSecondaryEquipmentDefault;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSecondaryEquipment))
            break;
          this.mSecondaryEquipment.SetActive(flag2);
          break;
      }
    }

    private void SetAttachmentParent(GameObject go, Transform parent)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) parent))
        return;
      go.get_transform().SetParent(parent, false);
      go.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
    }

    public void SwitchAttachmentLists(UnitController.EquipmentType type, int no)
    {
      if (no <= 0)
        return;
      int index = no - 1;
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (index >= this.Rig.RightHandChangeLists.Count || string.IsNullOrEmpty(this.Rig.RightHandChangeLists[index]))
            break;
          Transform childRecursively1 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHandChangeLists[index]);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively1, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mPrimaryEquipmentDefault, childRecursively1);
          using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively1);
            break;
          }
        case UnitController.EquipmentType.SECONDARY:
          if (index >= this.Rig.LeftHandChangeLists.Count || string.IsNullOrEmpty(this.Rig.LeftHandChangeLists[index]))
            break;
          Transform childRecursively2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHandChangeLists[index]);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively2, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mSecondaryEquipmentDefault, childRecursively2);
          using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively2);
            break;
          }
      }
    }

    public void ResetAttachmentLists(UnitController.EquipmentType type)
    {
      switch (type)
      {
        case UnitController.EquipmentType.PRIMARY:
          if (string.IsNullOrEmpty(this.Rig.RightHand))
            break;
          Transform childRecursively1 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively1, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mPrimaryEquipmentDefault, childRecursively1);
          using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively1);
            break;
          }
        case UnitController.EquipmentType.SECONDARY:
          if (string.IsNullOrEmpty(this.Rig.LeftHand))
            break;
          Transform childRecursively2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively2, (UnityEngine.Object) null))
            break;
          this.SetAttachmentParent(this.mSecondaryEquipmentDefault, childRecursively2);
          using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetAttachmentParent(enumerator.Current, childRecursively2);
            break;
          }
      }
    }

    public void SetSubEquipment(GameObject primaryHand, GameObject secondaryHand, List<GameObject> primary_lists, List<GameObject> secondary_lists, bool hidden = false)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) primaryHand, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.Rig.RightHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        {
          this.mSubPrimaryEquipment = UnityEngine.Object.Instantiate((UnityEngine.Object) primaryHand, primaryHand.get_transform().get_position(), primaryHand.get_transform().get_rotation()) as GameObject;
          this.mSubPrimaryEquipment.get_transform().SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubPrimaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubPrimaryEquipment, GameUtility.LayerHidden, true);
          this.mSubPrimaryEquipment.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.RightHand + " not found."));
      }
      if (primary_lists != null && !string.IsNullOrEmpty(this.Rig.RightHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) childRecursively))
        {
          this.mSubPrimaryEquipmentDefault = this.mSubPrimaryEquipment;
          this.mSubPrimaryEquipmentChangeLists.Clear();
          using (List<GameObject>.Enumerator enumerator = primary_lists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GameObject current = enumerator.Current;
              GameObject materialObject = (GameObject) null;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
              {
                materialObject = UnityEngine.Object.Instantiate((UnityEngine.Object) current, current.get_transform().get_position(), current.get_transform().get_rotation()) as GameObject;
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) materialObject))
                {
                  materialObject.get_transform().SetParent(childRecursively, false);
                  materialObject.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
                  materialObject.get_gameObject().SetActive(false);
                  this.SetMaterialByGameObject(materialObject);
                }
              }
              this.mSubPrimaryEquipmentChangeLists.Add(materialObject);
            }
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) secondaryHand, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.Rig.LeftHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        {
          this.mSubSecondaryEquipment = UnityEngine.Object.Instantiate((UnityEngine.Object) secondaryHand, secondaryHand.get_transform().get_position(), secondaryHand.get_transform().get_rotation()) as GameObject;
          this.mSubSecondaryEquipment.get_transform().SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubSecondaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubSecondaryEquipment, GameUtility.LayerHidden, true);
          this.mSubSecondaryEquipment.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.LeftHand + " not found."));
      }
      if (primary_lists != null && !string.IsNullOrEmpty(this.Rig.LeftHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) childRecursively))
        {
          this.mSubSecondaryEquipmentDefault = this.mSubSecondaryEquipment;
          this.mSubSecondaryEquipmentChangeLists.Clear();
          using (List<GameObject>.Enumerator enumerator = secondary_lists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GameObject current = enumerator.Current;
              GameObject materialObject = (GameObject) null;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
              {
                materialObject = UnityEngine.Object.Instantiate((UnityEngine.Object) current, current.get_transform().get_position(), current.get_transform().get_rotation()) as GameObject;
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) materialObject))
                {
                  materialObject.get_transform().SetParent(childRecursively, false);
                  materialObject.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
                  materialObject.get_gameObject().SetActive(false);
                  this.SetMaterialByGameObject(materialObject);
                }
              }
              this.mSubSecondaryEquipmentChangeLists.Add(materialObject);
            }
          }
        }
      }
      this.mUseSubEquipment = true;
      this.SwitchEquipments();
    }

    private void SetMaterialByGameObject(GameObject materialObject)
    {
      this.ControlMaterialByGameObject(true, materialObject);
    }

    private void RemoveMaterialByGameObject(GameObject materialObject)
    {
      this.ControlMaterialByGameObject(false, materialObject);
    }

    private void ControlMaterialByGameObject(bool isSet, GameObject materialObject)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) materialObject, (UnityEngine.Object) null))
        return;
      Renderer[] componentsInChildren = (Renderer[]) materialObject.GetComponentsInChildren<Renderer>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
      {
        Material material = componentsInChildren[index].get_material();
        if (!string.IsNullOrEmpty(material.GetTag("Character", false)))
        {
          if (isSet)
            this.CharacterMaterials.Add(material);
          else
            this.CharacterMaterials.Remove(material);
        }
      }
    }

    public void SwitchEquipments()
    {
      GameObject primaryEquipment = this.mPrimaryEquipment;
      this.mPrimaryEquipment = this.mSubPrimaryEquipment;
      this.mSubPrimaryEquipment = primaryEquipment;
      GameObject secondaryEquipment = this.mSecondaryEquipment;
      this.mSecondaryEquipment = this.mSubSecondaryEquipment;
      this.mSubSecondaryEquipment = secondaryEquipment;
      GameObject equipmentDefault1 = this.mPrimaryEquipmentDefault;
      this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
      this.mSubPrimaryEquipmentDefault = equipmentDefault1;
      List<GameObject> equipmentChangeLists1 = this.mPrimaryEquipmentChangeLists;
      this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
      this.mSubPrimaryEquipmentChangeLists = equipmentChangeLists1;
      GameObject equipmentDefault2 = this.mSecondaryEquipmentDefault;
      this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
      this.mSubSecondaryEquipmentDefault = equipmentDefault2;
      List<GameObject> equipmentChangeLists2 = this.mSecondaryEquipmentChangeLists;
      this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
      this.mSubSecondaryEquipmentChangeLists = equipmentChangeLists2;
    }

    public void HideEquipments()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
        GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerHidden, true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
        return;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerHidden, true);
    }

    public void ResetSubEquipments()
    {
      this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
      this.mSubPrimaryEquipmentDefault = (GameObject) null;
      this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
      this.mSubSecondaryEquipmentDefault = (GameObject) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrimaryEquipment, (UnityEngine.Object) null))
      {
        this.RemoveMaterialByGameObject(this.mPrimaryEquipment);
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mPrimaryEquipment.get_gameObject());
      }
      this.mPrimaryEquipment = this.mSubPrimaryEquipment;
      this.mSubPrimaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerCH, true);
      using (List<GameObject>.Enumerator enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
          {
            this.RemoveMaterialByGameObject(current);
            UnityEngine.Object.Destroy((UnityEngine.Object) current.get_gameObject());
          }
        }
      }
      this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
      this.mSubPrimaryEquipmentChangeLists = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondaryEquipment, (UnityEngine.Object) null))
      {
        this.RemoveMaterialByGameObject(this.mSecondaryEquipment);
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSecondaryEquipment.get_gameObject());
      }
      this.mSecondaryEquipment = this.mSubSecondaryEquipment;
      this.mSubSecondaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerCH, true);
      using (List<GameObject>.Enumerator enumerator = this.mSecondaryEquipmentChangeLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current))
          {
            this.RemoveMaterialByGameObject(current);
            UnityEngine.Object.Destroy((UnityEngine.Object) current.get_gameObject());
          }
        }
      }
      this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
      this.mSubSecondaryEquipmentChangeLists = new List<GameObject>();
      this.mUseSubEquipment = false;
    }

    private void SetVesselStrength(float strength)
    {
      this.mVesselStrength = strength;
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        Material characterMaterial = this.CharacterMaterials[index];
        if (!string.IsNullOrEmpty(characterMaterial.GetTag("Bloom", false, (string) null)))
        {
          float num = 0.003921569f;
          characterMaterial.EnableKeyword("MODE_BLOOM");
          characterMaterial.DisableKeyword("MODE_DEFAULT");
          characterMaterial.SetVector("_glowColor", new Vector4((float) this.VesselColor.r * num, (float) this.VesselColor.g * num, (float) this.VesselColor.b * num, Mathf.Pow(strength, 0.7f)));
          characterMaterial.SetFloat("_colorMultipler", (float) (1.0 - (double) strength * 0.400000005960464));
          characterMaterial.SetFloat("_glowStrength", Mathf.Pow(strength, 1.5f) * GameSettings.Instance.Unit_MaxGlowStrength);
        }
        else
        {
          characterMaterial.EnableKeyword("MODE_DEFAULT");
          characterMaterial.DisableKeyword("MODE_BLOOM");
          characterMaterial.SetFloat("_colorMultipler", 1f);
        }
      }
    }

    public void AnimateVessel(float desiredStrength, float duration)
    {
      if ((double) duration <= 0.0)
      {
        this.SetVesselStrength(desiredStrength);
        this.mVesselAnimTime = 0.0f;
        this.mVesselAnimDuration = 0.0f;
      }
      else
      {
        this.mVesselAnimStart = this.mVesselStrength;
        this.mVesselAnimEnd = desiredStrength;
        this.mVesselAnimTime = 0.0f;
        this.mVesselAnimDuration = duration;
      }
    }

    private void UpdateVesselAnimation()
    {
      this.mVesselAnimTime += Time.get_deltaTime();
      this.SetVesselStrength(Mathf.Lerp(this.mVesselAnimStart, this.mVesselAnimEnd, Mathf.Clamp01(this.mVesselAnimTime / this.mVesselAnimDuration)));
    }

    public enum EquipmentType
    {
      PRIMARY,
      SECONDARY,
    }
  }
}
