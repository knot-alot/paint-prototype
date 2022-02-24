using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLORDE : MonoBehaviour
{
    private Color player1 = new Color(0.000f, 0.451f, 1.000f, 1.000f);
    private Color player2 = new Color(0.000f, 0.4117f, 1.000f, 1.000f);
    //public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
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

            if (Physics.Raycast(transform.position,dir,out hit, 100.0f, layermask))
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
                  
                    pCoord.x *= texture.width;
                       pCoord.y *= texture.height;
                //   Vector2 tiling = renderer.material.GetTextureScale("_MaskTexture");
                 //  Vector2 tiling = obj.GetComponent<MeshRenderer>().material.GetTextureScale("Texture2D_41271c3c5f484ca2a435c65087a81705");
                 Color color = texture.GetPixel(Mathf.FloorToInt(pCoord.x), Mathf.FloorToInt(pCoord.y ));
            // Debug.Log(hit.point.x + " " + hit.point.y);
         //   Debug.Log(color.g);
           // Debug.Log(Mathf.Abs(player2.g-color.g)<= 0.0001f );
            if (this.tag == "enemy"&& Mathf.Abs(player1.g - color.g) <= 0.0001f) this.GetComponent<enemy>().TakeDamage(1);
            if (this.tag == "Player" && Mathf.Abs(player2.g - color.g) <= 0.0001f) this.GetComponent<Player>().TakeDamage(1);
        }
        
    }
   
}

