using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
public class SerialReader : MonoBehaviour
{
    //COM改成自己，後面數字跟Arduino一樣。
    private readonly SerialPort _serialPort = new SerialPort("COM5", 115200);
    private Thread _readThread;
    [SerializeField] private float _data = 1;
    public event Action<float> OnDataReceived;
    public static SerialReader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        _readThread = new Thread(ReadSerial);
        DontDestroyOnLoad(this);
        Instance = this;
        _serialPort.Open();
        _readThread.Start();
    }
    private void OnDestroy()
    {
        if (Instance != this)
            return;
        _readThread.Abort();
        _serialPort.Close();
    }
    
    private void ReadSerial()
    {
        while (_serialPort.IsOpen)
        {
            OnDataReceived?.Invoke(float.Parse(_serialPort.ReadLine()));
            print(_serialPort.ReadLine());
        }
    }
}