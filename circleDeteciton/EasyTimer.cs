using System.Timers;

namespace circleDeteciton
{
    class EasyTimer
    {
        Timer timer = new Timer();
        int milisecondsInterval = 100;
        int counter = 0;
        public void Start()
        {
            timer.Interval = milisecondsInterval;
            timer.Elapsed += Timer_Elapsed; 
            timer.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            counter++;
        }

        public string Stop()
        {
            timer.Stop();
            var secondsElapsed = counter / 10;
            var time = "Seconds: " + secondsElapsed + " hundreth/miliseconds: " + (counter - (secondsElapsed * 10));
            return time;
        }

        public void Reset()
        {
            counter = 0;
        }
    }
}
