// Decompiled with JetBrains decompiler
// Type: ContactBtn
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using gu3.Device;
using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContactBtn : MonoBehaviour
{
  public ContactBtn.SubjectType Subject;

  public ContactBtn()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    Button component = (Button) ((Component) this).get_gameObject().GetComponent<Button>();
    if (Object.op_Equality((Object) component, (Object) null))
    {
      Debug.Log((object) "@@@[Menu]Button is Null Object!");
    }
    else
    {
      // ISSUE: method pointer
      ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OpenMailer)));
    }
  }

  private void OpenMailer()
  {
    string str1 = LocalizedText.Get("contact.CONTACT_ADDRESS");
    string str2 = LocalizedText.Get("contact.CONTACT_SUBJECT_" + ((int) this.Subject).ToString("d2"));
    if (string.IsNullOrEmpty(str2))
      str2 = LocalizedText.Get("contact.CONTACT_SUBJECT");
    string str3 = LocalizedText.Get("contact.CONTACT_BODY_TEMPLATE");
    string okyakusamaCode = MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode;
    string name = MonoSingleton<GameManager>.Instance.Player.Name;
    string bundleVersion = Application.GetBundleVersion();
    string str4 = AssetManager.AssetRevision.ToString();
    string deviceModel = SystemInfo.get_deviceModel();
    string operatingSystem = SystemInfo.get_operatingSystem();
    string str5 = str3 + LocalizedText.Get("contact.CONTACT_PLAYER_DATA", (object) okyakusamaCode, (object) bundleVersion, (object) str4, (object) name, (object) deviceModel, (object) operatingSystem);
    Application.LaunchMailer(str1, str2, str5);
  }

  public enum SubjectType : byte
  {
    DataRestore = 1,
    BuyCoin = 2,
    BugReport = 3,
    FgGID = 4,
    CommentRequest = 5,
    Other = 6,
  }
}
