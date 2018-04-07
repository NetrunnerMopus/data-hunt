# Netrunner game client

## Design goals

* Enforce game rules. Do not rely on players manually fixing the game state.
* Allow players to make the mistake of accidentally revealing too much information.
  E.g. when the corp tries to play Hard-Hitting News, when the runner didn't run last turn, the card is revealed,
  the corp is explained why they cannot play it and is forced to take the card back. The card has no effect on the game state at all.
* Make it hard to make a mistake via input controls.
  E.g. avoid designs where a single click performs a game-state-affecting action, especially if the click targets are small and cramped.
  Prefer drag and drop.
* Show all critical information.
  E.g. do not hide remote servers behind a scroll bar.
* Make the look and feel attractive to new players.
  E.g. expose the great art of Netrunner. Don't make the cards microscopic. Have the ability to zoom in on a card.
  Play cyberpunky sound effects. Have some free space for streamers to put their videocam feed in.
* Be mobile-friendly.
  E.g. do not embed it to browsers, which might be drag-and-drop-unfriendly. Have big click targets. Lay out responsively.
* Educate new players.
  E.g. include visual aids of turn and run structures and progress. Explain why certain plays would be illegal.
  Include beginner decks by default. Consider a simple, short tutorial with a "static" AI.
* Do not rely on chat for core game mechanics, like asking "action?" every time there's a paid ability window.
