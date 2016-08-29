using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Character : Identifiable, ISerialisable
   {
      public Character (int id, string name, SideMission relatedSideMission) : base(id, name)
      {
         RelatedSideMission = relatedSideMission;
      }
      
      public SideMission RelatedSideMission {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Character");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         if (RelatedSideMission != null)
            x.SetAttributeValue("sidemission", this.RelatedSideMission.Id);
         return x;
      }
      
      public static Character Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         SideMission sm = null;
         if (toObject.Attribute("name") != null)
            sm = SideMissionController.GetInstance().FindWithId(int.Parse(toObject.Attribute("sidemission").Value));
         Character newObj = new Character(id, name, sm);
         return newObj;
      }
   }
}

