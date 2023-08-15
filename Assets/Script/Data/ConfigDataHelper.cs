using Newtonsoft.Json;
using UnityEngine;

public class ConfigDataHelper 
{
    private static GameConfig gameconfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (gameconfig == null)
                gameconfig = JsonConvert.DeserializeObject<GameConfig>(Resources.Load<TextAsset>("/Data/GameConfig").text);
            return gameconfig;
        }
    }
    private static UserData userData = null;
    public static UserData UserData
    {
        get
        {
            if (!ES3.KeyExists(GameConstants.USERDATA))
            {

                ES3.Save(GameConstants.USERDATA, userData);
            }
            else
                userData = ES3.Load<UserData>(GameConstants.USERDATA);
            return userData;
        }
    }
}
