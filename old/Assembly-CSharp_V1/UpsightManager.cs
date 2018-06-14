// Decompiled with JetBrains decompiler
// Type: UpsightManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UpsightMiniJSON;

public class UpsightManager : MonoBehaviour
{
  public const string GameObjectName = "UpsightManager";
  private static bool initialized;
  private bool _destroyed;

  public UpsightManager()
  {
    base.\u002Ector();
  }

  public static event Action sessionDidStartEvent;

  public static event Action sessionDidResumeEvent;

  public static event Action<List<string>> managedVariablesDidSynchronizeEvent;

  public static event Action<string, UpsightContentAttributes> onBillboardAppearEvent;

  public static event Action<string> onBillboardDismissEvent;

  public static event Action<UpsightReward> billboardDidReceiveRewardEvent;

  public static event Action<UpsightPurchase> billboardDidReceivePurchaseEvent;

  public static event Action<UpsightData> billboardDidReceiveDataEvent;

  public static void init()
  {
    if (UpsightManager.initialized)
      return;
    UpsightManager.initialized = true;
    GameObject gameObject = GameObject.Find(nameof (UpsightManager)) ?? new GameObject(nameof (UpsightManager));
    if (Object.op_Equality((Object) (gameObject.GetComponent<UpsightManager>() ?? gameObject.AddComponent<UpsightManager>()), (Object) null))
      Debug.LogError((object) "No UpsightManager Component on GameObject");
    Object.DontDestroyOnLoad((Object) gameObject);
    Upsight.init();
  }

  private void Awake()
  {
    if (this._destroyed)
      return;
    UpsightManager[] objectsOfType = (UpsightManager[]) Object.FindObjectsOfType<UpsightManager>();
    bool flag = false;
    if (objectsOfType.Length > 1)
    {
      foreach (UpsightManager upsightManager in objectsOfType)
      {
        if (((Object) ((Component) upsightManager).get_gameObject()).get_name() == nameof (UpsightManager) && !flag)
          flag = true;
        else if (!upsightManager._destroyed)
        {
          upsightManager._destroyed = true;
          Object.Destroy((Object) upsightManager);
        }
      }
    }
    if (!flag)
      UpsightManager.init();
    else
      UpsightManager.initialized = true;
  }

  private void sessionDidStart()
  {
    if (UpsightManager.sessionDidStartEvent == null)
      return;
    UpsightManager.sessionDidStartEvent();
  }

  private void sessionDidResume()
  {
    if (UpsightManager.sessionDidResumeEvent == null)
      return;
    UpsightManager.sessionDidResumeEvent();
  }

  private void managedVariablesDidSynchronize(string json)
  {
    if (UpsightManager.managedVariablesDidSynchronizeEvent == null)
      return;
    List<string> stringList = (List<string>) null;
    if (json != null && 0 < json.Length)
    {
      List<object> jsonArray = Json.ToJsonArray(json);
      stringList = new List<string>();
      using (List<object>.Enumerator enumerator = jsonArray.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          stringList.Add(current.ToString());
        }
      }
    }
    UpsightManager.managedVariablesDidSynchronizeEvent(stringList);
  }

  private void onBillboardAppear(string json)
  {
    string scope;
    UpsightContentAttributes contentAttributes = UpsightContentAttributes.FromJson(json, out scope);
    if (UpsightManager.onBillboardAppearEvent == null)
      return;
    UpsightManager.onBillboardAppearEvent(scope, contentAttributes);
  }

  private void onBillboardDismiss(string scope)
  {
    if (UpsightManager.onBillboardDismissEvent == null)
      return;
    UpsightManager.onBillboardDismissEvent(scope);
  }

  private void billboardDidReceiveReward(string json)
  {
    if (UpsightManager.billboardDidReceiveRewardEvent == null)
      return;
    UpsightManager.billboardDidReceiveRewardEvent(UpsightReward.rewardFromJson(json));
  }

  private void billboardDidReceivePurchase(string json)
  {
    if (UpsightManager.billboardDidReceivePurchaseEvent == null)
      return;
    UpsightManager.billboardDidReceivePurchaseEvent(UpsightPurchase.purchaseFromJson(json));
  }

  private void billboardDidReceiveData(string json)
  {
    if (UpsightManager.billboardDidReceiveDataEvent == null)
      return;
    UpsightManager.billboardDidReceiveDataEvent(UpsightData.FromJson(json));
  }

  private void OnApplicationPause(bool paused)
  {
    if (paused)
      Upsight.onPause();
    else
      Upsight.onResume();
  }
}
