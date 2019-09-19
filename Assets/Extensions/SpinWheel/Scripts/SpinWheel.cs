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
    [SerializeField] private float angleSelected;

    private bool spinning;
    private float anglePerItem;
    private int itemNumber;


    [SerializeField] private bool isClockwise;

    //Action
    public System.Action<int> OnStartSpinAction;
    public System.Action<int> OnEndSpinAction;

    public void SetAngleEnd(int choice)
    {
        if (choice == 0) angleSelected = 90;
        if (choice == 1) angleSelected = 270;
        if (choice == 2) angleSelected = 180;
        if (choice == 3) angleSelected = 0;
    }

    public void SetClockwise(bool isClokwise)
    {
        this.isClockwise = isClokwise;
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
            if (Input.GetKeyDown(KeyCode.Keypad8))
                Spin(8);
            if (Input.GetKeyDown(KeyCode.Keypad9))
                Spin(9);

        }
    }

    void Spin(int order)
    {

        if (OnStartSpinAction != null)
            OnStartSpinAction.Invoke(order);

        Debug.Log("itemNumber No. : " + itemNumber);

        //float angleAdjust = anglePerItem / (float)2;

        if (!isClockwise)
        {
            itemNumber = order;
            float maxAngle = 360 * randomTime + (itemNumber * anglePerItem) + angleSelected; //  + angleAdjust
            StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
        }
        else
        {
            itemNumber = order;
            var _itemNumber = itemNumber;
            if (order == 0) _itemNumber = 0;
            else _itemNumber = prize.Count - order;
            float maxAngle = 360 * randomTime + (_itemNumber * anglePerItem) - angleSelected; // - angleAdjust
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
