// Decompiled with JetBrains decompiler
// Type: NetworkError
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
