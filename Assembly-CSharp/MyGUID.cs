// Decompiled with JetBrains decompiler
// Type: MyGUID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class MyGUID
{
  private static readonly string KeyChainName = "gu3";
  private static readonly string KeyChainNameOld = "gu3u3";
  private string device_ID;
  private bool mSetup;

  public string udid { get; private set; }

  public string secret_key { get; private set; }

  public string device_id { get; private set; }

  private byte[] Encrypt(int seed, string src)
  {
    if (string.IsNullOrEmpty(src))
      return (byte[]) null;
    byte[] bytes = Encoding.UTF8.GetBytes(src);
    GUtility.Encrypt(bytes, bytes.Length);
    return bytes;
  }

  private string Decrypt(int seed, byte[] src)
  {
    if (src == null)
      return (string) null;
    GUtility.Decrypt(src, src.Length);
    return Encoding.UTF8.GetString(src);
  }

  private string GetPersistFileNameOld()
  {
    return Application.get_persistentDataPath() + "/gu3u3.dat";
  }

  private string GetCacheFileNameOld()
  {
    return Application.get_temporaryCachePath() + "/gu3i3.dat";
  }

  private byte[] ReadPersistOld()
  {
    return MyGUIDPlugin.ReadKeyChain(MyGUID.KeyChainNameOld);
  }

  private byte[] ReadCacheOld()
  {
    string cacheFileNameOld = this.GetCacheFileNameOld();
    if (!File.Exists(cacheFileNameOld))
      return (byte[]) null;
    byte[] numArray = (byte[]) null;
    try
    {
      numArray = File.ReadAllBytes(cacheFileNameOld);
    }
    catch
    {
    }
    return numArray;
  }

  private byte[] ConvertOldCache(int seed)
  {
    byte[] data = this.ReadCacheOld();
    if (data == null || data.Length <= 0)
      return (byte[]) null;
    string src = MyEncrypt.Decrypt(seed, data, false);
    DebugUtility.LogWarning("[MyGUID]ConvertCacheData:" + src);
    byte[] numArray = this.Encrypt(seed, src);
    this.SaveCache(numArray);
    return numArray;
  }

  private byte[] ConvertOldPersist(int seed)
  {
    byte[] data = this.ReadPersistOld();
    if (data == null || data.Length <= 0)
      return (byte[]) null;
    string src = MyEncrypt.Decrypt(seed, data, false);
    DebugUtility.LogWarning("[MyGUID]ConvertPersistData:" + src);
    byte[] numArray = this.Encrypt(seed, src);
    this.SavePersist(numArray);
    return numArray;
  }

  private string GetPersistFileName()
  {
    string str = Application.get_persistentDataPath() + "/gu3p.dat";
    DebugUtility.Log(">>>>>>>>>>>>>>>>>>>> GetPersistFileName path:" + str);
    return str;
  }

  private string GetCacheFileName()
  {
    return Application.get_persistentDataPath() + "/gu3c.dat";
  }

  private bool SavePersist(byte[] value)
  {
    if (value == null)
      return false;
    return MyGUIDPlugin.WriteKeyChain(MyGUID.KeyChainName, value);
  }

  private byte[] ReadPersist()
  {
    return MyGUIDPlugin.ReadKeyChain(MyGUID.KeyChainName);
  }

  private bool SaveCache(byte[] value)
  {
    if (value == null)
      return false;
    string cacheFileName = this.GetCacheFileName();
    bool flag = true;
    try
    {
      File.WriteAllBytes(cacheFileName, value);
    }
    catch
    {
      flag = false;
    }
    return flag;
  }

  public bool ResetCache()
  {
    string cacheFileName = this.GetCacheFileName();
    bool flag = true;
    try
    {
      File.Delete(cacheFileName);
      string str = (string) null;
      this.secret_key = str;
      this.device_id = str;
      this.mSetup = false;
    }
    catch
    {
      flag = false;
    }
    return flag;
  }

  private byte[] ReadCache()
  {
    string cacheFileName = this.GetCacheFileName();
    if (!File.Exists(cacheFileName))
      return (byte[]) null;
    byte[] numArray = (byte[]) null;
    try
    {
      numArray = File.ReadAllBytes(cacheFileName);
    }
    catch
    {
    }
    return numArray;
  }

  public bool Init(int seed, string device_ID)
  {
    if (this.mSetup)
      return true;
    string str1 = (string) null;
    this.device_id = str1;
    string str2 = str1;
    this.udid = str2;
    this.secret_key = str2;
    byte[] src1 = this.ReadPersist();
    byte[] src2 = this.ReadCache();
    if (src1 == null && src2 == null)
    {
      src1 = this.ConvertOldPersist(seed);
      src2 = this.ConvertOldCache(seed);
    }
    if (src2 != null && src2.Length > 0)
    {
      string[] strArray = this.Decrypt(seed, src2).Split(' ');
      this.device_id = strArray.Length < 1 ? (string) null : strArray[0];
      this.secret_key = strArray.Length < 2 ? (string) null : strArray[1];
    }
    if (src1 != null && src1.Length > 0)
      this.udid = this.Decrypt(seed, src1);
    if (this.device_id == null)
    {
      this.secret_key = Guid.NewGuid().ToString();
      DebugUtility.Log("MY GUID : Init");
      this.udid = this.udid == null ? device_ID : this.udid;
    }
    seed = 0;
    this.mSetup = true;
    return this.mSetup;
  }

  public bool SaveAuth(int seed, string secretKey, string deviceId, string udId)
  {
    bool flag1 = true;
    DebugUtility.Log(nameof (SaveAuth));
    this.device_id = deviceId;
    this.secret_key = secretKey;
    string src = deviceId + " " + secretKey;
    byte[] numArray1 = this.Encrypt(seed, src);
    bool flag2 = flag1 & this.SaveCache(numArray1);
    if (!File.Exists(this.GetPersistFileName()))
    {
      byte[] numArray2 = this.Encrypt(seed, udId);
      flag2 &= this.SavePersist(numArray2);
    }
    seed = 0;
    return flag2;
  }
}
