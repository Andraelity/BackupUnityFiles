using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using MenuSettingsFontsNamespace;

namespace Michsky.UI.Shift
{

    public class SettingsButton : MonoBehaviour, IPointerEnterHandler
    {

        [Header("Resources")]
        public Image detailImage;
        public Image detailIcon;
        public Image detailBackground;

        public TextMeshProUGUI detailTitle;
        public TextMeshProUGUI detailDescription;
        public TextMeshProUGUI buttonTitleObj;
       
        [Header("TextPreview")]
        public TextMeshProUGUI textPreview;        
        public bool bool_ShowPreviewText; 

        [Header("Content")]
        public bool useCustomContent;
        public string buttonTitle;

        [Header("Preview")]
        public bool enableIconPreview;

        public string title;
        
        [TextArea] public string description;
        public Sprite imageSprite;
        public Sprite iconSprite;
        public Sprite iconBackground;

        TMP_FontAsset font_asset;

        void Start()
        {

            if (useCustomContent == false) { buttonTitleObj.text = buttonTitle; }

            if(textPreview != null)
            {
        
           		font_asset = (TMP_FontAsset)Resources.Load("Fonts/FORCEDSQUARESDF");
                // textPreview.font = font_asset;
                
            }
               
        }

        void Update()
        {


            if(textPreview != null)
            {

                if(textPreview.gameObject.activeSelf == true)
                {

                    // string string_CheckValueOnDirectory = "Fonts/BabyDollFont";

                    // Debug.Log(MenuSettingsFontsClass.int_SliderFontValue + 1);

                    string string_GetNameFont = MenuSettingsFontsClass.GetFontName(MenuSettingsFontsClass.int_SliderFontValue - 1);

                    // int int_fontSize = MenuSettingsFontsClass.int_fontSize;

                    // font_asset = (TMP_FontAsset)Resources.Load(string_CheckValueOnDirectory);

                    font_asset = (TMP_FontAsset)Resources.Load(string_GetNameFont);

                    // textPreview.gameObject.SetActive(true);

                    textPreview.font = font_asset;
                    // Debug.Log(textPreview.text);

                    
                }


            }


        }


        public void OnPointerEnter(PointerEventData eventData)
        {

            if (enableIconPreview == true)
            {
            
                detailImage.gameObject.SetActive(false);
                detailIcon.gameObject.SetActive(true);
                detailBackground.gameObject.SetActive(true);
                detailIcon.sprite = iconSprite;
                detailBackground.sprite = iconBackground;
                    

            }
            else
            {
            
                detailImage.gameObject.SetActive(true);
                detailIcon.gameObject.SetActive(false);
                detailBackground.gameObject.SetActive(false);
                detailImage.sprite = imageSprite;
            
            }

            if(bool_ShowPreviewText == true)
            {

                textPreview.gameObject.SetActive(true);

            }
            else
            {
            
                if(textPreview == null)
                {

                    detailIcon.transform.parent.transform.GetChild(3).gameObject.SetActive(false);
  

                }
                
            }
          
            

            detailTitle.text = title;
            detailDescription.text = description;
        }
    }
}