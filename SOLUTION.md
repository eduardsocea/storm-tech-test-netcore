# Task #8 
The proposed solution is taking into consideration the following assumptions:
- We can favor eventual consistency. If the username is edited in gravatar, the app will display eventually the updated value;
- The user is not able to edit the username through application UI and the update can be performed from Gravatar only;

The implemented solution is using a `BackgroundService`.
The reason behind it is to reduce the latency for user's requests. If Gravatar is down/slow, it won't have a direct impact on user's performance. 

The service `UsernameSeedingService` can have an improved performance if we only read from database the current username & email. Along this, pagination can be implemented in order to load in memory just a specific number of rows.

Setting the username is using Usermanager for convenience and consistency, but we can consider having a batch update in order to reduce the number of roundtrips to database.

The change of username directly in database is having a broader impact which required a custom implementation of SignInManager. The new SignInManager is not overriding all the required methods since it's not required for the sample app.

If changing the value of the username directly in database is not an option, an alternative is a custom implementation of `IUserClaimsPrincipalFactory<TUser>`.

If taking the username from Claims is not wanted, we can just cache/create a new datapoint in database to store the Gravatar Username.

# Task #9/#10
- For convenience, I decided to put all the Javascript code directly in the .cshtml files. For a production ready application, all the javascript code would be moved in separated files, minified and having the locals renamed. 
