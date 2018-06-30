namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class ShareString
    {
        private List<List<string>> m_string_types;

        public ShareString()
        {
            int num;
            List<string> list;
            this.m_string_types = new List<List<string>>();
            base..ctor();
            num = 0;
            goto Label_002E;
        Label_0018:
            list = new List<string>();
            this.m_string_types.Add(list);
            num += 1;
        Label_002E:
            if (num < 9)
            {
                goto Label_0018;
            }
            return;
        }

        private List<string> ChoiceDicitionary(Type type)
        {
            if (type < this.m_string_types.Count)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            return this.m_string_types[type];
        }

        public string Get(Type type, short index)
        {
            List<string> list;
            list = this.ChoiceDicitionary(type);
            if (index == -1)
            {
                goto Label_001B;
            }
            if (index < list.Count)
            {
                goto Label_0021;
            }
        Label_001B:
            return string.Empty;
        Label_0021:
            return list[index];
        }

        public short Set(Type type, string val)
        {
            List<string> list;
            short num;
            if (string.IsNullOrEmpty(val) == null)
            {
                goto Label_000D;
            }
            return -1;
        Label_000D:
            list = this.ChoiceDicitionary(type);
            num = (short) list.IndexOf(val);
            if (num != -1)
            {
                goto Label_005E;
            }
            if (list.Count < 0x7fff)
            {
                goto Label_004F;
            }
            DebugUtility.LogError("The registered character has exceeded the prescribed value. ShareString.Type = " + ((Type) type) + ", Please change short to int.");
        Label_004F:
            num = (short) list.Count;
            list.Add(val);
        Label_005E:
            return num;
        }

        public enum Type : byte
        {
            QuestParam_cond = 0,
            QuestParam_world = 1,
            QuestParam_area = 2,
            QuestParam_units = 3,
            QuestParam_ticket = 4,
            ChapterParam_world = 5,
            ChapterParam_section = 6,
            MapParam_battleSceneName = 7,
            MapParam_bgmName = 8,
            MAX_TYPE = 9
        }
    }
}

