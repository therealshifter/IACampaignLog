using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class ClassCard : Identifiable, ISerialisable
   {
      public ClassCard (int id, string name, int xpCost, bool isItem) : base(id, name)
      {
         XpCost = xpCost;
         IsItem = isItem;
      }

      public ClassCard (int id, string name, int xpCost) : this (id, name, xpCost, false) { }
      
      public int XpCost {get; set;}
      public bool IsItem {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Class");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("cost", this.XpCost);
         x.SetAttributeValue("isItem", this.IsItem);
         return x;
      }
      
      public static ClassCard Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         int cost = int.Parse(toObject.Attribute("cost").Value);
         bool isItem = false;
         if (toObject.Attribute("isItem") != null)
         {
            Boolean.TryParse(toObject.Attribute("isItem").Value, out isItem);
         }
         ClassCard newObj = new ClassCard(id, name, cost, isItem);
         return newObj;
      }
   }
}

