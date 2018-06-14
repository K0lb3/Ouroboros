// Decompiled with JetBrains decompiler
// Type: ContactBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    string mailto = LocalizedText.Get("contact.CONTACT_ADDRESS");
    int subject1 = (int) this.Subject;
    string subject2 = LocalizedText.Get("contact.CONTACT_SUBJECT_" + subject1.ToString("d2"));
    if (string.IsNullOrEmpty(subject2))
      subject2 = LocalizedText.Get("contact.CONTACT_SUBJECT");
    string str1 = LocalizedText.Get("contact.CONTACT_BODY_OPTION_" + subject1.ToString("d2"));
    string str2 = "CONTACT_BODY_OPTION_" + subject1.ToString("d2");
    string str3 = LocalizedText.Get("contact.CONTACT_BODY_TEMPLATE", new object[1]
    {
      (object) (!(str1 == str2) ? str1 : string.Empty)
    });
    string str4 = MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode;
    if (string.IsNullOrEmpty(str4))
    {
      string configOkyakusamaCode = GameUtility.Config_OkyakusamaCode;
      if (!string.IsNullOrEmpty(configOkyakusamaCode))
        str4 = configOkyakusamaCode;
    }
    string name = MonoSingleton<GameManager>.Instance.Player.Name;
    string bundleVersion = Application.GetBundleVersion();
    string str5 = AssetManager.AssetRevision.ToString();
    string deviceModel = SystemInfo.get_deviceModel();
    string operatingSystem = SystemInfo.get_operatingSystem();
    string body = str3 + LocalizedText.Get("contact.CONTACT_PLAYER_DATA", (object) str4, (object) bundleVersion, (object) str5, (object) name, (object) deviceModel, (object) operatingSystem);
    MailerUtility.Launch(mailto, subject2, body);
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
