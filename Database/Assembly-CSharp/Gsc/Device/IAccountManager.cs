// Decompiled with JetBrains decompiler
// Type: Gsc.Device.IAccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Device
{
  public interface IAccountManager
  {
    string GetSecretKey(string name);

    string GetDeviceId(string name);

    void SetKeyPair(string name, string secretKey, string deviceId);

    void SetDeviceId(string name, string deviceId);

    void Remove(string name);

    void Reset();
  }
}
