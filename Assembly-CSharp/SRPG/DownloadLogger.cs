// Decompiled with JetBrains decompiler
// Type: SRPG.DownloadLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;
using UnityEngine.Experimental.Networking;

namespace SRPG
{
  public class DownloadLogger : DownloadHandlerScript
  {
    private VersusAudienceManager mManager;
    private string mParam;

    public DownloadLogger()
    {
      base.\u002Ector();
    }

    public VersusAudienceManager Manager
    {
      set
      {
        this.mManager = value;
      }
    }

    public string Response
    {
      get
      {
        return this.mParam;
      }
    }

    protected virtual bool ReceiveData(byte[] data, int dataLength)
    {
      if (data == null || data.Length < 1)
      {
        Debug.Log((object) "LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
        return false;
      }
      string str1 = Encoding.UTF8.GetString(data).Replace("\r\n", string.Empty).Replace("\x0005", string.Empty);
      string[] strArray = str1.Split('\n');
      if (this.mManager != null)
      {
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!string.IsNullOrEmpty(strArray[index]))
          {
            string str2 = strArray[index];
            char[] chArray = new char[1]{ '\r' };
            foreach (string data1 in str2.Split(chArray))
              this.mManager.Add(data1);
          }
        }
      }
      this.mParam = str1;
      return true;
    }

    protected virtual void CompleteContent()
    {
      if (this.mManager != null)
        this.mManager.Disconnect();
      Debug.Log((object) this.mParam);
    }
  }
}
