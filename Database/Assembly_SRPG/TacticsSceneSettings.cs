namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [ExecuteInEditMode]
    public class TacticsSceneSettings : SceneRoot
    {
        public static bool AutoDeactivateScene;
        private static List<TacticsSceneSettings> mScenes;
        [HideInInspector]
        public bool OverrideCameraSettings;
        [HideInInspector]
        public float CameraYawMin;
        [HideInInspector]
        public float CameraYawMax;
        public Texture2D ScreenFilter;
        public Texture2D BackgroundImage;
        private Mesh mGridMesh;
        private List<GridLayer> mGridLayers;

        static TacticsSceneSettings()
        {
            mScenes = new List<TacticsSceneSettings>();
            return;
        }

        public TacticsSceneSettings()
        {
            this.CameraYawMin = 45f;
            this.CameraYawMax = 135f;
            this.mGridLayers = new List<GridLayer>(6);
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            mScenes.Add(this);
            if (AutoDeactivateScene == null)
            {
                goto Label_002D;
            }
            base.get_gameObject().SetActive(0);
            AutoDeactivateScene = 0;
        Label_002D:
            TacticsSceneCamera.ClearInstance();
            return;
        }

        private void ExtendBounds()
        {
        }

        private GridLayer FindGridLayer(int layerID, bool autoCreate, string mat_path)
        {
            Transform transform1;
            int num;
            GameObject obj2;
            GridLayer layer;
            num = this.mGridLayers.Count - 1;
            goto Label_003B;
        Label_0013:
            if (this.mGridLayers[num].LayerID != layerID)
            {
                goto Label_0037;
            }
            return this.mGridLayers[num];
        Label_0037:
            num -= 1;
        Label_003B:
            if (num >= 0)
            {
                goto Label_0013;
            }
            if (autoCreate != null)
            {
                goto Label_004A;
            }
            return null;
        Label_004A:
            obj2 = new GameObject("Grid");
            layer = obj2.AddComponent<GridLayer>();
            layer.get_transform().SetParent(base.get_transform(), 0);
            layer.GetComponent<MeshFilter>().set_mesh(this.mGridMesh);
            layer.LayerID = layerID;
            layer.get_gameObject().SetActive(0);
            transform1 = layer.get_transform();
            transform1.set_position(transform1.get_position() + ((Vector3.get_up() * ((float) layerID)) * 0.01f));
            if (string.IsNullOrEmpty(mat_path) != null)
            {
                goto Label_00D0;
            }
            layer.ChangeMaterial(mat_path);
        Label_00D0:
            this.mGridLayers.Add(layer);
            return layer;
        }

        public unsafe void GenerateGridMesh(int x, int y)
        {
            GridMeshGenerator generator;
            Rect rect;
            Matrix4x4 matrixx;
            MeshFilter[] filterArray;
            int num;
            Transform transform;
            Vector3 vector;
            bool flag;
            generator = new GridMeshGenerator();
            &rect..ctor(0f, 0f, (float) x, (float) y);
            matrixx = base.get_transform().get_worldToLocalMatrix();
            filterArray = base.GetComponentsInChildren<MeshFilter>(1);
            num = 0;
            goto Label_00ED;
        Label_0037:
            if (filterArray[num].get_gameObject().get_activeSelf() != null)
            {
                goto Label_004F;
            }
            goto Label_00E7;
        Label_004F:
            if (filterArray[num].get_gameObject().get_layer() == GameUtility.LayerBG)
            {
                goto Label_006C;
            }
            goto Label_00E7;
        Label_006C:
            if ((filterArray[num].get_sharedMesh() == null) == null)
            {
                goto Label_0085;
            }
            goto Label_00E7;
        Label_0085:
            transform = filterArray[num].get_transform();
            vector = transform.get_lossyScale();
            float introduced8 = Mathf.Sign(&vector.x);
            flag = ((introduced8 * Mathf.Sign(&vector.y)) * Mathf.Sign(&vector.z)) < 0f;
            generator.AddMesh(filterArray[num].get_sharedMesh(), matrixx * transform.get_localToWorldMatrix(), rect, flag);
        Label_00E7:
            num += 1;
        Label_00ED:
            if (num < ((int) filterArray.Length))
            {
                goto Label_0037;
            }
            this.mGridMesh = generator.CreateMesh();
            return;
        }

        public void HideGridLayer(int layerID)
        {
            GridLayer layer;
            layer = this.FindGridLayer(layerID, 0, null);
            if ((layer != null) == null)
            {
                goto Label_001C;
            }
            layer.Hide();
        Label_001C:
            return;
        }

        public void HideGridLayers()
        {
            int num;
            num = this.mGridLayers.Count - 1;
            goto Label_0028;
        Label_0013:
            this.mGridLayers[num].Hide();
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return;
        }

        private void OnDestroy()
        {
            if ((this.mGridMesh != null) == null)
            {
                goto Label_0023;
            }
            Object.DestroyImmediate(this.mGridMesh);
            this.mGridMesh = null;
        Label_0023:
            mScenes.Remove(this);
            return;
        }

        public static void PopFirstScene()
        {
            if (mScenes.Count > 0)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Object.DestroyImmediate(mScenes[0].get_gameObject());
            return;
        }

        public unsafe bool RaycastDeformed(Ray ray, out RaycastHit result)
        {
            string[] textArray1;
            MeshFilter[] filterArray;
            string[] strArray;
            Transform transform;
            Vector3 vector;
            int num;
            Mesh mesh;
            GameObject obj2;
            Transform transform2;
            float num2;
            Vector3 vector2;
            Vector3 vector3;
            filterArray = base.GetComponentsInChildren<MeshFilter>(0);
            result = new RaycastHit();
            result.set_distance(3.402823E+38f);
            textArray1 = new string[] { "BG" };
            strArray = textArray1;
            vector = Camera.get_main().get_transform().get_position();
            num = 0;
            goto Label_00E8;
        Label_0043:
            mesh = filterArray[num].get_sharedMesh();
            if ((mesh == null) == null)
            {
                goto Label_0060;
            }
            goto Label_00E2;
        Label_0060:
            obj2 = filterArray[num].get_gameObject();
            if (((1 << (obj2.get_layer() & 0x1f)) & LayerMask.GetMask(strArray)) != null)
            {
                goto Label_0088;
            }
            goto Label_00E2;
        Label_0088:
            transform2 = obj2.get_transform();
            if (this.RaycastDeformedMesh(mesh, &ray.get_origin(), &ray.get_direction(), transform2.get_localToWorldMatrix(), &vector.z, &num2, &vector2, &vector3) == null)
            {
                goto Label_00E2;
            }
            if (num2 < result.get_distance())
            {
                goto Label_00D2;
            }
            goto Label_00E2;
        Label_00D2:
            result.set_point(vector3);
            result.set_distance(num2);
        Label_00E2:
            num += 1;
        Label_00E8:
            if (num < ((int) filterArray.Length))
            {
                goto Label_0043;
            }
            return (result.get_distance() < 3.402823E+38f);
        }

        private unsafe bool RaycastDeformedMesh(Mesh mesh, Vector3 start, Vector3 direction, Matrix4x4 world, float cameraZ, out float hitDistance, out Vector3 hitPos, out Vector3 hitPosReal)
        {
            Vector3[] vectorArray;
            int[] numArray;
            int num;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            Vector3 vector6;
            Vector3 vector7;
            Vector3 vector8;
            Vector3 vector9;
            float num2;
            float num3;
            float num4;
            Vector3 vector10;
            Vector3 vector11;
            Vector3 vector12;
            Vector3 vector13;
            float num5;
            Vector3 vector14;
            Vector3 vector15;
            Vector3 vector16;
            float num6;
            float num7;
            float num8;
            float num9;
            float num10;
            float num11;
            float num12;
            Vector3 vector17;
            Vector3 vector18;
            float num13;
            float num14;
            Vector3 vector19;
            Vector3 vector20;
            Vector3 vector21;
            Vector3 vector22;
            Vector3 vector23;
            Vector3 vector24;
            *((float*) hitDistance) = 3.402823E+38f;
            *(hitPos) = Vector3.get_zero();
            *(hitPosReal) = Vector3.get_zero();
            vectorArray = mesh.get_vertices();
            numArray = mesh.get_triangles();
            num = 0;
            goto Label_02D3;
        Label_0035:
            vector = *(&(vectorArray[numArray[num]]));
            vector2 = *(&(vectorArray[numArray[num + 1]]));
            vector3 = *(&(vectorArray[numArray[num + 2]]));
            vector = &world.MultiplyPoint(vector);
            vector2 = &world.MultiplyPoint(vector2);
            vector3 = &world.MultiplyPoint(vector3);
            vector4 = GameUtility.DeformPosition(vector, cameraZ);
            vector5 = GameUtility.DeformPosition(vector2, cameraZ);
            vector6 = GameUtility.DeformPosition(vector3, cameraZ);
            vector19 = vector5 - vector4;
            vector7 = &vector19.get_normalized();
            vector20 = vector6 - vector4;
            vector9 = &Vector3.Cross(&vector20.get_normalized(), vector7).get_normalized();
            num2 = Vector3.Dot(vector9, direction);
            if (num2 > 0f)
            {
                goto Label_00FE;
            }
            goto Label_02CF;
        Label_00FE:
            num4 = Vector3.Dot(vector4 - start, vector9) / num2;
            if (num4 < *(((float*) hitDistance)))
            {
                goto Label_0125;
            }
            goto Label_02CF;
        Label_0125:
            vector10 = start + (direction * num4);
            vector22 = vector4 - vector10;
            vector11 = &vector22.get_normalized();
            vector23 = vector5 - vector10;
            vector12 = &vector23.get_normalized();
            vector24 = vector6 - vector10;
            vector13 = &vector24.get_normalized();
            num5 = (Mathf.Acos(Mathf.Clamp(Vector3.Dot(vector11, vector12), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(vector12, vector13), -1f, 1f))) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(vector13, vector11), -1f, 1f));
            if (num5 > 6.273185f)
            {
                goto Label_01DD;
            }
            goto Label_02CF;
        Label_01DD:
            *((float*) hitDistance) = num4;
            *(hitPos) = vector10;
            vector14 = vector10 - vector4;
            vector15 = vector6 - vector5;
            vector16 = vector4 - vector5;
            num6 = Vector3.Dot(vector14, vector14);
            num7 = Vector3.Dot(vector14, vector15);
            num8 = Vector3.Dot(vector15, vector15);
            num9 = (num6 * num8) - (num7 * num7);
            num10 = Vector3.Dot(vector14, vector16);
            num11 = Vector3.Dot(vector15, vector16);
            num12 = ((num7 * num11) - (num10 * num8)) / num9;
            vector17 = vector4 + (vector14 * num12);
            vector18 = vector17 - vector4;
            num13 = Vector3.Dot(vector17 - vector5, vector15) / Vector3.Dot(vector15, vector15);
            num14 = Vector3.Dot(vector10 - vector4, vector18) / Vector3.Dot(vector18, vector18);
            *(hitPosReal) = Vector3.Lerp(vector, Vector3.Lerp(vector2, vector3, num13), num14);
        Label_02CF:
            num += 3;
        Label_02D3:
            if (num < ((int) numArray.Length))
            {
                goto Label_0035;
            }
            return (*(((float*) hitDistance)) < 3.402823E+38f);
        }

        public static void SetScenesActive(bool active)
        {
            int num;
            num = 0;
            goto Label_0021;
        Label_0007:
            mScenes[num].get_gameObject().SetActive(active);
            num += 1;
        Label_0021:
            if (num < mScenes.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void ShowGridLayer(int layerID, GridMap<Color32> grid, bool mask)
        {
            GridLayer layer;
            layer = this.FindGridLayer(layerID, 1, null);
            layer.UpdateGrid(grid);
            layer.SetMask(mask);
            layer.Show();
            return;
        }

        public void ShowGridLayer(int layerID, GridMap<Color32> grid, string mat_path)
        {
            GridLayer layer;
            layer = this.FindGridLayer(layerID, 1, mat_path);
            layer.UpdateGrid(grid);
            layer.SetMask(1);
            layer.Show();
            return;
        }

        public void ShowGridLayer(int layerID, GridMap<bool> grid, Color32 color, bool mask)
        {
            GridMap<Color32> map;
            int num;
            int num2;
            if (grid != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = new GridMap<Color32>(grid.w, grid.h);
            num = 0;
            goto Label_0051;
        Label_0020:
            num2 = 0;
            goto Label_0041;
        Label_0027:
            if (grid.get(num, num2) == null)
            {
                goto Label_003D;
            }
            map.set(num, num2, color);
        Label_003D:
            num2 += 1;
        Label_0041:
            if (num2 < grid.h)
            {
                goto Label_0027;
            }
            num += 1;
        Label_0051:
            if (num < grid.w)
            {
                goto Label_0020;
            }
            this.ShowGridLayer(layerID, map, mask);
            return;
        }

        private void Start()
        {
            Light[] lightArray;
            int num;
            this.ExtendBounds();
            if (Application.get_isPlaying() == null)
            {
                goto Label_0037;
            }
            lightArray = base.GetComponentsInChildren<Light>(1);
            num = ((int) lightArray.Length) - 1;
            goto Label_0030;
        Label_0023:
            lightArray[num].set_shadows(0);
            num -= 1;
        Label_0030:
            if (num >= 0)
            {
                goto Label_0023;
            }
        Label_0037:
            return;
        }

        public static TacticsSceneSettings[] Scenes
        {
            get
            {
                return mScenes.ToArray();
            }
        }

        public static int SceneCount
        {
            get
            {
                return mScenes.Count;
            }
        }

        public static TacticsSceneSettings LastScene
        {
            get
            {
                return ((mScenes.Count <= 0) ? null : mScenes[mScenes.Count - 1]);
            }
        }

        public static TacticsSceneSettings Instance
        {
            get
            {
                return ((mScenes.Count <= 0) ? null : mScenes[0]);
            }
        }
    }
}

