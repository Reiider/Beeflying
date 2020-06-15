using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;

public class MainCamera : MonoBehaviour
{
    public float startSpeed = 1;
    float speed;
    float currentY = 0;
    public float acceleration = 1; //accelerating every 10 points
    public GameObject bottom;
    public GameObject player;
    public GameObject[] grounds;

    public GameObject fence;
    private List<GameObject>[] fences;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        player.GetComponent<Player>().IncreaseSpeed(speed * Time.deltaTime);
        
        fences = new List<GameObject>[grounds.Length];
        for(int i = 0; i < grounds.Length; i++)
        {
            fences[i] = new List<GameObject>();
        }
        ReCreateFence(1, 10);
        ReCreateFence(2, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < bottom.transform.position.y) Death();

        player.GetComponent<Player>().IncreaseSpeed(speed * Time.deltaTime);
        Vector3 position = transform.position;
        if(position.y > currentY + 10)
        {
            ResetGrounds(currentY);
            currentY += 10;
            speed += acceleration;
            player.GetComponent<Player>().IncreaseSpeed(speed * Time.deltaTime);
        }
        position.y += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }

    private void ResetGrounds(float y)
    {
        int i = (int) ((y % 30) / 10);
        print(i);
        grounds[i].transform.position = new Vector3(grounds[i].transform.position.x,
                                                    grounds[i].transform.position.y + 30,
                                                    grounds[i].transform.position.z);
        ReCreateFence(i, y + 30);
    }

    private void Death()
    {
        print("СМЕЕЕРть");
    }

    private void ReCreateFence(int index, float y)
    {
        for(int i = 0; i < fences[index].Count; i++)
        {
            Destroy(fences[index][i]);
        }
        fences[index].Clear();

        Random random = new Random();

        for(int i = random.Next(3,9); i >= 0; i--)
        {
            GameObject obj = Instantiate(fence);
            obj.transform.position = new Vector3(random.Next(-65, 65) / 10.0f,
                                             y + random.Next(-45, 45) / 10.0f,
                                             obj.transform.position.z);
            fences[index].Add(obj);
        }
    } 
}
