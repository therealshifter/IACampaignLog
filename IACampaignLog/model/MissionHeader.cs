using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class MissionHeader : ISerialisable
   {
      public enum MissionType
      {
         Story,
         Side,
         Forced
      }
      
      public MissionHeader (MissionType type, int threatLevel)
      {
         Type = type;
         ThreatLevel = threatLevel;
      }
      
      public int ThreatLevel {get; set;}
      public MissionType Type {get; set;}
      public string Name {get; set;}
      public Item.ItemTier AvailableItemTiers {get; set;}
      
      public MissionHeader Clone()
      {
         MissionHeader newHeader = new MissionHeader(this.Type, this.ThreatLevel);
         newHeader.Name = this.Name;
         newHeader.AvailableItemTiers = this.AvailableItemTiers;
         return newHeader;
      }
      
      public virtual XElement Serialise()
      {
         XElement x = new XElement("Mission");
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("type", this.Type);
         x.SetAttributeValue("threatlevel", this.ThreatLevel);
         x.SetAttributeValue("itemtiers", this.AvailableItemTiers);
         return x;
      }
      
      public static MissionHeader Deserialise(XElement toObject)
      {
         MissionType type = (MissionType)Enum.Parse(typeof(MissionType), toObject.Attribute("type").Value);
         int threatLevel = int.Parse(toObject.Attribute("threatlevel").Value);
         Item.ItemTier tiers = Item.ItemTier.I;
         if (toObject.Attribute("itemtiers") != null)
            tiers = (Item.ItemTier)Enum.Parse(typeof(Item.ItemTier), toObject.Attribute("itemtiers").Value);
         MissionHeader newObj = new MissionHeader(type, threatLevel);
         newObj.AvailableItemTiers = tiers;
         if (toObject.Attribute("name") != null)
            newObj.Name = toObject.Attribute("name").Value;
         return newObj;
      }
   }
}

