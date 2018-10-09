using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class SideMissionController : SingletonListController<SideMission>
   {
      public SideMissionController() : base(ConfigController.SideMissionResourcePath(),
                                       (x) =>
                                       {
                                          if (x.Name == "ThreatMission")
                                             return ThreatMission.Deserialise(x);
                                          else
                                             return SideMission.Deserialise(x);
                                       })
      {
         
      }
      
      public static SideMissionController GetInstance()
      {
         if (_instance == null)
         {_instance = new SideMissionController();}
         return (SideMissionController)_instance;
      }
      
      public SideMission AddSideMission(string name, SideMission.MissionType missionType)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Side Mission name cannot be null or empty");}
         
         return base.AddT((x) => new SideMission(x, name, missionType));
      }

        public SideMission AddThreatMission(string name, Reward baneReward)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Threat Mission name cannot be null or empty");
            }
            if (baneReward == null)
            {
                throw new ArgumentNullException("baneReward");
            }

            return base.AddT((x) => new ThreatMission(x, name, baneReward));
        }
      
      public IList<SideMission> SideMissionsOfType(SideMission.MissionType mType)
      {
         return ListOfT.Where((x) => x.SideMissionType == mType).ToList();
      }
      
      public IList<SideMission> SideMissionsNotOfType(SideMission.MissionType mType)
      {
         return ListOfT.Where((x) => x.SideMissionType != mType).ToList();
      }

      public IList<ThreatMission> ThreatMissions()
      {
         return (from SideMission sm in this.ListOfT
                 where sm is ThreatMission
                 select (ThreatMission)sm).ToList();
      }
   }
}

