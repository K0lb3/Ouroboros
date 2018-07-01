// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLighting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CharacterLighting : MonoBehaviour
  {
    public CharacterLighting()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Update();
    }

    private void Update()
    {
      Vector3 position = ((Component) this).get_transform().get_position();
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if (Object.op_Equality((Object) volume, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      MeshRenderer[] componentsInChildren1 = (MeshRenderer[]) ((Component) this).GetComponentsInChildren<MeshRenderer>();
      for (int index = 0; index < componentsInChildren1.Length; ++index)
      {
        ((Renderer) componentsInChildren1[index]).get_material().SetColor("_directLitColor", directLit);
        ((Renderer) componentsInChildren1[index]).get_material().SetColor("_indirectLitColor", indirectLit);
      }
      SkinnedMeshRenderer[] componentsInChildren2 = (SkinnedMeshRenderer[]) ((Component) this).GetComponentsInChildren<SkinnedMeshRenderer>();
      for (int index = 0; index < componentsInChildren2.Length; ++index)
      {
        ((Renderer) componentsInChildren2[index]).get_material().SetColor("_directLitColor", directLit);
        ((Renderer) componentsInChildren2[index]).get_material().SetColor("_indirectLitColor", indirectLit);
      }
    }
  }
}
