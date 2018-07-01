// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_GimmickEvent
  {
    public string skill = string.Empty;
    public string su_iname = string.Empty;
    public string su_tag = string.Empty;
    public string st_iname = string.Empty;
    public string st_tag = string.Empty;
    public string cu_iname = string.Empty;
    public string cu_tag = string.Empty;
    public string ct_iname = string.Empty;
    public string ct_tag = string.Empty;
    public int[] x = new int[1];
    public int[] y = new int[1];
    public int ev_type;
    public int type;
    public int count;
  }
}
