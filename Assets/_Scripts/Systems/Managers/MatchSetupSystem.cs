
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchSetupSystem : MonoBehaviour
{
    

    [SerializeField] Transform heroViewPos;
    
    [SerializeField] TMP_Text deckUIText;

    private void Start()
    {
        HeroSystem.Instance.SpawnHeroView(heroViewPos);
        
        //Setup HeroData
        HeroSystem.Instance.Setup(HeroSystem.Instance.HeroData);
        //Initialize the map if its a start of the act
        if(MapSystem.Instance.CurrentNode == null)
        {
            MapSystem.Instance.InitializeMap();
        }

        //Initialize the Deck if its a start of the run
        if(DeckSystem.Instance.Deck.Count == 0)
        {
            DeckSystem.Instance.InitializeDeck(HeroSystem.Instance.HeroData.Deck);
        }
        //Setup DeckData to CardSystem
        CardSystem.Instance.Setup(DeckSystem.Instance.Deck.ToList());
        
        //if its a combat encounter , setup Enemies 
        if ( MapSystem.Instance.CurrentNode.RoomType == RoomType.RCOMBAT || MapSystem.Instance.CurrentNode.RoomType == RoomType.ECOMBAT || MapSystem.Instance.CurrentNode.RoomType == RoomType.BOSS)
        {
            EnemySystem.Instance.Setup(MapSystem.Instance.CurrentNode.Enemies);
        }
        
        DeckSystem.Instance.SetUIText(deckUIText);
           
        

            RefillManaGA refillManaGA = new();

        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardsGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);

        });

        
    }

    public void ProceedToLobby()
    {
        MapSystem.Instance.CompleteNode();
        SceneManager.LoadScene(SceneNames.Lobby);
    }
}
