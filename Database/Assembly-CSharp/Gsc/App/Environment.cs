// Decompiled with JetBrains decompiler
// Type: Gsc.App.Environment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System.Collections.Generic;

namespace Gsc.App
{
  public struct Environment : Configuration.IEnvironment
  {
    private const string NATIVEBASE_URL = "https://production-alchemist.nativebase.gu3.jp";
    private const string AUTH_API_PREFIX = "/gauth";
    private const string PURCHASE_API_PREFIX = "/charge";
    private const string BUNDLE_PURCHASE_API_PREFIX = "/bundle";

    public string ServerUrl { get; set; }

    public string NativeBaseUrl { get; set; }

    public string LogCollectionUrl { get; set; }

    public string ClientErrorApi { get; set; }

    public string AuthApiPrefix { get; set; }

    public string PurchaseApiPrefix { get; set; }

    public string BundlePurchaseApiPrefix { get; set; }

    public string DLHost { get; set; }

    public string SiteHost { get; set; }

    public string NewsHost { get; set; }

    public string Assets { get; set; }

    public string AssetsEx { get; set; }

    public string Digest { get; set; }

    public string Pub { get; set; }

    public string PubU { get; set; }

    public Network.EErrCode Stat { get; set; }

    public string StatMsg { get; set; }

    public string StatCode { get; set; }

    public long ServerTime { get; set; }

    public void SetValue(string key, string value)
    {
      this.ClientErrorApi = (string) null;
      this.AuthApiPrefix = "/gauth";
      this.PurchaseApiPrefix = "/charge";
      this.BundlePurchaseApiPrefix = "/bundle";
      string key1 = key;
      if (key1 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (Environment.\u003C\u003Ef__switch\u0024map16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Environment.\u003C\u003Ef__switch\u0024map16 = new Dictionary<string, int>(15)
        {
          {
            "stat",
            0
          },
          {
            "stat_msg",
            1
          },
          {
            "stat_code",
            2
          },
          {
            "time",
            3
          },
          {
            "host_ap",
            4
          },
          {
            "nativebase_url",
            5
          },
          {
            "logcollection_url",
            6
          },
          {
            "host_dl",
            7
          },
          {
            "host_site",
            8
          },
          {
            "host_news",
            9
          },
          {
            "assets",
            10
          },
          {
            "assets_ex",
            11
          },
          {
            "digest",
            12
          },
          {
            "pub",
            13
          },
          {
            "pub_u",
            14
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!Environment.\u003C\u003Ef__switch\u0024map16.TryGetValue(key1, out num))
        return;
      switch (num)
      {
        case 0:
          this.Stat = (Network.EErrCode) int.Parse(value);
          break;
        case 1:
          this.StatMsg = value;
          break;
        case 2:
          this.StatCode = value;
          break;
        case 3:
          this.ServerTime = long.Parse(value);
          break;
        case 4:
          this.ServerUrl = Environment.EndsSlashDelete(value);
          break;
        case 5:
          this.NativeBaseUrl = Environment.EndsSlashDelete(value);
          break;
        case 6:
          this.LogCollectionUrl = Environment.EndsSlashDelete(value);
          break;
        case 7:
          this.DLHost = value;
          break;
        case 8:
          this.SiteHost = value;
          break;
        case 9:
          this.NewsHost = value;
          break;
        case 10:
          this.Assets = value;
          break;
        case 11:
          this.AssetsEx = value;
          break;
        case 12:
          this.Digest = value;
          break;
        case 13:
          this.Pub = value;
          break;
        case 14:
          this.PubU = value;
          break;
      }
    }

    private static string EndsSlashDelete(string value)
    {
      if (value.EndsWith("/"))
        value = value.Substring(0, value.Length - 1);
      return value;
    }

    public static Configuration.Builder<Environment> SetEnvironment(Configuration.Builder<Environment> builder)
    {
      string url = Environment.EndsSlashDelete(Network.GetDefaultHostConfigured()) + "/chkver2";
      return builder.SetEnvironment(url);
    }
  }
}
