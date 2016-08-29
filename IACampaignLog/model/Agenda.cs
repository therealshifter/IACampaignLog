using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Agenda : Identifiable, ISerialisable
   {
      public enum AgendaType
      {
         SideMission,
         ForcedMission,
         Ongoing,
         Secret
      }
      
      public Agenda (int id, string name, int influenceCost, AgendaType agendaType) : base(id, name)
      {
         InfluenceCost = influenceCost;
         AgendaCardType = agendaType;
      }
      
      public int InfluenceCost {get; set;}
      public AgendaType AgendaCardType {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Agenda");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("cost", this.InfluenceCost);
         x.SetAttributeValue("agendatype", this.AgendaCardType);
         return x;
      }
      
      public static Agenda Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         int cost = int.Parse(toObject.Attribute("cost").Value);
         AgendaType agendaType = AgendaType.Secret;
         if (toObject.Attribute("agendatype") != null)
            agendaType = (AgendaType)Enum.Parse(typeof(AgendaType), toObject.Attribute("agendatype").Value);
         Agenda newObj = new Agenda(id, name, cost, agendaType);
         return newObj;
      }
   }
}

