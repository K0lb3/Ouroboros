namespace SRPG
{
    using System;

    public class SectionParam
    {
        public string iname;
        public string name;
        public string expr;
        public long start;
        public long end;
        public bool hidden;
        public string home;
        public string unit;
        public string prefabPath;
        public string shop;
        public string inn;
        public string bar;
        public string bgm;
        public int storyPart;
        public string releaseKeyQuest;

        public SectionParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_SectionParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.start = json.start;
            this.end = json.end;
            this.hidden = (json.hide == 0) == 0;
            this.home = json.home;
            this.unit = json.unit;
            this.prefabPath = json.item;
            this.shop = json.shop;
            this.inn = json.inn;
            this.bar = json.bar;
            this.bgm = json.bgm;
            this.storyPart = json.story_part;
            this.releaseKeyQuest = json.release_key_quest;
            return;
        }

        public bool IsDateUnlock()
        {
            long num;
            num = Network.GetServerTime();
            if (this.end == null)
            {
                goto Label_002D;
            }
            if (this.start > num)
            {
                goto Label_002B;
            }
            if (num >= this.end)
            {
                goto Label_002B;
            }
            return 1;
        Label_002B:
            return 0;
        Label_002D:
            return (this.hidden == 0);
        }
    }
}

