using UnityEngine;
using UnityEngine.UI;
public class Characters : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private string ign;
    [SerializeField] private int age;
    [SerializeField] private Sprite icon;

    public Sprite bronzeSelfie;
    public Sprite silverSelfie;
    public Sprite goldSelfie;
    [SerializeField] private string bio;
    [SerializeField] private string likes;
    [SerializeField] private string dislikes;
    [SerializeField] private string favGames;
    private int reputation;

    public string getCharacterName()
    {
        return characterName;
    }
    public void setCharacterName(string characterName)
    {
        this.characterName = characterName;
    }

    public string getIgn()
    {
        return ign;
    }
    public void setIgn(string ign)
    {
        this.ign = ign;
    }

    public int getAge()
    {
        return age;
    }
    public void setAge(int age)
    {
        this.age = age;
    }

    public Sprite getIcon()
    {
        return icon;
    }
    public void setIcon(Sprite icon)
    {
        this.icon = icon;
    }

    public string getBio()
    {
        return bio;
    }
    public void setBio(string bio)
    {
        this.bio = bio;
    }

    public string getLikes()
    {
        return likes;
    }
    public void setLikes(string likes)
    {
        this.likes = likes;
    }

    public string getDislikes()
    {
        return dislikes;
    }
    public void setDislikes(string dislikes)
    {
        this.dislikes = dislikes;
    }

    public string getFavGames()
    {
        return favGames;
    }
    public void setFavGames(string favGames)
    {
        this.favGames = favGames;
    }

    public int getReputation()
    {
        return reputation;
    }
    public void setReputation(int reputation)
    {
        this.reputation = reputation;
    }

}
