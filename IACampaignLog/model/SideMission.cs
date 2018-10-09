using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class SideMission : Identifiable, ISerialisable
   {
      public enum MissionType
      {
         Red,
         Green,
         Grey,
         Forced
      }
      
      public SideMission (int id, string name, MissionType missionType) : base(id, name)
      {
         SideMissionType = missionType;
      }
      
      public MissionType SideMissionType {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("SideMission");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("missiontype", this.SideMissionType);
         return x;
      }
      
      public static SideMission Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         MissionType mType = (MissionType)Enum.Parse(typeof(MissionType), toObject.Attribute("missiontype").Value);
         SideMission newObj = new SideMission(id, name, mType);
         return newObj;
      }
   }
}

