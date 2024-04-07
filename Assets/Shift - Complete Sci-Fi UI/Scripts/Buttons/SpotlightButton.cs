using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Michsky.UI.Shift
{
    public class SpotlightButton : MonoBehaviour
    {
        [Header("Text")]
        public bool useCustomText = false;
        public string buttonTitle = "My Title";
        public string buttonDescription = "My Description";

        [Header("Image")]
        public bool useCustomImage = false;
        public Sprite firstImage;
        public Sprite secondImage;
        public Sprite thirdImage;
        public Sprite forthImage;
        public Sprite fifthImage;
        public Sprite sixthImage;
        public Sprite seventhImage;
        public Sprite eigthImage;
        public Sprite ninthImage;
        public Sprite tenthImage;
        public Sprite elevenImage;
        
        TextMeshProUGUI titleText;
        TextMeshProUGUI descriptionText;
        Image image1;
        Image image2;
        Image image3;
        Image image4;

        void Start()
        {
            List<Sprite> list_Sprites = new List<Sprite>();
            list_Sprites.Add(firstImage);
            list_Sprites.Add(secondImage);
            list_Sprites.Add(thirdImage);
            list_Sprites.Add(forthImage);
            list_Sprites.Add(fifthImage);
            list_Sprites.Add(sixthImage);
            list_Sprites.Add(seventhImage);
            list_Sprites.Add(eigthImage);
            list_Sprites.Add(ninthImage);
            list_Sprites.Add(tenthImage);
            list_Sprites.Add(elevenImage);


            if (useCustomText == false)
            {
                titleText = gameObject.transform.Find("Content/Title").GetComponent<TextMeshProUGUI>();
                descriptionText = gameObject.transform.Find("Content/Description").GetComponent<TextMeshProUGUI>();

                titleText.text = buttonTitle;
                descriptionText.text = buttonDescription;
            }

            if (useCustomImage == false)
            {
                image1 = gameObject.transform.Find("Content/Background/Image 1").GetComponent<Image>();
                image2 = gameObject.transform.Find("Content/Background/Image 2").GetComponent<Image>();

                System.Random randomElement = new System.Random(84353957);

                image1.sprite = list_Sprites[randomElement.Next(0, 11)];
                
                randomElement = new System.Random(739875349);
                
                image2.sprite = list_Sprites[randomElement.Next(0, 11)];

            
            }

        }
    }
}