using UnityEngine;

public class Rotate3DObject : MonoBehaviour
{
    private float m_rotationSpeed = 25; 
    private float m_timeToTravel = 3;
    private bool m_direction = false; // Direction at the object is rotating
        public float RotationSpeed{
        get { return m_rotationSpeed; } 
        set { 
            if (value > 0 && value <= 90) { // Set boundaries for recivied value
                m_rotationSpeed = value; 
            } else { 
                Debug.Log("Out of boundaries value"); 
            }
        }
    }
    private int m_state = 0;
    void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", this.transform.position + new Vector3(0, 5, 5), "easetype", iTween.EaseType.easeOutElastic, "time", m_timeToTravel));
    }

    // Update is called once per frame
    // Time.deltaTime is the amount of time between each frame
    void Update()
    {
        // Old input system - Do with new Input System later
        /*
        if (Input.GetKey(KeyCode.Space) == true){
            m_direction = true;
        } else {
            m_direction = false;
        }

        if (m_direction == true){
            this.transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0)); // Rotate 25 degrees in Y axis every second
        } else {
            this.transform.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0)); // Rotate 25 degrees in Y axis every second
        }
        */

        if (Input.GetKeyDown(KeyCode.Space) == true) {
            m_state = (m_state + 1) % 4; // The "%" symbol performs an integer division and returns the remainder, so it sets to 0 if it's 3 before pressing the space bar
        }

        switch(m_state)
        {
            case 0:
                this.transform.Rotate(new Vector3(0, m_rotationSpeed * Time.deltaTime, 0));
                break;
            case 1:
                this.transform.Rotate(new Vector3(0, -m_rotationSpeed * Time.deltaTime, 0));
                break;
            case 2:
                this.transform.Rotate(new Vector3(0, 4 * m_rotationSpeed * Time.deltaTime, 0));
                break;
            case 3:
                this.transform.Rotate(new Vector3(4 * m_rotationSpeed * Time.deltaTime, 0, 0));
                break;
        }

    }
}
