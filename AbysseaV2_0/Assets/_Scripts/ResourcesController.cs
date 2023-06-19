using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    float _currentO2, _maxO2, _currentBattery, _maxBattery;

    float o2RechargePower = 20f, batteryRechergePower = 0.5f;

    public float O2
    {
        get
        {
            return _currentO2;
        }

        set
        {
            _currentO2 = value;
        }
    }

    public float MaxO2
    {
        get
        {
            return _maxO2;
        }

        set
        {
            _maxO2 = value;
        }
    }

    public float Battery
    {
        get
        {
            return _currentBattery;
        }

        set
        {
            _currentBattery = value;
        }
    }

    public float MaxBattery
    {
        get
        {
            return _maxBattery;
        }

        set
        {
            _maxBattery = value;
        }
    }

    public bool rechargeO2, rechargeBattery;

    private void OnEnable()
    {
        rechargeO2 = false;
        rechargeBattery = false;
    }

    public void InitializeResources(float o2, float maxO2, float battery, float maxBattery)
    {
        _currentO2 = o2;
        _maxO2 = maxO2;
        _currentBattery = battery;
        _maxBattery = maxBattery;
    }
    
    //CONSUMO DE OXIGÊNIO
    public void O2Controller(float o2Amount)
    {
        if (rechargeO2 == true)
        {
            if(_currentO2 < _maxO2)
            {
                _currentO2 += o2RechargePower * Time.deltaTime;
                
                if(_currentO2 > _maxO2)
                {
                    _currentO2 = _maxO2;
                }
            }
        }
        else
        {
            if(_currentO2 >= 0)
            {
                _currentO2 -= o2Amount * Time.deltaTime;
            }
        }
    }

    //CONSUMO DE BATERIA
    public void BatteryController(float batteryAmount)
    {
        if (rechargeBattery == true)
        {
            if (_currentBattery < _maxBattery)
            {
                _currentBattery += batteryRechergePower * Time.deltaTime;

                if (_currentBattery > _maxBattery)
                {
                    _currentBattery = _maxBattery;
                }
            }
        }
        else
        {
            if (_currentBattery >= 0)
            {
                _currentBattery -= batteryAmount * Time.deltaTime;
            }
        }
    }
}
