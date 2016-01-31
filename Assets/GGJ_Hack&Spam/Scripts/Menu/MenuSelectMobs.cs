using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuSelectMobs : MenuSelect
{
	public GameObject Mob1 = null;
	public GameObject Mob2 = null;
	public GameObject Mob3 = null;
	public GameObject Mob4 = null;
	public GameObject Mob5 = null;
	public GameObject Mob6 = null;
	public GameObject Mob7 = null;
	public GameObject Mob8 = null;

	protected override void HandleMessageRecievedEvent(MessageIRC message)
	{
		base.HandleMessageRecievedEvent(message);

		/*WordComparator wc = new WordComparator();
		wc.Comparator(*/
	}
}
