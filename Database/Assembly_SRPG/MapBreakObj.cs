// Decompiled with JetBrains decompiler
// Type: SRPG.MapBreakObj
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class MapBreakObj
  {
    public int is_ui = 1;
    public int clash_type;
    public int ai_type;
    public int side_type;
    public int ray_type;
    public int max_hp;
    public int[] rest_hps;

    public void CopyTo(MapBreakObj dst)
    {
      dst.clash_type = this.clash_type;
      dst.ai_type = this.ai_type;
      dst.side_type = this.side_type;
      dst.ray_type = this.ray_type;
      dst.is_ui = this.is_ui;
      dst.max_hp = this.max_hp;
      if (this.rest_hps == null || this.rest_hps.Length == 0)
        return;
      dst.rest_hps = new int[this.rest_hps.Length];
      for (int index = 0; index < this.rest_hps.Length; ++index)
        dst.rest_hps[index] = this.rest_hps[index];
    }
  }
}
