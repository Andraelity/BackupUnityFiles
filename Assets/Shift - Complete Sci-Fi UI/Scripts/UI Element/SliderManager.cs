using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MenuSettingsFontsNamespace;

namespace Michsky.UI.Shift
{

    public class SliderManager : MonoBehaviour
    {

        [Header("ActivateComunicationWithFont")]
        public bool bool_SetFontCommunication = false;
        // public bool bool_SetFontSizeComm'unication = false;

        [Header("Resources")]
        public TextMeshProUGUI valueText;

        [Header("Saving")]
        public bool enableSaving = false;
        public string sliderTag = "Tag Text";
        public float defaultValue = 1;

        [Header("Settings")]
        public bool usePercent = false;
        public bool showValue = true;
        public bool useRoundValue = false;

        Slider mainSlider;
        float saveValue;


        void Start()
        {

            mainSlider = gameObject.GetComponent<Slider>();


            if (showValue == false)
                valueText.enabled = false;

            if (enableSaving == true)
            {
                if (PlayerPrefs.HasKey(sliderTag + "SliderValue") == false)
                    saveValue = defaultValue;
                else
                    saveValue = PlayerPrefs.GetFloat(sliderTag + "SliderValue");

                mainSlider.value = saveValue;

                mainSlider.onValueChanged.AddListener(delegate
                {
                    saveValue = mainSlider.value;
                    PlayerPrefs.SetFloat(sliderTag + "SliderValue", saveValue);
                });
            }


            MenuSettingsFontsClass.int_SliderFontValue = (int)Mathf.Round(mainSlider.value * 1.0f);

        }


        int counter_Value = 0;


        void Update()
        {

            // Debug.Log("Code representations element " + mainSlider.value + "    element counter = " + counter_Value);

            if(bool_SetFontCommunication)
            {   
                
                MenuSettingsFontsClass.int_SliderFontValue = (int)Mathf.Round(mainSlider.value * 1.0f);
                // Debug.Log("Code representations element " + mainSlider.value + "    element counter = " + counter_Value);

            }


            if (useRoundValue == true)
            {
                if (usePercent == true)
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString() + "%";
                else
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString();
            }

            else
            {
                if (usePercent == true)
                    valueText.text = mainSlider.value.ToString("F1") + "%";
                else
                    valueText.text = mainSlider.value.ToString("F1");
            }
        
        }
    
    }
}