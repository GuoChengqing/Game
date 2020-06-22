using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Message
{
    public static string message;

    public static void SendMessage(string message)
    {
        Message.message = message;
    }

    public static string GetMessage()
    {
        return Message.message;
    }
}
