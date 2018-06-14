// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeEvent : BaseReferenceHolder, IEvent
  {
    internal NativeEvent(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    public string Id
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Id(this.SelfPtr(), out_string, out_size)));
      }
    }

    public string Name
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Name(this.SelfPtr(), out_string, out_size)));
      }
    }

    public string Description
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Description(this.SelfPtr(), out_string, out_size)));
      }
    }

    public string ImageUrl
    {
      get
      {
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_ImageUrl(this.SelfPtr(), out_string, out_size)));
      }
    }

    public ulong CurrentCount
    {
      get
      {
        return Event.Event_Count(this.SelfPtr());
      }
    }

    public GooglePlayGames.BasicApi.Events.EventVisibility Visibility
    {
      get
      {
        Types.EventVisibility eventVisibility = Event.Event_Visibility(this.SelfPtr());
        switch (eventVisibility)
        {
          case Types.EventVisibility.HIDDEN:
            return GooglePlayGames.BasicApi.Events.EventVisibility.Hidden;
          case Types.EventVisibility.REVEALED:
            return GooglePlayGames.BasicApi.Events.EventVisibility.Revealed;
          default:
            throw new InvalidOperationException("Unknown visibility: " + (object) eventVisibility);
        }
      }
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      Event.Event_Dispose(selfPointer);
    }

    public override string ToString()
    {
      if (this.IsDisposed())
        return "[NativeEvent: DELETED]";
      return string.Format("[NativeEvent: Id={0}, Name={1}, Description={2}, ImageUrl={3}, CurrentCount={4}, Visibility={5}]", (object) this.Id, (object) this.Name, (object) this.Description, (object) this.ImageUrl, (object) this.CurrentCount, (object) this.Visibility);
    }
  }
}
