using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing {
	public class GameFlow {
		public readonly PaidWindow paidWindow = new PaidWindow();
		public readonly corp.Turn corpTurn;
		public readonly runner.Turn runnerTurn;
		private bool ended;
		private HashSet<IGameFinishObserver> finishObservers = new HashSet<IGameFinishObserver>();

		public GameFlow(corp.Turn corpTurn, runner.Turn runnerTurn) {
			this.corpTurn = corpTurn;
			this.runnerTurn = runnerTurn;
		}

		async public Task Start() {
			try {
				while (!ended) {
					await corpTurn.Start();
					await runnerTurn.Start();
				}
			} catch (Exception e) {
				if (ended) {
					UnityEngine.Debug.Log("The game is over! " + e.Message);
				}
			}
		}

		public void ObserveFinish(IGameFinishObserver observer) {
			finishObservers.Add(observer);
		}

		public void DeckCorp() {
			Finish(new GameFinish(
				"The Runner",
				"The Corp",
				"Corp R&D is empty"
			));
		}

		private void Finish(GameFinish finish) {
			ended = true;
			foreach (var observer in finishObservers) {
				observer.NotifyGameFinished(finish);
			}
			throw new Exception("Game over, " + finish.reason);
		}
	}

	public interface IGameFinishObserver {
		void NotifyGameFinished(GameFinish finish);
	}

	public class GameFinish {
		public string winner;
		public string loser;
		public string reason;

		public GameFinish(string winner, string loser, string reason) {
			this.winner = winner;
			this.loser = loser;
			this.reason = reason;
		}
	}
}