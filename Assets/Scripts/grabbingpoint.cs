using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabbingpoint : MonoBehaviour {

	//make _grabbable false when the branch breaks
	private bool _grabbable = true;
	public bool grabbable
	{
		get
		{
			return _grabbable;
		}

		set
		{
			_grabbable = value;

		}
	}

}
