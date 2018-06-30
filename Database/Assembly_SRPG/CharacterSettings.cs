namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class CharacterSettings : MonoBehaviour
    {
        public RigSetup Rig;
        public string DefaultSkeleton;
        public Projector ShadowProjector;
        public Color32 GlowColor;
        private BoneStateCache[] mBoneStates;
        private RigSetup.SkeletonInfo mActiveSkeleton;

        public CharacterSettings()
        {
            this.GlowColor = new Color32(0xff, 0xff, 0xff, 0xff);
            base..ctor();
            return;
        }

        private unsafe void AdjustBones()
        {
            int num;
            if (this.mBoneStates != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_00F8;
        Label_0013:
            if ((&(this.mBoneStates[num]).transform != null) == null)
            {
                goto Label_00F4;
            }
            if ((&(this.mBoneStates[num]).transform.get_localScale() != &(this.mBoneStates[num]).localScale) == null)
            {
                goto Label_008C;
            }
            &(this.mBoneStates[num]).transform.set_localScale(&(this.mBoneStates[num]).boneInfo.scale);
        Label_008C:
            if ((&(this.mBoneStates[num]).transform.get_localPosition() != &(this.mBoneStates[num]).localPosition) == null)
            {
                goto Label_00F4;
            }
            &(this.mBoneStates[num]).transform.set_localPosition(&(this.mBoneStates[num]).transform.get_localPosition() + &(this.mBoneStates[num]).boneInfo.offset);
        Label_00F4:
            num += 1;
        Label_00F8:
            if (num < ((int) this.mBoneStates.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void Awake()
        {
            base.set_enabled(0);
            if (string.IsNullOrEmpty(this.DefaultSkeleton) != null)
            {
                goto Label_0023;
            }
            this.SetSkeleton(this.DefaultSkeleton);
        Label_0023:
            return;
        }

        private unsafe void CacheBoneStates()
        {
            int num;
            if (this.mBoneStates != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_0081;
        Label_0013:
            if ((&(this.mBoneStates[num]).transform != null) == null)
            {
                goto Label_007D;
            }
            &(this.mBoneStates[num]).localScale = &(this.mBoneStates[num]).transform.get_localScale();
            &(this.mBoneStates[num]).localPosition = &(this.mBoneStates[num]).transform.get_localPosition();
        Label_007D:
            num += 1;
        Label_0081:
            if (num < ((int) this.mBoneStates.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void LateUpdate()
        {
            this.AdjustBones();
            this.CacheBoneStates();
            return;
        }

        public unsafe void SetSkeleton(string rigName)
        {
            int num;
            int num2;
            base.set_enabled(0);
            if ((this.Rig == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            this.mActiveSkeleton = null;
            num = 0;
            goto Label_0060;
        Label_0027:
            if ((this.Rig.Skeletons[num].name == rigName) == null)
            {
                goto Label_005C;
            }
            this.mActiveSkeleton = this.Rig.Skeletons[num];
            goto Label_0073;
        Label_005C:
            num += 1;
        Label_0060:
            if (num < ((int) this.Rig.Skeletons.Length))
            {
                goto Label_0027;
            }
        Label_0073:
            if (this.mActiveSkeleton == null)
            {
                goto Label_018C;
            }
            this.mBoneStates = new BoneStateCache[(int) this.mActiveSkeleton.bones.Length];
            num2 = 0;
            goto Label_016C;
        Label_009D:
            &(this.mBoneStates[num2]).boneInfo = this.mActiveSkeleton.bones[num2];
            &(this.mBoneStates[num2]).transform = GameUtility.findChildRecursively(base.get_transform(), this.mActiveSkeleton.bones[num2].name);
            if ((&(this.mBoneStates[num2]).transform != null) == null)
            {
                goto Label_0168;
            }
            &(this.mBoneStates[num2]).transform.set_localScale(&(this.mBoneStates[num2]).boneInfo.scale);
            &(this.mBoneStates[num2]).transform.set_localPosition(&(this.mBoneStates[num2]).transform.get_localPosition() + &(this.mBoneStates[num2]).boneInfo.offset);
        Label_0168:
            num2 += 1;
        Label_016C:
            if (num2 < ((int) this.mActiveSkeleton.bones.Length))
            {
                goto Label_009D;
            }
            this.CacheBoneStates();
            base.set_enabled(1);
        Label_018C:
            return;
        }

        private void Update()
        {
            this.CacheBoneStates();
            return;
        }

        public float Height
        {
            get
            {
                if ((this.Rig != null) == null)
                {
                    goto Label_001D;
                }
                return this.Rig.Height;
            Label_001D:
                return 1f;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BoneStateCache
        {
            public RigSetup.BoneInfo boneInfo;
            public Transform transform;
            public Vector3 localPosition;
            public Vector3 localScale;
        }
    }
}

