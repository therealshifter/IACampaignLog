using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class HeroPlayerPanel : PlayerPanelBase<HeroPlayer>
   {
      private IList<Item> _itemsList;
      private Item _emptyItem;
      public delegate bool ItemPurchasedEventHandler(PlayerPanelBase<HeroPlayer> sender, Item i, EventArgs e);
      public event ItemPurchasedEventHandler ItemPurchased;
      public delegate void ItemSoldEventHandler(PlayerPanelBase<HeroPlayer> sender, Item i, EventArgs e);
      public event ItemSoldEventHandler ItemSold;
      
      
      public HeroPlayerPanel () : base()
      {
         this.Initialise();
         _emptyItem = new Item(-1, string.Empty, 0, Item.ItemTier.I);
      }

      protected override void PerformAddItem ()
      {
         Item i = (Item)_addItemCombo.SelectedItem;
         if (i != _emptyItem)
         {
            if (CurrentPlayer.PurchasedItems.Contains(i))
               MessageBox.Show("Item has already been purchased");
            else
            {
               if (ItemPurchased == null || ItemPurchased(this, i, EventArgs.Empty))
               {
                  CurrentPlayer.PurchasedItems.Add(i);
                  _addItemCombo.SelectedItem = _emptyItem;
               }
               else
               {
                  MessageBox.Show("Not enough credits to purchase selected item.\nCost $" + i.CreditCost);
               }
            }
         }
      }

      public override void RefreshItemsListView()
      {
         _itemListView.Items.Clear();
         foreach (Item i in CurrentPlayer.PurchasedItems)
         {
            ListViewItem lvi = new ListViewItem(new string[]{i.Id.ToString(), i.Name, i.CreditCost.ToString(), i.Tier.ToString()});
            _itemListView.Items.Add(lvi);
         }
         
         if (_itemListView.Items.Count > 0)
         {
            _itemListView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            _itemListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            _itemListView.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.HeaderSize);
            _itemListView.Columns[0].Width = 0;
         }
      }
      
      public new void LoadPlayer(HeroPlayer playerData, IList<HeroPlayer> heroes)
      {
         //Set default item purchase data
         this.BackColor = System.Drawing.Color.LightSalmon;
         _itemsList = new List<Item>(ItemController.GetInstance().ListOfT);
         _itemsList.Insert(0, _emptyItem);
         _addItemCombo.DataSource = _itemsList;
         playerData.ItemTraded += HandleItemAction;
         _addItemCombo.DisplayMember = "NameWithTier";
         _addItemCombo.ValueMember = "Id";
         
         base.LoadPlayer(playerData, heroes.Where((x) => x != playerData).ToList());
         
         _sellMenuItem.Click += Handle_sellMenuItemClick;
      }

      void Handle_sellMenuItemClick (object sender, EventArgs e)
      {
         if (sender is MenuItem && _itemListView.SelectedItems.Count > 0)
         {
            Item sellMe = CurrentPlayer.PurchasedItems.Where((x) => x.Id == int.Parse(_itemListView.SelectedItems[0].Text)).SingleOrDefault();
            if (sellMe != null)
            {
               CurrentPlayer.PurchasedItems.Remove(sellMe);
               if (ItemSold != null)
                  ItemSold(this, sellMe, EventArgs.Empty);
            }
         }
         RefreshItemsListView();
      }
      
      protected override void PerformItemAction(HeroPlayer performActionOn)
      {
         Item tradeMe = CurrentPlayer.PurchasedItems.Where((x) => x.Id.Equals(int.Parse(_itemListView.SelectedItems[0].Text))).SingleOrDefault();
         if (tradeMe != null)
         {
            performActionOn.PurchasedItems.Add(tradeMe);
            CurrentPlayer.PurchasedItems.Remove(tradeMe);
            RefreshItemsListView();
            performActionOn.RaiseItemTradedEvent(this);
         }
      }      
   }
}

