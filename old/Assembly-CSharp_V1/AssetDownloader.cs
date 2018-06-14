// Decompiled with JetBrains decompiler
// Type: AssetDownloader
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;

public class AssetDownloader : MonoBehaviour
{
  public static string DownloadURL = string.Empty;
  public static string StreamingURL = string.Empty;
  private static List<string> mRequestIDs = new List<string>();
  private static bool mDownloadedText = false;
  private static float mDownloadProgress = 0.0f;
  private static long totalDownloadSize = 0;
  private static long currentDownloadSize = 0;
  private static long downloadedSize = 0;
  private static float mAverageDownloadSpeed = 1048576f;
  public static long UnZipFileSize = 0;
  public static List<AssetDownloader.ExistAssetList> mExistFile = new List<AssetDownloader.ExistAssetList>();
  public static bool BatchDownload = true;
  public const string FileListName = "ASSETS";
  private const string MetaExt = ".meta";
  public const int maxRetryCount = 5;
  private const int SIZE_MB = 1048576;
  private const int ConnectionSteamLimit = 16;
  public const int LimitDownloadSize = 5242880;
  private const int UnZipRequestJobCapacity = 10;
  public readonly string FileAssetListName;
  public readonly string FileRevisionName;
  public readonly string FileExistName;
  private static AssetDownloader mInstance;
  private Dictionary<string, int> itemCompressedSize;
  private static Coroutine mCoroutine;
  private static bool mHasError;
  private static bool mRetryOnError;
  private static AssetManager.AssetFormats oldFormat;
  private float[] mSpeedHistory;
  private int mSpeedHistorySize;
  private int mSpeedHistoryPos;
  private Thread mUnzipThread;
  private Mutex mMutex;
  private AutoResetEvent mUnzipSignal;
  private bool mMutexAcquired;
  private bool mShuttingDown;
  private List<AssetDownloader.UnzipJob> mUnzipJobs;
  private AssetDownloader.UnzipThread2Arg mUnzipThreadArg;
  private Thread mCompareHashThread;
  private Mutex mCompareHashMutex;
  private float mCompareHashProgressShared;
  private static float mCompareHashProgress;
  private DownloadObserver mDownloadObserver;
  private static AssetDownloader.DownloadPhases mPhase;
  private string mLog;
  private static string mCachePath;
  private static string mTextCachePath;

  public AssetDownloader()
  {
    base.\u002Ector();
  }

  public static float AverageDownloadSpeed
  {
    get
    {
      return AssetDownloader.mAverageDownloadSpeed;
    }
  }

  public static void Reset()
  {
    AssetDownloader.mHasError = false;
    AssetDownloader.mCoroutine = (Coroutine) null;
    AssetDownloader.mRequestIDs.Clear();
    if (!Object.op_Inequality((Object) AssetDownloader.mInstance, (Object) null))
      return;
    Object.Destroy((Object) ((Component) AssetDownloader.mInstance).get_gameObject());
    AssetDownloader.mInstance = (AssetDownloader) null;
  }

  public static void ResetTextSetting()
  {
    AssetDownloader.mDownloadedText = false;
  }

  public static bool HasError
  {
    get
    {
      return AssetDownloader.mHasError;
    }
  }

  public static bool HasDownloadedText
  {
    get
    {
      return AssetDownloader.mDownloadedText;
    }
  }

  private static AssetDownloader Instance
  {
    get
    {
      if (Object.op_Equality((Object) AssetDownloader.mInstance, (Object) null))
      {
        GameObject gameObject = new GameObject(typeof (AssetDownloader).Name, new System.Type[1]{ typeof (AssetDownloader) });
        AssetDownloader.mInstance = (AssetDownloader) gameObject.GetComponent<AssetDownloader>();
        Object.DontDestroyOnLoad((Object) gameObject);
      }
      return AssetDownloader.mInstance;
    }
  }

  private void Awake()
  {
    this.mMutex = new Mutex();
    this.mMutex.WaitOne();
    this.mMutexAcquired = true;
    this.mUnzipSignal = new AutoResetEvent(false);
    AssetDownloader.oldFormat = AssetManager.Format;
  }

  private void Shutdown()
  {
    if (this.mDownloadObserver != null)
      this.mDownloadObserver.Abort();
    if (this.mMutex == null)
      return;
    if (!this.mMutexAcquired)
      this.mMutex.WaitOne();
    this.mShuttingDown = true;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    AssetDownloader.mDownloadProgress = 0.0f;
    AssetDownloader.currentDownloadSize = 0L;
    AssetDownloader.totalDownloadSize = 0L;
    AssetDownloader.downloadedSize = 0L;
    AssetDownloader.mPhase = AssetDownloader.DownloadPhases.FileCheck;
    AssetDownloader.mCompareHashProgress = 0.0f;
    this.mCompareHashProgressShared = 0.0f;
    this.mUnzipSignal.Set();
    if (this.mUnzipThread != null)
    {
      this.mUnzipThread.Join();
      this.mUnzipThread = (Thread) null;
    }
    this.mMutex.Close();
    this.mMutex = (Mutex) null;
    this.mUnzipSignal.Close();
    this.mUnzipSignal = (AutoResetEvent) null;
    if (this.mCompareHashThread != null)
    {
      this.mCompareHashThread.Join();
      this.mCompareHashThread = (Thread) null;
    }
    if (this.mCompareHashMutex == null)
      return;
    this.mCompareHashMutex.Close();
    this.mCompareHashMutex = (Mutex) null;
  }

  private void OnDestroy()
  {
    this.Shutdown();
  }

  private void OnApplicationQuit()
  {
    this.Shutdown();
  }

  private void SetError(Network.EErrCode code, string textID)
  {
    Network.ErrCode = code;
    Network.ErrMsg = LocalizedText.Get(textID);
    Network.ResetError();
    GlobalEvent.Invoke(PredefinedGlobalEvents.ERROR_NETWORK.ToString(), (object) null);
    if (AssetDownloader.mCoroutine != null)
    {
      this.StopCoroutine(AssetDownloader.mCoroutine);
      AssetDownloader.mCoroutine = (Coroutine) null;
    }
    AssetDownloader.mHasError = true;
    if (this.mUnzipThread == null)
      return;
    if (!this.mMutexAcquired)
    {
      this.mMutex.WaitOne();
      this.mMutexAcquired = true;
    }
    if (this.mUnzipThreadArg != null)
      this.mUnzipThreadArg.abort = true;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    this.mUnzipSignal.Set();
    this.mUnzipThread.Join();
    this.mUnzipThread = (Thread) null;
  }

  private void BeginUnzip(byte[] bytes, int size, string dest, List<string> requestIDs, AssetList assetList)
  {
    if (!this.mMutexAcquired)
    {
      this.mMutex.WaitOne();
      this.mMutexAcquired = true;
    }
    AssetDownloader.UnzipJob unzipJob = new AssetDownloader.UnzipJob();
    unzipJob.Bytes = bytes;
    unzipJob.Size = size;
    unzipJob.Dest = dest;
    unzipJob.nodes = new List<AssetDownloader.UnzipJob.Node>();
    using (List<string>.Enumerator enumerator = requestIDs.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        AssetList.Item itemById = assetList.FindItemByID(current);
        unzipJob.nodes.Add(new AssetDownloader.UnzipJob.Node()
        {
          ID = current,
          hash = itemById.Hash
        });
      }
    }
    this.mUnzipJobs.Add(unzipJob);
    if (size <= 0)
      unzipJob.State = AssetDownloader.UnzipJobState.Error;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    this.mUnzipSignal.Set();
  }

  private void UnzipThread()
  {
label_1:
    while (true)
    {
      this.mUnzipSignal.WaitOne();
      this.mMutex.WaitOne();
      if (!this.mShuttingDown)
      {
        if (this.mUnzipJobs == null)
          this.mMutex.ReleaseMutex();
        else
          goto label_5;
      }
      else
        break;
    }
    this.mMutex.ReleaseMutex();
    return;
label_5:
    AssetDownloader.UnzipJob[] array = this.mUnzipJobs.ToArray();
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index].State == AssetDownloader.UnzipJobState.Waiting)
        array[index].State = AssetDownloader.UnzipJobState.Extracting;
    }
    this.mMutex.ReleaseMutex();
    for (int index = 0; index < array.Length; ++index)
    {
      AssetDownloader.UnzipJob unzipJob = array[index];
      if (unzipJob.State == AssetDownloader.UnzipJobState.Extracting)
      {
        if (NativePlugin.UnZip(unzipJob.Dest, unzipJob.Bytes, unzipJob.Size) != 0)
        {
          this.mMutex.WaitOne();
          unzipJob.State = AssetDownloader.UnzipJobState.Error;
          this.mMutex.ReleaseMutex();
        }
        else
        {
          this.mMutex.WaitOne();
          unzipJob.State = AssetDownloader.UnzipJobState.Finished;
          this.mMutex.ReleaseMutex();
        }
      }
    }
    goto label_1;
  }

  private void UnzipThread2()
  {
    AssetDownloader.UnzipThread2Arg mUnzipThreadArg = this.mUnzipThreadArg;
    if (mUnzipThreadArg == null)
      return;
    bool flag = false;
    while (!flag)
    {
      this.mMutex.WaitOne();
      AssetDownloader.UnzipJob[] array = this.mUnzipJobs.ToArray();
      this.mUnzipJobs.Clear();
      bool completed = mUnzipThreadArg.completed;
      bool abort = mUnzipThreadArg.abort;
      bool mShuttingDown = this.mShuttingDown;
      this.mMutex.ReleaseMutex();
      if (mShuttingDown || abort)
        return;
      if (array == null || array.Length <= 0)
      {
        if (completed)
          return;
        this.mUnzipSignal.WaitOne();
      }
      else
      {
        AssetDownloader.UnzipJob unzipJob = (AssetDownloader.UnzipJob) null;
        for (int index1 = 0; index1 < array.Length; ++index1)
        {
          unzipJob = array[index1];
          if (unzipJob.State == AssetDownloader.UnzipJobState.Error)
          {
            flag = true;
            break;
          }
          try
          {
            if (array != null)
              AssetDownloader.UnZipFileSize = (long) ((IEnumerable<AssetDownloader.UnzipJob>) array).Sum<AssetDownloader.UnzipJob>((Func<AssetDownloader.UnzipJob, int>) (arg => arg.Size));
          }
          catch (Exception ex)
          {
            Debug.Log((object) ex.ToString());
          }
          File.WriteAllBytes(AssetDownloader.CachePath + unzipJob.nodes[0].ID, unzipJob.Bytes);
          if ((unzipJob.nodes[0].Item.Flags & AssetBundleFlags.IsCombined) != (AssetBundleFlags) 0)
          {
            int size = 0;
            string path = AssetDownloader.CachePath + unzipJob.nodes[0].ID;
            if ((unzipJob.nodes[0].Item.Flags & AssetBundleFlags.Compressed) != (AssetBundleFlags) 0)
            {
              IntPtr num1 = NativePlugin.DecompressFile(path, out size);
              if (num1 == IntPtr.Zero)
                throw new Exception("Failed to decompress file [" + path + "]");
              byte[] numArray = new byte[size];
              Marshal.Copy(num1, numArray, 0, size);
              using (BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(numArray)))
              {
                int num2 = binaryReader.ReadInt32();
                for (int index2 = 0; index2 < num2; ++index2)
                {
                  string lower = binaryReader.ReadInt32().ToString("X8").ToLower();
                  int count = binaryReader.ReadInt32();
                  byte[] bytes = binaryReader.ReadBytes(count);
                  File.WriteAllBytes(AssetDownloader.CachePath + lower, bytes);
                  AssetList.Item itemById = mUnzipThreadArg.assetlist.FindItemByID(lower);
                  itemById.Exist = true;
                  AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(itemById.ID, mUnzipThreadArg.assetlist.SearchItemIdx(itemById.ID)));
                }
              }
              NativePlugin.FreePtr(num1);
              File.Delete(path + ".tmp");
            }
            else
            {
              using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(path, FileMode.Open)))
              {
                int num = binaryReader.ReadInt32();
                for (int index2 = 0; index2 < num; ++index2)
                {
                  string lower = binaryReader.ReadInt32().ToString("X8").ToLower();
                  int count = binaryReader.ReadInt32();
                  byte[] bytes = binaryReader.ReadBytes(count);
                  File.WriteAllBytes(AssetDownloader.CachePath + lower, bytes);
                  AssetList.Item itemById = mUnzipThreadArg.assetlist.FindItemByID(lower);
                  itemById.Exist = true;
                  AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(itemById.ID, mUnzipThreadArg.assetlist.SearchItemIdx(itemById.ID)));
                }
              }
            }
          }
          using (List<AssetDownloader.UnzipJob.Node>.Enumerator enumerator = unzipJob.nodes.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              AssetDownloader.UnzipJob.Node current = enumerator.Current;
              if (!File.Exists(unzipJob.Dest + current.ID))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
          {
            using (List<AssetDownloader.UnzipJob.Node>.Enumerator enumerator = unzipJob.nodes.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                AssetDownloader.UnzipJob.Node current = enumerator.Current;
                current.Item.Exist = true;
                AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(current.Item.ID, mUnzipThreadArg.assetlist.SearchItemIdx(current.Item.ID)));
              }
            }
          }
          else
            break;
        }
        try
        {
          AssetDownloader.UnZipFileSize = 0L;
          if (this.mDownloadObserver != null)
            this.mDownloadObserver.IsWait = false;
        }
        catch (Exception ex)
        {
          Debug.Log((object) ex.ToString());
        }
        if (flag && unzipJob != null)
        {
          for (int index = 0; index < unzipJob.nodes.Count; ++index)
          {
            string path1 = unzipJob.Dest + unzipJob.nodes[index].ID;
            if (File.Exists(path1))
              File.Delete(path1);
            string path2 = unzipJob.Dest + unzipJob.nodes[index].ID + ".meta";
            if (!File.Exists(path2))
              File.Delete(path2);
            unzipJob.nodes[index].Item.Exist = false;
          }
        }
      }
    }
    this.mMutex.WaitOne();
    mUnzipThreadArg.error = true;
    this.mMutex.ReleaseMutex();
  }

  private static long GetFileSize(string path)
  {
    try
    {
      return new FileInfo(path).Length;
    }
    catch (Exception ex)
    {
      return 0;
    }
  }

  private void CompareFileListHashThread(object args)
  {
    AssetDownloader.CompareFileListHashArg compareFileListHashArg = args as AssetDownloader.CompareFileListHashArg;
    if (compareFileListHashArg == null || string.IsNullOrEmpty(compareFileListHashArg.cacheDir) || compareFileListHashArg.nodes == null)
      return;
    string cacheDir = compareFileListHashArg.cacheDir;
    AssetList assetList = AssetManager.AssetList;
    if (compareFileListHashArg.dic.Count == 0)
    {
      AssetDownloader.mExistFile.Clear();
      for (int index = 0; index < compareFileListHashArg.nodes.Count; ++index)
      {
        this.mCompareHashMutex.WaitOne();
        this.mCompareHashProgressShared = (float) index / (float) compareFileListHashArg.nodes.Count;
        this.mCompareHashMutex.ReleaseMutex();
        AssetDownloader.CompareFileListHashArg.Node node = compareFileListHashArg.nodes[index];
        if (node != null && !string.IsNullOrEmpty(node.IDStr) && !string.IsNullOrEmpty(node.metaPath))
        {
          string path = cacheDir + node.IDStr;
          string metaPath = node.metaPath;
          bool flag1 = File.Exists(path);
          bool flag2 = false;
          if (File.Exists(metaPath))
          {
            if (flag1)
            {
              if (AssetDownloader.GetFileSize(path) != (long) node.Size)
              {
                flag2 = true;
                goto label_17;
              }
            }
            try
            {
              using (FileStream fileStream = new FileStream(metaPath, FileMode.Open))
              {
                uint num = new BinaryReader((Stream) fileStream).ReadUInt32();
                if ((int) node.Hash != (int) num)
                  flag2 = true;
              }
            }
            catch (Exception ex)
            {
              flag2 = true;
            }
label_17:
            if (flag2)
              File.Delete(metaPath);
          }
          else
            flag2 = true;
          if (flag1)
          {
            if (flag2)
            {
              File.Delete(path);
            }
            else
            {
              node.Item.Exist = true;
              AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(node.Item.ID, assetList.SearchItemIdx(node.Item.ID)));
            }
          }
        }
      }
    }
    else
    {
      AssetDownloader.ExistAssetList[] array = AssetDownloader.mExistFile.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        this.mCompareHashMutex.WaitOne();
        this.mCompareHashProgressShared = (float) index / (float) array.Length;
        this.mCompareHashMutex.ReleaseMutex();
        bool flag = false;
        AssetList.Item itemById = AssetManager.AssetList.FastFindItemByID(array[index].FileID);
        AssetList.Item obj;
        if (itemById != null && compareFileListHashArg.dic.TryGetValue(array[index].FileID, out obj))
        {
          if (flag | itemById.Size != obj.Size | (int) itemById.Hash != (int) obj.Hash)
            AssetDownloader.mExistFile.Remove(array[index]);
          else
            itemById.Exist = true;
        }
      }
    }
  }

  public static void Add(string assetID)
  {
    if (AssetDownloader.mRequestIDs.Contains(assetID))
      return;
    AssetDownloader.mRequestIDs.Add(assetID);
  }

  public static bool isDone
  {
    get
    {
      return AssetDownloader.Instance.Internal_isDone;
    }
  }

  private bool Internal_isDone
  {
    get
    {
      if (AssetDownloader.mCoroutine == null)
        return AssetDownloader.mRequestIDs.Count == 0;
      return false;
    }
  }

  public static AssetDownloader.DownloadState StartDownload(bool checkUpdates, bool canRetry = true, ThreadPriority threadPriority = ThreadPriority.Normal)
  {
    if (AssetDownloader.mCoroutine != null || !checkUpdates && AssetDownloader.mRequestIDs.Count == 0)
      return (AssetDownloader.DownloadState) null;
    if (AssetManager.Format == AssetManager.AssetFormats.Text)
    {
      AssetDownloader.mCachePath = AssetDownloader.mCachePath.Replace(AssetManager.Format.ToPath(), AssetDownloader.oldFormat.ToPath());
      AssetManager.Format = AssetDownloader.oldFormat;
    }
    AssetDownloader.DownloadState state = new AssetDownloader.DownloadState();
    AssetDownloader.mRetryOnError = canRetry;
    AssetDownloader.mCoroutine = AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.InternalDownloadAssets(state, checkUpdates, false, threadPriority));
    return state;
  }

  public static AssetDownloader.DownloadState StartTextDownload(bool checkUpdates, bool canRetry = true, ThreadPriority threadPriority = ThreadPriority.Normal)
  {
    if (AssetDownloader.mCoroutine != null || !checkUpdates && AssetDownloader.mRequestIDs.Count == 0 || AssetDownloader.mDownloadedText)
      return (AssetDownloader.DownloadState) null;
    if (AssetManager.Format != AssetManager.AssetFormats.Text)
    {
      AssetDownloader.mCachePath = AssetDownloader.mTextCachePath;
      AssetManager.Format = AssetManager.AssetFormats.Text;
    }
    AssetDownloader.DownloadState state = new AssetDownloader.DownloadState();
    AssetDownloader.mRetryOnError = canRetry;
    AssetDownloader.mCoroutine = AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.InternalDownloadAssets(state, checkUpdates, false, threadPriority));
    return state;
  }

  private string ComposeDownloadURI(string prefix, string[] fileID)
  {
    StringBuilder stringBuilder = new StringBuilder(1000);
    stringBuilder.Append(prefix);
    for (int index = 0; index < fileID.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(',');
      stringBuilder.Append(fileID[index]);
    }
    return stringBuilder.ToString();
  }

  private void RecordDownloadSpeed(float bytesPerSecond)
  {
    this.mSpeedHistory[this.mSpeedHistoryPos] = bytesPerSecond;
    this.mSpeedHistoryPos = (this.mSpeedHistoryPos + 1) % this.mSpeedHistory.Length;
    this.mSpeedHistorySize = Mathf.Min(this.mSpeedHistorySize + 1, this.mSpeedHistory.Length);
    AssetDownloader.mAverageDownloadSpeed = 0.0f;
    for (int index = this.mSpeedHistorySize - 1; index >= 0; --index)
      AssetDownloader.mAverageDownloadSpeed += this.mSpeedHistory[index];
    AssetDownloader.mAverageDownloadSpeed /= (float) this.mSpeedHistorySize;
  }

  private AssetDownloader.UnzipJobState UnzipState
  {
    get
    {
      if (!this.mMutexAcquired)
      {
        this.mMutex.WaitOne();
        this.mMutexAcquired = true;
      }
      AssetDownloader.UnzipJobState unzipJobState = AssetDownloader.UnzipJobState.Waiting;
      int num = 0;
      for (int index = 0; index < this.mUnzipJobs.Count; ++index)
      {
        if (this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Error || this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Extracting)
        {
          unzipJobState = this.mUnzipJobs[index].State;
          break;
        }
        if (this.mUnzipJobs[index].State == AssetDownloader.UnzipJobState.Finished)
          ++num;
      }
      if (this.mUnzipJobs.Count > 0 && this.mUnzipJobs.Count == num)
        unzipJobState = AssetDownloader.UnzipJobState.Finished;
      this.mMutex.ReleaseMutex();
      this.mMutexAcquired = false;
      return unzipJobState;
    }
  }

  private byte[] LoadFileList()
  {
    byte[] numArray = (byte[]) null;
    if (File.Exists(AssetDownloader.FileListPath))
    {
      using (FileStream fileStream = new FileStream(AssetDownloader.FileListPath, FileMode.Open))
        numArray = new BinaryReader((Stream) fileStream).ReadBytes((int) fileStream.Length);
    }
    return numArray;
  }

  private void AddRequiredAssets(string cacheDir, AssetList.Item[] assets)
  {
    AssetList assetList;
    bool flag1;
    if (AssetManager.Format == AssetManager.AssetFormats.Text)
    {
      assetList = AssetManager.TxtAssetList;
      flag1 = true;
    }
    else
    {
      assetList = AssetManager.AssetList;
      flag1 = false;
    }
    AssetList.Item obj = (AssetList.Item) null;
    List<string> stringList = new List<string>();
    for (int index = 0; index < assets.Length; ++index)
    {
      AssetList.Item asset = assets[index];
      if ((asset.Flags & AssetBundleFlags.Required) != (AssetBundleFlags) 0)
      {
        bool flag2 = asset.Size > 0 && !asset.Exist && asset.IsMatchLanguageItem();
        if (GameUtility.Config_Language == "english" || flag1)
        {
          if (flag2)
            stringList.Add(asset.IDStr);
        }
        else
        {
          string localizedObjectName = AssetManager.GetLocalizedObjectName(asset.Path, false);
          AssetList.Item itemByPath = assetList.FindItemByPath(localizedObjectName);
          if (localizedObjectName == asset.Path || itemByPath == null)
          {
            if (flag2)
              stringList.Add(asset.IDStr);
          }
          else
            this.DownloadLocalizedAssetAndDependencies(itemByPath, assetList);
        }
      }
      else if (asset.Path == "reqAssetPack")
        obj = asset;
    }
    if (obj == null || stringList.Count < 30 || GameUtility.Config_Language != "english")
    {
      using (List<string>.Enumerator enumerator = stringList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          AssetDownloader.Add(enumerator.Current);
      }
    }
    else
      AssetDownloader.Add(obj.IDStr);
    stringList.Clear();
  }

  private void DownloadLocalizedAssetAndDependencies(AssetList.Item node, AssetList assetList)
  {
    if (node == null)
      return;
    if (!node.Exist)
      AssetDownloader.Add(node.IDStr);
    for (int index = 0; index < node.Dependencies.Length; ++index)
    {
      if (!File.Exists(AssetDownloader.CachePath + node.Dependencies[index].IDStr))
        AssetDownloader.Add(node.Dependencies[index].IDStr);
    }
    for (int index = 0; index < node.AdditionalDependencies.Length; ++index)
    {
      string path = AssetDownloader.CachePath + node.AdditionalDependencies[index].IDStr;
      string localizedObjectName = AssetManager.GetLocalizedObjectName(node.AdditionalDependencies[index].Path, false);
      AssetList.Item itemByPath = assetList.FindItemByPath(localizedObjectName);
      if (localizedObjectName == node.AdditionalDependencies[index].Path || itemByPath == null)
      {
        if (!File.Exists(path))
          AssetDownloader.Add(node.AdditionalDependencies[index].IDStr);
      }
      else if (!File.Exists(AssetDownloader.CachePath + itemByPath.IDStr))
        AssetDownloader.Add(itemByPath.IDStr);
    }
    for (int index = 0; index < node.AdditionalStreamingAssets.Length; ++index)
    {
      string path = AssetDownloader.CachePath + node.AdditionalStreamingAssets[index].IDStr;
      string localizedObjectName = AssetManager.GetLocalizedObjectName(node.AdditionalStreamingAssets[index].Path, false);
      AssetList.Item itemByPath = assetList.FindItemByPath(localizedObjectName);
      if (localizedObjectName == node.AdditionalStreamingAssets[index].Path || itemByPath == null)
      {
        if (!File.Exists(path))
          AssetDownloader.Add(node.AdditionalStreamingAssets[index].IDStr);
      }
      else if (!File.Exists(AssetDownloader.CachePath + itemByPath.IDStr))
      {
        DebugUtility.LogWarning("Downloading localized streaming dependency: " + localizedObjectName);
        AssetDownloader.Add(itemByPath.IDStr);
      }
    }
  }

  private void RemoveCompletedDownloadRequests(string cacheDir, AssetList assets)
  {
    for (int index = 0; index < AssetDownloader.mRequestIDs.Count; ++index)
    {
      AssetList.Item itemById = assets.FindItemByID(AssetDownloader.mRequestIDs[index]);
      if (itemById != null && itemById.Exist)
        AssetDownloader.mRequestIDs.RemoveAt(index--);
    }
  }

  private static ThreadPriority TranslateThreadPriority(ThreadPriority priority)
  {
    switch (priority)
    {
      case ThreadPriority.Lowest:
        return (ThreadPriority) 0;
      case ThreadPriority.BelowNormal:
        return (ThreadPriority) 1;
      case ThreadPriority.Highest:
        return (ThreadPriority) 4;
      default:
        return (ThreadPriority) 2;
    }
  }

  private bool IsUnZipWorkerThreadNotTight()
  {
    if (this.mUnzipJobs == null)
      return false;
    return this.mUnzipJobs.Count < 10;
  }

  private string ComposeDownloadURI(string prefix, string fileID)
  {
    return prefix + fileID;
  }

  private void BeginUnzip(byte[] bytes, int size, string dest, string requestID, AssetList assetList)
  {
    if (!this.mMutexAcquired)
    {
      this.mMutex.WaitOne();
      this.mMutexAcquired = true;
    }
    AssetDownloader.UnzipJob unzipJob = new AssetDownloader.UnzipJob();
    unzipJob.Bytes = bytes;
    unzipJob.Size = size;
    unzipJob.Dest = dest;
    unzipJob.nodes = new List<AssetDownloader.UnzipJob.Node>();
    AssetList.Item itemById = assetList.FindItemByID(requestID);
    unzipJob.nodes.Add(new AssetDownloader.UnzipJob.Node()
    {
      ID = requestID,
      hash = itemById.Hash,
      Item = itemById
    });
    this.mUnzipJobs.Add(unzipJob);
    if (size <= 0)
      unzipJob.State = AssetDownloader.UnzipJobState.Error;
    this.mMutex.ReleaseMutex();
    this.mMutexAcquired = false;
    this.mUnzipSignal.Set();
  }

  [DebuggerHidden]
  private IEnumerator ParallelDonwloading(AssetList assetList, ThreadPriority threadPriority, string prefix, string cacheDir, Dictionary<string, int> itemCompressedSize)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CParallelDonwloading\u003Ec__Iterator43() { prefix = prefix, itemCompressedSize = itemCompressedSize, cacheDir = cacheDir, assetList = assetList, \u003C\u0024\u003Eprefix = prefix, \u003C\u0024\u003EitemCompressedSize = itemCompressedSize, \u003C\u0024\u003EcacheDir = cacheDir, \u003C\u0024\u003EassetList = assetList, \u003C\u003Ef__this = this };
  }

  private void LoadExistFile()
  {
    AssetDownloader.mExistFile.Clear();
    if (!File.Exists(AssetDownloader.CachePath + this.FileExistName))
      return;
    using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(AssetDownloader.CachePath + this.FileExistName, FileMode.Open)))
    {
      long length = binaryReader.BaseStream.Length;
      byte[] numArray = binaryReader.ReadBytes((int) length);
      int num = 8;
      int startIndex1 = 0;
      for (int index = 0; (long) index < length / (long) num; ++index)
      {
        uint uint32 = BitConverter.ToUInt32(numArray, startIndex1);
        int startIndex2 = startIndex1 + 4;
        int int32 = BitConverter.ToInt32(numArray, startIndex2);
        startIndex1 = startIndex2 + 4;
        AssetDownloader.mExistFile.Add(new AssetDownloader.ExistAssetList(uint32, int32));
      }
    }
  }

  private void CreateExistFile()
  {
    using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(AssetDownloader.CachePath + this.FileExistName, FileMode.Create)))
    {
      for (int index = 0; index < AssetDownloader.mExistFile.Count; ++index)
      {
        binaryWriter.Write(AssetDownloader.mExistFile[index].FileID);
        binaryWriter.Write(AssetDownloader.mExistFile[index].AssetID);
      }
    }
  }

  [DebuggerHidden]
  private IEnumerator InternalDownloadAssets(AssetDownloader.DownloadState state, bool checkUpdates, bool isRetry, ThreadPriority threadPriority)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CInternalDownloadAssets\u003Ec__Iterator44() { checkUpdates = checkUpdates, isRetry = isRetry, threadPriority = threadPriority, state = state, \u003C\u0024\u003EcheckUpdates = checkUpdates, \u003C\u0024\u003EisRetry = isRetry, \u003C\u0024\u003EthreadPriority = threadPriority, \u003C\u0024\u003Estate = state, \u003C\u003Ef__this = this };
  }

  public void FileCheckThread(object arg)
  {
    AssetDownloader.FileCheckArg fileCheckArg = arg as AssetDownloader.FileCheckArg;
    if (fileCheckArg == null)
      return;
    for (int index = 0; index < AssetDownloader.mExistFile.Count; ++index)
    {
      AssetList.Item itemById = fileCheckArg.assetList.FastFindItemByID(AssetDownloader.mExistFile[index].FileID);
      if (itemById != null)
        itemById.Exist = true;
    }
  }

  [DebuggerHidden]
  private IEnumerator ConfirmRetry(AssetDownloader.RetryParam param)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CConfirmRetry\u003Ec__Iterator45() { param = param, \u003C\u0024\u003Eparam = param };
  }

  public static AssetDownloader.DownloadPhases Phase
  {
    get
    {
      return AssetDownloader.mPhase;
    }
  }

  public static float Progress
  {
    get
    {
      if (AssetDownloader.mPhase == AssetDownloader.DownloadPhases.FileCheck)
        return AssetDownloader.mCompareHashProgress;
      return AssetDownloader.mDownloadProgress;
    }
  }

  public static long TotalDownloadSize
  {
    get
    {
      if (AssetDownloader.totalDownloadSize <= 0L)
        return 0;
      return (AssetDownloader.totalDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static long CurrentDownloadSize
  {
    get
    {
      if (AssetDownloader.currentDownloadSize <= 0L)
        return 0;
      return (AssetDownloader.currentDownloadSize - 1L) / 1048576L + 1L;
    }
  }

  public static string CachePath
  {
    get
    {
      if (AssetDownloader.mCachePath == null)
        AssetDownloader.mCachePath = Application.get_persistentDataPath() + "/new_" + AssetManager.Format.ToPath();
      return AssetDownloader.mCachePath;
    }
  }

  public static string TextCachePath
  {
    get
    {
      if (AssetDownloader.mTextCachePath == null)
        AssetDownloader.mTextCachePath = Application.get_persistentDataPath() + "/new_" + AssetManager.AssetFormats.Text.ToPath();
      return AssetDownloader.mTextCachePath;
    }
  }

  public static string CachePathOld
  {
    get
    {
      return Application.get_temporaryCachePath() + "/" + AssetManager.Format.ToPath();
    }
  }

  public static string OldDownloadPath
  {
    get
    {
      return Application.get_persistentDataPath() + "/" + AssetManager.Format.ToPath();
    }
  }

  public static string FileListPath
  {
    get
    {
      return AssetDownloader.CachePath + "ASSETS";
    }
  }

  public static string FileListVerPath
  {
    get
    {
      return AssetDownloader.CachePath + "ASSETS.VER";
    }
  }

  public static string AssetListPath
  {
    get
    {
      return AssetDownloader.CachePath + "ASSETLIST";
    }
  }

  public static string AssetListTmpPath
  {
    get
    {
      return AssetDownloader.CachePath + "tmp/ASSETLIST";
    }
  }

  public static string TxtAssetListPath
  {
    get
    {
      return AssetDownloader.TextCachePath + "ASSETLIST";
    }
  }

  public static string TxtAssetListTmpPath
  {
    get
    {
      return AssetDownloader.TextCachePath + "tmp/ASSETLIST";
    }
  }

  public static string AssetListTmpDir
  {
    get
    {
      return AssetDownloader.CachePath + "tmp/";
    }
  }

  public static string RevisionFilePath
  {
    get
    {
      return AssetDownloader.CachePath + "REVISION.dat";
    }
  }

  public static string TxtRevisionFilePath
  {
    get
    {
      return AssetDownloader.TextCachePath + "REVISION.dat";
    }
  }

  public static string RevisionTmpFilePath
  {
    get
    {
      return AssetDownloader.CachePath + "REVISION.tmp";
    }
  }

  public static string ExistFilePath
  {
    get
    {
      return AssetDownloader.CachePath + "EXISTLIST";
    }
  }

  public static string TxtExistFilePath
  {
    get
    {
      return AssetDownloader.TextCachePath + "EXISTLIST";
    }
  }

  private static unsafe bool CompareBytes(byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      return false;
    int length = a.Length;
    byte* numPtr1 = a == null || a.Length == 0 ? (byte*) null : &a[0];
    byte* numPtr2 = b == null || b.Length == 0 ? (byte*) null : &b[0];
    for (int index = 0; index < length; ++index)
    {
      if ((int) numPtr1[index] != (int) numPtr2[index])
        return false;
    }
    numPtr2 = (byte*) null;
    numPtr1 = (byte*) null;
    return true;
  }

  public static void ClearCache()
  {
    string cachePath = AssetDownloader.CachePath;
    if (File.Exists(AssetDownloader.RevisionFilePath))
      File.Delete(AssetDownloader.RevisionFilePath);
    if (File.Exists(AssetDownloader.TxtRevisionFilePath))
      File.Delete(AssetDownloader.TxtRevisionFilePath);
    if (File.Exists(AssetDownloader.AssetListPath))
      File.Delete(AssetDownloader.AssetListPath);
    if (File.Exists(AssetDownloader.TxtAssetListPath))
      File.Delete(AssetDownloader.TxtAssetListPath);
    if (File.Exists(AssetDownloader.ExistFilePath))
      File.Delete(AssetDownloader.ExistFilePath);
    if (File.Exists(AssetDownloader.TxtExistFilePath))
      File.Delete(AssetDownloader.TxtExistFilePath);
    if (Directory.Exists(cachePath))
      Directory.Delete(cachePath, true);
    if (Directory.Exists(AssetDownloader.TextCachePath))
      Directory.Delete(AssetDownloader.TextCachePath, true);
    if (!AssetManager.HasInstance)
      return;
    AssetList assetList = AssetManager.AssetList;
    if (assetList != null)
    {
      AssetList.Item[] assetsOriginal = assetList.Assets_Original;
      for (int index = assetsOriginal.Length - 1; index >= 0; --index)
        assetsOriginal[index].Exist = false;
    }
    AssetList txtAssetList = AssetManager.TxtAssetList;
    if (txtAssetList == null)
      return;
    AssetList.Item[] assetsOriginal1 = txtAssetList.Assets_Original;
    for (int index = assetsOriginal1.Length - 1; index >= 0; --index)
      assetsOriginal1[index].Exist = false;
  }

  public static void DestroyAssetStart(AssetBundleFlags flags)
  {
    AssetDownloader.Instance.StartCoroutine(AssetDownloader.Instance.DestroyAsset(flags));
  }

  [DebuggerHidden]
  public IEnumerator DestroyAsset(AssetBundleFlags flags)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AssetDownloader.\u003CDestroyAsset\u003Ec__Iterator46() { flags = flags, \u003C\u0024\u003Eflags = flags, \u003C\u003Ef__this = this };
  }

  public enum DownloadPhases
  {
    FileCheck,
    Download,
  }

  private enum UnzipJobState
  {
    Error = -1,
    Waiting = 0,
    Extracting = 1,
    Finished = 2,
  }

  private class UnzipJob
  {
    public byte[] Bytes;
    public int Size;
    public string Dest;
    public AssetDownloader.UnzipJobState State;
    public List<AssetDownloader.UnzipJob.Node> nodes;

    public class Node
    {
      public string ID;
      public uint hash;
      public AssetList.Item Item;
    }
  }

  public class ExistAssetList
  {
    public uint FileID;
    public int AssetID;

    public ExistAssetList(uint file, int asset)
    {
      this.FileID = file;
      this.AssetID = asset;
    }
  }

  private class UnzipThread2Arg
  {
    public bool completed;
    public bool abort;
    public bool error;
    public AssetList assetlist;
  }

  private class CompareFileListHashArg
  {
    public List<AssetDownloader.CompareFileListHashArg.Node> nodes;
    public string cacheDir;
    public Dictionary<uint, AssetList.Item> dic;

    public class Node
    {
      public string IDStr;
      public string metaPath;
      public uint Hash;
      public int Size;
      public AssetList.Item Item;
    }
  }

  public class DownloadState
  {
    public bool Finished;
    public bool HasError;
  }

  private class StorageException : Exception
  {
  }

  private class WWWException : Exception
  {
    private int mStatusCode;
    private string mMessage;

    public WWWException(WWW www)
    {
      this.mStatusCode = int.Parse(www.get_responseHeaders()["STATUS"]);
      this.mMessage = www.get_error();
    }

    public int StatusCode
    {
      get
      {
        return this.mStatusCode;
      }
    }

    public override string Message
    {
      get
      {
        return this.mMessage;
      }
    }
  }

  private class FileCheckArg
  {
    public AssetList assetList;
  }

  private class RetryParam
  {
    public ThreadPriority threadPriority = ThreadPriority.Normal;
    public AssetDownloader downloader;
    public AssetDownloader.DownloadState state;
    public bool checkUpdates;
    public bool isRetry;

    public void RetryEvent(bool retry)
    {
      if (retry)
        this.downloader.StartCoroutine(this.downloader.InternalDownloadAssets(this.state, this.checkUpdates, this.isRetry, this.threadPriority));
      else
        FlowNode_LoadScene.LoadBootScene();
    }
  }
}
