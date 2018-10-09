using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class ClassController : CardSetController<ClassCard>
   {
      public ClassController() : base(ConfigController.ClassResourcePath(),
                                      (x) => ClassCard.Deserialise(x))
      {
         
      }
      
      public static ClassController GetInstance()
      {
         if (_instance == null)
         {_instance = new ClassController();}
         return (ClassController)_instance;
      }
      
      public ClassCard FindClassCardWithId(int id)
      {
         return base.FindTFromAllSetsWithId(id);
      }
      
      public IList<ClassCard> FindClassCardInSetWithName(int setId, string name)
      {
         return base.FindTInSetWithName(setId, name);
      }
      
      public IList<CardSet<ClassCard>> FindClassSetForCharacter(int characterId)
      {
         return (from CardSet<ClassCard> c in this.ListOfT
                 where c.AssociatedCharacter.Id == characterId
                 select c).ToList();
      }
      
      public ClassCard AddClassCard(CardSet<ClassCard> classSet, string name, int xpCost, bool isItem)
      {
         if (string.IsNullOrWhiteSpace(name))
         {throw new ArgumentException("Class name cannot be empty");}
         return base.AddToSet(classSet, (x) => new ClassCard(x, name, xpCost, isItem));
      }
      
   }
}

