using ASCOM.DeviceInterface;

namespace ConeSharp
{
    internal class AllDoneStep : IStep
    {
        private State _state;

        public void Initialize(State state)
        {
            _state = state;
        }

        public string Title { get { return "Alignment Complete"; } }
        public string Description { get { return "Cone error correction complete."; } }
        public string MoreInfo { get { return "Press 'Next' to repeat the procedure to check the alignment and make further corrections";  } }
        public IStep OnNext()
        {
            return new GotoWestStep();
        }

        public void OnButton()
        {

        }

        public string ButtonText { get { return null; } }

        public bool TopMost
        {
            get { return false; }
        }

    }
}