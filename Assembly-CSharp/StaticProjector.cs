// Decompiled with JetBrains decompiler
// Type: StaticProjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Static Projector")]
[RequireComponent(typeof (MeshFilter))]
[ExecuteInEditMode]
[RequireComponent(typeof (MeshRenderer))]
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
