// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakusei
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitKakusei : MonoBehaviour
  {
    public GameObject JobUnlock;
    public JobParam UnlockJobParam;

    public UnitKakusei()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.JobUnlock, (Object) null))
        return;
      bool flag = false;
      if (this.UnlockJobParam != null)
      {
        DataSource.Bind<JobParam>(this.JobUnlock, this.UnlockJobParam);
        flag = true;
      }
      this.JobUnlock.SetActive(flag);
    }
  }
}
