using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class ViewGameForm : EditFormBase
   {
      private Form _gameDetailsForm;
      private Game _currentGame;
      
      public ViewGameForm () : base()
      {
         _currentGame = null;
         _gameDetailsForm = null;
         Initialise();
         
         _showGameDetailsButton.Click += Handle_showGameDetailsButtonClick;
         _applyMissionChangesButton.Click += Handle_applyMissionChangesButtonClick;
         _cancelMissionChangesButton.Click += Handle_cancelMissionChangesButtonClick;
         _addForcedMissionButton.Click += Handle_addForcedMissionButtonClick;
         this.Closing += HandleClosing;
      }

      void Handle_addForcedMissionButtonClick (object sender, EventArgs e)
      {
         AddForcedMission(string.Empty);
      }

      void Handle_cancelMissionChangesButtonClick (object sender, EventArgs e)
      {
         foreach (MissionPanel mp in _missionListPanel.Controls)
         {
            mp.LoadMission(mp.CurrentMission);
         }
         SetHasMissionChanges(false);
      }

      void Handle_applyMissionChangesButtonClick (object sender, EventArgs e)
      {
         ApplyMissionChanges();
      }

      void HandleClosing (object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (_gameDetailsForm != null)
            _gameDetailsForm.Close();
      }

      void Handle_showGameDetailsButtonClick (object sender, EventArgs e)
      {
         if (_gameDetailsForm == null)
         {
            GameDetailsPanel gameDetails = new GameDetailsPanel();
            gameDetails.Dock = DockStyle.Fill;
            _gameDetailsForm = new Form();
            _gameDetailsForm.Text = "Game Details";
            _gameDetailsForm.Size = gameDetails.Size;
            _gameDetailsForm.Controls.Add(gameDetails);
            _gameDetailsForm.Closed += Handle_gameDetailsFormClosed;
            _gameDetailsForm.Show();
            gameDetails.LoadGame(_currentGame);
         }
      }

      void Handle_gameDetailsFormClosed (object sender, EventArgs e)
      {
         _gameDetailsForm.Closed -= Handle_gameDetailsFormClosed;
         _gameDetailsForm = null;
      }
      
      private void LoadMissions()
      {
         if (_currentGame == null)
            throw new InvalidOperationException("Must load game before loading missions");
         
         IList<string> storyMissions = new List<string>(StoryMissionController.GetInstance().StoryMissionsForCampaign(_currentGame.GameCampaign.Id).Select((x) => x.Name));
         IList<string> sideMissions = new List<string>(_currentGame.ImpPlayer.PurchasedAgendas.Where((x) => x.Key.AgendaCardType == Agenda.AgendaType.SideMission).Select((x) => x.Key.Name).Union(_currentGame.SelectedSideMissions.Select((x) => x.Name)));
         IList<string> forcedMissions = new List<string>(_currentGame.ImpPlayer.PurchasedAgendas.Where((x) => x.Key.AgendaCardType == Agenda.AgendaType.ForcedMission).Select((x) => x.Key.Name).Union(SideMissionController.GetInstance().SideMissionsOfType(SideMission.MissionType.Forced).Select((x) => x.Name)));
         
         storyMissions.Insert(0, string.Empty);
         sideMissions.Insert(0, string.Empty);
         
         _availableSide1Combo.DataSource = sideMissions.ToList();
         _availableSide2Combo.DataSource = sideMissions.ToList();
         
         if (_currentGame.AvailableSideMission1 != null)
            _availableSide1Combo.SelectedItem = _currentGame.AvailableSideMission1;
         else
            _availableSide1Combo.SelectedIndex = 0;
         
         if (_currentGame.AvailableSideMission2 != null)
            _availableSide2Combo.SelectedItem = _currentGame.AvailableSideMission2;
         else
            _availableSide2Combo.SelectedIndex = 0;
         
         _availableSide1Combo.SelectedValueChanged += Handle_availableSide1ComboSelectedValueChanged;
         _availableSide2Combo.SelectedValueChanged += Handle_availableSide2ComboSelectedValueChanged;
         
         _missionListPanel.Controls.Clear();
         int ypos = 5;
         foreach (Mission m in _currentGame.Missions)
         {
            MissionPanel missionControl = null;
            switch (m.Type)
            {
            case Mission.MissionType.Story :
                  missionControl = new MissionPanel(storyMissions);
                  break;
            case Mission.MissionType.Side:
                  missionControl = new MissionPanel(sideMissions);
                  break;
            case Mission.MissionType.Forced:
                  missionControl = new MissionPanel(forcedMissions);
                  break;
            }
            missionControl.Location = new System.Drawing.Point(5, ypos);
            _missionListPanel.Controls.Add(missionControl);
            missionControl.LoadMission(m);
            missionControl.MissionChanged += HandleMissionChanged;
            ypos = missionControl.Location.Y + missionControl.Height + 5;
         }
         SetHasMissionChanges(false);
      }

      void Handle_availableSide2ComboSelectedValueChanged (object sender, EventArgs e)
      {
         _currentGame.AvailableSideMission2 = _availableSide2Combo.SelectedValue.ToString();
      }

      void Handle_availableSide1ComboSelectedValueChanged (object sender, EventArgs e)
      {
         _currentGame.AvailableSideMission1 = _availableSide1Combo.SelectedValue.ToString();
      }
      
      public void LoadGame(Game gameToView)
      {
         if (gameToView == null)
         {throw new ArgumentNullException("gameToView");}
         
         _currentGame = gameToView;
         this.Text = "Game - " + _currentGame.Id + " - " + _currentGame.GameDate.ToShortDateString();
         
         //Load missions
         LoadMissions();
         
         //Load players
         int ypos = 5;
         ImperialPlayerPanel impPlayerControl = new ImperialPlayerPanel();
         impPlayerControl.Location = new System.Drawing.Point(0, ypos);
         _playerListPanel.Controls.Add(impPlayerControl);
         impPlayerControl.LoadPlayer(_currentGame.ImpPlayer, _currentGame.SelectedAgendaSets, _currentGame.Heroes);
         impPlayerControl.AgendaPurchased += HandleImpPlayerControlAgendaPurchased;
         ypos = impPlayerControl.Location.Y + impPlayerControl.Height + 5;
         
         foreach (HeroPlayer h in _currentGame.Heroes)
         {
            HeroPlayerPanel playerControl = new HeroPlayerPanel();
            playerControl.Location = new System.Drawing.Point(0, ypos);
            _playerListPanel.Controls.Add(playerControl);
            playerControl.LoadPlayer(h, _currentGame.Heroes);
            playerControl.ItemPurchased += HandlePlayerControlItemPurchased;
            playerControl.ItemSold += HandlePlayerControlItemSold;
            ypos = playerControl.Location.Y + playerControl.Height + 5;
         }
         
         _currentGame.CreditsChanged += Handle_currentGameCreditsChanged;
         UpdateHeroCreditsPoolLabel();
      }

      bool HandleImpPlayerControlAgendaPurchased (PlayerPanelBase<ImperialPlayer> sender, Agenda a, EventArgs e)
      {
         bool allowed = a.AgendaCardType == Agenda.AgendaType.Secret || a.AgendaCardType == Agenda.AgendaType.Ongoing;
         if (!allowed)
         {
            if (_currentGame.GameCampaign.AllowForcedMissions && a.AgendaCardType == Agenda.AgendaType.ForcedMission)
            {
               AddForcedMission(a.Name);
               allowed = true;
            }
            else if (_currentGame.GameCampaign.AllowSideMissions && a.AgendaCardType == Agenda.AgendaType.SideMission)
               allowed = true;
         }
         
         return allowed;
      }
      
      void Handle_currentGameCreditsChanged (object sender, EventArgs e)
      {
         UpdateHeroCreditsPoolLabel();
      }

      bool HandlePlayerControlItemPurchased (PlayerPanelBase<HeroPlayer> sender, Item i, EventArgs e)
      {
         if (_currentGame.HeroCreditsPool - i.CreditCost >= 0)
         {
            _currentGame.HeroCreditsPool -= i.CreditCost;
            return true;
         }
         else
            return false;
      }
      
      void HandlePlayerControlItemSold (PlayerPanelBase<HeroPlayer> sender, Item i, EventArgs e)
      {
         _currentGame.HeroCreditsPool += i.CreditCost / 2;
      }

      public void UpdateHeroCreditsPoolLabel()
      {
         _heroCreditsLabel.Text = "Hero Credits: " + _currentGame.HeroCreditsPool;
      }

      private void AddForcedMission(string name)
      {
         IEnumerable<Mission> completedMissions = _currentGame.Missions.Where((x) => x.IsCompleted);
         if (completedMissions.Count() > 0)
         {
            Mission lastCompleted = completedMissions.Last();
            Mission forcedMission = new Mission(Mission.MissionType.Forced, lastCompleted.ThreatLevel);
            forcedMission.Name = name;
            _currentGame.Missions.Insert(_currentGame.Missions.IndexOf(lastCompleted) + 1, forcedMission);
            LoadMissions();
         }
         else
            MessageBox.Show("Must have at least 1 completed mission");
      }
      
      private void ApplyMissionChanges()
      {
         int rebelXpChangeTotal = 0;
         int impXpChangeTotal = 0;
         int creditsChangeTotal = 0;
         int influenceChangeTotal = 0;
         
         foreach (MissionPanel m in _missionListPanel.Controls)
         {
            if (m.HasChanges)
            {
               int rebelXpDiff = m.EnteredRebelXP - m.CurrentMission.RebelXPAwarded;
               int impXpDiff = m.EnteredImperialXP - m.CurrentMission.ImperialXpAwarded;
               int creditsDiff = m.EnteredCredits - m.CurrentMission.CreditsAwarded;
               int influenceDiff = m.EnteredInfluence - m.CurrentMission.InfluenceAwarded;
               
               m.ApplyChanges();
               rebelXpChangeTotal += rebelXpDiff;
               impXpChangeTotal += impXpDiff;
               creditsChangeTotal += creditsDiff;
               influenceChangeTotal += influenceDiff;
            }
         }
         
         //Apply diff values to players
         _currentGame.ImpPlayer.Xp += impXpChangeTotal;
         _currentGame.ImpPlayer.Influence += influenceChangeTotal;
         foreach (HeroPlayer p in _currentGame.Heroes)
         {
            p.Xp += rebelXpChangeTotal;
         }
         _currentGame.HeroCreditsPool += creditsChangeTotal;
         
         _currentGame.AvailableSideMission1 = _availableSide1Combo.SelectedValue.ToString();
         _currentGame.AvailableSideMission2 = _availableSide2Combo.SelectedValue.ToString();
         
         SetHasMissionChanges(false);
      }
      
      private void HandleMissionChanged(MissionPanel sender, EventArgs e)
      {
         SetHasMissionChanges(true);
      }
      
      private void SetHasMissionChanges(bool hasChanges)
      {
         _applyMissionChangesButton.Enabled = hasChanges;
         _cancelMissionChangesButton.Enabled = hasChanges;
         _addForcedMissionButton.Enabled = !hasChanges && _currentGame.GameCampaign.AllowForcedMissions;
      }
      
      public bool ValidateGame()
      {
         if (_applyMissionChangesButton.Enabled)
         {
            MessageBox.Show("You have unapplied mission changes.");
            return false;
         }
         
         return true;
      }
      
      public override void ExecuteSave ()
      {
         if (ValidateGame())
         {
            GameController.GetInstance().SaveGame(_currentGame);
            base.ExecuteSave ();
         }
      }
      
      public override void ExecuteCancel ()
      {
         if (_applyMissionChangesButton.Enabled)
         {
            if (MessageBox.Show("You have unapplied mission changes. Cancel changes?", "Cancel changes?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {return;}
         }
         base.ExecuteCancel ();
      }
   }
}

