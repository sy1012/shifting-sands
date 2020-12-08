using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caravan : MonoBehaviour
{
    Vector2 targetposition;
    public List<Node> path;
    public OasisNode currentNode;
    public PlayerOverworldTraversal traversal;
    public bool entering = false;
    public Pyramid enterPyramid;
    public GameObject fade;
    SpriteRenderer fadeRenderer;
    public List<Follower> followers = new List<Follower>();
    public bool waiting = false;

    public Follower camelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        traversal = FindObjectOfType<PlayerOverworldTraversal>();
        currentNode = traversal.currentNode;
        fadeRenderer = fade.GetComponent<SpriteRenderer>();
        if (followers.Count == 0)
        {
            Instantiate(camelPrefab);
        }
    }

    public void EnterDungeon()
    {
        StartCoroutine(EnterDungeonCo());
    }

    IEnumerator EnterDungeonCo()
    {
        bool arrived = false;
        while (!arrived)
        {
            transform.position = Vector2.MoveTowards(transform.position, enterPyramid.transform.position, 10f * Time.deltaTime);
            if (transform.position == enterPyramid.transform.position)
            {
                arrived = true;
            }
            yield return null;
        }

        float time = 0;
        fadeRenderer.GetComponent<Animator>().SetTrigger("Fade_Out");

        while (time < 1f)
        {
            enterPyramid.transform.localScale = Vector2.Lerp(enterPyramid.transform.localScale, new Vector2(1000, 1000), Time.deltaTime / 8);
            enterPyramid.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            enterPyramid.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
            time += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("Dungeon");
    }

    // Update is called once per frame
    void Update()
    {
        if(path.Count != 0)
		{
            currentNode = (OasisNode)path[0];
            traversal.currentNode = (OasisNode)currentNode;
            if (transform.position != ((OasisNode)path[0]).getOasis().transform.position)
            {
                if (!waiting)
                {
                    transform.position = Vector2.MoveTowards(transform.position, ((OasisNode)path[0]).getOasis().transform.position, 10f * Time.deltaTime);
                    EventManager.TriggerOnOverworldMovement();
                }
                
            }
			else
			{
                path.RemoveAt(0);
            }
        }
        if (entering && transform.position != enterPyramid.transform.position)
        {
            EventManager.TriggerOnOverworldMovement();
        }

/*        if(Input.GetKeyDown(KeyCode.E)){
            Instantiate(camelPrefab);
        }*/

    }
}
