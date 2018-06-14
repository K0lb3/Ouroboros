// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsSceneSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  public class TacticsSceneSettings : SceneRoot
  {
    private static List<TacticsSceneSettings> mScenes = new List<TacticsSceneSettings>();
    [HideInInspector]
    public float CameraYawMin = 45f;
    [HideInInspector]
    public float CameraYawMax = 135f;
    private List<GridLayer> mGridLayers = new List<GridLayer>(6);
    public static bool AutoDeactivateScene;
    [HideInInspector]
    public bool OverrideCameraSettings;
    public Texture2D ScreenFilter;
    public Texture2D BackgroundImage;
    private Mesh mGridMesh;

    public static TacticsSceneSettings[] Scenes
    {
      get
      {
        return TacticsSceneSettings.mScenes.ToArray();
      }
    }

    public static int SceneCount
    {
      get
      {
        return TacticsSceneSettings.mScenes.Count;
      }
    }

    public static void SetScenesActive(bool active)
    {
      for (int index = 0; index < TacticsSceneSettings.mScenes.Count; ++index)
        ((Component) TacticsSceneSettings.mScenes[index]).get_gameObject().SetActive(active);
    }

    public static TacticsSceneSettings LastScene
    {
      get
      {
        if (TacticsSceneSettings.mScenes.Count > 0)
          return TacticsSceneSettings.mScenes[TacticsSceneSettings.mScenes.Count - 1];
        return (TacticsSceneSettings) null;
      }
    }

    public static TacticsSceneSettings Instance
    {
      get
      {
        if (TacticsSceneSettings.mScenes.Count > 0)
          return TacticsSceneSettings.mScenes[0];
        return (TacticsSceneSettings) null;
      }
    }

    public static void PopFirstScene()
    {
      if (TacticsSceneSettings.mScenes.Count <= 0)
        return;
      Object.DestroyImmediate((Object) ((Component) TacticsSceneSettings.mScenes[0]).get_gameObject());
    }

    private void Start()
    {
      this.ExtendBounds();
      if (!Application.get_isPlaying())
        return;
      Light[] componentsInChildren = (Light[]) ((Component) this).GetComponentsInChildren<Light>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        componentsInChildren[index].set_shadows((LightShadows) 0);
    }

    protected override void Awake()
    {
      base.Awake();
      TacticsSceneSettings.mScenes.Add(this);
      if (!TacticsSceneSettings.AutoDeactivateScene)
        return;
      ((Component) this).get_gameObject().SetActive(false);
      TacticsSceneSettings.AutoDeactivateScene = false;
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mGridMesh, (Object) null))
      {
        Object.DestroyImmediate((Object) this.mGridMesh);
        this.mGridMesh = (Mesh) null;
      }
      TacticsSceneSettings.mScenes.Remove(this);
    }

    public void GenerateGridMesh(int x, int y)
    {
      GridMeshGenerator gridMeshGenerator = new GridMeshGenerator();
      Rect clipRect;
      // ISSUE: explicit reference operation
      ((Rect) @clipRect).\u002Ector(0.0f, 0.0f, (float) x, (float) y);
      Matrix4x4 worldToLocalMatrix = ((Component) this).get_transform().get_worldToLocalMatrix();
      MeshFilter[] componentsInChildren = (MeshFilter[]) ((Component) this).GetComponentsInChildren<MeshFilter>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (((Component) componentsInChildren[index]).get_gameObject().get_activeSelf() && ((Component) componentsInChildren[index]).get_gameObject().get_layer() == GameUtility.LayerBG && !Object.op_Equality((Object) componentsInChildren[index].get_sharedMesh(), (Object) null))
        {
          Transform transform = ((Component) componentsInChildren[index]).get_transform();
          Vector3 lossyScale = transform.get_lossyScale();
          bool mirror = (double) Mathf.Sign((float) lossyScale.x) * (double) Mathf.Sign((float) lossyScale.y) * (double) Mathf.Sign((float) lossyScale.z) < 0.0;
          gridMeshGenerator.AddMesh(componentsInChildren[index].get_sharedMesh(), Matrix4x4.op_Multiply(worldToLocalMatrix, transform.get_localToWorldMatrix()), clipRect, mirror);
        }
      }
      this.mGridMesh = gridMeshGenerator.CreateMesh();
    }

    private GridLayer FindGridLayer(int layerID, bool autoCreate, string mat_path = null)
    {
      for (int index = this.mGridLayers.Count - 1; index >= 0; --index)
      {
        if (this.mGridLayers[index].LayerID == layerID)
          return this.mGridLayers[index];
      }
      if (!autoCreate)
        return (GridLayer) null;
      GridLayer gridLayer = (GridLayer) new GameObject("Grid").AddComponent<GridLayer>();
      ((Component) gridLayer).get_transform().SetParent(((Component) this).get_transform(), false);
      ((MeshFilter) ((Component) gridLayer).GetComponent<MeshFilter>()).set_mesh(this.mGridMesh);
      gridLayer.LayerID = layerID;
      ((Component) gridLayer).get_gameObject().SetActive(false);
      Transform transform = ((Component) gridLayer).get_transform();
      transform.set_position(Vector3.op_Addition(transform.get_position(), Vector3.op_Multiply(Vector3.op_Multiply(Vector3.get_up(), (float) layerID), 0.01f)));
      if (!string.IsNullOrEmpty(mat_path))
        gridLayer.ChangeMaterial(mat_path);
      this.mGridLayers.Add(gridLayer);
      return gridLayer;
    }

    public void HideGridLayer(int layerID)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, false, (string) null);
      if (!Object.op_Inequality((Object) gridLayer, (Object) null))
        return;
      gridLayer.Hide();
    }

    public void HideGridLayers()
    {
      for (int index = this.mGridLayers.Count - 1; index >= 0; --index)
        this.mGridLayers[index].Hide();
    }

    public void ShowGridLayer(int layerID, GridMap<Color32> grid, bool mask)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, true, (string) null);
      gridLayer.UpdateGrid(grid);
      gridLayer.SetMask(mask);
      gridLayer.Show();
    }

    public void ShowGridLayer(int layerID, GridMap<bool> grid, Color32 color, bool mask = false)
    {
      if (grid == null)
        return;
      GridMap<Color32> grid1 = new GridMap<Color32>(grid.w, grid.h);
      for (int x = 0; x < grid.w; ++x)
      {
        for (int y = 0; y < grid.h; ++y)
        {
          if (grid.get(x, y))
            grid1.set(x, y, color);
        }
      }
      this.ShowGridLayer(layerID, grid1, mask);
    }

    public void ShowGridLayer(int layerID, GridMap<Color32> grid, string mat_path)
    {
      GridLayer gridLayer = this.FindGridLayer(layerID, true, mat_path);
      gridLayer.UpdateGrid(grid);
      gridLayer.SetMask(true);
      gridLayer.Show();
    }

    private void ExtendBounds()
    {
    }

    private bool RaycastDeformedMesh(Mesh mesh, Vector3 start, Vector3 direction, Matrix4x4 world, float cameraZ, out float hitDistance, out Vector3 hitPos, out Vector3 hitPosReal)
    {
      hitDistance = float.MaxValue;
      hitPos = Vector3.get_zero();
      hitPosReal = Vector3.get_zero();
      Vector3[] vertices = mesh.get_vertices();
      int[] triangles = mesh.get_triangles();
      int index = 0;
      while (index < triangles.Length)
      {
        Vector3 vector3_1 = vertices[triangles[index]];
        Vector3 vector3_2 = vertices[triangles[index + 1]];
        Vector3 vector3_3 = vertices[triangles[index + 2]];
        // ISSUE: explicit reference operation
        Vector3 pos1 = ((Matrix4x4) @world).MultiplyPoint(vector3_1);
        // ISSUE: explicit reference operation
        Vector3 pos2 = ((Matrix4x4) @world).MultiplyPoint(vector3_2);
        // ISSUE: explicit reference operation
        Vector3 pos3 = ((Matrix4x4) @world).MultiplyPoint(vector3_3);
        Vector3 vector3_4 = GameUtility.DeformPosition(pos1, cameraZ);
        Vector3 vector3_5 = GameUtility.DeformPosition(pos2, cameraZ);
        Vector3 vector3_6 = GameUtility.DeformPosition(pos3, cameraZ);
        Vector3 vector3_7 = Vector3.op_Subtraction(vector3_5, vector3_4);
        // ISSUE: explicit reference operation
        Vector3 normalized1 = ((Vector3) @vector3_7).get_normalized();
        Vector3 vector3_8 = Vector3.op_Subtraction(vector3_6, vector3_4);
        // ISSUE: explicit reference operation
        Vector3 vector3_9 = Vector3.Cross(((Vector3) @vector3_8).get_normalized(), normalized1);
        // ISSUE: explicit reference operation
        Vector3 normalized2 = ((Vector3) @vector3_9).get_normalized();
        float num1 = Vector3.Dot(normalized2, direction);
        if ((double) num1 > 0.0)
        {
          float num2 = Vector3.Dot(Vector3.op_Subtraction(vector3_4, start), normalized2) / num1;
          if ((double) num2 < (double) hitDistance)
          {
            Vector3 vector3_10 = Vector3.op_Addition(start, Vector3.op_Multiply(direction, num2));
            Vector3 vector3_11 = Vector3.op_Subtraction(vector3_4, vector3_10);
            // ISSUE: explicit reference operation
            Vector3 normalized3 = ((Vector3) @vector3_11).get_normalized();
            Vector3 vector3_12 = Vector3.op_Subtraction(vector3_5, vector3_10);
            // ISSUE: explicit reference operation
            Vector3 normalized4 = ((Vector3) @vector3_12).get_normalized();
            Vector3 vector3_13 = Vector3.op_Subtraction(vector3_6, vector3_10);
            // ISSUE: explicit reference operation
            Vector3 normalized5 = ((Vector3) @vector3_13).get_normalized();
            if ((double) (Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized3, normalized4), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized4, normalized5), -1f, 1f)) + Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized5, normalized3), -1f, 1f))) > 6.27318525314331)
            {
              hitDistance = num2;
              hitPos = vector3_10;
              Vector3 vector3_14 = Vector3.op_Subtraction(vector3_10, vector3_4);
              Vector3 vector3_15 = Vector3.op_Subtraction(vector3_6, vector3_5);
              Vector3 vector3_16 = Vector3.op_Subtraction(vector3_4, vector3_5);
              float num3 = Vector3.Dot(vector3_14, vector3_14);
              float num4 = Vector3.Dot(vector3_14, vector3_15);
              float num5 = Vector3.Dot(vector3_15, vector3_15);
              float num6 = (float) ((double) num3 * (double) num5 - (double) num4 * (double) num4);
              float num7 = Vector3.Dot(vector3_14, vector3_16);
              float num8 = Vector3.Dot(vector3_15, vector3_16);
              float num9 = (float) ((double) num4 * (double) num8 - (double) num7 * (double) num5) / num6;
              Vector3 vector3_17 = Vector3.op_Addition(vector3_4, Vector3.op_Multiply(vector3_14, num9));
              Vector3 vector3_18 = Vector3.op_Subtraction(vector3_17, vector3_4);
              float num10 = Vector3.Dot(Vector3.op_Subtraction(vector3_17, vector3_5), vector3_15) / Vector3.Dot(vector3_15, vector3_15);
              float num11 = Vector3.Dot(Vector3.op_Subtraction(vector3_10, vector3_4), vector3_18) / Vector3.Dot(vector3_18, vector3_18);
              hitPosReal = Vector3.Lerp(pos1, Vector3.Lerp(pos2, pos3, num10), num11);
            }
          }
        }
        index += 3;
      }
      return (double) hitDistance < 3.40282346638529E+38;
    }

    public bool RaycastDeformed(Ray ray, out RaycastHit result)
    {
      MeshFilter[] componentsInChildren = (MeshFilter[]) ((Component) this).GetComponentsInChildren<MeshFilter>(false);
      result = (RaycastHit) null;
      // ISSUE: explicit reference operation
      ((RaycastHit) @result).set_distance(float.MaxValue);
      string[] strArray = new string[1]{ "BG" };
      Vector3 position = ((Component) Camera.get_main()).get_transform().get_position();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Mesh sharedMesh = componentsInChildren[index].get_sharedMesh();
        if (!Object.op_Equality((Object) sharedMesh, (Object) null))
        {
          GameObject gameObject = ((Component) componentsInChildren[index]).get_gameObject();
          if ((1 << gameObject.get_layer() & LayerMask.GetMask(strArray)) != 0)
          {
            Transform transform = gameObject.get_transform();
            float hitDistance;
            Vector3 hitPos;
            Vector3 hitPosReal;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            if (this.RaycastDeformedMesh(sharedMesh, ((Ray) @ray).get_origin(), ((Ray) @ray).get_direction(), transform.get_localToWorldMatrix(), (float) position.z, out hitDistance, out hitPos, out hitPosReal) && (double) hitDistance < (double) ((RaycastHit) @result).get_distance())
            {
              // ISSUE: explicit reference operation
              ((RaycastHit) @result).set_point(hitPosReal);
              // ISSUE: explicit reference operation
              ((RaycastHit) @result).set_distance(hitDistance);
            }
          }
        }
      }
      // ISSUE: explicit reference operation
      return (double) ((RaycastHit) @result).get_distance() < 3.40282346638529E+38;
    }
  }
}
