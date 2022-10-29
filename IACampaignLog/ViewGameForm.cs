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
      private PlayerPanel<ImperialPlayer> _impPlayerControl;


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
         IList<string> sideMissions = new List<string>(_currentGame.ImpPlayer.PurchasedAgendas.Where((x) => x.AgendaCardType == Agenda.AgendaType.SideMission).Select((x) => x.Name).Union(_currentGame.SelectedSideMissions.Where((x) => x.SideMissionType != SideMission.MissionType.Threat).Select((x) => x.Name)));
         IList<string> forcedMissions = new List<string>(_currentGame.ImpPlayer.PurchasedAgendas.Where((x) => x.AgendaCardType == Agenda.AgendaType.ForcedMission).Select((x) => x.Name).Union(SideMissionController.GetInstance().SideMissionsOfType(SideMission.MissionType.Forced).Select((x) => x.Name)));
         IList<ThreatMission> threatMissions = _currentGame.SelectedSideMissions.Where((x) => x is ThreatMission).Select((x) => (ThreatMission)x).ToList();
         
         storyMissions.Insert(0, string.Empty);
         sideMissions.Insert(0, string.Empty);
         threatMissions.Insert(0, new ThreatMission(-1, string.Empty, null));
         
         _availableSide1Combo.DataSource = sideMissions.ToList();
         _availableSide2Combo.DataSource = sideMissions.ToList();
         _availableThreatMission1Combo.DataSource = threatMissions.ToList();
         _availableThreatMission1Combo.DisplayMember = "Name";
         _availableThreatMission1Combo.ValueMember = "Id";
         _availableThreatMission2Combo.DataSource = threatMissions.ToList();
         _availableThreatMission2Combo.DisplayMember = "Name";
         _availableThreatMission2Combo.ValueMember = "Id";

         if (_currentGame.AvailableSideMission1 != null)
            _availableSide1Combo.SelectedItem = _currentGame.AvailableSideMission1;
         else
            _availableSide1Combo.SelectedIndex = 0;
         
         if (_currentGame.AvailableSideMission2 != null)
            _availableSide2Combo.SelectedItem = _currentGame.AvailableSideMission2;
         else
            _availableSide2Combo.SelectedIndex = 0;

         if (_currentGame.AvailableThreatMission1 != null)
            _availableThreatMission1Combo.SelectedItem = _currentGame.AvailableThreatMission1;
         else
            _availableThreatMission1Combo.SelectedIndex = 0;

         if (_currentGame.AvailableThreatMission2 != null)
            _availableThreatMission2Combo.SelectedItem = _currentGame.AvailableThreatMission2;
         else
            _availableThreatMission2Combo.SelectedIndex = 0;

         _availableSide1Combo.SelectedValueChanged += Handle_availableSide1ComboSelectedValueChanged;
         _availableSide2Combo.SelectedValueChanged += Handle_availableSide2ComboSelectedValueChanged;
         _availableThreatMission1Combo.SelectedValueChanged += Handle_availableThreatMission1ComboValueChanged;
         _availableThreatMission2Combo.SelectedValueChanged += Handle_availableThreatMission2ComboValueChanged;

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
                  missionControl = new MissionPanel(sideMissions.Union(threatMissions.Where((x) => x.Id >= 0).Select((x) => x.Name)).ToList());
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

      void Handle_availableThreatMission1ComboValueChanged(object sender, EventArgs e)
      {
         _currentGame.AvailableThreatMission1 = (ThreatMission)_availableThreatMission1Combo.SelectedItem;
         UpdateBaneRewards();
      }

      void Handle_availableThreatMission2ComboValueChanged(object sender, EventArgs e)
      {
         _currentGame.AvailableThreatMission2 = (ThreatMission)_availableThreatMission2Combo.SelectedItem;
         UpdateBaneRewards();
      }

      private void UpdateBaneRewards()
      {
         IList<Reward> banes = _currentGame.ImpPlayer.Rewards.Where((x) => x.RewardSubType == Reward.RewardType.Bane).ToList();
         foreach (Reward b in banes)
         {
            if ((_currentGame.AvailableThreatMission1 == null || b != _currentGame.AvailableThreatMission1.BaneReward)
            && (_currentGame.AvailableThreatMission2 == null || b != _currentGame.AvailableThreatMission2.BaneReward))
               _currentGame.ImpPlayer.Rewards.Remove(b);
         }
         if (_currentGame.AvailableThreatMission1 != null && _currentGame.AvailableThreatMission1.Id >= 0 && !banes.Contains(_currentGame.AvailableThreatMission1.BaneReward))
            _currentGame.ImpPlayer.Rewards.Add(_currentGame.AvailableThreatMission1.BaneReward);
         if (_currentGame.AvailableThreatMission2 != null && _currentGame.AvailableThreatMission2.Id >= 0 && !banes.Contains(_currentGame.AvailableThreatMission2.BaneReward))
            _currentGame.ImpPlayer.Rewards.Add(_currentGame.AvailableThreatMission2.BaneReward);

         _impPlayerControl.RefreshRewardListView();
      }

      public void LoadGame(Game gameToView)
      {
         if (gameToView == null)
         {throw new ArgumentNullException("gameToView");}
         
         _currentGame = gameToView;
         this.Text = "Game - " + _currentGame.Id + " - " + _currentGame.GameDate.ToShortDateString();
         _availableThreatsLabel.Visible = _availableThreatMission1Combo.Visible = 
            _availableThreatMission2Combo.Visible = _currentGame.GameCampaign.IncludeThreatMissions;
         if (!_currentGame.GameCampaign.IncludeThreatMissions)
         {
            _availableSidesLabel.Top += 10;
            _availableSide1Combo.Top += 10;
            _availableSide2Combo.Top += 10;
         }
         
         //Load missions
         LoadMissions();
         
         //Load players
         int ypos = 5;
         _impPlayerControl = new PlayerPanel<ImperialPlayer>();
         _impPlayerControl.Location = new System.Drawing.Point(0, ypos);
         _playerListPanel.Controls.Add(_impPlayerControl);
         _impPlayerControl.LoadImperialPlayer(_currentGame.ImpPlayer, _currentGame.SelectedAgendaSets);
         _impPlayerControl.AgendaPurchased += HandleImpPlayerControlAgendaPurchased;
         _impPlayerControl.AgendaDiscarded += HandleImpPlayerControlAgendaDiscarded;
         ypos = _impPlayerControl.Location.Y + _impPlayerControl.Height + 5;
         
         foreach (HeroPlayer h in _currentGame.Heroes)
         {
            PlayerPanel<HeroPlayer> playerControl = new PlayerPanel<HeroPlayer>();
            playerControl.Location = new System.Drawing.Point(0, ypos);
            _playerListPanel.Controls.Add(playerControl);
            playerControl.LoadHeroPlayer(h, _currentGame.Heroes);
            playerControl.ItemPurchased += HandlePlayerControlItemPurchased;
            playerControl.ItemSold += HandlePlayerControlItemSold;
            ypos = playerControl.Location.Y + playerControl.Height + 5;
         }
         
         _currentGame.CreditsChanged += Handle_currentGameCreditsChanged;
         UpdateHeroCreditsPoolLabel();
      }

      bool HandleImpPlayerControlAgendaPurchased (object sender, Agenda a, EventArgs e)
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
            {
               LoadMissions();
               allowed = true;
            }
         }
         
         return allowed;
      }

      private bool PlayerSpendCredits(int cost)
      {
         if (_currentGame.HeroCreditsPool - cost >= 0)
         {
            _currentGame.HeroCreditsPool -= cost;
            return true;
         }
         else
            return false;
      }

      bool HandleImpPlayerControlAgendaDiscarded (object sender, Agenda a, EventArgs e)
      {
         return PlayerSpendCredits(a.DiscardCost);
      }
      
      void Handle_currentGameCreditsChanged (object sender, EventArgs e)
      {
         UpdateHeroCreditsPoolLabel();
      }

      bool HandlePlayerControlItemPurchased (object sender, Item i, EventArgs e)
      {
         return PlayerSpendCredits(i.CreditCost);
      }

      bool HandlePlayerControlItemSold(object sender, Item i, EventArgs e)
      {
         if (i != null)
         {
            _currentGame.HeroCreditsPool += (i.CreditCost / 2);
            return true;
         }
         else
            return false;
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

