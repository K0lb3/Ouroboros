// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSetting
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class UnitSetting
  {
    public OString uniqname;
    public OString ai;
    public OIntVector2 pos;
    public OInt dir;
    public OInt side;
    public OInt waitEntryClock;
    public OInt waitMoveTurn;
    public OInt waitExitTurn;
    public bool DisableFirceVoice;
    public AIActionType ai_type;
    public OIntVector2 ai_pos;
    public OInt ai_len;
    public EventTrigger trigger;
    public List<UnitEntryTrigger> entries;
    public OInt entries_and;
    public OString parent;

    public UnitSetting()
    {
    }

    public UnitSetting(JSON_MapPartyUnit json)
    {
      this.uniqname = (OString) json.name;
      this.ai = (OString) json.ai;
      this.pos.x = (OInt) json.x;
      this.pos.y = (OInt) json.y;
      this.dir = (OInt) json.dir;
      this.waitEntryClock = (OInt) json.wait_e;
      this.waitMoveTurn = (OInt) json.wait_m;
      this.waitExitTurn = (OInt) json.wait_exit;
      this.DisableFirceVoice = json.fvoff != 0;
      this.side = (OInt) 0;
      this.ai_type = (AIActionType) json.ai_type;
      this.ai_pos.x = (OInt) json.ai_x;
      this.ai_pos.y = (OInt) json.ai_y;
      this.ai_len = (OInt) json.ai_len;
      this.parent = (OString) json.parent;
      if (json.trg != null)
      {
        this.trigger = new EventTrigger();
        this.trigger.Deserialize(json.trg);
      }
      if (json.entries == null || json.entries.Length <= 0)
        return;
      this.entries = new List<UnitEntryTrigger>((IEnumerable<UnitEntryTrigger>) json.entries);
      this.entries_and = (OInt) json.entries_and;
    }
  }
}
