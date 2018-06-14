// Decompiled with JetBrains decompiler
// Type: StaticProjector
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (MeshRenderer))]
[ExecuteInEditMode]
[AddComponentMenu("Rendering/Static Projector")]
[RequireComponent(typeof (MeshFilter))]
public class StaticProjector : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  private Mesh mMesh;
  public float FOVAngle;
  public float FarPlane;

  public StaticProjector()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    if (!Application.get_isPlaying())
      return;
    ((Behaviour) this).set_enabled(false);
  }
}
