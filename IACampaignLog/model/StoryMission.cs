using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class StoryMission : Identifiable, ISerialisable
   {
      public StoryMission (int id, string name, Campaign missionCampaign) : base(id, name)
      {
         MissionCampaign = missionCampaign;
      }
      
      public Campaign MissionCampaign {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("StoryMission");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("campaign", this.MissionCampaign.Id);
         return x;
      }
      
      public static StoryMission Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         Campaign missionCampaign = CampaignController.GetInstance().FindWithId(int.Parse(toObject.Attribute("campaign").Value));
         StoryMission newObj = new StoryMission(id, name, missionCampaign);
         return newObj;
      }
   }
}

