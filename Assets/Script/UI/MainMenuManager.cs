using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private Button buttonTapToStart;
    [SerializeField]private Button buttonNewGame;
    [SerializeField]private Button buttonLoadGame;
    [SerializeField]private Button buttonQuit;
    [SerializeField]private Button buttonBackMenu;
    [SerializeField] private Button buttonPrevSelectHero;
    [SerializeField] private Button buttonNextSelectHero;

    [SerializeField]private RectTransform mainMenuBackGround;

    [SerializeField]private GameObject tapToStart;
    [SerializeField]private GameObject menu;
    [SerializeField]private GameObject selectHero;
    [SerializeField]private GameObject menuLoadGame;
    [SerializeField]private GameObject gameobjectBackMenu;

    [SerializeField] private SelectHeroController showHero;


    private void Start()
    {
        buttonTapToStart.onClick.AddListener(TapToStart);
        buttonNewGame.onClick.AddListener(NewGame);
        buttonLoadGame.onClick.AddListener(LoadGame);
        buttonQuit.onClick.AddListener(Quit);
        buttonBackMenu.onClick.AddListener(ButtonBackMenu);
        buttonPrevSelectHero.onClick.AddListener(SelectPrev);
        buttonNextSelectHero.onClick.AddListener(SelectNext);
    }
    public void TapToStart()
    {
        mainMenuBackGround.DOScale(2,1);
        mainMenuBackGround.DOLocalMoveY(300,1);
        tapToStart.SetActive(false);
        menu.SetActive(true);
    }
    public void Quit()
    {
        tapToStart.SetActive(true);
        menu.SetActive(false);
        selectHero.SetActive(false);
        mainMenuBackGround.DOScale(1, 1);
        mainMenuBackGround.DOLocalMoveY(0, 1);
        Debug.Log("Quit");
    }
    public void NewGame()
    {
        menu.SetActive(false);
        selectHero.SetActive(true);
        gameobjectBackMenu.SetActive(true);
    }
    public void SelectPrev()
    {
        showHero.Prev();
    }
    public void SelectNext()
    {
        showHero.Next();
    }
    public void LoadGame()
    {
        menu.SetActive(false);
        gameobjectBackMenu.SetActive(true);
    }
    public void ButtonBackMenu()
    {
        menu.SetActive(true);
        selectHero.SetActive(false);
        menuLoadGame.SetActive(false);
        gameobjectBackMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
