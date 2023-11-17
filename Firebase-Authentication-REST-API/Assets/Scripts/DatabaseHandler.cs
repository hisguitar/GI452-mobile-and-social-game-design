using System.Collections.Generic;
using System.Diagnostics;
using FullSerializer;
using Proyecto26;


public static class DatabaseHandler
{
    private const string projectId = "teshauth01"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.asia-southeast1.firebasedatabase.app/";
    
    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);


    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    /// <param name="idToken"> Token which authenticates the request </param>
    public static void PostUser(User user, string userId, PostUserCallback callback, string idToken)
    {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json?auth={idToken}", user).Then(response => { callback(); }).Catch(ex =>
        {
            UnityEngine.Debug.Log(ex.Message);
        });
    }

    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    /// <param name="idToken"> Token which authenticates the request </param>
    public static void GetUser(string userId, GetUserCallback callback, string idToken)
    {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json?auth={idToken}").Then(user => { callback(user); });
    }

    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json?auth={AuthHandler.idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }
}
