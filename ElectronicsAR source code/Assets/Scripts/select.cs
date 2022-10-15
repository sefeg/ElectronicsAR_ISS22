using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to detect input of the user 
public class select : MonoBehaviour
{
    [SerializeField] private string selectTag = "selectable";
    [SerializeField] private string dummyTag = "dummy";
    public elemtPositions elemtPositions;
    public GameObject parent;
    public GameObject WirePrefab;
    public GameObject RPrefab;
    public GameObject LEDPrefab;
    public GameObject CPrefab;
    public GameObject BJTPrefab;
    public GameObject WireExtraPrefab;
    public bool boardactiv = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                checkTouch(Input.mousePosition);
            }
        }
    }

    // Raycast from mouse/ touch position
    // To confirm the element
    private void checkTouch(Vector3 pos)
    {
        if (boardactiv)
        {
            var ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                Debug.Log(selection.gameObject.name);

                if (selection.CompareTag(selectTag))
                {
                    Debug.Log(selection.name);
                    while (selection.GetComponent<CircuitElement>() == null)
                    {
                        selection = selection.parent;
                    }
                    // prefab abhängig vom board instatiieren
                    //adden

                    CircuitElement elem = selection.GetComponent<CircuitElement>();
                    Quaternion q = Quaternion.Inverse(parent.transform.rotation) * selection.transform.rotation;
                    Vector3 position = parent.transform.InverseTransformPoint(selection.transform.position);
                    elemtPositions.add(selection.gameObject, position, q, elem);

                    // target deaktivieren
                    selection.gameObject.SetActive(false);

                    GameObject tmp;
                    if (elem.type == 0)
                    {
                        tmp = Instantiate(WirePrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);
                        // link target
                    }
                    else if (elem.type ==1)
                    {
                        tmp = Instantiate(RPrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);
                    }
                    else if (elem.type == 2)
                    {
                        tmp = Instantiate(LEDPrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);
                    }
                    else if (elem.type == 3)
                    {
                        tmp = Instantiate(CPrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);
                    }
                    else if (elem.type == 4)
                    {
                        tmp = Instantiate(BJTPrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);
                    }
                    else if (elem.type == -2)
                    {
                        tmp = Instantiate(WireExtraPrefab, position, q);
                        tmp.transform.SetParent(parent.transform, false);

                        
                    }

                }
                else if (selection.CompareTag(dummyTag))
                {
                    while (selection.GetComponent<CircuitElement>() == null)
                    {
                        selection = selection.parent;
                    }
                    //selection.GetComponent<dummy>().dummyOf.SetActive(true);
                    //elemtPositions.listOfFoundElements.Remove((this.transform.position, this.transform.rotation, selection.GetComponent<CircuitElement>()));
                    //elemtPositions.foundElements.Remove(selection.GetComponent<CircuitElement>().id);
                    //Destroy(this);
                }

            }
        }

    }

    public void boardisactiv()
    {
        boardactiv = true;
    }
    public void boardisinactiv ()
    {
        boardactiv = false;
    }
}
