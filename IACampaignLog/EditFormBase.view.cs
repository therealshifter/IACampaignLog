using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class EditFormBase : Form
   {
      private Button _saveButton;
      private Button _cancelButton;
      private Panel _bottomButtonsPanel;
      
      private void Initialise ()
      {
         this.Size = new System.Drawing.Size(500, 500);
         
         _bottomButtonsPanel = new Panel();
         _bottomButtonsPanel.Dock = DockStyle.Bottom;
         _bottomButtonsPanel.Height = 40;
         //Save button
         _saveButton = new Button();
         _saveButton.Text = "Save";
         _saveButton.Size = new System.Drawing.Size(50, 30);
         _saveButton.Location = new System.Drawing.Point(90, 5);
         _saveButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         _bottomButtonsPanel.Controls.Add(_saveButton);
         //Cancel button
         _cancelButton = new Button();
         _cancelButton.Text = "Cancel";
         _cancelButton.Size = new System.Drawing.Size(50, 30);
         _cancelButton.Location = new System.Drawing.Point(145, 5);
         _cancelButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         _bottomButtonsPanel.Controls.Add(_cancelButton);
         
         this.Controls.Add(_bottomButtonsPanel);
      }
   }
}

