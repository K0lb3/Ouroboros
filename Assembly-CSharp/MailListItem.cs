// Decompiled with JetBrains decompiler
// Type: MailListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MailListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject limit;
  [SerializeField]
  private GameObject button;
  [SerializeField]
  private Text timeText;
  private ListItemEvents _listItemEvents;

  public MailListItem()
  {
    base.\u002Ector();
  }

  public ListItemEvents listItemEvents
  {
    get
    {
      return (ListItemEvents) ((Component) this).GetComponent<ListItemEvents>();
    }
  }

  public GameObject Button
  {
    get
    {
      return this.button;
    }
  }

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
    {
      this.limit.get_gameObject().SetActive(false);
      this.button.get_gameObject().SetActive(false);
      this.SetTime(read);
    }
    else
    {
      this.limit.get_gameObject().SetActive(isPeriod);
      this.button.get_gameObject().SetActive(true);
      this.SetTime(post_at);
    }
  }

  private void SetTime(long time)
  {
    DateTime serverTime = TimeManager.ServerTime;
    DateTime localTime = GameUtility.UnixtimeToLocalTime(time);
    TimeSpan timeSpan = serverTime - localTime;
    string empty = string.Empty;
    string str;
    if (timeSpan.Days >= 1)
    {
      string format = "yyyy/MM/dd";
      str = localTime.ToString(format);
    }
    else if (timeSpan.Hours >= 1)
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_HOURS", new object[1]
      {
        (object) timeSpan.Hours
      });
    else if (timeSpan.Minutes >= 1)
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_MINUTES", new object[1]
      {
        (object) timeSpan.Minutes
      });
    else
      str = LocalizedText.Get("sys.MAILBOX_RECEIVE_TIME_SECONDS", new object[1]
      {
        (object) timeSpan.Seconds
      });
    this.timeText.set_text(str);
  }
}
