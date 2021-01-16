using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using controller;
using model;
using model.cards;
using model.choices;
using model.choices.steal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RectTransform;

namespace view.gui
{
    public class StealChoiceScreen : IDecision<Card, IStealOption>
    {
        private GameObject blanket;
        private CardPrinter subjectRow;
        private GameObject optionsRow;
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public StealChoiceScreen(BoardParts parts)
        {
            blanket = CreateBlanket(parts.board);
            subjectRow = CreateSubjectRow(blanket, parts);
            optionsRow = CreateOptionsRow(blanket);
        }

        private GameObject CreateBlanket(GameObject board)
        {
            var blanket = new GameObject("Steal screen")
            {
                layer = 5
            };
            blanket.SetActive(false);
            var image = blanket.AddComponent<Image>();
            image.color = new Color(0, 0, 0, 0.5f);
            blanket.transform.SetParent(board.transform);
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.05f, 0.05f);
            rectangle.anchorMax = new Vector2(0.95f, 0.95f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return blanket;
        }

        private CardPrinter CreateSubjectRow(GameObject blanket, BoardParts parts)
        {
            var subjectRow = new GameObject("Subject row")
            {
                layer = 5
            };
            subjectRow.transform.SetParent(blanket.transform);
            var rectangle = subjectRow.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.1f, 0.5f);
            rectangle.anchorMax = new Vector2(0.9f, 1.0f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return parts.Print(subjectRow);
        }


        private GameObject CreateOptionsRow(GameObject blanket)
        {
            var optionsRow = new GameObject("Options row")
            {
                layer = 5
            };
            optionsRow.transform.SetParent(blanket.transform);
            var rectangle = optionsRow.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.1f, 0.0f);
            rectangle.anchorMax = new Vector2(0.9f, 0.5f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            var horizontalLayout = optionsRow.AddComponent<HorizontalLayoutGroup>();
            horizontalLayout.spacing = 200;
            return optionsRow;
        }

        async Task<IStealOption> IDecision<Card, IStealOption>.Declare(Card card, IEnumerable<IStealOption> options, Game game)
        {
            var subject = subjectRow.Print(card);
            blanket.transform.SetAsLastSibling();
            blanket.SetActive(true);
            var droppableChoices = options.Select(it => DisplayOption(it, card, subject, game)).ToList();
            var asyncChoices = droppableChoices.Select(it => it.AwaitChoice()).ToArray();
            var choice = await Task.WhenAny(asyncChoices);
            blanket.SetActive(false);
            Dispose(subject);
            droppableChoices.ForEach(it => Dispose(it.GameObject));
            return choice.Result;
        }

        private void Dispose(GameObject o)
        {
            o.transform.SetParent(null);
            Object.Destroy(o);
        }

        private InteractiveChoice<IStealOption> DisplayOption(IStealOption option, Card card, GameObject subject, Game game)
        {
            var optionCard = new GameObject("Steal option " + option);
            var image = optionCard.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>(option.Art);
            image.preserveAspect = true;
            var rectangle = image.rectTransform;
            rectangle.SetSizeWithCurrentAnchors(Axis.Horizontal, 100);
            rectangle.SetSizeWithCurrentAnchors(Axis.Vertical, 100);
            optionCard.transform.SetParent(optionsRow.transform);
            var dropZone = optionCard.AddComponent<DropZone>();
            bool legal = option.IsLegal(game);
            var choice = new InteractiveChoice<IStealOption>(option, legal, dropZone, optionCard);
            subject.AddComponent<Droppable>().Represent(choice);
            return choice;
        }
    }
}
