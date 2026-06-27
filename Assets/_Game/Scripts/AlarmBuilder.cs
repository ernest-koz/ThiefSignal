using UnityEngine;

namespace ThiefSignal
{
    public static class AlarmBuilder
    {
        public static AlarmController Build(float rampSpeed)
        {
            GameObject alarmObject = new GameObject("Alarm");
            alarmObject.AddComponent<AudioSource>();

            AlarmController alarm = alarmObject.AddComponent<AlarmController>();
            alarm.Configure(AlarmSoundFactory.CreateSiren(), rampSpeed);

            return alarm;
        }
    }
}
