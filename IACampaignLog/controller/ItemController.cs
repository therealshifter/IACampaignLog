using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class ItemController : SingletonListController<Item>
   {
      public ItemController() : base (ConfigController.ItemResourcePath(),
                                      (x) => Item.Deserialise(x))
      {
         
      }
      
      public static ItemController GetInstance()
      {
         if (_instance == null)
         {_instance = new ItemController();}
         return (ItemController)_instance;
      }
      
      public Item FindWithNameInTier(string name, Item.ItemTier tier)
      {
         return (from Item i in FindWithName(name)
                 where i.Tier == tier
                 select i).SingleOrDefault();
      }
      
      public Item AddItem(string name, int creditCost, Item.ItemTier tier)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Item name cannot be null or empty");}
         
         return base.AddT((x) => new Item(x, name, creditCost, tier));
      }
      
   }
}

