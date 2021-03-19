using System;

namespace SalamandrBag.animal
{
    public class AnimalState
    {
        private AnimalType type;
        private String speech;

        public AnimalState(AnimalType type, string speech)
        {
            this.type = type;
            this.speech = speech;
        }

        public AnimalType GetType()
        {
            return type;
        }

        public String GetSpeech()
        {
            return speech;
        }
    }
}