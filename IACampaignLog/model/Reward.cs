using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Reward : Identifiable, ISerialisable
   {
      public Reward (int id, string name) : base(id, name)
      {
      }
      
      public XElement Serialise()
      {
         XElement x = new XElement("Reward");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         return x;
      }
      
      public static Reward Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         Reward newObj = new Reward(id, name);
         return newObj;
      }
   }
}

