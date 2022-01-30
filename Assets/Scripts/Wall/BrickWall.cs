using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    [SerializeField]
    GameObject wall, wall_Broken;
    // Start is called before the first frame update
  public void GetHit()
    {
        wall.SetActive(false);
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        wall_Broken.SetActive(true);
        yield return new WaitForSeconds(2f);
        wall_Broken.SetActive(false);
        Destroy(this.gameObject);
        yield return null;
    }


}
