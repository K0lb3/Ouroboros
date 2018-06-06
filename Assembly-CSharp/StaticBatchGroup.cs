// Decompiled with JetBrains decompiler
// Type: StaticBatchGroup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StaticBatchGroup : MonoBehaviour
{
  private static List<StaticBatchGroup> mInstances = new List<StaticBatchGroup>();
  private const int DEFAULT_INDEX_BUFFER_SIZE = 512;
  private MeshRenderer mMeshRenderer;
  private MeshFilter mMeshFilter;

  public StaticBatchGroup()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    StaticBatchGroup.mInstances.Add(this);
    this.mMeshRenderer = (MeshRenderer) ((Component) this).GetComponent<MeshRenderer>();
    this.mMeshFilter = (MeshFilter) ((Component) this).GetComponent<MeshFilter>();
  }

  private void OnDestroy()
  {
    StaticBatchGroup.mInstances.Remove(this);
  }

  private static float GetScaleSign(Transform tr)
  {
    Vector3 lossyScale = tr.get_lossyScale();
    return Mathf.Sign((float) lossyScale.x) * Mathf.Sign((float) lossyScale.y) * Mathf.Sign((float) lossyScale.z);
  }

  private static Mesh MergeMeshes(List<StaticBatchGroup.Section>[] groups, int numSubMeshes, List<Vector3> verts, List<Vector3> normals, List<Color32> colors, List<Vector2> uvs, List<Vector2> uvs1, List<Vector2> uvs2, List<int>[] newIndices, List<int> indexMap)
  {
    Mesh mesh = new Mesh();
    ((Object) mesh).set_hideFlags((HideFlags) 52);
    mesh.set_subMeshCount(numSubMeshes);
    if (newIndices.Length < numSubMeshes)
    {
      int length = newIndices.Length;
      Array.Resize<List<int>>(ref newIndices, numSubMeshes + 2);
      for (int index = length - 1; index < newIndices.Length; ++index)
        newIndices[index] = new List<int>(512);
    }
    verts.Clear();
    uvs.Clear();
    uvs2.Clear();
    int num1 = 0;
    Transform transform1 = ((Component) groups[0][0].MeshFilter).get_transform();
    Matrix4x4 worldToLocalMatrix = transform1.get_worldToLocalMatrix();
    float scaleSign1 = StaticBatchGroup.GetScaleSign(transform1);
    for (int index1 = 0; index1 < numSubMeshes; ++index1)
    {
      List<StaticBatchGroup.Section> group = groups[index1];
      newIndices[index1].Clear();
      for (int index2 = 0; index2 < group.Count; ++index2)
      {
        Transform transform2 = ((Component) group[index2].MeshFilter).get_transform();
        float scaleSign2 = StaticBatchGroup.GetScaleSign(transform2);
        Matrix4x4 matrix4x4 = Matrix4x4.op_Multiply(worldToLocalMatrix, transform2.get_localToWorldMatrix());
        MeshRenderer meshRenderer = group[index2].MeshRenderer;
        Mesh sharedMesh = group[index2].MeshFilter.get_sharedMesh();
        int[] triangles = sharedMesh.GetTriangles(group[index2].SubMesh);
        if (triangles.Length > 0)
        {
          indexMap.Clear();
          Vector3[] vertices = sharedMesh.get_vertices();
          Vector2[] uv = sharedMesh.get_uv();
          Vector2[] uv2 = sharedMesh.get_uv2();
          LightmapLayout component = (LightmapLayout) ((Component) meshRenderer).GetComponent<LightmapLayout>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            for (int index3 = uv.Length - 1; index3 >= 0; --index3)
            {
              uv2[index3].x = uv2[index3].x * component.Position.x + component.Position.z;
              uv2[index3].y = uv2[index3].y * component.Position.y + component.Position.w;
            }
          }
          int num2;
          int num3;
          int num4;
          if ((double) scaleSign1 * (double) scaleSign2 < 0.0)
          {
            num2 = triangles.Length - 1;
            num3 = -1;
            num4 = -1;
          }
          else
          {
            num2 = 0;
            num3 = triangles.Length;
            num4 = 1;
          }
          int index4 = num2;
          while (index4 != num3)
          {
            int index3 = triangles[index4];
            int num5 = indexMap.IndexOf(index3);
            if (num5 >= 0)
            {
              newIndices[index1].Add(num5 + num1);
            }
            else
            {
              // ISSUE: explicit reference operation
              verts.Add(((Matrix4x4) @matrix4x4).MultiplyPoint(vertices[index3]));
              if (uv != null)
                uvs.Add(uv[index3]);
              else
                uvs.Add(Vector2.get_zero());
              if (uv2 != null)
                uvs2.Add(uv2[index3]);
              else
                uvs2.Add(Vector2.get_zero());
              newIndices[index1].Add(indexMap.Count + num1);
              indexMap.Add(index3);
            }
            index4 += num4;
          }
          num1 += indexMap.Count;
        }
      }
    }
    mesh.set_vertices(verts.ToArray());
    mesh.set_uv(uvs.ToArray());
    mesh.set_uv2(uvs2.ToArray());
    for (int index = 0; index < numSubMeshes; ++index)
      mesh.SetTriangles(newIndices[index].ToArray(), index);
    mesh.UploadMeshData(true);
    return mesh;
  }

  private static Mesh[] GenerateBatch()
  {
    List<StaticBatchGroup.Section> sectionList = new List<StaticBatchGroup.Section>(64);
    List<Mesh> meshList = new List<Mesh>();
    for (int index1 = 0; index1 < StaticBatchGroup.mInstances.Count; ++index1)
    {
      MeshRenderer mMeshRenderer = StaticBatchGroup.mInstances[index1].mMeshRenderer;
      if (!Object.op_Equality((Object) mMeshRenderer, (Object) null) && ((Renderer) mMeshRenderer).get_sharedMaterials().Length > 0)
      {
        MeshFilter mMeshFilter = StaticBatchGroup.mInstances[index1].mMeshFilter;
        if (!Object.op_Equality((Object) mMeshFilter, (Object) null) && !Object.op_Equality((Object) mMeshFilter.get_sharedMesh(), (Object) null))
        {
          for (int index2 = 0; index2 < ((Renderer) mMeshRenderer).get_sharedMaterials().Length; ++index2)
          {
            Material sharedMaterial = ((Renderer) mMeshRenderer).get_sharedMaterials()[index2];
            if (!Object.op_Equality((Object) sharedMaterial, (Object) null))
              sectionList.Add(new StaticBatchGroup.Section()
              {
                Material = sharedMaterial,
                SubMesh = index2,
                MeshFilter = mMeshFilter,
                MeshRenderer = mMeshRenderer
              });
          }
        }
      }
    }
    for (int index = 0; index < StaticBatchGroup.mInstances.Count; ++index)
      Object.Destroy((Object) StaticBatchGroup.mInstances[index]);
    List<StaticBatchGroup.Section>[] array = (List<StaticBatchGroup.Section>[]) null;
    List<Vector3> verts = new List<Vector3>();
    List<Vector3> normals = (List<Vector3>) null;
    List<Color32> colors = (List<Color32>) null;
    List<Vector2> uvs1 = (List<Vector2>) null;
    List<int> indexMap = new List<int>(1024);
    List<int>[] newIndices = new List<int>[8];
    for (int index = 0; index < newIndices.Length; ++index)
      newIndices[index] = new List<int>(512);
    List<Vector2> uvs = new List<Vector2>();
    List<Vector2> uvs2 = new List<Vector2>();
    for (int index1 = 0; index1 < sectionList.Count; ++index1)
    {
      if (!sectionList[index1].Merged)
      {
        Material[] sharedMaterials = ((Renderer) sectionList[index1].MeshRenderer).get_sharedMaterials();
        if (array == null)
          array = new List<StaticBatchGroup.Section>[sharedMaterials.Length * 2];
        else if (array.Length < sharedMaterials.Length)
          Array.Resize<List<StaticBatchGroup.Section>>(ref array, sharedMaterials.Length + 2);
        for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
        {
          if (!Object.op_Equality((Object) sharedMaterials[index2], (Object) null))
          {
            if (array[index2] != null)
              array[index2].Clear();
            else
              array[index2] = new List<StaticBatchGroup.Section>(8);
            for (int index3 = index1; index3 < sectionList.Count; ++index3)
            {
              if (!Object.op_Inequality((Object) sharedMaterials[index2], (Object) sectionList[index3].Material) && !sectionList[index3].Merged)
              {
                sectionList[index3].Merged = true;
                ((Renderer) sectionList[index3].MeshRenderer).set_enabled(false);
                array[index2].Add(sectionList[index3]);
              }
            }
          }
        }
        int length = sharedMaterials.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (array[index2].Count <= 0)
          {
            array[index2] = array[length - 1];
            --length;
            --index2;
          }
        }
        if (length <= 0)
        {
          ((Renderer) sectionList[index1].MeshRenderer).set_enabled(false);
        }
        else
        {
          Mesh mesh = StaticBatchGroup.MergeMeshes(array, length, verts, normals, colors, uvs, uvs1, uvs2, newIndices, indexMap);
          sectionList[index1].MeshFilter.set_sharedMesh(mesh);
          meshList.Add(mesh);
          Material[] materialArray = new Material[length];
          for (int index2 = 0; index2 < length; ++index2)
            materialArray[index2] = array[index2][0].Material;
          LightmapLayout component = (LightmapLayout) ((Component) sectionList[index1].MeshRenderer).GetComponent<LightmapLayout>();
          component.Position = new Vector4(1f, 1f, 0.0f, 0.0f);
          component.ApplyLayout();
          ((Renderer) sectionList[index1].MeshRenderer).set_lightmapScaleOffset(new Vector4(1f, 1f, 0.0f, 0.0f));
          ((Renderer) sectionList[index1].MeshRenderer).set_sharedMaterials(materialArray);
          ((Renderer) sectionList[index1].MeshRenderer).set_enabled(true);
        }
      }
    }
    return meshList.ToArray();
  }

  private void Start()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    StaticBatchGroup.\u003CStart\u003Ec__AnonStorey206 startCAnonStorey206 = new StaticBatchGroup.\u003CStart\u003Ec__AnonStorey206();
    if (Object.op_Inequality((Object) this, (Object) StaticBatchGroup.mInstances[StaticBatchGroup.mInstances.Count - 1]))
      return;
    // ISSUE: reference to a compiler-generated field
    startCAnonStorey206.meshes = StaticBatchGroup.GenerateBatch();
    // ISSUE: reference to a compiler-generated field
    if (startCAnonStorey206.meshes == null)
      return;
    // ISSUE: reference to a compiler-generated method
    ((DestroyEventListener) ((Component) this).get_gameObject().AddComponent<DestroyEventListener>()).Listeners += new DestroyEventListener.DestroyEvent(startCAnonStorey206.\u003C\u003Em__1DB);
  }

  private class Section
  {
    public Material Material;
    public MeshRenderer MeshRenderer;
    public MeshFilter MeshFilter;
    public int SubMesh;
    public bool Merged;
  }
}
