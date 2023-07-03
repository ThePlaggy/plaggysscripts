using System;
using System.Collections;
using UnityEngine;

namespace PlaggysMod
{
	// Token: 0x020003D7 RID: 983
	public class PlaggySlinger : MonoBehaviour
	{
		// Token: 0x06001D2F RID: 7471
		public static void FireTimer(float zeit, Action methode, float anzahl)
		{
			PlaggySlinger plaggySlinger = new GameObject("PlaggySlinger").AddComponent<PlaggySlinger>();
			plaggySlinger.StartCoroutine(plaggySlinger.InvokeMethodWithDelay(zeit, methode));
			plaggySlinger.StartCoroutine(plaggySlinger.ErneutAusführen(zeit, methode, anzahl));
		}

		// Token: 0x06001D30 RID: 7472
		private IEnumerator InvokeMethodWithDelay(float zeit, Action methode)
		{
			if (!this.isCoroutineRunning)
			{
				this.isCoroutineRunning = true;
				yield return new WaitForSeconds(zeit);
				if (methode != null)
				{
					methode();
				}
				this.isCoroutineRunning = false;
			}
			yield break;
		}

		// Token: 0x06001D31 RID: 7473
		private IEnumerator ErneutAusführen(float zeit, Action methode, float anzahl)
		{
			yield return new WaitForSeconds(zeit);
			if (anzahl == -1f)
			{
				yield return base.StartCoroutine(this.InvokeMethodWithDelay(zeit, methode));
				yield return base.StartCoroutine(this.ErneutAusführen(zeit, methode, anzahl));
				this.AntiReturn = true;
			}
			if (anzahl >= 1f && !this.AntiReturn)
			{
				anzahl -= 1f;
				yield return base.StartCoroutine(this.InvokeMethodWithDelay(zeit, methode));
				yield return base.StartCoroutine(this.ErneutAusführen(zeit, methode, anzahl));
			}
			yield break;
		}

		// Token: 0x040019AC RID: 6572
		private bool isCoroutineRunning;

		// Token: 0x040019AD RID: 6573
		private bool AntiReturn;
	}
}
