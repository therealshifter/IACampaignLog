using System;
using System.Xml.Linq;

namespace IACampaignLog
{
    public class ThreatMission : SideMission
    {
        public ThreatMission(int id, string name, Reward baneReward) : base(id, name, MissionType.Threat)
        {
            BaneReward = baneReward;
        }

        public ThreatMission(SideMission baseDetails, Reward baneReward) : this(baseDetails.Id, baseDetails.Name, baneReward)
        {
        }
        
        public Reward BaneReward { get; set; }

        public override XElement Serialise()
        {
            XElement x = base.Serialise();
            x.Name = "ThreatMission";
            x.SetAttributeValue("BaneReward", BaneReward.Id);
            return x;
        }

        public static new ThreatMission Deserialise(XElement toObject)
        {
            SideMission baseDetails = SideMission.Deserialise(toObject);
            int baneId = int.Parse(toObject.Attribute("BaneReward").Value);
            Reward baneReward = RewardController.GetInstance().FindWithId(baneId);
            ThreatMission newObject = new ThreatMission(baseDetails, baneReward);
            return newObject;
        }
    }
}
