using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class ButtonExtension {
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick) {

        //Debug.Log("item " + param + " Added Listener");
        button.onClick.AddListener(delegate ()
        { 
            OnClick(param);
        });
    }
}
public class ListGenerator : MonoBehaviour
{
    static public bool afegit;
    public Text afegitText;
    public GameObject contentPanel;

    static public Text errorText;

    [SerializeField] Text listHelperText;

    private GameObject graph_windowObj;
    private Window_Graph graph_window;
    // Start is called before the first frame update
    void Start()
    {
        float myHeight = 0;
        int listSize = RoutesManager.rutas.Count;


        try
        {
            GameObject go = GameObject.Find("ErrorText");
            errorText = (Text)go.GetComponent(typeof(Text));


            errorText.text = "";
        }
        catch (Exception)
        {

        }
       

        if (listHelperText != null) {
            if (listSize == 0)
            {
                listHelperText.text = "No hi han rutes afegides";
            }
            else
            {
                listHelperText.text = "";
            }
        }
       

        if (listSize <= 7)
        {
            myHeight = 430f;
        } else
        {
            myHeight = listSize * transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, myHeight);
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {
            Ruta ruta = RoutesManager.rutas[i];
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = ruta.name;
            g.transform.GetChild(3).GetComponent<Text>().text = ruta.totalDistance.ToString() + " km";
            g.transform.GetChild(5).GetComponent<Text>().text = ruta.positiveElevation.ToString() + "m";
            g.transform.GetChild(7).GetComponent<Text>().text = ruta.negativeElevation.ToString() + "m";

            g.GetComponent<Button>().AddEventListener(i, ItemClickedSelectRoute);
      
        }
        
        Destroy(buttonTemplate);
    }

    private void ItemClicked(int i)
    {

        Debug.Log("Ruta id:" + i +" selected");

        GameObject buttonTemplate ;
        Button button;

        buttonTemplate = transform.GetChild(i).gameObject;
        button = buttonTemplate.GetComponent<Button>();
        button.Select();
    }

    private void ItemClickedSelectRoute(int i)
    {
        GameObject buttonTemplate;
        Button button;

        Debug.Log("Ruta id:" + i);
       
        buttonTemplate = transform.GetChild(i).gameObject;
        button = buttonTemplate.GetComponent<Button>();
        button.Select();

        //agafar ruta (l'id del boto es la posicio de la ruta en la llista)
        Ruta ruta = RoutesManager.rutas[i];

        if (GameObject.Find("Window_Graph"))
        {
            graph_windowObj = GameObject.Find("Window_Graph");
            graph_window = (Window_Graph)graph_windowObj.GetComponent(typeof(Window_Graph));
            graph_window.DestroyGraph();
            graph_window.ShowGraph(ruta.trackPoints, ruta.pendentPunts, ruta.totalDistance, 150);     
        }

        FollowRoute.ruta = ruta;

        Debug.Log("item " + i + " clicked");
    }


}
