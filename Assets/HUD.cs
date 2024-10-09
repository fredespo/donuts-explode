using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private ScoreBonus scoreBonus;
    [SerializeField] private Lives lives;

    [SerializeField] private GameOverUI levelLostUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject pauseButton;
    private Animator animator;
    [SerializeField] private AudioSource coffeeDrinkSound;
    [SerializeField] private AudioSource coffeeSipSound;
    private int coffeeSipNumber = 0;
    [SerializeField] private int totalCoffeeSips = 3;
    [SerializeField] private EventChannel<SipCoffeeEvent> coffeeSipEventChannel;

    public void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void OnLevelLost()
    {
        if (this.scoreBonus != null)
        {
            this.scoreBonus.Reset();
        }
        StartCoroutine(UpdateUiOnLevelLost());
    }

    private IEnumerator UpdateUiOnLevelLost()
    {
        if (this.lives != null)
        {
            if (this.lives.GetLivesLeft() > 0)
            {
                float explosionTime = 1.3f;
                if (this.score.GetScore() > 0)
                {
                    yield return DeductPoints(explosionTime, 0.6f, 0.5f);
                }
                else
                {
                    yield return new WaitForSeconds(explosionTime);
                }
                this.levelLostUI.ShowAfterDelay(0.3f);
            }
            else if (this.gameOverUI != null)
            {
                this.gameOverUI.ShowAfterDelay(1.0f);
            }
        }
        yield return new WaitForSeconds(0);
    }

    public void DrinkCoffee()
    {
        this.animator.Play("HighlightLivesLeft");
        this.coffeeSipNumber = 0;
        RaiseSipCoffeeEvent();
    }

    public void TakeSipOfCoffee() {
        this.coffeeSipNumber++;
        RaiseSipCoffeeEvent();
    }

    private void RaiseSipCoffeeEvent() {
        this.coffeeSipEventChannel.RaiseEvent(
            new SipCoffeeEvent(this.coffeeSipNumber, this.totalCoffeeSips)
        );
    }

    public void PlayCoffeeDrinkSound() {
        this.coffeeDrinkSound.Play();
    }

    public void PlayCoffeeSipSound() {
        this.coffeeSipSound.Play();
    }

    public void DecrementLives()
    {
        this.lives.Decrement();
    }

    public void DoneDrinkingCoffee()
    {
        this.animator.Play("Default");
        this.levelLoader.ResetCurrentLevel(() => this.levelLoader.StartCurrentLevelAfterDelaySec(0.1f), () => { });
    }

    private IEnumerator DeductPoints(float initialDelay, float duration, float delayBeforeGoingBack)
    {
        float scoreStartX = score.GetPos().x;
        float scoreStartY = score.GetPos().y;
        int scoreStartFontSize = score.GetFontSize();
        this.score.StartMoveToHorizontalCenterCoroutineAfterDelay(duration, initialDelay);
        this.score.MoveYPos(-320f, duration, initialDelay);
        this.score.TweenFontSize(120, duration, initialDelay);
        this.score.SetSnapToCenter(true, initialDelay + duration);

        float delayBeforeScoreChange = 0.3f;
        float timeToChangeScore = Mathf.Min(score.maxTimeToChange, (float)Score.CalcScoreAfterLoss(score.GetScore()) / score.scoreChangePerSec) + delayBeforeScoreChange;
        float totalTime = initialDelay + duration + timeToChangeScore + delayBeforeGoingBack + duration + delayBeforeScoreChange;
        float startGoingBackTime = initialDelay + duration + timeToChangeScore + delayBeforeGoingBack;
        this.score.SetSnapToCenter(false, startGoingBackTime);
        this.score.MoveYPos(scoreStartY, duration, startGoingBackTime);
        this.score.MoveXPos(scoreStartX, duration, startGoingBackTime);
        this.score.TweenFontSize(scoreStartFontSize, duration, startGoingBackTime);

        this.score.AddAfterDelay(-1 * (int)Mathf.Ceil((float)score.GetScore() / 2), initialDelay + duration + delayBeforeScoreChange);
        yield return new WaitForSeconds(totalTime);
    }

    public void HandleBonusLevelEvent(string value) {
        if (value == LevelState.PLAY_STARTED) {
            this.pauseButton.SetActive(true);
        } else if (value == LevelState.PLAY_ENDED) {
            this.pauseButton.SetActive(false);
        }
    }
}
