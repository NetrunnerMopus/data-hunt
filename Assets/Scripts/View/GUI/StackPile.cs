﻿using UnityEngine;
using controller;
using model;
using model.zones.runner;

namespace view.gui {
	public class StackPile : MonoBehaviour, IStackPopObserver {
		private Game game;
		private DropZone gripZone;
		private GameObject top;
		private CardPrinter printer;
		public void Construct(Game game, DropZone gripZone) {
			this.game = game;
			this.gripZone = gripZone;
		}

		void Awake() {
			printer = gameObject.AddComponent<CardPrinter>();
		}

		void IStackPopObserver.NotifyCardPopped(bool empty) {
			Destroy(top);
			if (!empty) {
				top = printer.PrintRunnerFacedown("Top of stack");
				top.transform.SetAsFirstSibling();
				top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
				var rect = top.GetComponent<RectTransform>();
				rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
				top
					.AddComponent<DroppableAbility>()
					.Represent(
						game.runner.actionCard.draw,
						game,
						gripZone
					);
			}
		}
	}
}