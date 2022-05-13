using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Photon.Pun;

public class PlaceObjectsController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Placement Settings")]
    public float maxPlacementDistance = 5f;
    public static int maxItemsPerPerson = 5;


    [Header("Components")]
    [SerializeField] private Transform ContentContainer;
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private GameObject[] prefabItems;
    public Animator PlaceObjectParentWindow;

    [Header("Readonly Details")]
    private int itemsAlreadyPlaced = 0;
    public GameObject SelectedItemGhost;                    // Ghost is used as transparant representation of object over mouse cursor.
    private GameObject SelectedItemPrefab;                  // Actual object that is instantitated. Not Modified like ghost.
    public Transform localPlayer;

    void Start()
    {
        localPlayer = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        PopulateWindow();
        
    }

    private void Update()
    {
        if (!SelectedItemGhost) return;
        PlacementModeLoop();            // If Item is selected, we are in placement mode Loop.
    }

    /*
     *  Placement Mode Functions
     * 
     */

    void PlacementModeLoop()
    {
        ShowGhostGameObjectOverMousePosition();

        if (Input.GetKeyDown(KeyCode.Escape))
            ResetItemSelection();
        if (Input.GetMouseButtonDown(1))
        {
            PlaceSelectedItem();
        }
    }

    public void PlaceSelectedItem()
    {
        itemsAlreadyPlaced++;
        ResetItemSelection();                                                                           // Clear Item Selection.
        Vector3 worldPos = GetWorldPositionOnPlane(Input.mousePosition, 0f);                            // Get World Position.
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Instantiate(SelectedItemPrefab.name, worldPos, Quaternion.identity);          // Instantiate in PhotonNetwork.
        else
            Instantiate(SelectedItemPrefab, worldPos, Quaternion.identity);
        //TODO:
        //Firebase.StoreItemCoordinate(LobbyID, SelectedItemPrefab.name, worldPos.x, worldPos.y);
    }

    void ShowGhostGameObjectOverMousePosition()
    {
        Vector3 worldPos = GetWorldPositionOnPlane(Input.mousePosition, 0f);
        if (Vector2.Distance(worldPos, localPlayer.position) > maxPlacementDistance)
        {
            SelectedItemGhost.GetComponent<SpriteRenderer>().color = Color.red;                                                  // Can't place object, make it a red tint.
        }
        else
        {
            SelectedItemGhost.GetComponent<SpriteRenderer>().color = SelectedItemPrefab.GetComponent<SpriteRenderer>().color;    // Can Place Object, normal tint.
        }
        FadeObjectSlightly(SelectedItemGhost);
        SelectedItemGhost.transform.position = worldPos;
    }

    void FadeObjectSlightly(GameObject g)
    {
        Color color = SelectedItemGhost.GetComponent<SpriteRenderer>().color;          // Make it slightly transparant.
        color.a /= 2;
        SelectedItemGhost.GetComponent<SpriteRenderer>().color = color;
    }
 
    void DisableAllColliders(GameObject g)
    {   
        Collider2D col = SelectedItemGhost.GetComponent<Collider2D>();
        if(col)
            col.enabled = false;                                   // Disable Collider.
    }

   /*
    *  End of Placement Mode Functions
    * 
    */


    /*
     *  Special Function from Stackoverflow for Perspective Camera.
     * 
    */

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z=0f)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        Vector3 pos = ray.GetPoint(distance);
        pos.z = 0;
        return pos;
    }



    /*
     *  Placement Window UI Functions.
     * 
    */

    public void PopulateWindow()
    {
        foreach (Transform child in ContentContainer.transform)
        {
            GameObject.Destroy(child.gameObject);       // Murder the Existing children.
        }

        int i = 0;
        foreach (GameObject item in prefabItems)
        {
            GameObject itemTile = Instantiate(TilePrefab);
            // do something with the instantiated item -- for instance
            itemTile.GetComponentInChildren<TMP_Text>().text = item.name;
            itemTile.GetComponentInChildren<Image>().color = i % 2 == 0 ? new Color(1f, 0.9f, 1f, 1f) : new Color(0.9f, 1f, 1f, 1f);
            SetThumbnail(itemTile, item); // Takes a while to load thunbmail image, hence co-routine. 
            
            itemTile.transform.SetParent(ContentContainer);
            //reset the item's scale -- this can get munged with UI prefabs
            itemTile.transform.localScale = Vector2.one;
            itemTile.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(item); CloseObjectPlacementWindow(); });
            i++;
        }
        
    }

    private void SetThumbnail(GameObject itemTile, GameObject item)
    {
        itemTile.transform.Find("ItemThumbnail").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite ;
    }

    /*
    private void AddListenersOnRuntimeFix()
    {
        int i = 0;
        foreach (Transform itemTile in ContentContainer.transform)
        {
            i++;
            int x = i;      // "Delegate" bugs out if I directly use i below.
            Button b = itemTile.GetComponent<Button>();
            if(b)
                b.onClick.AddListener(delegate { SelectItem(prefabItems[x]); CloseObjectPlacementWindow(); });
        }
       
    }
    */
    public void CloseObjectPlacementWindow()
    {
        PlaceObjectParentWindow.SetTrigger("close");
    }

    public void OpenObjectPlacementWindow()
    {
        PlaceObjectParentWindow.SetTrigger("open");
    }

    public void ResetItemSelection()
    {
        Destroy(SelectedItemGhost);
        SelectedItemGhost = null;
    }
    public void SelectItem(GameObject g)
    {
        SelectedItemGhost = Instantiate(g, Vector2.zero, Quaternion.identity);
        SelectedItemPrefab = g;
        DisableAllColliders(SelectedItemGhost);
    }



    /*
     *  End of Placement Window UI Functions.
     * 
    */
}
