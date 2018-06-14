// Decompiled with JetBrains decompiler
// Type: NonInteractableMessage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Selectable))]
public class NonInteractableMessage : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
  public string Caption;
  public string Message;

  public NonInteractableMessage()
  {
    base.\u002Ector();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (((Selectable) ((Component) this).GetComponent<Selectable>()).get_interactable())
      return;
    UIUtility.NegativeSystemMessage(LocalizedText.Get(this.Caption), LocalizedText.Get(this.Message), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
  }
}
