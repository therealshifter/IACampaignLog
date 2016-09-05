using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class ImperialPlayerPanel : PlayerPanelBase<ImperialPlayer>
   {
      private IList<Agenda> _agendaList;
      private Agenda _emptyAgenda;
      public delegate bool AgendaPurchasedEventHandler(PlayerPanelBase<ImperialPlayer> sender, Agenda a, EventArgs e);
      public event AgendaPurchasedEventHandler AgendaPurchased;
      
      public ImperialPlayerPanel () : base()
      {
         this.Initialise();
         _emptyAgenda = new Agenda(-1, string.Empty, 0, Agenda.AgendaType.SideMission);
      }

      protected override void PerformAddItem ()
      {
         Agenda a = (Agenda)_addItemCombo.SelectedItem;
         if (a != _emptyAgenda)
         {
            if (CurrentPlayer.PurchasedAgendas.ContainsKey(a))
               MessageBox.Show("Agenda already purchased");
            else if (CurrentPlayer.Influence - a.InfluenceCost < 0)
               MessageBox.Show("Not enough Influence to purchase selected Agenda.\nCost " + a.InfluenceCost + " Inf");
            else
            {
               CurrentPlayer.PurchasedAgendas.Add(a, null);
               if (AgendaPurchased != null && !AgendaPurchased(this, a, EventArgs.Empty))
               {
                  CurrentPlayer.PurchasedAgendas.Remove(a);
                  MessageBox.Show("Cannot purchase selected Agenda");
               }
               else
               {
                  CurrentPlayer.Influence -= a.InfluenceCost;
                  _addItemCombo.SelectedItem = _emptyAgenda;
               }
            }
         }
      }

      public override void RefreshItemsListView()
      {
         _itemListView.Items.Clear();
         foreach (KeyValuePair<Agenda, Character> i in CurrentPlayer.PurchasedAgendas)
         {
            string target = i.Value == null ? string.Empty : i.Value.Name;
            ListViewItem lvi = new ListViewItem(new string[]{i.Key.Id.ToString(), i.Key.Name, i.Key.InfluenceCost.ToString(), target});
            _itemListView.Items.Add(lvi);
         }
         
         if (_itemListView.Items.Count > 0)
         {
            _itemListView.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            _itemListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            _itemListView.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
            _itemListView.Columns[0].Width = 0;
         }
      }
      
      public void LoadPlayer(ImperialPlayer playerData, IList<CardSet<Agenda>> selectedAgendaSets, IList<HeroPlayer> heroes)
      {
         this.BackColor = System.Drawing.Color.LightBlue;
         playerData.InfluenceChanged += HandleInfluenceChanged;
         _agendaList = new List<Agenda>(from CardSet<Agenda> cs in selectedAgendaSets
                                        from Agenda a in cs.ListOfT
                                        select a);
         _agendaList.Insert(0, _emptyAgenda);
         _addItemCombo.DataSource = _agendaList;
         _addItemCombo.DisplayMember = "Name";
         _addItemCombo.ValueMember = "Id";
         
         base.LoadPlayer(playerData, heroes);
         
         UpdateInfluence();
      }
      
      protected override void PerformItemAction(HeroPlayer performActionOn)
      {
         Agenda setMyTarget = CurrentPlayer.PurchasedAgendas.Where((x) => x.Key.Id.Equals(int.Parse(_itemListView.SelectedItems[0].Text))).Select((x) => x.Key).SingleOrDefault();
         if (setMyTarget != null)
         {
            CurrentPlayer.PurchasedAgendas[setMyTarget] = performActionOn.PlayerCharacter;
         }
      }

      void HandleInfluenceChanged (ImperialPlayer sender, EventArgs e)
      {
         UpdateInfluence();
      }

      public void UpdateInfluence()
      {
         _influenceLabel.Text = CurrentPlayer.Influence.ToString() + " Inf";
      }
   }
}

