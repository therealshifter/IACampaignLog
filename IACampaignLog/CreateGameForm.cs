using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class CreateGameForm : EditFormBase
   {
      public CreateGameForm () : base()
      {
         Initialise();
      }
      
      private HeroPlayer CreateHeroPlayer(string name, Character playerChar)
      {
         CardSet<ClassCard> playerClass = ClassController.GetInstance().FindClassSetForCharacter(playerChar.Id).FirstOrDefault();
         HeroPlayer hero = new HeroPlayer(name, playerChar, playerClass);
         foreach (ClassCard c in playerClass.ListOfT.Where((x) => x.XpCost <= 0))
            hero.PurchasedClassCards.Add(c);
         return hero;
      }
      
      private IList<Mission> CreateMissions(Campaign gameCampaign)
      {
         IList<MissionHeader> missions = gameCampaign.CloneMissionTemplate();
         return Mission.ToMissionList(missions);
      }
      
      private Game CreateGame()
      {
         Game newGame = null;
         ImperialPlayer impPlayer = new ImperialPlayer(_gameDetails.ImperialPlayerName, _gameDetails.ImperialClass);
         List<HeroPlayer> players = new List<HeroPlayer>();
         IList<CardSet<Agenda>> agendas;
         IList<SideMission> sideMissions;
         IList<Mission> missions = CreateMissions(_gameDetails.SelectedCampaign);
         
         foreach (ClassCard c in impPlayer.PlayerClass.ListOfT.Where((x) => x.XpCost <= 0))
            impPlayer.PurchasedClassCards.Add(c);
         
         if (!string.IsNullOrWhiteSpace(_gameDetails.Player1Name))
         {
            HeroPlayer x = CreateHeroPlayer(_gameDetails.Player1Name, _gameDetails.Player1Character);
            x.Xp = _gameDetails.SelectedCampaign.StartingRebelXP;
            players.Add(x);
         }
         if (!string.IsNullOrWhiteSpace(_gameDetails.Player2Name))
         {
            HeroPlayer x = CreateHeroPlayer(_gameDetails.Player2Name, _gameDetails.Player2Character);
            x.Xp = _gameDetails.SelectedCampaign.StartingRebelXP;
            players.Add(x);
         }
         if (!string.IsNullOrWhiteSpace(_gameDetails.Player3Name))
         {
            HeroPlayer x = CreateHeroPlayer(_gameDetails.Player3Name, _gameDetails.Player3Character);
            x.Xp = _gameDetails.SelectedCampaign.StartingRebelXP;
            players.Add(x);
         }
         if (!string.IsNullOrWhiteSpace(_gameDetails.Player4Name))
         {
            HeroPlayer x = CreateHeroPlayer(_gameDetails.Player4Name, _gameDetails.Player4Character);
            x.Xp = _gameDetails.SelectedCampaign.StartingRebelXP;
            players.Add(x);
         }
         impPlayer.Xp = _gameDetails.SelectedCampaign.StartingImperialXP;
         impPlayer.Influence = _gameDetails.SelectedCampaign.StartingInfluence;
         agendas = _gameDetails.SelectedAgendas();
         if (_gameDetails.SelectedCampaign.AllowSideMissions)
         {
            sideMissions = _gameDetails.SelectedSideMissions();
            foreach (HeroPlayer p in players)
            {sideMissions.Add(p.PlayerCharacter.RelatedSideMission);}
         }
         else
            sideMissions = new List<SideMission>();
         if (_gameDetails.SelectedCampaign.IncludeThreatMissions)
         {
            foreach (SideMission sm in SideMissionController.GetInstance().ThreatMissions())
            {sideMissions.Add(sm);}
         }
         newGame = GameController.GetInstance().AddGame(_gameDetails.Name, DateTime.Today, _gameDetails.SelectedCampaign, impPlayer, players, agendas, missions, sideMissions);
         newGame.HeroCreditsPool = _gameDetails.SelectedCampaign.StartingCredits * players.Count();
         return newGame;
      }
      
      public override void ExecuteSave ()
      {
         if (_gameDetails.Validate())
         {
            Game addedGame = CreateGame();
            GameController.GetInstance().SaveGame(addedGame);
            GameController.GetInstance().Save();
            base.ExecuteSave ();
         }
      }
      
      public override void ExecuteCancel ()
      {
         base.ExecuteCancel ();
      }
   }
}

