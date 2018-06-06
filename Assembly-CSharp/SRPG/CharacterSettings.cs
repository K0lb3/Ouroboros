// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CharacterSettings : MonoBehaviour
  {
    public RigSetup Rig;
    public string DefaultSkeleton;
    public Projector ShadowProjector;
    public Color32 GlowColor;
    private CharacterSettings.BoneStateCache[] mBoneStates;
    private RigSetup.SkeletonInfo mActiveSkeleton;

    public CharacterSettings()
    {
      base.\u002Ector();
    }

    public float Height
    {
      get
      {
        if (Object.op_Inequality((Object) this.Rig, (Object) null))
          return this.Rig.Height;
        return 1f;
      }
    }

    private void Awake()
    {
      ((Behaviour) this).set_enabled(false);
      if (string.IsNullOrEmpty(this.DefaultSkeleton))
        return;
      this.SetSkeleton(this.DefaultSkeleton);
    }

    public void SetSkeleton(string rigName)
    {
      ((Behaviour) this).set_enabled(false);
      if (Object.op_Equality((Object) this.Rig, (Object) null))
        return;
      this.mActiveSkeleton = (RigSetup.SkeletonInfo) null;
      for (int index = 0; index < this.Rig.Skeletons.Length; ++index)
      {
        if (this.Rig.Skeletons[index].name == rigName)
        {
          this.mActiveSkeleton = this.Rig.Skeletons[index];
          break;
        }
      }
      if (this.mActiveSkeleton == null)
        return;
      this.mBoneStates = new CharacterSettings.BoneStateCache[this.mActiveSkeleton.bones.Length];
      for (int index = 0; index < this.mActiveSkeleton.bones.Length; ++index)
      {
        this.mBoneStates[index].boneInfo = this.mActiveSkeleton.bones[index];
        this.mBoneStates[index].transform = GameUtility.findChildRecursively(((Component) this).get_transform(), this.mActiveSkeleton.bones[index].name);
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
        {
          this.mBoneStates[index].transform.set_localScale(this.mBoneStates[index].boneInfo.scale);
          Transform transform = this.mBoneStates[index].transform;
          transform.set_localPosition(Vector3.op_Addition(transform.get_localPosition(), this.mBoneStates[index].boneInfo.offset));
        }
      }
      this.CacheBoneStates();
      ((Behaviour) this).set_enabled(true);
    }

    private void CacheBoneStates()
    {
      if (this.mBoneStates == null)
        return;
      for (int index = 0; index < this.mBoneStates.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
        {
          this.mBoneStates[index].localScale = this.mBoneStates[index].transform.get_localScale();
          this.mBoneStates[index].localPosition = this.mBoneStates[index].transform.get_localPosition();
        }
      }
    }

    private void AdjustBones()
    {
      if (this.mBoneStates == null)
        return;
      for (int index = 0; index < this.mBoneStates.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mBoneStates[index].transform, (Object) null))
        {
          if (Vector3.op_Inequality(this.mBoneStates[index].transform.get_localScale(), this.mBoneStates[index].localScale))
            this.mBoneStates[index].transform.set_localScale(this.mBoneStates[index].boneInfo.scale);
          if (Vector3.op_Inequality(this.mBoneStates[index].transform.get_localPosition(), this.mBoneStates[index].localPosition))
          {
            Transform transform = this.mBoneStates[index].transform;
            transform.set_localPosition(Vector3.op_Addition(transform.get_localPosition(), this.mBoneStates[index].boneInfo.offset));
          }
        }
      }
    }

    private void Update()
    {
      this.CacheBoneStates();
    }

    private void LateUpdate()
    {
      this.AdjustBones();
      this.CacheBoneStates();
    }

    private struct BoneStateCache
    {
      public RigSetup.BoneInfo boneInfo;
      public Transform transform;
      public Vector3 localPosition;
      public Vector3 localScale;
    }
  }
}
