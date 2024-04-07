using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MenuSettingsFontsNamespace 
{

    public static class MenuSettingsFontsClass
    {

        public static int int_SliderFontValue = 1;

        public static string string_NameFont_0 = "BabyDollFont";
        public static string string_NameFont_1 = "CoolveticaFont";
        public static string string_NameFont_2 = "CoolveticaFont";
        public static string string_NameFont_3 = "NexaHeavyFont";
        public static string string_NameFont_4 = "PeachCakeFont";
        public static string string_NameFont_5 = "PixelFontFont";
        public static string string_NameFont_6 = "RobotoRegularFont";
        public static string string_NameFont_7 = "SkylandFont";
        public static string string_NameFont_8 = "SuperSundayFont";
        public static string string_NameFont_9 = "TunaSandwichFont";
        public static string string_NameFont_10 = "WaltographFont";

        public static string[] arraystring_NameFont = {
                                        "BabyDollFont",
                                        "CoolveticaFont",
                                        "CoolveticaFont",
                                        "NexaHeavyFont",
                                        "PeachCakeFont",
                                        "PixelFontFont",
                                        "RobotoRegularFont",
                                        "SkylandFont",
                                        "SuperSundayFont",
                                        "TunaSandwichFont",
                                        "WaltographFont"
                                        };


        public static string GetFontName(int int_PositionArray)
        {

            return "Fonts/" + arraystring_NameFont[int_PositionArray];

        }

    }
}
