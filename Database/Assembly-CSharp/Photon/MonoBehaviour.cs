// Decompiled with JetBrains decompiler
// Type: Photon.MonoBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Photon
{
  public class MonoBehaviour : MonoBehaviour
  {
    private PhotonView pvCache;

    public MonoBehaviour()
    {
      base.\u002Ector();
    }

    public PhotonView photonView
    {
      get
      {
        if (Object.op_Equality((Object) this.pvCache, (Object) null))
          this.pvCache = PhotonView.Get((Component) this);
        return this.pvCache;
      }
    }
  }
}
