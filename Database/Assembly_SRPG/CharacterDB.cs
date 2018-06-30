namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class CharacterDB
    {
        public const string BodyPath = "CH/BODY/";
        public const string BodyTexturePath = "CH/BODYTEX/";
        public const string HeadPath = "CH/HEAD/";
        public const string HairPath = "CH/HAIR/";
        public const string HeadAttachmentPath = "CH/HEADOPT/";
        public const string BodyAttachmentPath = "CH/BODYOPT/";
        public static readonly string DatabasePath;
        private static List<Character> mCharacters;
        private static bool mDBLoaded;

        static CharacterDB()
        {
            DatabasePath = "Data/CHDB";
            mCharacters = new List<Character>();
            return;
        }

        public CharacterDB()
        {
            base..ctor();
            return;
        }

        public static GameObject ComposeCharacter(string characterID, string jobID)
        {
            string[] textArray1;
            Character character;
            int num;
            character = FindCharacter(characterID);
            if (character != null)
            {
                goto Label_0024;
            }
            Debug.LogError("Character '" + characterID + "' not found.");
            return null;
        Label_0024:
            num = 0;
            goto Label_004B;
        Label_002B:
            if ((character.Jobs[num].JobID == jobID) == null)
            {
                goto Label_0047;
            }
        Label_0047:
            num += 1;
        Label_004B:
            if (num < character.Jobs.Count)
            {
                goto Label_002B;
            }
            textArray1 = new string[] { "Character '", characterID, "'can't be '", jobID, "'." };
            Debug.LogError(string.Concat(textArray1));
            return null;
        }

        public static Character FindCharacter(string characterID)
        {
            int num;
            int num2;
            LoadDatabase();
            num = characterID.GetHashCode();
            num2 = mCharacters.Count - 1;
            goto Label_005F;
        Label_001E:
            if (mCharacters[num2].HashID != num)
            {
                goto Label_005B;
            }
            if ((mCharacters[num2].CharacterID == characterID) == null)
            {
                goto Label_005B;
            }
            return mCharacters[num2];
        Label_005B:
            num2 -= 1;
        Label_005F:
            if (num2 >= 0)
            {
                goto Label_001E;
            }
            return null;
        }

        public static Job FindCharacter(string characterID, string jobResourceID)
        {
            Character character;
            int num;
            character = FindCharacter(characterID);
            if (character != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            if (string.IsNullOrEmpty(jobResourceID) == null)
            {
                goto Label_0021;
            }
            jobResourceID = "none";
        Label_0021:
            num = 0;
            goto Label_0055;
        Label_0028:
            if ((character.Jobs[num].JobID == jobResourceID) == null)
            {
                goto Label_0051;
            }
            return character.Jobs[num];
        Label_0051:
            num += 1;
        Label_0055:
            if (num < character.Jobs.Count)
            {
                goto Label_0028;
            }
            return character.Jobs[0];
        }

        public static unsafe void LoadDatabase()
        {
            char[] chArray1;
            string str;
            char[] chArray;
            StringReader reader;
            string str2;
            string[] strArray;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            string[] strArray2;
            Character character;
            Job job;
            int num13;
            if (mDBLoaded == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            mDBLoaded = 1;
            str = null;
            str = AssetManager.LoadTextData(DatabasePath);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0034;
            }
            Debug.LogError("Failed to load CharacterDB");
            return;
        Label_0034:
            chArray1 = new char[] { 9 };
            chArray = chArray1;
            mCharacters.Clear();
            reader = new StringReader(str);
        Label_0051:
            try
            {
                strArray = null;
                str2 = reader.ReadLine();
                if (string.IsNullOrEmpty(str2) == null)
                {
                    goto Label_006B;
                }
                goto Label_0248;
            Label_006B:
                strArray = str2.Split(chArray);
                num = Array.IndexOf<string>(strArray, "ID");
                num2 = Array.IndexOf<string>(strArray, "JOB");
                num3 = Array.IndexOf<string>(strArray, "BODY");
                num4 = Array.IndexOf<string>(strArray, "BODY_TEXTURE");
                num5 = Array.IndexOf<string>(strArray, "BODY_ATTACHMENT");
                num6 = Array.IndexOf<string>(strArray, "HEAD");
                num7 = Array.IndexOf<string>(strArray, "HEAD_ATTACHMENT");
                num8 = Array.IndexOf<string>(strArray, "HAIR");
                num9 = Array.IndexOf<string>(strArray, "HAIR_COLOR1");
                num10 = Array.IndexOf<string>(strArray, "HAIR_COLOR2");
                num11 = Array.IndexOf<string>(strArray, "PREFIX");
                num12 = Array.IndexOf<string>(strArray, "MOVABLE");
                goto Label_0229;
            Label_0121:
                strArray2 = str2.Split(chArray);
                if (str2.Length > 0)
                {
                    goto Label_013B;
                }
                goto Label_0229;
            Label_013B:
                character = ReserveCharacter(strArray2[num]);
                job = new Job();
                job.JobID = strArray2[num2];
                job.HashID = job.JobID.GetHashCode();
                job.HairName = strArray2[num8];
                job.BodyName = strArray2[num3];
                job.BodyTextureName = strArray2[num4];
                job.BodyAttachmentName = strArray2[num5];
                job.HeadName = strArray2[num6];
                job.HeadAttachmentName = strArray2[num7];
                job.HairColor0 = GameUtility.ParseColor(strArray2[num9]);
                job.HairColor1 = GameUtility.ParseColor(strArray2[num10]);
                job.Movable = 1;
                if (num11 == -1)
                {
                    goto Label_01F3;
                }
                job.AssetPrefix = strArray2[num11];
            Label_01F3:
                if (num12 == -1)
                {
                    goto Label_021B;
                }
                if (int.TryParse(strArray2[num12], &num13) == null)
                {
                    goto Label_021B;
                }
                job.Movable = (num13 == 0) == 0;
            Label_021B:
                character.Jobs.Add(job);
            Label_0229:
                if ((str2 = reader.ReadLine()) != null)
                {
                    goto Label_0121;
                }
                goto Label_0248;
            }
            finally
            {
            Label_023B:
                if (reader == null)
                {
                    goto Label_0247;
                }
                reader.Dispose();
            Label_0247:;
            }
        Label_0248:
            return;
        }

        public static void ReloadDatabase()
        {
            UnloadAll();
            LoadDatabase();
            return;
        }

        public static Character ReserveCharacter(string characterID)
        {
            Character character;
            character = FindCharacter(characterID);
            if (character != null)
            {
                goto Label_001F;
            }
            character = new Character(characterID);
            mCharacters.Add(character);
        Label_001F:
            return character;
        }

        private static unsafe string SerializeColor(Color32 color)
        {
            object[] objArray1;
            objArray1 = new object[] { (byte) &color.r, ",", (byte) &color.g, ",", (byte) &color.b };
            return string.Concat(objArray1);
        }

        public static void UnloadAll()
        {
            mDBLoaded = 0;
            mCharacters.Clear();
            return;
        }

        [Serializable]
        public class Character
        {
            public int HashID;
            public string CharacterID;
            public List<CharacterDB.Job> Jobs;

            public Character(string characterID)
            {
                this.Jobs = new List<CharacterDB.Job>();
                base..ctor();
                this.CharacterID = characterID;
                this.HashID = this.CharacterID.GetHashCode();
                return;
            }

            public int IndexOfJob(string jobID)
            {
                int num;
                num = 0;
                goto Label_0029;
            Label_0007:
                if ((this.Jobs[num].JobID == jobID) == null)
                {
                    goto Label_0025;
                }
                return num;
            Label_0025:
                num += 1;
            Label_0029:
                if (num < this.Jobs.Count)
                {
                    goto Label_0007;
                }
                return -1;
            }
        }

        [Serializable]
        public class Job
        {
            public int HashID;
            public string JobID;
            public string AssetPrefix;
            [StringIsResourcePath(typeof(GameObject), "CH/BODY/")]
            public string BodyName;
            [StringIsResourcePath(typeof(GameObject), "CH/BODYOPT/")]
            public string BodyAttachmentName;
            [StringIsResourcePath(typeof(Texture2D), "CH/BODYTEX/")]
            public string BodyTextureName;
            [StringIsResourcePath(typeof(GameObject), "CH/HEAD/")]
            public string HeadName;
            [StringIsResourcePath(typeof(GameObject), "CH/HAIR/")]
            public string HairName;
            [StringIsResourcePath(typeof(GameObject), "CH/HEADOPT/")]
            public string HeadAttachmentName;
            public Color32 HairColor0;
            public Color32 HairColor1;
            public bool Movable;

            public Job()
            {
                base..ctor();
                return;
            }

            public Job(string jobID)
            {
                base..ctor();
                this.JobID = jobID;
                this.HashID = this.JobID.GetHashCode();
                this.AssetPrefix = null;
                this.BodyName = null;
                this.BodyAttachmentName = null;
                this.BodyTextureName = null;
                this.HeadName = null;
                this.HairName = null;
                this.HeadAttachmentName = null;
                this.HairColor0 = new Color32(0, 0, 0, 0xff);
                this.HairColor1 = this.HairColor0;
                this.Movable = 1;
                return;
            }
        }
    }
}

