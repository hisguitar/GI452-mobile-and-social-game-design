using System;

/// <summary>
/// The UserData class, which gets downloaded when verifying the user's email 
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class UserData
{
    public UserInfo[] users;
}