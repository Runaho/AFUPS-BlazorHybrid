# AFUPS-BlazorHybrid

![Apple iPhone 11 Pro Max Presentation 2](https://user-images.githubusercontent.com/16222645/203026089-614ada3a-4784-49e4-b295-9713b2a932cd.png)

[Google Play Store](https://play.google.com/store/apps/details?id=com.runaho.afup.mauiblazor) - AFUP

AFUP is a cross-platform application I developed to upload and share files anonymously.
You can simply archive your files and then upload the archived file to one of the providers.
You can also upload the archive to another provider if you want, archives and uploads are developed in a one-to-many structure.

I faced many difficulties in this implementation.
Asynchronously, no matter where you are in the application, your operations continue without stopping, such as loading or getting files into memory.
At the beginning, I was thinking of developing a version for the web, so the whole structure was developed as shared ui.
But since the current structure is sqlite and transistent file interface, it is not technically possible to work on the web.

I converted it into a web application as WASM in the first place, but since service providers reject requests from cross-origin, file uploads cannot be done with wasm.

In order to prevent this, it is necessary to bring the app to the server side, but this time, due to the functioning of the application, file upload processes should be client-specific and not transistent.

For this reason, I do not plan to publish the application on the web at the moment.
If you want to contribute to the development process, you can fork and then have your development merge. I will be constantly checking the project and I will continue to improve it in the future.

I added Auth0 for authentication, but in the future I plan to remove it and integrate  [Social Auth API](https://github.com/Runaho/SocialAuth.API) that I developed with .NET.
That's why the current version is a bit ahead of the published version.

<img width="1400" alt="Screenshot 2023-01-07 at 12 16 55" src="https://user-images.githubusercontent.com/16222645/211143869-b9e06c5c-2a57-4ca0-a48a-77c702c3df8a.png">


* * *
##### Resources
*   [HTML Design](https://www.creative-tim.com/product/paper-dashboard) - Creative Tim
*   [Icons](https://nucleoapp.com/free-icons) - 100 Nucleo Icon
*   [Upload Form](https://codepen.io/jotavejv/pen/bRdaVJ) - Codepen.io Jotavejv
*   [Blazor MAUI](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-6.0) - Creating cross platform app's on .NET Blazor.
*   [Blazor Shared UI](https://learn.microsoft.com/en-us/mobile-blazor-bindings/walkthroughs/shared-web-ui) - Razor Component Library
*   [Logo Maker](https://www.namecheap.com/logo-maker/app/) - Branding & Logo Designs
*   [Mockup APP](https://studio.app-mockup.com/) - Store Image Mockups

* * *

##### Default File Hosting Providers

*   [LetsUpload (Click for terms)](https://letsupload.cc/terms)
*   [Share-Online (Click for terms)](https://share-online.is/terms)
*   [MyFile (Click for terms)](https://myfile.is/terms)
*   [Mega Upload (Click for terms)](https://megaupload.nz/terms)
*   [FileChan (Click for terms)](https://filechan.org/terms)

* * *

##### Features
For data security reasons and for the privacy of the sharer, the data is kept completely on the device.
*   Upload multiple files via converting files to archive.
*   Save locally archives.
*   Easy share and re-upload/upload another provider.
*   Upload history & Calculating total upload size.
*   Archive history & Calculating total created archive size.
*   Zero data collecting.
*   Zero AD Policy.
*   Auth0

* * *
