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
         x.SetAttributeValue("startingcredits", this.StartingCredits);
         x.SetAttributeValue("startingimpxp", this.StartingImperialXP);
         x.SetAttributeValue("startinginfluence", this.StartingInfluence);
         x.SetAttributeValue("startingrebelxp", this.StartingRebelXP);
         XElement missionsElem = new XElement("MissionTemplate");
         foreach (MissionHeader m in MissionTemplate) {missionsElem.Add(m.Serialise());}
         x.Add(missionsElem);
         return x;
      }
      
      public static Campaign Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         bool allowSides = bool.Parse(toObject.Attribute("allowsidemissions").Value);
         bool allowForced = bool.Parse(toObject.Attribute("allowforcedmissions").Value);
         int startingCredits = int.Parse(toObject.Attribute("startingcredits").Value);
         int startingImpXp = int.Parse(toObject.Attribute("startingimpxp").Value);
         int startingInfluence = int.Parse(toObject.Attribute("startinginfluence").Value);
         int startingRebelXp = int.Parse(toObject.Attribute("startingrebelxp").Value);
         IList<MissionHeader> missions = (from XElement x in toObject.Element("MissionTemplate").Elements("Mission")
                                          select MissionHeader.Deserialise(x)).ToList();
         Campaign newObj = new Campaign(id, name, missions){
            AllowSideMissions = allowSides,
            AllowForcedMissions = allowForced,
            StartingCredits = startingCredits,
            StartingImperialXP = startingImpXp,
            StartingInfluence = startingInfluence,
            StartingRebelXP = startingRebelXp};
         
         return newObj;
      }
   }
}

