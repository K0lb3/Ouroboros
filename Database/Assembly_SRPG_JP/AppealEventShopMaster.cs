// Decompiled with JetBrains decompiler
// Type: SRPG.AppealEventShopMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class AppealEventShopMaster
  {
    public string appeal_id;
    public long start_at;
    public long end_at;
    public int priority;
    public float position_chara;
    public float position_text;

    public bool Deserialize(JSON_AppealEventShopMaster json)
    {
      if (json == null)
        return false;
      this.appeal_id = json.fields.appeal_id;
      this.start_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.start_at));
      this.end_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.end_at));
      this.priority = json.fields.priority;
      this.position_chara = json.fields.position_chara;
      this.position_text = json.fields.position_text;
      return true;
    }
  }
}
