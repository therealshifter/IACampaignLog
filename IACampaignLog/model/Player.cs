using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
	public abstract class Player : ISerialisable
	{
      public delegate void XpChangedEventHandler(Player sender, EventArgs e);
      
      private string _name;
      private int _xp;
		private CardSet<ClassCard> _playerClass;
		private Character _character;
		private IList<Reward> _rewards;
      private IList<ClassCard> _purchasedClassCards;
      public event XpChangedEventHandler XpChanged;
		
		public Player (string name, Character playerCharacter, CardSet<ClassCard> playerClass)
		{
         _name = name;
			_playerClass = playerClass;
			_character = playerCharacter;
			_rewards = new List<Reward>();
         _purchasedClassCards = new List<ClassCard>();
		}
		
      public string Name {get{return _name;}}
		public CardSet<ClassCard> PlayerClass {get{return _playerClass;}}
		public Character PlayerCharacter {get{return _character;}}
		public IList<Reward> Rewards {get{return _rewards;}}
      public IList<ClassCard> PurchasedClassCards {get{return _purchasedClassCards;}}
      
		public int Xp
      {
         get {return _xp;}
         set
         {
            _xp = value;
            if (XpChanged != null) XpChanged(this, EventArgs.Empty);
         }
      }
      
		public XElement Serialise()
		{
			XElement x = new XElement("Player");
			x.SetAttributeValue("name", this.Name);
			x.SetAttributeValue("character", this.PlayerCharacter.Id);
			x.SetAttributeValue("class", this.PlayerClass.Id);
         x.SetAttributeValue("xp", this.Xp);
			XElement rewardElem  = new XElement("Rewards");
			rewardElem.Add(from Reward r in _rewards
			               select new XElement("Reward", r.Id));
         x.Add(rewardElem);
         XElement classCardsElem = new XElement("PurchasedClassCards");
         classCardsElem.Add(from ClassCard c in _purchasedClassCards
                           select new XElement("ClassCard", c.Id));
         x.Add(classCardsElem);
			return x;
		}
		
		public static T Deserialise<T>(XElement toObject, Func<string, Character, CardSet<ClassCard>, T> constructT) where T : Player
		{
			string name = toObject.Attribute("name").Value;
			int charId = int.Parse(toObject.Attribute("character").Value);
         int xp = int.Parse(toObject.Attribute("xp").Value);
         Character playerChar;
         if (charId == -1)
            playerChar = CharacterController.GetInstance().ImperialCharacter;
         else
			   playerChar = CharacterController.GetInstance().FindWithId(charId);
			CardSet<ClassCard> playerClass = ClassController.GetInstance().FindWithId(int.Parse(toObject.Attribute("class").Value));
			IList<Reward> rewards = (from XElement elem in toObject.Element("Rewards").Elements("Reward")
			                         let rewardId = int.Parse(elem.Value)
			                         select RewardController.GetInstance().FindWithId(rewardId)).ToList();
         IList<ClassCard> classCards = (from XElement elem in toObject.Element("PurchasedClassCards").Elements("ClassCard")
                                        let classCardId = int.Parse(elem.Value)
                                        select ClassController.GetInstance().FindClassCardWithId(classCardId)).ToList();
			T newObj = constructT(name, playerChar, playerClass);
         newObj.Xp = xp;
         foreach (Reward r in rewards) {newObj.Rewards.Add(r);}
         foreach (ClassCard c in classCards) {newObj.PurchasedClassCards.Add(c);}
			return newObj;
		}
	}
}

