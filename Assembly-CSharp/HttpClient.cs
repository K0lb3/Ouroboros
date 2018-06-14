// Decompiled with JetBrains decompiler
// Type: HttpClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using UnityEngine;

public class HttpClient
{
  protected static byte[] Eol = new byte[2]
  {
    (byte) 13,
    (byte) 10
  };
  private const int SleepTime = 50;
  public bool IsGotRest;
  public bool CanWriteStream;
  private MemoryStream _requestData;
  private byte[] _exchangedBytes;
  private long _downloadStart;
  private long _downloadEnd;
  private Thread _thread;
  private TcpClient _tcpClient;
  private int _timeOutCount;

  public bool IsDone { get; private set; }

  public string HasError { get; private set; }

  public int ContentLength { get; private set; }

  private static void PrintCertificate(X509Certificate certificate)
  {
    Debug.Log((object) "===========================================");
    Debug.Log((object) ("Subject=" + certificate.Subject));
    Debug.Log((object) ("Issuer=" + certificate.Issuer));
    Debug.Log((object) ("Format=" + certificate.GetFormat()));
    Debug.Log((object) ("ExpirationDate=" + certificate.GetExpirationDateString()));
    Debug.Log((object) ("EffectiveDate=" + certificate.GetEffectiveDateString()));
    Debug.Log((object) ("KeyAlgorithm=" + certificate.GetKeyAlgorithm()));
    Debug.Log((object) ("PublicKey=" + certificate.GetPublicKeyString()));
    Debug.Log((object) ("SerialNumber=" + certificate.GetSerialNumberString()));
    Debug.Log((object) "===========================================");
  }

  private static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
  {
    return true;
  }

  public void Download(string url)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    HttpClient.\u003CDownload\u003Ec__AnonStorey29A downloadCAnonStorey29A = new HttpClient.\u003CDownload\u003Ec__AnonStorey29A();
    // ISSUE: reference to a compiler-generated field
    downloadCAnonStorey29A.url = url;
    // ISSUE: reference to a compiler-generated field
    downloadCAnonStorey29A.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    downloadCAnonStorey29A.timeout = 10000;
    this._exchangedBytes = (byte[]) null;
    this.IsDone = false;
    this.IsGotRest = false;
    this.ContentLength = 0;
    this.CanWriteStream = false;
    this.HasError = string.Empty;
    this._downloadStart = Stopwatch.GetTimestamp();
    // ISSUE: reference to a compiler-generated method
    this._thread = new Thread(new ThreadStart(downloadCAnonStorey29A.\u003C\u003Em__260))
    {
      IsBackground = true
    };
    this._thread.Start();
  }

  private void CollectHeaders(Stream inputStream)
  {
    while (true)
    {
      string[] strArray;
      do
      {
        strArray = HttpClient.ReadKeyValue(inputStream);
        if (strArray == null)
          goto label_3;
      }
      while (!(strArray[0].ToLower() == "content-length"));
      this.ContentLength = int.Parse(strArray[1]);
    }
label_3:;
  }

  private static string[] ReadKeyValue(Stream stream)
  {
    string str = HttpClient.ReadLine(stream);
    if (str == string.Empty)
      return (string[]) null;
    int length = str.IndexOf(':');
    if (length == -1)
      return (string[]) null;
    return new string[2]
    {
      str.Substring(0, length).Trim(),
      str.Substring(length + 1).Trim()
    };
  }

  private void WriteMemoryStreamResponse(Stream stream)
  {
    this._requestData = new MemoryStream(this.ContentLength);
    int millisecondsTimeout = 100;
    this._timeOutCount = 0;
    stream.BeginRead(this._requestData.GetBuffer(), 0, this._requestData.Capacity, new AsyncCallback(this.ReadCallback), (object) stream);
    while (!this.IsDone)
    {
      Thread.Sleep(millisecondsTimeout);
      this._timeOutCount += millisecondsTimeout;
      if (this._timeOutCount >= 10000)
      {
        this.HasError = "error WriteMemoryStreamResponse(): time out";
        Debug.Log((object) this.HasError);
        this.IsDone = true;
        if (stream != null)
          stream.Dispose();
        if (this._tcpClient != null)
          this._tcpClient.Close();
      }
    }
  }

  private void ReadCallback(IAsyncResult asyncResult)
  {
    Stream asyncState = (Stream) asyncResult.AsyncState;
    try
    {
      int num = asyncState.EndRead(asyncResult);
      if (num > 0)
      {
        this._timeOutCount = 0;
        this._requestData.Position += (long) num;
        asyncState.BeginRead(this._requestData.GetBuffer(), (int) this._requestData.Position, this._requestData.Capacity - (int) this._requestData.Position, new AsyncCallback(this.ReadCallback), (object) asyncState);
      }
      else
      {
        this._exchangedBytes = this._requestData.GetBuffer();
        asyncState.Dispose();
        this._downloadEnd = Stopwatch.GetTimestamp();
        this.IsDone = true;
      }
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("ReadCallback: " + ex.Message));
    }
  }

  public int LoadingSize
  {
    get
    {
      try
      {
        return this._requestData == null ? 0 : (int) this._requestData.Position;
      }
      catch (ObjectDisposedException ex)
      {
        DebugUtility.LogException((Exception) ex);
        return 0;
      }
    }
  }

  public byte[] DataBytes
  {
    get
    {
      return this._exchangedBytes;
    }
  }

  public int DownloadedSize
  {
    get
    {
      if (this._exchangedBytes == null)
        return 0;
      return this._exchangedBytes.Length;
    }
  }

  public double DownloadTime
  {
    get
    {
      return (double) (this._downloadEnd - this._downloadStart) / (double) Stopwatch.Frequency;
    }
  }

  private void WriteHeaderTo(Stream socket, Uri uri)
  {
    BinaryWriter binaryWriter = new BinaryWriter(socket);
    binaryWriter.Write(Encoding.ASCII.GetBytes("GET " + uri.PathAndQuery + " HTTP/1.0"));
    binaryWriter.Write(HttpClient.Eol);
    binaryWriter.Write(Encoding.ASCII.GetBytes("Accept: */*"));
    binaryWriter.Write(HttpClient.Eol);
    string str = uri.Host;
    if (uri.Port != 80 && uri.Port != 443)
      str = str + ":" + uri.Port.ToString();
    binaryWriter.Write(Encoding.ASCII.GetBytes("Host: " + str));
    binaryWriter.Write(HttpClient.Eol);
    binaryWriter.Write(HttpClient.Eol);
  }

  private static string ReadLine(Stream stream)
  {
    List<byte> byteList = new List<byte>();
    while (true)
    {
      int num1;
      try
      {
        num1 = stream.ReadByte();
      }
      catch (IOException ex)
      {
        throw new IOException("Terminated Stream");
      }
      if (num1 != -1)
      {
        byte num2 = (byte) num1;
        if ((int) num2 != (int) HttpClient.Eol[1])
          byteList.Add(num2);
        else
          goto label_8;
      }
      else
        break;
    }
    throw new IOException("Unterminated Stream");
label_8:
    return Encoding.ASCII.GetString(byteList.ToArray()).Trim();
  }

  private static void CheckHttpHeader(Stream inputStream)
  {
    if (inputStream == null)
      throw new IOException("Cannot read from server, server probably dropped the connection.");
    string[] strArray = HttpClient.ReadLine(inputStream).Split(' ');
    int result = -1;
    if (strArray.Length <= 0 || !int.TryParse(strArray[1], out result))
      throw new IOException("Bad Status Code, server probably dropped the connection.");
    int num = result;
    if (num / 100 != 2)
      throw new IOException("bad status: {0}" + num.ToString());
  }

  public void Abort()
  {
    Debug.LogWarning((object) "Aborting");
    this._thread.Abort();
    this._thread.Join();
    this.Dispose();
  }

  public void Dispose()
  {
    this._exchangedBytes = (byte[]) null;
    if (this._tcpClient != null)
      this._tcpClient.Close();
    this._tcpClient = (TcpClient) null;
    if (this._requestData != null)
      this._requestData.Dispose();
    this._requestData = (MemoryStream) null;
    this.CanWriteStream = false;
  }
}
