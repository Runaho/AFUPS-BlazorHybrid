``NOTE : I must state that this project is very old now. There are much better alternatives to both the codes and the methods, and also new versions of blazor has many improvement about problems I encountered, and I will most likely rewrite the project again in a free time. If a suitable provider or an opportunity comes across, I think I will evaluate it.``

Hello, first of all, I would like to tell you why I entered this challenging process.

[Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)  has always struck me as a great technology.  
The idea of publishing native  [Blazor applications with MAUI](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-7.0&pivots=windows)  is incredible.I immediately said I had to learn this technology and started researching. I wrote small applications, to-do app etc. But then I asked myself why don’t I write an application that people will use. The plan was now ready, I was going to write an app, submit it to all the stores, create a reference for myself and literally take over every stage of an app development from start to finish.

Normally my job is just to develop apps, I have written mobile apps in the past but I have never published them myself.

## What will the app do?

Even though the idea of development was good, if I was going to create a product, I had to figure out what the application would do.At this point I wanted to move on from data security and file sharing over the internet. I discovered services where I can upload and share files for free.

- [LetsUpload](https://letsupload.cc/terms)
- [Share-Online](https://share-online.is/terms)
- [MyFile](https://myfile.is/terms)
- [Mega Upload](https://megaupload.nz/terms)
- [FileChan](https://filechan.org/terms)

They all work the same on each other’s api side, so there is a possibility that I can make the user choose where to upload the files.

And the application was prepared in my head.  
Files will be uploaded and archived, a service provider will upload and store the archive over the internet. It will then be able to look at what it has shared in the past or re-upload an archive to another provider.

Looking at it this way, it seems like an incredibly easy and short process, but it took me almost 3 months and i rewrote the app 4 times from scratch. It is much more difficult than we thought to make a complete product from scratch on your own.

## How will it look like?

The biggest problem for me has always been how an app will look. As a programmer, designing has never been something I wanted to do.

Fortunately, there are resources where I can find designs for free, but it took me a long time just to search for them. Of course, the problem doesn’t end when you find a template, you need to translate it into your app, into the template you have in mind.  
You have to adjust your logo, tweak the design and make your app’s features interactive.

[Click here for the dashboard.](https://www.creative-tim.com/product/paper-dashboard)

This design is nice but for me the free version didn’t have enough content so I started looking for an uploader.

I found a nice uploader on codepen.io and I had to seriously change it.

[Upload CSS Animation UI (codepen.io)](https://codepen.io/jotavejv/pen/bRdaVJ)

Now that the basic content is ready, let’s start coding.

![coding](https://miro.medium.com/v2/resize:fit:1400/1*pUxwZN7DAMmm33-shPUhSg.jpeg)

comparison of the  [design](https://www.creative-tim.com/product/paper-dashboard)  and the finished application

## Getting used to a new technology

Yes, everyone knows that new technology means new ideas and new beauties, but it also means new problems and you can be sure that there are tons of them here.

After compiling once, getting errors in recompiling, cleaning the project and compiling again.  
Not being able to inspect when running the project on mac.  
Not being able to access log commands such as console write, debugger not connecting sometimes, application locking in case of error, debugger closing due to crash in some cases.  
Recompiling the application and uploading it to the emulator every time. We are talking about a serious waste of time.

I’m not talking about signing the application, my God, it’s a pit. But despite everything, this technology promises and achieves incredible things.

For example, you can write your project in one place and make both a mobile application and a website.  
In my application, the share button directly runs a native component and looks different on each platform.  
It is great that you can develop as an interface and do platform-based coding and load classes according to the platform.

The most important feature is that the CSharp codes you write inside work directly as assembly.  
You will understand that the Http requests are not used here, compared to other technologies.  
Combining Blazor and Xamarin can really be the smartest thing Microsoft has ever done.

That’s what keeps you going despite all these troubles.

## Let’s move to development

We have come to the most important point.

We said security is important, we said let’s be anonymous, we even made the name of the app related to this. So where will we keep the data and how will we ensure security?  
Obviously, I went for a simplicity here and preferred to keep the data inside the application rather than keeping it on the internet.It may be a little disappointing, but what if I say I keep the data in sqlite (even worse do not do that)?

At this point you have to say how come I was able to use sqlite on all android and iOS/MAC devices?  [SQLitePCLRaw.provider.e_sqlite3.netstandard11](https://www.nuget.org/packages/SQLitePCLRaw.provider.e_sqlite3.netstandard11)  and  [SQLitePCLRaw.bundle_green](https://www.nuget.org/packages/SQLitePCLRaw.bundle_green/) Their libraries come to our rescue.

Simply put, all libraries developed with net standards can easily work cross platform.

In short, I hosted tables and data with sqlite in the .db file inside the application.  
Not enough, I use this db in all stages, how?

I create an empty archive record when selecting the files. I convert the uploaded files into a byte array and convert them into a single archive and keep the archive as a byte array in a separate table so that I can re-upload or read the archive whenever I want. So single archive multiple uploads.

![archive example](https://miro.medium.com/v2/resize:fit:1400/1*cQgydv0eKRPwVCIbgKs9BQ.png)

AFUP Extracting selected files to ram.

Since I was going to extract the files one by one to ram and finally create the archive, I needed to convert the browser file files to byte array and here in the transient file upload process class that you will read a little later, I also prepared it so that I can get the conversion status of these files.  
In this way, while the files are being selected, they are being converted to bytes and at the same time we can see their status with the progress bar.

**Let’s proceed to uploading the archive.**

I created a transient file upload processor.

With this class, I would manage all the states and build the application on this class.  
This class would manage the selected files, start archiving, show whether the archive was ready or not and start uploading them over the internet.  
It would act as a kind of state machine in itself.

Well, we had to do these operations async and on the other hand we needed to be able to navigate in the application, at first this class was not transient and for this reason it was not possible to navigate to other pages, I re-coded the class and built it to work as transient.

I’ve been through all these steps, I’ve struggled with a lot of bugs, but I’ve successfully turned it into a working and optimized system.

![AFUP List](https://miro.medium.com/v2/resize:fit:1400/1*OQojGJkUBpHEkt0W1TbfTw.png)

Create Archive

![AFUP Archive Create](https://miro.medium.com/v2/resize:fit:1400/1*dmpgHWBId_CBUCr5cD8Ohg.png)

Start uploading

So far so good, but when we say again where can I see the archives I have uploaded, we are preparing a page for this.

![AFUP Archive Upload](https://miro.medium.com/v2/resize:fit:1400/1*sMgu725k7lMPs4uqBVSyeQ.png)

Upload Ended

And where do I see my archives, maybe I want to upload them to another source, what do I do then? You can call me this is an over engineering but I think it’s a nice feature and I wanted it from the beginning. in case the upload process is interrupted, or the file is deleted, etc. the archives should be able to be uploaded to other sources again.

![AFUP Archive Upload Ended](https://miro.medium.com/v2/resize:fit:1400/1*_vF8djyIoHyyPxS0aasuuw.png)

AFUP Archives

This page has created a lot of bugs for me.

If the file is already archived, it comes here, if it is already uploading, the provider should not be able to change it, and so on, I had to test many situations and develop accordingly.

## We’ve finished the basic functions and the rest is trivial stuff

Unfortunately, when you make the actual functions of your application work, you are not making an application.Unfortunately, in order to turn an application into a product, you also need to do unnecessary but necessary work, which I call trivia. 😆

You need to include references to the libraries you use, respect copyrights and mention the producers.

![Copyrights](https://miro.medium.com/v2/resize:fit:1400/1*Psfepyb5Kcbx4vA0r2Vujg.png)

AFUP referances

That’s not enough, you need to prepare a privacy policy as if your app will be used by idiots and get consent so that you don’t have a headache tomorrow.

![Referances](https://miro.medium.com/v2/resize:fit:1400/1*mrI9Sfi9YxTAZs6IqbsJxg.png)

AFUP Consent

I created a support page for those who want to support.

![Consent](https://miro.medium.com/v2/resize:fit:1400/1*WcdTm4O8vkZLm3BVtEsyTA.png)

[Buy Runaho a coffe?](https://www.buymeacoffee.com/runaho)

So now the application side is finished, but is our product finished?

## Product entering the markets

Now we can move on to the more trivia-filled part.As I see it, publishing your application in the markets is a separate torture.

First I created a developer account for the Play Store, then I added the app, then Google asked me for my ID and I gave them a photo. Not enough, I had to answer a ton of questions for my app.  
Obviously, these processes are automated after a while for people who are constantly dealing with this business, but for me it was nothing but a waste of time.

Of course, let’s not skip this stage, since not everyone adds a consent text into the application, they want you to prepare and add a serious privacy policy.

Then i found this site :  [App Privacy Policy Generator (app-privacy-policy-generator.firebaseapp.com)](https://app-privacy-policy-generator.firebaseapp.com/)

From there, after I went ahead and got the consent text, I edited it nicely and made it ready.I uploaded it to Google drive because it’s not a website, it’s a resource accessible through a domain and since I didn’t think of a domain for this project and I saw that others had done it this way, I jumped at the opportunity.

## How will your app look in the store?

Welcome to the show business side of things.

Here you really have to take a ton of screenshots and go through the mock-ups and create the images that best express you. I found a great tool for that too.

[AppMockUp Studio (Beta) (app-mockup.com)](https://studio.app-mockup.com/)

From this point on, after writing the articles introducing your application, here is your page.

![Store](https://miro.medium.com/v2/resize:fit:1400/1*rjXiBYlpAuKZOi5qSe87Vw.png)

[AFUP (Anonymous File Upload)](https://play.google.com/store/apps/details?id=com.runaho.afup.mauiblazor)

## Wrapping Up!

Obviously, if you have read this far, I thank you very much.  
It is wonderful that you are sharing this beautiful day with me.  
In the coming days, I will gradually share the development problems I have experienced in this process.

Even though it was an ordeal for me, I am now a developer who can go through all the stages from start to finish and I have learned a new technology.  
Doing this in my spare time while working has also given me a lot of confidence.

The apple port of the app is also ready, but its store page is a bit more detailed, so that part seems to take a bit longer.

Please don’t forget to leave a comment if you want to.

Take care and see you soon. 👋
