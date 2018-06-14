// Decompiled with JetBrains decompiler
// Type: DownloadObseve
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class DownloadObseve
{
  private HttpClient _httpClient = new HttpClient();
  private IEnumerator _downloadEnumerator;
  private MonoBehaviour _coroutineExecuter;
  public int CompressedSize;

  public DownloadObseve()
  {
    this.State = DownloadObseve.StateEnum.IsRunning;
    this.State = DownloadObseve.StateEnum.IsNone;
  }

  public event DownloadObseve.FailureHandler Failure = _param0 => {};

  public event DownloadObseve.LoadingHandler Loading = (_param0, _param1) => {};

  public event DownloadObseve.SuccessHandler Success = (_param0, _param1, _param2, _param3, _param4) => {};

  public event DownloadObseve.RetryHandler Retry = () => {};

  public event DownloadObseve.CanWriteHandler CanWrite = _param0 => false;

  private string _url { get; set; }

  private string _identificationKey { get; set; }

  public int RetryCount { get; private set; }

  public int MaxRetryNumber { get; private set; }

  public int ContentsSize
  {
    get
    {
      return this._httpClient.ContentLength;
    }
  }

  public bool CanWriteStream
  {
    get
    {
      return this._httpClient.CanWriteStream;
    }
  }

  public DownloadObseve.StateEnum State { get; private set; }

  public void SetRetryNumber(int retryNumber)
  {
    this.MaxRetryNumber = retryNumber;
  }

  public void StartDownload(string identificationKey, MonoBehaviour behabior, string url)
  {
    this._identificationKey = identificationKey;
    this._url = url.Replace("http", "https");
    this.State = DownloadObseve.StateEnum.IsRunning;
    this._coroutineExecuter = behabior;
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this.Download();
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  [DebuggerHidden]
  private IEnumerator Download()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObseve.\u003CDownload\u003Ec__Iterator7E()
    {
      \u003C\u003Ef__this = this
    };
  }

  public DownloadObseve.DownloadInfo getDownloadInfo()
  {
    DownloadObseve.DownloadInfo downloadInfo;
    downloadInfo.identificationKey = this._identificationKey;
    if (this._httpClient != null)
    {
      downloadInfo.position = (long) this._httpClient.LoadingSize;
      downloadInfo.ContentLength = this._httpClient.ContentLength;
    }
    else
    {
      downloadInfo.position = 0L;
      downloadInfo.ContentLength = 0;
    }
    return downloadInfo;
  }

  private void RetryDownload()
  {
    Debug.Log((object) ("download retry[" + (object) this.RetryCount + "]..."));
    ++this.RetryCount;
    this.Retry();
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this.Download();
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  private bool CanRetryDownload()
  {
    return this.RetryCount < this.MaxRetryNumber;
  }

  [DebuggerHidden]
  public IEnumerator Abort()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloadObseve.\u003CAbort\u003Ec__Iterator7F()
    {
      \u003C\u003Ef__this = this
    };
  }

  public void Dispose()
  {
    if (this._httpClient != null)
      this._httpClient.Dispose();
    this._httpClient = (HttpClient) null;
    this._url = (string) null;
    this._identificationKey = (string) null;
  }

  public enum StateEnum
  {
    IsRunning,
    IsCompleted,
    IsFailureFinished,
    IsDestroying,
    IsDestroyed,
    IsNone,
  }

  public struct DownloadInfo
  {
    public string identificationKey;
    public long position;
    public int ContentLength;
  }

  public delegate void FailureHandler(string errorText);

  public delegate void LoadingHandler(string identificationKey, int loadingSize);

  public delegate void SuccessHandler(DownloadObseve observe, string identificationKey, byte[] bytes, int size, double downloadTime);

  public delegate bool CanWriteHandler(int contentsSize);

  public delegate void RetryHandler();
}
