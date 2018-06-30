namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ChatLog
    {
        private List<ChatLogParam> add_log_list;
        public List<ChatLogParam> messages;
        public bool is_dirty;
        private long top_message_id_server;
        private long last_message_id;
        private long last_message_posted_at;
        private long last_message_id_server;
        [CompilerGenerated]
        private static Comparison<ChatLogParam> <>f__am$cache7;

        public ChatLog()
        {
            this.add_log_list = new List<ChatLogParam>();
            this.messages = new List<ChatLogParam>();
            base..ctor();
            this.is_dirty = 0;
            this.top_message_id_server = 0L;
            this.last_message_id = 0L;
            this.last_message_id_server = 0L;
            this.last_message_posted_at = 0L;
            return;
        }

        [CompilerGenerated]
        private static int <AddMessage>m__2C3(ChatLogParam a, ChatLogParam b)
        {
            return (int) (a.posted_at - b.posted_at);
        }

        public void AddMessage(ChatLogParam _param)
        {
            List<ChatLogParam> list;
            list = new List<ChatLogParam>();
            list.Add(_param);
            this.AddMessage(list);
            return;
        }

        public void AddMessage(List<ChatLogParam> _message)
        {
            int num;
            int num2;
            <AddMessage>c__AnonStorey31C storeyc;
            <AddMessage>c__AnonStorey31D storeyd;
            storeyc = new <AddMessage>c__AnonStorey31C();
            storeyc._message = _message;
            this.add_log_list.Clear();
            storeyd = new <AddMessage>c__AnonStorey31D();
            storeyd.<>f__ref$796 = storeyc;
            storeyd.i = 0;
            goto Label_007C;
        Label_0031:
            if (this.messages.Find(new Predicate<ChatLogParam>(storeyd.<>m__2C2)) == null)
            {
                goto Label_0052;
            }
            goto Label_006E;
        Label_0052:
            this.add_log_list.Add(storeyc._message[storeyd.i]);
        Label_006E:
            storeyd.i += 1;
        Label_007C:
            if (storeyd.i < storeyc._message.Count)
            {
                goto Label_0031;
            }
            num = (this.messages.Count + this.add_log_list.Count) - ChatWindow.MAX_CHAT_LOG_ITEM;
            num = Mathf.Max(0, num);
            num2 = 0;
            goto Label_00CF;
        Label_00BF:
            this.messages.RemoveAt(0);
            num2 += 1;
        Label_00CF:
            if (num2 < num)
            {
                goto Label_00BF;
            }
            this.messages.AddRange(this.add_log_list);
            if (<>f__am$cache7 != null)
            {
                goto Label_0105;
            }
            <>f__am$cache7 = new Comparison<ChatLogParam>(ChatLog.<AddMessage>m__2C3);
        Label_0105:
            this.messages.Sort(<>f__am$cache7);
            this.RefreshId();
            return;
        }

        public void Deserialize(JSON_ChatLog json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.messages.Clear();
            this.messages = new List<ChatLogParam>(ChatWindow.MAX_CHAT_LOG_ITEM);
            if (json.messages == null)
            {
                goto Label_003F;
            }
            this.messages.AddRange(json.messages);
            return;
        Label_003F:
            return;
        }

        private void RefreshId()
        {
            int num;
            long num2;
            int num3;
            num = 0;
            goto Label_0043;
        Label_0007:
            if (this.messages[num].message_type != 3)
            {
                goto Label_0023;
            }
            goto Label_003F;
        Label_0023:
            this.top_message_id_server = this.messages[num].id;
            goto Label_0054;
        Label_003F:
            num += 1;
        Label_0043:
            if (num < this.messages.Count)
            {
                goto Label_0007;
            }
        Label_0054:
            num2 = this.last_message_id;
            num3 = 0;
            goto Label_00C7;
        Label_0062:
            this.last_message_id = this.messages[num3].id;
            this.last_message_posted_at = this.messages[num3].posted_at;
            if (this.messages[num3].message_type != 3)
            {
                goto Label_00AC;
            }
            goto Label_00C3;
        Label_00AC:
            this.last_message_id_server = this.messages[num3].id;
        Label_00C3:
            num3 += 1;
        Label_00C7:
            if (num3 < this.messages.Count)
            {
                goto Label_0062;
            }
            if (num2 == this.last_message_id)
            {
                goto Label_00EB;
            }
            this.is_dirty = 1;
        Label_00EB:
            return;
        }

        public void RemoveByIndex(int _index)
        {
            if ((this.messages.Count - 1) >= _index)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.messages.RemoveAt(_index);
            this.RefreshId();
            return;
        }

        public void Reset()
        {
            this.top_message_id_server = 0L;
            this.last_message_id = 0L;
            this.last_message_id_server = 0L;
            this.last_message_posted_at = 0L;
            this.messages.Clear();
            return;
        }

        public long TopMessageIdServer
        {
            get
            {
                return ((this.top_message_id_server < 0L) ? 0L : this.top_message_id_server);
            }
        }

        public long LastMessageIdServer
        {
            get
            {
                return ((this.last_message_id_server < 0L) ? 0L : this.last_message_id_server);
            }
        }

        public long LastMessagePostedAt
        {
            get
            {
                return this.last_message_posted_at;
            }
        }

        [CompilerGenerated]
        private sealed class <AddMessage>c__AnonStorey31C
        {
            internal List<ChatLogParam> _message;

            public <AddMessage>c__AnonStorey31C()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <AddMessage>c__AnonStorey31D
        {
            internal int i;
            internal ChatLog.<AddMessage>c__AnonStorey31C <>f__ref$796;

            public <AddMessage>c__AnonStorey31D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C2(ChatLogParam msg)
            {
                return (msg.id == this.<>f__ref$796._message[this.i].id);
            }
        }
    }
}

