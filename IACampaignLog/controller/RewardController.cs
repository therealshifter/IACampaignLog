using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class RewardController : SingletonListController<Reward>
   {
      public RewardController() : base(ConfigController.RewardResourcePath(),
                                       (x) => Reward.Deserialise(x))
      {
         
      }
      
      public static RewardController GetInstance()
      {
         if (_instance == null)
         {_instance = new RewardController();}
         return (RewardController)_instance;
      }
      
      public Reward AddReward(string name, Reward.RewardType rType)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Reward name cannot be null or empty");}
         
         return base.AddT((x) => new Reward(x, name, rType));
      }

      public IList<Reward> RewardsOfType(Reward.RewardType rType)
      {
         return ListOfT.Where((x) => x.RewardSubType == rType).ToList();
      }
      
   }
}

