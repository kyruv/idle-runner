using UnityEngine;
using UnityEngine.EventSystems;

public class UIBtnActions : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPlus = true;

    private float min_hold = .75f;
    private float every = .03f;

    private bool button_pressed;
    private float btn_counter = 0f;
    private float btn_counter2 = 0f;

    private int num_typed = 0;

    private RunManager runManager;

    void Start()
    {
        runManager = GameObject.Find("RunManager").GetComponent<RunManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button_pressed = true;
        btn_counter = 0f;
        btn_counter2 = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button_pressed = false;
        btn_counter = 0f;
        btn_counter2 = 0f;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            bool typed_num = false;
            for (int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
            {
                KeyCode key = (KeyCode)i;

                if (Input.GetKeyDown(key))
                {
                    int cur_num = i - (int)KeyCode.Alpha0;
                    num_typed = num_typed * 10 + cur_num;
                    if (num_typed > 0)
                    {
                        runManager.SetLevel(num_typed);
                        if (num_typed < 100)
                        {
                            typed_num = true;
                        }
                    }
                }
            }

            if (!typed_num)
            {
                num_typed = 0;
            }

            if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return))
            {
                runManager.StartRun();
            }
        }

        if (button_pressed)
        {
            btn_counter += Time.deltaTime;

            if (btn_counter > min_hold)
            {
                btn_counter2 += Time.deltaTime;

                if (btn_counter2 > every)
                {
                    btn_counter2 = 0;
                    if (isPlus)
                    {
                        runManager.IncreaseLevel();
                    }
                    else
                    {
                        runManager.DecreaseLevel();
                    }
                }
            }
        }
    }
}
