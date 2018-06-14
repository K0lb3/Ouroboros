// Decompiled with JetBrains decompiler
// Type: OnClickDestroy
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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

  [PunRPC]
  [DebuggerHidden]
  public IEnumerator DestroyRpc()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new OnClickDestroy.\u003CDestroyRpc\u003Ec__IteratorB() { \u003C\u003Ef__this = this };
  }
}
