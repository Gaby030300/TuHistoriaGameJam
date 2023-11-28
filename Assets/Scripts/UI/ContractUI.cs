using UnityEngine;
using TMPro;

public class ContractUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text positionText;
    public TMP_Text hoursText;
    public TMP_Text salaryText;
    public TMP_Text commentsText;

    public void FillContractUI(Contract contract)
    {
        nameText.text = contract.InviteOrOfferFrom;
        positionText.text = contract.Position;
        hoursText.text = "Hours: " + contract.DailyHours.ToString();
        salaryText.text = "Salary: " + contract.Salary;
        commentsText.text = "Comments: " + contract.Comments;
    }
}

