using UnityEngine;
using System.Collections;

public class RankUpIconController : MonoBehaviour
{

    public UIRankScroller uIRankScroller;
    public Sprite[] rewards;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        CheckIcon();
    }

    void CheckIcon()
    {
        //sr.sprite = rewards[PlayerPersistentData.Instance.Level - 1];

        var get = UIRankScroller.rewardSprites.Find((item) => item.level == PlayerPersistentData.Instance.Level);

        sr.sprite = get.sprite;
    }
}
