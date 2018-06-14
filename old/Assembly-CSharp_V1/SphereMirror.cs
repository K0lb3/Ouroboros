// Decompiled with JetBrains decompiler
// Type: SphereMirror
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SphereMirror : MonoBehaviour
{
  public SphereMirror()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    Vector2[] uv = ((MeshFilter) ((Component) ((Component) this).get_transform()).GetComponent<MeshFilter>()).get_mesh().get_uv();
    for (int index = 0; index < uv.Length; ++index)
      uv[index] = new Vector2((float) (1.0 - uv[index].x), (float) uv[index].y);
    ((MeshFilter) ((Component) ((Component) this).get_transform()).GetComponent<MeshFilter>()).get_mesh().set_uv(uv);
  }

  private void Update()
  {
  }
}
