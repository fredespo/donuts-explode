using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
namespace AlmostEngine.Examples
{
    public class CameraController : MonoBehaviour
    {
        public bool m_MouseLookOnClickOnly = true;
        public float m_RotationCoeff = 200.0f;
        public float m_TranslationCoeff = 8.0f;
        public float m_TranslationMouseCoeff = 2.0f;
        public float m_TranslationMouseScrollCoeff = 25.0f;
        Transform m_Character;
        public Transform m_Head;
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
        Vector2 m_PreviousMousePosition;
#else
        Vector3 m_PreviousMousePosition;
#endif


#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
        public UnityEngine.InputSystem.InputActionReference m_MoveAction;
#endif

        void Start()
        {
            m_Character = transform;
            if (m_Head == null)
            {
                m_Head = GetComponentInChildren<Camera>().transform;
            }
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            m_PreviousMousePosition = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
            if (m_MoveAction != null)
            {
                m_MoveAction.action.Enable();
            }
#else
            m_PreviousMousePosition = Input.mousePosition;
#endif
        }
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
        bool wasPressed = false;
#endif

        void Update()
        {
            // Keyboard
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            float forward = m_MoveAction != null ? m_MoveAction.action.ReadValue<Vector2>().y : 0f;
            float left = m_MoveAction != null ? m_MoveAction.action.ReadValue<Vector2>().x : 0f;
#else
			float forward = Input.GetAxis ("Vertical") * Time.deltaTime * m_TranslationCoeff; 
			float left = Input.GetAxis ("Horizontal") * Time.deltaTime * m_TranslationCoeff;
#endif
            m_Character.transform.position += m_Head.transform.forward * forward + m_Head.transform.right * left;

            // Mouse plannar
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            Vector2 mousePosition = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
            bool mouseButton1 = Mouse.current.rightButton.isPressed;
            bool mouseButton2 = Mouse.current.middleButton.isPressed;
            wasPressed = mouseButton2;
#else
            Vector3 mousePosition = Input.mousePosition;
            bool mouseButton1 = Input.GetMouseButton(1);
            bool mouseButton2 = Input.GetMouseButton(2);
#endif

            if (mouseButton2)
            {
                float up = -(mousePosition - m_PreviousMousePosition).y * m_TranslationMouseCoeff * Time.deltaTime;
                float right = -(mousePosition - m_PreviousMousePosition).x * m_TranslationMouseCoeff * Time.deltaTime;

                m_Character.transform.position += m_Head.transform.up * up + m_Head.transform.right * right;
            }

            // Mouse scroll
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            float scroll = Mouse.current.scroll.y.ReadValue() * m_TranslationMouseScrollCoeff * Time.deltaTime;
#else
			float scroll = Input.mouseScrollDelta.y * m_TranslationMouseScrollCoeff * Time.deltaTime;
#endif
            m_Character.transform.position += m_Head.transform.forward * scroll;


            if (!m_MouseLookOnClickOnly || mouseButton1)
            {

                // Mouse look
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
                float x = (mousePosition - m_PreviousMousePosition).x;
                float y = -(mousePosition - m_PreviousMousePosition).y;
#else
			float x = Input.GetAxis ("Mouse X") * Time.deltaTime * m_RotationCoeff;
			float y = -Input.GetAxis ("Mouse Y") * Time.deltaTime * m_RotationCoeff;
#endif
                m_Head.localRotation = ClampRotationAroundXAxis(m_Head.localRotation * Quaternion.AngleAxis(y, Vector3.right));
                m_Character.localRotation *= Quaternion.AngleAxis(x, Vector3.up);
            }

            m_PreviousMousePosition = mousePosition;
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, -80f, 80f);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}
