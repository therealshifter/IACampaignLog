using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace IACampaignLog
{
   public class Mission : MissionHeader, ISerialisable
   {
      public Mission (MissionType type, int threatLevel) : base(type, threatLevel)
      {
         IsCompleted = false;
         RebelUpgradeCompleted = false;
         ImperialUpgradeCompleted = false;
         RebelXPAwarded = 0;
         CreditsAwarded = 0;
         ImperialXpAwarded = 0;
         InfluenceAwarded = 0;
      }
      
      public bool IsCompleted {get; set;}
      public bool RebelUpgradeCompleted {get; set;}
      public bool ImperialUpgradeCompleted {get; set;}
      public int RebelXPAwarded {get; set;}
      public int CreditsAwarded {get; set;}
      public int ImperialXpAwarded {get; set;}
      public int InfluenceAwarded {get;set;}
      
      public static IList<Mission> ToMissionList(IList<MissionHeader> input)
      {
         return (from MissionHeader m in input
                 select new Mission(m.Type, m.ThreatLevel){Name = m.Name, AvailableItemTiers = m.AvailableItemTiers}).ToList();
      }
      
      public override XElement Serialise()
      {
         XElement x = base.Serialise();
         x.SetAttributeValue("completed", this.IsCompleted);
         x.SetAttributeValue("rebelupgrade", this.RebelUpgradeCompleted);
         x.SetAttributeValue("imperialupgrade", this.ImperialUpgradeCompleted);
         x.SetAttributeValue("rebelxp", this.RebelXPAwarded);
         x.SetAttributeValue("imperialxp", this.ImperialXpAwarded);
         x.SetAttributeValue("credits", this.CreditsAwarded);
         x.SetAttributeValue("influence", this.InfluenceAwarded);
         return x;
      }
      
      public static new Mission Deserialise(XElement toObject)
      {
         MissionHeader head = MissionHeader.Deserialise(toObject);
         Mission newObj = new Mission(head.Type, head.ThreatLevel);
         newObj.Name = head.Name;
         newObj.AvailableItemTiers = head.AvailableItemTiers;
         newObj.IsCompleted = bool.Parse(toObject.Attribute("completed").Value);
         newObj.RebelUpgradeCompleted = bool.Parse(toObject.Attribute("rebelupgrade").Value);
         newObj.ImperialUpgradeCompleted = bool.Parse(toObject.Attribute("imperialupgrade").Value);
         newObj.RebelXPAwarded = int.Parse(toObject.Attribute("rebelxp").Value);
         newObj.ImperialXpAwarded = int.Parse(toObject.Attribute("imperialxp").Value);
         newObj.CreditsAwarded = int.Parse(toObject.Attribute("credits").Value);
         newObj.InfluenceAwarded = int.Parse(toObject.Attribute("influence").Value);
         return newObj;
      }
   }
}

