// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemQuest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AppealItemQuest : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/quest";
    private readonly string MASTER_PATH = "Data/appeal/AppealQuest";
    protected Dictionary<string, Sprite> mCacheAppealSprites = new Dictionary<string, Sprite>();
    private readonly float WAIT_SWAP_APPEAL = 5f;
    private bool IsUpdated = true;
    [SerializeField]
    private Image AppealObject1;
    [SerializeField]
    private CanvasGroup AppealGroup0;
    [SerializeField]
    private CanvasGroup AppealGroup1;
    private string[] mAppealIds;
    private int mCurrentIndex;
    private bool IsLoaded;
    private float mWaitSwapeAppealTime;
    private Sprite mCurrentSprite;
    private Sprite mNextSprite;

    protected override void Awake()
    {
      base.Awake();
      if (!Object.op_Inequality((Object) this.AppealObject1, (Object) null))
        return;
      ((Component) this.AppealObject1).get_gameObject().SetActive(false);
    }

    protected override void Start()
    {
      base.Start();
      if (!this.LoadAppealMaster(this.MASTER_PATH))
        return;
      this.StartCoroutine(this.LoadAppealResources());
    }

    protected override void Update()
    {
      base.Update();
      if (!this.IsLoaded)
        return;
      if (this.IsUpdated)
        this.Refresh();
      else
        this.UpdateAppeal();
    }

    private void UpdateAppeal()
    {
      if (Object.op_Equality((Object) this.mCurrentSprite, (Object) null) || Object.op_Equality((Object) this.mNextSprite, (Object) null))
        return;
      this.mWaitSwapeAppealTime -= Time.get_deltaTime();
      if ((double) this.mWaitSwapeAppealTime >= 0.0 || !Object.op_Inequality((Object) this.AppealGroup0, (Object) null) || !Object.op_Inequality((Object) this.AppealGroup1, (Object) null))
        return;
      float deltaTime = Time.get_deltaTime();
      CanvasGroup appealGroup0 = this.AppealGroup0;
      appealGroup0.set_alpha(appealGroup0.get_alpha() + -deltaTime);
      this.AppealGroup0.set_alpha(Mathf.Clamp(this.AppealGroup0.get_alpha() - deltaTime, 0.0f, 1f));
      CanvasGroup appealGroup1 = this.AppealGroup1;
      appealGroup1.set_alpha(appealGroup1.get_alpha() + deltaTime);
      this.AppealGroup1.set_alpha(Mathf.Clamp(this.AppealGroup1.get_alpha() + deltaTime, 0.0f, 1f));
      if ((double) this.AppealGroup1.get_alpha() > 0.0 && (double) this.AppealGroup1.get_alpha() < 1.0)
        return;
      this.IsUpdated = true;
      this.mWaitSwapeAppealTime = this.WAIT_SWAP_APPEAL;
    }

    protected override void Refresh()
    {
      this.mCurrentSprite = this.mAppealIds.Length <= this.mCurrentIndex || !this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex]) ? this.mCacheAppealSprites[this.mAppealIds[0]] : this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex]];
      this.mNextSprite = this.mAppealIds.Length <= this.mCurrentIndex + 1 || !this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex + 1]) ? (this.mAppealIds.Length != 1 ? this.mCacheAppealSprites[this.mAppealIds[0]] : (Sprite) null) : this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex + 1]];
      if (Object.op_Inequality((Object) this.AppealObject, (Object) null))
      {
        this.AppealObject.set_sprite(this.mCurrentSprite);
        ((Component) this.AppealObject).get_gameObject().SetActive(Object.op_Inequality((Object) this.mCurrentSprite, (Object) null));
        if (Object.op_Inequality((Object) this.AppealGroup0, (Object) null))
          this.AppealGroup0.set_alpha(1f);
      }
      if (Object.op_Inequality((Object) this.AppealObject1, (Object) null) && Object.op_Inequality((Object) this.mNextSprite, (Object) null))
      {
        this.AppealObject1.set_sprite(this.mNextSprite);
        ((Component) this.AppealObject1).get_gameObject().SetActive(Object.op_Inequality((Object) this.mNextSprite, (Object) null));
        if (Object.op_Inequality((Object) this.AppealGroup1, (Object) null))
          this.AppealGroup1.set_alpha(0.0f);
      }
      ++this.mCurrentIndex;
      if (this.mCurrentIndex >= this.mAppealIds.Length)
        this.mCurrentIndex = 0;
      this.IsUpdated = Object.op_Equality((Object) this.mNextSprite, (Object) null);
    }

    protected override void Destroy()
    {
      base.Destroy();
      using (Dictionary<string, Sprite>.KeyCollection.Enumerator enumerator = this.mCacheAppealSprites.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
          Resources.UnloadAsset((Object) this.mCacheAppealSprites[enumerator.Current]);
      }
      this.mCacheAppealSprites = (Dictionary<string, Sprite>) null;
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
        JSON_AppealQuestMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealQuestMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        List<string> stringList = new List<string>();
        foreach (JSON_AppealQuestMaster json in jsonArray)
        {
          AppealQuestMaster appealQuestMaster = new AppealQuestMaster();
          if (appealQuestMaster.Deserialize(json) && appealQuestMaster.start_at <= serverTime && appealQuestMaster.end_at > serverTime)
            stringList.Add(appealQuestMaster.appeal_id);
        }
        if (stringList != null)
        {
          if (stringList.Count > 0)
          {
            this.mAppealIds = new string[stringList.Count];
            this.mAppealIds = stringList.ToArray();
          }
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
    private IEnumerator LoadAppealResources()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemQuest.\u003CLoadAppealResources\u003Ec__Iterator9A() { \u003C\u003Ef__this = this };
    }
  }
}
