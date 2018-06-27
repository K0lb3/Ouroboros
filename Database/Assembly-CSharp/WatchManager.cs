// Decompiled with JetBrains decompiler
// Type: WatchManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System.Collections.Generic;
using UnityEngine;

public class WatchManager : MonoSingleton<WatchManager>
{
  public bool IsAvailable { get; private set; }

  public List<int> GetMealHours()
  {
    return new List<int>();
  }

  public void Clear()
  {
  }

  private static bool connectToWatch()
  {
    return false;
  }

  private static void sendDataToWatch(string eventName)
  {
  }

  private static void updateTimer(long left, long total)
  {
  }

  public void Init()
  {
    this.IsAvailable = false;
    Debug.Log((object) "##### connecting watch");
    if (!WatchManager.connectToWatch())
      return;
    Debug.Log((object) "##### connected watch");
    this.IsAvailable = true;
  }

  protected override void Initialize()
  {
    Object.DontDestroyOnLoad((Object) this);
    this.Init();
  }

  protected override void Release()
  {
  }

  public void OnEatMeal(string message)
  {
  }

  public void OnPullGacha(string message)
  {
    Network.RequestAPIImmediate((WebAPI) new ReqGachaExec("Normal_Gacha", new Network.ResponseCallback(this.ResponseCallback), 0, 1), false);
  }

  public void ResponseCallback(WWWResult www)
  {
    Network.RemoveAPI();
    if (Network.IsError)
      return;
    WebAPI.JSON_BodyResponse<Json_GachaResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(www.text);
    string eventName = string.Empty;
    for (int index = 0; index < jsonObject.body.add.Length; ++index)
      eventName = eventName + jsonObject.body.add[index].iname + ",";
    WatchManager.sendDataToWatch(eventName);
  }

  public void UpdateGachaTimer(long left, long total)
  {
    WatchManager.updateTimer(left, total);
  }
}
