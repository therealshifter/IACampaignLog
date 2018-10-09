using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
	public class ImperialPlayer : Player
	{
      public delegate void InfluenceChangedEventHandeler(ImperialPlayer sender, EventArgs e);
      public delegate void AgendaDiscardedEventHandler(ImperialPlayer sender, EventArgs e);
		private IList<Agenda> _purchasedAgendas;
      private IList<Agenda> _discardedAgendas;
      private int _influence;
		public event InfluenceChangedEventHandeler InfluenceChanged;
      public event AgendaDiscardedEventHandler AgendaDiscarded;
      
		public ImperialPlayer (string name, CardSet<ClassCard> impClass) : 
			base(name, CharacterController.GetInstance().ImperialCharacter, impClass)
		{
			_purchasedAgendas = new List<Agenda>();
         _discardedAgendas = new List<Agenda>();
		}
		
		public int Influence
      {
         get
         {return _influence;}
         set
         {
            _influence = value;
            InfluenceChanged?.Invoke(this, EventArgs.Empty);
         }
      }

		public IList<Agenda> PurchasedAgendas {get{return _purchasedAgendas;}}
      public IList<Agenda> DiscardedAgendas {get{return _discardedAgendas;}}

      public void RaiseAgendaDiscardedEvent(object sender)
      {
         if (AgendaDiscarded != null)
         {
            AgendaDiscarded(this, EventArgs.Empty);
         }
      }

      public void DiscardAgenda(int agendaId)
      {
         Agenda agendaToDiscard = PurchasedAgendas.Single((a) => a.Id == agendaId);
         if (agendaToDiscard != null)
         {
            PurchasedAgendas.Remove(agendaToDiscard);
            DiscardedAgendas.Add(agendaToDiscard);
            RaiseAgendaDiscardedEvent(agendaToDiscard);
         }
      }
		
		public new XElement Serialise()
		{
			XElement elem = base.Serialise();
			elem.Name = "ImperialPlayer";
			elem.SetAttributeValue("influence", Influence);
			XElement agendaElem = new XElement("PurchasedAgendas");
			agendaElem.Add(from Agenda a in PurchasedAgendas
			               select new XElement("Agenda", a.Id));
			elem.Add(agendaElem);
         XElement discardedAgendaElem = new XElement("DiscardedAgendas");
         discardedAgendaElem.Add(from Agenda a in DiscardedAgendas
                        select new XElement("Agenda", a.Id));
         elem.Add(discardedAgendaElem);
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
			IEnumerable<Agenda> agendas = from XElement e in elem.Element("PurchasedAgendas").Elements("Agenda")
										  let agendaId = int.Parse(e.Value)
										  select AgendaController.GetInstance().FindAgendaWithId(agendaId);
			foreach (Agenda a in agendas) {imp.PurchasedAgendas.Add(a);}
         IEnumerable<Agenda> discardedAgendas = from XElement e in elem.Element("DiscardedAgendas").Elements("Agenda")
                                       let agendaId = int.Parse(e.Value)
                                       select AgendaController.GetInstance().FindAgendaWithId(agendaId);
         foreach (Agenda a in discardedAgendas) {imp.DiscardedAgendas.Add(a);}
         return imp;
		}
	}
}

