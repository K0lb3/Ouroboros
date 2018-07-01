// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ChatLog
  {
    private List<ChatLogParam> add_log_list = new List<ChatLogParam>();
    public List<ChatLogParam> messages = new List<ChatLogParam>();
    public bool is_dirty;
    private long top_message_id_server;
    private long last_message_id;
    private long last_message_id_server;

    public ChatLog()
    {
      this.is_dirty = false;
      this.top_message_id_server = 0L;
      this.last_message_id = 0L;
      this.last_message_id_server = 0L;
    }

    public long TopMessageIdServer
    {
      get
      {
        if (this.top_message_id_server >= 0L)
          return this.top_message_id_server;
        return 0;
      }
    }

    public long LastMessageIdServer
    {
      get
      {
        if (this.last_message_id_server >= 0L)
          return this.last_message_id_server;
        return 0;
      }
    }

    public void AddMessage(ChatLogParam _param)
    {
      this.AddMessage(new List<ChatLogParam>() { _param });
    }

    public void AddMessage(List<ChatLogParam> _message)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatLog.\u003CAddMessage\u003Ec__AnonStorey31D messageCAnonStorey31D = new ChatLog.\u003CAddMessage\u003Ec__AnonStorey31D();
      // ISSUE: reference to a compiler-generated field
      messageCAnonStorey31D._message = _message;
      this.add_log_list.Clear();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatLog.\u003CAddMessage\u003Ec__AnonStorey31E messageCAnonStorey31E = new ChatLog.\u003CAddMessage\u003Ec__AnonStorey31E();
      // ISSUE: reference to a compiler-generated field
      messageCAnonStorey31E.\u003C\u003Ef__ref\u0024797 = messageCAnonStorey31D;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (messageCAnonStorey31E.i = 0; messageCAnonStorey31E.i < messageCAnonStorey31D._message.Count; ++messageCAnonStorey31E.i)
      {
        // ISSUE: reference to a compiler-generated method
        if (this.messages.Find(new Predicate<ChatLogParam>(messageCAnonStorey31E.\u003C\u003Em__33A)) == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.add_log_list.Add(messageCAnonStorey31D._message[messageCAnonStorey31E.i]);
        }
      }
      int num = Mathf.Max(0, this.messages.Count + this.add_log_list.Count - (int) ChatWindow.MAX_CHAT_LOG_ITEM);
      for (int index = 0; index < num; ++index)
        this.messages.RemoveAt(0);
      this.messages.AddRange((IEnumerable<ChatLogParam>) this.add_log_list);
      this.messages.Sort((Comparison<ChatLogParam>) ((a, b) => (int) (a.posted_at - b.posted_at)));
      this.RefreshId();
    }

    public void Reset()
    {
      this.top_message_id_server = 0L;
      this.last_message_id = 0L;
      this.last_message_id_server = 0L;
      this.messages.Clear();
    }

    public void RemoveByIndex(int _index)
    {
      if (this.messages.Count - 1 < _index)
        return;
      this.messages.RemoveAt(_index);
      this.RefreshId();
    }

    private void RefreshId()
    {
      for (int index = 0; index < this.messages.Count; ++index)
      {
        if ((int) this.messages[index].message_type != 3)
        {
          this.top_message_id_server = this.messages[index].id;
          break;
        }
      }
      long lastMessageId = this.last_message_id;
      for (int index = 0; index < this.messages.Count; ++index)
      {
        this.last_message_id = this.messages[index].id;
        if ((int) this.messages[index].message_type != 3)
          this.last_message_id_server = this.messages[index].id;
      }
      if (lastMessageId == this.last_message_id)
        return;
      this.is_dirty = true;
    }

    public void Deserialize(JSON_ChatLog json)
    {
      if (json == null)
        return;
      this.messages.Clear();
      this.messages = new List<ChatLogParam>((int) ChatWindow.MAX_CHAT_LOG_ITEM);
      if (json.messages == null)
        return;
      this.messages.AddRange((IEnumerable<ChatLogParam>) json.messages);
    }
  }
}
