// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemEventShop
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
  public class AppealItemEventShop : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/eventshop";
    private readonly string MASTER_PATH = "Data/appeal/AppealEventShop";
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
    private SRPG_Button EventShopButton;
    [SerializeField]
    private GameObject LockObject;
    private string mAppealID;
    private float mPosX_Chara;
    private float mPosX_Text;
    private bool IsLoaded;
    private bool IsInitalize;
    private Sprite CharaSprite;
    private Sprite TextLSprite;
    private Sprite TextRSprite;

    protected override void Awake()
    {
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
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResources(this.SPRITES_PATH));
      if (string.IsNullOrEmpty(this.mAppealID) && Object.op_Inequality((Object) this.EventShopButton, (Object) null))
      {
        ((Selectable) this.EventShopButton).set_interactable(false);
        if (Object.op_Inequality((Object) this.LockObject, (Object) null))
          this.LockObject.SetActive(true);
      }
      GlobalVars.IsEventShopOpen.Set(!string.IsNullOrEmpty(this.mAppealID));
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.IsInitalize)
        return;
      this.Refresh();
    }

    protected override void Destroy()
    {
      base.Destroy();
    }

    protected override void Refresh()
    {
      ((Component) this.AppealChara).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealChara, (Object) null) && Object.op_Inequality((Object) this.CharaSprite, (Object) null));
      ((Component) this.AppealTextL).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealTextL, (Object) null) && Object.op_Inequality((Object) this.TextLSprite, (Object) null));
      ((Component) this.AppealTextR).get_gameObject().SetActive(Object.op_Inequality((Object) this.AppealTextR, (Object) null) && Object.op_Inequality((Object) this.TextRSprite, (Object) null));
      this.AppealChara.set_sprite(this.CharaSprite);
      this.AppealTextL.set_sprite(this.TextLSprite);
      this.AppealTextR.set_sprite(this.TextRSprite);
      this.AppealCharaRect.set_anchoredPosition(new Vector2(this.mPosX_Chara, (float) this.AppealCharaRect.get_anchoredPosition().y));
      this.AppealTextRect.set_anchoredPosition(new Vector2(this.mPosX_Text, (float) this.AppealTextRect.get_anchoredPosition().y));
      ((Selectable) this.EventShopButton).set_interactable(true);
      if (Object.op_Inequality((Object) this.LockObject, (Object) null))
        this.LockObject.SetActive(false);
      this.IsInitalize = true;
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
        JSON_AppealEventShopMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealEventShopMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        AppealEventShopMaster appealEventShopMaster1 = new AppealEventShopMaster();
        foreach (JSON_AppealEventShopMaster json in jsonArray)
        {
          AppealEventShopMaster appealEventShopMaster2 = new AppealEventShopMaster();
          if (appealEventShopMaster2.Deserialize(json) && appealEventShopMaster2.start_at <= serverTime && appealEventShopMaster2.end_at > serverTime)
          {
            if (appealEventShopMaster1 == null)
              appealEventShopMaster1 = appealEventShopMaster2;
            else if (appealEventShopMaster1.priority < appealEventShopMaster2.priority)
              appealEventShopMaster1 = appealEventShopMaster2;
          }
        }
        if (appealEventShopMaster1 != null)
        {
          this.mAppealID = appealEventShopMaster1.appeal_id;
          this.mPosX_Chara = appealEventShopMaster1.position_chara;
          this.mPosX_Text = appealEventShopMaster1.position_text;
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
    private IEnumerator LoadAppealResources(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemEventShop.\u003CLoadAppealResources\u003Ec__Iterator97() { path = path, \u003C\u0024\u003Epath = path, \u003C\u003Ef__this = this };
    }
  }
}
