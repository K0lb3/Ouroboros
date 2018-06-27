// Decompiled with JetBrains decompiler
// Type: SmoothSyncMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class SmoothSyncMovement : MonoBehaviour, IPunObservable
{
  public float SmoothingDelay = 5f;
  private Vector3 correctPlayerPos = Vector3.get_zero();
  private Quaternion correctPlayerRot = Quaternion.get_identity();

  public void Awake()
  {
    bool flag = false;
    using (List<Component>.Enumerator enumerator = this.photonView.ObservedComponents.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        if (Object.op_Equality((Object) enumerator.Current, (Object) this))
        {
          flag = true;
          break;
        }
      }
    }
    if (flag)
      return;
    Debug.LogWarning((object) (this.ToString() + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used."));
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
    }
    else
    {
      this.correctPlayerPos = (Vector3) stream.ReceiveNext();
      this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
    }
  }

  public void Update()
  {
    if (this.photonView.isMine)
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctPlayerPos, Time.get_deltaTime() * this.SmoothingDelay));
    ((Component) this).get_transform().set_rotation(Quaternion.Lerp(((Component) this).get_transform().get_rotation(), this.correctPlayerRot, Time.get_deltaTime() * this.SmoothingDelay));
  }
}
