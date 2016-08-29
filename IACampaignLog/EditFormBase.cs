using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class EditFormBase : Form
   {
      public EditFormBase ()
      {
         Initialise();
         _cancelButton.Click += Handle_cancelButtonClick;
         _saveButton.Click += Handle_saveButtonClick;
      }
      
      public virtual void ExecuteSave()
      {
         CloseForm();
      }
  
      public virtual void ExecuteCancel()
      {
         CloseForm();
      }
      
      void Handle_saveButtonClick (object sender, EventArgs e)
      {
         ExecuteSave();
      }
      
      void CloseForm()
      {
         _cancelButton.Click -= Handle_cancelButtonClick;
         _saveButton.Click -= Handle_saveButtonClick;
         this.Close();
      }

      void Handle_cancelButtonClick (object sender, EventArgs e)
      {
         ExecuteCancel();
      }
   }
}

