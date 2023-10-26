using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    /// <summary>
    /// The name of the player tag.
    /// </summary>
    public const string TAG_PLAYER = "Player";

    /// <summary>
    /// The name of the state interact animation
    /// </summary>
    public const string NAME_STATE_ANIM_INTERACT = "Interact";

    /// <summary>
    /// The name of the state interact animation
    /// </summary>
    public const string NAME_STATE_ANIM_WALK = "Walk";

    /// <summary>
    /// The name of the state interact animation
    /// </summary>
    public const string NAME_STATE_ANIM_SADIDLE = "Sad Idle";

    /// <summary>
    /// A static boolean the controls this class' logging output.
    /// </summary>
    public static bool LOGGING_ON = true;

    /// <summary>
    /// A static method that provides support for centralized logging.
    /// </summary>
    /// <param name="s"></param>
    public static void wr(string s)
    {
        if (LOGGING_ON)
        {
            Debug.Log(s);
        }
    }

    /// <summary>
    /// A static method that provides support for centralized logging. This method always tries to write the debug log.
    /// </summary>
    /// <param name="s"></param>
    public static void wrForce(string s)
    {
        Debug.Log(s);
    }

    /// <summary>
    /// A static method that provides support for more detailed centralized logging.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="sClass"></param>
    /// <param name="sMethod"></param>
    /// <param name="sNote"></param>
    public static void wr(string s, string sClass, string sMethod, string sNote)
    {
        if (LOGGING_ON)
        {
            Debug.Log(sClass + "." + sMethod + ": " + sNote + ": " + s);
        }
    }

    /// <summary>
    /// A static method that provides support for centralized error logging.
    /// </summary>
    /// <param name="s"></param>
    public static void wrErr(string s)
    {
        if (LOGGING_ON)
        {
            Debug.LogError(s);
        }
    }

    /// <summary>
    /// A static method that provides support for more centralized error logging.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="sClass"></param>
    /// <param name="sMethod"></param>
    /// <param name="sNote"></param>
    public static void wrErr(string s, string sClass, string sMethod, string sNote)
    {
        if (LOGGING_ON)
        {
            Debug.LogError(sClass + "." + sMethod + ": " + sNote + ": " + s);
        }
    }
}
