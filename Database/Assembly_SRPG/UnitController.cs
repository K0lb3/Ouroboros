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

    public abstract class UnitController : AnimationPlayer
    {
        public const string CharacterAnimationDir = "CHM/";
        public const string COLLABO_SKILL_ASSET_PREFIX = "D";
        private SRPG.UnitData mUnitData;
        private string mCharacterID;
        private string mJobID;
        private string mJobResourceID;
        protected CharacterDB.Job mCharacterData;
        private bool mSetupStarted;
        private int mNumLoadRequests;
        private GameObject mUnitObject;
        protected CharacterSettings mCharacterSettings;
        public Color32 VesselColor;
        private GameObject mPrimaryEquipment;
        private GameObject mSecondaryEquipment;
        private List<GameObject> mPrimaryEquipmentChangeLists;
        private List<GameObject> mSecondaryEquipmentChangeLists;
        private GameObject mPrimaryEquipmentDefault;
        private GameObject mSecondaryEquipmentDefault;
        private bool mUseSubEquipment;
        private GameObject mSubPrimaryEquipment;
        private GameObject mSubSecondaryEquipment;
        private List<GameObject> mSubPrimaryEquipmentChangeLists;
        private List<GameObject> mSubSecondaryEquipmentChangeLists;
        private GameObject mSubPrimaryEquipmentDefault;
        private GameObject mSubSecondaryEquipmentDefault;
        private FaceAnimation mFaceAnimation;
        private bool mPlayingFaceAnimation;
        public bool LoadEquipments;
        public bool KeepUnitHidden;
        protected List<Material> CharacterMaterials;
        protected List<CharacterDB.Job> mCharacterDataLists;
        protected List<GameObject> mUnitObjectLists;
        protected List<CharacterSettings> mCharacterSettingsLists;
        protected List<FaceAnimation> mFaceAnimationLists;
        private float mVesselStrength;
        private float mVesselAnimTime;
        private float mVesselAnimDuration;
        private float mVesselAnimStart;
        private float mVesselAnimEnd;

        protected UnitController()
        {
            this.VesselColor = new Color32(0xff, 0, 0, 0xff);
            this.mPrimaryEquipmentChangeLists = new List<GameObject>();
            this.mSecondaryEquipmentChangeLists = new List<GameObject>();
            this.mSubPrimaryEquipmentChangeLists = new List<GameObject>();
            this.mSubSecondaryEquipmentChangeLists = new List<GameObject>();
            this.CharacterMaterials = new List<Material>(4);
            this.mCharacterDataLists = new List<CharacterDB.Job>();
            this.mUnitObjectLists = new List<GameObject>();
            this.mCharacterSettingsLists = new List<CharacterSettings>();
            this.mFaceAnimationLists = new List<FaceAnimation>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <Start>m__CC(JobData j)
        {
            return (j.Param.iname == this.mJobID);
        }

        protected void AddLoadThreadCount()
        {
            this.mNumLoadRequests += 1;
            return;
        }

        public void AnimateVessel(float desiredStrength, float duration)
        {
            if (duration > 0f)
            {
                goto Label_002D;
            }
            this.SetVesselStrength(desiredStrength);
            this.mVesselAnimTime = 0f;
            this.mVesselAnimDuration = 0f;
            goto Label_0052;
        Label_002D:
            this.mVesselAnimStart = this.mVesselStrength;
            this.mVesselAnimEnd = desiredStrength;
            this.mVesselAnimTime = 0f;
            this.mVesselAnimDuration = duration;
        Label_0052:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncSetup(CharacterDB.Character ch, int jobIndex)
        {
            <AsyncSetup>c__Iterator4E iteratore;
            iteratore = new <AsyncSetup>c__Iterator4E();
            iteratore.ch = ch;
            iteratore.jobIndex = jobIndex;
            iteratore.<$>ch = ch;
            iteratore.<$>jobIndex = jobIndex;
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void ControlMaterialByGameObject(bool isSet, GameObject materialObject)
        {
            Renderer[] rendererArray;
            int num;
            Material material;
            if ((materialObject == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            rendererArray = materialObject.GetComponentsInChildren<Renderer>(1);
            num = ((int) rendererArray.Length) - 1;
            goto Label_0067;
        Label_0020:
            material = rendererArray[num].get_material();
            if (string.IsNullOrEmpty(material.GetTag("Character", 0)) != null)
            {
                goto Label_0063;
            }
            if (isSet == null)
            {
                goto Label_0056;
            }
            this.CharacterMaterials.Add(material);
            goto Label_0063;
        Label_0056:
            this.CharacterMaterials.Remove(material);
        Label_0063:
            num -= 1;
        Label_0067:
            if (num >= 0)
            {
                goto Label_0020;
            }
            return;
        }

        private void createRootBoneList(Transform Root, ref List<Transform> Dest)
        {
            int num;
            Transform transform;
            if ((Root == null) != null)
            {
                goto Label_0013;
            }
            if (*(Dest) != null)
            {
                goto Label_0014;
            }
        Label_0013:
            return;
        Label_0014:
            if (0 < Root.get_childCount())
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            num = 0;
            goto Label_0044;
        Label_0028:
            transform = Root.GetChild(num);
            *(Dest).Add(transform);
            this.createRootBoneList(transform, Dest);
            num += 1;
        Label_0044:
            if (num < Root.get_childCount())
            {
                goto Label_0028;
            }
            return;
        }

        private void FindCharacterMaterials()
        {
            Renderer[] rendererArray;
            int num;
            Material[] materialArray;
            int num2;
            Material material;
            this.CharacterMaterials.Clear();
            rendererArray = base.GetComponentsInChildren<Renderer>(1);
            num = ((int) rendererArray.Length) - 1;
            goto Label_008A;
        Label_001E:
            materialArray = rendererArray[num].get_materials();
            if (materialArray == null)
            {
                goto Label_0086;
            }
            num2 = 0;
            goto Label_007D;
        Label_0034:
            material = rendererArray[num].get_material();
            if (string.IsNullOrEmpty(material.GetTag("Character", 0)) == null)
            {
                goto Label_006C;
            }
            if (string.IsNullOrEmpty(material.GetTag("CharacterSimple", 0)) != null)
            {
                goto Label_0079;
            }
        Label_006C:
            this.CharacterMaterials.Add(material);
        Label_0079:
            num2 += 1;
        Label_007D:
            if (num2 < ((int) materialArray.Length))
            {
                goto Label_0034;
            }
        Label_0086:
            num -= 1;
        Label_008A:
            if (num >= 0)
            {
                goto Label_001E;
            }
            return;
        }

        protected Transform GetCharacterRoot()
        {
            Transform transform;
            if (((this.mCharacterSettings == null) == null) && ((this.mCharacterSettings.Rig == null) == null))
            {
                goto Label_002E;
            }
            return base.get_transform();
        Label_002E:
            transform = GameUtility.findChildRecursively(base.get_transform(), this.mCharacterSettings.Rig.RootBoneName);
            return (((transform != null) == null) ? base.get_transform() : transform);
        }

        public void HideEquipments()
        {
            if ((this.mPrimaryEquipment != null) == null)
            {
                goto Label_0022;
            }
            GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerHidden, 1);
        Label_0022:
            if ((this.mSecondaryEquipment != null) == null)
            {
                goto Label_0044;
            }
            GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerHidden, 1);
        Label_0044:
            return;
        }

        public bool IsVisible()
        {
            int num;
            return ((base.get_gameObject().get_layer() == GameUtility.LayerHidden) == 0);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            this.UpdateFaceAnimation();
            if (this.mVesselAnimTime >= this.mVesselAnimDuration)
            {
                goto Label_0023;
            }
            this.UpdateVesselAnimation();
        Label_0023:
            return;
        }

        public bool LoadAddModels(int job_index)
        {
            CharacterDB.Character character;
            if (this.mNumLoadRequests <= 0)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            character = CharacterDB.FindCharacter(this.mCharacterID);
            if (character != null)
            {
                goto Label_003C;
            }
            Debug.LogError("Unknown character '" + this.mCharacterID + "'");
            return 0;
        Label_003C:
            if (job_index < 0)
            {
                goto Label_0054;
            }
            if (job_index < character.Jobs.Count)
            {
                goto Label_0056;
            }
        Label_0054:
            return 0;
        Label_0056:
            base.StartCoroutine(this.AsyncSetup(character, job_index));
            return 1;
        }

        private LoadRequest LoadResource<T>(string path)
        {
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return AssetManager.LoadAsync(path, typeof(T));
        }

        public void LoadUnitAnimationAsync(string id, string animationName, bool addJobName, bool is_collabo_skill)
        {
            string[] textArray1;
            string str;
            str = this.mCharacterData.AssetPrefix;
            if (is_collabo_skill == null)
            {
                goto Label_0019;
            }
            str = "D";
        Label_0019:
            if (addJobName == null)
            {
                goto Label_005F;
            }
            textArray1 = new string[] { "CHM/", str, "_", this.mJobResourceID, "_", animationName };
            base.LoadAnimationAsync(id, string.Concat(textArray1));
            goto Label_0077;
        Label_005F:
            base.LoadAnimationAsync(id, "CHM/" + str + "_" + animationName);
        Label_0077:
            return;
        }

        protected override unsafe void OnDestroy()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            base.OnDestroy();
            enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator();
        Label_0012:
            try
            {
                goto Label_003A;
            Label_0017:
                obj2 = &enumerator.Current;
                if (obj2 != null)
                {
                    goto Label_002F;
                }
                goto Label_003A;
            Label_002F:
                Object.Destroy(obj2.get_gameObject());
            Label_003A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0017;
                }
                goto Label_0057;
            }
            finally
            {
            Label_004B:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0057:
            this.mPrimaryEquipmentChangeLists = new List<GameObject>();
            enumerator2 = this.mSecondaryEquipmentChangeLists.GetEnumerator();
        Label_006E:
            try
            {
                goto Label_0096;
            Label_0073:
                obj3 = &enumerator2.Current;
                if (obj3 != null)
                {
                    goto Label_008B;
                }
                goto Label_0096;
            Label_008B:
                Object.Destroy(obj3.get_gameObject());
            Label_0096:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0073;
                }
                goto Label_00B3;
            }
            finally
            {
            Label_00A7:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_00B3:
            this.mSecondaryEquipmentChangeLists = new List<GameObject>();
            return;
        }

        protected virtual void OnVisibilityChange(bool visible)
        {
        }

        protected virtual void PostSetup()
        {
        }

        protected void RemoveLoadThreadCount()
        {
            this.mNumLoadRequests -= 1;
            if (this.mNumLoadRequests >= 0)
            {
                goto Label_002B;
            }
            Debug.LogError("Invalid load request count");
            this.mNumLoadRequests = 0;
        Label_002B:
            return;
        }

        private void RemoveMaterialByGameObject(GameObject materialObject)
        {
            this.ControlMaterialByGameObject(0, materialObject);
            return;
        }

        public unsafe void ResetAttachmentLists(EquipmentType type)
        {
            Transform transform;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            Transform transform2;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            EquipmentType type2;
            type2 = type;
            if (type2 == null)
            {
                goto Label_0017;
            }
            if (type2 == 1)
            {
                goto Label_00A4;
            }
            goto Label_0135;
        Label_0017:
            if (string.IsNullOrEmpty(this.Rig.RightHand) != null)
            {
                goto Label_0135;
            }
            transform = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
            if ((transform != null) == null)
            {
                goto Label_0135;
            }
            this.SetAttachmentParent(this.mPrimaryEquipmentDefault, transform);
            enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator();
        Label_006D:
            try
            {
                goto Label_0082;
            Label_0072:
                obj2 = &enumerator.Current;
                this.SetAttachmentParent(obj2, transform);
            Label_0082:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0072;
                }
                goto Label_009F;
            }
            finally
            {
            Label_0093:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_009F:
            goto Label_0135;
        Label_00A4:
            if (string.IsNullOrEmpty(this.Rig.LeftHand) != null)
            {
                goto Label_0135;
            }
            transform2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
            if ((transform2 != null) == null)
            {
                goto Label_0135;
            }
            this.SetAttachmentParent(this.mSecondaryEquipmentDefault, transform2);
            enumerator2 = this.mSecondaryEquipmentChangeLists.GetEnumerator();
        Label_00FB:
            try
            {
                goto Label_0112;
            Label_0100:
                obj3 = &enumerator2.Current;
                this.SetAttachmentParent(obj3, transform2);
            Label_0112:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0100;
                }
                goto Label_0130;
            }
            finally
            {
            Label_0123:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_0130:;
        Label_0135:
            return;
        }

        public void ResetEquipmentLists(EquipmentType type)
        {
            bool flag;
            bool flag2;
            EquipmentType type2;
            type2 = type;
            if (type2 == null)
            {
                goto Label_0014;
            }
            if (type2 == 1)
            {
                goto Label_0095;
            }
            goto Label_0116;
        Label_0014:
            if ((this.mPrimaryEquipment != this.mPrimaryEquipmentDefault) == null)
            {
                goto Label_0116;
            }
            flag = (this.mPrimaryEquipment == null) ? 1 : this.mPrimaryEquipment.get_activeSelf();
            if (this.mPrimaryEquipment == null)
            {
                goto Label_0068;
            }
            this.mPrimaryEquipment.SetActive(0);
        Label_0068:
            this.mPrimaryEquipment = this.mPrimaryEquipmentDefault;
            if (this.mPrimaryEquipment == null)
            {
                goto Label_0116;
            }
            this.mPrimaryEquipment.SetActive(flag);
            goto Label_0116;
        Label_0095:
            if ((this.mSecondaryEquipment != this.mSecondaryEquipmentDefault) == null)
            {
                goto Label_0116;
            }
            flag2 = (this.mSecondaryEquipment == null) ? 1 : this.mSecondaryEquipment.get_activeSelf();
            if (this.mSecondaryEquipment == null)
            {
                goto Label_00E9;
            }
            this.mSecondaryEquipment.SetActive(0);
        Label_00E9:
            this.mSecondaryEquipment = this.mSecondaryEquipmentDefault;
            if (this.mSecondaryEquipment == null)
            {
                goto Label_0116;
            }
            this.mSecondaryEquipment.SetActive(flag2);
        Label_0116:
            return;
        }

        public unsafe void ResetSubEquipments()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
            this.mSubPrimaryEquipmentDefault = null;
            this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
            this.mSubSecondaryEquipmentDefault = null;
            if ((this.mPrimaryEquipment != null) == null)
            {
                goto Label_0053;
            }
            this.RemoveMaterialByGameObject(this.mPrimaryEquipment);
            Object.Destroy(this.mPrimaryEquipment.get_gameObject());
        Label_0053:
            this.mPrimaryEquipment = this.mSubPrimaryEquipment;
            this.mSubPrimaryEquipment = null;
            GameUtility.SetLayer(this.mPrimaryEquipment, GameUtility.LayerCH, 1);
            enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator();
        Label_0083:
            try
            {
                goto Label_00B2;
            Label_0088:
                obj2 = &enumerator.Current;
                if (obj2 != null)
                {
                    goto Label_00A0;
                }
                goto Label_00B2;
            Label_00A0:
                this.RemoveMaterialByGameObject(obj2);
                Object.Destroy(obj2.get_gameObject());
            Label_00B2:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0088;
                }
                goto Label_00CF;
            }
            finally
            {
            Label_00C3:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00CF:
            this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
            this.mSubPrimaryEquipmentChangeLists = new List<GameObject>();
            if ((this.mSecondaryEquipment != null) == null)
            {
                goto Label_0113;
            }
            this.RemoveMaterialByGameObject(this.mSecondaryEquipment);
            Object.Destroy(this.mSecondaryEquipment.get_gameObject());
        Label_0113:
            this.mSecondaryEquipment = this.mSubSecondaryEquipment;
            this.mSubSecondaryEquipment = null;
            GameUtility.SetLayer(this.mSecondaryEquipment, GameUtility.LayerCH, 1);
            enumerator2 = this.mSecondaryEquipmentChangeLists.GetEnumerator();
        Label_0143:
            try
            {
                goto Label_0172;
            Label_0148:
                obj3 = &enumerator2.Current;
                if (obj3 != null)
                {
                    goto Label_0160;
                }
                goto Label_0172;
            Label_0160:
                this.RemoveMaterialByGameObject(obj3);
                Object.Destroy(obj3.get_gameObject());
            Label_0172:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0148;
                }
                goto Label_018F;
            }
            finally
            {
            Label_0183:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_018F:
            this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
            this.mSubSecondaryEquipmentChangeLists = new List<GameObject>();
            this.mUseSubEquipment = 0;
            return;
        }

        public unsafe bool SetActivateUnitObject(int idx)
        {
            int num;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            if (idx < 0)
            {
                goto Label_0018;
            }
            if (idx < this.mUnitObjectLists.Count)
            {
                goto Label_001A;
            }
        Label_0018:
            return 0;
        Label_001A:
            num = 0;
            goto Label_0046;
        Label_0021:
            if ((this.mUnitObject == this.mUnitObjectLists[num]) == null)
            {
                goto Label_0042;
            }
            goto Label_0057;
        Label_0042:
            num += 1;
        Label_0046:
            if (num < this.mUnitObjectLists.Count)
            {
                goto Label_0021;
            }
        Label_0057:
            enumerator = this.mUnitObjectLists.GetEnumerator();
        Label_0063:
            try
            {
                goto Label_0077;
            Label_0068:
                obj2 = &enumerator.Current;
                obj2.SetActive(0);
            Label_0077:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0068;
                }
                goto Label_0094;
            }
            finally
            {
            Label_0088:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0094:
            this.mUnitObject = this.mUnitObjectLists[idx];
            this.mUnitObject.SetActive(1);
            base.SetAnimationComponent(this.mUnitObject.GetComponent<Animation>());
            if (idx >= this.mCharacterDataLists.Count)
            {
                goto Label_00E6;
            }
            this.mCharacterData = this.mCharacterDataLists[idx];
        Label_00E6:
            if (idx >= this.mCharacterSettingsLists.Count)
            {
                goto Label_0109;
            }
            this.mCharacterSettings = this.mCharacterSettingsLists[idx];
        Label_0109:
            if (idx >= this.mFaceAnimationLists.Count)
            {
                goto Label_012C;
            }
            this.mFaceAnimation = this.mFaceAnimationLists[idx];
        Label_012C:
            return ((idx == num) == 0);
        }

        private void SetAttachmentParent(GameObject go, Transform parent)
        {
            if (go == null)
            {
                goto Label_0016;
            }
            if (parent != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            go.get_transform().SetParent(parent, 0);
            go.get_transform().set_localScale(Vector3.get_one() * this.Rig.EquipmentScale);
            return;
        }

        public void SetEquipmentsVisible(bool visible)
        {
            if ((this.mPrimaryEquipment != null) == null)
            {
                goto Label_0022;
            }
            this.mPrimaryEquipment.get_gameObject().SetActive(visible);
        Label_0022:
            if ((this.mSecondaryEquipment != null) == null)
            {
                goto Label_0044;
            }
            this.mSecondaryEquipment.get_gameObject().SetActive(visible);
        Label_0044:
            return;
        }

        private void SetMaterialByGameObject(GameObject materialObject)
        {
            this.ControlMaterialByGameObject(1, materialObject);
            return;
        }

        public void SetPrimaryEquipmentsVisible(bool visible)
        {
            if ((this.mPrimaryEquipment != null) == null)
            {
                goto Label_0022;
            }
            this.mPrimaryEquipment.get_gameObject().SetActive(visible);
        Label_0022:
            return;
        }

        public void SetSecondaryEquipmentsVisible(bool visible)
        {
            if ((this.mSecondaryEquipment != null) == null)
            {
                goto Label_0022;
            }
            this.mSecondaryEquipment.get_gameObject().SetActive(visible);
        Label_0022:
            return;
        }

        public unsafe void SetSubEquipment(GameObject primaryHand, GameObject secondaryHand, List<GameObject> primary_lists, List<GameObject> secondary_lists, bool hidden)
        {
            Transform transform;
            Transform transform2;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            GameObject obj3;
            Transform transform3;
            Transform transform4;
            GameObject obj4;
            List<GameObject>.Enumerator enumerator2;
            GameObject obj5;
            if ((primaryHand != null) == null)
            {
                goto Label_00EF;
            }
            if (string.IsNullOrEmpty(this.Rig.RightHand) != null)
            {
                goto Label_00EF;
            }
            transform = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
            if ((transform != null) == null)
            {
                goto Label_00D0;
            }
            this.mSubPrimaryEquipment = Object.Instantiate(primaryHand, primaryHand.get_transform().get_position(), primaryHand.get_transform().get_rotation()) as GameObject;
            this.mSubPrimaryEquipment.get_transform().SetParent(transform, 0);
            this.SetMaterialByGameObject(this.mSubPrimaryEquipment);
            if (hidden == null)
            {
                goto Label_00A6;
            }
            GameUtility.SetLayer(this.mSubPrimaryEquipment, GameUtility.LayerHidden, 1);
        Label_00A6:
            this.mSubPrimaryEquipment.get_transform().set_localScale(Vector3.get_one() * this.Rig.EquipmentScale);
            goto Label_00EF;
        Label_00D0:
            Debug.LogError("Node " + this.Rig.RightHand + " not found.");
        Label_00EF:
            if (primary_lists == null)
            {
                goto Label_0207;
            }
            if (string.IsNullOrEmpty(this.Rig.RightHand) != null)
            {
                goto Label_0207;
            }
            transform2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHand);
            if (transform2 == null)
            {
                goto Label_0207;
            }
            this.mSubPrimaryEquipmentDefault = this.mSubPrimaryEquipment;
            this.mSubPrimaryEquipmentChangeLists.Clear();
            enumerator = primary_lists.GetEnumerator();
        Label_014F:
            try
            {
                goto Label_01EA;
            Label_0154:
                obj2 = &enumerator.Current;
                obj3 = null;
                if (obj2 == null)
                {
                    goto Label_01DD;
                }
                obj3 = Object.Instantiate(obj2, obj2.get_transform().get_position(), obj2.get_transform().get_rotation()) as GameObject;
                if (obj3 == null)
                {
                    goto Label_01DD;
                }
                obj3.get_transform().SetParent(transform2, 0);
                obj3.get_transform().set_localScale(Vector3.get_one() * this.Rig.EquipmentScale);
                obj3.get_gameObject().SetActive(0);
                this.SetMaterialByGameObject(obj3);
            Label_01DD:
                this.mSubPrimaryEquipmentChangeLists.Add(obj3);
            Label_01EA:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0154;
                }
                goto Label_0207;
            }
            finally
            {
            Label_01FB:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0207:
            if ((secondaryHand != null) == null)
            {
                goto Label_02F9;
            }
            if (string.IsNullOrEmpty(this.Rig.LeftHand) != null)
            {
                goto Label_02F9;
            }
            transform3 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
            if ((transform3 != null) == null)
            {
                goto Label_02DA;
            }
            this.mSubSecondaryEquipment = Object.Instantiate(secondaryHand, secondaryHand.get_transform().get_position(), secondaryHand.get_transform().get_rotation()) as GameObject;
            this.mSubSecondaryEquipment.get_transform().SetParent(transform3, 0);
            this.SetMaterialByGameObject(this.mSubSecondaryEquipment);
            if (hidden == null)
            {
                goto Label_02B0;
            }
            GameUtility.SetLayer(this.mSubSecondaryEquipment, GameUtility.LayerHidden, 1);
        Label_02B0:
            this.mSubSecondaryEquipment.get_transform().set_localScale(Vector3.get_one() * this.Rig.EquipmentScale);
            goto Label_02F9;
        Label_02DA:
            Debug.LogError("Node " + this.Rig.LeftHand + " not found.");
        Label_02F9:
            if (primary_lists == null)
            {
                goto Label_041C;
            }
            if (string.IsNullOrEmpty(this.Rig.LeftHand) != null)
            {
                goto Label_041C;
            }
            transform4 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHand);
            if (transform4 == null)
            {
                goto Label_041C;
            }
            this.mSubSecondaryEquipmentDefault = this.mSubSecondaryEquipment;
            this.mSubSecondaryEquipmentChangeLists.Clear();
            enumerator2 = secondary_lists.GetEnumerator();
        Label_035D:
            try
            {
                goto Label_03FE;
            Label_0362:
                obj4 = &enumerator2.Current;
                obj5 = null;
                if (obj4 == null)
                {
                    goto Label_03F1;
                }
                obj5 = Object.Instantiate(obj4, obj4.get_transform().get_position(), obj4.get_transform().get_rotation()) as GameObject;
                if (obj5 == null)
                {
                    goto Label_03F1;
                }
                obj5.get_transform().SetParent(transform4, 0);
                obj5.get_transform().set_localScale(Vector3.get_one() * this.Rig.EquipmentScale);
                obj5.get_gameObject().SetActive(0);
                this.SetMaterialByGameObject(obj5);
            Label_03F1:
                this.mSubSecondaryEquipmentChangeLists.Add(obj5);
            Label_03FE:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0362;
                }
                goto Label_041C;
            }
            finally
            {
            Label_040F:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_041C:
            this.mUseSubEquipment = 1;
            this.SwitchEquipments();
            return;
        }

        public virtual void SetupUnit(SRPG.UnitData unitData, int jobIndex)
        {
            this.mUnitData = unitData;
            this.mCharacterID = unitData.UnitParam.model;
            if (jobIndex != -1)
            {
                goto Label_0046;
            }
            this.mJobID = (unitData.CurrentJob == null) ? null : unitData.CurrentJob.JobID;
            goto Label_0059;
        Label_0046:
            this.mJobID = unitData.Jobs[jobIndex].JobID;
        Label_0059:
            return;
        }

        public virtual void SetupUnit(string unitID, string jobID)
        {
            UnitParam param;
            param = MonoSingleton<GameManager>.Instance.GetUnitParam(unitID);
            this.mCharacterID = param.model;
            this.mJobID = jobID;
            return;
        }

        private unsafe void SetVesselStrength(float strength)
        {
            int num;
            Material material;
            float num2;
            this.mVesselStrength = strength;
            num = 0;
            goto Label_00FB;
        Label_000E:
            material = this.CharacterMaterials[num];
            if (string.IsNullOrEmpty(material.GetTag("Bloom", 0, null)) != null)
            {
                goto Label_00D1;
            }
            num2 = 0.003921569f;
            material.EnableKeyword("MODE_BLOOM");
            material.DisableKeyword("MODE_DEFAULT");
            material.SetVector("_glowColor", new Vector4(((float) &this.VesselColor.r) * num2, ((float) &this.VesselColor.g) * num2, ((float) &this.VesselColor.b) * num2, Mathf.Pow(strength, 0.7f)));
            material.SetFloat("_colorMultipler", 1f - (strength * 0.4f));
            material.SetFloat("_glowStrength", Mathf.Pow(strength, 1.5f) * GameSettings.Instance.Unit_MaxGlowStrength);
            goto Label_00F7;
        Label_00D1:
            material.EnableKeyword("MODE_DEFAULT");
            material.DisableKeyword("MODE_BLOOM");
            material.SetFloat("_colorMultipler", 1f);
        Label_00F7:
            num += 1;
        Label_00FB:
            if (num < this.CharacterMaterials.Count)
            {
                goto Label_000E;
            }
            return;
        }

        public void SetVisible(bool visible)
        {
            GameUtility.SetLayer(this, (visible == null) ? GameUtility.LayerHidden : GameUtility.LayerCH, 1);
            this.OnVisibilityChange(visible);
            return;
        }

        protected override void Start()
        {
            CharacterDB.Character character;
            JobParam param;
            string str;
            ArtifactParam param2;
            int num;
            int num2;
            base.Start();
            this.mCharacterDataLists.Clear();
            this.mUnitObjectLists.Clear();
            this.mCharacterSettingsLists.Clear();
            this.mFaceAnimationLists.Clear();
            character = CharacterDB.FindCharacter(this.mCharacterID);
            if (character != null)
            {
                goto Label_0065;
            }
            Debug.LogError("Unknown character '" + this.mCharacterID + "'");
            base.SetLoadError();
            return;
        Label_0065:
            if (string.IsNullOrEmpty(this.mJobID) == null)
            {
                goto Label_0085;
            }
            this.mJobResourceID = "none";
            goto Label_00A2;
        Label_0085:
            param = MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID);
            this.mJobResourceID = param.model;
        Label_00A2:
            str = null;
            param2 = null;
            if ((this.mUnitData == null) || (this.mUnitData.Jobs == null))
            {
                goto Label_00FC;
            }
            param2 = this.mUnitData.GetSelectedSkin(Array.FindIndex<JobData>(this.mUnitData.Jobs, new Predicate<JobData>(this.<Start>m__CC)));
            str = (param2 == null) ? null : param2.asset;
        Label_00FC:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_010E;
            }
            str = this.mJobResourceID;
        Label_010E:
            num = -1;
            num2 = 0;
            goto Label_0145;
        Label_0119:
            if ((character.Jobs[num2].JobID == str) == null)
            {
                goto Label_013F;
            }
            num = num2;
            goto Label_0157;
        Label_013F:
            num2 += 1;
        Label_0145:
            if (num2 < character.Jobs.Count)
            {
                goto Label_0119;
            }
        Label_0157:
            if (num >= 0)
            {
                goto Label_0162;
            }
            num = 0;
        Label_0162:
            base.StartCoroutine(this.AsyncSetup(character, num));
            return;
        }

        public unsafe void SwitchAttachmentLists(EquipmentType type, int no)
        {
            int num;
            Transform transform;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            Transform transform2;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            EquipmentType type2;
            if (no > 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            num = no - 1;
            type2 = type;
            if (type2 == null)
            {
                goto Label_0023;
            }
            if (type2 == 1)
            {
                goto Label_00D2;
            }
            goto Label_0189;
        Label_0023:
            if (num >= this.Rig.RightHandChangeLists.Count)
            {
                goto Label_0189;
            }
            if (string.IsNullOrEmpty(this.Rig.RightHandChangeLists[num]) != null)
            {
                goto Label_0189;
            }
            transform = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.RightHandChangeLists[num]);
            if ((transform != null) == null)
            {
                goto Label_0189;
            }
            this.SetAttachmentParent(this.mPrimaryEquipmentDefault, transform);
            enumerator = this.mPrimaryEquipmentChangeLists.GetEnumerator();
        Label_009B:
            try
            {
                goto Label_00B0;
            Label_00A0:
                obj2 = &enumerator.Current;
                this.SetAttachmentParent(obj2, transform);
            Label_00B0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A0;
                }
                goto Label_00CD;
            }
            finally
            {
            Label_00C1:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00CD:
            goto Label_0189;
        Label_00D2:
            if (num >= this.Rig.LeftHandChangeLists.Count)
            {
                goto Label_0189;
            }
            if (string.IsNullOrEmpty(this.Rig.LeftHandChangeLists[num]) != null)
            {
                goto Label_0189;
            }
            transform2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), this.Rig.LeftHandChangeLists[num]);
            if ((transform2 != null) == null)
            {
                goto Label_0189;
            }
            this.SetAttachmentParent(this.mSecondaryEquipmentDefault, transform2);
            enumerator2 = this.mSecondaryEquipmentChangeLists.GetEnumerator();
        Label_014E:
            try
            {
                goto Label_0166;
            Label_0153:
                obj3 = &enumerator2.Current;
                this.SetAttachmentParent(obj3, transform2);
            Label_0166:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0153;
                }
                goto Label_0184;
            }
            finally
            {
            Label_0177:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_0184:;
        Label_0189:
            return;
        }

        public void SwitchEquipmentLists(EquipmentType type, int no)
        {
            int num;
            bool flag;
            bool flag2;
            EquipmentType type2;
            if (no > 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            num = no - 1;
            type2 = type;
            if (type2 == null)
            {
                goto Label_0020;
            }
            if (type2 == 1)
            {
                goto Label_00A2;
            }
            goto Label_0124;
        Label_0020:
            if (num >= this.mPrimaryEquipmentChangeLists.Count)
            {
                goto Label_0124;
            }
            flag = (this.mPrimaryEquipment == null) ? 1 : this.mPrimaryEquipment.get_activeSelf();
            if (this.mPrimaryEquipment == null)
            {
                goto Label_006F;
            }
            this.mPrimaryEquipment.SetActive(0);
        Label_006F:
            this.mPrimaryEquipment = this.mPrimaryEquipmentChangeLists[num];
            if (this.mPrimaryEquipment == null)
            {
                goto Label_0124;
            }
            this.mPrimaryEquipment.SetActive(flag);
            goto Label_0124;
        Label_00A2:
            if (num >= this.mSecondaryEquipmentChangeLists.Count)
            {
                goto Label_0124;
            }
            flag2 = (this.mSecondaryEquipment == null) ? 1 : this.mSecondaryEquipment.get_activeSelf();
            if (this.mSecondaryEquipment == null)
            {
                goto Label_00F1;
            }
            this.mSecondaryEquipment.SetActive(0);
        Label_00F1:
            this.mSecondaryEquipment = this.mSecondaryEquipmentChangeLists[num];
            if (this.mSecondaryEquipment == null)
            {
                goto Label_0124;
            }
            this.mSecondaryEquipment.SetActive(flag2);
        Label_0124:
            return;
        }

        public void SwitchEquipments()
        {
            GameObject obj2;
            List<GameObject> list;
            obj2 = this.mPrimaryEquipment;
            this.mPrimaryEquipment = this.mSubPrimaryEquipment;
            this.mSubPrimaryEquipment = obj2;
            obj2 = this.mSecondaryEquipment;
            this.mSecondaryEquipment = this.mSubSecondaryEquipment;
            this.mSubSecondaryEquipment = obj2;
            obj2 = this.mPrimaryEquipmentDefault;
            this.mPrimaryEquipmentDefault = this.mSubPrimaryEquipmentDefault;
            this.mSubPrimaryEquipmentDefault = obj2;
            list = this.mPrimaryEquipmentChangeLists;
            this.mPrimaryEquipmentChangeLists = this.mSubPrimaryEquipmentChangeLists;
            this.mSubPrimaryEquipmentChangeLists = list;
            obj2 = this.mSecondaryEquipmentDefault;
            this.mSecondaryEquipmentDefault = this.mSubSecondaryEquipmentDefault;
            this.mSubSecondaryEquipmentDefault = obj2;
            list = this.mSecondaryEquipmentChangeLists;
            this.mSecondaryEquipmentChangeLists = this.mSubSecondaryEquipmentChangeLists;
            this.mSubSecondaryEquipmentChangeLists = list;
            return;
        }

        private unsafe void UpdateFaceAnimation()
        {
            AnimDef def;
            float num;
            AnimationCurve curve;
            if ((this.mFaceAnimation == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mPlayingFaceAnimation == null)
            {
                goto Label_0072;
            }
            this.mFaceAnimation.PlayAnimation = 1;
            this.mPlayingFaceAnimation = 0;
            if (&this.mFaceAnimation.Animation0.Curve != null)
            {
                goto Label_0051;
            }
            this.mFaceAnimation.Face0 = 0;
        Label_0051:
            if (&this.mFaceAnimation.Animation1.Curve != null)
            {
                goto Label_0072;
            }
            this.mFaceAnimation.Face1 = 0;
        Label_0072:
            def = base.GetActiveAnimation(&num);
            if ((def != null) == null)
            {
                goto Label_0102;
            }
            curve = def.FindCustomCurve("FAC0");
            if (curve == null)
            {
                goto Label_00B9;
            }
            this.mFaceAnimation.Face0 = Mathf.FloorToInt(curve.Evaluate(num)) - 1;
            this.mPlayingFaceAnimation = 1;
        Label_00B9:
            curve = def.FindCustomCurve("FAC1");
            if (curve == null)
            {
                goto Label_00EB;
            }
            this.mFaceAnimation.Face1 = Mathf.FloorToInt(curve.Evaluate(num)) - 1;
            this.mPlayingFaceAnimation = 1;
        Label_00EB:
            if (this.mPlayingFaceAnimation == null)
            {
                goto Label_0102;
            }
            this.mFaceAnimation.PlayAnimation = 0;
        Label_0102:
            return;
        }

        private void UpdateVesselAnimation()
        {
            float num;
            float num2;
            num = Time.get_deltaTime();
            this.mVesselAnimTime += num;
            num2 = Mathf.Clamp01(this.mVesselAnimTime / this.mVesselAnimDuration);
            this.SetVesselStrength(Mathf.Lerp(this.mVesselAnimStart, this.mVesselAnimEnd, num2));
            return;
        }

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
                return (((this.mCharacterSettings != null) == null) ? null : this.mCharacterSettings.Rig);
            }
        }

        public float Height
        {
            get
            {
                RigSetup setup;
                Vector3 vector;
                setup = this.Rig;
                if ((setup != null) == null)
                {
                    goto Label_002E;
                }
                return (setup.Height * &base.get_transform().get_localScale().y);
            Label_002E:
                return 0f;
            }
        }

        public Vector3 CenterPosition
        {
            get
            {
                return (base.get_transform().get_position() + (Vector3.get_up() * (this.Height * 0.5f)));
            }
        }

        public GameObject UnitObject
        {
            get
            {
                return this.mUnitObject;
            }
        }

        public SRPG.UnitData UnitData
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
                if (this.mSetupStarted == null)
                {
                    goto Label_0017;
                }
                if (this.mNumLoadRequests <= 0)
                {
                    goto Label_0019;
                }
            Label_0017:
                return 1;
            Label_0019:
                return base.IsLoading;
            }
        }

        protected virtual ESex Sex
        {
            get
            {
                return this.mUnitData.UnitParam.sex;
            }
        }

        [CompilerGenerated]
        private sealed class <AsyncSetup>c__Iterator4E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <equipReq>__0;
            internal LoadRequest <artifactReq>__1;
            internal CharacterDB.Character ch;
            internal int jobIndex;
            internal LoadRequest <reqBody>__2;
            internal LoadRequest <reqBodyTexture>__3;
            internal LoadRequest <reqBodyAttachment>__4;
            internal LoadRequest <reqHead>__5;
            internal LoadRequest <reqHair>__6;
            internal LoadRequest <reqHeadAttachment>__7;
            internal CharacterComposer <cc>__8;
            internal JobParam <jobParam>__9;
            internal JobData <currentJob>__10;
            internal JobData[] <$s_309>__11;
            internal int <$s_310>__12;
            internal JobData <jd>__13;
            internal long <curAfId>__14;
            internal ArtifactData <afData>__15;
            internal ArtifactParam <artifact>__16;
            internal EquipmentSet <artifact>__17;
            internal EquipmentSet <equipment>__18;
            internal GameObject <primary>__19;
            internal List<GameObject> <primary_lists>__20;
            internal GameObject <secondary>__21;
            internal List<GameObject> <secondary_lists>__22;
            internal Transform <parent>__23;
            internal Transform <parent>__24;
            internal List<GameObject>.Enumerator <$s_311>__25;
            internal GameObject <primary_chg>__26;
            internal GameObject <go>__27;
            internal Transform <parent>__28;
            internal Transform <parent>__29;
            internal List<GameObject>.Enumerator <$s_312>__30;
            internal GameObject <secondary_chg>__31;
            internal GameObject <go>__32;
            internal int $PC;
            internal object $current;
            internal CharacterDB.Character <$>ch;
            internal int <$>jobIndex;
            internal UnitController <>f__this;

            public <AsyncSetup>c__Iterator4E()
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
                CharacterComposer composer;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0049;

                    case 1:
                        goto Label_02EF;

                    case 2:
                        goto Label_0342;

                    case 3:
                        goto Label_0395;

                    case 4:
                        goto Label_03E8;

                    case 5:
                        goto Label_043B;

                    case 6:
                        goto Label_048E;

                    case 7:
                        goto Label_052C;

                    case 8:
                        goto Label_0867;

                    case 9:
                        goto Label_0908;

                    case 10:
                        goto Label_0977;

                    case 11:
                        goto Label_12AC;
                }
                goto Label_12B3;
            Label_0049:
                this.<>f__this.AddLoadThreadCount();
                this.<equipReq>__0 = null;
                this.<artifactReq>__1 = null;
                this.<>f__this.mSetupStarted = 1;
                this.<>f__this.mCharacterData = this.ch.Jobs[this.jobIndex];
                this.<>f__this.mCharacterDataLists.Add(this.<>f__this.mCharacterData);
                this.<reqBody>__2 = null;
                this.<reqBodyTexture>__3 = null;
                this.<reqBodyAttachment>__4 = null;
                this.<reqHead>__5 = null;
                this.<reqHair>__6 = null;
                this.<reqHeadAttachment>__7 = null;
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.BodyName) != null)
                {
                    goto Label_0119;
                }
                this.<reqBody>__2 = this.<>f__this.LoadResource<GameObject>("CH/BODY/" + this.<>f__this.mCharacterData.BodyName);
            Label_0119:
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.BodyTextureName) != null)
                {
                    goto Label_015E;
                }
                this.<reqBodyTexture>__3 = this.<>f__this.LoadResource<Texture2D>("CH/BODYTEX/" + this.<>f__this.mCharacterData.BodyTextureName);
            Label_015E:
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.BodyAttachmentName) != null)
                {
                    goto Label_01A3;
                }
                this.<reqBodyAttachment>__4 = this.<>f__this.LoadResource<GameObject>("CH/BODYOPT/" + this.<>f__this.mCharacterData.BodyAttachmentName);
            Label_01A3:
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.HeadName) != null)
                {
                    goto Label_01E8;
                }
                this.<reqHead>__5 = this.<>f__this.LoadResource<GameObject>("CH/HEAD/" + this.<>f__this.mCharacterData.HeadName);
            Label_01E8:
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.HairName) != null)
                {
                    goto Label_022D;
                }
                this.<reqHair>__6 = this.<>f__this.LoadResource<GameObject>("CH/HAIR/" + this.<>f__this.mCharacterData.HairName);
            Label_022D:
                if (string.IsNullOrEmpty(this.<>f__this.mCharacterData.HeadAttachmentName) != null)
                {
                    goto Label_0272;
                }
                this.<reqHeadAttachment>__7 = this.<>f__this.LoadResource<GameObject>("CH/HEADOPT/" + this.<>f__this.mCharacterData.HeadAttachmentName);
            Label_0272:
                this.<cc>__8 = new CharacterComposer();
                &this.<cc>__8.HairColor0 = this.<>f__this.mCharacterData.HairColor0;
                &this.<cc>__8.HairColor1 = this.<>f__this.mCharacterData.HairColor1;
                if (this.<reqBody>__2 == null)
                {
                    goto Label_030A;
                }
                if (this.<reqBody>__2.isDone != null)
                {
                    goto Label_02EF;
                }
                this.$current = this.<reqBody>__2.StartCoroutine();
                this.$PC = 1;
                goto Label_12B5;
            Label_02EF:
                &this.<cc>__8.Body = this.<reqBody>__2.asset as GameObject;
            Label_030A:
                if (this.<reqBodyTexture>__3 == null)
                {
                    goto Label_035D;
                }
                if (this.<reqBodyTexture>__3.isDone != null)
                {
                    goto Label_0342;
                }
                this.$current = this.<reqBodyTexture>__3.StartCoroutine();
                this.$PC = 2;
                goto Label_12B5;
            Label_0342:
                &this.<cc>__8.BodyTexture = this.<reqBodyTexture>__3.asset as Texture2D;
            Label_035D:
                if (this.<reqBodyAttachment>__4 == null)
                {
                    goto Label_03B0;
                }
                if (this.<reqBodyAttachment>__4.isDone != null)
                {
                    goto Label_0395;
                }
                this.$current = this.<reqBodyAttachment>__4.StartCoroutine();
                this.$PC = 3;
                goto Label_12B5;
            Label_0395:
                &this.<cc>__8.BodyAttachment = this.<reqBodyAttachment>__4.asset as GameObject;
            Label_03B0:
                if (this.<reqHead>__5 == null)
                {
                    goto Label_0403;
                }
                if (this.<reqHead>__5.isDone != null)
                {
                    goto Label_03E8;
                }
                this.$current = this.<reqHead>__5.StartCoroutine();
                this.$PC = 4;
                goto Label_12B5;
            Label_03E8:
                &this.<cc>__8.Head = this.<reqHead>__5.asset as GameObject;
            Label_0403:
                if (this.<reqHair>__6 == null)
                {
                    goto Label_0456;
                }
                if (this.<reqHair>__6.isDone != null)
                {
                    goto Label_043B;
                }
                this.$current = this.<reqHair>__6.StartCoroutine();
                this.$PC = 5;
                goto Label_12B5;
            Label_043B:
                &this.<cc>__8.Hair = this.<reqHair>__6.asset as GameObject;
            Label_0456:
                if (this.<reqHeadAttachment>__7 == null)
                {
                    goto Label_04A9;
                }
                if (this.<reqHeadAttachment>__7.isDone != null)
                {
                    goto Label_048E;
                }
                this.$current = this.<reqHeadAttachment>__7.StartCoroutine();
                this.$PC = 6;
                goto Label_12B5;
            Label_048E:
                &this.<cc>__8.HeadAttachment = this.<reqHeadAttachment>__7.asset as GameObject;
            Label_04A9:
                this.<>f__this.mUnitObject = &this.<cc>__8.Compose(Vector3.get_zero(), Quaternion.get_identity());
                if ((this.<>f__this.mUnitObject == null) == null)
                {
                    goto Label_052C;
                }
                Debug.LogError("Failed to create character " + this.ch.CharacterID + "@" + this.<>f__this.mCharacterData.JobID);
                this.<>f__this.SetLoadError();
                this.$current = null;
                this.$PC = 7;
                goto Label_12B5;
            Label_052C:
                this.<>f__this.mUnitObject.get_transform().SetParent(this.<>f__this.get_transform(), 0);
                this.<>f__this.SetAnimationComponent(this.<>f__this.mUnitObject.GetComponent<Animation>());
                this.<>f__this.mUnitObjectLists.Add(this.<>f__this.mUnitObject);
                this.<>f__this.mFaceAnimation = this.<>f__this.mUnitObject.GetComponentInChildren<FaceAnimation>();
                this.<>f__this.mFaceAnimationLists.Add(this.<>f__this.mFaceAnimation);
                GameUtility.SetLayer(this.<>f__this, GameUtility.LayerHidden, 1);
                this.<>f__this.mCharacterSettings = this.<>f__this.mUnitObject.GetComponent<CharacterSettings>();
                this.<>f__this.mCharacterSettingsLists.Add(this.<>f__this.mCharacterSettings);
                if (((this.<>f__this.mCharacterSettings != null) == null) || ((this.<>f__this.mCharacterSettings.Rig != null) == null))
                {
                    goto Label_0651;
                }
                this.<>f__this.RootMotionBoneName = this.<>f__this.mCharacterSettings.Rig.RootBoneName;
            Label_0651:
                if (((this.<>f__this.LoadEquipments == null) || (string.IsNullOrEmpty(this.<>f__this.mJobID) != null)) || (((this.<>f__this.mCharacterSettings != null) == null) || ((this.<>f__this.Rig != null) == null)))
                {
                    goto Label_1256;
                }
                this.<jobParam>__9 = MonoSingleton<GameManager>.Instance.GetJobParam(this.<>f__this.mJobID);
                this.<currentJob>__10 = null;
                if (this.<>f__this.mUnitData == null)
                {
                    goto Label_07A1;
                }
                if (string.IsNullOrEmpty(this.<>f__this.mJobID) != null)
                {
                    goto Label_0780;
                }
                this.<$s_309>__11 = this.<>f__this.mUnitData.Jobs;
                this.<$s_310>__12 = 0;
                goto Label_076D;
            Label_070B:
                this.<jd>__13 = this.<$s_309>__11[this.<$s_310>__12];
                if ((this.<jd>__13 == null) || ((this.<jd>__13.Param.iname == this.<>f__this.mJobID) == null))
                {
                    goto Label_075F;
                }
                this.<currentJob>__10 = this.<jd>__13;
                goto Label_0780;
            Label_075F:
                this.<$s_310>__12 += 1;
            Label_076D:
                if (this.<$s_310>__12 < ((int) this.<$s_309>__11.Length))
                {
                    goto Label_070B;
                }
            Label_0780:
                if (this.<currentJob>__10 != null)
                {
                    goto Label_07A1;
                }
                this.<currentJob>__10 = this.<>f__this.mUnitData.CurrentJob;
            Label_07A1:
                this.<curAfId>__14 = (long) ((this.<currentJob>__10 == null) ? -1 : JobData.GetArtifactSlotIndex(1));
                this.<afData>__15 = null;
                if (this.<curAfId>__14 == -1L)
                {
                    goto Label_0814;
                }
                this.<afData>__15 = this.<currentJob>__10.ArtifactDatas[(IntPtr) this.<curAfId>__14];
                if ((this.<afData>__15 == null) || (this.<afData>__15.ArtifactParam.type == 1))
                {
                    goto Label_0814;
                }
                this.<afData>__15 = null;
            Label_0814:
                if (this.<afData>__15 == null)
                {
                    goto Label_088F;
                }
                this.<artifactReq>__1 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<afData>__15.ArtifactParam));
                if (this.<artifactReq>__1.isDone != null)
                {
                    goto Label_0867;
                }
                this.$current = this.<artifactReq>__1.StartCoroutine();
                this.$PC = 8;
                goto Label_12B5;
            Label_0867:
                this.<artifactReq>__1 = ((this.<artifactReq>__1.asset != null) == null) ? null : this.<artifactReq>__1;
            Label_088F:
                if (string.IsNullOrEmpty(this.<jobParam>__9.artifact) != null)
                {
                    goto Label_099F;
                }
                this.<artifact>__16 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.<jobParam>__9.artifact);
                this.<equipReq>__0 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<artifact>__16));
                if (this.<equipReq>__0.isDone != null)
                {
                    goto Label_0908;
                }
                this.$current = this.<equipReq>__0.StartCoroutine();
                this.$PC = 9;
                goto Label_12B5;
            Label_0908:
                if (((this.<equipReq>__0.asset == null) == null) || (string.IsNullOrEmpty(this.<jobParam>__9.wepmdl) != null))
                {
                    goto Label_0977;
                }
                this.<equipReq>__0 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.JobEquipment(this.<jobParam>__9));
                if (this.<equipReq>__0.isDone != null)
                {
                    goto Label_0977;
                }
                this.$current = this.<equipReq>__0.StartCoroutine();
                this.$PC = 10;
                goto Label_12B5;
            Label_0977:
                this.<equipReq>__0 = ((this.<equipReq>__0.asset != null) == null) ? null : this.<equipReq>__0;
            Label_099F:
                if ((this.<equipReq>__0 == null) && (this.<artifactReq>__1 == null))
                {
                    goto Label_124B;
                }
                this.<artifact>__17 = (this.<artifactReq>__1 == null) ? null : (this.<artifactReq>__1.asset as EquipmentSet);
                this.<equipment>__18 = (this.<equipReq>__0 == null) ? null : (this.<equipReq>__0.asset as EquipmentSet);
                this.<primary>__19 = null;
                this.<primary_lists>__20 = (((this.<artifact>__17 != null) == null) || (this.<artifact>__17.PrimaryHandChangeLists == null)) ? this.<equipment>__18.PrimaryHandChangeLists : this.<artifact>__17.PrimaryHandChangeLists;
                if (((this.<artifact>__17 != null) == null) || (this.<artifact>__17.PrimaryForceOverride == null))
                {
                    goto Label_0A94;
                }
                this.<primary>__19 = this.<artifact>__17.PrimaryHand;
                this.<primary_lists>__20 = this.<artifact>__17.PrimaryHandChangeLists;
                goto Label_0B24;
            Label_0A94:
                if (((this.<equipment>__18 != null) == null) || (this.<equipment>__18.PrimaryForceOverride == null))
                {
                    goto Label_0ADC;
                }
                this.<primary>__19 = this.<equipment>__18.PrimaryHand;
                this.<primary_lists>__20 = this.<equipment>__18.PrimaryHandChangeLists;
                goto Label_0B24;
            Label_0ADC:
                this.<primary>__19 = (((this.<artifact>__17 != null) == null) || ((this.<artifact>__17.PrimaryHand != null) == null)) ? this.<equipment>__18.PrimaryHand : this.<artifact>__17.PrimaryHand;
            Label_0B24:
                this.<secondary>__21 = (((this.<artifact>__17 != null) == null) || ((this.<artifact>__17.SecondaryHand != null) == null)) ? this.<equipment>__18.SecondaryHand : this.<artifact>__17.SecondaryHand;
                this.<secondary_lists>__22 = (((this.<artifact>__17 != null) == null) || (this.<artifact>__17.SecondaryHandChangeLists == null)) ? this.<equipment>__18.SecondaryHandChangeLists : this.<artifact>__17.SecondaryHandChangeLists;
                if (((this.<artifact>__17 != null) == null) || (this.<artifact>__17.SecondaryForceOverride == null))
                {
                    goto Label_0BF6;
                }
                this.<secondary>__21 = this.<artifact>__17.SecondaryHand;
                this.<secondary_lists>__22 = this.<artifact>__17.SecondaryHandChangeLists;
                goto Label_0C86;
            Label_0BF6:
                if (((this.<equipment>__18 != null) == null) || (this.<equipment>__18.PrimaryForceOverride == null))
                {
                    goto Label_0C3E;
                }
                this.<secondary>__21 = this.<equipment>__18.SecondaryHand;
                this.<secondary_lists>__22 = this.<equipment>__18.SecondaryHandChangeLists;
                goto Label_0C86;
            Label_0C3E:
                this.<secondary>__21 = (((this.<artifact>__17 != null) == null) || ((this.<artifact>__17.SecondaryHand != null) == null)) ? this.<equipment>__18.SecondaryHand : this.<artifact>__17.SecondaryHand;
            Label_0C86:
                if ((this.<primary>__19 != null) == null)
                {
                    goto Label_0DCE;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Rig.RightHand) != null)
                {
                    goto Label_0DCE;
                }
                this.<parent>__23 = GameUtility.findChildRecursively(this.<>f__this.UnitObject.get_transform(), this.<>f__this.Rig.RightHand);
                if ((this.<parent>__23 != null) == null)
                {
                    goto Label_0DAA;
                }
                this.<>f__this.mPrimaryEquipment = Object.Instantiate(this.<primary>__19, this.<primary>__19.get_transform().get_position(), this.<primary>__19.get_transform().get_rotation()) as GameObject;
                this.<>f__this.mPrimaryEquipment.get_transform().SetParent(this.<parent>__23, 0);
                this.<>f__this.mPrimaryEquipment.get_transform().set_localScale(Vector3.get_one() * this.<>f__this.Rig.EquipmentScale);
                GameUtility.SetLayer(this.<>f__this.mPrimaryEquipment, GameUtility.LayerHidden, 1);
                if (this.<equipment>__18.PrimaryHidden == null)
                {
                    goto Label_0DCE;
                }
                this.<>f__this.SetPrimaryEquipmentsVisible(0);
                goto Label_0DCE;
            Label_0DAA:
                Debug.LogError("Node " + this.<>f__this.Rig.RightHand + " not found.");
            Label_0DCE:
                if (this.<primary_lists>__20 == null)
                {
                    goto Label_0F66;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Rig.RightHand) != null)
                {
                    goto Label_0F66;
                }
                this.<parent>__24 = GameUtility.findChildRecursively(this.<>f__this.UnitObject.get_transform(), this.<>f__this.Rig.RightHand);
                if (this.<parent>__24 == null)
                {
                    goto Label_0F66;
                }
                this.<>f__this.mPrimaryEquipmentDefault = this.<>f__this.mPrimaryEquipment;
                this.<>f__this.mPrimaryEquipmentChangeLists.Clear();
                this.<$s_311>__25 = this.<primary_lists>__20.GetEnumerator();
            Label_0E65:
                try
                {
                    goto Label_0F40;
                Label_0E6A:
                    this.<primary_chg>__26 = &this.<$s_311>__25.Current;
                    this.<go>__27 = null;
                    if (this.<primary_chg>__26 == null)
                    {
                        goto Label_0F2A;
                    }
                    this.<go>__27 = Object.Instantiate(this.<primary_chg>__26, this.<primary_chg>__26.get_transform().get_position(), this.<primary_chg>__26.get_transform().get_rotation()) as GameObject;
                    if (this.<go>__27 == null)
                    {
                        goto Label_0F2A;
                    }
                    this.<go>__27.get_transform().SetParent(this.<parent>__24, 0);
                    this.<go>__27.get_transform().set_localScale(Vector3.get_one() * this.<>f__this.Rig.EquipmentScale);
                    this.<go>__27.get_gameObject().SetActive(0);
                Label_0F2A:
                    this.<>f__this.mPrimaryEquipmentChangeLists.Add(this.<go>__27);
                Label_0F40:
                    if (&this.<$s_311>__25.MoveNext() != null)
                    {
                        goto Label_0E6A;
                    }
                    goto Label_0F66;
                }
                finally
                {
                Label_0F55:
                    ((List<GameObject>.Enumerator) this.<$s_311>__25).Dispose();
                }
            Label_0F66:
                if ((this.<secondary>__21 != null) == null)
                {
                    goto Label_10AE;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Rig.LeftHand) != null)
                {
                    goto Label_10AE;
                }
                this.<parent>__28 = GameUtility.findChildRecursively(this.<>f__this.UnitObject.get_transform(), this.<>f__this.Rig.LeftHand);
                if ((this.<parent>__28 != null) == null)
                {
                    goto Label_108A;
                }
                this.<>f__this.mSecondaryEquipment = Object.Instantiate(this.<secondary>__21, this.<secondary>__21.get_transform().get_position(), this.<secondary>__21.get_transform().get_rotation()) as GameObject;
                this.<>f__this.mSecondaryEquipment.get_transform().SetParent(this.<parent>__28, 0);
                this.<>f__this.mSecondaryEquipment.get_transform().set_localScale(Vector3.get_one() * this.<>f__this.Rig.EquipmentScale);
                GameUtility.SetLayer(this.<>f__this.mSecondaryEquipment, GameUtility.LayerHidden, 1);
                if (this.<equipment>__18.SecondaryHidden == null)
                {
                    goto Label_10AE;
                }
                this.<>f__this.SetSecondaryEquipmentsVisible(0);
                goto Label_10AE;
            Label_108A:
                Debug.LogError("Node " + this.<>f__this.Rig.LeftHand + " not found.");
            Label_10AE:
                if (this.<secondary_lists>__22 == null)
                {
                    goto Label_1256;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Rig.LeftHand) != null)
                {
                    goto Label_1256;
                }
                this.<parent>__29 = GameUtility.findChildRecursively(this.<>f__this.UnitObject.get_transform(), this.<>f__this.Rig.LeftHand);
                if (this.<parent>__29 == null)
                {
                    goto Label_1256;
                }
                this.<>f__this.mSecondaryEquipmentDefault = this.<>f__this.mSecondaryEquipment;
                this.<>f__this.mSecondaryEquipmentChangeLists.Clear();
                this.<$s_312>__30 = this.<secondary_lists>__22.GetEnumerator();
            Label_1145:
                try
                {
                    goto Label_1220;
                Label_114A:
                    this.<secondary_chg>__31 = &this.<$s_312>__30.Current;
                    this.<go>__32 = null;
                    if (this.<secondary_chg>__31 == null)
                    {
                        goto Label_120A;
                    }
                    this.<go>__32 = Object.Instantiate(this.<secondary_chg>__31, this.<secondary_chg>__31.get_transform().get_position(), this.<secondary_chg>__31.get_transform().get_rotation()) as GameObject;
                    if (this.<go>__32 == null)
                    {
                        goto Label_120A;
                    }
                    this.<go>__32.get_transform().SetParent(this.<parent>__29, 0);
                    this.<go>__32.get_transform().set_localScale(Vector3.get_one() * this.<>f__this.Rig.EquipmentScale);
                    this.<go>__32.get_gameObject().SetActive(0);
                Label_120A:
                    this.<>f__this.mSecondaryEquipmentChangeLists.Add(this.<go>__32);
                Label_1220:
                    if (&this.<$s_312>__30.MoveNext() != null)
                    {
                        goto Label_114A;
                    }
                    goto Label_1246;
                }
                finally
                {
                Label_1235:
                    ((List<GameObject>.Enumerator) this.<$s_312>__30).Dispose();
                }
            Label_1246:
                goto Label_1256;
            Label_124B:
                this.<>f__this.SetLoadError();
            Label_1256:
                this.<>f__this.PostSetup();
                if (this.<>f__this.KeepUnitHidden != null)
                {
                    goto Label_1282;
                }
                GameUtility.SetLayer(this.<>f__this, GameUtility.LayerCH, 1);
            Label_1282:
                this.<>f__this.FindCharacterMaterials();
                this.<>f__this.RemoveLoadThreadCount();
                this.$current = null;
                this.$PC = 11;
                goto Label_12B5;
            Label_12AC:
                this.$PC = -1;
            Label_12B3:
                return 0;
            Label_12B5:
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

        public enum EquipmentType
        {
            PRIMARY,
            SECONDARY
        }
    }
}

