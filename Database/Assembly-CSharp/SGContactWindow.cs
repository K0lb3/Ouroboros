// Decompiled with JetBrains decompiler
// Type: SGContactWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGContactWindow : MonoBehaviour
{
  [SerializeField]
  private Dropdown issueDropdown;
  [SerializeField]
  private Text emailText;
  [SerializeField]
  private Text messageText;
  [SerializeField]
  private Text playerNameText;
  [SerializeField]
  private Text deviceModelText;
  [SerializeField]
  private Text clientVerText;

  public SGContactWindow()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.UpdateDropdownList();
    this.UpdateText();
  }

  protected void UpdateDropdownList()
  {
    for (int index = 0; index < this.issueDropdown.get_options().Count; ++index)
      this.issueDropdown.get_options()[index].set_text(LocalizedText.Get(this.issueDropdown.get_options()[index].get_text()));
    this.issueDropdown.get_captionText().set_text(this.issueDropdown.get_options()[0].get_text());
  }

  protected void UpdateText()
  {
    if (PlayerPrefs.HasKey("PlayerName"))
      this.playerNameText.set_text(LocalizedText.Get("sys.CONTACT_PLAYER_NAME") + ": " + PlayerPrefs.GetString("PlayerName"));
    else
      this.playerNameText.set_text(string.Empty);
    this.deviceModelText.set_text(LocalizedText.Get("sys.CONTACT_DEVICE_MODEL") + ": " + SystemInfo.get_deviceModel());
    this.clientVerText.set_text("v" + MyApplicationPlugin.get_version());
  }
}
