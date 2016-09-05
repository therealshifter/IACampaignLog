using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public abstract partial class PlayerPanelBase<T> : Panel where T : Player
   {
      private bool _isReadOnly;
      private T _player;
      private IList<ClassCard> _classCardsList;
      private IList<Reward> _rewardsList;
      private IList<HeroPlayer> _otherPlayers;
      private ClassCard _emptyClassCard;
      private Reward _emptyReward;
      
      public PlayerPanelBase ()
      {
         _player = null;
         this.Initialise();
         _emptyClassCard = new ClassCard(-1, string.Empty, 0);
         _emptyReward = new Reward(-1, string.Empty);
         _addItemButton.Click += Handle_addItemButtonClick;
         _addClassCardButton.Click += Handle_addClassCardButtonClick;
         _addRewardButton.Click += Handle_addRewardButtonClick;
      }

      void Handle_addRewardButtonClick (object sender, EventArgs e)
      {
         Reward r = (Reward)_addRewardCombo.SelectedItem;
         if (r != _emptyReward)
         {
            if (_player.Rewards.Contains(r))
               MessageBox.Show("Reward already obtained");
            else
            {
               _player.Rewards.Add(r);
               _addRewardCombo.SelectedItem = _emptyReward;
            }
         }
         
         RefreshRewardListView();
      }

      void Handle_addItemButtonClick (object sender, EventArgs e)
      {
         PerformAddItem();
         
         RefreshItemsListView();
      }

      void Handle_addClassCardButtonClick (object sender, EventArgs e)
      {
         ClassCard card = (ClassCard)_addClassCombo.SelectedItem;
         if (card != _emptyClassCard)
         {
            if (_player.PurchasedClassCards.Contains(card))
               MessageBox.Show("Class Card already purcahsed");
            else if (_player.Xp - card.XpCost < 0)
               MessageBox.Show("Not enough XP to purchase Class card.\nCost " + card.XpCost + " XP");
            else
            {
               _player.Xp -= card.XpCost;
               _player.PurchasedClassCards.Add(card);
               _addClassCombo.SelectedItem = _emptyClassCard;
            }
         }
         
         RefreshClassCardListView();
      }
      
      public T CurrentPlayer {get{return _player;}}

      public abstract void RefreshItemsListView();
      protected abstract void PerformAddItem();
      protected abstract void PerformItemAction(HeroPlayer performActionOn);
      
      public void RefreshClassCardListView()
      {
         _classCardListView.Items.Clear();
         foreach (ClassCard c in _player.PurchasedClassCards)
         {
            ListViewItem lvi = new ListViewItem(new string[]{c.Id.ToString(), c.Name, c.XpCost.ToString()});
            _classCardListView.Items.Add(lvi);
         }
         
         if (_classCardListView.Items.Count > 0)
         {
            _classCardListView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            _classCardListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            _classCardListView.Columns[0].Width = 0;
         }
      }
      
      public void RefreshRewardListView()
      {
         _rewardListView.Items.Clear();
         foreach (Reward r in _player.Rewards)
         {
            ListViewItem lvi = new ListViewItem(new string[]{r.Id.ToString(), r.Name});
            _rewardListView.Items.Add(lvi);
         }
         
         if (_rewardListView.Items.Count > 0)
         {
            _rewardListView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            _rewardListView.Columns[0].Width = 0;
         }
      }
      
      protected virtual void LoadPlayer(T playerData, IList<HeroPlayer> heroes)
      {
         if (playerData == null)
            throw new ArgumentNullException("playerData");
         
         //Set basic player info
         _player = playerData;
         _playerNameLabel.Text = _player.Name;
         _heroNameLabel.Text = _player.PlayerCharacter.Name;
         _player.XpChanged += Handle_playerXpChanged;
         UpdateXP();
         
         //Set default class card purchase data
         _classCardsList = new List<ClassCard>(_player.PlayerClass.ListOfT);
         _classCardsList.Insert(0, _emptyClassCard);
         _addClassCombo.DataSource = _classCardsList;
         _addClassCombo.DisplayMember = "Name";
         _addClassCombo.ValueMember = "Id";
         
         //Set default reward purchase data
         _rewardsList = new List<Reward>(RewardController.GetInstance().ListOfT);
         _rewardsList.Insert(0, _emptyReward);
         _addRewardCombo.DataSource = _rewardsList;
         _addRewardCombo.DisplayMember = "Name";
         _addRewardCombo.ValueMember = "Id";
         
         //Set up item trading menu items
         if (heroes != null)
         {
            _otherPlayers = heroes;
            foreach (HeroPlayer h in _otherPlayers)
            {
               MenuItem m = new MenuItem(h.PlayerCharacter.Name, HandleItemAction);
               m.Tag = h;
               _itemActionMenuItem.MenuItems.Add(m);
            }
         }
         _itemListView.ContextMenu = _itemContextMenu;
         
         //populate item list
         RefreshItemsListView();
         
         //populate class card list
         RefreshClassCardListView();
         
         //populate reward list
         RefreshRewardListView();
      }
      
      protected void HandleItemAction(object sender, EventArgs e)
      {
         if (sender is MenuItem && ((MenuItem)sender).Tag is HeroPlayer && _itemListView.SelectedItems.Count > 0)
            PerformItemAction((HeroPlayer)((MenuItem)sender).Tag);
         RefreshItemsListView();
      }

      void Handle_playerXpChanged (Player sender, EventArgs e)
      {
         UpdateXP();
      }
      
      public void UpdateXP()
      {
         _xpLabel.Text = _player.Xp.ToString() + " XP";
      }
      
      public bool ReadOnly {
         get
         {return _isReadOnly;}
         set
         {
            _isReadOnly = value;
         }
      }
      
   }
}

