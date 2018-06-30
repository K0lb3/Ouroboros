namespace SRPG
{
    using System;

    public class ChatBlackList
    {
        public ChatBlackListParam[] lists;
        public int total;

        public ChatBlackList()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatBlackList json)
        {
            int num;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.lists = null;
            if (json.blacklist == null)
            {
                goto Label_005A;
            }
            this.lists = new ChatBlackListParam[(int) json.blacklist.Length];
            num = 0;
            goto Label_0047;
        Label_0033:
            this.lists[num] = json.blacklist[num];
            num += 1;
        Label_0047:
            if (num < ((int) json.blacklist.Length))
            {
                goto Label_0033;
            }
            goto Label_0066;
        Label_005A:
            this.lists = new ChatBlackListParam[0];
        Label_0066:
            this.total = json.total;
            return;
        }
    }
}

