using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class StoryMissionController : SingletonListController<StoryMission>
   {
      public StoryMissionController() : base(ConfigController.StoryMissionResourcePath(),
                                       (x) => StoryMission.Deserialise(x))
      {
         
      }
      
      public static StoryMissionController GetInstance()
      {
         if (_instance == null)
         {_instance = new StoryMissionController();}
         return (StoryMissionController)_instance;
      }
      
      public IList<StoryMission> StoryMissionsForCampaign(int campaignId)
      {
         return ListOfT.Where((x) => x.MissionCampaign.Id == campaignId).ToList();
      }
      
      public StoryMission AddStoryMission(string name, Campaign missionCampaign)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Story Mission name cannot be null or empty");}
         
         return base.AddT((x) => new StoryMission(x, name, missionCampaign));
      }
      
   }
}

