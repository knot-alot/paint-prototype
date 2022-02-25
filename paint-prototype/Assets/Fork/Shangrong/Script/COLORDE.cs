using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLORDE : MonoBehaviour
{
    private Color player1 = new Color(0.000f, 0.451f, 1.000f, 1.000f);
    private Color player2 = new Color(0.000f, 0.4117f, 1.000f, 1.000f);
    //public GameObject obj;

    // Start is called before the first frame update
    Texture2D texture;
    Material mat;
    RenderTexture tex;
    Rect rect;

    void Start()
    {
        texture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        rect = new Rect(0, 0, 1024, 1024);
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 position = this.GetComponent<Transform>().position;

        Vector3 dir = transform.TransformDirection(Vector3.down);


        // Vector3 position = Input.mousePosition;
        int layermask = 1 << 8;
        layermask = ~layermask;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, 100.0f, layermask))
        {
            Renderer renderer = hit.collider.GetComponent<Paintable>().getRenderer();
            // Debug.Log("Got renderer " + renderer);
            mat = renderer.material;
            //  Debug.Log("Got material " + mat);
            tex = (RenderTexture)renderer.sharedMaterial.GetTexture("_MaskTexture");
            //  Debug.Log("Got texture " + tex.ToString());

            RenderTexture.active = tex;
            texture.ReadPixels(rect, 0, 0, false);
            RenderTexture.active = null;
            texture.Apply();

            Vector2 pCoord = hit.textureCoord;

            pCoord.x *= texture.width;
            pCoord.y *= texture.height;

            Color color = texture.GetPixel(Mathf.FloorToInt(pCoord.x), Mathf.FloorToInt(pCoord.y));

            if (this.tag == "enemy" && Mathf.Abs(player1.g - color.g) <= 0.0001f) this.GetComponent<enemy>().TakeDamage(1);
            if (this.tag == "Player" && Mathf.Abs(player2.g - color.g) <= 0.0001f) this.GetComponent<Player>().TakeDamage(1);
        }

    }

}

