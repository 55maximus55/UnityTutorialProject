using UnityEngine;

public class WorldPhysics : MonoBehaviour
{
    
    public float gravity = -9.81f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = new Vector2(0, gravity);
    }
    
}
