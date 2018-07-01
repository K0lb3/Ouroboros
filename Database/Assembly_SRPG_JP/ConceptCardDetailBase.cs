// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailBase : MonoBehaviour
  {
    protected ConceptCardData mConceptCardData;

    public ConceptCardDetailBase()
    {
      base.\u002Ector();
    }

    protected GameManager GM
    {
      get
      {
        return MonoSingleton<GameManager>.Instance;
      }
    }

    protected MasterParam Master
    {
      get
      {
        return this.GM.MasterParam;
      }
    }

    public virtual void SetParam(ConceptCardData card_data)
    {
      this.mConceptCardData = card_data;
    }

    public virtual void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
    }

    public virtual void Refresh()
    {
    }

    public void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(str);
    }

    public void LoadImage(string path, RawImage image)
    {
      if (!Object.op_Inequality((Object) image, (Object) null))
        return;
      string fileName = Path.GetFileName(path);
      if (!(((Object) image.get_mainTexture()).get_name() != fileName))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, path);
    }

    public void SwitchObject(bool is_on, GameObject obj, GameObject opposite_obj)
    {
      if (Object.op_Inequality((Object) obj, (Object) null))
        obj.SetActive(is_on);
      if (!Object.op_Inequality((Object) opposite_obj, (Object) null))
        return;
      opposite_obj.SetActive(!is_on);
    }

    public void SetSprite(Image image, Sprite sprite)
    {
      if (!Object.op_Inequality((Object) image, (Object) null))
        return;
      image.set_sprite(sprite);
    }
  }
}
