Configuring Firebase
--------------------

1. Create a Firebase project in the [Firebase Console][1], if you don't already have one setup.  If you have an existing Google project associated with your mobile app, you can use the **Import Google Project** option.  Otherwise, use the **Create New Project** option.

2. Click **Add Firebase to your Android app**.  

  a. Enter your app's ***Package Name***
  
  b. Enter the SHA-1 Hash for your signing certificate.  If you don't know how to find this, [check out this guide][2]

3. Click **Add App** and download the ***google-services.json*** file generated for you.

4. Add the ***google-services.json*** file to your Xamarin.Android project.

5. Set the *Build Action* for your ***google-services.json*** file to ***GoogleServicesJson***.  

The build process will take the api keys and values from your ***google-services.json*** and translate them into the correct resource string key/value pairs in your app.




