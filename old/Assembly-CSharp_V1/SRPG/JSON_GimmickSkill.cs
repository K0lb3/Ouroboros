// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GimmickSkill
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_GimmickSkill
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
    public int type;
    public int count;
  }
}
