using UnityEngine;

namespace AlmostEngine
{

		/// <summary>
		/// Use the singleton to manage persistent data between scenes.
		/// </summary>
		public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
		{
				public static T m_Instance;
				public bool m_DontDestroyOnLoad = true;

				/// <summary>
				/// Create the singleton instance if needed and call OnSingletonAwake().
				/// </summary>
				private void Awake ()
				{
						if (m_Instance == null) {
								//If I am the first instance, make me the Singleton
								m_Instance = this as T;

								if (transform.parent == null && m_DontDestroyOnLoad) {
										DontDestroyOnLoad (this.gameObject);
								}
						} else {
								//If a Singleton already exists and you find
								//another reference in scene, destroy it!
								if (this != m_Instance) {
										DestroyImmediate (this.gameObject);
								}
								return;
						}

						OnSingletonAwake ();
				}

				void OnDestroy ()
				{
						if (m_Instance == this) {
								m_Instance = null;
						}
				}

				/// <summary>
				/// This method is called just after the singleton construction.
				/// Override it to perform the initial setup.
				/// </summary>
				protected virtual void OnSingletonAwake ()
				{
				}
		}

}