// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemLimitedShop
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AppealItemLimitedShop : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/limitedshop";
    private readonly string MASTER_PATH = "Data/appeal/AppealLimitedShop";
    [SerializeField]
    private Image AppealChara;
    [SerializeField]
    private RectTransform AppealCharaRect;
    [SerializeField]
    private Image AppealTextL;
    [SerializeField]
    private Image AppealTextR;
    [SerializeField]
    private RectTransform AppealTextRect;
    [SerializeField]
    private Button LimitedShopButton;
    [SerializeField]
    private GameObject LockObject;
    private string mAppealID;
    private float mPosX_Chara;
    private float mPosX_Text;
    private bool IsLoaded;
    private bool IsInitalized;
    private Sprite CharaSprite;
    private Sprite TextLSprite;
    private Sprite TextRSprite;

    protected override void Awake()
    {
      base.Awake();
      if (Object.op_Inequality((Object) this.AppealChara, (Object) null))
        ((Component) this.AppealChara).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AppealTextL, (Object) null))
        ((Component) this.AppealTextL).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AppealTextR, (Object) null))
        ((Component) this.AppealTextR).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AppealCharaRect, (Object) null))
        this.mPosX_Chara = (float) this.AppealCharaRect.get_anchoredPosition().x;
      if (!Object.op_Inequality((Object) this.AppealTextRect, (Object) null))
        return;
      this.mPosX_Text = (float) this.AppealTextRect.get_anchoredPosition().x;
    }

    protected override void Start()
    {
      base.Start();
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResourcess(this.SPRITES_PATH));
      if (!string.IsNullOrEmpty(this.mAppealID) || !Object.op_Inequality((Object) this.LimitedShopButton, (Object) null))
        return;
      ((Selectable) this.LimitedShopButton).set_interactable(false);
      if (!Object.op_Inequality((Object) this.LockObject, (Object) null))
        return;
      this.LockObject.SetActive(true);
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.IsInitalized)
        return;
      this.Refresh();
    }

    protected override void Refresh()
    {
      ((Component) this.AppealChara).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealChara, (Object) null) && Object.op_Inequality((Object) this.CharaSprite, (Object) null));
      ((Component) this.AppealTextL).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealTextL, (Object) null) && Object.op_Inequality((Object) this.TextLSprite, (Object) null));
      ((Component) this.AppealTextR).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealTextR, (Object) null) && Object.op_Inequality((Object) this.TextRSprite, (Object) null));
      this.AppealChara.set_sprite(this.CharaSprite);
      this.AppealTextL.set_sprite(this.TextLSprite);
      this.AppealTextR.set_sprite(this.TextRSprite);
      this.AppealTextRect.set_anchoredPosition(new Vector2(this.mPosX_Text, (float) this.AppealTextRect.get_anchoredPosition().y));
      this.AppealCharaRect.set_anchoredPosition(new Vector2(this.mPosX_Chara, (float) this.AppealCharaRect.get_anchoredPosition().y));
      ((Selectable) this.LimitedShopButton).set_interactable(true);
      if (Object.op_Inequality((Object) this.LockObject, (Object) null))
        this.LockObject.SetActive(false);
      this.IsInitalized = true;
    }

    protected override void Destroy()
    {
      base.Destroy();
    }

    private bool LoadAppealMaster(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_AppealLimitedShopMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealLimitedShopMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        AppealLimitedShopMaster limitedShopMaster1 = new AppealLimitedShopMaster();
        foreach (JSON_AppealLimitedShopMaster json in jsonArray)
        {
          AppealLimitedShopMaster limitedShopMaster2 = new AppealLimitedShopMaster();
          if (limitedShopMaster2.Deserialize(json) && limitedShopMaster2.start_at <= serverTime && limitedShopMaster2.end_at > serverTime)
          {
            if (limitedShopMaster1 == null)
              limitedShopMaster1 = limitedShopMaster2;
            else if (limitedShopMaster1.priority < limitedShopMaster2.priority)
              limitedShopMaster1 = limitedShopMaster2;
          }
        }
        if (limitedShopMaster1 != null)
        {
          this.mAppealID = limitedShopMaster1.appeal_id;
          this.mPosX_Chara = limitedShopMaster1.pos_x_chara;
          this.mPosX_Text = limitedShopMaster1.pos_x_text;
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadAppealResourcess(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemLimitedShop.\u003CLoadAppealResourcess\u003Ec__Iterator99() { path = path, \u003C\u0024\u003Epath = path, \u003C\u003Ef__this = this };
    }
  }
}
