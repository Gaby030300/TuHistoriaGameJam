using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUIManager : MonoBehaviour
{
    public GameObject contractUIPrefab; 
    public Transform contractUIParent; 
    public List<Contract> contractsToDisplay; 

    private void Start()
    {
        LoadContracts(); 

        foreach (var contract in contractsToDisplay)
        {
            CreateContractUI(contract);
        }
    }

    private void CreateContractUI(Contract contract)
    {
        GameObject contractUI = Instantiate(contractUIPrefab, contractUIParent);
        ContractUI contractUIScript = contractUI.GetComponent<ContractUI>();

        if (contractUIScript != null)
        {
            contractUIScript.FillContractUI(contract);
        }
        else
        {
            Debug.LogError("ContractUI script not found on the Contract UI prefab!");
        }
    }

    private void LoadContracts()
    {
        contractsToDisplay = new List<Contract>();

        if (PlayerPrefs.HasKey("Contracts"))
        {
            string contractsJson = PlayerPrefs.GetString("Contracts");
            contractsToDisplay = JsonUtility.FromJson<List<Contract>>(contractsJson);
        }
    }
}