// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemLimitedShop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    private bool mIsInitialized;
    private Sprite CharaSprite;
    private Sprite TextLSprite;
    private Sprite TextRSprite;

    public bool IsInitialized
    {
      get
      {
        return this.mIsInitialized;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealChara, (UnityEngine.Object) null))
        ((Component) this.AppealChara).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextL, (UnityEngine.Object) null))
        ((Component) this.AppealTextL).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextR, (UnityEngine.Object) null))
        ((Component) this.AppealTextR).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealCharaRect, (UnityEngine.Object) null))
        this.mPosX_Chara = (float) this.AppealCharaRect.get_anchoredPosition().x;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextRect, (UnityEngine.Object) null))
        return;
      this.mPosX_Text = (float) this.AppealTextRect.get_anchoredPosition().x;
    }

    protected override void Start()
    {
      base.Start();
      if (this.LoadAppealMaster(this.MASTER_PATH))
        this.StartCoroutine(this.LoadAppealResourcess(this.SPRITES_PATH));
      MonoSingleton<GameManager>.Instance.IsLimitedShopOpen = true;
      if (!string.IsNullOrEmpty(this.mAppealID) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitedShopButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.LimitedShopButton).set_interactable(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockObject, (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.IsLimitedShopOpen = false;
      this.LockObject.SetActive(true);
    }

    protected override void Update()
    {
      if (!this.IsLoaded || this.mIsInitialized)
        return;
      this.Refresh();
    }

    protected override void Refresh()
    {
      ((Component) this.AppealChara).get_gameObject().SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealChara, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharaSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextL).get_gameObject().SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextL, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextLSprite, (UnityEngine.Object) null));
      ((Component) this.AppealTextR).get_gameObject().SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealTextR, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextRSprite, (UnityEngine.Object) null));
      this.AppealChara.set_sprite(this.CharaSprite);
      this.AppealTextL.set_sprite(this.TextLSprite);
      this.AppealTextR.set_sprite(this.TextRSprite);
      this.AppealTextRect.set_anchoredPosition(new Vector2(this.mPosX_Text, (float) this.AppealTextRect.get_anchoredPosition().y));
      this.AppealCharaRect.set_anchoredPosition(new Vector2(this.mPosX_Chara, (float) this.AppealCharaRect.get_anchoredPosition().y));
      ((Selectable) this.LimitedShopButton).set_interactable(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockObject, (UnityEngine.Object) null))
        this.LockObject.SetActive(false);
      this.mIsInitialized = true;
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
      return (IEnumerator) new AppealItemLimitedShop.\u003CLoadAppealResourcess\u003Ec__IteratorDC() { path = path, \u003C\u0024\u003Epath = path, \u003C\u003Ef__this = this };
    }
  }
}
