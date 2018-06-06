// Decompiled with JetBrains decompiler
// Type: Photon.MonoBehaviour
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
