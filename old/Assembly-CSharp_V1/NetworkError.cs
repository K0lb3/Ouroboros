// Decompiled with JetBrains decompiler
// Type: NetworkError
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

public class NetworkError : MonoSingleton<NetworkError>
{
  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("NetworkErrorHandler"), Vector3.get_zero(), Quaternion.get_identity()));
    Debug.Log((object) "NetworkError Initialized");
  }

  protected override void Release()
  {
  }
}
