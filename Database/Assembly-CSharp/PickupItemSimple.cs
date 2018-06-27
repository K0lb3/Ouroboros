// Decompiled with JetBrains decompiler
// Type: PickupItemSimple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class PickupItemSimple : MonoBehaviour
{
  public float SecondsBeforeRespawn = 2f;
  public bool PickupOnCollide;
  public bool SentPickup;

  public void OnTriggerEnter(Collider other)
  {
    PhotonView component = (PhotonView) ((Component) other).GetComponent<PhotonView>();
    if (!this.PickupOnCollide || !Object.op_Inequality((Object) component, (Object) null) || !component.isMine)
      return;
    this.Pickup();
  }

  public void Pickup()
  {
    if (this.SentPickup)
      return;
    this.SentPickup = true;
    this.photonView.RPC("PunPickupSimple", PhotonTargets.AllViaServer);
  }

  [PunRPC]
  public void PunPickupSimple(PhotonMessageInfo msgInfo)
  {
    if (!this.SentPickup || !msgInfo.sender.IsLocal || !((Component) this).get_gameObject().GetActive())
      ;
    this.SentPickup = false;
    if (!((Component) this).get_gameObject().GetActive())
    {
      Debug.Log((object) ("Ignored PU RPC, cause item is inactive. " + (object) ((Component) this).get_gameObject()));
    }
    else
    {
      float num = this.SecondsBeforeRespawn - (float) (PhotonNetwork.time - msgInfo.timestamp);
      if ((double) num <= 0.0)
        return;
      ((Component) this).get_gameObject().SetActive(false);
      this.Invoke("RespawnAfter", num);
    }
  }

  public void RespawnAfter()
  {
    if (!Object.op_Inequality((Object) ((Component) this).get_gameObject(), (Object) null))
      return;
    ((Component) this).get_gameObject().SetActive(true);
  }
}
