// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeConnectionRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeConnectionRequest : BaseReferenceHolder
  {
    internal NativeConnectionRequest(IntPtr pointer)
      : base(pointer)
    {
    }

    internal string RemoteEndpointId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionRequest_GetRemoteEndpointId(this.SelfPtr(), out_arg, out_size)));
    }

    internal string RemoteDeviceId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionRequest_GetRemoteDeviceId(this.SelfPtr(), out_arg, out_size)));
    }

    internal string RemoteEndpointName()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionRequest_GetRemoteEndpointName(this.SelfPtr(), out_arg, out_size)));
    }

    internal byte[] Payload()
    {
      return PInvokeUtilities.OutParamsToArray<byte>((PInvokeUtilities.OutMethod<byte>) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionRequest_GetPayload(this.SelfPtr(), out_arg, out_size)));
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      NearbyConnectionTypes.ConnectionRequest_Dispose(selfPointer);
    }

    internal ConnectionRequest AsRequest()
    {
      return new ConnectionRequest(this.RemoteEndpointId(), this.RemoteDeviceId(), this.RemoteEndpointName(), NearbyConnectionsManager.ServiceId, this.Payload());
    }

    internal static NativeConnectionRequest FromPointer(IntPtr pointer)
    {
      if (pointer == IntPtr.Zero)
        return (NativeConnectionRequest) null;
      return new NativeConnectionRequest(pointer);
    }
  }
}
