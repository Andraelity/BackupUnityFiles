using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectCancel_ButtonCommunicationNamespace 
{

    public static class SelectCancel_ButtonCommunicationClass
    {

        public static int int_AdditionStatusModified = 0;
        public static bool bool_StatusModified_Select = false;

        public static void SetAdditionModifier(int int_AdditionModifier)
        {

            int_AdditionStatusModified = int_AdditionModifier;
            bool_StatusModified_Select = true;

        }

    }
    // Start is called before the first frame update
}
