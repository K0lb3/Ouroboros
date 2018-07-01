// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartySubCT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
