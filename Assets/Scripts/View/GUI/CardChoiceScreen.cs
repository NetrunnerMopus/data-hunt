using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using controller;
using model;
using model.cards;
using model.choices;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CardChoiceScreen : IDecision<string, Card>
    {
        private GameObject blanket;
        private CardPrinter subjectRow;
        private CardPrinter optionsRow;
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public CardChoiceScreen(BoardParts parts)
        {
            blanket = CreateBlanket(parts.board);
            subjectRow = CreateSubjectRow(blanket, parts);
            optionsRow = CreateOptionsRow(blanket, parts);
        }

        private GameObject CreateBlanket(GameObject board)
        {
            var blanket = new GameObject("Card choice screen")
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


        private CardPrinter CreateOptionsRow(GameObject blanket, BoardParts parts)
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
            return parts.Print(optionsRow);
        }

        async Task<Card> IDecision<string, Card>.Declare(string q, IEnumerable<Card> options)
        {
            var subject = subjectRow.Print("Which card to access?", "Images/UI/hand-click"); // TODO good for accessing, but `CardChoiceScreen` might be used in other contexts
            blanket.transform.SetAsLastSibling();
            blanket.SetActive(true);
            var droppableChoices = options.Select(it => DisplayOption(it, q, subject)).ToList();
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

        private InteractiveChoice<Card> DisplayOption(Card option, string q, GameObject subject)
        {
            var optionCard = optionsRow.Print(option);
            var dropZone = optionCard.AddComponent<DropZone>();
            var choice = new InteractiveChoice<Card>(option, true, dropZone, optionCard);
            subject.AddComponent<Droppable>().Represent(choice);
            return choice;
        }
    }
}
