// Decompiled with JetBrains decompiler
// Type: OnAwakeUsePhotonView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class OnAwakeUsePhotonView : MonoBehaviour
{
  private void Awake()
  {
    if (!this.photonView.isMine)
      return;
    this.photonView.RPC("OnAwakeRPC", PhotonTargets.All);
  }

  private void Start()
  {
    if (!this.photonView.isMine)
      return;
    this.photonView.RPC("OnAwakeRPC", PhotonTargets.All, new object[1]
    {
      (object) (byte) 1
    });
  }

  [PunRPC]
  public void OnAwakeRPC()
  {
    Debug.Log((object) ("RPC: 'OnAwakeRPC' PhotonView: " + (object) this.photonView));
  }

  [PunRPC]
  public void OnAwakeRPC(byte myParameter)
  {
    Debug.Log((object) ("RPC: 'OnAwakeRPC' Parameter: " + (object) myParameter + " PhotonView: " + (object) this.photonView));
  }
}
