using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class CampaignController : SingletonListController<Campaign>
   {
      public CampaignController() : base(ConfigController.CampaignResourcePath(),
                                       (x) => Campaign.Deserialise(x))
      {
         
      }
      
      public static CampaignController GetInstance()
      {
         if (_instance == null)
         {_instance = new CampaignController();}
         return (CampaignController)_instance;
      }
      
      public Campaign AddCampaign(string name, IList<MissionHeader> missionTemplate)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Campaign name cannot be null or empty");}
         
         return base.AddT((x) => new Campaign(x, name, missionTemplate));
      }
      
   }
}

