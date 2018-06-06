// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Events.IEventsClient
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Events
{
  public interface IEventsClient
  {
    void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback);

    void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback);

    void IncrementEvent(string eventId, uint stepsToIncrement);
  }
}
