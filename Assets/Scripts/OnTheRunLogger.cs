using System;
using System.Collections.Generic;
using UnityEngine;
using SBS.Core;

[AddComponentMenu("OnTheRun/OnTheRunLogger")]
public class OnTheRunLogger : MonoBehaviour
{
    #region Singleton instance
    protected static OnTheRunLogger instance = null;

    public static OnTheRunLogger Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Protected methods
    #endregion

    #region Messages
    void OnStartGame()
    {
    }

    void OnGameover()
    {
    }
    #endregion

    #region Public methods
    public void LogBuy(String category, String id, int owned, int nuggets, int rank)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("category", category);
        values.Add("id", id);
        values.Add("owned", owned.ToString());
        values.Add("nggts", nuggets.ToString());
        values.Add("rank", rank.ToString());
    }

    public void LogInAppPurchase(String id, int nuggets, int rank)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("id", id);
        values.Add("nggts", nuggets.ToString());
        values.Add("rank", rank.ToString());
    }

    public void LogPage(String screenId)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("id", screenId);
    }

    public void LogPageTimed(String screenId)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("id", screenId);
    }

    public void EndPageTimed()
    {
    }

    public void LogClick(String id)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("id", id);
    }

    public void LogMissionDone(String id, int totalMissions)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("id", id);
        values.Add("totalMissions", totalMissions.ToString());
    }

    public void LogNewRank(int rank, int bestRank)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("rank", rank.ToString());
        values.Add("bestRank", bestRank.ToString());
    }

    public void LogRockeggGift()
    {
    }

    public void LogGemTaken(String gemType)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("type", gemType);
    }

    public void LogEnvironmentChange(String envType)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("type", envType);
    }

    public void LogDie(String obstacleType)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("type", obstacleType);
    }

    public void LogPlay(String charaId, int rank, int totalPlays, int totalMissionsDone, int totalMeters, int nuggetsEarned, int bestMeters)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("characterId", charaId);
        values.Add("rank", rank.ToString());
        values.Add("totalPlays", totalPlays.ToString());
        values.Add("totalMissions", totalMissionsDone.ToString());
        values.Add("totalMeters", totalMeters.ToString());
        values.Add("totalNuggetsEarned", nuggetsEarned.ToString());
        values.Add("bestMeters", bestMeters.ToString());
    }

    public void EndPlay()
    {
		print ("---------End Play---------------");
    }

    public void LogPlayData(int meters, int nuggets, int nuggetsEarnedPlay, int nuggetsEarned, int rank, float velocity, int bestMeters, int numJumps, int numCrouches, int numTrackChanged)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("meters", meters.ToString());
        values.Add("nuggets", nuggets.ToString());
        values.Add("nuggetsEarnedPlay", nuggetsEarnedPlay.ToString());
        values.Add("nuggetsEarned", nuggetsEarned.ToString());
        values.Add("rank", rank.ToString());
        values.Add("velocity", velocity.ToString("0.00"));
        values.Add("bestMeters", bestMeters.ToString());
        values.Add("numJumps", numJumps.ToString());
        values.Add("numCrouches", numCrouches.ToString());
        values.Add("numTrackChanged", numTrackChanged.ToString());
    }

    public void LogPlayDataMeters(int totalMeters, int totalMetersOnLeft, int totalMetersOnCenter, int totalMetersOnRight, int totalMetersTiltRight, int totalMetersTiltLeft)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("totalMeters", totalMeters.ToString());
        values.Add("totalMetersOnLeft", totalMetersOnLeft.ToString());
        values.Add("totalMetersOnCenter", totalMetersOnCenter.ToString());
        values.Add("totalMetersOnRight", totalMetersOnRight.ToString());
        values.Add("totalMetersTiltRight", totalMetersTiltRight.ToString());
        values.Add("totalMetersTiltLeft", totalMetersTiltLeft.ToString());
    }

    public void LogPlayDataObstacles(int obstaclesUpperDone, int obstaclesLowerDone, int obstaclesRightDone, int obstaclesLeftDone)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("upper", obstaclesUpperDone.ToString());
        values.Add("lower", obstaclesLowerDone.ToString());
        values.Add("right", obstaclesRightDone.ToString());
        values.Add("left", obstaclesLeftDone.ToString());
    }
    #endregion

    #region Unity callbacks
    void Awake()
    {
        //.Assert(null == instance);
        instance = this;
    }

    void OnDestroy()
    {
        //.Assert(this == instance);
        instance = null;
    }
    #endregion
}
