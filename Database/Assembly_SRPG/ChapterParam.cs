namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class ChapterParam
    {
        public string iname;
        public string name;
        public string expr;
        private short world_index;
        public long start;
        public long end;
        public long key_end;
        public bool hidden;
        private short section_index;
        public string banner;
        public string prefabPath;
        public ChapterParam parent;
        public List<ChapterParam> children;
        public List<QuestParam> quests;
        public SectionParam sectionParam;
        public List<KeyItem> keys;
        public long keytime;
        public string helpURL;

        public ChapterParam()
        {
            this.world_index = -1;
            this.section_index = -1;
            this.children = new List<ChapterParam>();
            this.quests = new List<QuestParam>();
            base..ctor();
            return;
        }

        public bool CheckHasKeyItem()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            if (this.keys[num].IsHasItem() == null)
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.keys.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public void Deserialize(JSON_ChapterParam json)
        {
            KeyItem item;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.world = json.world;
            this.start = json.start;
            this.end = json.end;
            this.hidden = (json.hide == 0) == 0;
            this.section = json.chap;
            this.banner = json.banr;
            this.prefabPath = json.item;
            this.helpURL = json.hurl;
            this.keys = new List<KeyItem>();
            if (string.IsNullOrEmpty(json.keyitem1) != null)
            {
                goto Label_00E7;
            }
            if (json.keynum1 <= 0)
            {
                goto Label_00E7;
            }
            item = new KeyItem();
            item.iname = json.keyitem1;
            item.num = json.keynum1;
            this.keys.Add(item);
        Label_00E7:
            if (this.keys.Count <= 0)
            {
                goto Label_0104;
            }
            this.keytime = json.keytime;
        Label_0104:
            this.quests.Clear();
            return;
        }

        public KeyQuestTypes GetKeyQuestType()
        {
            if (this.IsKeyQuest() == null)
            {
                goto Label_001A;
            }
            if (this.keytime == null)
            {
                goto Label_0018;
            }
            return 1;
        Label_0018:
            return 2;
        Label_001A:
            return 0;
        }

        public SubQuestTypes GetSubQuestType()
        {
            if (this.quests == null)
            {
                goto Label_002E;
            }
            if (this.quests.Count <= 0)
            {
                goto Label_002E;
            }
            return this.quests[0].subtype;
        Label_002E:
            if (this.children == null)
            {
                goto Label_005C;
            }
            if (this.children.Count <= 0)
            {
                goto Label_005C;
            }
            return this.children[0].GetSubQuestType();
        Label_005C:
            return 0;
        }

        public bool HasGpsQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_003F;
            }
            num = 0;
            goto Label_002E;
        Label_0012:
            if (this.quests[num].gps_enable == null)
            {
                goto Label_002A;
            }
            return 1;
        Label_002A:
            num += 1;
        Label_002E:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_003F:
            num2 = 0;
            goto Label_0062;
        Label_0046:
            if (this.children[num2].HasGpsQuest() == null)
            {
                goto Label_005E;
            }
            return 1;
        Label_005E:
            num2 += 1;
        Label_0062:
            if (num2 < this.children.Count)
            {
                goto Label_0046;
            }
            return 0;
        }

        public bool IsAvailable(DateTime t)
        {
            DateTime time;
            DateTime time2;
            if (this.end > 0L)
            {
                goto Label_0017;
            }
            return (this.hidden == 0);
        Label_0017:
            time = TimeManager.FromUnixTime(this.start);
            time2 = TimeManager.FromUnixTime(this.end);
            return (((time <= t) == null) ? 0 : (t < time2));
        }

        public bool IsBeginnerQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_0041;
            }
            num = 0;
            goto Label_0030;
        Label_0012:
            if (this.quests[num].type != 13)
            {
                goto Label_002C;
            }
            return 1;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_0041:
            num2 = 0;
            goto Label_0064;
        Label_0048:
            if (this.children[num2].IsBeginnerQuest() == null)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            num2 += 1;
        Label_0064:
            if (num2 < this.children.Count)
            {
                goto Label_0048;
            }
            return 0;
        }

        public bool IsDateUnlock(long unixtime)
        {
            int num;
            num = 0;
            goto Label_0024;
        Label_0007:
            if (this.quests[num].IsDateUnlock(unixtime) == null)
            {
                goto Label_0020;
            }
            return 1;
        Label_0020:
            num += 1;
        Label_0024:
            if (num < this.quests.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool IsGpsQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_0041;
            }
            num = 0;
            goto Label_0030;
        Label_0012:
            if (this.quests[num].type != 10)
            {
                goto Label_002C;
            }
            return 1;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_0041:
            num2 = 0;
            goto Label_0064;
        Label_0048:
            if (this.children[num2].IsGpsQuest() == null)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            num2 += 1;
        Label_0064:
            if (num2 < this.children.Count)
            {
                goto Label_0048;
            }
            return 0;
        }

        public bool IsKeyQuest()
        {
            return (this.keys.Count > 0);
        }

        public bool IsKeyUnlock(long unixtime)
        {
            KeyQuestTypes types;
            int num;
            KeyQuestTypes types2;
            if (this.IsKeyQuest() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.IsDateUnlock(unixtime) != null)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            types = this.GetKeyQuestType();
            if (this.key_end > 0L)
            {
                goto Label_0031;
            }
            return 0;
        Label_0031:
            types2 = types;
            if (types2 == 1)
            {
                goto Label_0046;
            }
            if (types2 == 2)
            {
                goto Label_0050;
            }
            goto Label_0086;
        Label_0046:
            return (unixtime < this.key_end);
        Label_0050:
            num = 0;
            goto Label_0073;
        Label_0057:
            if (this.quests[num].CheckEnableChallange() == null)
            {
                goto Label_006F;
            }
            return 1;
        Label_006F:
            num += 1;
        Label_0073:
            if (num < this.quests.Count)
            {
                goto Label_0057;
            }
            return 0;
        Label_0086:
            return 0;
        }

        public bool IsMultiGpsQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_0041;
            }
            num = 0;
            goto Label_0030;
        Label_0012:
            if (this.quests[num].type != 14)
            {
                goto Label_002C;
            }
            return 1;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_0041:
            num2 = 0;
            goto Label_0064;
        Label_0048:
            if (this.children[num2].IsMultiGpsQuest() == null)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            num2 += 1;
        Label_0064:
            if (num2 < this.children.Count)
            {
                goto Label_0048;
            }
            return 0;
        }

        public bool IsOrdealQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_0041;
            }
            num = 0;
            goto Label_0030;
        Label_0012:
            if (this.quests[num].type != 15)
            {
                goto Label_002C;
            }
            return 1;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_0041:
            num2 = 0;
            goto Label_0064;
        Label_0048:
            if (this.children[num2].IsOrdealQuest() == null)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            num2 += 1;
        Label_0064:
            if (num2 < this.children.Count)
            {
                goto Label_0048;
            }
            return 0;
        }

        public bool IsTowerQuest()
        {
            int num;
            int num2;
            if (this.quests == null)
            {
                goto Label_0058;
            }
            num = 0;
            goto Label_0047;
        Label_0012:
            if (this.quests[num].type == 7)
            {
                goto Label_0041;
            }
            if (this.quests[num].type != 12)
            {
                goto Label_0043;
            }
        Label_0041:
            return 1;
        Label_0043:
            num += 1;
        Label_0047:
            if (num < this.quests.Count)
            {
                goto Label_0012;
            }
        Label_0058:
            num2 = 0;
            goto Label_007B;
        Label_005F:
            if (this.children[num2].IsTowerQuest() == null)
            {
                goto Label_0077;
            }
            return 1;
        Label_0077:
            num2 += 1;
        Label_007B:
            if (num2 < this.children.Count)
            {
                goto Label_005F;
            }
            return 0;
        }

        public string world
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(5, this.world_index);
            }
            set
            {
                this.world_index = Singleton<ShareVariable>.Instance.str.Set(5, value);
                return;
            }
        }

        public string section
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(6, this.section_index);
            }
            set
            {
                this.section_index = Singleton<ShareVariable>.Instance.str.Set(6, value);
                return;
            }
        }
    }
}

