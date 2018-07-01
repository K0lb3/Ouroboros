// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterComposer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public struct CharacterComposer
  {
    public GameObject Body;
    public GameObject BodyAttachment;
    public Texture2D BodyTexture;
    public GameObject Head;
    public GameObject HeadAttachment;
    public GameObject Hair;
    public Color32 HairColor0;
    public Color32 HairColor1;

    public bool IsValid
    {
      get
      {
        return Object.op_Inequality((Object) this.Body, (Object) null);
      }
    }

    public void LoadImmediately(string characterID, ESex sex, string jobID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character != null)
      {
        int index = character.IndexOfJob(jobID);
        if (string.IsNullOrEmpty(jobID) || index >= 0)
        {
          CharacterDB.Job job = character.Jobs[index];
          if (!string.IsNullOrEmpty(job.BodyName))
          {
            this.Body = (GameObject) Resources.Load<GameObject>("CH/BODY/" + job.BodyName);
            if (Object.op_Equality((Object) this.Body, (Object) null))
            {
              Debug.LogError((object) ("Failed to load " + job.BodyName));
            }
            else
            {
              if (!string.IsNullOrEmpty(job.BodyTextureName))
              {
                this.BodyTexture = (Texture2D) Resources.Load<Texture2D>("CH/BODYTEX/" + job.BodyTextureName);
                if (Object.op_Equality((Object) this.BodyTexture, (Object) null))
                {
                  Debug.LogError((object) ("Failed to load " + job.BodyTextureName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.BodyAttachmentName))
              {
                this.BodyAttachment = (GameObject) Resources.Load<GameObject>("CH/BODYOPT/" + job.BodyAttachmentName);
                if (Object.op_Equality((Object) this.BodyAttachment, (Object) null))
                {
                  Debug.LogError((object) ("Failed to load " + job.BodyAttachmentName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HeadName))
              {
                this.Head = (GameObject) Resources.Load<GameObject>("CH/HEAD/" + job.HeadName);
                if (Object.op_Equality((Object) this.Head, (Object) null))
                {
                  Debug.LogError((object) ("Failed to load " + job.HeadName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HairName))
              {
                this.Hair = (GameObject) Resources.Load<GameObject>("CH/HAIR/" + job.HairName);
                if (Object.op_Equality((Object) this.Hair, (Object) null))
                {
                  Debug.LogError((object) ("Failed to load " + job.HairName));
                  goto label_24;
                }
              }
              if (!string.IsNullOrEmpty(job.HeadAttachmentName))
              {
                this.HeadAttachment = (GameObject) Resources.Load<GameObject>("CH/HEADOPT/" + job.HeadAttachmentName);
                if (Object.op_Equality((Object) this.HeadAttachment, (Object) null))
                {
                  Debug.LogError((object) ("Failed to load " + job.HeadAttachmentName));
                  goto label_24;
                }
              }
              this.HairColor0 = job.HairColor0;
              this.HairColor1 = job.HairColor1;
              return;
            }
          }
        }
        else
        {
          this.Body = (GameObject) Resources.Load<GameObject>("Units/" + sex.ToPrefix() + characterID);
          if (Object.op_Inequality((Object) this.Body, (Object) null))
            return;
          Debug.LogError((object) ("Failed to load " + sex.ToPrefix() + characterID));
        }
      }
label_24:
      this.Body = (GameObject) Resources.Load<GameObject>("Units/NULL");
      this.BodyAttachment = (GameObject) null;
      this.BodyTexture = (Texture2D) null;
      this.Head = (GameObject) null;
      this.HeadAttachment = (GameObject) null;
      this.Hair = (GameObject) null;
    }

    public GameObject Compose(Vector3 position, Quaternion rotation)
    {
      if (Object.op_Equality((Object) this.Body, (Object) null))
        return (GameObject) null;
      GameObject gameObject1 = Object.Instantiate((Object) this.Body, position, Quaternion.op_Multiply(rotation, this.Body.get_transform().get_rotation())) as GameObject;
      Transform transform1 = gameObject1.get_transform();
      if (Object.op_Inequality((Object) this.BodyTexture, (Object) null))
      {
        SkinnedMeshRenderer componentInChildren = (SkinnedMeshRenderer) gameObject1.GetComponentInChildren<SkinnedMeshRenderer>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        {
          Material material = new Material(((Renderer) componentInChildren).get_sharedMaterial());
          material.set_mainTexture((Texture) this.BodyTexture);
          ((Renderer) componentInChildren).set_sharedMaterial(material);
        }
      }
      Transform childRecursively = GameUtility.findChildRecursively(transform1, "Bip001 Head");
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
      {
        if (Object.op_Inequality((Object) this.Head, (Object) null))
        {
          Transform transform2 = this.Head.get_transform();
          (Object.Instantiate((Object) this.Head, transform2.get_localPosition(), transform2.get_localRotation()) as GameObject).get_transform().SetParent(childRecursively, false);
        }
        if (Object.op_Inequality((Object) this.Hair, (Object) null))
        {
          Transform transform2 = this.Hair.get_transform();
          GameObject gameObject2 = Object.Instantiate((Object) this.Hair, transform2.get_localPosition(), transform2.get_localRotation()) as GameObject;
          gameObject2.get_transform().SetParent(childRecursively, false);
          Renderer[] componentsInChildren = (Renderer[]) gameObject2.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < componentsInChildren.Length; ++index)
          {
            if ((componentsInChildren[index] is MeshRenderer || componentsInChildren[index] is SkinnedMeshRenderer) && Object.op_Inequality((Object) componentsInChildren[index].get_sharedMaterial(), (Object) null))
            {
              Material material = new Material(componentsInChildren[index].get_sharedMaterial());
              ((Object) material).set_hideFlags((HideFlags) 52);
              material.SetColor("_hairColor0", Color32.op_Implicit(this.HairColor0));
              material.SetColor("_hairColor1", Color32.op_Implicit(this.HairColor1));
              componentsInChildren[index].set_sharedMaterial(material);
            }
          }
        }
        if (Object.op_Inequality((Object) this.HeadAttachment, (Object) null))
        {
          Transform transform2 = this.HeadAttachment.get_transform();
          (Object.Instantiate((Object) this.HeadAttachment, transform2.get_localPosition(), transform2.get_localRotation()) as GameObject).get_transform().SetParent(childRecursively, false);
        }
      }
      return gameObject1;
    }
  }
}
