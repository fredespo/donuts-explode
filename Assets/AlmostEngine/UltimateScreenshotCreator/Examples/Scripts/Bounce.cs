using UnityEngine;
using System.Collections;


namespace AlmostEngine.Examples
{
	public class Bounce : MonoBehaviour
	{

		Vector3 m_Origin;
		float m_Offset;

		void Start ()
		{
			m_Origin = transform.position;
			m_Offset = Mathf.PI * Random.value;
		}

		void Update ()
		{
			transform.position = m_Origin + new Vector3 (0f, Mathf.Abs (Mathf.Sin (m_Offset + 3f * Time.time)), 0f);
		}
	}

}