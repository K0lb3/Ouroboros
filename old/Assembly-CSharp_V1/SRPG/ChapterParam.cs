// Decompiled with JetBrains decompiler
// Type: SRPG.ChapterParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ChapterParam
  {
    public string section = string.Empty;
    public List<QuestParam> quests = new List<QuestParam>();
    private string localizedNameID;
    private string localizedExprID;
    public string iname;
    public string name;
    public string expr;
    public string world;
    public long start;
    public long end;
    public long key_end;
    public bool hidden;
    public string banner;
    public string prefabPath;
    public ChapterParam parent;
    public SectionParam sectionParam;
    public List<KeyItem> keys;
    public long keytime;
    public string helpURL;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedNameID);
      this.expr = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedExprID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.iname, "EXPR");
    }

    public void Deserialize(string language, JSON_ChapterParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public void Deserialize(JSON_ChapterParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.world = json.world;
      this.start = json.start;
      this.end = json.end;
      this.hidden = json.hide != 0;
      this.section = json.chap;
      this.banner = json.banr;
      this.prefabPath = json.item;
      this.helpURL = json.hurl;
      this.keys = new List<KeyItem>();
      if (!string.IsNullOrEmpty(json.keyitem1) && json.keynum1 > 0)
        this.keys.Add(new KeyItem()
        {
          iname = json.keyitem1,
          num = json.keynum1
        });
      if (this.keys.Count > 0)
        this.keytime = json.keytime;
      this.quests.Clear();
    }

    public bool IsAvailable(DateTime t)
    {
      if (this.end <= 0L)
        return !this.hidden;
      DateTime dateTime1 = TimeManager.FromUnixTime(this.start);
      DateTime dateTime2 = TimeManager.FromUnixTime(this.end);
      if (dateTime1 <= t)
        return t < dateTime2;
      return false;
    }

    public bool IsKeyQuest()
    {
      return this.keys.Count > 0;
    }

    public KeyQuestTypes GetKeyQuestType()
    {
      if (!this.IsKeyQuest())
        return KeyQuestTypes.None;
      return this.keytime != 0L ? KeyQuestTypes.Timer : KeyQuestTypes.Count;
    }

    public bool IsDateUnlock(long unixtime)
    {
      for (int index = 0; index < this.quests.Count; ++index)
      {
        if (this.quests[index].IsDateUnlock(unixtime))
          return true;
      }
      return false;
    }

    public bool IsKeyUnlock(long unixtime)
    {
      if (!this.IsKeyQuest() || !this.IsDateUnlock(unixtime))
        return false;
      KeyQuestTypes keyQuestType = this.GetKeyQuestType();
      if (this.key_end <= 0L)
        return false;
      switch (keyQuestType)
      {
        case KeyQuestTypes.Timer:
          return unixtime < this.key_end;
        case KeyQuestTypes.Count:
          for (int index = 0; index < this.quests.Count; ++index)
          {
            if (this.quests[index].CheckEnableChallange())
              return true;
          }
          return false;
        default:
          return false;
      }
    }

    public bool CheckHasKeyItem()
    {
      for (int index = 0; index < this.keys.Count; ++index)
      {
        if (this.keys[index].IsHasItem())
          return true;
      }
      return false;
    }
  }
}
