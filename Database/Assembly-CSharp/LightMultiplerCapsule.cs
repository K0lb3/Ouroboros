// Decompiled with JetBrains decompiler
// Type: LightMultiplerCapsule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Rendering/Light Multipler Capsule")]
public class LightMultiplerCapsule : LightMultipler
{
  [SerializeField]
  public List<Vector3> mPoints = new List<Vector3>();
}
