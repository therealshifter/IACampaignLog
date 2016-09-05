using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
	public class ImperialPlayer : Player
	{
      public delegate void InfluenceChangedEventHandeler(ImperialPlayer sender, EventArgs e);
      
		private IDictionary<Agenda, Character> _purchasedAgendas;
      private IList<Agenda> _spentAgendas;
      private int _influence;
		public event InfluenceChangedEventHandeler InfluenceChanged;
      
		public ImperialPlayer (string name, CardSet<ClassCard> impClass) : 
			base(name, CharacterController.GetInstance().ImperialCharacter, impClass)
		{
			_purchasedAgendas = new Dictionary<Agenda, Character>();
         _spentAgendas = new List<Agenda>();
		}
		
		public int Influence
      {
         get
         {return _influence;}
         set
         {
            _influence = value;
            if (InfluenceChanged != null) {InfluenceChanged(this, EventArgs.Empty);}
         }
      }
		public IDictionary<Agenda, Character> PurchasedAgendas {get{return _purchasedAgendas;}}
      public IList<Agenda> SpentAgendas {get{return _spentAgendas;}}
		
		public new XElement Serialise()
		{
			XElement elem = base.Serialise();
			elem.Name = "ImperialPlayer";
			elem.SetAttributeValue("influence", Influence);
			XElement agendaElem = new XElement("PurchasedAgendas");
         foreach (KeyValuePair<Agenda, Character> kv in PurchasedAgendas)
         {
            XElement a = new XElement("Agenda", kv.Key.Id);
            if (kv.Value != null)
               a.SetAttributeValue("target", kv.Value.Id);
			   agendaElem.Add(a);
         }
			elem.Add(agendaElem);
			return elem;
		}
		
		public static ImperialPlayer Deserialise(XElement elem)
		{
			ImperialPlayer imp = Player.Deserialise<ImperialPlayer>(elem, 
				   (name, playerChar, playerClass) => {
						ImperialPlayer i = new ImperialPlayer(name, playerClass);
						return i;
					});
			imp.Influence = int.Parse(elem.Attribute("influence").Value);
			IEnumerable<KeyValuePair<Agenda, Character>> agendas =
               from XElement e in elem.Element("PurchasedAgendas").Elements("Agenda")
				   let agenda = AgendaController.GetInstance().FindAgendaWithId(int.Parse(e.Value))
               let charId = int.Parse(e.Attribute("target") == null ? "-1" : e.Attribute("target").Value)
               let character = charId < 0 ? null : CharacterController.GetInstance().FindWithId(charId)
				   select new KeyValuePair<Agenda, Character>(agenda, character);
			foreach (KeyValuePair<Agenda, Character> a in agendas) {imp.PurchasedAgendas.Add(a.Key, a.Value);}
			return imp;
		}
	}
}

