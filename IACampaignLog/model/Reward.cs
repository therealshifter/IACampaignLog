using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public class Reward : Identifiable, ISerialisable
   {
      public enum RewardType
      {
         Regular,
         Item,
         Ally,
         Villain,
         Bane,
         Boon
      }
      
      public Reward (int id, string name, RewardType type) : base(id, name)
      {
         RewardSubType = type;
      }

      public RewardType RewardSubType {get; set;}
      
      public XElement Serialise()
      {
         XElement x = new XElement("Reward");
         x.SetAttributeValue("id", this.Id);
         x.SetAttributeValue("name", this.Name);
         x.SetAttributeValue("rewardtype", this.RewardSubType.ToString());
         return x;
      }
      
      public static Reward Deserialise(XElement toObject)
      {
         int id = int.Parse(toObject.Attribute("id").Value);
         string name = toObject.Attribute("name").Value;
         RewardType rt;
         if (toObject.Attribute("rewardtype") == null || !Enum.TryParse(toObject.Attribute("rewardtype").Value, out rt))
            rt = RewardType.Regular;
         Reward newObj = new Reward(id, name, rt);
         return newObj;
      }
   }
}

