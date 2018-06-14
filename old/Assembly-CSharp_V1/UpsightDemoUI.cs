// Decompiled with JetBrains decompiler
// Type: UpsightDemoUI
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpsightDemoUI : MonoBehaviour
{
  [HideInInspector]
  public string _customScope;
  public Image BackgroundPanel;
  public Image OfferImage;
  public Image OrnamentImage;
  public Text ButtonText;
  private bool _shouldSynchronizeManagedVariables;
  private string _isContentReady;
  private bool _showingNativeMessage;
  private Texture2D _texture;
  private UpsightData _upsightData;

  public UpsightDemoUI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    Upsight.init();
    this.StartCoroutine(this._contentCheck());
  }

  [DebuggerHidden]
  public IEnumerator _contentCheck()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UpsightDemoUI.\u003C_contentCheck\u003Ec__IteratorF8() { \u003C\u003Ef__this = this };
  }

  private void OnEnable()
  {
    UpsightManager.onBillboardAppearEvent += new Action<string, UpsightContentAttributes>(this.onBillboardAppearEvent);
    UpsightManager.onBillboardDismissEvent += new Action<string>(this.onBillboardDismissEvent);
    UpsightManager.billboardDidReceiveDataEvent += new Action<UpsightData>(this.billboardDidReceiveDataEvent);
  }

  private void OnDisable()
  {
    UpsightManager.onBillboardAppearEvent -= new Action<string, UpsightContentAttributes>(this.onBillboardAppearEvent);
    UpsightManager.onBillboardDismissEvent -= new Action<string>(this.onBillboardDismissEvent);
    UpsightManager.billboardDidReceiveDataEvent -= new Action<UpsightData>(this.billboardDidReceiveDataEvent);
  }

  private void OnGUI()
  {
    if (this._showingNativeMessage)
      return;
    this.beginGuiColomn();
    if (GUILayout.Button("Enable Verbose Logs", new GUILayoutOption[0]))
      Upsight.setLoggerLevel(UpsightLoggerLevel.Verbose);
    if (GUILayout.Button("Log Config Data", new GUILayoutOption[0]))
    {
      Debug.Log((object) ("plugin version: " + Upsight.getPluginVersion()));
      Debug.Log((object) ("app token: " + Upsight.getAppToken()));
      Debug.Log((object) ("public key: " + Upsight.getPublicKey()));
      Debug.Log((object) ("sender ID: " + Upsight.getSid()));
    }
    GUILayout.Space(20f);
    if (GUILayout.Button("Set User Attributes", new GUILayoutOption[0]))
    {
      Upsight.setUserAttributeString("name", "mary");
      Upsight.setUserAttributeInt("age", 14);
      Upsight.setUserAttributeBool("isAlive", true);
      Upsight.setUserAttributeFloat("distance", 26.7f);
    }
    if (GUILayout.Button("Get User Attributes", new GUILayoutOption[0]))
    {
      Debug.Log((object) ("name: " + Upsight.getUserAttributeString("name")));
      Debug.Log((object) ("age: " + (object) Upsight.getUserAttributeInt("age")));
      Debug.Log((object) ("isAlive: " + (object) Upsight.getUserAttributeBool("isAlive")));
      Debug.Log((object) ("distance: " + (object) Upsight.getUserAttributeFloat("distance")));
    }
    if (GUILayout.Button("Set Opt-Out Status", new GUILayoutOption[0]))
      Upsight.setOptOutStatus(true);
    if (GUILayout.Button("Get Opt-Out Status", new GUILayoutOption[0]))
      Debug.Log((object) ("opt-out status: " + (object) Upsight.getOptOutStatus()));
    if (GUILayout.Button("Set Location", new GUILayoutOption[0]))
      Upsight.setLocation(51.792, 4.6307);
    GUILayout.Space(20f);
    if (GUILayout.Button("Report Custom Event", new GUILayoutOption[0]))
      Upsight.recordCustomEvent("my-event", new Dictionary<string, object>()
      {
        {
          "first_key",
          (object) "first_value"
        },
        {
          "second_key",
          (object) 38
        }
      });
    if (GUILayout.Button("Record IAP Event", new GUILayoutOption[0]))
    {
      Upsight.recordGooglePlayPurchase(2, "USD", 1.99, 1.99, "com.upsight.product1", 0, "{\"orderId\":\"12999763169054705758.1371079406387615\",\"packageName\":\"com.example.app\",\"productId\":\"exampleSku\",\"purchaseTime\":1345678900000,\"purchaseState\":0,\"developerPayload\":\"bGoa+V7g/yqDXvKRqq+JTFn4uQZbPiQJo4pf9RzJ\",\"purchaseToken\":\"opaque-token-up-to-1000-characters\"}", "mockSignature", (Dictionary<string, object>) null);
      Upsight.recordAppleStorePurchase(2, "USD", 1.99, "com.upsight.product1", "some product", UpsightPurchaseResolution.Buy, (Dictionary<string, object>) null);
    }
    if (GUILayout.Button("Record Monetization Event", new GUILayoutOption[0]))
      Upsight.recordMonetizationEvent(2.99, "USD", UpsightPurchaseResolution.Buy, "product-monetization", -1.0, -1, (Dictionary<string, object>) null);
    bool flag = GUILayout.Toggle(this._shouldSynchronizeManagedVariables, "Should sync UXM values", new GUILayoutOption[0]);
    if (flag != this._shouldSynchronizeManagedVariables)
    {
      this._shouldSynchronizeManagedVariables = flag;
      Upsight.setShouldSynchronizeManagedVariables(this._shouldSynchronizeManagedVariables);
    }
    string str1 = Upsight.getManagedBool("enable_extended_mode").ToString();
    string managedString = Upsight.getManagedString("name");
    string str2 = Upsight.getManagedInt("coins").ToString();
    string str3 = Upsight.getManagedFloat("strength").ToString();
    GUILayout.Label("enable_extended_mode: " + str1, new GUILayoutOption[0]);
    GUILayout.Label("name: " + managedString, new GUILayoutOption[0]);
    GUILayout.Label("coins: " + str2, new GUILayoutOption[0]);
    GUILayout.Label("strength: " + str3, new GUILayoutOption[0]);
    this.endGuiColumn(true);
    if (GUILayout.Button("Register for Push Notifications", new GUILayoutOption[0]))
      Upsight.registerForPushNotifications();
    if (GUILayout.Button("Unregister for Push Notifications", new GUILayoutOption[0]))
      Upsight.unregisterForPushNotifications();
    GUILayout.Space(20f);
    if (GUILayout.Button("Prepare Billboard (rewcl)", new GUILayoutOption[0]))
      Upsight.prepareBillboard("rewcl");
    if (GUILayout.Button("Record Milestone (rewcl)", new GUILayoutOption[0]))
      Upsight.recordMilestoneEvent("rewcl", (Dictionary<string, object>) null);
    if (GUILayout.Button("Destroy Billboard (rewcl)", new GUILayoutOption[0]))
      Upsight.destroyBillboard("rewcl");
    GUILayout.Space(20f);
    if (GUILayout.Button("Prepare Billboard (vgpcl)", new GUILayoutOption[0]))
      Upsight.prepareBillboard("vgpcl");
    if (GUILayout.Button("Record Milestone (vgpcl)", new GUILayoutOption[0]))
      Upsight.recordMilestoneEvent("vgpcl", (Dictionary<string, object>) null);
    if (GUILayout.Button("Destroy Billboard (vgpcl)", new GUILayoutOption[0]))
      Upsight.destroyBillboard("vgpcl");
    GUILayout.Space(20f);
    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
    GUILayout.Label("Custom Scope", new GUILayoutOption[1]
    {
      GUILayout.Width((float) (Screen.get_width() / 4))
    });
    this._customScope = GUILayout.TextField(this._customScope, new GUILayoutOption[1]
    {
      GUILayout.Width((float) (Screen.get_width() / 4))
    });
    GUILayout.EndHorizontal();
    if (GUILayout.Button("Prepare Billboard (" + this._customScope + ")", new GUILayoutOption[0]))
      Upsight.prepareBillboard(this._customScope);
    if (GUILayout.Button("Record Milestone (" + this._customScope + ")", new GUILayoutOption[0]))
      Upsight.recordMilestoneEvent(this._customScope, (Dictionary<string, object>) null);
    if (GUILayout.Button("Destroy Billboard (" + this._customScope + ")", new GUILayoutOption[0]))
      Upsight.destroyBillboard(this._customScope);
    GUILayout.Label(this._isContentReady, new GUILayoutOption[0]);
    this.endGuiColumn(false);
  }

  private void onBillboardAppearEvent(string scope, UpsightContentAttributes content)
  {
  }

  private void onBillboardDismissEvent(string scope)
  {
  }

  private void billboardDidReceiveDataEvent(UpsightData data)
  {
    this._upsightData = data;
    ((Graphic) this.BackgroundPanel).set_color(data.GetColor("BGColor"));
    this.ButtonText.set_text(data.GetString("BodyCopy"));
    Texture2D texture2D1 = new Texture2D(1, 1);
    texture2D1.LoadImage(File.ReadAllBytes(data.GetImage("OfferImage").ImagePath));
    this.OfferImage.set_sprite(Sprite.Create(texture2D1, new Rect(0.0f, 0.0f, (float) ((Texture) texture2D1).get_width(), (float) ((Texture) texture2D1).get_height()), Vector2.get_zero()));
    Texture2D texture2D2 = new Texture2D(1, 1);
    texture2D2.LoadImage(File.ReadAllBytes(data.GetImage("OrnamentImage").ImagePath));
    this.OrnamentImage.set_sprite(Sprite.Create(texture2D2, new Rect(0.0f, 0.0f, (float) ((Texture) texture2D2).get_width(), (float) ((Texture) texture2D2).get_height()), Vector2.get_zero()));
    this._showingNativeMessage = true;
    ((Component) this.BackgroundPanel).get_gameObject().SetActive(true);
  }

  public void nativeMessageDismissed()
  {
    ((Component) this.BackgroundPanel).get_gameObject().SetActive(false);
    this._showingNativeMessage = false;
    this._upsightData.RecordClickEvent();
    this._upsightData.RecordDismissEvent();
    this._upsightData.Destroy();
    this._upsightData = (UpsightData) null;
  }

  private void beginGuiColomn()
  {
    int num = Screen.get_width() >= 960 || Screen.get_height() >= 960 ? 70 : 30;
    GUI.get_skin().get_label().set_margin(new RectOffset(0, 0, 10, 0));
    GUI.get_skin().get_label().set_alignment((TextAnchor) 4);
    GUI.get_skin().get_button().set_margin(new RectOffset(0, 0, 10, 0));
    GUI.get_skin().get_button().set_fixedHeight((float) num);
    GUI.get_skin().get_button().set_fixedWidth((float) (Screen.get_width() / 2 - 20));
    GUI.get_skin().get_button().set_wordWrap(true);
    GUI.get_skin().get_button().set_fontSize(num / 3);
    GUI.get_skin().get_toggle().set_fixedHeight((float) num);
    GUI.get_skin().get_toggle().set_fixedWidth((float) (Screen.get_width() / 2 - 20));
    GUI.get_skin().get_toggle().set_fontSize(num / 3);
    GUI.get_skin().get_toggle().set_alignment((TextAnchor) 3);
    GUI.get_skin().get_textField().set_fontSize(num / 2);
    GUI.get_skin().get_textField().set_fixedWidth((float) (Screen.get_width() / 2 - 20));
    GUI.get_skin().get_label().set_fontSize(num / 3);
    GUI.get_skin().get_label().set_alignment((TextAnchor) 3);
    GUI.get_skin().get_label().set_margin(new RectOffset(5, 5, 0, 0));
    GUILayout.BeginArea(new Rect(10f, 10f, (float) (Screen.get_width() / 2), (float) Screen.get_height()));
    GUILayout.BeginVertical(new GUILayoutOption[0]);
  }

  private void endGuiColumn(bool hasSecondColumn = false)
  {
    GUILayout.EndVertical();
    GUILayout.EndArea();
    if (!hasSecondColumn)
      return;
    GUILayout.BeginArea(new Rect((float) (Screen.get_width() - Screen.get_width() / 2), 10f, (float) (Screen.get_width() / 2), (float) Screen.get_height()));
    GUILayout.BeginVertical(new GUILayoutOption[0]);
  }
}
