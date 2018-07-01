// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChatChannelItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject Begginer;
    [SerializeField]
    private GameObject ChannelName;
    [SerializeField]
    private GameObject Fever;
    private int mChannelID;

    public ChatChannelItem()
    {
      base.\u002Ector();
    }

    public int ChannelID
    {
      get
      {
        return this.mChannelID;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Begginer, (Object) null))
        this.Begginer.SetActive(false);
      if (Object.op_Inequality((Object) this.ChannelName, (Object) null))
        this.ChannelName.SetActive(false);
      if (!Object.op_Inequality((Object) this.Fever, (Object) null))
        return;
      this.Fever.SetActive(false);
    }

    public void Refresh(ChatChannelParam param)
    {
      if (param == null)
        return;
      if (param.category_id == 2)
        this.Begginer.SetActive(true);
      this.mChannelID = param.id;
      string str = "CH " + param.id.ToString();
      if (!string.IsNullOrEmpty(param.name))
        str = param.name;
      ((Text) ((Component) this.ChannelName.get_transform().FindChild("text")).GetComponent<Text>()).set_text(str);
      this.ChannelName.SetActive(true);
      ((ImageArray) this.Fever.GetComponent<ImageArray>()).ImageIndex = param.fever_level < 15 ? (param.fever_level < 10 ? 0 : 1) : 2;
      this.Fever.SetActive(true);
    }
  }
}
