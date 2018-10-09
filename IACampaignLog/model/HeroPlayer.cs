using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
	public class HeroPlayer : Player
	{
		private IList<Item> _purchasedItems;
      public delegate void ItemEventHandler(object sender, EventArgs e);
      public event ItemEventHandler ItemTraded;
		
		public HeroPlayer (string name, Character heroCharacter, CardSet<ClassCard> heroClass) : 
			base(name, heroCharacter, heroClass)
		{
			_purchasedItems = new List<Item>();
		}
		
		public IList<Item> PurchasedItems {get{return _purchasedItems;}}
      
      public void RaiseItemTradedEvent(object sender)
      {
         if (ItemTraded != null)
            ItemTraded(sender, EventArgs.Empty);
      }
      
      public new XElement Serialise()
      {
         XElement elem = base.Serialise();
         elem.Name = "HeroPlayer";
         XElement itemsElem = new XElement("PurchasedItems");
         itemsElem.Add(from Item a in PurchasedItems
                       select new XElement("Item", a.Id));
         elem.Add(itemsElem);
         return elem;
      }
      
      public static HeroPlayer Deserialise(XElement elem)
      {
         HeroPlayer hero = Player.Deserialise<HeroPlayer>(elem, 
               (name, playerChar, playerClass) => {
                  HeroPlayer h = new HeroPlayer(name, playerChar, playerClass);
                  return h;
               });
         IEnumerable<Item> items = from XElement e in elem.Element("PurchasedItems").Elements("Item")
                                   let itemId = int.Parse(e.Value)
                                   select ItemController.GetInstance().FindWithId(itemId);
         foreach (Item i in items) {hero.PurchasedItems.Add(i);}
         
         return hero;
      }
	}
}

