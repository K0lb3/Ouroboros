// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLogItem
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
  public class ChatLogItem : MonoBehaviour
  {
    public static readonly string CHAT_STAMP_IMG = "Stamps/StampTable";
    [SerializeField]
    private LayoutElement Element;
    [SerializeField]
    private RectTransform TextRootObject;
    [SerializeField]
    private GameObject Icon;
    [SerializeField]
    private RawImage LeftIcon;
    [SerializeField]
    private RawImage RightIcon;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text FuID;
    [SerializeField]
    private Text PostAt;
    [SerializeField]
    private GameObject MessageIcon;
    [SerializeField]
    private GameObject MessageLog;
    [SerializeField]
    private GameObject MyMessageIcon;
    [SerializeField]
    private GameObject MyMessageLog;
    [SerializeField]
    private GameObject AdminMessageLog;
    private GameObject MyStamp;
    private GameObject AnyStamp;
    private Transform mStampRoot;
    private Transform mMessageRoot;
    private Image mLogImg;
    private ChatStampParam[] mStampParams;
    private bool IsStampSettings;
    private bool mSetuped;
    private long mPostAt;
    private string mFUID;
    private Coroutine mCoroutine;
    private GameObject mRoot;
    private readonly int STAMP_SIZE;
    private Text mNameObj;
    private Text mPostAtObj;
    private Text mFuIDObj;
    private static SpriteSheet mStampImages;
    private static bool IsStampLoaded;
    private static bool mStampLoaded;
    private static string mStampLanguage;

    public ChatLogItem()
    {
      base.\u002Ector();
    }

    public GameObject GetIcon
    {
      get
      {
        return this.Icon;
      }
    }

    public bool IsSetuped
    {
      get
      {
        return this.mSetuped;
      }
    }

    public void Awake()
    {
      if (Object.op_Inequality((Object) this.MessageIcon, (Object) null))
        this.MessageIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.MessageLog, (Object) null))
      {
        this.MessageLog.SetActive(false);
        Transform transform = this.MessageLog.get_transform().Find("messages/stamp");
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          this.AnyStamp = ((Component) transform).get_gameObject();
          ((Component) transform).get_gameObject().SetActive(false);
        }
      }
      if (Object.op_Inequality((Object) this.MyMessageIcon, (Object) null))
        this.MyMessageIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.MyMessageLog, (Object) null))
      {
        this.MyMessageLog.SetActive(false);
        Transform transform = this.MyMessageLog.get_transform().Find("messages/stamp");
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          this.MyStamp = ((Component) transform).get_gameObject();
          ((Component) transform).get_gameObject().SetActive(false);
        }
      }
      if (!Object.op_Inequality((Object) this.AdminMessageLog, (Object) null))
        return;
      this.AdminMessageLog.SetActive(false);
      Transform transform1 = this.AdminMessageLog.get_transform().Find("messages/stamp");
      if (!Object.op_Inequality((Object) transform1, (Object) null))
        return;
      ((Component) transform1).get_gameObject().SetActive(false);
    }

    private void Start()
    {
      this.SetupChatStampMaster();
      this.IsStampSettings = true;
      if (ChatLogItem.mStampLanguage != GameUtility.Config_Language)
      {
        ChatLogItem.mStampImages = (SpriteSheet) null;
        ChatLogItem.mStampLoaded = false;
      }
      if (!Object.op_Equality((Object) ChatLogItem.mStampImages, (Object) null) || ChatLogItem.mStampLoaded)
        return;
      this.StartCoroutine(this.LoadStampImages());
    }

    public void OnDisable()
    {
      if (this.mCoroutine == null)
        return;
      this.StopCoroutine(this.mCoroutine);
      this.mCoroutine = (Coroutine) null;
    }

    public void Refresh(ChatLogParam param, ChatWindow.MessageTemplateType type)
    {
      if (param == null)
        return;
      if (this.mCoroutine != null)
      {
        this.StopCoroutine(this.mCoroutine);
        this.mCoroutine = (Coroutine) null;
      }
      if (Object.op_Equality((Object) this.mRoot, (Object) null))
      {
        if (!Object.op_Inequality((Object) ((Component) this).get_transform().get_parent(), (Object) null))
          return;
        this.mRoot = ((Component) ((Component) this).get_transform().get_parent()).get_gameObject();
      }
      this.MessageIcon.SetActive(false);
      this.MessageLog.SetActive(false);
      this.MyMessageIcon.SetActive(false);
      this.MyMessageLog.SetActive(false);
      this.AdminMessageLog.SetActive(false);
      switch (type)
      {
        case ChatWindow.MessageTemplateType.OtherUser:
          this.MessageIcon.SetActive(true);
          this.MessageLog.SetActive(true);
          this.mStampRoot = this.AnyStamp.get_transform();
          this.mMessageRoot = this.MessageLog.get_transform();
          break;
        case ChatWindow.MessageTemplateType.User:
          this.MyMessageIcon.SetActive(true);
          this.MyMessageLog.SetActive(true);
          this.mStampRoot = this.MyStamp.get_transform();
          this.mMessageRoot = this.MyMessageLog.get_transform();
          break;
        case ChatWindow.MessageTemplateType.Official:
          this.AdminMessageLog.SetActive(true);
          this.mMessageRoot = this.AdminMessageLog.get_transform();
          break;
      }
      if (Object.op_Inequality((Object) this.Icon, (Object) null) && Object.op_Inequality((Object) this.LeftIcon, (Object) null) && Object.op_Inequality((Object) this.RightIcon, (Object) null))
      {
        RawImage target = type != ChatWindow.MessageTemplateType.User ? this.LeftIcon : this.RightIcon;
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.icon);
        if (unitParam != null)
        {
          if (!string.IsNullOrEmpty(param.skin_iname) && Object.op_Inequality((Object) target, (Object) null))
          {
            ArtifactParam skin = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == param.skin_iname));
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(target, AssetPath.UnitSkinIconSmall(unitParam, skin, param.job_iname));
          }
          else
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(target, AssetPath.UnitIconSmall(unitParam, param.job_iname));
        }
      }
      Transform transform = this.mMessageRoot.Find("status");
      this.mNameObj = (Text) ((Component) transform.Find("name").Find("text")).GetComponent<Text>();
      this.mNameObj.set_text(param.name);
      this.mFuIDObj = (Text) ((Component) transform.Find("fuid").Find("text")).GetComponent<Text>();
      this.mFuIDObj.set_text(LocalizedText.Get("sys.TEXT_CHAT_FUID", new object[1]
      {
        (object) param.fuid.Substring(param.fuid.Length - 4, 4)
      }));
      this.mPostAtObj = (Text) ((Component) transform.Find("postat").Find("text")).GetComponent<Text>();
      this.mPostAtObj.set_text(ChatLogItem.GetPostAt(param.posted_at));
      this.TextRootObject = (RectTransform) ((Component) this.mMessageRoot.Find("messages")).GetComponent<RectTransform>();
      this.mLogImg = (Image) ((Component) this.TextRootObject).GetComponent<Image>();
      if ((int) param.message_type == 1)
      {
        if (!Object.op_Inequality((Object) this.mRoot, (Object) null) || !this.mRoot.get_activeInHierarchy())
          return;
        ((Component) this.mStampRoot).get_gameObject().SetActive(false);
        this.mCoroutine = this.StartCoroutine(this.RefreshTextLine(param.message));
      }
      else
      {
        if ((int) param.message_type != 2 || !Object.op_Inequality((Object) this.mRoot, (Object) null) || !this.mRoot.get_activeInHierarchy())
          return;
        ((Component) this.mStampRoot).get_gameObject().SetActive(true);
        if (Object.op_Inequality((Object) this.Element, (Object) null))
        {
          int stampSize = this.STAMP_SIZE;
          VerticalLayoutGroup component = (VerticalLayoutGroup) ((Component) this.TextRootObject).GetComponent<VerticalLayoutGroup>();
          this.Element.set_minHeight((float) (stampSize + ((LayoutGroup) component).get_padding().get_top() + ((LayoutGroup) component).get_padding().get_bottom() + (int) Mathf.Abs((float) this.TextRootObject.get_anchoredPosition().y)));
        }
        ((Behaviour) this.mLogImg).set_enabled(false);
        DebugUtility.Log(param.stamp_id.ToString());
        this.mCoroutine = this.StartCoroutine(this.RefreshStamp(param.stamp_id));
      }
    }

    public void RefreshPushMessage(ChatLogParam param, ChatWindow.MessageTemplateType type)
    {
      if (param == null)
        return;
      if (this.mCoroutine != null)
      {
        this.StopCoroutine(this.mCoroutine);
        this.mCoroutine = (Coroutine) null;
      }
      if (Object.op_Equality((Object) this.mRoot, (Object) null))
      {
        if (!Object.op_Inequality((Object) ((Component) this).get_transform().get_parent(), (Object) null))
          return;
        this.mRoot = ((Component) ((Component) this).get_transform().get_parent()).get_gameObject();
      }
      this.MessageIcon.SetActive(false);
      this.MessageLog.SetActive(false);
      this.MyMessageIcon.SetActive(false);
      this.MyMessageLog.SetActive(false);
      this.AdminMessageLog.SetActive(false);
      Transform transform = this.mMessageRoot.Find("status");
      ((Text) ((Component) transform.Find("name").Find("text")).GetComponent<Text>()).set_text(param.name);
      ((Text) ((Component) transform.Find("fuid").Find("text")).GetComponent<Text>()).set_text(LocalizedText.Get("sys.TEXT_CHAT_FUID", new object[1]
      {
        (object) param.fuid.Substring(param.fuid.Length - 4, 4)
      }));
      ((Text) ((Component) transform.Find("postat").Find("text")).GetComponent<Text>()).set_text(ChatLogItem.GetPostAt(param.posted_at));
      this.TextRootObject = (RectTransform) ((Component) this.mMessageRoot.Find("messages")).GetComponent<RectTransform>();
      if (!Object.op_Inequality((Object) this.mRoot, (Object) null) || !this.mRoot.get_activeInHierarchy())
        return;
      this.StartCoroutine(this.RefreshTextLine(param.message));
    }

    public static string GetPostAt(long postat)
    {
      string empty = string.Empty;
      TimeSpan timeSpan = TimeManager.ServerTime - GameUtility.UnixtimeToLocalTime(postat);
      int days = timeSpan.Days;
      int hours = timeSpan.Hours;
      int minutes = timeSpan.Minutes;
      int seconds = timeSpan.Seconds;
      string str;
      if (days > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_DAY", new object[1]
        {
          (object) days.ToString()
        });
      else if (hours > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_HOUR", new object[1]
        {
          (object) hours.ToString()
        });
      else if (minutes > 0)
        str = LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", new object[1]
        {
          (object) minutes.ToString()
        });
      else if (seconds > 10)
        str = LocalizedText.Get("sys.CHAT_POSTAT_SECOND", new object[1]
        {
          (object) seconds.ToString()
        });
      else
        str = LocalizedText.Get("sys.CHAT_POSTAT_NOW");
      return str;
    }

    public void RefreshPostAt()
    {
      TimeSpan timeSpan = TimeManager.ServerTime - GameUtility.UnixtimeToLocalTime(this.mPostAt);
      int days = timeSpan.Days;
      int hours = timeSpan.Hours;
      int minutes = timeSpan.Minutes;
      int seconds = timeSpan.Seconds;
      if (days > 0)
        this.PostAt.set_text(LocalizedText.Get("sys.CHAT_POSTAT_DAY", new object[1]
        {
          (object) days.ToString()
        }));
      else if (hours > 0)
        this.PostAt.set_text(LocalizedText.Get("sys.CHAT_POSTAT_HOUR", new object[1]
        {
          (object) hours.ToString()
        }));
      else if (minutes > 0)
        this.PostAt.set_text(LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", new object[1]
        {
          (object) minutes.ToString()
        }));
      else if (seconds > 10)
      {
        this.PostAt.set_text(LocalizedText.Get("sys.CHAT_POSTAT_MINUTE", new object[1]
        {
          (object) minutes.ToString()
        }));
      }
      else
      {
        if (seconds >= 10)
          return;
        this.PostAt.set_text(LocalizedText.Get("sys.CHAT_POSTAT_NOW"));
      }
    }

    [DebuggerHidden]
    private IEnumerator RefreshTextLine(string text)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshTextLine\u003Ec__IteratorA5() { text = text, \u003C\u0024\u003Etext = text, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator RefreshStamp(int id)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatLogItem.\u003CRefreshStamp\u003Ec__IteratorA6() { id = id, \u003C\u0024\u003Eid = id, \u003C\u003Ef__this = this };
    }

    private bool SetupChatStampMaster()
    {
      string src = AssetManager.LoadTextData(ChatStamp.CHAT_STAMP_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_ChatStampParam[] jsonArray = JSONParser.parseJSONArray<JSON_ChatStampParam>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        this.mStampParams = new ChatStampParam[jsonArray.Length];
        for (int index = 0; index < jsonArray.Length; ++index)
        {
          ChatStampParam chatStampParam = new ChatStampParam();
          if (chatStampParam.Deserialize(jsonArray[index]))
            this.mStampParams[index] = chatStampParam;
        }
      }
      catch
      {
        return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadStampImages()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatLogItem.\u003CLoadStampImages\u003Ec__IteratorA7 imagesCIteratorA7 = new ChatLogItem.\u003CLoadStampImages\u003Ec__IteratorA7();
      return (IEnumerator) imagesCIteratorA7;
    }
  }
}
