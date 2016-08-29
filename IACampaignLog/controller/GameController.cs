using System;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public class GameController : SingletonListController<Game>
   {
      public GameController() : base(ConfigController.GameSavePath() + "GameList.xml",
                                          (x) => Game.DeserialiseFromGameList(x))
      {
         
      }
      
      public static GameController GetInstance()
      {
         if (_instance == null)
         {_instance = new GameController();}
         return (GameController)_instance;
      }
      
      public Game AddGame(string name, DateTime gameDate, Campaign gameCampaign,
                          ImperialPlayer impPlayer, IList<HeroPlayer> players,
                          IList<CardSet<Agenda>> selectedAgendas, IList<Mission> missions,
                          IList<SideMission> selectedSideMissions)
      {
         return base.AddT((x) => new Game(x, name, gameDate, gameCampaign,
                                          impPlayer, players, selectedAgendas, missions, selectedSideMissions));
      }
      
      public Game LoadGame(int id)
      {
         Game g = null;
         string gamePath = System.IO.Path.GetDirectoryName(ResourcePath) + "\\" + id + ".xml";
         System.Console.WriteLine("Loading game: " + gamePath);
         
         if (System.IO.File.Exists(gamePath))
         {
            System.Xml.Linq.XDocument xdoc = System.Xml.Linq.XDocument.Load(gamePath);
            g = Game.Deserialise(xdoc.Root);
         }
         else System.Console.WriteLine("Unable to to load game: " + gamePath);
         
         return g;
      }
      
      public void SaveGame(Game gameToSave)
      {
         string gamePath = System.IO.Path.GetDirectoryName(ResourcePath) + "\\" + gameToSave.Id + ".xml";
         System.Console.WriteLine("Saving game: " + gamePath);
         System.Xml.Linq.XDocument xdoc = new System.Xml.Linq.XDocument(gameToSave.Serialise());
         xdoc.Save(gamePath);
      }
      
      public new void Save()
      {
         base.Save((x) => x.SerialiseForGameList());
      }
   }
}

