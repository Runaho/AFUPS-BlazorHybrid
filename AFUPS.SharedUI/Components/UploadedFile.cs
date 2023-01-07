using System;
using AFUPS.Data;
using AFUPS.SharedServices;
using AFUPS.SharedUI.Components.Archive;
using Microsoft.AspNetCore.Components;

namespace AFUPS.SharedUI.Components
{
    public partial class UploadedFile : ComponentBase
    {
        [Parameter]
        public UserUpload userUpload { get; set; }

        [Parameter]
        public ArchiveContentDialog ArchiveDetailModal { get; set; }

        [Parameter]
        public EventCallback Removed { get; set; }

        [Parameter]
        public bool CanBeEreased { get; set; }

        [Parameter]
        public EventCallback<string> ShareUri { get; set; }

        [Inject]
        private IUserArchives userArchives { get; set; }

        [Inject]
        private IUserUploads userUploads { get; set; }

        [Inject]
        private IPromptService promptService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            StateHasChanged();
        }

        private async void Remove()
        {
            var status = await promptService.ShowConfirmationAsync("Upload Deletion.", "Are you sure you want to delete the record?\n Please be aware that this will only make it invisible to you.");
            if (!status)
                return;

            userUploads.Remove((int)userUpload.UploadID);
            await Removed.InvokeAsync();

        }
        private async void OpenDialog()
        {
            if (ArchiveDetailModal == null)
                return;

            ArchiveDetailModal.Upload = userUpload;
            ArchiveDetailModal.Archive = userArchives.Get().FirstOrDefault(f => f.ArchiveID == userUpload.ArchiveID);

            if (ArchiveDetailModal.Archive != null)
            {
                ArchiveDetailModal.Show();
            }
            else
            {
                await promptService.ShowAlertAsync("Archive Detail", "File cannot be readed. Missing or removed.");
            }
            StateHasChanged();
        }
    }
}

