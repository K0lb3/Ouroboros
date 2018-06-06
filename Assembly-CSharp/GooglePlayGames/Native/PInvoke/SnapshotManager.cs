// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.SnapshotManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class SnapshotManager
  {
    private readonly GameServices mServices;

    internal SnapshotManager(GameServices services)
    {
      this.mServices = Misc.CheckNotNull<GameServices>(services);
    }

    internal void FetchAll(Types.DataSource source, Action<SnapshotManager.FetchAllResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_FetchAll(this.mServices.AsHandle(), source, new GooglePlayGames.Native.Cwrapper.SnapshotManager.FetchAllCallback(SnapshotManager.InternalFetchAllCallback), Callbacks.ToIntPtr<SnapshotManager.FetchAllResponse>(callback, new Func<IntPtr, SnapshotManager.FetchAllResponse>(SnapshotManager.FetchAllResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.SnapshotManager.FetchAllCallback))]
    internal static void InternalFetchAllCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("SnapshotManager#FetchAllCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void SnapshotSelectUI(bool allowCreate, bool allowDelete, uint maxSnapshots, string uiTitle, Action<SnapshotManager.SnapshotSelectUIResponse> callback)
    {
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_ShowSelectUIOperation(this.mServices.AsHandle(), allowCreate, allowDelete, maxSnapshots, uiTitle, new GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotSelectUICallback(SnapshotManager.InternalSnapshotSelectUICallback), Callbacks.ToIntPtr<SnapshotManager.SnapshotSelectUIResponse>(callback, new Func<IntPtr, SnapshotManager.SnapshotSelectUIResponse>(SnapshotManager.SnapshotSelectUIResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotSelectUICallback))]
    internal static void InternalSnapshotSelectUICallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("SnapshotManager#SnapshotSelectUICallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Open(string fileName, Types.DataSource source, Types.SnapshotConflictPolicy conflictPolicy, Action<SnapshotManager.OpenResponse> callback)
    {
      Misc.CheckNotNull<string>(fileName);
      Misc.CheckNotNull<Action<SnapshotManager.OpenResponse>>(callback);
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_Open(this.mServices.AsHandle(), source, fileName, conflictPolicy, new GooglePlayGames.Native.Cwrapper.SnapshotManager.OpenCallback(SnapshotManager.InternalOpenCallback), Callbacks.ToIntPtr<SnapshotManager.OpenResponse>(callback, new Func<IntPtr, SnapshotManager.OpenResponse>(SnapshotManager.OpenResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.SnapshotManager.OpenCallback))]
    internal static void InternalOpenCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("SnapshotManager#OpenCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Commit(NativeSnapshotMetadata metadata, NativeSnapshotMetadataChange metadataChange, byte[] updatedData, Action<SnapshotManager.CommitResponse> callback)
    {
      Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
      Misc.CheckNotNull<NativeSnapshotMetadataChange>(metadataChange);
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_Commit(this.mServices.AsHandle(), metadata.AsPointer(), metadataChange.AsPointer(), updatedData, new UIntPtr((ulong) updatedData.Length), new GooglePlayGames.Native.Cwrapper.SnapshotManager.CommitCallback(SnapshotManager.InternalCommitCallback), Callbacks.ToIntPtr<SnapshotManager.CommitResponse>(callback, new Func<IntPtr, SnapshotManager.CommitResponse>(SnapshotManager.CommitResponse.FromPointer)));
    }

    internal void Resolve(NativeSnapshotMetadata metadata, NativeSnapshotMetadataChange metadataChange, string conflictId, Action<SnapshotManager.CommitResponse> callback)
    {
      Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
      Misc.CheckNotNull<NativeSnapshotMetadataChange>(metadataChange);
      Misc.CheckNotNull<string>(conflictId);
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_ResolveConflict(this.mServices.AsHandle(), metadata.AsPointer(), metadataChange.AsPointer(), conflictId, new GooglePlayGames.Native.Cwrapper.SnapshotManager.CommitCallback(SnapshotManager.InternalCommitCallback), Callbacks.ToIntPtr<SnapshotManager.CommitResponse>(callback, new Func<IntPtr, SnapshotManager.CommitResponse>(SnapshotManager.CommitResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.SnapshotManager.CommitCallback))]
    internal static void InternalCommitCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("SnapshotManager#CommitCallback", Callbacks.Type.Temporary, response, data);
    }

    internal void Delete(NativeSnapshotMetadata metadata)
    {
      Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_Delete(this.mServices.AsHandle(), metadata.AsPointer());
    }

    internal void Read(NativeSnapshotMetadata metadata, Action<SnapshotManager.ReadResponse> callback)
    {
      Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
      Misc.CheckNotNull<Action<SnapshotManager.ReadResponse>>(callback);
      GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_Read(this.mServices.AsHandle(), metadata.AsPointer(), new GooglePlayGames.Native.Cwrapper.SnapshotManager.ReadCallback(SnapshotManager.InternalReadCallback), Callbacks.ToIntPtr<SnapshotManager.ReadResponse>(callback, new Func<IntPtr, SnapshotManager.ReadResponse>(SnapshotManager.ReadResponse.FromPointer)));
    }

    [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.SnapshotManager.ReadCallback))]
    internal static void InternalReadCallback(IntPtr response, IntPtr data)
    {
      Callbacks.PerformInternalCallback("SnapshotManager#ReadCallback", Callbacks.Type.Temporary, response, data);
    }

    internal class OpenResponse : BaseReferenceHolder
    {
      internal OpenResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~(CommonErrorStatus.SnapshotOpenStatus.ERROR_INTERNAL | CommonErrorStatus.SnapshotOpenStatus.VALID);
      }

      internal CommonErrorStatus.SnapshotOpenStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_GetStatus(this.SelfPtr());
      }

      internal string ConflictId()
      {
        if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
          throw new InvalidOperationException("OpenResponse did not have a conflict");
        return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_GetConflictId(this.SelfPtr(), out_string, out_size)));
      }

      internal NativeSnapshotMetadata Data()
      {
        if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID)
          throw new InvalidOperationException("OpenResponse had a conflict");
        return new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_GetData(this.SelfPtr()));
      }

      internal NativeSnapshotMetadata ConflictOriginal()
      {
        if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
          throw new InvalidOperationException("OpenResponse did not have a conflict");
        return new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_GetConflictOriginal(this.SelfPtr()));
      }

      internal NativeSnapshotMetadata ConflictUnmerged()
      {
        if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
          throw new InvalidOperationException("OpenResponse did not have a conflict");
        return new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_GetConflictUnmerged(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_OpenResponse_Dispose(selfPointer);
      }

      internal static SnapshotManager.OpenResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (SnapshotManager.OpenResponse) null;
        return new SnapshotManager.OpenResponse(pointer);
      }
    }

    internal class FetchAllResponse : BaseReferenceHolder
    {
      internal FetchAllResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_FetchAllResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal IEnumerable<NativeSnapshotMetadata> Data()
      {
        return PInvokeUtilities.ToEnumerable<NativeSnapshotMetadata>(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_FetchAllResponse_GetData_Length(this.SelfPtr()), (Func<UIntPtr, NativeSnapshotMetadata>) (index => new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_FetchAllResponse_GetData_GetElement(this.SelfPtr(), index))));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_FetchAllResponse_Dispose(selfPointer);
      }

      internal static SnapshotManager.FetchAllResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (SnapshotManager.FetchAllResponse) null;
        return new SnapshotManager.FetchAllResponse(pointer);
      }
    }

    internal class CommitResponse : BaseReferenceHolder
    {
      internal CommitResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_CommitResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal NativeSnapshotMetadata Data()
      {
        if (!this.RequestSucceeded())
          throw new InvalidOperationException("Request did not succeed");
        return new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_CommitResponse_GetData(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_CommitResponse_Dispose(selfPointer);
      }

      internal static SnapshotManager.CommitResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (SnapshotManager.CommitResponse) null;
        return new SnapshotManager.CommitResponse(pointer);
      }
    }

    internal class ReadResponse : BaseReferenceHolder
    {
      internal ReadResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.ResponseStatus ResponseStatus()
      {
        return GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_CommitResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.ResponseStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
      }

      internal byte[] Data()
      {
        if (!this.RequestSucceeded())
          throw new InvalidOperationException("Request did not succeed");
        return PInvokeUtilities.OutParamsToArray<byte>((PInvokeUtilities.OutMethod<byte>) ((out_bytes, out_size) => GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_ReadResponse_GetData(this.SelfPtr(), out_bytes, out_size)));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_ReadResponse_Dispose(selfPointer);
      }

      internal static SnapshotManager.ReadResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (SnapshotManager.ReadResponse) null;
        return new SnapshotManager.ReadResponse(pointer);
      }
    }

    internal class SnapshotSelectUIResponse : BaseReferenceHolder
    {
      internal SnapshotSelectUIResponse(IntPtr selfPointer)
        : base(selfPointer)
      {
      }

      internal CommonErrorStatus.UIStatus RequestStatus()
      {
        return GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_GetStatus(this.SelfPtr());
      }

      internal bool RequestSucceeded()
      {
        return this.RequestStatus() > ~(CommonErrorStatus.UIStatus.ERROR_INTERNAL | CommonErrorStatus.UIStatus.VALID);
      }

      internal NativeSnapshotMetadata Data()
      {
        if (!this.RequestSucceeded())
          throw new InvalidOperationException("Request did not succeed");
        return new NativeSnapshotMetadata(GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_GetData(this.SelfPtr()));
      }

      protected override void CallDispose(HandleRef selfPointer)
      {
        GooglePlayGames.Native.Cwrapper.SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_Dispose(selfPointer);
      }

      internal static SnapshotManager.SnapshotSelectUIResponse FromPointer(IntPtr pointer)
      {
        if (pointer.Equals((object) IntPtr.Zero))
          return (SnapshotManager.SnapshotSelectUIResponse) null;
        return new SnapshotManager.SnapshotSelectUIResponse(pointer);
      }
    }
  }
}
