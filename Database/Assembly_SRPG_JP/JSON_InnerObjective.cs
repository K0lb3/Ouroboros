// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_InnerObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_InnerObjective
  {
    public int type;
    public string val;
    public string item;
    public int num;
    public int item_type;
    public int is_takeover_progress;

    public bool IsTakeoverProgress
    {
      get
      {
        return this.is_takeover_progress == 1;
      }
    }
  }
}
