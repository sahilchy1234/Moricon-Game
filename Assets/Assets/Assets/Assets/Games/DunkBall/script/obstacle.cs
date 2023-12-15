using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	public int difficulty=0;
	public int appearance=0;
	public  int iteration;




	public void iterate(){

		if (appearance >0) {

			iteration++;
			if (iteration >= 10) {
				appearance = 0;
				iteration = 0;
			}

		}
	}
}
