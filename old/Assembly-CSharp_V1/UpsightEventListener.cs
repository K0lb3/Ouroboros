// Decompiled with JetBrains decompiler
// Type: UpsightEventListener
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UpsightEventListener : MonoBehaviour
{
  public UpsightEventListener()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    UpsightManager.sessionDidStartEvent += new Action(this.sessionDidStartEvent);
    UpsightManager.sessionDidResumeEvent += new Action(this.sessionDidResumeEvent);
    UpsightManager.managedVariablesDidSynchronizeEvent += new Action<List<string>>(this.managedVariablesDidSynchronizeEvent);
    UpsightManager.onBillboardAppearEvent += new Action<string, UpsightContentAttributes>(this.onBillboardAppearEvent);
    UpsightManager.onBillboardDismissEvent += new Action<string>(this.onBillboardDismissEvent);
    UpsightManager.billboardDidReceivePurchaseEvent += new Action<UpsightPurchase>(this.billboardDidReceivePurchaseEvent);
    UpsightManager.billboardDidReceiveRewardEvent += new Action<UpsightReward>(this.billboardDidReceiveRewardEvent);
    UpsightManager.billboardDidReceiveDataEvent += new Action<UpsightData>(this.billboardDidReceiveDataEvent);
  }

  private void OnDisable()
  {
    UpsightManager.sessionDidStartEvent -= new Action(this.sessionDidStartEvent);
    UpsightManager.sessionDidResumeEvent -= new Action(this.sessionDidResumeEvent);
    UpsightManager.managedVariablesDidSynchronizeEvent -= new Action<List<string>>(this.managedVariablesDidSynchronizeEvent);
    UpsightManager.onBillboardAppearEvent -= new Action<string, UpsightContentAttributes>(this.onBillboardAppearEvent);
    UpsightManager.onBillboardDismissEvent -= new Action<string>(this.onBillboardDismissEvent);
    UpsightManager.billboardDidReceivePurchaseEvent -= new Action<UpsightPurchase>(this.billboardDidReceivePurchaseEvent);
    UpsightManager.billboardDidReceiveRewardEvent -= new Action<UpsightReward>(this.billboardDidReceiveRewardEvent);
    UpsightManager.billboardDidReceiveDataEvent -= new Action<UpsightData>(this.billboardDidReceiveDataEvent);
  }

  private void sessionDidStartEvent()
  {
    Debug.Log((object) nameof (sessionDidStartEvent));
  }

  private void sessionDidResumeEvent()
  {
    Debug.Log((object) nameof (sessionDidResumeEvent));
  }

  private void managedVariablesDidSynchronizeEvent(List<string> tags)
  {
    if (tags == null)
    {
      Debug.Log((object) "managedVariablesDidSynchronizeEvent: received null tags list");
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      using (List<string>.Enumerator enumerator = tags.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          stringBuilder.Append("\n").Append(current);
        }
      }
      Debug.Log((object) ("managedVariablesDidSynchronizeEvent:" + stringBuilder.ToString()));
    }
  }

  private void onBillboardAppearEvent(string scope, UpsightContentAttributes content)
  {
    Debug.Log((object) ("onBillboardAppearEvent: scope=" + scope + ", content=" + (object) content));
  }

  private void onBillboardDismissEvent(string scope)
  {
    Debug.Log((object) ("billboardDidDismissEvent: " + scope));
  }

  private void billboardDidReceivePurchaseEvent(UpsightPurchase purchase)
  {
    Debug.Log((object) ("billboardDidReceivePurchaseEvent: " + (object) purchase));
  }

  private void billboardDidReceiveRewardEvent(UpsightReward reward)
  {
    Debug.Log((object) ("billboardDidReceiveRewardEvent: " + (object) reward));
  }

  private void billboardDidReceiveDataEvent(UpsightData data)
  {
    Debug.Log((object) ("billboardDidReceiveDataEvent: " + (object) data));
  }
}
