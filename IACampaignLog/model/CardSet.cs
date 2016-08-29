using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
   public class CardSet<T> : Identifiable, ISerialisable where T : ISerialisable
   {
      private IList<T> _listOfT;
      private Character _associatedCharacter;
      
      public CardSet (int id, string name, Character associatedCharacter) : base(id, name)
      {
         _associatedCharacter = associatedCharacter;
         _listOfT = new List<T>();
      }
      
      public IList<T> ListOfT {get{return _listOfT;}}
      public Character AssociatedCharacter {get{return _associatedCharacter;}}
      
      public XElement Serialise()
      {
         XElement elem = new XElement("Set");
         elem.SetAttributeValue("id", this.Id);
         elem.SetAttributeValue("name", this.Name);
         elem.SetAttributeValue("character", this.AssociatedCharacter.Id);
         foreach (T t in _listOfT) {elem.Add(t.Serialise());}
         return elem;
      }
      
      public static CardSet<T> Deserialise(XElement elem, Func<XElement, T> deserialiseT)
      {
         int id = int.Parse(elem.Attribute("id").Value);
         string name = elem.Attribute("name").Value;
         int assocCharId = int.Parse(elem.Attribute("character").Value);
         Character assocChar;
         if (assocCharId == CharacterController.GetInstance().ImperialCharacter.Id)
            assocChar = CharacterController.GetInstance().ImperialCharacter;
         else
            assocChar = CharacterController.GetInstance().FindWithId(assocCharId);
         CardSet<T> cs = new CardSet<T>(id, name, assocChar);
         IList<T> items = (from XElement x in elem.Elements()
                           select deserialiseT(x)).ToList();
         foreach (T item in items) {cs.ListOfT.Add(item);}
         return cs;
      }
   }
}

