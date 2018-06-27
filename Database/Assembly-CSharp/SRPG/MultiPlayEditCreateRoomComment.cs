// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayEditCreateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MultiPlayEditCreateRoomComment : MonoBehaviour
  {
    public InputFieldCensorship Comment;

    public MultiPlayEditCreateRoomComment()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnClickEdit()
    {
      this.Comment.set_readOnly(false);
      this.Comment.ActivateInputField();
    }

    public void OnEndEdit()
    {
      this.Comment.set_readOnly(true);
    }
  }
}
