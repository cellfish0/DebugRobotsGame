using UnityEngine;

namespace Assets.Parsers.RoboParser
{
    public interface IHeatable
    {
        float TemperatureC { get; }
        float TemperatureF { get; }
        void HeatUp(float value);
        void CoolDown(float value);
    }

    public abstract class TestSubject : ScriptableObject, IHeatable
    {
        [SerializeField] private float temperatureC;
        private float temperatureF;
        [SerializeField]private string _name;

        public string Name => _name; 
        public abstract bool IsHuman();
        public abstract bool IsDead();
        public float TemperatureC { get => temperatureC; protected set => SetTemperatureC(value); }
        public float TemperatureF { get => temperatureF; protected set => SetTemperatureF(value); }

        private void SetTemperatureC(float value)
        {
            temperatureC = value;
            temperatureF = value * 9 / 5 + 32;
        }

        private void SetTemperatureF(float value)
        {
            temperatureF = value;
            temperatureC = (value - 32) * 5 / 9;
        }

        public void HeatUp(float value)
        {
            if (value < 0)
            {
                throw new NegativeValueError(null, -1, "HeatUp");
            }
            TemperatureC += value;

        }

        public void CoolDown(float value)
        {
            if (value < 0)
            {
                throw new NegativeValueError(null, -1, "CoolDown");
            }

            if (TemperatureC - value < -273.15f)
            {
                throw new RoboError("Cannot cool down below absolute zero!");
            }

            TemperatureC -= value;

        }


    }


}
