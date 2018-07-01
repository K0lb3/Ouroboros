// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartySubCT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_MapPartySubCT
  {
    public int ct_calc;
    public int ct_val;

    public void CopyTo(JSON_MapPartySubCT dst)
    {
      dst.ct_calc = this.ct_calc;
      dst.ct_val = this.ct_val;
    }
  }
}
