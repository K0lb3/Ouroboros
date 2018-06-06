// Decompiled with JetBrains decompiler
// Type: SRPG.UnitController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public abstract class UnitController : AnimationPlayer
  {
    public Color32 VesselColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    protected List<Material> CharacterMaterials = new List<Material>(4);
    public const string CharacterAnimationDir = "CHM/";
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
    private bool mUseSubEquipment;
    private GameObject mSubPrimaryEquipment;
    private GameObject mSubSecondaryEquipment;
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
        if (Object.op_Inequality((Object) this.mCharacterSettings, (Object) null))
          return this.mCharacterSettings.Rig;
        return (RigSetup) null;
      }
    }

    public float Height
    {
      get
      {
        RigSetup rig = this.Rig;
        if (Object.op_Inequality((Object) rig, (Object) null))
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

    protected override void Start()
    {
      base.Start();
      this.AddLoadThreadCount();
      this.StartCoroutine(this.AsyncSetup());
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
      if (Object.op_Equality((Object) Root, (Object) null) || Dest == null || 0 >= Root.get_childCount())
        return;
      for (int index = 0; index < Root.get_childCount(); ++index)
      {
        Transform child = Root.GetChild(index);
        Dest.Add(child);
        this.createRootBoneList(child, ref Dest);
      }
    }

    private void UpdateFaceAnimation()
    {
      if (Object.op_Equality((Object) this.mFaceAnimation, (Object) null))
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
      if (!Object.op_Inequality((Object) activeAnimation, (Object) null))
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
      if (Object.op_Equality((Object) this.mCharacterSettings, (Object) null))
        return ((Component) this).get_transform();
      Transform childRecursively = GameUtility.findChildRecursively(((Component) this).get_transform(), this.mCharacterSettings.Rig.RootBoneName);
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
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

    [DebuggerHidden]
    private IEnumerator AsyncSetup()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitController.\u003CAsyncSetup\u003Ec__IteratorE() { \u003C\u003Ef__this = this };
    }

    private void FindCharacterMaterials()
    {
      this.CharacterMaterials.Clear();
      Renderer[] componentsInChildren = (Renderer[]) ((Component) this).GetComponentsInChildren<Renderer>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
      {
        Material material = componentsInChildren[index].get_material();
        if (!string.IsNullOrEmpty(material.GetTag("Character", false)) || !string.IsNullOrEmpty(material.GetTag("CharacterSimple", false)))
          this.CharacterMaterials.Add(material);
      }
    }

    protected virtual void PostSetup()
    {
    }

    public void SetEquipmentsVisible(bool visible)
    {
      if (Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
        this.mPrimaryEquipment.get_gameObject().SetActive(visible);
      if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
        return;
      this.mSecondaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SetPrimaryEquipmentsVisible(bool visible)
    {
      if (!Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
        return;
      this.mPrimaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SetSecondaryEquipmentsVisible(bool visible)
    {
      if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
        return;
      this.mSecondaryEquipment.get_gameObject().SetActive(visible);
    }

    public void SetSubEquipment(GameObject primaryHand, GameObject secondaryHand, bool hidden = false)
    {
      if (Object.op_Inequality((Object) primaryHand, (Object) null) && !string.IsNullOrEmpty(this.Rig.RightHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
        if (Object.op_Inequality((Object) childRecursively, (Object) null))
        {
          this.mSubPrimaryEquipment = Object.Instantiate((Object) primaryHand, primaryHand.get_transform().get_position(), primaryHand.get_transform().get_rotation()) as GameObject;
          this.mSubPrimaryEquipment.get_transform().SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubPrimaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubPrimaryEquipment, GameUtility.LayerHidden, true);
          this.mSubPrimaryEquipment.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.LeftHand + " not found."));
      }
      if (Object.op_Inequality((Object) secondaryHand, (Object) null) && !string.IsNullOrEmpty(this.Rig.LeftHand))
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
        if (Object.op_Inequality((Object) childRecursively, (Object) null))
        {
          this.mSubSecondaryEquipment = Object.Instantiate((Object) secondaryHand, secondaryHand.get_transform().get_position(), secondaryHand.get_transform().get_rotation()) as GameObject;
          this.mSubSecondaryEquipment.get_transform().SetParent(childRecursively, false);
          this.SetMaterialByGameObject(this.mSubSecondaryEquipment);
          if (hidden)
            GameUtility.SetLayer(this.mSubSecondaryEquipment, GameUtility.LayerHidden, true);
          this.mSubSecondaryEquipment.get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.Rig.EquipmentScale));
        }
        else
          Debug.LogError((object) ("Node " + this.Rig.LeftHand + " not found."));
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
      if (Object.op_Equality((Object) materialObject, (Object) null))
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
    }

    public void HideEquipments()
    {
      if (Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
        GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerHidden, true);
      if (!Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
        return;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerHidden, true);
    }

    public void ResetSubEquipments()
    {
      if (Object.op_Inequality((Object) this.mPrimaryEquipment, (Object) null))
      {
        this.RemoveMaterialByGameObject(this.mPrimaryEquipment);
        Object.Destroy((Object) this.mPrimaryEquipment.get_gameObject());
      }
      this.mPrimaryEquipment = this.mSubPrimaryEquipment;
      this.mSubPrimaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerCH, true);
      if (Object.op_Inequality((Object) this.mSecondaryEquipment, (Object) null))
      {
        this.RemoveMaterialByGameObject(this.mSecondaryEquipment);
        Object.Destroy((Object) this.mSecondaryEquipment.get_gameObject());
      }
      this.mSecondaryEquipment = this.mSubSecondaryEquipment;
      this.mSubSecondaryEquipment = (GameObject) null;
      GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerCH, true);
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
  }
}
