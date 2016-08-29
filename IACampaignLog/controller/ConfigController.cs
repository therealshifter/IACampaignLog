using System;
using System.Configuration;

namespace IACampaignLog
{
   public static class ConfigController
   {
      static ConfigController()
      {
         //ConfigurationManager.OpenExeConfiguration()
      }
      
      public static string AgendaResourcePath()
      {
         return ConfigurationManager.AppSettings["AgendaResPath"];
      }
      
      public static string CampaignResourcePath()
      {
         return ConfigurationManager.AppSettings["CampaignResPath"];
      }
      
      public static string CharacterResourcePath()
      {
         return ConfigurationManager.AppSettings["CharacterResPath"];
      }
      
      public static string ClassResourcePath()
      {
         return ConfigurationManager.AppSettings["ClassResPath"];
      }
      
      public static string ItemResourcePath()
      {
         return ConfigurationManager.AppSettings["ItemResPath"];
      }
      
      public static string RewardResourcePath()
      {
         return ConfigurationManager.AppSettings["RewardResPath"];
      }
      
      public static string SupplyResourcePath()
      {
         return ConfigurationManager.AppSettings["SupplyResPath"];
      }
      
      public static string SideMissionResourcePath()
      {
         return ConfigurationManager.AppSettings["SideMissionResPath"];
      }
      
      public static string StoryMissionResourcePath()
      {
         return ConfigurationManager.AppSettings["StoryMissionResPath"];
      }
      
      public static string GameSavePath()
      {
         return ConfigurationManager.AppSettings["GameSavePath"];
      }
   }
}

