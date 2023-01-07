using System;
using AFUPS.Data;
using AFUPS.SharedServices;
using Microsoft.AspNetCore.Components;

namespace AFUPS.SharedUI.Components.Archive
{
    public partial class ArchiveContentDialog : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [CascadingParameter]
        public UserArchive Archive { get; set; }

        [CascadingParameter]
        public UserUpload Upload { get; set; }

        [Inject]
        private IUserArchives userArchives { get; set; }

        private List<FileModel> Files { get; set; }

        public async void Show()
        {
            ResetDialogModel();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }


        private void ResetDialogModel()
        {
            try
            {
                var archive = userArchives.GetArchiveFile(Archive);
                Files = Archiver.ReadArchive(archive);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

