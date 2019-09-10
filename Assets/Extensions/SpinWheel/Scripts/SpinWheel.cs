using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpinWheel : MonoBehaviour
{
    public List<int> prize;

    public List<AnimationCurve> animationCurves;
    [Range(0, 4)]
    [SerializeField] private int animationSelectNo;

    [SerializeField] private Transform rotateTransfrom;
    [Range(0, 10)]
    [SerializeField] private int randomTime;

    [Range(0, 10)]
    [SerializeField] private float speed;

    /// <summary>
    /// Angle to put stick recieve reward
    /// </summary>
    private float angleSelected;

    private bool spinning;
    private float anglePerItem;
    private int itemNumber;

    [SerializeField] private bool isClockwise;

    //Action
    System.Action OnStartSpinAction;
    System.Action<int> OnEndSpinAction;

    public void SetAngleEnd(int choice)
    {
        if (choice == 0) angleSelected = 90;
        if (choice == 1) angleSelected = 270;
        if (choice == 2) angleSelected = 180;
        if (choice == 3) angleSelected = 0;
    }

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
    }

    void Update()
    {
        if (!spinning)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
                Spin(1);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                Spin(2);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                Spin(3);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                Spin(4);
            if (Input.GetKeyDown(KeyCode.Keypad5))
                Spin(5);
            if (Input.GetKeyDown(KeyCode.Keypad6))
                Spin(6);
            if (Input.GetKeyDown(KeyCode.Keypad7))
                Spin(7);
            if (Input.GetKeyDown(KeyCode.Keypad0))
                Spin(0);

            //randomTime = Random.Range(1, 4);
            //itemNumber = Random.Range(0, prize.Count);
            //float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
            //Debug.Log("itemNumber No. : " + itemNumber);
            //StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
        }
    }

    void Spin(int order)
    {

        if (OnStartSpinAction != null)
            OnStartSpinAction.Invoke();

        if (!isClockwise)
        {
            itemNumber = order;
            float maxAngle = 360 * randomTime + (itemNumber * anglePerItem) + angleSelected; // + angleSelected
            Debug.Log("itemNumber No. : " + itemNumber);
            StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
        }
        else
        {
            if (order == 0) itemNumber = 0;
            else itemNumber = prize.Count - order;
            float maxAngle = 360 * randomTime + (itemNumber * anglePerItem) - angleSelected; // - angleSelected
            Debug.Log("itemNumber No. : " + itemNumber);
            StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
        }
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {

        if (!isClockwise)
        {
            spinning = true;

            float timer = 0.0f;
            float startAngle = rotateTransfrom.eulerAngles.z;
            maxAngle = maxAngle - startAngle;

            //int animationCurveNumber = Random.Range(0, animationCurves.Count);
            int animationCurveNumber = animationSelectNo;
            Debug.Log("Animation Curve No. : " + animationCurveNumber);

            while (timer < time)
            {
                //to calculate rotation
                float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
                rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
                timer += speed * Time.deltaTime;
                yield return 0;
            }

            rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
            spinning = false;


            if (OnEndSpinAction != null)
                OnEndSpinAction.Invoke(itemNumber);
            Debug.Log("Prize: " + prize[itemNumber]);//use prize[itemNumnber] as per requirement
        }
        else
        {
            spinning = true;

            float timer = 0.0f;
            float startAngle = -rotateTransfrom.eulerAngles.z;
            maxAngle = startAngle - maxAngle;

            //int animationCurveNumber = Random.Range(0, animationCurves.Count);
            int animationCurveNumber = animationSelectNo;

            while (timer < time)
            {
                //to calculate rotation
                float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
                rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, angle - startAngle);
                timer += speed * Time.deltaTime;
                yield return 0;
            }

            rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle - startAngle);
            spinning = false;


            if (OnEndSpinAction != null)
                OnEndSpinAction.Invoke(itemNumber);
            Debug.Log("Prize: " + prize[itemNumber]);//use prize[itemNumnber] as per requirement
        }

    }
}
