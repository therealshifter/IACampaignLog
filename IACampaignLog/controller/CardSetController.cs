using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class CardSetController<T> : SingletonListController<CardSet<T>>
      where T : Identifiable, ISerialisable
   {
      public CardSetController(string resourcePath, Func<System.Xml.Linq.XElement, T> deserialiseT) :
         base(resourcePath, (x) => CardSet<T>.Deserialise(x, deserialiseT))
      {
         
      }
      
      protected T FindTFromAllSetsWithId(int id)
      {
         return (from CardSet<T> aSet in this.ListOfT
                 where aSet.ListOfT.Any((x) => x.Id == id)
                 select aSet.ListOfT.Where((x) => x.Id == id).SingleOrDefault()).SingleOrDefault();
      }
      
      protected IList<T> FindTInSetWithName(int setId, string name)
      {
         CardSet<T> cSet = this.ListOfT.Where((x) => x.Id == setId).SingleOrDefault();
         if (cSet != null)
         {
            return cSet.ListOfT.Where((x) => x.Name.ToLower().Equals(name.ToLower())).ToList();
         }
         else
            return null;
      }
      
      public CardSet<T> AddSet(string setName, Character associatedCharacter)
      {
         if (string.IsNullOrEmpty(setName))
         {throw new ArgumentException("Set name cannot be null or empty");}
         
         return base.AddT((x) => new CardSet<T>(x, setName, associatedCharacter));
      }
      
      protected T AddToSet(CardSet<T> tSet, Func<int, T> constructT)
      {
         int newId = 0;
         T newT = null;
         if (tSet == null)
         {throw new ArgumentNullException("tSet");}
         if (constructT == null)
         {throw new ArgumentException("Must specify type construction delegate", "constructT");}
         
         if (this.ListOfT.Count > 0)
         {
            IEnumerable<int> idList = (from t in this.ListOfT
                     where t.ListOfT.Count > 0
                     select t.ListOfT.Max((x) => x.Id));
            //Get max id
            if (idList.Count() > 0)
               newId = idList.Max() + 1;
         }
         newT = constructT(newId);
         tSet.ListOfT.Add(newT);
         HasChanges = true;
         return newT;
      }
      
   }
}

