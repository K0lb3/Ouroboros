// Decompiled with JetBrains decompiler
// Type: NetworkCullingHandler
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class NetworkCullingHandler : MonoBehaviour
{
  private int orderIndex;
  private CullArea cullArea;
  private List<int> previousActiveCells;
  private List<int> activeCells;
  private PhotonView pView;
  private Vector3 lastPosition;
  private Vector3 currentPosition;

  public NetworkCullingHandler()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    if (Object.op_Equality((Object) this.pView, (Object) null))
    {
      this.pView = (PhotonView) ((Component) this).GetComponent<PhotonView>();
      if (!this.pView.isMine)
        return;
    }
    if (Object.op_Equality((Object) this.cullArea, (Object) null))
      this.cullArea = (CullArea) Object.FindObjectOfType<CullArea>();
    this.previousActiveCells = new List<int>(0);
    this.activeCells = new List<int>(0);
    this.currentPosition = this.lastPosition = ((Component) this).get_transform().get_position();
  }

  private void Start()
  {
    if (!this.pView.isMine || !PhotonNetwork.inRoom)
      return;
    if (this.cullArea.NumberOfSubdivisions == 0)
    {
      this.pView.group = this.cullArea.FIRST_GROUP_ID;
      PhotonNetwork.SetReceivingEnabled(this.cullArea.FIRST_GROUP_ID, true);
      PhotonNetwork.SetSendingEnabled(this.cullArea.FIRST_GROUP_ID, true);
    }
    else
    {
      this.CheckGroupsChanged();
      this.InvokeRepeating("UpdateActiveGroup", 0.0f, 1f / (float) PhotonNetwork.sendRateOnSerialize);
    }
  }

  private void Update()
  {
    if (!this.pView.isMine)
      return;
    this.lastPosition = this.currentPosition;
    this.currentPosition = ((Component) this).get_transform().get_position();
    if (!Vector3.op_Inequality(this.currentPosition, this.lastPosition))
      return;
    this.CheckGroupsChanged();
  }

  private void OnDisable()
  {
    this.CancelInvoke();
  }

  private void CheckGroupsChanged()
  {
    if (this.cullArea.NumberOfSubdivisions == 0)
      return;
    this.previousActiveCells = new List<int>((IEnumerable<int>) this.activeCells);
    this.activeCells = this.cullArea.GetActiveCells(((Component) this).get_transform().get_position());
    if (this.activeCells.Count != this.previousActiveCells.Count)
    {
      this.UpdateInterestGroups();
    }
    else
    {
      using (List<int>.Enumerator enumerator = this.activeCells.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (!this.previousActiveCells.Contains(enumerator.Current))
          {
            this.UpdateInterestGroups();
            break;
          }
        }
      }
    }
  }

  private void UpdateInterestGroups()
  {
    using (List<int>.Enumerator enumerator = this.previousActiveCells.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        PhotonNetwork.SetReceivingEnabled(current, false);
        PhotonNetwork.SetSendingEnabled(current, false);
      }
    }
    using (List<int>.Enumerator enumerator = this.activeCells.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        int current = enumerator.Current;
        PhotonNetwork.SetReceivingEnabled(current, true);
        PhotonNetwork.SetSendingEnabled(current, true);
      }
    }
  }

  private void UpdateActiveGroup()
  {
    while (this.activeCells.Count <= this.cullArea.NumberOfSubdivisions)
      this.activeCells.Add(this.cullArea.FIRST_GROUP_ID);
    if (this.cullArea.NumberOfSubdivisions == 1)
    {
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER[this.orderIndex]];
    }
    else if (this.cullArea.NumberOfSubdivisions == 2)
    {
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER[this.orderIndex]];
    }
    else
    {
      if (this.cullArea.NumberOfSubdivisions != 3)
        return;
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER[this.orderIndex]];
    }
  }

  private void OnGUI()
  {
    if (!this.pView.isMine)
      return;
    string str1 = "Inside cells:\n";
    string str2 = "Subscribed cells:\n";
    for (int index = 0; index < this.activeCells.Count; ++index)
    {
      if (index <= this.cullArea.NumberOfSubdivisions)
        str1 = str1 + (object) this.activeCells[index] + "  ";
      str2 = str2 + (object) this.activeCells[index] + "  ";
    }
    Rect rect1 = new Rect(20f, (float) Screen.get_height() - 100f, 200f, 40f);
    string str3 = "<color=white>" + str1 + "</color>";
    GUIStyle guiStyle1 = new GUIStyle();
    guiStyle1.set_alignment((TextAnchor) 0);
    guiStyle1.set_fontSize(16);
    GUIStyle guiStyle2 = guiStyle1;
    GUI.Label(rect1, str3, guiStyle2);
    Rect rect2 = new Rect(20f, (float) Screen.get_height() - 60f, 200f, 40f);
    string str4 = "<color=white>" + str2 + "</color>";
    GUIStyle guiStyle3 = new GUIStyle();
    guiStyle3.set_alignment((TextAnchor) 0);
    guiStyle3.set_fontSize(16);
    GUIStyle guiStyle4 = guiStyle3;
    GUI.Label(rect2, str4, guiStyle4);
  }
}
