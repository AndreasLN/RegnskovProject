using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
[ExecuteInEditMode]
public class LEDCam : MonoBehaviour
{
    public static int cams = 0;
    public Camera LEDRenderCam;
    public Camera LEDApply;
    public Material LEDMaterial;
    public int ledWidth = 192;
    public int ledHeight = 64;
    public int posX = 0;
    public int posY = 0;
    public float rotation = -90;

    Transform quad;
    int width;
    int height;
    public static RenderTexture ledTexture = null;
    
    public static Material ledVisualizer = null;
    public static int LEDrenderers = 0;
    private void OnEnable()
    {
        Camera onThis = GetComponent<Camera>();
        if (onThis != null)
        {
            //This is the old setup. Fix


            LEDCam ledCamComponent = transform.parent.gameObject.GetComponent<LEDCam>();
            if (ledCamComponent == null)
            {
                ledCamComponent = transform.parent.gameObject.AddComponent<LEDCam>();
            }
            
            
            ledCamComponent.LEDRenderCam = onThis;
            ledCamComponent.LEDApply = LEDApply;
            ledCamComponent.LEDMaterial = LEDMaterial;
            ledCamComponent.ledWidth = ledWidth;
            ledCamComponent.ledHeight = ledHeight;
            ledCamComponent.posX = posX;
            ledCamComponent.posY = posY;
            ledCamComponent.rotation = rotation;
            Destroy(this);
        }
        LEDrenderers++;
    }

    private void OnDisable()
    {
        LEDrenderers--;
        if (LEDrenderers <= 0)
        {
            ledTexture.DiscardContents();
            if (Application.isPlaying)
            {
                Destroy(ledTexture);
            }
            else
            {
                DestroyImmediate(ledTexture);
            }

            ledTexture = null;
        }
    }
    void Awake()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }

        cams++;


        ReadSettings();
        if (ledTexture == null)
        {
            ledTexture = new RenderTexture(ledWidth * 2, ledHeight * 2, 24, RenderTextureFormat.ARGB32, 0);
            ledTexture.antiAliasing = 4;
            ledVisualizer = new Material(Shader.Find("LED/Visualizer"));
            ledVisualizer.SetTexture("_MainTex", ledTexture);
            ledVisualizer.SetTexture("_LEDMask", Instantiate(Resources.Load("LEDMask", typeof(Texture2D))) as Texture2D);
        }
        
        LEDRenderCam.targetTexture = ledTexture;
        LEDMaterial.SetTexture("_MainTex", ledTexture);
        SetCam();
    }

    void ReadSettings ()
    {
        string path = Application.dataPath + "\\" + "ledSpecifications.txt";
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);

            string[] configs = data.Split(';');

            for (int i = 0; i < configs.Length; i++)
            {
                string[] current = configs[i].Split(':');

                if (current.Length == 2)
                {
                    if (current[0].Contains("ledWidth"))
                    {
                        if (int.TryParse(current[1], out int result))
                        {
                            ledWidth = result;
                        }
                    } 
                    else if (current[0].Contains("ledHeight"))
                    {
                        if (int.TryParse(current[1], out int result))
                        {
                            ledHeight = result;
                        }
                    }
                    else if (current[0].Contains("posX"))
                    {
                        if (int.TryParse(current[1], out int result))
                        {
                            posX = result;
                        }
                    }
                    else if (current[0].Contains("posY"))
                    {
                        if (int.TryParse(current[1], out int result))
                        {
                            posY = result;
                        }
                    }
                    else if (current[0].Contains("rotation"))
                    {
                        if (float.TryParse(current[1], out float result))
                        {
                            rotation = result;
                        }
                    }
                }
            }
        }
        else
        {
            WriteSettings();
        }
    }

    void WriteSettings ()
    {
        string path = Application.dataPath + "\\" + "ledSpecifications.txt";
        string data = "";
        data += "ledWidth:" + ledWidth + ";\r\n";
        data += "ledHeight:" + ledHeight + ";\r\n";
        data += "posX:" + posX + ";\r\n";
        data += "posY:" + posY + ";\r\n";
        data += "rotation:" + rotation + ";";

        File.WriteAllText(path, data);
    }

    void SetCam()
    {
        // int width = Display.displays[1].renderingWidth;
        // int height = Display.displays[1].renderingHeight;
        if (LEDRenderCam != null && Application.isPlaying)
        {
            LEDRenderCam.rect = new Rect(0, 0, 1, 1);
            float aspect = (float)ledWidth/ ledHeight;

            LEDRenderCam.aspect = aspect;

            width = 1024;// Display.displays[1].renderingWidth;
            height = 768;// Display.displays[1].renderingHeight;

            if (Display.displays.Length > 1)
            {
                width = Display.displays[1].renderingWidth;
                height = Display.displays[1].renderingHeight;
            }

            LEDApply.transform.SetParent(null);
            LEDApply.gameObject.hideFlags = HideFlags.HideInHierarchy;
            LEDApply.aspect = (float)width / height;
            LEDApply.transform.position = Vector3.one * 1000 * ((float)cams/10);
            GameObject parent = new GameObject("LED Quad");

            quad = parent.transform;

            quad.SetParent(LEDApply.transform);
            quad.localPosition = Vector3.zero;
            GameObject rtHolder = GameObject.CreatePrimitive(PrimitiveType.Quad);
            MeshRenderer rtRenderer = rtHolder.GetComponent<MeshRenderer>();
            rtRenderer.sharedMaterial = Instantiate(LEDMaterial);
            rtHolder.transform.SetParent(quad);
            rtHolder.transform.localPosition = new Vector3(.5f, .5f, 0);
            SetCamPos();


            quad.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }

    }
    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            LEDRenderCam.ResetProjectionMatrix();
            float aspect = (float)ledWidth / ledHeight;

            LEDRenderCam.aspect = aspect;
        }

        SetCamPos();
    }

    void SetCamPos ()
    {
        if (quad != null && LEDRenderCam != null)
        {
            float aspect = (float)ledWidth / ledHeight;

            LEDRenderCam.aspect = aspect;
            float posXOffset = ((LEDApply.orthographicSize * LEDApply.aspect) * 2) * ((float)posX / width);
            float posYOffset = ((LEDApply.orthographicSize) * 2) * ((float)posY / height);
            Vector3 xPos = Vector3.right * LEDApply.orthographicSize * LEDApply.aspect;
            Vector3 yPos = Vector3.down * (LEDApply.orthographicSize);
            xPos.x -= posXOffset;
            yPos.y += posYOffset;
            quad.localPosition = Vector3.forward * 10 - xPos - yPos;

            float localScaleX = (LEDApply.orthographicSize / ((float)height / ledWidth)) * 2;
            float localScaleY = ((LEDApply.orthographicSize * LEDApply.aspect) / ((float)width / ledHeight)) * 2;

            quad.localScale = new Vector3(localScaleX, localScaleY, 1);
            quad.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }
        
    }

}
