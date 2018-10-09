using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class ClassCard : Identifiable, ISerialisable
   {
      public ClassCard (int id, string name, int xpCost) : base(id, name)
      {
         XpCost = xpCost;
      }
      
      public int XpCost {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Class");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("cost", this.XpCost);
         return x;
      }
      
      public static ClassCard Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         int cost = int.Parse(toObject.Attribute("cost").Value);
         ClassCard newObj = new ClassCard(id, name, cost);
         return newObj;
      }
   }
}

