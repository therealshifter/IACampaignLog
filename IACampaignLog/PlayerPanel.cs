using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class PlayerPanel<T> : Panel where T : Player
   {
      private bool _isReadOnly;
      private T _player;
      private IList<Item> _itemsList;
      private IList<ClassCard> _classCardsList;
      private IList<Reward> _rewardsList;
      private IList<Agenda> _agendaList;
      private IList<HeroPlayer> _otherPlayers;
      private Item _emptyItem;
      private Agenda _emptyAgenda;
      private ClassCard _emptyClassCard;
      private Reward _emptyReward;
      public delegate bool ItemEventHandler(object sender, Item i, EventArgs e);
      public event ItemEventHandler ItemPurchased;
      public event ItemEventHandler ItemSold;
      public delegate bool AgendaEventHandler(object sender, Agenda a, EventArgs e);
      public event AgendaEventHandler AgendaPurchased;
      public event AgendaEventHandler AgendaDiscarded;
      
      public PlayerPanel ()
      {
         _player = null;
         this.Initialise();
         _emptyItem = new Item(-1, string.Empty, 0, Item.ItemTier.I);
         _emptyClassCard = new ClassCard(-1, string.Empty, 0);
         _emptyReward = new Reward(-1, string.Empty, Reward.RewardType.Regular);
         _emptyAgenda = new Agenda(-1, string.Empty, 0, Agenda.AgendaType.SideMission, 0);
         _addItemButton.Click += Handle_addItemButtonClick;
         _addClassCardButton.Click += Handle_addClassCardButtonClick;
         _addRewardButton.Click += Handle_addRewardButtonClick;
         _classContextMenu.Opening += Handle_classContextMenu_Opening;
         _agendaContextMenu.Opening += Handle_agendaItemContextMenu_Opening;
         _itemContextMenu.Opening += Handle_agendaItemContextMenu_Opening;
      }

      private void Handle_classContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
      {
         ClassCard cc = null;
         if (_classCardListView.SelectedItems.Count == 1)
            cc = _player.PurchasedClassCards.Where((x) => x.Id.Equals(int.Parse(_classCardListView.SelectedItems[0].Text))).SingleOrDefault();
         if (cc == null || !cc.IsItem)
            e.Cancel = true;
      }
      
      private void Handle_agendaItemContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (_itemListView.SelectedItems.Count <= 0)
            e.Cancel = true;
      }
      
      void AddNewItem()
      {
         Item i = (Item)_addItemCombo.SelectedItem;
         if (i != _emptyItem)
         {
            if ((_player as HeroPlayer).PurchasedItems.Contains(i))
               MessageBox.Show("Item has already been purchased");
            else
            {
               if (ItemPurchased == null || ItemPurchased(this, i, EventArgs.Empty))
               {
                  (_player as HeroPlayer).PurchasedItems.Add(i);
                  _addItemCombo.SelectedItem = _emptyItem;
               }
               else
               {
                  MessageBox.Show("Not enough credits to purchase selected item.\nCost $" + i.CreditCost);
               }
            }
         }
      }

      void AddNewAgenda()
      {
         Agenda a = (Agenda)_addItemCombo.SelectedItem;
         if (a != _emptyAgenda)
         {
            if ((_player as ImperialPlayer).PurchasedAgendas.Contains(a))
               MessageBox.Show("Agenda already purchased");
            else if ((_player as ImperialPlayer).Influence - a.InfluenceCost < 0)
               MessageBox.Show("Not enough Influence to purchase selected Agenda.\nCost " + a.InfluenceCost + " Inf");
            else
            {
               (_player as ImperialPlayer).PurchasedAgendas.Add(a);
               if (AgendaPurchased != null && !AgendaPurchased(this, a, EventArgs.Empty))
               {
                  (_player as ImperialPlayer).PurchasedAgendas.Remove(a);
                  MessageBox.Show("Cannot purchase selected Agenda");
               }
               else
               {
                  (_player as ImperialPlayer).Influence -= a.InfluenceCost;
                  _addItemCombo.SelectedItem = _emptyAgenda;
               }
            }
         }
      }

      void Handle_addRewardButtonClick(object sender, EventArgs e)
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
         if (_player.GetType() == typeof(HeroPlayer))
         {
            AddNewItem();
         }
         else if (_player.GetType() == typeof(ImperialPlayer))
         {
            AddNewAgenda();
         }
         
         RefreshItemsAgendasListView();
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

      public void RefreshItemsAgendasListView()
      {
         _itemListView.Items.Clear();
         if (_player is HeroPlayer)
         {
            foreach (Item i in (_player as HeroPlayer).PurchasedItems)
            {
               ListViewItem lvi = new ListViewItem(new string[]{i.Id.ToString(), i.Name, i.CreditCost.ToString(), i.Tier.ToString()});
               _itemListView.Items.Add(lvi);
            }
         }
         else if (_player is ImperialPlayer)
         {
            foreach (Agenda i in (_player as ImperialPlayer).PurchasedAgendas)
            {
               ListViewItem lvi = new ListViewItem(new string[]{i.Id.ToString(), i.Name, i.InfluenceCost.ToString()});
               _itemListView.Items.Add(lvi);
            }
         }
         
         if (_itemListView.Items.Count > 0)
         {
            _itemListView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            _itemListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            if (_player is HeroPlayer)
               _itemListView.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.HeaderSize);
            _itemListView.Columns[0].Width = 0;
         }
      }
      
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
            ListViewItem lvi = new ListViewItem(new string[]{r.Id.ToString(), r.Name, r.RewardSubType.ToString()});
            _rewardListView.Items.Add(lvi);
         }
      }
      
      public void LoadHeroPlayer(T playerData, IList<HeroPlayer> heroes)
      {
         LoadPlayer(playerData, null, heroes);
      }
      
      public void LoadImperialPlayer(T playerData, IList<CardSet<Agenda>> selectedAgendaSets)
      {
         LoadPlayer(playerData, selectedAgendaSets, null);
      }
      
      private void LoadPlayer(T playerData, IList<CardSet<Agenda>> selectedAgendaSets, IList<HeroPlayer> heroes)
      {
         if (playerData == null)
            throw new ArgumentNullException("playerData");
         
         //Set basic player info
         _player = playerData;
         _playerNameLabel.Text = _player.Name;
         _heroNameLabel.Text = _player.PlayerCharacter.Name;
         _player.XpChanged += Handle_playerXpChanged;
         UpdateXP();
         
         //Set default item purchase data
         if (_player is HeroPlayer)
         {
            this.BackColor = System.Drawing.Color.LightSalmon;
            _influenceLabel.Visible = false;
            _itemsList = new List<Item>(ItemController.GetInstance().ListOfT);
            _itemsList.Insert(0, _emptyItem);
            _addItemCombo.DataSource = _itemsList;
            _addItemCombo.DisplayMember = "NameWithTier";
            (_player as HeroPlayer).ItemTraded += HandleItemTrade;
         }
         else if (_player is ImperialPlayer)
         {
            this.BackColor = System.Drawing.Color.LightBlue;
            (_player as ImperialPlayer).InfluenceChanged += HandleInfluenceChanged;
            _agendaList = new List<Agenda>(from CardSet<Agenda> cs in selectedAgendaSets
                                           from Agenda a in cs.ListOfT
                                           select a);
            _agendaList.Insert(0, _emptyAgenda);
            _addItemCombo.DataSource = _agendaList;
            _addItemCombo.DisplayMember = "Name";
            (_player as ImperialPlayer).AgendaDiscarded += HandleDiscardAgenda;
         }
         _addItemCombo.ValueMember = "Id";
         
         UpdateInfluence();
         
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
            _otherPlayers = heroes.Where((x) => x != _player).ToList();
            foreach (HeroPlayer h in _otherPlayers)
            {
               ToolStripMenuItem m = new ToolStripMenuItem(h.PlayerCharacter.Name, null, HandleItemTrade);
               m.Tag = h;
               _tradeItemMenuItem.DropDownItems.Add(m);
            }
            _itemListView.ContextMenuStrip = _itemContextMenu;
         }
         else
         {
            _itemListView.ContextMenuStrip = _agendaContextMenu;
         }
         
         //populate item list
         RefreshItemsAgendasListView();

         //populate class card list
         _classCardListView.ContextMenuStrip = _classContextMenu;
         RefreshClassCardListView();
         
         //populate reward list
         RefreshRewardListView();
      }
      
      void HandleItemTrade(object sender, EventArgs e)
      {
         if (sender is ToolStripMenuItem && (sender as ToolStripMenuItem).Tag is HeroPlayer && _itemListView.SelectedItems.Count > 0)
         {
            Item tradeMe = (_player as HeroPlayer).PurchasedItems.Where((x) => x.Id.Equals(int.Parse(_itemListView.SelectedItems[0].Text))).SingleOrDefault();
            if (tradeMe != null)
            {
               ((HeroPlayer)(sender as ToolStripMenuItem).Tag).PurchasedItems.Add(tradeMe);
               (_player as HeroPlayer).PurchasedItems.Remove(tradeMe);
               RefreshItemsAgendasListView();
               ((HeroPlayer)(sender as ToolStripMenuItem).Tag).RaiseItemTradedEvent(this);
            }
         }
         RefreshItemsAgendasListView();
      }

      void HandleInfluenceChanged (ImperialPlayer sender, EventArgs e)
      {
         UpdateInfluence();
      }

      void Handle_playerXpChanged (Player sender, EventArgs e)
      {
         UpdateXP();
      }

      void HandleSellItem(object sender, EventArgs e)
      {
         if (_itemListView.SelectedItems.Count > 0)
         {
            Item sellMe = (_player as HeroPlayer).PurchasedItems.Where((x) => x.Id.Equals(int.Parse(_itemListView.SelectedItems[0].Text))).SingleOrDefault();
            if (sellMe != null && MessageBox.Show(String.Format("Sell item for ${0}?", sellMe.CreditCost / 2), "Sell Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               (_player as HeroPlayer).PurchasedItems.Remove(sellMe);
               RefreshItemsAgendasListView();
               RaiseItemSoldEvent(this, sellMe);
            }
         }
      }

      void HandleSellClassItem(object sender, EventArgs e)
      {
         if (_classCardListView.SelectedItems.Count > 0)
         {
            ClassCard sellMe = _player.PurchasedClassCards.Where((x) => x.Id.Equals(int.Parse(_classCardListView.SelectedItems[0].Text))).FirstOrDefault();
            if (sellMe != null && MessageBox.Show("Sell item for $50?", "Sell Class Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               _player.PurchasedClassCards.Remove(sellMe);
               RefreshClassCardListView();
               RaiseItemSoldEvent(this, new Item(-1, "Class Item", 100, Item.ItemTier.I));
            }
         }
      }

      void RaiseItemSoldEvent(object sender, Item i)
      {
         if (this.ItemSold != null)
            ItemSold(sender, i, EventArgs.Empty);
      }

      void HandleDiscardAgenda(object sender, EventArgs e)
      {
         if (_itemListView.SelectedItems.Count > 0)
         {
            Agenda discardMe = (_player as ImperialPlayer).PurchasedAgendas.Where((x) => x.Id.Equals(int.Parse(_itemListView.SelectedItems[0].Text))).SingleOrDefault();
            if (discardMe != null && MessageBox.Show(String.Format("Discard Agenda card? This will cost the heroes ${0}", discardMe.DiscardCost), "Discard Agenda", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               if (AgendaDiscarded == null || AgendaDiscarded(this, discardMe, EventArgs.Empty))
               {
                  (_player as ImperialPlayer).PurchasedAgendas.Remove(discardMe);
                  RefreshItemsAgendasListView();
                  (_player as ImperialPlayer).RaiseAgendaDiscardedEvent(this);
               }
               else
               {
                  MessageBox.Show("Not enough credits to discard selected Agenda.\nCost $" + discardMe.DiscardCost);
               }
            }
         }
         RefreshItemsAgendasListView();
      }

      public void UpdateXP()
      {
         _xpLabel.Text = _player.Xp.ToString() + " XP";
      }
      
      public void UpdateInfluence()
      {
         
         if (_player is ImperialPlayer)
         {
            _influenceLabel.Text = (_player as ImperialPlayer).Influence.ToString() + " Inf";
         }
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

