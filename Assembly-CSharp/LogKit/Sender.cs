// Decompiled with JetBrains decompiler
// Type: LogKit.Sender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading;

namespace LogKit
{
  public class Sender
  {
    private readonly LinkedList<Guid> queue = new LinkedList<Guid>();
    private readonly string key;

    public Sender(string key)
    {
      this.key = key;
      this.Init();
    }

    private void Init()
    {
      string[] files = Directory.GetFiles(Logger.GetLogFilePath(this.key, (string) null));
      if (files.Length > 100)
      {
        new Thread(new ParameterizedThreadStart(Sender.FileDeleteThread)).Start((object) files);
      }
      else
      {
        for (int index = 0; index < files.Length; ++index)
          this.queue.AddLast(new Guid(Path.GetFileName(files[index])));
      }
    }

    private static void FileDeleteThread(object data)
    {
      foreach (string path in (string[]) data)
        System.IO.File.Delete(path);
    }

    public void Push(Guid logId)
    {
      lock (this.queue)
        this.queue.AddLast(logId);
    }

    private void Pop(Guid logId)
    {
      lock (this.queue)
      {
        this.queue.Remove(logId);
        try
        {
          System.IO.File.Delete(Logger.GetLogFilePath(this.key, logId));
        }
        catch (Exception ex)
        {
        }
      }
    }

    public void Emit()
    {
      if (!Logger.IsSetServerUrl)
        return;
      if (this.queue.Count == 0)
        return;
      try
      {
        if (string.IsNullOrEmpty(Logger.LogCollectionUrl))
        {
          List<Guid> guidList = new List<Guid>();
          using (LinkedList<Guid>.Enumerator enumerator = this.queue.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Guid current = enumerator.Current;
              guidList.Add(current);
            }
          }
          using (List<Guid>.Enumerator enumerator = guidList.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.Pop(enumerator.Current);
          }
        }
        else
        {
          ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true);
          Guid logId = this.queue.First.Value;
          byte[] buffer = System.IO.File.ReadAllBytes(Logger.GetLogFilePath(this.key, logId));
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Logger.LogCollectionUrl);
          httpWebRequest.Method = "POST";
          httpWebRequest.ContentType = "application/json";
          httpWebRequest.ContentLength = (long) buffer.Length;
          using (Stream requestStream = httpWebRequest.GetRequestStream())
          {
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();
          }
          HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
          int statusCode = (int) response.StatusCode;
          response.Close();
          if (200 > statusCode || statusCode > 299)
            return;
          this.Pop(logId);
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
