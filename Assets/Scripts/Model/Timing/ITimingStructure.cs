﻿using System.Threading.Tasks;

namespace model.timing {
    public abstract class ITimingStructure {
        event AsyncAction<ITimingStructure> Initiated;
        event AsyncAction<ITimingStructure> Completed;
        public string Name { get; }

        protected ITimingStructure(string name) {
            this.Name = name;
        }

        async public Task Initiate() {
            await Initiated?.Invoke(this);
            await Proceed();
            await Completed?.Invoke(this);
        }

        protected abstract Task Proceed();
    }
}
