namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
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
                return (this.Body != null);
            }
        }
        public void LoadImmediately(string characterID, ESex sex, string jobID)
        {
            CharacterDB.Character character;
            int num;
            CharacterDB.Job job;
            character = CharacterDB.FindCharacter(characterID);
            if (character == null)
            {
                goto Label_029A;
            }
            num = character.IndexOfJob(jobID);
            if (string.IsNullOrEmpty(jobID) != null)
            {
                goto Label_0027;
            }
            if (num < 0)
            {
                goto Label_0256;
            }
        Label_0027:
            job = character.Jobs[num];
            if (string.IsNullOrEmpty(job.BodyName) == null)
            {
                goto Label_0049;
            }
            goto Label_029A;
        Label_0049:
            this.Body = Resources.Load<GameObject>("CH/BODY/" + job.BodyName);
            if ((this.Body == null) == null)
            {
                goto Label_008F;
            }
            Debug.LogError("Failed to load " + job.BodyName);
            goto Label_029A;
        Label_008F:
            if (string.IsNullOrEmpty(job.BodyTextureName) != null)
            {
                goto Label_00E5;
            }
            this.BodyTexture = Resources.Load<Texture2D>("CH/BODYTEX/" + job.BodyTextureName);
            if ((this.BodyTexture == null) == null)
            {
                goto Label_00E5;
            }
            Debug.LogError("Failed to load " + job.BodyTextureName);
            goto Label_029A;
        Label_00E5:
            if (string.IsNullOrEmpty(job.BodyAttachmentName) != null)
            {
                goto Label_013B;
            }
            this.BodyAttachment = Resources.Load<GameObject>("CH/BODYOPT/" + job.BodyAttachmentName);
            if ((this.BodyAttachment == null) == null)
            {
                goto Label_013B;
            }
            Debug.LogError("Failed to load " + job.BodyAttachmentName);
            goto Label_029A;
        Label_013B:
            if (string.IsNullOrEmpty(job.HeadName) != null)
            {
                goto Label_0191;
            }
            this.Head = Resources.Load<GameObject>("CH/HEAD/" + job.HeadName);
            if ((this.Head == null) == null)
            {
                goto Label_0191;
            }
            Debug.LogError("Failed to load " + job.HeadName);
            goto Label_029A;
        Label_0191:
            if (string.IsNullOrEmpty(job.HairName) != null)
            {
                goto Label_01E7;
            }
            this.Hair = Resources.Load<GameObject>("CH/HAIR/" + job.HairName);
            if ((this.Hair == null) == null)
            {
                goto Label_01E7;
            }
            Debug.LogError("Failed to load " + job.HairName);
            goto Label_029A;
        Label_01E7:
            if (string.IsNullOrEmpty(job.HeadAttachmentName) != null)
            {
                goto Label_023D;
            }
            this.HeadAttachment = Resources.Load<GameObject>("CH/HEADOPT/" + job.HeadAttachmentName);
            if ((this.HeadAttachment == null) == null)
            {
                goto Label_023D;
            }
            Debug.LogError("Failed to load " + job.HeadAttachmentName);
            goto Label_029A;
        Label_023D:
            this.HairColor0 = job.HairColor0;
            this.HairColor1 = job.HairColor1;
            return;
        Label_0256:
            this.Body = Resources.Load<GameObject>("Units/" + SRPG_Extensions.ToPrefix(sex) + characterID);
            if ((this.Body != null) == null)
            {
                goto Label_0284;
            }
            return;
        Label_0284:
            Debug.LogError("Failed to load " + SRPG_Extensions.ToPrefix(sex) + characterID);
        Label_029A:
            this.Body = Resources.Load<GameObject>("Units/NULL");
            this.BodyAttachment = null;
            this.BodyTexture = null;
            this.Head = null;
            this.HeadAttachment = null;
            this.Hair = null;
            return;
        }

        public GameObject Compose(Vector3 position, Quaternion rotation)
        {
            GameObject obj2;
            Transform transform;
            SkinnedMeshRenderer renderer;
            Material material;
            Transform transform2;
            Transform transform3;
            GameObject obj3;
            Transform transform4;
            GameObject obj4;
            Renderer[] rendererArray;
            int num;
            Material material2;
            Transform transform5;
            GameObject obj5;
            if ((this.Body == null) == null)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            obj2 = Object.Instantiate(this.Body, position, rotation * this.Body.get_transform().get_rotation()) as GameObject;
            transform = obj2.get_transform();
            if ((this.BodyTexture != null) == null)
            {
                goto Label_0085;
            }
            renderer = obj2.GetComponentInChildren<SkinnedMeshRenderer>();
            if ((renderer != null) == null)
            {
                goto Label_0085;
            }
            material = new Material(renderer.get_sharedMaterial());
            material.set_mainTexture(this.BodyTexture);
            renderer.set_sharedMaterial(material);
        Label_0085:
            transform2 = GameUtility.findChildRecursively(transform, "Bip001 Head");
            if ((transform2 != null) == null)
            {
                goto Label_022F;
            }
            if ((this.Head != null) == null)
            {
                goto Label_00EC;
            }
            transform3 = this.Head.get_transform();
            obj3 = Object.Instantiate(this.Head, transform3.get_localPosition(), transform3.get_localRotation()) as GameObject;
            obj3.get_transform().SetParent(transform2, 0);
        Label_00EC:
            if ((this.Hair != null) == null)
            {
                goto Label_01E2;
            }
            transform4 = this.Hair.get_transform();
            obj4 = Object.Instantiate(this.Hair, transform4.get_localPosition(), transform4.get_localRotation()) as GameObject;
            obj4.get_transform().SetParent(transform2, 0);
            rendererArray = obj4.GetComponentsInChildren<Renderer>();
            num = 0;
            goto Label_01D7;
        Label_014A:
            if ((rendererArray[num] as MeshRenderer) != null)
            {
                goto Label_0168;
            }
            if ((rendererArray[num] as SkinnedMeshRenderer) == null)
            {
                goto Label_01D1;
            }
        Label_0168:
            if ((rendererArray[num].get_sharedMaterial() != null) == null)
            {
                goto Label_01D1;
            }
            material2 = new Material(rendererArray[num].get_sharedMaterial());
            material2.set_hideFlags(0x34);
            material2.SetColor("_hairColor0", this.HairColor0);
            material2.SetColor("_hairColor1", this.HairColor1);
            rendererArray[num].set_sharedMaterial(material2);
        Label_01D1:
            num += 1;
        Label_01D7:
            if (num < ((int) rendererArray.Length))
            {
                goto Label_014A;
            }
        Label_01E2:
            if ((this.HeadAttachment != null) == null)
            {
                goto Label_022F;
            }
            transform5 = this.HeadAttachment.get_transform();
            obj5 = Object.Instantiate(this.HeadAttachment, transform5.get_localPosition(), transform5.get_localRotation()) as GameObject;
            obj5.get_transform().SetParent(transform2, 0);
        Label_022F:
            return obj2;
        }
    }
}

