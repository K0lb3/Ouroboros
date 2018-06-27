// Decompiled with JetBrains decompiler
// Type: ManualPhotonViewAllocator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
  public GameObject Prefab;

  public ManualPhotonViewAllocator()
  {
    base.\u002Ector();
  }

  public void AllocateManualPhotonView()
  {
    PhotonView photonView = ((Component) this).get_gameObject().GetPhotonView();
    if (Object.op_Equality((Object) photonView, (Object) null))
    {
      Debug.LogError((object) "Can't do manual instantiation without PhotonView component.");
    }
    else
    {
      int num = PhotonNetwork.AllocateViewID();
      photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, new object[1]
      {
        (object) num
      });
    }
  }

  [PunRPC]
  public void InstantiateRpc(int viewID)
  {
    GameObject go = Object.Instantiate((Object) this.Prefab, Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.get_identity()) as GameObject;
    go.GetPhotonView().viewID = viewID;
    ((OnClickDestroy) go.GetComponent<OnClickDestroy>()).DestroyByRpc = true;
  }
}
