// Decompiled with JetBrains decompiler
// Type: LogMonitor
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class LogMonitor : MonoBehaviour
{
  private static LogMonitor mInstnace;
  private List<LogMonitor.Log> mLogs;
  private int mLogCount;
  private GUIStyle mBackgroundStyle;
  private GUIStyle mErrorStyle;
  private GUIStyle mExceptionStyle;
  private GUIStyle mStackTraceStyle;
  private string mStackTrace;
  private LogMonitor.GUICallback mCallback;

  public LogMonitor()
  {
    base.\u002Ector();
  }

  public static void Start()
  {
    if (!GameUtility.IsDebugBuild || !Object.op_Equality((Object) LogMonitor.mInstnace, (Object) null))
      return;
    LogMonitor.mInstnace = (LogMonitor) new GameObject(nameof (LogMonitor)).AddComponent<LogMonitor>();
  }

  private void Awake()
  {
    Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
    ((Object) this).set_hideFlags((HideFlags) 61);
  }

  private void OnEnable()
  {
    // ISSUE: method pointer
    GameUtility.RegisterLogCallback(new Application.LogCallback((object) this, __methodptr(HandleLog)));
  }

  private void OnDisable()
  {
    // ISSUE: method pointer
    GameUtility.UnregisterLogCallback(new Application.LogCallback((object) this, __methodptr(HandleLog)));
  }

  private void OnGUICallback()
  {
    if (this.mLogs.Count <= 0)
      return;
    if (this.mErrorStyle == null)
    {
      this.mErrorStyle = new GUIStyle(GUI.get_skin().get_label());
      this.mErrorStyle.set_contentOffset(Vector2.op_Multiply(Vector2.get_right(), 5f));
      this.mErrorStyle.set_margin(new RectOffset(0, 0, 0, 0));
      this.mErrorStyle.set_fontSize(10);
      this.mErrorStyle.set_normal(new GUIStyleState());
      this.mErrorStyle.get_normal().set_textColor(Color.get_white());
      this.mErrorStyle.get_normal().set_background(new Texture2D(1, 1));
      this.mErrorStyle.get_normal().get_background().SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f));
      this.mErrorStyle.get_normal().get_background().Apply();
      this.mErrorStyle.set_hover(new GUIStyleState());
      this.mErrorStyle.get_hover().set_textColor(Color.get_white());
      this.mErrorStyle.get_hover().set_background(new Texture2D(1, 1));
      this.mErrorStyle.get_hover().get_background().SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
      this.mErrorStyle.get_hover().get_background().Apply();
    }
    if (this.mStackTraceStyle == null)
    {
      this.mStackTraceStyle = new GUIStyle(GUI.get_skin().get_label());
      this.mStackTraceStyle.set_contentOffset(Vector2.op_Multiply(Vector2.get_right(), 5f));
      this.mStackTraceStyle.set_margin(new RectOffset(0, 0, 0, 0));
      this.mStackTraceStyle.set_fontSize(10);
      this.mStackTraceStyle.set_normal(new GUIStyleState());
      this.mStackTraceStyle.get_normal().set_textColor(Color.get_white());
      this.mStackTraceStyle.get_normal().set_background(new Texture2D(1, 1));
      this.mStackTraceStyle.get_normal().get_background().SetPixel(0, 0, new Color(0.0f, 0.0f, 1f));
      this.mStackTraceStyle.get_normal().get_background().Apply();
    }
    if (this.mExceptionStyle == null)
    {
      this.mExceptionStyle = new GUIStyle(GUI.get_skin().get_label());
      this.mExceptionStyle.set_contentOffset(Vector2.op_Multiply(Vector2.get_right(), 5f));
      this.mExceptionStyle.set_margin(new RectOffset(0, 0, 0, 0));
      this.mExceptionStyle.set_fontSize(10);
      this.mExceptionStyle.set_normal(new GUIStyleState());
      this.mExceptionStyle.get_normal().set_textColor(Color.get_yellow());
      this.mExceptionStyle.get_normal().set_background(new Texture2D(1, 1));
      this.mExceptionStyle.get_normal().get_background().SetPixel(0, 0, new Color(0.5f, 0.0f, 0.0f));
      this.mExceptionStyle.get_normal().get_background().Apply();
      this.mExceptionStyle.set_hover(new GUIStyleState());
      this.mExceptionStyle.get_hover().set_textColor(Color.get_yellow());
      this.mExceptionStyle.get_hover().set_background(new Texture2D(1, 1));
      this.mExceptionStyle.get_hover().get_background().SetPixel(0, 0, new Color(0.6f, 0.0f, 0.0f));
      this.mExceptionStyle.get_hover().get_background().Apply();
    }
    if (this.mBackgroundStyle == null)
    {
      this.mBackgroundStyle = new GUIStyle(GUI.get_skin().get_label());
      this.mBackgroundStyle.set_stretchWidth(true);
      this.mBackgroundStyle.set_stretchHeight(true);
      this.mBackgroundStyle.get_normal().set_background(new Texture2D(1, 1));
      this.mBackgroundStyle.get_normal().get_background().SetPixel(0, 0, Color.get_black());
      this.mBackgroundStyle.get_normal().get_background().Apply();
      this.mBackgroundStyle.set_margin(new RectOffset(0, 0, 0, 0));
      this.mBackgroundStyle.set_padding(this.mBackgroundStyle.get_margin());
    }
    GUI.Box(new Rect(0.0f, 0.0f, (float) Screen.get_width(), 25f), string.Empty, this.mBackgroundStyle);
    if (GUI.Button(new Rect((float) (Screen.get_width() - 25), 0.0f, 25f, 25f), "X"))
    {
      this.mLogs.Clear();
      this.mStackTrace = (string) null;
      Object.Destroy((Object) ((Component) this.mCallback).get_gameObject());
      this.mCallback = (LogMonitor.GUICallback) null;
    }
    else
    {
      GUILayout.Space(25f);
      GUILayout.BeginVertical(new GUILayoutOption[0]);
      for (int index = 0; index < this.mLogs.Count; ++index)
      {
        if (GUILayout.Button("#" + (object) this.mLogs[index].index + " " + this.mLogs[index].logString, this.mLogs[index].type == 4 ? this.mExceptionStyle : this.mErrorStyle, new GUILayoutOption[1]{ GUILayout.Width((float) Screen.get_width()) }))
          this.mStackTrace = this.mLogs[index].stackTrace;
      }
      GUILayout.EndVertical();
      if (!string.IsNullOrEmpty(this.mStackTrace))
      {
        GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
        {
          GUILayout.Height(4f)
        });
        GUILayout.Label(this.mStackTrace, this.mStackTraceStyle, new GUILayoutOption[1]
        {
          GUILayout.Width((float) Screen.get_width())
        });
      }
      GUILayout.Box(string.Empty, this.mBackgroundStyle, new GUILayoutOption[1]
      {
        GUILayout.Height(8f)
      });
    }
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != 4 && type != null || type == null && logString.StartsWith("Asynchronous Background loading is only supported in Unity Pro."))
      return;
    LogMonitor.Log log;
    if (this.mLogs.Count > 5)
    {
      log = this.mLogs[0];
      this.mLogs.RemoveAt(0);
    }
    else
      log = new LogMonitor.Log();
    log.type = type;
    log.logString = logString;
    log.stackTrace = stackTrace;
    log.index = ++this.mLogCount;
    this.mLogs.Add(log);
    if (!Object.op_Equality((Object) this.mCallback, (Object) null))
      return;
    this.mCallback = (LogMonitor.GUICallback) new GameObject("callback", new System.Type[2]
    {
      typeof (GameObject),
      typeof (LogMonitor.GUICallback)
    }).GetComponent<LogMonitor.GUICallback>();
    this.mCallback.OnGUIListener = new LogMonitor.GUICallback.GUIEvent(this.OnGUICallback);
    ((Component) this.mCallback).get_transform().SetParent(((Component) this).get_transform(), false);
  }

  private class Log
  {
    public int index;
    public string logString;
    public string stackTrace;
    public LogType type;
  }

  [AddComponentMenu("")]
  public class GUICallback : MonoBehaviour
  {
    public LogMonitor.GUICallback.GUIEvent OnGUIListener;

    public GUICallback()
    {
      base.\u002Ector();
    }

    private void OnGUI()
    {
      this.OnGUIListener();
    }

    public delegate void GUIEvent();
  }
}
