using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class PlaggySlinger : MonoBehaviour
{
    private bool isCoroutineRunning = false;

    public static void FireTimer(int zeit, Action methode, float anzahl)
    {
        PlaggySlinger plaggySlinger = new GameObject("PlaggySlinger").AddComponent<PlaggySlinger>();
        plaggySlinger.StartCoroutine(plaggySlinger.Ausführen(zeit, methode, anzahl));
    }

    private IEnumerator MethodeStarten(int zeit, Action methode)
    {
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;

            yield return new WaitForSeconds(zeit);
            if (methode != null)
            {
                methode.Invoke();
            }

            isCoroutineRunning = false;
        }
    }

    private IEnumerator Ausführen(int zeit, Action methode, float anzahl)
    {
        yield return new WaitForSeconds(zeit);
        if(anzahl != -1)
        {
            AntiReturn = false;
        }
        if (anzahl == -1f)
        {
            yield return StartCoroutine(MethodeStarten(zeit, methode));
            yield return StartCoroutine(Ausführen(zeit, methode, anzahl));
            AntiReturn = true;
        }
        if (anzahl >= 1 && AntiReturn == false)
        {
            anzahl -= 1;
            yield return StartCoroutine(MethodeStarten(zeit, methode));
            yield return StartCoroutine(Ausführen(zeit, methode, anzahl));
        }
      
    }
    private bool AntiReturn;
}
