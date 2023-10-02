using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Holster : MonoBehaviour
{
    [SerializeField] TMP_Text reloadText;
    // Start is called before the first frame update
    void Start()
    {
        ShowReload();
    }

    public void ShowReload()
    {
        reloadText.gameObject.SetActive(true);
    }

    public void HideReload()
    {
        reloadText.gameObject.SetActive(false);
    }
}
