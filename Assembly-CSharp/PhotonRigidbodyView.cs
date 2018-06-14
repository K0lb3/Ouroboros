// Decompiled with JetBrains decompiler
// Type: PhotonRigidbodyView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Photon Networking/Photon Rigidbody View")]
[RequireComponent(typeof (PhotonView))]
[RequireComponent(typeof (Rigidbody))]
public class PhotonRigidbodyView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  private bool m_SynchronizeVelocity;
  [SerializeField]
  private bool m_SynchronizeAngularVelocity;
  private Rigidbody m_Body;

  public PhotonRigidbodyView()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    this.m_Body = (Rigidbody) ((Component) this).GetComponent<Rigidbody>();
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      if (this.m_SynchronizeVelocity)
        stream.SendNext((object) this.m_Body.get_velocity());
      if (!this.m_SynchronizeAngularVelocity)
        return;
      stream.SendNext((object) this.m_Body.get_angularVelocity());
    }
    else
    {
      if (this.m_SynchronizeVelocity)
        this.m_Body.set_velocity((Vector3) stream.ReceiveNext());
      if (!this.m_SynchronizeAngularVelocity)
        return;
      this.m_Body.set_angularVelocity((Vector3) stream.ReceiveNext());
    }
  }
}
