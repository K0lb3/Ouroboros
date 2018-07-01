// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraParamLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitTobiraParamLevel : MonoBehaviour
  {
    [SerializeField]
    private GameObject OnGO;
    [SerializeField]
    private GameObject OffGO;
    [SerializeField]
    private int OwnLevel;

    public UnitTobiraParamLevel()
    {
      base.\u002Ector();
    }

    public void Refresh(int targetLevel)
    {
      if (Object.op_Equality((Object) this.OnGO, (Object) null) || Object.op_Equality((Object) this.OffGO, (Object) null))
        return;
      bool flag = targetLevel >= this.OwnLevel;
      this.OnGO.SetActive(flag);
      this.OffGO.SetActive(!flag);
    }
  }
}
