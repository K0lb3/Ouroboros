// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterDB
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SRPG
{
  public class CharacterDB
  {
    public static readonly string DatabasePath = "Data/CHDB";
    private static List<CharacterDB.Character> mCharacters = new List<CharacterDB.Character>();
    public const string BodyPath = "CH/BODY/";
    public const string BodyTexturePath = "CH/BODYTEX/";
    public const string HeadPath = "CH/HEAD/";
    public const string HairPath = "CH/HAIR/";
    public const string HeadAttachmentPath = "CH/HEADOPT/";
    public const string BodyAttachmentPath = "CH/BODYOPT/";
    private static bool mDBLoaded;

    public static GameObject ComposeCharacter(string characterID, string jobID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
      {
        Debug.LogError((object) ("Character '" + characterID + "' not found."));
        return (GameObject) null;
      }
      for (int index = 0; index < character.Jobs.Count; ++index)
      {
        if (!(character.Jobs[index].JobID == jobID))
          ;
      }
      Debug.LogError((object) ("Character '" + characterID + "'can't be '" + jobID + "'."));
      return (GameObject) null;
    }

    public static void UnloadAll()
    {
      CharacterDB.mDBLoaded = false;
      CharacterDB.mCharacters.Clear();
    }

    public static void ReloadDatabase()
    {
      CharacterDB.UnloadAll();
      CharacterDB.LoadDatabase();
    }

    public static CharacterDB.Character ReserveCharacter(string characterID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
      {
        character = new CharacterDB.Character(characterID);
        CharacterDB.mCharacters.Add(character);
      }
      return character;
    }

    public static CharacterDB.Character FindCharacter(string characterID)
    {
      CharacterDB.LoadDatabase();
      int hashCode = characterID.GetHashCode();
      for (int index = CharacterDB.mCharacters.Count - 1; index >= 0; --index)
      {
        if (CharacterDB.mCharacters[index].HashID == hashCode && CharacterDB.mCharacters[index].CharacterID == characterID)
          return CharacterDB.mCharacters[index];
      }
      return (CharacterDB.Character) null;
    }

    public static CharacterDB.Job FindCharacter(string characterID, string jobResourceID)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(characterID);
      if (character == null)
        return (CharacterDB.Job) null;
      if (string.IsNullOrEmpty(jobResourceID))
        jobResourceID = "none";
      for (int index = 0; index < character.Jobs.Count; ++index)
      {
        if (character.Jobs[index].JobID == jobResourceID)
          return character.Jobs[index];
      }
      return character.Jobs[0];
    }

    private static string SerializeColor(Color32 color)
    {
      return color.r.ToString() + "," + (object) (byte) color.g + "," + (object) (byte) color.b;
    }

    public static void LoadDatabase()
    {
      if (CharacterDB.mDBLoaded)
        return;
      CharacterDB.mDBLoaded = true;
      string s = AssetManager.LoadTextData(CharacterDB.DatabasePath);
      if (string.IsNullOrEmpty(s))
      {
        Debug.LogError((object) "Failed to load CharacterDB");
      }
      else
      {
        char[] chArray = new char[1]{ '\t' };
        CharacterDB.mCharacters.Clear();
        using (StringReader stringReader = new StringReader(s))
        {
          string str1 = stringReader.ReadLine();
          if (string.IsNullOrEmpty(str1))
            return;
          string[] array = str1.Split(chArray);
          int index1 = Array.IndexOf<string>(array, "ID");
          int index2 = Array.IndexOf<string>(array, "JOB");
          int index3 = Array.IndexOf<string>(array, "BODY");
          int index4 = Array.IndexOf<string>(array, "BODY_TEXTURE");
          int index5 = Array.IndexOf<string>(array, "BODY_ATTACHMENT");
          int index6 = Array.IndexOf<string>(array, "HEAD");
          int index7 = Array.IndexOf<string>(array, "HEAD_ATTACHMENT");
          int index8 = Array.IndexOf<string>(array, "HAIR");
          int index9 = Array.IndexOf<string>(array, "HAIR_COLOR1");
          int index10 = Array.IndexOf<string>(array, "HAIR_COLOR2");
          int index11 = Array.IndexOf<string>(array, "PREFIX");
          int index12 = Array.IndexOf<string>(array, "MOVABLE");
          string str2;
          while ((str2 = stringReader.ReadLine()) != null)
          {
            string[] strArray = str2.Split(chArray);
            if (str2.Length > 0)
            {
              CharacterDB.Character character = CharacterDB.ReserveCharacter(strArray[index1]);
              CharacterDB.Job job = new CharacterDB.Job();
              job.JobID = strArray[index2];
              job.HashID = job.JobID.GetHashCode();
              job.HairName = strArray[index8];
              job.BodyName = strArray[index3];
              job.BodyTextureName = strArray[index4];
              job.BodyAttachmentName = strArray[index5];
              job.HeadName = strArray[index6];
              job.HeadAttachmentName = strArray[index7];
              job.HairColor0 = GameUtility.ParseColor(strArray[index9]);
              job.HairColor1 = GameUtility.ParseColor(strArray[index10]);
              job.Movable = true;
              if (index11 != -1)
                job.AssetPrefix = strArray[index11];
              int result;
              if (index12 != -1 && int.TryParse(strArray[index12], out result))
                job.Movable = result != 0;
              character.Jobs.Add(job);
            }
          }
        }
      }
    }

    [Serializable]
    public class Job
    {
      public int HashID;
      public string JobID;
      public string AssetPrefix;
      [StringIsResourcePath(typeof (GameObject), "CH/BODY/")]
      public string BodyName;
      [StringIsResourcePath(typeof (GameObject), "CH/BODYOPT/")]
      public string BodyAttachmentName;
      [StringIsResourcePath(typeof (Texture2D), "CH/BODYTEX/")]
      public string BodyTextureName;
      [StringIsResourcePath(typeof (GameObject), "CH/HEAD/")]
      public string HeadName;
      [StringIsResourcePath(typeof (GameObject), "CH/HAIR/")]
      public string HairName;
      [StringIsResourcePath(typeof (GameObject), "CH/HEADOPT/")]
      public string HeadAttachmentName;
      public Color32 HairColor0;
      public Color32 HairColor1;
      public bool Movable;

      public Job()
      {
      }

      public Job(string jobID)
      {
        this.JobID = jobID;
        this.HashID = this.JobID.GetHashCode();
        this.AssetPrefix = (string) null;
        this.BodyName = (string) null;
        this.BodyAttachmentName = (string) null;
        this.BodyTextureName = (string) null;
        this.HeadName = (string) null;
        this.HairName = (string) null;
        this.HeadAttachmentName = (string) null;
        this.HairColor0 = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
        this.HairColor1 = this.HairColor0;
        this.Movable = true;
      }
    }

    [Serializable]
    public class Character
    {
      public List<CharacterDB.Job> Jobs = new List<CharacterDB.Job>();
      public int HashID;
      public string CharacterID;

      public Character(string characterID)
      {
        this.CharacterID = characterID;
        this.HashID = this.CharacterID.GetHashCode();
      }

      public int IndexOfJob(string jobID)
      {
        for (int index = 0; index < this.Jobs.Count; ++index)
        {
          if (this.Jobs[index].JobID == jobID)
            return index;
        }
        return -1;
      }
    }
  }
}
