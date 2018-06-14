// Decompiled with JetBrains decompiler
// Type: MapPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class MapPickup : MonoBehaviour
{
  public MapPickup.PickupEvent OnPickup;
  public Transform Shadow;
  public Vector3 DropEffectOffset;

  public MapPickup()
  {
    base.\u002Ector();
  }

  public delegate void PickupEvent();
}
