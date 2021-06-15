using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;
using model.player;

namespace model.timing {
    public class Priority {
        public IPilot Pilot { get; private set; }
        public bool Passed { get; private set; } = false;
        public bool Declined { get; private set; } = true;
        private IList<IPlayOption> options = new List<IPlayOption>();
        private IPlayOption pass = new Pass();

        internal Priority(IPilot pilot, bool canPass) {
            this.Pilot = pilot;
            if (canPass) {
                options.Add(pass);
            }
        }

        public void Add(IPlayOption option) {
            options.Add(option);
        }

        async public Task Choose() {
            var option = Pilot.Choose(options);
            if (option.Legal) {
                await option.Resolve();
            } else {
                throw new System.Exception(Pilot + " chose an illegal option: " + option);
            }
            if (option == pass) {
                Passed = true;
            } else {
                Declined = false;
            }
        }

        private class Pass : IPlayOption {
            public bool Legal => true;

            async public Task Resolve() {
                await Task.CompletedTask;
            }
        }
    }
}
