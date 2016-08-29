using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Supply : Identifiable, ISerialisable
   {
      public Supply (int id, string name) : base(id, name)
      {
      }
      
      public XElement Serialise()
      {
         XElement x = new XElement("Supply");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         return x;
      }
      
      public static Supply Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         Supply newObj = new Supply(id, name);
         return newObj;
      }
   }
}

