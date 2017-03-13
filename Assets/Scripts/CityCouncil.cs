using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCouncil : MonoBehaviour {
    public int seats;
    public string mayor;
    public int lastElectionYear;
    public List<CityParty> parties;

    List<CityParty> defaultParties = new List<CityParty>
    (new CityParty[] {
        new CityParty("Red", "Tom Atoll"),
        new CityParty("Blue", "Barry B."),
        new CityParty("Green", "Ada Sal")
    });

    [Header("Calculated")]
    public List<string> PartyPopularity;
    public float totalVotes;

    private void Reset()
    {
        seats = 11;
        
        parties = defaultParties;
        
    }

    private void OnValidate()
    {
        totalVotes = 0;
        foreach (CityParty p in parties)
        {
            totalVotes += p.votes;
        }
        if (totalVotes > 100)
        {
            float subtract = 100 - totalVotes;

           
        }
    }
}

[System.Serializable]
public class CityParty : System.IComparable<CityParty>
{
    public string name;
    public string candidate;
    public int votes;

    public CityParty(string _name, string _candidate)
    {
        name = _name;
        candidate = _candidate;
    }

    public int CompareTo(CityParty obj)
    {
        if (obj == null) return 1;

        return votes.CompareTo(obj.votes);
    }
}