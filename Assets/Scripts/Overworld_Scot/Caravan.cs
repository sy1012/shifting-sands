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
    PlayerOverworldTraversal traversal;
    public bool entering = false;
    public Pyramid enterPyramid;
    public GameObject fade;
    SpriteRenderer fadeRenderer;
    public List<Follower> followers = new List<Follower>();

    public Follower camelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        traversal = FindObjectOfType<PlayerOverworldTraversal>();
        currentNode = traversal.currentNode;
        fadeRenderer = fade.GetComponent<SpriteRenderer>();
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
                transform.position = Vector2.MoveTowards(transform.position, ((OasisNode)path[0]).getOasis().transform.position, 10f * Time.deltaTime);
            }
			else
			{
                path.RemoveAt(0);
            }
        }

        if(entering)
        {
            if(fadeRenderer.color.a > 1)
            {
                SceneManager.LoadScene("DemoDungeon");
            }
            transform.position = Vector2.MoveTowards(transform.position, enterPyramid.transform.position, 10f * Time.deltaTime);
            if (transform.position == enterPyramid.transform.position)
            {
                enterPyramid.transform.localScale = Vector2.Lerp(enterPyramid.transform.localScale, new Vector2(1000, 1000), Time.deltaTime / 8);
                enterPyramid.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                fadeRenderer.color = new Color(0, 0, 0, fadeRenderer.color.a + 1f * Time.deltaTime);
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Instantiate(camelPrefab);
        }

    }
}
