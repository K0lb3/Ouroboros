// Decompiled with JetBrains decompiler
// Type: SRPG.ChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ChapterParam
  {
    private short world_index = -1;
    private short section_index = -1;
    public List<ChapterParam> children = new List<ChapterParam>();
    public List<QuestParam> quests = new List<QuestParam>();
    public string iname;
    public string name;
    public string expr;
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

    public string world
    {
      set
      {
        this.world_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.ChapterParam_world, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.ChapterParam_world, this.world_index);
      }
    }

    public string section
    {
      set
      {
        this.section_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.ChapterParam_section, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.ChapterParam_section, this.section_index);
      }
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

    public bool IsGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Gps)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsGpsQuest())
          return true;
      }
      return false;
    }

    public bool IsTowerQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Tower || this.quests[index].type == QuestTypes.MultiTower)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsTowerQuest())
          return true;
      }
      return false;
    }

    public bool IsBeginnerQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Beginner)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsBeginnerQuest())
          return true;
      }
      return false;
    }

    public bool IsMultiGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.MultiGps)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsMultiGpsQuest())
          return true;
      }
      return false;
    }

    public bool IsOrdealQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].type == QuestTypes.Ordeal)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].IsOrdealQuest())
          return true;
      }
      return false;
    }

    public SubQuestTypes GetSubQuestType()
    {
      if (this.quests != null && this.quests.Count > 0)
        return this.quests[0].subtype;
      if (this.children != null && this.children.Count > 0)
        return this.children[0].GetSubQuestType();
      return SubQuestTypes.Normal;
    }

    public bool HasGpsQuest()
    {
      if (this.quests != null)
      {
        for (int index = 0; index < this.quests.Count; ++index)
        {
          if (this.quests[index].gps_enable)
            return true;
        }
      }
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].HasGpsQuest())
          return true;
      }
      return false;
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
