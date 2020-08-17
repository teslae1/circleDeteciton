using System;

namespace circleDeteciton.circellogic.settings
{
    class DoubleThresholdFilterSettings
    {
        public int LowThreshold = 20;
        public int HighThreshold = 100;
        public DoubleThresholdFilterSettings()
        {

        }

        public DoubleThresholdFilterSettings(int lowTreshold, int highThreshold)
        {
            ValidateThreshholds(highThreshold, lowTreshold);
            LowThreshold = lowTreshold;
            HighThreshold = highThreshold;
        }

        void ValidateThreshholds(int high, int low)
        {
            if ((high >= 0 &&
                low >= 0 &&
                high >= low) == false)
                throw new Exception("Invalid thresholds");
        }
    }
}
