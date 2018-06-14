// Decompiled with JetBrains decompiler
// Type: HighlightOwnedGameObj
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class HighlightOwnedGameObj : MonoBehaviour
{
  public float Offset = 0.5f;
  public GameObject PointerPrefab;
  private Transform markerTransform;

  private void Update()
  {
    if (this.photonView.isMine)
    {
      if (Object.op_Equality((Object) this.markerTransform, (Object) null))
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.PointerPrefab);
        gameObject.get_transform().set_parent(((Component) this).get_gameObject().get_transform());
        this.markerTransform = gameObject.get_transform();
      }
      Vector3 position = ((Component) this).get_gameObject().get_transform().get_position();
      this.markerTransform.set_position(new Vector3((float) position.x, (float) position.y + this.Offset, (float) position.z));
      this.markerTransform.set_rotation(Quaternion.get_identity());
    }
    else
    {
      if (!Object.op_Inequality((Object) this.markerTransform, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.markerTransform).get_gameObject());
      this.markerTransform = (Transform) null;
    }
  }
}
