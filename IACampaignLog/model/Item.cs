using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Item : Identifiable, ISerialisable
   {
      [FlagsAttribute]
      public enum ItemTier
      {
         I = 1,
         II = 2,
         III = 4
      }
      
      public Item (int id, string name, int creditCost, ItemTier tier) : base(id, name)
      {
         CreditCost = creditCost;
         Tier = tier;
      }
      
      public int CreditCost {get; set;}
      public ItemTier Tier {get; set;}
      public string NameWithTier {get{return string.IsNullOrWhiteSpace(Name) ? Name : Name + " (Tier " + Tier + ")";}}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Item");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("cost", this.CreditCost);
         x.SetAttributeValue("tier", this.Tier);
         return x;
      }
      
      public static Item Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         int cost = int.Parse(toObject.Attribute("cost").Value);
         ItemTier tier = (ItemTier)Enum.Parse(typeof(ItemTier), toObject.Attribute("tier").Value);
         Item newObj = new Item(id, name, cost, tier);
         return newObj;
      }
   }
}

