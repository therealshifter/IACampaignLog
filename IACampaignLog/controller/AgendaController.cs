using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class AgendaController : CardSetController<Agenda>
   {
      public AgendaController() : base(ConfigController.AgendaResourcePath(),
                                       (x) => Agenda.Deserialise(x))
      {
         
      }
      
      public static AgendaController GetInstance()
      {
         if (_instance == null)
         {_instance = new AgendaController();}
         return (AgendaController)_instance;
      }
      
      public Agenda FindAgendaWithId(int id)
      {
         return base.FindTFromAllSetsWithId(id);
      }
      
      public IList<Agenda> FindAgendaInSetWithName(int setId, string name)
      {
         return base.FindTInSetWithName(setId, name);
      }
      
      public Agenda AddAgenda(CardSet<Agenda> agendaSet, string name, int influence, Agenda.AgendaType agendaType, int discardCost)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentException("Agenda name cannot be null or empty");}
         
         return base.AddToSet(agendaSet, (x) => new Agenda(x, name, influence, agendaType, discardCost));
      }
      
   }
}

