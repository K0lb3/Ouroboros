namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TacticsSceneCamera : MonoBehaviour
    {
        public MoveRange m_MoveRange;
        public AllRange m_AllRange;
        private AllRangeObj m_AllRangeObj;
        private static TacticsSceneCamera m_Instance;

        static TacticsSceneCamera()
        {
        }

        public TacticsSceneCamera()
        {
            base..ctor();
            return;
        }

        private void CalcAllRangeCamera()
        {
            float num;
            int num2;
            AllRangeObj.GroupObj obj2;
            float num3;
            float num4;
            float num5;
            bool flag;
            bool flag2;
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            num = 360f - SceneBattle.Instance.GetCameraAngle();
            if (num >= 0f)
            {
                goto Label_003A;
            }
            num += 360f;
            goto Label_004D;
        Label_003A:
            if (num < 360f)
            {
                goto Label_004D;
            }
            num -= 360f;
        Label_004D:
            num2 = 0;
            goto Label_0272;
        Label_0054:
            obj2 = this.m_AllRangeObj.groups[num2];
            num3 = obj2.data.min;
            num4 = obj2.data.max;
            num5 = obj2.data.prange;
            flag = 0;
            flag2 = 0;
            if (num5 != 0f)
            {
                goto Label_00A1;
            }
            num5 = 1f;
        Label_00A1:
            flag = ((num3 < num) && ((num3 + num5) > num)) ? 1 : ((num4 <= num) ? 0 : ((num4 - num5) < num));
            if (obj2.state != null)
            {
                goto Label_0198;
            }
            if (num <= num3)
            {
                goto Label_0142;
            }
            if (num >= num4)
            {
                goto Label_0142;
            }
            if (flag2 == null)
            {
                goto Label_0109;
            }
            obj2.alpha -= obj2.alpha * 0.2f;
            goto Label_0114;
        Label_0109:
            obj2.alpha = 0f;
        Label_0114:
            if (obj2.alpha >= 0.01f)
            {
                goto Label_026E;
            }
            obj2.alpha = 0f;
            if (flag != null)
            {
                goto Label_026E;
            }
            obj2.state = 1;
            goto Label_0193;
        Label_0142:
            if (flag2 == null)
            {
                goto Label_016D;
            }
            obj2.alpha += (1f - obj2.alpha) * 0.2f;
            goto Label_0178;
        Label_016D:
            obj2.alpha = 1f;
        Label_0178:
            if (obj2.alpha <= 0.99f)
            {
                goto Label_026E;
            }
            obj2.alpha = 1f;
        Label_0193:
            goto Label_026E;
        Label_0198:
            if (obj2.state != 1)
            {
                goto Label_026E;
            }
            num3 += num5;
            num4 -= num5;
            if (num < num3)
            {
                goto Label_020F;
            }
            if (num > num4)
            {
                goto Label_020F;
            }
            if (flag2 == null)
            {
                goto Label_01E4;
            }
            obj2.alpha -= obj2.alpha * 0.2f;
            goto Label_01EF;
        Label_01E4:
            obj2.alpha = 0f;
        Label_01EF:
            if (obj2.alpha >= 0.01f)
            {
                goto Label_026E;
            }
            obj2.alpha = 0f;
            goto Label_026E;
        Label_020F:
            if (flag2 == null)
            {
                goto Label_023A;
            }
            obj2.alpha += (1f - obj2.alpha) * 0.2f;
            goto Label_0245;
        Label_023A:
            obj2.alpha = 1f;
        Label_0245:
            if (obj2.alpha <= 0.99f)
            {
                goto Label_026E;
            }
            obj2.alpha = 1f;
            if (flag != null)
            {
                goto Label_026E;
            }
            obj2.state = 0;
        Label_026E:
            num2 += 1;
        Label_0272:
            if (num2 < ((int) this.m_AllRangeObj.groups.Length))
            {
                goto Label_0054;
            }
            return;
        }

        private static bool CheckMaterialShader(Material material, string[] shaders)
        {
            int num;
            if (shaders != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            num = 0;
            goto Label_002D;
        Label_000F:
            if ((material.get_shader().get_name() == shaders[num]) == null)
            {
                goto Label_0029;
            }
            return 1;
        Label_0029:
            num += 1;
        Label_002D:
            if (num < ((int) shaders.Length))
            {
                goto Label_000F;
            }
            return 0;
        }

        public static void ClearInstance()
        {
            m_Instance = null;
            return;
        }

        public void CreateAllRange()
        {
            if (this.m_AllRange != null)
            {
                goto Label_0016;
            }
            this.m_AllRange = new AllRange();
        Label_0016:
            return;
        }

        public void CreateMoveRange(TacticsSceneSettings setting)
        {
            if (this.m_MoveRange != null)
            {
                goto Label_0076;
            }
            this.m_MoveRange = new MoveRange();
            if (setting.OverrideCameraSettings == null)
            {
                goto Label_0076;
            }
            this.m_MoveRange.isOverride = setting.OverrideCameraSettings;
            this.m_MoveRange.min = 360f - setting.CameraYawMax;
            this.m_MoveRange.max = 360f - setting.CameraYawMin;
            this.m_MoveRange.start = this.m_MoveRange.min;
        Label_0076:
            return;
        }

        public static RenderSet GetRenderSet(GameObject gobj, string[] shaders)
        {
            RenderSet set;
            MeshRenderer[] rendererArray;
            int num;
            Material[] materialArray;
            int num2;
            int num3;
            Material[] materialArray2;
            int num4;
            SkinnedMeshRenderer[] rendererArray2;
            int num5;
            Material[] materialArray3;
            int num6;
            set = new RenderSet();
            rendererArray = gobj.GetComponentsInChildren<MeshRenderer>();
            num = 0;
            goto Label_0126;
        Label_0014:
            if (Application.get_isPlaying() != null)
            {
                goto Label_00D1;
            }
            materialArray = rendererArray[num].get_sharedMaterials();
            if (materialArray == null)
            {
                goto Label_007F;
            }
            if (((int) materialArray.Length) <= 0)
            {
                goto Label_007F;
            }
            num2 = 0;
            goto Label_0070;
        Label_003E:
            if (CheckMaterialShader(materialArray[num2], shaders) == null)
            {
                goto Label_006A;
            }
            set.meshRenderers.Add(rendererArray[num]);
            set.materials.Add(materialArray[num2]);
        Label_006A:
            num2 += 1;
        Label_0070:
            if (num2 < ((int) materialArray.Length))
            {
                goto Label_003E;
            }
            goto Label_00CC;
        Label_007F:
            materialArray = rendererArray[num].get_materials();
            num3 = 0;
            goto Label_00C2;
        Label_0090:
            if (CheckMaterialShader(materialArray[num3], shaders) == null)
            {
                goto Label_00BC;
            }
            set.meshRenderers.Add(rendererArray[num]);
            set.materials.Add(materialArray[num3]);
        Label_00BC:
            num3 += 1;
        Label_00C2:
            if (num3 < ((int) materialArray.Length))
            {
                goto Label_0090;
            }
        Label_00CC:
            goto Label_0122;
        Label_00D1:
            materialArray2 = rendererArray[num].get_materials();
            num4 = 0;
            goto Label_0117;
        Label_00E3:
            if (CheckMaterialShader(materialArray2[num4], shaders) == null)
            {
                goto Label_0111;
            }
            set.meshRenderers.Add(rendererArray[num]);
            set.materials.Add(materialArray2[num4]);
        Label_0111:
            num4 += 1;
        Label_0117:
            if (num4 < ((int) materialArray2.Length))
            {
                goto Label_00E3;
            }
        Label_0122:
            num += 1;
        Label_0126:
            if (num < ((int) rendererArray.Length))
            {
                goto Label_0014;
            }
            rendererArray2 = gobj.GetComponentsInChildren<SkinnedMeshRenderer>();
            num5 = 0;
            goto Label_019A;
        Label_013F:
            materialArray3 = rendererArray2[num5].get_materials();
            num6 = 0;
            goto Label_0189;
        Label_0153:
            if (CheckMaterialShader(materialArray3[num6], shaders) == null)
            {
                goto Label_0183;
            }
            set.skinRenderers.Add(rendererArray2[num5]);
            set.materials.Add(materialArray3[num6]);
        Label_0183:
            num6 += 1;
        Label_0189:
            if (num6 < ((int) materialArray3.Length))
            {
                goto Label_0153;
            }
            num5 += 1;
        Label_019A:
            if (num5 < ((int) rendererArray2.Length))
            {
                goto Label_013F;
            }
            if (set.materials.Count != null)
            {
                goto Label_01B7;
            }
            return null;
        Label_01B7:
            return set;
        }

        public static RenderSet[] GetRenderSets(GameObject[] gobjs, string[] shaders)
        {
            List<RenderSet> list;
            int num;
            RenderSet set;
            list = new List<RenderSet>();
            num = 0;
            goto Label_0028;
        Label_000D:
            set = GetRenderSet(gobjs[num], shaders);
            if (set == null)
            {
                goto Label_0024;
            }
            list.Add(set);
        Label_0024:
            num += 1;
        Label_0028:
            if (num < ((int) gobjs.Length))
            {
                goto Label_000D;
            }
            return list.ToArray();
        }

        private void OnDisable()
        {
            m_Instance = null;
            return;
        }

        private void OnEnable()
        {
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            m_Instance = this;
            SceneBattle.Instance.SetNewCamera(instance);
            return;
        }

        private void Start()
        {
            if (this.isAllRange == null)
            {
                goto Label_003D;
            }
            if (this.m_AllRange.groups == null)
            {
                goto Label_003D;
            }
            this.m_AllRangeObj = new AllRangeObj();
            TacticsSceneCameraUtility.Create(this.m_AllRangeObj, this.m_AllRange);
            this.UpdateAllRangeCamera();
        Label_003D:
            return;
        }

        private void Update()
        {
            if (this.m_AllRangeObj == null)
            {
                goto Label_0017;
            }
            this.CalcAllRangeCamera();
            this.UpdateAllRangeCamera();
        Label_0017:
            return;
        }

        private void UpdateAllRangeCamera()
        {
            int num;
            AllRangeObj.GroupObj obj2;
            int num2;
            RenderSet set;
            bool flag;
            int num3;
            int num4;
            num = 0;
            goto Label_00B9;
        Label_0007:
            obj2 = this.m_AllRangeObj.groups[num];
            num2 = 0;
            goto Label_00A4;
        Label_001C:
            set = obj2.renders[num2];
            flag = obj2.alpha > 0f;
            num3 = 0;
            goto Label_005A;
        Label_0040:
            set.meshRenderers[num3].set_enabled(flag);
            num3 += 1;
        Label_005A:
            if (num3 < set.meshRenderers.Count)
            {
                goto Label_0040;
            }
            num4 = 0;
            goto Label_008E;
        Label_0074:
            set.meshRenderers[num4].set_enabled(flag);
            num4 += 1;
        Label_008E:
            if (num4 < set.skinRenderers.Count)
            {
                goto Label_0074;
            }
            num2 += 1;
        Label_00A4:
            if (num2 < obj2.renders.Count)
            {
                goto Label_001C;
            }
            num += 1;
        Label_00B9:
            if (num < ((int) this.m_AllRangeObj.groups.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public MoveRange moveRange
        {
            get
            {
                return this.m_MoveRange;
            }
        }

        public AllRange allRange
        {
            get
            {
                return this.m_AllRange;
            }
        }

        public bool isAllRange
        {
            get
            {
                return ((this.m_AllRange == null) ? 0 : this.m_AllRange.enable);
            }
            set
            {
                this.m_AllRange.enable = value;
                return;
            }
        }

        public static TacticsSceneCamera instance
        {
            get
            {
                return m_Instance;
            }
        }

        [Serializable]
        public class AllRange
        {
            public bool enable;
            public Group[] groups;

            public AllRange()
            {
                base..ctor();
                return;
            }

            [Serializable]
            public class Group
            {
                public string name;
                public float min;
                public float max;
                public float prange;
                public bool fade;
                public GameObject[] gobjs;

                public Group()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class AllRangeObj
        {
            public TacticsSceneCamera.AllRange data;
            public GroupObj[] groups;

            public AllRangeObj()
            {
                base..ctor();
                return;
            }

            public class GroupObj
            {
                public TacticsSceneCamera.AllRange.Group data;
                public int state;
                public float alpha;
                public List<TacticsSceneCamera.RenderSet> renders;

                public GroupObj()
                {
                    this.renders = new List<TacticsSceneCamera.RenderSet>();
                    base..ctor();
                    return;
                }
            }
        }

        [Serializable]
        public class MoveRange
        {
            public bool isOverride;
            public float min;
            public float max;
            public float start;

            public MoveRange()
            {
                base..ctor();
                return;
            }
        }

        public class RenderSet
        {
            public List<MeshRenderer> meshRenderers;
            public List<SkinnedMeshRenderer> skinRenderers;
            public List<Material> materials;

            public RenderSet()
            {
                this.meshRenderers = new List<MeshRenderer>();
                this.skinRenderers = new List<SkinnedMeshRenderer>();
                this.materials = new List<Material>();
                base..ctor();
                return;
            }
        }
    }
}

