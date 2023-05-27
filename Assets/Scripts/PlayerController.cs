using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public PlayerBallista ballista;
    SerialPort sp = new SerialPort("COM4", 38400);
    // Start is called before the first frame update
    void Start()
    {
        //        sp.Open();
        sp.ReadTimeout = 1;
        ballista = GetComponent<PlayerBallista>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            ballista.ShootingLogic();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ballista.Reload();
        }
        /* if (!sp.IsOpen)
         {
             sp.Open();
             sp.ReadTimeout = 1;
             ballista = GetComponent<PlayerBallista>();
         }
             if (!UIManager.deathPanelBool)
         {
             if (Input.GetAxis("Mouse X") != 0)
             {
                 transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed, 0);
             }
             if (sp.IsOpen)
             {

                 try
                 {

                     string smt = sp.ReadLine();
                     Debug.Log(smt);

                     if (int.TryParse(smt, out int number))
                     {
                         int value = System.Int32.Parse(smt);
                         ballista.ShootingLogic();
                     }
                     else
                     {
                         switch (smt)
                         {
                             case "ccw":
                                 MoveObject(-1);
                                 break;
                             case "cw":
                                 MoveObject(1);
                                 break;
                             case "S":
                                 ballista.ShootingLogic();
                                 break;
                             case "R":
                                 ballista.Reload();
                                 break;

                         }
                         // Debug.Log("NaN");
                     }

                 }
                 catch (System.Exception)
                 {
                     throw;
                 }
             }

         }*/
    }

    void MoveObject(int direction)
    {
        transform.Rotate(0, direction * Time.deltaTime * rotationSpeed, 0);
    }



    [field: SerializeField] public float rotationSpeed { get; private set; }



}