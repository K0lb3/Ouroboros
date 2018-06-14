// Decompiled with JetBrains decompiler
// Type: OnClickDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class OnClickDestroy : MonoBehaviour
{
  public bool DestroyByRpc;

  public void OnClick()
  {
    if (!this.DestroyByRpc)
      PhotonNetwork.Destroy(((Component) this).get_gameObject());
    else
      this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
  }

  [DebuggerHidden]
  [PunRPC]
  public IEnumerator DestroyRpc()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new OnClickDestroy.\u003CDestroyRpc\u003Ec__Iterator2A()
    {
      \u003C\u003Ef__this = this
    };
  }
}
