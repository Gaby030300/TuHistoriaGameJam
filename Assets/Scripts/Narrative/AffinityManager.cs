using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Yarn.Unity;

public class AffinityManager : MonoBehaviour
{
    private Dictionary<string, float> affinityEntries = new Dictionary<string, float>();
    private List<Contract> contracts = new List<Contract>();

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string, float>("affinity", NewAffinityEntry);
    }

    public void NewAffinityEntry(string name, float value)
    {
        affinityEntries.Add(name, value);
        Debug.Log($"Name: {name} value: {value}");

        // Generate contract based on affinity value if it's above a certain threshold
        if (value >= 10)
        {
            Contract jobContract = GenerateContract(name, value);
            contracts.Add(jobContract);
        }
        else if (name == "Angen") // Always generate a specific contract for "Angen"
        {
            Contract angensContract = new Contract
            {
                InviteOrOfferFrom = "Angen",
                Position = "Big U Instructor",
                DailyHours = 12,
                Salary = "$$",
                Comments = "Hope you won't be a creepy one like the last one",
                Type = Contract.ContractType.Bootcamp
            };
            contracts.Add(angensContract);
        }
        else
        {
            Debug.Log("Affinity value too low for a contract.");
            // Handle cases where affinity is too low for a contract
        }
    }

    private Contract GenerateContract(string name, float value)
    {
        Contract contract = new Contract();
        contract.InviteOrOfferFrom = name; // Store the name in the contract

        int opportunitySelector = Random.Range(0, 4); // Randomly select an opportunity type

        if (value <= 10)
        {
            if (opportunitySelector >= 4)
            {
                // Coffee Deliverer
                contract.Position = "Coffee Deliverer";
                contract.DailyHours = Random.Range(4, 7); // Varying hours
                contract.Salary = "$";
                contract.Type = Contract.ContractType.BadJob;
            }
        }
        else if (value <= 20)
        {
            // Assistant or similar roles
            string[] assistantRoles = { "Assistant", "Junior Assistant", "Technical Assistant" };
            contract.Position = assistantRoles[Random.Range(0, assistantRoles.Length)];
            contract.DailyHours = Random.Range(6, 9); // Varying hours
            contract.Salary = "$$";
            contract.Type = Contract.ContractType.MidJob;
        }
        else if (value <= 40)
        {
            // Game Developer or similar roles
            string[] developerRoles = { "Game Developer", "Software Engineer", "U Developer" };
            contract.Position = developerRoles[Random.Range(0, developerRoles.Length)];
            contract.DailyHours = Random.Range(7, 10); // Varying hours
            contract.Salary = "$$$";
            contract.Type = Contract.ContractType.MidJob;
        }
        else
        {
            // Tech Artist or similar roles
            string[] artistRoles = { "Tech Artist", "Game Designer", "Level Designer" };
            contract.Position = artistRoles[Random.Range(0, artistRoles.Length)];
            contract.DailyHours = Random.Range(6, 11); // Varying hours
            contract.Salary = "$$$";
            contract.Type = Contract.ContractType.GoodJob;
        }

        return contract;
    }
    private void SaveContracts()
    {
        string contractsJson = JsonUtility.ToJson(contracts);
        PlayerPrefs.SetString("Contracts", contractsJson);
        PlayerPrefs.Save();
    }
}

// Contract class to store job details
public class Contract
{
    public string InviteOrOfferFrom { get; set; } // Person's name offering the job
    public string Position { get; set; }
    public int DailyHours { get; set; }
    public string Salary { get; set; }
    public string Comments { get; set; } // Additional comments for the contract
    public ContractType Type; // New variable to store ContractType

    public enum ContractType
    {
        EndDialogue,
        NoJob,
        BadJob,
        MidJob,
        GoodJob,
        Bootcamp
    }
}
