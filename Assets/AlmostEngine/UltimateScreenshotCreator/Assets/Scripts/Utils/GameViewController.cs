using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif


namespace AlmostEngine.Screenshot
{
    /// <summary>
    /// The Game View controller is used to set and unset the gameview size.
    /// </summary>
    public static class GameViewController
    {

#if UNITY_EDITOR

        static int m_PreviousSizeId;
        static EditorWindow m_PreviousWindowFocus;



#if (UNITY_5_4_OR_NEWER)
        static string m_AspectName = "UltimateScreenshotCreator";
        //static int m_PreviousWidth;
        //static int m_PreviousHeight;


#else
	
		static Rect m_PreviousRect;
		static Vector2 m_PreviousMinSize;
		static Vector2 m_PreviousMaxSize;

#endif





        public static void SaveCurrentGameViewSize()
        {
            // Save current size id
            m_PreviousSizeId = GameViewUtils.GetCurrentSizeIndex();

            // Save current focused window
            m_PreviousWindowFocus = EditorWindow.focusedWindow;

            // Debug.Log("Saving current gameview size id " + m_PreviousSizeId);

            // Save current gameview properties

#if (UNITY_5_4_OR_NEWER)
            //Vector2 size = GameViewUtils.GetCurrentGameViewSize();
            //m_PreviousWidth = (int)size.x;
            //m_PreviousHeight = (int)size.y;


#else
			m_PreviousRect = GameViewUtils.GetCurrentGameViewRect ();
			m_PreviousMinSize = GameViewUtils.GetGameView ().minSize;
			m_PreviousMaxSize = GameViewUtils.GetGameView ().maxSize;
			LayoutUtils.SaveLayoutToFile ("UltimateScreenshotCreator_layout.bk");
#endif

        }



        public static void SetGameViewSize(int width, int height)
        {
#if (UNITY_5_4_OR_NEWER)

            // Checks if temp size exists, creates it if needed
            if (!GameViewUtils.SizeExists(m_AspectName))
            {
                GameViewUtils.AddCustomSize(GameViewUtils.SizeType.FIXED_RESOLUTION, width, height, m_AspectName);
            }

            // Set the size of the custom size
            int size = GameViewUtils.FindSize(m_AspectName);
            if (size == -1)
            {
                Debug.LogError("Can not find the gameview size " + m_AspectName);
                return;
            }
            GameViewUtils.SetCurrentSizeIndex(size);

            // Change the size
            GameViewUtils.SetGameViewSize(width, height);

#else

			// Force Free aspect
			GameViewUtils.SetCurrentSizeIndex (0);

			// Change rect size
			int topOffset = 17;
			GameViewUtils.GetGameView ().minSize = new Vector2 (width, height + topOffset);
			GameViewUtils.GetGameView ().maxSize = new Vector2 (width, height + topOffset);
			GameViewUtils.SetGameViewRect (new Rect (m_PreviousRect.x, m_PreviousRect.y, width, height + topOffset));

			// Force repaint for "waitendoframe"
			GameViewUtils.GetGameView ().Repaint ();	

#endif
        }

        public static void RestoreGameViewSize(int id = -1)
        {
            // Debug.Log("Restoring current gameview size id " + m_PreviousSizeId);
            // Restore previous size id
            GameViewUtils.SetCurrentSizeIndex(m_PreviousSizeId);

#if (UNITY_5_4_OR_NEWER)

            // Restore previous gameview size 
            // GameViewUtils.SetGameViewSize((int)m_PreviousWidth, (int)m_PreviousHeight);

            // Remove temp size id
            //int size = GameViewUtils.FindSize(m_AspectName);
            //if (size != -1) {
            //	GameViewUtils.RemoveCustomSize(size);
            //}

#else
	
			// Restore gameview rect
			GameViewUtils.GetGameView ().minSize = m_PreviousMinSize;
			GameViewUtils.GetGameView ().maxSize = m_PreviousMaxSize;		
			GameViewUtils.SetGameViewRect (m_PreviousRect);
		
#endif


            // Restore previous focus windows
            if (m_PreviousWindowFocus != null)
            {
                m_PreviousWindowFocus.Focus();
            }

        }

        public static Vector2 GetCurrentGameViewSize()
        {
            string[] res = UnityStats.screenRes.Split('x');
            return new Vector2(int.Parse(res[0]), int.Parse(res[1]));

            //			if (GameViewUtils.GetCurrentSizeType () == GameViewUtils.SizeType.RATIO) {
            //				return GameViewUtils.GetCurrentGameViewRectSize ();
            //			} else {
            //				return GameViewUtils.GetCurrentGameViewSize ();
            //			}
        }

        public static void RestoreLayout()
        {
#if (!(UNITY_5_4_OR_NEWER))

			LayoutUtils.LoadLayoutFromFile ("UltimateScreenshotCreator_layout.bk");

#endif
        }


#elif UNITY_STANDALONE_WIN
				static bool m_PreviousFullscreen;
				static bool m_PreviousResizable;
				static Vector2 m_PreviousSize;
				static Vector2 m_PreviousPos;
				static int m_PreviousLong;
				
				public static void SaveCurrentGameViewSize ()
				{
					m_PreviousFullscreen = Screen.fullScreen;
					m_PreviousSize.x = Screen.width;
					m_PreviousSize.y = Screen.height;
					
					int handle = GetForegroundWindow();
					m_PreviousLong = GetWindowLong(handle, GWL_STYLE);
					
					// Set Window mode
					if (Screen.fullScreen) {
						Screen.SetResolution (Screen.width, Screen.height, false);
					}
				
				}
				public static void SetGameViewSize (int width, int height)
				{			
					int handle = GetForegroundWindow();
					int newLong = m_PreviousLong;
					newLong &= ~(WS_CAPTION | WS_THICKFRAME | WS_MINIMIZE | WS_MAXIMIZE | WS_SYSMENU);
					SetWindowLong(handle, GWL_STYLE,  newLong);
					SetWindowPos(handle, 0, 0,0,width,height, SWP_FRAMECHANGED);

				}
			
				public static void RestoreGameViewSize ()
				{
					int handle = GetForegroundWindow();
					SetWindowLong(handle, GWL_STYLE, m_PreviousLong);

					Screen.SetResolution ((int)m_PreviousSize.x, (int)m_PreviousSize.y, m_PreviousFullscreen);			
				}

				public static Vector2 GetCurrentGameViewSize ()
				{
					return new Vector2 (Screen.width, Screen.height);
				}
					
				private const int GWL_STYLE = -16;              //hex constant for style changing
				private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
				private const int WS_CAPTION = 0x00C00000;
				private const int WS_THICKFRAME = 0x00040000;
				private const int WS_MINIMIZE = 0x20000000;
				private const int WS_MAXIMIZE = 0x01000000;
				private const int SWP_FRAMECHANGED = 0x0020;

			
				[DllImport("user32.dll")] 
				static extern int GetForegroundWindow();

				[DllImport("user32.dll", EntryPoint="MoveWindow")]  
				static extern int  MoveWindow (int hwnd, int x, int y,int nWidth,int nHeight,int bRepaint );

				[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
				public static extern int SetWindowPos(int hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

				[DllImport("user32.dll", EntryPoint="SetWindowLongA")]  
				static extern int  SetWindowLong (int hwnd, int nIndex, int dwNewLong);
				
				[DllImport("user32.dll")]
				public static extern int GetWindowLong(int hWnd, int nIndex);

				[DllImport("user32.dll")]
				static extern bool ShowWindowAsync(int hWnd, int nCmdShow);


				
				

#else
						
				
				public static void SaveCurrentGameViewSize ()
				{
					Debug.LogError ("GAMEVIEW_RESIZING capture mode is only available  for Editor and Windows Standalone.");
				}
				
				public static void SetGameViewSize (int width, int height)
				{
					Debug.LogError ("GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");			
				}
				
				public static void RestoreGameViewSize ()
				{
					Debug.LogError ("GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");
				}
				
				public static Vector2 GetCurrentGameViewSize ()
				{
					return new Vector2 (Screen.width, Screen.height);
				}
#endif


    }

}
