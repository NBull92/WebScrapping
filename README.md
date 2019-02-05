# WebScrapping
Learning Web scrapping in C# and making a Prism WPF application that gets the latest news from Worcester City F.C. 's website.

I wondered whether my home town football team had a mobile app that I could use to keep up with their latest news and realised they didn't. This got me thinking: "How hard could it be to get the news articles from theirs news pages and create an application from it?".

Using the HtmlAgilityPack and C# I created a console application that would just get the title of the news articles. Then I created a basic WPF application. Lastly I created another WPF application with the Prism Library that allowed me to separate the retrieving of Html data into one module and another one for displaying the news articles to the user.

When the user clicks the button, the associated article will be loaded in their default web browser. In the futrue I would look into creating a new view within the app to showcase the news article itself.

![alt text](https://github.com/NBull92/WebScrapping/blob/master/WorcesterCity%20News%20App.jpg)
