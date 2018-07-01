// Decompiled with JetBrains decompiler
// Type: SRPG.TeamNameInputWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TeamNameInputWindow : MonoBehaviour
  {
    [SerializeField]
    private InputFieldCensorship inputField;

    public TeamNameInputWindow()
    {
      base.\u002Ector();
    }

    public void SetInputName()
    {
      if (string.IsNullOrEmpty(this.inputField.get_text()))
        return;
      string str = this.inputField.get_text();
      if (str.Length > this.inputField.get_characterLimit())
        str = str.Substring(0, this.inputField.get_characterLimit());
      GlobalVars.TeamName = str;
    }
  }
}
