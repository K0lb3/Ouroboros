// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.ChatChannel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace ExitGames.Client.Photon.Chat
{
  public class ChatChannel
  {
    public readonly List<string> Senders = new List<string>();
    public readonly List<object> Messages = new List<object>();
    public readonly string Name;
    public int MessageLimit;

    public ChatChannel(string name)
    {
      this.Name = name;
    }

    public bool IsPrivate { get; protected internal set; }

    public int MessageCount
    {
      get
      {
        return this.Messages.Count;
      }
    }

    public void Add(string sender, object message)
    {
      this.Senders.Add(sender);
      this.Messages.Add(message);
      this.TruncateMessages();
    }

    public void Add(string[] senders, object[] messages)
    {
      this.Senders.AddRange((IEnumerable<string>) senders);
      this.Messages.AddRange((IEnumerable<object>) messages);
      this.TruncateMessages();
    }

    public void TruncateMessages()
    {
      if (this.MessageLimit <= 0 || this.Messages.Count <= this.MessageLimit)
        return;
      int count = this.Messages.Count - this.MessageLimit;
      this.Senders.RemoveRange(0, count);
      this.Messages.RemoveRange(0, count);
    }

    public void ClearMessages()
    {
      this.Senders.Clear();
      this.Messages.Clear();
    }

    public string ToStringMessages()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.Messages.Count; ++index)
        stringBuilder.AppendLine(string.Format("{0}: {1}", (object) this.Senders[index], this.Messages[index]));
      return stringBuilder.ToString();
    }
  }
}
