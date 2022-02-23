using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLORDE : MonoBehaviour
{
    //public GameObject obj;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.GetComponent<Transform>().position;
        Vector3 dir = new Vector3(0, -1, 0);
        Debug.DrawRay(position, dir, Color.green);

        // Vector3 position = Input.mousePosition;

        RaycastHit hit;

        if (Physics.Raycast(position, dir, out hit, 100.0f))
        {

            Renderer renderer = hit.collider.GetComponent<Paintable>().getRenderer();
            // Debug.Log("Got renderer " + renderer);
            Material mat = renderer.material;
            //  Debug.Log("Got material " + mat);
            RenderTexture tex = (RenderTexture)renderer.sharedMaterial.GetTexture("_MaskTexture");
            //  Debug.Log("Got texture " + tex.ToString());
            Texture2D texture = new Texture2D(tex.width, tex.height, TextureFormat.RGB24, false);
            RenderTexture.active = tex;
            texture.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0, false);
            RenderTexture.active = null;
            texture.Apply();
            //  Debug.Log("Got texture2D " + texture.GetPixel(0, 1));
            //   Debug.Log(tex.GetRawTextureData());
            //   Texture2D texture2D1 = obj.GetComponent<MeshRenderer>().material.GetTexture("Texture2D_41271c3c5f484ca2a435c65087a81705") as Texture2D;
            // Texture texture2D1 = obj.GetComponent<MeshRenderer>().material.GetTexture("_MaskTexture");
            Vector2 pCoord = hit.textureCoord;
            transform.position = hit.point;
            pCoord.x *= texture.width;
            pCoord.y *= texture.height;
            //   Vector2 tiling = renderer.material.GetTextureScale("_MaskTexture");
            //  Vector2 tiling = obj.GetComponent<MeshRenderer>().material.GetTextureScale("Texture2D_41271c3c5f484ca2a435c65087a81705");
            Color color = texture.GetPixel(Mathf.FloorToInt(pCoord.x), Mathf.FloorToInt(pCoord.y));
            // Debug.Log(hit.point.x + " " + hit.point.y);
            //    Debug.Log(color);

        }

    }

}
