using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using controller;
using model;
using model.cards;
using model.choices;
using model.choices.trash;
using model.play;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RectTransform;

namespace view.gui
{
    public class TrashChoiceScreen : IDecision<Card>
    {
        private GameObject blanket;
        private CardPrinter subjectRow;
        private GameObject optionsRow;
        private IDictionary<Card, GameObject> visuals = new Dictionary<Card, GameObject>();

        public TrashChoiceScreen(GameObject board)
        {
            blanket = CreateBlanket(board);
            subjectRow = CreateSubjectRow(blanket);
            optionsRow = CreateOptionsRow(blanket);
        }

        private GameObject CreateBlanket(GameObject board)
        {
            var blanket = new GameObject("Trash screen")
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

        private CardPrinter CreateSubjectRow(GameObject blanket)
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
            return subjectRow.AddComponent<CardPrinter>();
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

       override public Task<ITrashOption> Declare(Card card, IEnumerable<ITrashOption> options, Game game)
        {
            var subject = subjectRow.Print(card);
            blanket.transform.SetAsLastSibling();
            blanket.SetActive(true);
            var optionsResolving = options
                .Select(option =>
                    DisplayOption(option, card, subject, game)
                )
                .ToArray();
            var chosenIndex = Task.WaitAny(optionsResolving);
            blanket.SetActive(false);
            return optionsResolving[chosenIndex];
        }

        async private Task<ITrashOption> DisplayOption(ITrashOption option, Card card, GameObject subject, Game game)
        {
            var optionCard = new GameObject("Trash option " + option);
            var image = optionCard.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>(option.Art);
            image.preserveAspect = true;
            var rectangle = image.rectTransform;
            rectangle.SetSizeWithCurrentAnchors(Axis.Horizontal, 100);
            rectangle.SetSizeWithCurrentAnchors(Axis.Vertical, 100);
            optionCard.transform.SetParent(optionsRow.transform);
            var ability = option.AsAbility(card);
            var resolving = new AwaitingResolutionObserver();
            ability.ObserveResolution(resolving);
            var dropZone = optionCard.AddComponent<DropZone>();
            subject
                 .AddComponent<DroppableChoice>()
                 .Represent(dropZone);
            await resolving.AwaitResolution();
        }
    }
}