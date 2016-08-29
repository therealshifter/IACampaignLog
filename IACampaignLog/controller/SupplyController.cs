using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class SupplyController : SingletonListController<Supply>
   {
      public SupplyController() : base(ConfigController.SupplyResourcePath(),
                                       (x) => Supply.Deserialise(x))
      {
         
      }
      
      public static SupplyController GetInstance()
      {
         if (_instance == null)
         {_instance = new SupplyController();}
         return (SupplyController)_instance;
      }
      
      public Supply AddSupply(string name)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Supply name cannot be null or empty");}
         
         return base.AddT((x) => new Supply(x, name));
      }
      
   }
}

