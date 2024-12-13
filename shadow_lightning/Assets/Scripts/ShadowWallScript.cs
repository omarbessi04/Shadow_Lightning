using UnityEngine;

public class ShadowWallScript : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 3; //ground
    }

    public void makePassable(bool passable){
        if (passable){
            gameObject.layer = 0; // default layer
        }else{
            gameObject.layer = 3; //groun
        }
    }
}
