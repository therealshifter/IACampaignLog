using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
	public class Game : Identifiable, ISerialisable
	{
      public delegate void CreditsChangedEventHandler(Object sender, EventArgs e);
      public event CreditsChangedEventHandler CreditsChanged;
	   private DateTime _gameDate;
	   private Campaign _gameCampaign;
	   private IList<HeroPlayer> _heroes;
	   private ImperialPlayer _impPlayer;
	   private IList<CardSet<Agenda>> _selectedAgendaSets;
      private IList<SideMission> _selectedSideMissions;
      private IList<Mission> _missions;
      private int _heroCredits;
		
		public Game (int id, string name, DateTime gameDate, Campaign gameCampaign) : base(id, name)
		{
			_gameDate = gameDate;
			_gameCampaign = gameCampaign;
         _heroCredits = 0;
			_impPlayer = null;
			_heroes = null;
			_selectedAgendaSets = null;
         _missions = null;
         _selectedSideMissions = null;
		}
      
      public Game (int id, string name, DateTime gameDate, Campaign gameCampaign, ImperialPlayer impPlayer, IList<HeroPlayer> players, IList<CardSet<Agenda>> selectedAgendaSets, IList<Mission> gameMissions, IList<SideMission> selectedSideMissions) :
            this(id, name, gameDate, gameCampaign)
      {
         _impPlayer = impPlayer;
         _heroes = players;
         _selectedAgendaSets = selectedAgendaSets;
         _missions = gameMissions;
         _selectedSideMissions = selectedSideMissions;
      }
      
      public Game (Game header, ImperialPlayer impPlayer, IList<HeroPlayer> players, IList<CardSet<Agenda>> selectedAgendaSets, IList<Mission> gameMissions, IList<SideMission> selectedSideMissions) :
            this(header.Id, header.Name, header.GameDate, header.GameCampaign, impPlayer, players, selectedAgendaSets, gameMissions, selectedSideMissions)
      {
         
      }
      
		public DateTime GameDate { get{return _gameDate;} }
		public Campaign GameCampaign {get{return _gameCampaign;}}
		public IList<HeroPlayer> Heroes {get{return _heroes;}}
		public ImperialPlayer ImpPlayer {get{return _impPlayer;}}
		public IList<CardSet<Agenda>> SelectedAgendaSets {get{return _selectedAgendaSets;}}
      public IList<Mission> Missions {get{return _missions;}}
		public IList<SideMission> SelectedSideMissions {get{return _selectedSideMissions;}}
      public string AvailableSideMission1 {get; set;}
      public string AvailableSideMission2 {get; set;}
      public ThreatMission AvailableThreatMission1 {get; set;}
      public ThreatMission AvailableThreatMission2 {get; set;}
      
      public int HeroCreditsPool
      {
         get
         {return _heroCredits;}
         set
         {
            _heroCredits = value;
            if (CreditsChanged != null) {CreditsChanged(this, EventArgs.Empty);}
         }
      }
      
      public XElement SerialiseHeader()
      {
         XElement elem = new XElement("Game");
         elem.SetAttributeValue("id", this.Id);
         elem.SetAttributeValue("name", this.Name);
         elem.SetAttributeValue("gamedate", this.GameDate.ToShortDateString());
         elem.SetAttributeValue("campaign", this.GameCampaign.Id);
         return elem;
      }
      
      public XElement SerialiseForGameList()
      {
         XElement elem = SerialiseHeader();
         elem.Add(new XElement("ImperialPlayer", this.ImpPlayer.Name));
         XElement playersElem = new XElement("Players");
         foreach (HeroPlayer h in Heroes) {playersElem.Add(new XElement("Player", h.Name));}
         elem.Add(playersElem);
         return elem;
      }
      
		public XElement Serialise()
		{
			XElement elem = SerialiseHeader();
         elem.SetAttributeValue("credits", HeroCreditsPool);
         if (AvailableSideMission1 != null)
            elem.SetAttributeValue("availableside1", AvailableSideMission1);
         if (AvailableSideMission2 != null)
            elem.SetAttributeValue("availableside2", AvailableSideMission2);
         if (AvailableThreatMission1 != null && AvailableThreatMission1.Id >= 0)
            elem.SetAttributeValue("availableThreat1", AvailableThreatMission1.Id);
         if (AvailableThreatMission2 != null && AvailableThreatMission2.Id >= 0)
            elem.SetAttributeValue("availableThreat2", AvailableThreatMission2.Id);
         elem.Add(this.ImpPlayer.Serialise());
         XElement playersElem = new XElement("Players");
         foreach (HeroPlayer h in Heroes) {playersElem.Add(h.Serialise());}
         elem.Add(playersElem);
			XElement agendaSetsElem = new XElement("SelectedAgendaSets");
			foreach (CardSet<Agenda> csa in SelectedAgendaSets) {agendaSetsElem.Add(new XElement("AgendaSet", csa.Id));}
			elem.Add(agendaSetsElem);
         XElement missionsElem = new XElement("Missions");
         foreach (Mission m in Missions) {missionsElem.Add(m.Serialise());}
         elem.Add(missionsElem);
         XElement missionsToChooseElem = new XElement("SelectedSideMissions");
         foreach (SideMission m in SelectedSideMissions) {missionsToChooseElem.Add(new XElement("SideMission", m.Id));}
         elem.Add(missionsToChooseElem);
			return elem;
		}
      
		public static Game Deserialise(XElement elem)
		{
			Game g = Game.DeserialiseHeader(elem);
         ImperialPlayer imp = ImperialPlayer.Deserialise(elem.Element("ImperialPlayer"));
         IList<HeroPlayer> heroes = (from XElement x in elem.Element("Players").Elements()
                                     select HeroPlayer.Deserialise(x)).ToList();
			IList<CardSet<Agenda>> agendaSets = (from XElement x in elem.Element("SelectedAgendaSets").Elements()
			                                     let setId = int.Parse(x.Value)
			                                     select AgendaController.GetInstance().FindWithId(setId)).ToList();
         IList<Mission> missions = (from XElement x in elem.Element("Missions").Elements("Mission")
                                    select Mission.Deserialise(x)).ToList();
         IList<SideMission> sideMissions = (from XElement x in elem.Element("SelectedSideMissions").Elements()
                                            let smId = int.Parse(x.Value)
                                            select SideMissionController.GetInstance().FindWithId(smId)).ToList();
			string availableSide1 = null;
         if (elem.Attribute("availableside1") != null)
            availableSide1 = elem.Attribute("availableside1").Value;
         string availableSide2 = null;
         if (elem.Attribute("availableside2") != null)
            availableSide2 = elem.Attribute("availableside2").Value;
         ThreatMission tm1, tm2;
         tm1 = tm2 = null;
         if (elem.Attribute("availableThreat1") != null)
         {
            tm1 = (ThreatMission)SideMissionController.GetInstance().FindWithId(int.Parse(elem.Attribute("availableThreat1").Value));
         }
         if (elem.Attribute("availableThreat2") != null)
         {
            tm2 = (ThreatMission)SideMissionController.GetInstance().FindWithId(int.Parse(elem.Attribute("availableThreat2").Value));
         }
         g = new Game(g, imp, heroes, agendaSets, missions, sideMissions);
         g.HeroCreditsPool = int.Parse(elem.Attribute("credits").Value);
         g.AvailableSideMission1 = availableSide1;
         g.AvailableSideMission2 = availableSide2;
         g.AvailableThreatMission1 = tm1;
         g.AvailableThreatMission2 = tm2;
			return g;
		}
      
      private static Game DeserialiseHeader(XElement elem)
      {
         int id = int.Parse(elem.Attribute("id").Value);
         string name = elem.Attribute("name").Value;
         DateTime gameDate = DateTime.Parse(elem.Attribute("gamedate").Value);
         Campaign gameCampaign = CampaignController.GetInstance().FindWithId(int.Parse(elem.Attribute("campaign").Value));
         Game g = new Game(id, name, gameDate, gameCampaign);
         return g;
      }
      
      public static Game DeserialiseFromGameList(XElement elem)
      {
         Game g = Game.DeserialiseHeader(elem);
         ImperialPlayer imp = new ImperialPlayer(elem.Element("ImperialPlayer").Value, null);
         IList<HeroPlayer> heroes = (from XElement x in elem.Element("Players").Elements()
                                     select new HeroPlayer(x.Value, null, null)).ToList();
         g = new Game(g, imp, heroes, null, null, null);
         return g;
      }
	}
}

