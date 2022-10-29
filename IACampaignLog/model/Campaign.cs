using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace IACampaignLog
{
   public class Campaign : Identifiable, ISerialisable
   {
      public Campaign (int id, string name) : this(id, name, new List<MissionHeader>())
      {
      }
      
      public Campaign (int id, string name, IList<MissionHeader> missionTemplate) : base(id, name)
      {
         MissionTemplate = missionTemplate;
      }
      
      public bool AllowSideMissions {get; set;}
      public bool AllowForcedMissions {get; set;}
      public bool IncludeThreatMissions {get; set;}
      public int StartingRebelXP {get; set;}
      public int StartingImperialXP {get; set;}
      public int StartingCredits {get; set;}
      public int StartingInfluence {get; set;}
      public IList<MissionHeader> MissionTemplate {get; set;}
      
      public IList<MissionHeader> CloneMissionTemplate()
      {
         return (from MissionHeader m in MissionTemplate
                 select m.Clone()).ToList();
      }
      
      public XElement Serialise()
      {
         XElement x = new XElement("Campaign");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("allowsidemissions", this.AllowSideMissions);
         x.SetAttributeValue("allowforcedmissions", this.AllowForcedMissions);
         x.SetAttributeValue("includethreatmissions", this.IncludeThreatMissions);
         x.SetAttributeValue("startingcredits", this.StartingCredits);
         x.SetAttributeValue("startingimpxp", this.StartingImperialXP);
         x.SetAttributeValue("startinginfluence", this.StartingInfluence);
         x.SetAttributeValue("startingrebelxp", this.StartingRebelXP);
         XElement missionsElem = new XElement("MissionTemplate");
         foreach (MissionHeader m in MissionTemplate) {missionsElem.Add(m.Serialise());}
         x.Add(missionsElem);
         return x;
      }

      private static bool ParseBoolAttributeValue(string attributeName, XElement xmlWithAttribute, bool defaultValue = true)
      {
         return xmlWithAttribute.Attribute(attributeName) != null ? bool.Parse(xmlWithAttribute.Attribute(attributeName).Value) : defaultValue;
      }
      
      private static int ParseIntAttributeValue(string attributeName, XElement xmlWithAttribute, int defaultValue = 0)
      {
         return xmlWithAttribute.Attribute(attributeName) != null ? int.Parse(xmlWithAttribute.Attribute(attributeName).Value) : defaultValue;
      }
      
      public static Campaign Deserialise(XElement toObject)
      {
         int id = ParseIntAttributeValue("id", toObject);
         string name = toObject.Attribute("name").Value;
         bool allowSides = ParseBoolAttributeValue("allowsidemissions", toObject);
         bool allowForced = ParseBoolAttributeValue("allowforcedmissions", toObject);
         bool includeThreat = ParseBoolAttributeValue("includethreatmissions", toObject, false);
         int startingCredits = ParseIntAttributeValue("startingcredits", toObject);
         int startingImpXp = ParseIntAttributeValue("startingimpxp", toObject);
         int startingInfluence = ParseIntAttributeValue("startinginfluence", toObject);
         int startingRebelXp = ParseIntAttributeValue("startingrebelxp", toObject);
         IList<MissionHeader> missions = (from XElement x in toObject.Element("MissionTemplate").Elements("Mission")
                                          select MissionHeader.Deserialise(x)).ToList();
         Campaign newObj = new Campaign(id, name, missions){
            AllowSideMissions = allowSides,
            AllowForcedMissions = allowForced,
            IncludeThreatMissions = includeThreat,
            StartingCredits = startingCredits,
            StartingImperialXP = startingImpXp,
            StartingInfluence = startingInfluence,
            StartingRebelXP = startingRebelXp};
         
         return newObj;
      }
   }
}

