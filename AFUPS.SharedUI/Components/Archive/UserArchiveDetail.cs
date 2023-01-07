using System;
using AFUPS.Data;
using AFUPS.SharedServices;
using AFUPS.SharedUI.Components.Provider;
using AFUPS.SharedUI.Pages;
using Microsoft.AspNetCore.Components;

namespace AFUPS.SharedUI.Components.Archive
{
    public partial class UserArchiveDetail : ComponentBase
    {
        [Parameter]
        public ArchiveContentDialog ArchiveContentDialog { get; set; }

        [Parameter]
        public List<UploaderProvider> uploaderProviders { get; set; } = new List<UploaderProvider>();

        [Parameter]
        public EventCallback Removed { get; set; }

        [Parameter]
        public bool CanBeEreased { get; set; }

        private UserArchive _archive { get; set; }

        [Parameter]
        public UserArchive archive
        {
            get
            {
                return _archive;
            }
            set
            {
                _archive = value;
                upload = UserUploads.GetUserUploadByArchiveId((int)archive.ArchiveID);
                StateHasChanged();
            }
        }

        [Inject]
        private IUserArchives userArchives { get; set; }

        [Inject]
        private IUserUploads UserUploads { get; set; }

        [Inject]
        private IFileProcessManager fileProcessManager { get; set; }

        [Inject]
        private IPromptService promptService { get; set; }

        private UserUpload upload { get; set; }

        private UserArchive SelectedArchive { get; set; }

        private string SelectedProvider { get; set; }

        private bool archiveSelected => (SelectedArchive != null) && archive == SelectedArchive;

        [Parameter]
        public EventCallback<string> ShareUri { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                if (fileProcessManager.GetState() == ProcessManagerState.Uploading)
                {
                    var genArch = fileProcessManager.GetGeneratedArchive();
                    if (genArch?.ArchiveFileID == archive.ArchiveFileId)
                    {
                        Control();
                    }
                }
                StateHasChanged();
            }
        }

        private async void Control()
        {
            if (fileProcessManager.GetState() == ProcessManagerState.Uploading)
            {
                await Task.Delay(1000);
                Control();
            }
            else if (archive.ArchiveID != null)
            {
                upload = UserUploads.GetUserUploadByArchiveId((int)archive.ArchiveID);
                StateHasChanged();
            }
        }

        protected async void RemoveArchive(UserArchive archive)
        {
            var status = await promptService.ShowConfirmationAsync("Archive Deletion.","Are you sure removing this archive file. You cannot be able to re-upload.");
            if (!status)
                return;

            if (archive != null)
            {
                var archiveFile = userArchives.GetArchiveFile(archive);
                userArchives.RemoveArchiveFile(archiveFile);
                userArchives.RemoveUserArchive(archive);
            }

            Removed.InvokeAsync();
            StateHasChanged();
        }

        private void OpenDialog(UserArchive userArchive)
        {
            ArchiveContentDialog.Archive = userArchive;
            ArchiveContentDialog.Show();
            StateHasChanged();
        }

        private void SelectForUpload(UserArchive archive, string value)
        {
            SelectedArchive = archive;
            SelectedProvider = value;
            StateHasChanged();
        }

        private async Task ReUploadAsync(UserArchive archive)
        {
            var archiveFile = userArchives.GetArchiveFile(archive);

            fileProcessManager.SetGeneratedArchive(archive, archiveFile, archiveFile.Guid);
            await fileProcessManager.StartUploadAsync(Convert.ToInt32(SelectedProvider));
            Control();
            StateHasChanged();
        }

    }
}

