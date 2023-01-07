using System;
using System.Text.RegularExpressions;
using AFUPS.Data;
using AFUPS.SharedServices;
using AFUPS.SharedUI.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AFUPS.SharedUI.Components.Uploader
{
    public partial class MultipleFileUploader : ComponentBase
    {
        private const double _maxFileSize = 10;
        private double maxFileSize = ByteSizeLib.ByteSize.FromGibiBytes(_maxFileSize).Bytes;

        private List<IBrowserFile> SelectedFiles = new();

        [CascadingParameter]
        protected MainLayout mainLayout { get; set; }

        protected InputFile filePicker => mainLayout?.filePicker;


        private int maxAllowedFiles = 100;
        private bool FirstTime = true;

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        public IFileProcessManager fileProcessManager { get; set; }

        [Inject]
        private IUploaderProviders UploaderProviders { get; set; }

        [Inject]
        private IConsent consentService { get; set; }


        private UploaderBody uploaderBody { get; set; }

        [Parameter]
        public EventCallback FileSelected { get; set; }

        [Inject]
        private IPromptService promptService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                mainLayout.updateState = new EventCallbackFactory().Create(this, () =>
                {
                    StateHasChanged();
                    //uploaderBody.ReRender();
                });
                mainLayout.StartTimer();
            }
        }

        private async Task GenerateArchiveAsync()
        {
            mainLayout.StartTimer(100);

            var archiveName = await promptService.ShowPromptAsync("Archive", "Do you wanna set an name for your archive?", "Yes", "No Create Random");

            if (!string.IsNullOrEmpty(archiveName))
            {
                archiveName = Regex.Replace(archiveName, "[^a-zA-Z0-9_. ığüşöçİĞÜŞÖÇ123456789]+", string.Empty, RegexOptions.Compiled);
                fileProcessManager.SetArchiveName(archiveName);
            }

            await fileProcessManager.GenerateArchiveAsync();
            StateHasChanged();
        }

        private async Task StartUploading()
        {
            int providerId = uploaderBody.SelectedProviderId;

            mainLayout.StartTimer();

            await fileProcessManager.StartUploadAsync(providerId, "");

            StateHasChanged();
        }

        private void UploadingControl()
        {
            if (fileProcessManager.GetState() != ProcessManagerState.Uploading)
            {
                StateHasChanged();
                mainLayout.DisposeTimer();
            }
        }

        private void RemoveFile(FileConvertingProcess file)
        {
            fileProcessManager.RemoveFile(file);
        }


        public async Task OpenFileSelector()
        {
            SelectedFiles.Clear();
            await JsRuntime.InvokeAsync<object>("TriggerFileInput", filePicker.Element);
        }
    }
}

