using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class CharacterController : SingletonListController<Character>
   {
      private Character _imperialCharacter;
      
      public Character ImperialCharacter {get{return _imperialCharacter;}}
      
      public CharacterController() : base(ConfigController.CharacterResourcePath(),
                                          (x) => Character.Deserialise(x))
      {
         
      }
      
      public static CharacterController GetInstance()
      {
         if (_instance == null)
         {_instance = new CharacterController();}
         return (CharacterController)_instance;
      }
      
      public Character AddCharacter(string name, SideMission relatedSideMission)
      {
         if (string.IsNullOrEmpty(name))
         {throw new ArgumentNullException("name");}
         if (relatedSideMission == null)
         {throw new ArgumentNullException("relatedSideMission");}
         
         return base.AddT((x) => new Character(x, name, relatedSideMission));
      }
      
      public override void Load()
      {
         _imperialCharacter = new Character(-1, "Imperial", null);
         //Load
         base.Load();
      }
      
   }
}

