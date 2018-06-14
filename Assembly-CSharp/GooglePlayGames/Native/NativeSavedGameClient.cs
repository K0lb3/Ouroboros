// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.NativeSavedGameClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GooglePlayGames.Native
{
  internal class NativeSavedGameClient : ISavedGameClient
  {
    private static readonly Regex ValidFilenameRegex = new Regex("\\A[a-zA-Z0-9-._~]{1,100}\\Z");
    private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mSnapshotManager;

    internal NativeSavedGameClient(GooglePlayGames.Native.PInvoke.SnapshotManager manager)
    {
      this.mSnapshotManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.SnapshotManager>(manager);
    }

    public void OpenWithAutomaticConflictResolution(string filename, GooglePlayGames.BasicApi.DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
      Misc.CheckNotNull<string>(filename);
      Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(callback);
      callback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(callback);
      if (!NativeSavedGameClient.IsValidFilename(filename))
      {
        Logger.e("Received invalid filename: " + filename);
        callback(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata) null);
      }
      else
        this.OpenWithManualConflictResolution(filename, source, false, (ConflictCallback) ((resolver, original, originalData, unmerged, unmergedData) =>
        {
          switch (resolutionStrategy)
          {
            case ConflictResolutionStrategy.UseLongestPlaytime:
              if (original.TotalTimePlayed >= unmerged.TotalTimePlayed)
              {
                resolver.ChooseMetadata(original);
                break;
              }
              resolver.ChooseMetadata(unmerged);
              break;
            case ConflictResolutionStrategy.UseOriginal:
              resolver.ChooseMetadata(original);
              break;
            case ConflictResolutionStrategy.UseUnmerged:
              resolver.ChooseMetadata(unmerged);
              break;
            default:
              Logger.e("Unhandled strategy " + (object) resolutionStrategy);
              callback(SavedGameRequestStatus.InternalError, (ISavedGameMetadata) null);
              break;
          }
        }), callback);
    }

    private ConflictCallback ToOnGameThread(ConflictCallback conflictCallback)
    {
      return (ConflictCallback) ((resolver, original, originalData, unmerged, unmergedData) =>
      {
        Logger.d("Invoking conflict callback");
        PlayGamesHelperObject.RunOnGameThread((Action) (() => conflictCallback(resolver, original, originalData, unmerged, unmergedData)));
      });
    }

    public void OpenWithManualConflictResolution(string filename, GooglePlayGames.BasicApi.DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
    {
      Misc.CheckNotNull<string>(filename);
      Misc.CheckNotNull<ConflictCallback>(conflictCallback);
      Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
      conflictCallback = this.ToOnGameThread(conflictCallback);
      completedCallback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(completedCallback);
      if (!NativeSavedGameClient.IsValidFilename(filename))
      {
        Logger.e("Received invalid filename: " + filename);
        completedCallback(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata) null);
      }
      else
        this.InternalManualOpen(filename, source, prefetchDataOnConflict, conflictCallback, completedCallback);
    }

    private void InternalManualOpen(string filename, GooglePlayGames.BasicApi.DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
    {
      this.mSnapshotManager.Open(filename, NativeSavedGameClient.AsDataSource(source), Types.SnapshotConflictPolicy.MANUAL, (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.OpenResponse>) (response =>
      {
        if (!response.RequestSucceeded())
          completedCallback(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata) null);
        else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID)
          completedCallback(SavedGameRequestStatus.Success, (ISavedGameMetadata) response.Data());
        else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          NativeSavedGameClient.\u003CInternalManualOpen\u003Ec__AnonStorey1B6.\u003CInternalManualOpen\u003Ec__AnonStorey1B7 openCAnonStorey1B7 = new NativeSavedGameClient.\u003CInternalManualOpen\u003Ec__AnonStorey1B6.\u003CInternalManualOpen\u003Ec__AnonStorey1B7();
          // ISSUE: reference to a compiler-generated field
          openCAnonStorey1B7.\u003C\u003Ef__ref\u0024438 = this;
          // ISSUE: reference to a compiler-generated field
          openCAnonStorey1B7.original = response.ConflictOriginal();
          // ISSUE: reference to a compiler-generated field
          openCAnonStorey1B7.unmerged = response.ConflictUnmerged();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          openCAnonStorey1B7.resolver = new NativeSavedGameClient.NativeConflictResolver(this.mSnapshotManager, response.ConflictId(), openCAnonStorey1B7.original, openCAnonStorey1B7.unmerged, completedCallback, new Action(openCAnonStorey1B7.\u003C\u003Em__B7));
          if (!prefetchDataOnConflict)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            conflictCallback((IConflictResolver) openCAnonStorey1B7.resolver, (ISavedGameMetadata) openCAnonStorey1B7.original, (byte[]) null, (ISavedGameMetadata) openCAnonStorey1B7.unmerged, (byte[]) null);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            NativeSavedGameClient.Prefetcher prefetcher = new NativeSavedGameClient.Prefetcher(new Action<byte[], byte[]>(openCAnonStorey1B7.\u003C\u003Em__B8), completedCallback);
            // ISSUE: reference to a compiler-generated field
            this.mSnapshotManager.Read(openCAnonStorey1B7.original, new Action<GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse>(prefetcher.OnOriginalDataRead));
            // ISSUE: reference to a compiler-generated field
            this.mSnapshotManager.Read(openCAnonStorey1B7.unmerged, new Action<GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse>(prefetcher.OnUnmergedDataRead));
          }
        }
        else
        {
          Logger.e("Unhandled response status");
          completedCallback(SavedGameRequestStatus.InternalError, (ISavedGameMetadata) null);
        }
      }));
    }

    public void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback)
    {
      Misc.CheckNotNull<ISavedGameMetadata>(metadata);
      Misc.CheckNotNull<Action<SavedGameRequestStatus, byte[]>>(completedCallback);
      completedCallback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, byte[]>(completedCallback);
      NativeSnapshotMetadata metadata1 = metadata as NativeSnapshotMetadata;
      if (metadata1 == null)
      {
        Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
        completedCallback(SavedGameRequestStatus.BadInputError, (byte[]) null);
      }
      else if (!metadata1.IsOpen)
      {
        Logger.e("This method requires an open ISavedGameMetadata.");
        completedCallback(SavedGameRequestStatus.BadInputError, (byte[]) null);
      }
      else
        this.mSnapshotManager.Read(metadata1, (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse>) (response =>
        {
          if (!response.RequestSucceeded())
            completedCallback(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), (byte[]) null);
          else
            completedCallback(SavedGameRequestStatus.Success, response.Data());
        }));
    }

    public void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback)
    {
      Misc.CheckNotNull<string>(uiTitle);
      Misc.CheckNotNull<Action<SelectUIStatus, ISavedGameMetadata>>(callback);
      callback = NativeSavedGameClient.ToOnGameThread<SelectUIStatus, ISavedGameMetadata>(callback);
      if (maxDisplayedSavedGames <= 0U)
      {
        Logger.e("maxDisplayedSavedGames must be greater than 0");
        callback(SelectUIStatus.BadInputError, (ISavedGameMetadata) null);
      }
      else
        this.mSnapshotManager.SnapshotSelectUI(showCreateSaveUI, showDeleteSaveUI, maxDisplayedSavedGames, uiTitle, (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.SnapshotSelectUIResponse>) (response => callback(NativeSavedGameClient.AsUIStatus(response.RequestStatus()), !response.RequestSucceeded() ? (ISavedGameMetadata) null : (ISavedGameMetadata) response.Data())));
    }

    public void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
      Misc.CheckNotNull<ISavedGameMetadata>(metadata);
      Misc.CheckNotNull<byte[]>(updatedBinaryData);
      Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(callback);
      callback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(callback);
      NativeSnapshotMetadata metadata1 = metadata as NativeSnapshotMetadata;
      if (metadata1 == null)
      {
        Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
        callback(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata) null);
      }
      else if (!metadata1.IsOpen)
      {
        Logger.e("This method requires an open ISavedGameMetadata.");
        callback(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata) null);
      }
      else
        this.mSnapshotManager.Commit(metadata1, NativeSavedGameClient.AsMetadataChange(updateForMetadata), updatedBinaryData, (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.CommitResponse>) (response =>
        {
          if (!response.RequestSucceeded())
            callback(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata) null);
          else
            callback(SavedGameRequestStatus.Success, (ISavedGameMetadata) response.Data());
        }));
    }

    public void FetchAllSavedGames(GooglePlayGames.BasicApi.DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback)
    {
      Misc.CheckNotNull<Action<SavedGameRequestStatus, List<ISavedGameMetadata>>>(callback);
      callback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, List<ISavedGameMetadata>>(callback);
      this.mSnapshotManager.FetchAll(NativeSavedGameClient.AsDataSource(source), (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.FetchAllResponse>) (response =>
      {
        if (!response.RequestSucceeded())
          callback(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), new List<ISavedGameMetadata>());
        else
          callback(SavedGameRequestStatus.Success, response.Data().Cast<ISavedGameMetadata>().ToList<ISavedGameMetadata>());
      }));
    }

    public void Delete(ISavedGameMetadata metadata)
    {
      Misc.CheckNotNull<ISavedGameMetadata>(metadata);
      this.mSnapshotManager.Delete((NativeSnapshotMetadata) metadata);
    }

    internal static bool IsValidFilename(string filename)
    {
      if (filename == null)
        return false;
      return NativeSavedGameClient.ValidFilenameRegex.IsMatch(filename);
    }

    private static Types.SnapshotConflictPolicy AsConflictPolicy(ConflictResolutionStrategy strategy)
    {
      switch (strategy)
      {
        case ConflictResolutionStrategy.UseLongestPlaytime:
          return Types.SnapshotConflictPolicy.LONGEST_PLAYTIME;
        case ConflictResolutionStrategy.UseOriginal:
          return Types.SnapshotConflictPolicy.LAST_KNOWN_GOOD;
        case ConflictResolutionStrategy.UseUnmerged:
          return Types.SnapshotConflictPolicy.MOST_RECENTLY_MODIFIED;
        default:
          throw new InvalidOperationException("Found unhandled strategy: " + (object) strategy);
      }
    }

    private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.SnapshotOpenStatus status)
    {
      CommonErrorStatus.SnapshotOpenStatus snapshotOpenStatus = status;
      switch (snapshotOpenStatus + 5)
      {
        case ~(CommonErrorStatus.SnapshotOpenStatus.ERROR_INTERNAL | CommonErrorStatus.SnapshotOpenStatus.VALID):
          return SavedGameRequestStatus.TimeoutError;
        case ~CommonErrorStatus.SnapshotOpenStatus.ERROR_NOT_AUTHORIZED:
          return SavedGameRequestStatus.AuthenticationError;
        default:
          if (snapshotOpenStatus == CommonErrorStatus.SnapshotOpenStatus.VALID)
            return SavedGameRequestStatus.Success;
          Logger.e("Encountered unknown status: " + (object) status);
          return SavedGameRequestStatus.InternalError;
      }
    }

    private static Types.DataSource AsDataSource(GooglePlayGames.BasicApi.DataSource source)
    {
      switch (source)
      {
        case GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork:
          return Types.DataSource.CACHE_OR_NETWORK;
        case GooglePlayGames.BasicApi.DataSource.ReadNetworkOnly:
          return Types.DataSource.NETWORK_ONLY;
        default:
          throw new InvalidOperationException("Found unhandled DataSource: " + (object) source);
      }
    }

    private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.ResponseStatus status)
    {
      switch (status + 5)
      {
        case ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED:
          return SavedGameRequestStatus.TimeoutError;
        case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
          Logger.e("User was not authorized (they were probably not logged in).");
          return SavedGameRequestStatus.AuthenticationError;
        case CommonErrorStatus.ResponseStatus.VALID | CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
          return SavedGameRequestStatus.InternalError;
        case ~CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT:
          Logger.e("User attempted to use the game without a valid license.");
          return SavedGameRequestStatus.AuthenticationError;
        case (CommonErrorStatus.ResponseStatus) 6:
        case (CommonErrorStatus.ResponseStatus) 7:
          return SavedGameRequestStatus.Success;
        default:
          Logger.e("Unknown status: " + (object) status);
          return SavedGameRequestStatus.InternalError;
      }
    }

    private static SelectUIStatus AsUIStatus(CommonErrorStatus.UIStatus uiStatus)
    {
      switch (uiStatus + 6)
      {
        case ~(CommonErrorStatus.UIStatus.ERROR_INTERNAL | CommonErrorStatus.UIStatus.VALID):
          return SelectUIStatus.UserClosedUI;
        case CommonErrorStatus.UIStatus.VALID:
          return SelectUIStatus.TimeoutError;
        case ~CommonErrorStatus.UIStatus.ERROR_VERSION_UPDATE_REQUIRED:
          return SelectUIStatus.AuthenticationError;
        case ~CommonErrorStatus.UIStatus.ERROR_TIMEOUT:
          return SelectUIStatus.InternalError;
        case (CommonErrorStatus.UIStatus) 7:
          return SelectUIStatus.SavedGameSelected;
        default:
          Logger.e("Encountered unknown UI Status: " + (object) uiStatus);
          return SelectUIStatus.InternalError;
      }
    }

    private static NativeSnapshotMetadataChange AsMetadataChange(SavedGameMetadataUpdate update)
    {
      NativeSnapshotMetadataChange.Builder builder = new NativeSnapshotMetadataChange.Builder();
      if (update.IsCoverImageUpdated)
        builder.SetCoverImageFromPngData(update.UpdatedPngCoverImage);
      if (update.IsDescriptionUpdated)
        builder.SetDescription(update.UpdatedDescription);
      if (update.IsPlayedTimeUpdated)
        builder.SetPlayedTime((ulong) update.UpdatedPlayedTime.Value.TotalMilliseconds);
      return builder.Build();
    }

    private static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
    {
      return (Action<T1, T2>) ((val1, val2) => PlayGamesHelperObject.RunOnGameThread((Action) (() => toConvert(val1, val2))));
    }

    private class NativeConflictResolver : IConflictResolver
    {
      private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mManager;
      private readonly string mConflictId;
      private readonly NativeSnapshotMetadata mOriginal;
      private readonly NativeSnapshotMetadata mUnmerged;
      private readonly Action<SavedGameRequestStatus, ISavedGameMetadata> mCompleteCallback;
      private readonly Action mRetryFileOpen;

      internal NativeConflictResolver(GooglePlayGames.Native.PInvoke.SnapshotManager manager, string conflictId, NativeSnapshotMetadata original, NativeSnapshotMetadata unmerged, Action<SavedGameRequestStatus, ISavedGameMetadata> completeCallback, Action retryOpen)
      {
        this.mManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.SnapshotManager>(manager);
        this.mConflictId = Misc.CheckNotNull<string>(conflictId);
        this.mOriginal = Misc.CheckNotNull<NativeSnapshotMetadata>(original);
        this.mUnmerged = Misc.CheckNotNull<NativeSnapshotMetadata>(unmerged);
        this.mCompleteCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completeCallback);
        this.mRetryFileOpen = Misc.CheckNotNull<Action>(retryOpen);
      }

      public void ChooseMetadata(ISavedGameMetadata chosenMetadata)
      {
        NativeSnapshotMetadata metadata = chosenMetadata as NativeSnapshotMetadata;
        if (metadata != this.mOriginal && metadata != this.mUnmerged)
        {
          Logger.e("Caller attempted to choose a version of the metadata that was not part of the conflict");
          this.mCompleteCallback(SavedGameRequestStatus.BadInputError, (ISavedGameMetadata) null);
        }
        else
          this.mManager.Resolve(metadata, new NativeSnapshotMetadataChange.Builder().Build(), this.mConflictId, (Action<GooglePlayGames.Native.PInvoke.SnapshotManager.CommitResponse>) (response =>
          {
            if (!response.RequestSucceeded())
              this.mCompleteCallback(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), (ISavedGameMetadata) null);
            else
              this.mRetryFileOpen();
          }));
      }
    }

    private class Prefetcher
    {
      private readonly object mLock = new object();
      private bool mOriginalDataFetched;
      private byte[] mOriginalData;
      private bool mUnmergedDataFetched;
      private byte[] mUnmergedData;
      private Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback;
      private readonly Action<byte[], byte[]> mDataFetchedCallback;

      internal Prefetcher(Action<byte[], byte[]> dataFetchedCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
      {
        this.mDataFetchedCallback = Misc.CheckNotNull<Action<byte[], byte[]>>(dataFetchedCallback);
        this.completedCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
      }

      internal void OnOriginalDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
      {
        lock (this.mLock)
        {
          if (!readResponse.RequestSucceeded())
          {
            Logger.e("Encountered error while prefetching original data.");
            this.completedCallback(NativeSavedGameClient.AsRequestStatus(readResponse.ResponseStatus()), (ISavedGameMetadata) null);
            this.completedCallback = (Action<SavedGameRequestStatus, ISavedGameMetadata>) ((_param0, _param1) => {});
          }
          else
          {
            Logger.d("Successfully fetched original data");
            this.mOriginalDataFetched = true;
            this.mOriginalData = readResponse.Data();
            this.MaybeProceed();
          }
        }
      }

      internal void OnUnmergedDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
      {
        lock (this.mLock)
        {
          if (!readResponse.RequestSucceeded())
          {
            Logger.e("Encountered error while prefetching unmerged data.");
            this.completedCallback(NativeSavedGameClient.AsRequestStatus(readResponse.ResponseStatus()), (ISavedGameMetadata) null);
            this.completedCallback = (Action<SavedGameRequestStatus, ISavedGameMetadata>) ((_param0, _param1) => {});
          }
          else
          {
            Logger.d("Successfully fetched unmerged data");
            this.mUnmergedDataFetched = true;
            this.mUnmergedData = readResponse.Data();
            this.MaybeProceed();
          }
        }
      }

      private void MaybeProceed()
      {
        if (this.mOriginalDataFetched && this.mUnmergedDataFetched)
        {
          Logger.d("Fetched data for original and unmerged, proceeding");
          this.mDataFetchedCallback(this.mOriginalData, this.mUnmergedData);
        }
        else
          Logger.d("Not all data fetched - original:" + (object) this.mOriginalDataFetched + " unmerged:" + (object) this.mUnmergedDataFetched);
      }
    }
  }
}
